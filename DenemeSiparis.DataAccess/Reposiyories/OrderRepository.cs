using DenemeSiparis.DataAccess.Context;
using DenemeSiparis.Entities.Models;

namespace DenemeSiparis.DataAccess.Reposiyories
{
    public class OrderRepository : GenericCrudRepository<Order>
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }
    }
}
