using Previgesst.Repositories;
using Previgesst.ViewModels;
using Previgesst.ViewModels.DDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Previgesst.Services
{
    public class DommagePossibleService
    {
        private DommageRepository dommageRepository;

        public DommagePossibleService(DommageRepository dommageRepository)
        {
            this.dommageRepository = dommageRepository;
        }

        public IEnumerable<DommageDDLViewModel> GetAllAsDommageDDLViewModel()
        {
          
          
            var langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            var dommageDDL = dommageRepository.AsQueryable().Select(p => new DommageDDLViewModel()
            {
                DommagePossibleId = p.DommagePossibleId,
                Description = langue == "fr" ? p.Description : p.DescriptionEN
            }).OrderBy(p => p.Description);

            return dommageDDL;
        }


        public IEnumerable<EvenementDDLViewModel> GetAllAsEvenementDDLViewModel()
        {
            var langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            var evenementDDL = dommageRepository.AsQueryable().Select(e => new EvenementDDLViewModel()
            {
                EvenementId = e.DommagePossibleId,
                Description = langue == "fr" ? e.Description : e.DescriptionEN
            }).OrderBy(e => e.Description);

            return evenementDDL;
        }
    }
}