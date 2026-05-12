namespace Promotion.ApplicationCore.Events
{
    public class PromotionEndedEvent
    {
        public int PromotionId { get; set; }
        public string PromotionName { get; set; } = string.Empty;
        public decimal DiscountPercent { get; set; }
        public DateTime EndedAt { get; set; } = DateTime.UtcNow;
    }
}