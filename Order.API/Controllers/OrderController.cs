using Microsoft.AspNetCore.Mvc;
using Order.ApplicationCore.Contracts.Services;
using OrderEntity = Order.ApplicationCore.Entities.Order;

namespace Order.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
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
}