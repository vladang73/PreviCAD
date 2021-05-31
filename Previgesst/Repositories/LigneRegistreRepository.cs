using Previgesst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class LigneRegistreRepository:RepositoryBase<LigneRegistre>
    {
        public LigneRegistreRepository(DbContext context) : base(context)
        { 
        }

        public override LigneRegistre Get(int id)
        {
            var item = this.AsQueryable().Include(x=>x.EmployeRegistre.Departement).Include(x => x.FicheCadenassage).Include(f => f.FicheCadenassage.Departement).Include(x => x.FicheCadenassage.Equipement).Include(x => x.EmployeRegistre).Where(x => x.LigneRegistreId == id).FirstOrDefault();
            return item;
        }
    }
}