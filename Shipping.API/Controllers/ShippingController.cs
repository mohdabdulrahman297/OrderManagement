using Microsoft.AspNetCore.Mvc;
using Shipping.ApplicationCore.Contracts.Services;
using Shipping.ApplicationCore.DTOs;

namespace Shipping.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShippingController : ControllerBase
{
    private readonly IOrderServiceClient _orderServiceClient;

    public ShippingController(IOrderServiceClient orderServiceClient)
    {
        _orderServiceClient = orderServiceClient;
    }

    [HttpPatch("{orderId:int}/status")]
    public async Task<IActionResult> UpdateShippingStatus(int orderId, [FromBody] ShippingStatusUpdateDto dto)
    {
        if (orderId != dto.OrderId)
            return BadRequest("Order ID in URL does not match request body.");

        if (dto.Status != "Shipped" && dto.Status != "Delivered")
            return BadRequest("Status must be either 'Shipped' or 'Delivered'.");

        var success = await _orderServiceClient.UpdateShippingStatusAsync(dto);

        if (!success)
            return StatusCode(500, new { message = "Failed to update Order microservice." });

        return Ok(new
        {
            message = $"Shipping status for Order {dto.OrderId} updated to '{dto.Status}' in Order API ✅"
        });
    }
}