namespace Order.ApplicationCore.Events
{
    public class OrderCompletedEvent
    {
        // Order Info
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal BillAmount { get; set; }
        public string OrderStatus { get; set; } = string.Empty;

        // Customer Info
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;

        // Payment Info
        public int PaymentMethodId { get; set; }
        public string PaymentName { get; set; } = string.Empty;

        // Shipping Info
        public string ShippingAddress { get; set; } = string.Empty;
        public string ShippingMethod { get; set; } = string.Empty;

        // Order Items
        public List<OrderDetailDto> OrderDetails { get; set; } = new();
    }

    public class OrderDetailDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public float Discount { get; set; }
    }
}