namespace Shipping.ApplicationCore.DTOs
{
    public class ShippingStatusUpdateDto
    {
        public int OrderId { get; set; }
        public string Status { get; set; } = string.Empty;   // Shipped / Delivered
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public string TrackingNumber { get; set; } = string.Empty;
    }
}