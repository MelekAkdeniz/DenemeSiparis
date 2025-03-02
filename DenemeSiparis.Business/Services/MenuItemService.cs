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
    public class MenuItemService : ICrudRepository<MenuItem>
    {
        private readonly MenuItemRpository _menuItemRepository;
        public MenuItemService(MenuItemRpository mRepo)
        {
            _menuItemRepository = mRepo;
        }
        public void Create(MenuItem entity)
        {
            _menuItemRepository.Create(entity);
        }

        public void Delete(Guid id)
        {
            var obj = _menuItemRepository.GetByID(id);
            if (obj == null)
            {
                throw new Exception("Menü Bulunamadı.");
            }
            _menuItemRepository.Delete(id);

        }

        public IEnumerable<MenuItem> GetAll()
        {
            return _menuItemRepository.GetAll();
        }

        public MenuItem GetByID(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new Exception("ID bilgisi boş olamaz.");
            }
            return _menuItemRepository.GetByID(id);

        }

        public bool IfEntityExists(Expression<Func<MenuItem, bool>> filter)
        {
            return _menuItemRepository.IfEntityExists(filter);

        }

        public void Update(MenuItem entity)
        {
            if (entity == null)
            {
                throw new Exception("Menü bilgisi boş olamaz.");
            }
            _menuItemRepository.Update(entity);

        }
    }
}
