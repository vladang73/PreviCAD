using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Previgesst.Repositories;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Previgesst.Services
{
    public class CadenasService
    {
        private CadenasRepository cadenasRepository;
        public CadenasService(CadenasRepository cadenasRepository)
        {
            this.cadenasRepository = cadenasRepository;
        }

        internal DataSourceResult GetReadCadenas(DataSourceRequest request, int clientId)
        {
            var result = cadenasRepository.AsQueryable().Select(x => new  CadenassageViewModel()
            {
              CadenasId= x.CadenasId,
              Departement = x.Departement,
              Equipement = x.Departement,
              NoFiche = x.NoFiche,
              Travail = x.Travail
            }).ToDataSourceResult(request);

            return result;
        }


        public IEnumerable<TitreFicheDDLViewModel> GetAllTitresCadenassageDDLViewModel()
        {
            var langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            var list = new List<TitreFicheDDLViewModel>();
            list.Add(new TitreFicheDDLViewModel { Texte = langue=="fr"? "Fiche de cadenassage": "Lockout card" });
            list.Add(new TitreFicheDDLViewModel { Texte = langue=="fr"? "Procédure de travail sécuritaire":"Safe work procedure" });
            return list;
        }
    }

}