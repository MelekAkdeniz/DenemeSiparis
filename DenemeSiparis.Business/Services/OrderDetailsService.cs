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
    public class OrderDetailsService : ICrudRepository<OrderDetail>
    {
        private readonly OrderDetailsRepository _orderDetailRepository;
        public OrderDetailsService(OrderDetailsRepository odRepo)
        {
            _orderDetailRepository = odRepo;   
        }
        public void Create(OrderDetail entity)
        {
            _orderDetailRepository.Create(entity);

        }

        public void Delete(Guid id)
        {
            var obj = _orderDetailRepository.GetByID(id);
            if (obj == null)
            {
                throw new Exception("Sipariş Detayı Bulunamadı.");
            }
            _orderDetailRepository.Delete(id);

        }

        public IEnumerable<OrderDetail> GetAll()
        {
            return _orderDetailRepository.GetAll();

        }

        public OrderDetail GetByID(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new Exception("ID bilgisi boş olamaz.");
            }
            return _orderDetailRepository.GetByID(id);

        }

        public bool IfEntityExists(Expression<Func<OrderDetail, bool>> filter)
        {
            return _orderDetailRepository.IfEntityExists(filter);

        }

        public void Update(OrderDetail entity)
        {
            if (entity == null)
            {
                throw new Exception("Sipariş Detayı boş olamaz.");
            }
            _orderDetailRepository.Update(entity);

        }
    }
}
