using System;
using System.Collections.Generic;
using System.Text;

namespace Product.ApplicationCore.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string SKU { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        // one Product has many ProductDetails
        public ICollection<ProductDetail> ProductDetails { get; set; } = new List<ProductDetail>();
    }
}