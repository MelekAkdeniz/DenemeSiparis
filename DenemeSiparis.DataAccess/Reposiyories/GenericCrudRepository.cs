using DenemeSiparis.DataAccess.Abstractirons;
using DenemeSiparis.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DenemeSiparis.DataAccess.Reposiyories
{
    public class GenericCrudRepository<T> : ICrudRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericCrudRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Create(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var data = _dbSet.Find(id);
            if (data != null)
            {
                _dbSet.Remove(data);
                _context.SaveChanges();
            }
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetByID(Guid id)
        {

            var data = _dbSet.Find(id);
            if (data == null)
            {
                throw new Exception("Bulunamadı.");
            }
            return data;
        }

        public bool IfEntityExists(Expression<Func<T, bool>> filter)
        {
            return _dbSet.Any(filter);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
