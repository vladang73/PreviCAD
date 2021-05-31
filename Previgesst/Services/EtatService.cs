using Previgesst.Repositories;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Previgesst.Services
{
    public class EtatService
    {
        private EtatRepository etatRepository;

        public EtatService(EtatRepository etatRepository)
        {
            this.etatRepository = etatRepository;
        }

        public IEnumerable<EtatDDLViewModel> GetAllAsEtatDDLViewModel()
        {
            var langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            var etatDDL = etatRepository.AsQueryable().Select(p => new EtatDDLViewModel()
            {
                EtatId = p.EtatId,
                Description = langue=="fr"? p.Description:p.DescriptionEN
            }).OrderBy(p => p.Description);

            return etatDDL;
        }
    }
}