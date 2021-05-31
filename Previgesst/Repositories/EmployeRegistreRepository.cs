using Previgesst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class EmployeRegistreRepository:RepositoryBase<EmployeRegistre>
    {
        public EmployeRegistreRepository(DbContext context) : base(context)
        {
        }
        public override EmployeRegistre Get(int id)
        {
            return this.dbSet.Where(x => x.EmployeRegistreId == id).Include(x => x.Departement).FirstOrDefault();
        }

    }
}