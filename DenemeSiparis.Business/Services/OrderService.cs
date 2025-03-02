using DenemeSiparis.DataAccess.Abstractirons;
using DenemeSiparis.DataAccess.Reposiyories;
using DenemeSiparis.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DenemeSiparis.Business.Services
{
    public class OrderService : ICrudRepository<Order>
    {
        private readonly OrderRepository _orderRepository;
        public OrderService(OrderRepository oRepo)
        {
            _orderRepository = oRepo;
        }
        public void Create(Order entity)
        {
            _orderRepository.Create(entity);
        }

        public void Delete(Guid id)
        {
            var obj = _orderRepository.GetByID(id);

            if (obj == null)
            {
                throw new Exception("Sipariş Bulunamadı.");
            }

            _orderRepository.Delete(id);
        }

        public IEnumerable<Order> GetAll()
        {
            return _orderRepository.GetAll();
        }

        public Order GetByID(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new Exception("ID bilgisi boş olamaz.");
            }

            return _orderRepository.GetByID(id);
        }

        public bool IfEntityExists(Expression<Func<Order, bool>> filter)
        {
            return _orderRepository.IfEntityExists(filter);
        }

        public void Update(Order entity)
        {

            if (entity == null)
            {
                throw new Exception("Entity null olamaz.");
            }

            _orderRepository.Update(entity);
        }
    }
}
