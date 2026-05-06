using System;
using System.Collections.Generic;
using System.Text;

namespace Product.ApplicationCore.Entities
{
    public class ProductDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string AttributeName { get; set; } = string.Empty;
        public string AttributeValue { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;

        // belongs to one Product
        public Product Product { get; set; } = null!;
    }
}