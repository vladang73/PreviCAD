using Previgesst.Repositories;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Previgesst.Services
{
    public class PhenomeneService
    {
        private PhenomeneRepository phenomeneRepository;

        public PhenomeneService(PhenomeneRepository phenomeneRepository)
        {
            this.phenomeneRepository = phenomeneRepository;
        }

        public IEnumerable<PhenomeneDDLViewModel> GetAllAsPhenomeneDDLViewModel()
        {
            var langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            var phenomenesDDL = phenomeneRepository.AsQueryable().Select(p => new PhenomeneDDLViewModel()
            {
                PhenomeneId = p.PhenomeneId,
                Description = langue=="fr"? p.Description:p.DescriptionEN
            }).OrderBy(p => p.Description);

            return phenomenesDDL;
        }
    }
}