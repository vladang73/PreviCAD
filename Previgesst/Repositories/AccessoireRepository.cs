using Previgesst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class AccessoireRepository:RepositoryBase<Accessoire>
    {
        public AccessoireRepository(DbContext context) : base(context) { }

        public bool estUtilise(int id)
        {
            var item = this.dbSet.Where(x => x.AccessoireId == id).Include(x => x.Instructions).FirstOrDefault();
            var nbUtilisations = item.Instructions.Where(x => x.AccessoireId == id).Count();
            return (nbUtilisations != 0);


        }
    }
}