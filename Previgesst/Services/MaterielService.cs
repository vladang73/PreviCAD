using Previgesst.Repositories;
using Previgesst.ViewModels.DDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Previgesst.Services
{
    public class MaterielService
    {
        MaterielRepository materielRepository;
        public MaterielService(MaterielRepository materielRepository)
        {
            this.materielRepository = materielRepository;
        }

        public IEnumerable<MaterielDDLViewModel> GetAllMaterielDDLViewModel()
        {

            var langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            if (langue == "fr")
            {
                var applicationDDL = materielRepository.AsQueryable().OrderBy(s => s.Description).Select(s => new MaterielDDLViewModel()
                {

                    MaterielId = s.MaterielId,
                    Description = s.Description.Trim()
                });

                return applicationDDL;
            }
            else
            {
                var applicationDDL = materielRepository.AsQueryable().OrderBy(s => s.DescriptionEN).Select(s => new MaterielDDLViewModel()
                {

                    MaterielId = s.MaterielId,
                    Description = s.DescriptionEN.Trim()
                });
                return applicationDDL;
            }

        }
    }
}