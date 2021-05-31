using Previgesst.Repositories;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Previgesst.Services
{
    public class ProbabiliteService
    {
        private ProbabiliteRepository probabiliteRepository;

        public ProbabiliteService(ProbabiliteRepository probabiliteRepository)
        {
            this.probabiliteRepository = probabiliteRepository;
        }

        public IEnumerable<ProbabiliteDDLViewModel> GetAllAsProbabiliteDDLViewModel()
        {
            var probabiliteDDL = probabiliteRepository.AsQueryable().Select(p => new ProbabiliteDDLViewModel() {
                ProbabiliteId = p.ProbabiliteId,
                No = p.No,
                Explication = p.Explication
            }).OrderBy(p => p.ProbabiliteId);

            return probabiliteDDL;
        }
    }
}