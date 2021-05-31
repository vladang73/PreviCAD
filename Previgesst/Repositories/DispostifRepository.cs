using Previgesst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class DispositifRepository:RepositoryBase<Dispositif>
    {
        public DispositifRepository(DbContext context) : base(context) { }

        public bool estUtilise(int id)
        {
            var item = this.dbSet.Where(x => x.DispositifId == id).Include(x => x.Instructions).FirstOrDefault();
            var nbUtilisations = item.Instructions.Where(x => x.DispositifId == id).Count();
            return (nbUtilisations != 0);
        }


      
    }
}