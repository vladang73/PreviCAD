using Previgesst.Repositories;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Previgesst.Services
{
    public class SourceEnergieService
    {
        private SourceEnergieRepository sourceEnergieRepository;
        public SourceEnergieService(SourceEnergieRepository sourceEnergieRepository)
        {
            this.sourceEnergieRepository = sourceEnergieRepository;

        }
        public IEnumerable<SourceEnergieDDLViewModel> GetAllAsSourceEnergieDDLViewModel()
        {
            var langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
           
                var item = sourceEnergieRepository.AsQueryable().Select(p => new SourceEnergieDDLViewModel()
                {
                    SourceEnergieId = p.SourceEnergieId,
                    Description =langue=="fr"? p.Description:p.DescriptionEN
                }).OrderBy(p => p.Description);

                return item;
         
            
        }

    }
}