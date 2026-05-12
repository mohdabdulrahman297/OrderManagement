using Promotion.ApplicationCore.Events;

namespace Promotion.ApplicationCore.Contracts.Services
{
    public interface IPromotionPublisher
    {
        Task PublishPromotionStartedAsync(PromotionStartedEvent promotionEvent);
        Task PublishPromotionEndedAsync(PromotionEndedEvent promotionEvent);
    }
}