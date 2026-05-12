using Microsoft.AspNetCore.Mvc;
using Promotion.ApplicationCore.Contracts.Services;
using Promotion.ApplicationCore.Events;
using PromotionEntity = Promotion.ApplicationCore.Entities.Promotion;

namespace Promotion.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PromotionController : ControllerBase
{
    private readonly IPromotionPublisher _publisher;

    public PromotionController(IPromotionPublisher publisher)
    {
        _publisher = publisher;
    }

    // POST api/promotion/start
    [HttpPost("start")]
    public async Task<IActionResult> StartPromotion([FromBody] PromotionEntity promotion)
    {
        var promotionEvent = new PromotionStartedEvent
        {
            PromotionId = promotion.Id,
            PromotionName = promotion.PromotionName,
            Description = promotion.Description,
            DiscountPercent = promotion.DiscountPercent,
            StartDate = promotion.StartDate,
            EndDate = promotion.EndDate
        };

        await _publisher.PublishPromotionStartedAsync(promotionEvent);

        return Ok(new { message = $"Promotion '{promotion.PromotionName}' started! Event published to Azure Service Bus ✅" });
    }

    // POST api/promotion/end
    [HttpPost("end")]
    public async Task<IActionResult> EndPromotion([FromBody] PromotionEntity promotion)
    {
        var promotionEvent = new PromotionEndedEvent
        {
            PromotionId = promotion.Id,
            PromotionName = promotion.PromotionName,
            DiscountPercent = promotion.DiscountPercent
        };

        await _publisher.PublishPromotionEndedAsync(promotionEvent);

        return Ok(new { message = $"Promotion '{promotion.PromotionName}' ended! Event published to Azure Service Bus ✅" });
    }
}