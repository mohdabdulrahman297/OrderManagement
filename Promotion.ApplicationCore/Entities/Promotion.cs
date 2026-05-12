namespace Promotion.ApplicationCore.Entities
{
    public class Promotion
    {
        public int Id { get; set; }
        public string PromotionName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal DiscountPercent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = "Pending";
    }
}