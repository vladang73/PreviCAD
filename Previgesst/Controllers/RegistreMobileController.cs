using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Previgesst.Repositories;
using Previgesst.Services;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Previgesst.Ressources.Previcad;
using Newtonsoft.Json;

namespace Previgesst.Controllers
{
    public class RegistreMobileController : Controller
    {
        private EmployeRegistreService employeRegistreService;
        private ParametresAppService parametresAppService;
        private FicheCadenassageService ficheCadenassageService;
        private ParametresAppController parametresAppController;

        private FicheCadenassageRepository ficheCadenassageRepository;

        public RegistreMobileController(
            EmployeRegistreService employeRegistreService,
            FicheCadenassageService ficheCadenassageService,
            ParametresAppService parametresAppService,
            ParametresAppController parametresAppController,
            FicheCadenassageRepository ficheCadenassageRepository)
        {
            this.employeRegistreService = employeRegistreService;
            this.parametresAppService = parametresAppService;
            this.ficheCadenassageService = ficheCadenassageService;
            this.ficheCadenassageRepository = ficheCadenassageRepository;
            this.parametresAppController = parametresAppController;
        }

        [HttpGet]
        public ActionResult Index(string ClientID)
        {
            EmployeRegistreViewModel currentEMP = employeRegistreService.getEmployeRegistre();
            currentEMP.Langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower();

            if (currentEMP.ClientId != 0)
            {
                return View("RegistreEmploye", currentEMP);
            }

            var loginInfo = new LoginCadenassageViewModel() { Identificateur = Guid.Parse(ClientID) };
            Session["ClientID"] = Guid.Parse(ClientID);

            loginInfo.MaintenancePrevue = false;

            if (parametresAppService.VerifierStatusMaintenance())
            {
                loginInfo.MaintenancePrevue = true;
                var parametresAppIdRequest = JsonConvert.SerializeObject(parametresAppService.GetparametresAppId());
                var parametresAppConvert = parametresAppIdRequest.Replace("[", "").Replace("]", "");
                var parametresAppId = int.Parse(parametresAppConvert);
                var elementsMaintenance = parametresAppController.GetReadListParametresApp(parametresAppId);
                var NextUpdateTextFr = elementsMaintenance.NextUpdateTextFr;
                var NextUpdateTextEn = elementsMaintenance.NextUpdateTextEn;
                var langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

                loginInfo.NextUpdateTextFr = NextUpdateTextFr;
                loginInfo.NextUpdateTextEn = NextUpdateTextEn;
                loginInfo.Langue = langue;

                return View("Index", loginInfo);
            }
            else
            {
                loginInfo.MaintenancePrevue = false;
                return View("Index", loginInfo);
            }
        }

        [HttpPost]
        public ActionResult Index(LoginCadenassageViewModel vm)
        {
            var isLogOK = employeRegistreService.isLoginOK(vm);

            if (isLogOK)
            {
                employeRegistreService.setSession(vm);
                var employe = employeRegistreService.getEmployeRegistre();
                employe.Langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

                return View("RegistreEmploye", employe);
            }
            else
            {
                ModelState.AddModelError("Identificateur", PrevIndexRES.ErreurConnection);
                return View(vm);
            }
        }


        [HttpGet]
        public ActionResult ScanQR()
        {
            EmployeRegistreViewModel currentEMP = employeRegistreService.getEmployeRegistre();
            currentEMP.Langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower();

            if (currentEMP.ClientId != 0)
            {
                return View("ScanQR", currentEMP);
            }
            else
            {
                ModelState.AddModelError("Identificateur", PrevIndexRES.ErreurConnection);
                return RedirectToAction("Index", new { @ClientID = "" });
            }
        }


        public void GetFiche(int id)
        {
            GetFicheByLanguage(id, "fr");
        }

        public void GetFicheEN(int id)
        {
            GetFicheByLanguage(id, "en");
        }


        private void GetFicheByLanguage(int id, string lang)
        {
            var loginInfo = employeRegistreService.getEmployeRegistre();
            var fiche = ficheCadenassageService.getFicheVM(id);
            if (fiche == null || loginInfo == null)
                return;
            if (fiche.ClientId != loginInfo.ClientId)
                return;
            ficheCadenassageService.ReturnFile2(id, lang);
        }

        /*
        private void GetFicheByLanguage(int id, string lang)
        {
            var loginInfo = employeRegistreService.getEmployeRegistre();

            if (loginInfo == null) return;

            // get FicheCadenassageId by client and equipment id
            var allRecords = ficheCadenassageRepository.AsQueryable()
                                        .Where(x => x.EquipementId == id)
                                        .Where(x => x.ClientId == loginInfo.ClientId)
                                        .Where(x => x.IsApproved)
                                        .ToList()
                                        ;

            if (allRecords == null) return;
            if (allRecords != null && allRecords.Count() == 0) return;

            if (allRecords.Count() == 1)
            {
                var ficheCadenassageId = allRecords.FirstOrDefault().FicheCadenassageId;

                var fiche = ficheCadenassageService.getFicheVM(ficheCadenassageId);
                if (fiche == null || loginInfo == null)
                    return;
                if (fiche.ClientId != loginInfo.ClientId)
                    return;
                ficheCadenassageService.ReturnFile2(id, lang);
            }
        }
        */

        [HttpPost]
        public JsonResult GetFicheDetails(int id)
        {
            var loginInfo = employeRegistreService.getEmployeRegistre();

            // session expired
            if (loginInfo == null)
                return Json(new { isSuccess = false, ErrorMessage = PrevIndexRES.ErreurConnection }, JsonRequestBehavior.AllowGet);

            // get FicheCadenassageId by client and equipment id
            var allRecords = ficheCadenassageRepository.AsQueryable()
                                        .Where(x => x.EquipementId == id)
                                        .Where(x => x.ClientId == loginInfo.ClientId)
                                        .Where(x => x.IsApproved)
                                        .ToList()
                                        ;

            if (allRecords == null)
                return Json(new { isSuccess = false, ErrorMessage = Ressources.Analyse.AREditClientRES.AucunDocument }, JsonRequestBehavior.AllowGet);

            if (allRecords != null && allRecords.Count() == 0)
                return Json(new { isSuccess = false, ErrorMessage = Ressources.Analyse.AREditClientRES.AucunDocument }, JsonRequestBehavior.AllowGet);


            var ficheCadenassageId = allRecords.FirstOrDefault().FicheCadenassageId;

            return Json(new { isSuccess = true, TotalRecords = allRecords.Count(), Id = ficheCadenassageId }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Fiches(int id)
        {
            EmployeRegistreViewModel currentEMP = employeRegistreService.getEmployeRegistre();
            currentEMP.Langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower();

            if (currentEMP.ClientId != 0)
            {
                ViewBag.EquipmentID = id;
                return View(currentEMP);
            }

            return RedirectToAction("Index", new { @ClientID = "" });
        }

        public ActionResult ReadListFiches([DataSourceRequest] DataSourceRequest request, int client, int equipment)
        {
            var loginInfo = employeRegistreService.getEmployeRegistre();
            if (loginInfo == null)
                return Json("");
            if (client != loginInfo.ClientId)
                return Json("");
            else
                return Json(ficheCadenassageService.GetReadCadenasForRegistre(request, client, equipment), JsonRequestBehavior.AllowGet);

        }

    }
}