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
    public class ParametresAppService
    {
        private ParametresAppRepository parametresAppRepository;

        public ParametresAppService(ParametresAppRepository parametresAppRepository)
        {
            this.parametresAppRepository = parametresAppRepository;
        }

        internal DataSourceResult GetReadListParametresApp(DataSourceRequest request, int ParametresAppId)
        {
            var result = parametresAppRepository.AsQueryable().Select(a => new ParametresAppViewModel()
            {
                NextUpdateCase = a.NextUpdateCase,
                NextUpdateTextFr = a.NextUpdateTextFr,
                NextUpdateTextEn = a.NextUpdateTextEn

            }).ToDataSourceResult(request);

            return result;
        }

        internal object GetparametresAppId()
        {
            var parametresAppId = parametresAppRepository.AsQueryable().Select(x => x.ParametresAppId);

            return parametresAppId;
        }

        public void SaveParametersApp(ParametresAppViewModel model)
        {
            var item = parametresAppRepository.Get(model.ParametresAppId);

            if (item == null)
            {
                item = new Models.ParametresApp();
            }

            item.ParametresAppId = model.ParametresAppId;
            item.NextUpdateCase = model.NextUpdateCase;
            item.NextUpdateTextFr = model.NextUpdateTextFr;
            item.NextUpdateTextEn = model.NextUpdateTextEn;

            if (item.ParametresAppId > 0)
                parametresAppRepository.Update(item);
            else
                parametresAppRepository.Add(item);

            parametresAppRepository.SaveChanges();
            model.ParametresAppId = item.ParametresAppId;
        }

        public Boolean VerifierStatusMaintenance()
        {
            var statusMaintenanceRequest = parametresAppRepository.AsQueryable().Where(x => x.NextUpdateCase == true).ToList();
            if(statusMaintenanceRequest.Count() != 0)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}