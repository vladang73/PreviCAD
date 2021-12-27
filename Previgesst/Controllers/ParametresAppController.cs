using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using Previgesst.Repositories;
using Previgesst.Services;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Previgesst.Controllers
{
    public class ParametresAppController : Controller
    {
        // GET: ParametresApp
        private ParametresAppService parametresAppService;
        private ParametresAppRepository parametresAppRepository;
        //private string Layout;

        public ParametresAppController(ParametresAppRepository parametresAppRepository, ParametresAppService parametresAppService)
        {
            this.parametresAppRepository = parametresAppRepository;
            this.parametresAppService = parametresAppService;
        }

        public ActionResult Index()
        {
            var parametresAppIdRequest = JsonConvert.SerializeObject(parametresAppService.GetparametresAppId());
            var parametresAppConvert = parametresAppIdRequest.Replace("[", "").Replace("]", "");
            var parametresAppId = int.Parse(parametresAppConvert);
            var vm = GetReadListParametresApp(parametresAppId);
            return View("Index", vm);
        }

        public ActionResult SaveParametersApp([DataSourceRequest] DataSourceRequest request, ParametresAppViewModel item)
        {
            var parametresAppIdRequest = JsonConvert.SerializeObject(parametresAppService.GetparametresAppId());
            var parametresAppConvert = parametresAppIdRequest.Replace("[", "").Replace("]", "");
            var parametresAppId = int.Parse(parametresAppConvert);

            if (item != null && ModelState.IsValid)
            {
                parametresAppService.SaveParametersApp(item);
                return Redirect("~/ParametresApp/Index");
            }
            return Json(new[] { item }.ToDataSourceResult(request, ModelState));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ParametresAppViewModel GetReadListParametresApp(int parametresAppId)
        {
            var parametresApp = this.parametresAppRepository.Get(parametresAppId);
            var vm = new ParametresAppViewModel { ParametresAppId = parametresAppId };
            vm.NextUpdateCase = parametresApp.NextUpdateCase;
            vm.NextUpdateTextFr = parametresApp.NextUpdateTextFr;
            vm.NextUpdateTextEn = parametresApp.NextUpdateTextEn;

            return vm;
            //return Json(parametresAppService.GetReadListParametresApp(request, parametresAppId));
        }
    }
}