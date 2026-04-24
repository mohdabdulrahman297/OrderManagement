using System;
using System.Collections.Generic;
using System.Text;

namespace Order.ApplicationCore.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public int PaymentMethodId { get; set; }
        public string PaymentName { get; set; } = string.Empty;
        public string ShippingAddress { get; set; } = string.Empty;
        public string ShippingMethod { get; set; } = string.Empty;
        public decimal BillAmount { get; set; }
        public string OrderStatus { get; set; } = string.Empty;

        // one Order has many OrderDetails
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
