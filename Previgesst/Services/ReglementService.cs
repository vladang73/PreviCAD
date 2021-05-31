using Previgesst.Repositories;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Previgesst.Services
{
    public class ReglementService
    {
        public ReglementRepository reglementRepository;

        public ReglementService(ReglementRepository reglementRepository)
        {
            this.reglementRepository = reglementRepository;
        }

        public IEnumerable<ReglementDDLViewModel> GetAllAsReglementDDLViewModel()
        {
            var reglementDDL = reglementRepository.AsQueryable().Select(r => new ReglementDDLViewModel(){
                ReglementId = r.ReglementId,
                Description = r.Description
            }).OrderBy(r => r.Description);

            return reglementDDL;
        }
    }
}