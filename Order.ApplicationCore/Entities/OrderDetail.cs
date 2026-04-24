using System;
using System.Collections.Generic;
using System.Text;

namespace Order.ApplicationCore.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public float Discount { get; set; }

        //belongs to one Order
        public Order Order { get; set; } = null!;
    }
}
