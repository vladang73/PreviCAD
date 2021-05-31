using Previgesst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class ReglementRepository:RepositoryBase<Reglement>
    {
        public ReglementRepository(DbContext context) : base(context) { }


        public bool estUtilise(int id)
        {
            // var item = this.dbSet.Where(x => x.ReglementId == id).Include(x => x.LigneAnalyseRisque).FirstOrDefault();

            // return (item.LigneAnalyseRisque.Count != 0);
            return false;
        }
    }
}