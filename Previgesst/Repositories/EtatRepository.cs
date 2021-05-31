using Previgesst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class EtatRepository : RepositoryBase<Etat>
    {
        public EtatRepository(DbContext context) : base(context) {

        }

        public bool estUtilise(int id)
        {
            var item = this.dbSet.Where(x => x.EtatId == id).Include(x => x.LignesAnalysesRisque).FirstOrDefault();

            return (item.LignesAnalysesRisque.Count != 0);
        }
    }
}