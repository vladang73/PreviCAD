using Previgesst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class MaterielRepository : RepositoryBase<Materiel>

    {
        public MaterielRepository(DbContext context) : base(context) { }

        public bool estUtilise(int id)
        {


            var item = this.dbSet.Where(x => x.MaterielId == id).Include(x => x.MaterielRequisCadenassage).FirstOrDefault();
            var nbUtilisations = item.MaterielRequisCadenassage.Where(x => x.MaterielId == id).Count();
            return (nbUtilisations != 0);
        }
    }
}