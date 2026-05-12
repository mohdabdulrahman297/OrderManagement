namespace Promotion.ApplicationCore.Events
{
    public class PromotionStartedEvent
    {
        public int PromotionId { get; set; }
        public string PromotionName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal DiscountPercent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime PublishedAt { get; set; } = DateTime.UtcNow;
    }
}