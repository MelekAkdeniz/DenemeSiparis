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
    public class TableService : ICrudRepository<Table>
    {
        private readonly TableRepository _tableRepository;

        public TableService(TableRepository tRepo)
        {
            _tableRepository = tRepo;   

        }
        public void Create(Table entity)
        {
            _tableRepository.Create(entity);

        }

        public void Delete(Guid id)
        {
            var obj = _tableRepository.GetByID(id);

            if (obj == null)
            {
                throw new Exception("Sipariş Bulunamadı.");
            }

            _tableRepository.Delete(id);


        }

        public IEnumerable<Table> GetAll()
        {
            return _tableRepository.GetAll();

        }

        public Table GetByID(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new Exception("ID bilgisi boş olamaz.");
            }

            return _tableRepository.GetByID(id);

        }

        public bool IfEntityExists(Expression<Func<Table, bool>> filter)
        {
            return _tableRepository.IfEntityExists(filter);

        }

        public void Update(Table entity)
        {
            if (entity==null)
            {
                throw new Exception("Entity null olamaz.");

            }
            _tableRepository.Update(entity);
            
        }
    }
}
