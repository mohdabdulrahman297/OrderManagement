namespace Order.ApplicationCore.DTOs
{
    public class ShippingStatusUpdateDto
    {
        public int OrderId { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime UpdatedAt { get; set; }
        public string TrackingNumber { get; set; } = string.Empty;
    }
}