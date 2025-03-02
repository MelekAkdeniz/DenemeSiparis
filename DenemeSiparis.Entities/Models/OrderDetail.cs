using DenemeSiparis.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeSiparis.Entities.Models
{
    public class OrderDetail : Entity
    {
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }

        public Guid MenuItemId { get; set; }
        public MenuItem? MenuItem { get; set; }

        public string? MenuItemName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }

}
