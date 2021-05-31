using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Previgesst.Services;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Previgesst.Controllers
{
    public class DepartementController : Controller
    {


        private DepartementService departementService;

        public DepartementController(DepartementService departementService)
        {
            this.departementService = departementService;
        }
        public ActionResult ReadListDepartement([DataSourceRequest]DataSourceRequest request, int client)
        {

            return Json(departementService.GetReadListeDepartements(request, client), JsonRequestBehavior.AllowGet);
        }


        public ActionResult SaveDepartement([DataSourceRequest] DataSourceRequest request,
         DepartementViewModel item, int client)
        {
            if (item != null && ModelState.IsValid)
            {
                item.ClientId = client;
                departementService.SaveDepartement(item);
            }
            return Json(new[] { item }.ToDataSourceResult(request, ModelState));

        }

        public ActionResult DeleteDepartement([DataSourceRequest] DataSourceRequest request,
         DepartementViewModel departement)
        {
            if (departement != null)
            {
                if (!departementService.Supprimer(departement))
                {

                    departement = null;
                }
            }
            return Json(new[] { departement }.ToDataSourceResult(request, ModelState));

        }


    }
}