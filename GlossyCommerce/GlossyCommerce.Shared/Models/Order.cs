using System;
using System.Collections.Generic;
using System.Text;

namespace GlossyCommerce.Shared.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;
        public string ShippingAddress { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending";

        // Navigation property
        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
