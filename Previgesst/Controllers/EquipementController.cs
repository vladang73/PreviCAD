using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Previgesst.Models;
using Previgesst.Services;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Previgesst.Controllers
{
    public class EquipementController:Controller

   
    {
        private EquipementService equipementService;

        public EquipementController(EquipementService equipementService)
        {
            this.equipementService = equipementService;
        }
        public ActionResult ReadListEquipement([DataSourceRequest]DataSourceRequest request, int client)
        {

            return Json(equipementService.GetReadListeEquipements(request,client), JsonRequestBehavior.AllowGet);
        }


        public ActionResult SaveEquipement([DataSourceRequest] DataSourceRequest request,
         EquipementViewModel item, int client)
        {
            if (item != null && ModelState.IsValid)
            {
                item.ClientId = client;
               equipementService.SaveEquipement(item);
            }
            return Json(new[] { item }.ToDataSourceResult(request, ModelState));

        }

        public ActionResult DeleteEquipement([DataSourceRequest] DataSourceRequest request,
         EquipementViewModel equipement)
        {
            if (equipement != null)
            {
                if (!equipementService.Supprimer(equipement))
                {

                    equipement = null;
                }
            }
            return Json(new[] { equipement }.ToDataSourceResult(request, ModelState));

        }

    }
}