using Previgesst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class SourceEnergieRepository:RepositoryBase<SourceEnergie>
    {
        public SourceEnergieRepository(DbContext context) : base(context) { }

        public bool estUtilise(int id)
        {
            var item = this.dbSet.Where(x => x.SourceEnergieId == id).Include(x => x.SourceEnergieCadenassage).FirstOrDefault();
            var nbUtilisations = item.SourceEnergieCadenassage.Where(x => x.SourceEnergieId == id).Count();
            return (nbUtilisations != 0);
        }
    }
}