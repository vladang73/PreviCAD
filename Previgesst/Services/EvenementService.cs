using Previgesst.Repositories;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Previgesst.Services
{
    public class EvenementService
    {
        private EvenementRepository evenementRepository;

        public EvenementService(EvenementRepository evenementRepository)
        {
            this.evenementRepository = evenementRepository;
        }

        public IEnumerable<EvenementDDLViewModel> GetAllAsEvenementDDLViewModel()
        {
            var langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            var evenementDDL = evenementRepository.AsQueryable().Select(e => new EvenementDDLViewModel() {
                EvenementId = e.EvenementId,
                Description = langue=="fr"?e.Description:e.DescriptionEN
            }).OrderBy(e => e.Description);

            return evenementDDL;
        }
    }
}