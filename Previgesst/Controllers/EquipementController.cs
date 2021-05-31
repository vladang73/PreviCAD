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
    public class EquipementController : Controller
    {
        private EquipementService equipementService;

        public EquipementController(EquipementService equipementService)
        {
            this.equipementService = equipementService;
        }
        public ActionResult ReadListEquipement([DataSourceRequest] DataSourceRequest request, int client)
        {

            return Json(equipementService.GetReadListeEquipements(request, client), JsonRequestBehavior.AllowGet);
        }
        //public ActionResult GetEquipementArticuloes(int equipementId)
        //{
        //    return Json(equipementService.GetEquipementArticuloes(equipementId), JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult DeleteEquipementArticuloes(int equipementArticuloeID)
        //{
        //    return Json(new { IsSuccess = equipementService.DeleteEquipementArticula(equipementArticuloeID) }, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult SaveEquipementNew(EquipementViewModel item)
        {
            if (item != null && item.EquipementId > 0)
            {
                equipementService.SaveEquipementNew(item, "Equipment");
            }
            return Json(new { isSuccess = true });
        }

        //public ActionResult SaveEnergyInfo(EquipementArticuloViewModel item)
        //{
        //    if (item != null && item.EquipementId > 0)
        //    {
        //        equipementService.SaveEquipementArticula(item);
        //    }
        //    return Json(new { isSuccess = true });
        //}

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

        public ActionResult DeleteEquipement([DataSourceRequest] DataSourceRequest request, EquipementViewModel equipement)
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

        public ActionResult EditEquipement([DataSourceRequest] DataSourceRequest request, EquipementViewModel equipement)
        {
            return PartialView("EquipementEditor.cshtml");
        }

    }
}