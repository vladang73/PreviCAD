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
    public class RegistreController : Controller
    {
        // GET: Registre
        private EmployeRegistreService employeRegistreService;
        private FicheCadenassageService ficheCadenassageService;
        private UtilisateurService utilisateurService;
        private LignesRegistreService ligneRegistreService;
        private LigneRegistreRepository ligneRegistreRepository;
        private DocumentClientService documentClientService;
        private ParametresAppService parametresAppService;
        private ParametresAppController parametresAppController;
        private string Layout;

        public RegistreController(EmployeRegistreService employeRegistreService,
               FicheCadenassageService ficheCadenassageService,
               UtilisateurService utilisateurService,
               LignesRegistreService ligneRegistreService,
               LigneRegistreRepository ligneRegistreRepository,
               DocumentClientService documentClientService,
               ParametresAppService parametresAppService,
               ParametresAppController parametresAppController)
        {
            this.employeRegistreService = employeRegistreService;
            this.ficheCadenassageService = ficheCadenassageService;
            this.utilisateurService = utilisateurService;
            this.ligneRegistreService = ligneRegistreService;
            this.ligneRegistreRepository = ligneRegistreRepository;
            this.documentClientService = documentClientService;
            this.parametresAppService = parametresAppService;
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

                return View("Index", Layout, loginInfo);
            }
            else
            {
                loginInfo.MaintenancePrevue = false;
                return View("Index", Layout, loginInfo);
            }

            //return View("Index", Layout, loginInfo);
            //return View(loginInfo);
        }

        public ActionResult ReadListFiches([DataSourceRequest]DataSourceRequest request, int client)
        {
            var loginInfo = employeRegistreService.getEmployeRegistre();
            if (loginInfo == null)
                return Json("");
            if (client != loginInfo.ClientId)
                return Json("");
            else
                return Json(ficheCadenassageService.GetReadCadenasForRegistre(request, client), JsonRequestBehavior.AllowGet);

        }

        public ActionResult ReadListDocClientCadenassage([DataSourceRequest]DataSourceRequest request, int id)
        {
            var loginInfo = employeRegistreService.getEmployeRegistre();
            if (loginInfo == null)
                return Json("");
            if (id != loginInfo.ClientId)
                return Json("");
            return Json(documentClientService.GetReadListDocumentClient(request, Enums.Applications.Cadenassage, id));

        }

        public ActionResult ReadLignesRegistreCadenassageUnEmploye([DataSourceRequest]DataSourceRequest request, int client)
        {

            var loginInfo = employeRegistreService.getEmployeRegistre();

            if (loginInfo == null)
                return Json("");
            if (client != loginInfo.ClientId)
                return Json("");
            else
                return Json(ligneRegistreService.GetListeLignesRegistreParEmploye(request, loginInfo.EmployeRegistreId), JsonRequestBehavior.AllowGet);

        }

        public ActionResult AddNewLine([DataSourceRequest] DataSourceRequest request,
        LigneRegistreViewModel item, int client)
        {
            var loginInfo = employeRegistreService.getEmployeRegistre();

            if (loginInfo == null)
                return Json("");
            if (client != loginInfo.ClientId)
                return Json("");
            else
            {
                item.Rep1 = "";
                item.Rep2 = "";
                item.Rep3 = "";
                item.Rep4 = "";
                item.EmployeRegistreId = loginInfo.EmployeRegistreId;
                ligneRegistreService.SaveLigneRegistre(item);
                item.BonDeTravail = item.BonDeTravail;
                item.DateDebut = DateTime.Today;

                item.DateFin = null;
                item.LigneRegistreId = 0;
                return Json(item);
            }
        }



        public void GetFiche(int id)
        {
            //ficheCadenassageService.ReturnFile2();
            var fiche = ficheCadenassageService.getFicheVM(id);
            var loginInfo = employeRegistreService.getEmployeRegistre();
            if (fiche == null || loginInfo == null)
                return;
            if (fiche.ClientId != loginInfo.ClientId)
                return;
            ficheCadenassageService.ReturnFile2(id, "fr");

        }

        public void GetFicheEN(int id)
        {
            //ficheCadenassageService.ReturnFile2();
            var fiche = ficheCadenassageService.getFicheVM(id);
            var loginInfo = employeRegistreService.getEmployeRegistre();
            if (fiche == null || loginInfo == null)
                return;
            if (fiche.ClientId != loginInfo.ClientId)
                return;
            ficheCadenassageService.ReturnFile2(id, "en");

        }

        public void GetEtiquette(int id)
        {
            var loginInfo = employeRegistreService.getEmployeRegistre();
            var ligneRegistre = ligneRegistreRepository.Get(id);
            if (loginInfo.EmployeRegistreId != ligneRegistre.EmployeRegistreId)
                return;
            ligneRegistreService.GetEtiquette(id);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
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

        public ActionResult SaveLigneRegistreUnEmploye([DataSourceRequest] DataSourceRequest request,
         LigneRegistreViewModel item, int client)
        {

            var loginInfo = employeRegistreService.getEmployeRegistre();

            if (loginInfo == null)
                return Json("");
            if (client != loginInfo.ClientId)
                return Json("");
            else
            {
                if (item != null && ModelState.IsValid)
                {

                    ligneRegistreService.SaveLigneRegistre(item);
                    return Json(new[] { item }.ToDataSourceResult(request, ModelState));
                }
            }

            return Json("");


        }

        public ActionResult SaveAudit([DataSourceRequest] DataSourceRequest request,
         LigneRegistreViewModel item)
        {
            if (utilisateurService.VerifierBonClientCadenassage_Fiche(item.FicheCadenassageId, false))
            {
                if (item != null && ModelState.IsValid)
                {
                    // on sauvegarde seulement la partie d'audit
                    ligneRegistreService.SaveLigneAudit(item);
                }
                else
                {
                    item.BonDeTravail = item.BonDeTravail;
                    item.Rep1 = item.Rep1;
                    item.Rep2 = item.Rep2;
                    item.Rep3 = item.Rep3;
                    item.Rep4 = item.Rep4;
                    item.NoteAudit = item.NoteAudit;

                    ligneRegistreService.SaveLigneAudit(item);
                }
                return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            }
            return Json("");

        }
    }
}