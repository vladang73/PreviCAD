using Previgesst.Models;
using Previgesst.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Previgesst.Repositories
{
    public class AnalyseRisqueRepository : RepositoryBase<AnalyseRisque>
    {
        public AnalyseRisqueRepository(DbContext context) : base(context) { }



        public List<LigneAnalyseRapport> GetAnalyse(int id, string langue)
        {   if(langue=="fr")
                return context.Database.SqlQuery<LigneAnalyseRapport>
                (@"select * from vwLignesAnalyse where AnalyseRisqueId=" + id + " ORDER BY RANG, Equipement, Operation, Zone "  ).ToList();
            else
                return context.Database.SqlQuery<LigneAnalyseRapport>
              (@"select * from vwLignesAnalyseEN where AnalyseRisqueId=" + id + " ORDER BY RANG, Equipement, Operation, Zone ").ToList();


        }

        public List<LigneAnalyseRapport> GetAllAnalyses(int id, string langue)
        {   if (langue=="fr")
                return context.Database.SqlQuery<LigneAnalyseRapport>
                (@"select * from vwLignesAnalyse where  ClientId= " + id + " ORDER BY Description, RANG, Equipement, Operation, Zone ").ToList();
            else
                return context.Database.SqlQuery<LigneAnalyseRapport>
                    (@"select * from vwLignesAnalyseEN where  ClientId= " + id + " ORDER BY Description, RANG, Equipement, Operation, Zone ").ToList();

        }

    }
}