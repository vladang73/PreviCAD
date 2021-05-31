using Previgesst.Repositories;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Previgesst.Services
{
    public class SituationService
    {
        private SituationRepository situationRepository;

        public SituationService(SituationRepository situationRepository)
        {
            this.situationRepository = situationRepository;
        }

        public IEnumerable<SituationDDLViewModel> GetAllAsSituationDDLViewModel()
        {
            var langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            var situationDDL = situationRepository.AsQueryable().Select(s => new SituationDDLViewModel() {
                SituationId = s.SituationId,
                Description = langue == "fr" ? s.Description : s.DescriptionEN
            }).OrderBy(s => s.Description);

            return situationDDL;
        }
    }
}