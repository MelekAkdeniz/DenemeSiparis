using DenemeSiparis.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeSiparis.Entities.Models
{
    public class Order : Entity
    {
        public int TableNumber;

        public DateTime OrderDate { get; set; }

        // Table ile ilişki
        public Guid TableId { get; set; }
        public Table? Table { get; set; }

        public string? Status { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Notes { get; set; }

        // Sipariş detayları
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
