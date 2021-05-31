using Previgesst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class PhenomeneRepository : RepositoryBase<Phenomene>
    {
        public PhenomeneRepository(DbContext context) : base(context) {
            
        }

        public bool estUtilise(int id)
        {
            var item = this.dbSet.Where(x => x.PhenomeneId == id).Include(x => x.LignesAnalysesRisque).FirstOrDefault();

            return (item.LignesAnalysesRisque.Count != 0);
        }
    }
}