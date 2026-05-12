using Microsoft.AspNetCore.Mvc;
using Order.ApplicationCore.Contracts.Services;
using Order.ApplicationCore.Events;
using OrderEntity = Order.ApplicationCore.Entities.Order;
using Order.ApplicationCore.DTOs;

namespace Order.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IMessagePublisher _messagePublisher;

    public OrderController(IOrderService orderService, IMessagePublisher messagePublisher)
    {
        _orderService = orderService;
        _messagePublisher = messagePublisher;
    }

    // a. GET all Orders
    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        return Ok(orders);
    }

    // b. POST - Save new Order
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderEntity order)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _orderService.CreateOrderAsync(order);
        return CreatedAtAction(nameof(GetOrderById), new { id = created.Id }, created);
    }

    // Helper for CreatedAtAction above
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        return order is null ? NotFound() : Ok(order);
    }

    // c. GET Order by Customer Id
    [HttpGet("customer/{customerId:int}")]
    public async Task<IActionResult> GetOrdersByCustomerId(int customerId)
    {
        var orders = await _orderService.GetOrdersByCustomerIdAsync(customerId);
        if (!orders.Any())
            return NotFound($"No orders found for customer {customerId}.");
        return Ok(orders);
    }

    // d. DELETE Order
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var deleted = await _orderService.DeleteOrderAsync(id);
        return deleted ? NoContent() : NotFound();
    }

    // e. PUT - Update Order
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderEntity order)
    {
        if (id != order.Id)
            return BadRequest("ID in URL does not match ID in body.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existing = await _orderService.GetOrderByIdAsync(id);
        if (existing is null)
            return NotFound();

        var updated = await _orderService.UpdateOrderAsync(order);
        return Ok(updated);
    }


    // f. POST - Complete Order & Publish to Azure Service Bus
    [HttpPost("{id:int}/complete")]
    public async Task<IActionResult> CompleteOrder(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order is null)
            return NotFound(new { message = $"Order {id} not found" });

        var orderEvent = new OrderCompletedEvent
        {
            // Order Info
            OrderId = order.Id,
            OrderDate = order.OrderDate,
            BillAmount = order.BillAmount,
            OrderStatus = "Completed",

            // Customer Info
            CustomerId = order.CustomerId,
            CustomerName = order.CustomerName,

            // Payment Info
            PaymentMethodId = order.PaymentMethodId,
            PaymentName = order.PaymentName,

            // Shipping Info
            ShippingAddress = order.ShippingAddress,
            ShippingMethod = order.ShippingMethod,

            // Order Items
            OrderDetails = order.OrderDetails.Select(d => new OrderDetailDto
            {
                ProductId = d.ProductId,
                ProductName = d.ProductName,
                Qty = d.Qty,
                Price = d.Price,
                Discount = d.Discount
            }).ToList()
        };

        await _messagePublisher.PublishOrderCompletedAsync(orderEvent);

        return Ok(new { message = $"Order {id} completed! Event published to Azure Service Bus ✅" });
    }

    // PATCH api/Order/{id}/shipping-status
    [HttpPatch("{id:int}/shipping-status")]
    public async Task<IActionResult> UpdateShippingStatus(int id, [FromBody] ShippingStatusUpdateDto dto)
    {
        if (id != dto.OrderId)
            return BadRequest("Order ID in URL does not match request body.");

        var order = await _orderService.GetOrderByIdAsync(id);
        if (order is null)
            return NotFound(new { message = $"Order {id} not found" });

        order.OrderStatus = dto.Status;

        var updated = await _orderService.UpdateOrderAsync(order);

        return Ok(new
        {
            message = $"Order {id} shipping status updated to '{dto.Status}' ✅",
            order = updated
        });
    }
}