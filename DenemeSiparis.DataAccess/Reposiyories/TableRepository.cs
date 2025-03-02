using DenemeSiparis.DataAccess.Context;
using DenemeSiparis.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeSiparis.DataAccess.Reposiyories
{
    public class TableRepository : GenericCrudRepository<Table>
    {
        public TableRepository(AppDbContext context) : base(context)
        {
        }
    }
}
