using Previgesst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class LigneAnalyseRisqueRepository : RepositoryBase<LigneAnalyseRisque>
    {
        internal List<bool> ConfimiteAuxNormesInt;
        private AnalyseRisqueRepository analyseRisqueRepository;

        public LigneAnalyseRisqueRepository(DbContext context, AnalyseRisqueRepository analyseRisqueRepository) : base(context) {
            this.analyseRisqueRepository = analyseRisqueRepository;
        }


        public override LigneAnalyseRisque Get(int id)
        {
            var ligne = this.dbSet.Where(x => x.LigneAnalyseRisqueId == id).Include(x => x.GraviteAvant).Include(x => x.GraviteApres).Include(x => x.FrequenceAvant).Include(x => x.FrequenceApres).Include(x => x.PossibiliteAvant).Include(x => x.PossibiliteApres).Include(x => x.ProbabiliteAvant).Include(x => x.ProbabiliteApres).FirstOrDefault();
            return ligne;

        }

        public void SupprimerDeFicheAnalyse(int AnalyseRisqueId)
        {
            var liste = analyseRisqueRepository.Get(AnalyseRisqueId).LignesAnalyseRisque.Select(x=>x.LigneAnalyseRisqueId).ToList();
            foreach (var i in liste)
                this.Delete(i);
            this.SaveChanges();

        }

        internal void Add(int idLigneAnalyseRisqueEditor)
        {
            throw new NotImplementedException();
        }
    }
}