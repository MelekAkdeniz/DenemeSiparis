using DenemeSiparis.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeSiparis.Entities.Models
{
    public class Table : Entity
    {
        
        public int TableNumber { get; set; }

        // Bu masa ile ilişkili olan siparişler
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
