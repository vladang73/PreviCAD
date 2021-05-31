using Previgesst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class DepartementRepository:RepositoryBase<Departement>
    {

        public DepartementRepository(DbContext context) : base (context) {

        }
        public override Departement Get(int id)
        {
            return dbSet.Include(Z => Z.Client).Where(x => x.DepartementId == id).FirstOrDefault();
        }
    }
}