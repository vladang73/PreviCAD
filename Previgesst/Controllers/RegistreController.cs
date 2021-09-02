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

        private LigneInstructionService ligneInstructionService;
        private LigneDecadenassageService ligneDecadenassageService;
        private SavedInstructionService savedInstructionService;
        private ClientService clientService;

        private string Layout;

        public RegistreController(EmployeRegistreService employeRegistreService,
               FicheCadenassageService ficheCadenassageService,
               UtilisateurService utilisateurService,
               LignesRegistreService ligneRegistreService,
               LigneRegistreRepository ligneRegistreRepository,
               DocumentClientService documentClientService,
               ParametresAppService parametresAppService,
               ParametresAppController parametresAppController,
               LigneInstructionService ligneInstructionService,
               LigneDecadenassageService ligneDecadenassageService,
               SavedInstructionService savedInstructionService,
               ClientService clientService)
        {
            this.employeRegistreService = employeRegistreService;
            this.ficheCadenassageService = ficheCadenassageService;
            this.utilisateurService = utilisateurService;
            this.ligneRegistreService = ligneRegistreService;
            this.ligneRegistreRepository = ligneRegistreRepository;
            this.documentClientService = documentClientService;
            this.parametresAppService = parametresAppService;
            this.parametresAppController = parametresAppController;

            this.ligneInstructionService = ligneInstructionService;
            this.ligneDecadenassageService = ligneDecadenassageService;
            this.savedInstructionService = savedInstructionService;
            this.clientService = clientService;
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

        public ActionResult ReadListFiches([DataSourceRequest] DataSourceRequest request, int client)
        {
            var loginInfo = employeRegistreService.getEmployeRegistre();
            if (loginInfo == null)
                return Json("");
            if (client != loginInfo.ClientId)
                return Json("");
            else
                return Json(ficheCadenassageService.GetReadCadenasForRegistre(request, client), JsonRequestBehavior.AllowGet);

        }

        public ActionResult ReadListDocClientCadenassage([DataSourceRequest] DataSourceRequest request, int id)
        {
            var loginInfo = employeRegistreService.getEmployeRegistre();
            if (loginInfo == null)
                return Json("");
            if (id != loginInfo.ClientId)
                return Json("");
            return Json(documentClientService.GetReadListDocumentClient(request, Enums.Applications.Cadenassage, id));

        }

        public ActionResult ReadLignesRegistreCadenassageUnEmploye([DataSourceRequest] DataSourceRequest request, int client)
        {

            var loginInfo = employeRegistreService.getEmployeRegistre();

            if (loginInfo == null)
                return Json("");
            if (client != loginInfo.ClientId)
                return Json("");
            else
                return Json(ligneRegistreService.GetListeLignesRegistreParEmploye(request, loginInfo.EmployeRegistreId), JsonRequestBehavior.AllowGet);

        }

        public ActionResult AddNewLine([DataSourceRequest] DataSourceRequest request, LigneRegistreViewModel item, int client)
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

        public ActionResult SaveLigneRegistreUnEmploye([DataSourceRequest] DataSourceRequest request, LigneRegistreViewModel item, int client)
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

        public ActionResult SaveAudit([DataSourceRequest] DataSourceRequest request, LigneRegistreViewModel item)
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


        public ActionResult Instruction(int ficheId)
        {
            var langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            // Empty / Completed / Non-Compliant / Not Applicable
            // Vide/Complété/Non-conforme/Sans objet

            ViewData["StepStatuses"] = new List<SelectListItem> {
                new SelectListItem { Value = "1", Text = (langue == "fr" ? "Vide" : "Empty") } ,
                new SelectListItem { Value = "2", Text = (langue == "fr" ? "Complété" : "Completed") } ,
                new SelectListItem { Value = "3", Text = (langue == "fr" ? "Non-conforme" : "Non-Compliant") } ,
                new SelectListItem { Value = "4", Text = (langue == "fr" ? "Sans objet" : "Not Applicable") }
            };

            // get already saved instructions
            var alreadySaved = savedInstructionService.GetSavedInstructions(ficheId);

            // all instructions
            var model1 = ligneInstructionService.GetListeLignesInstruction(ficheId).ToList()
                        .Select(x => new InstructionAndDecadenassageViewModel
                        {
                            PKId = x.LigneInstructionId,
                            PKType = "Instruction",

                            FicheCadenassageId = x.FicheCadenassageId,

                            NoLigne = x.NoLigne,
                            Suppressible = x.Suppressible,
                            CocherColonneCadenas = x.CocherColonneCadenas,
                            InstructionId = x.InstructionId,
                            Realiser = x.Realiser,

                            //TexteSupplementaireDispositifEN = x.TexteSupplementaireDispositifEN,
                            //TexteSupplementaireInstructionEN = x.TexteSupplementaireInstructionEN,

                            TexteSupplementaireDispositif = langue == "fr" ? x.TexteSupplementaireDispositif : x.TexteSupplementaireDispositifEN,
                            TexteSupplementaireInstruction = langue == "fr" ? x.TexteSupplementaireInstruction : x.TexteSupplementaireInstructionEN,

                            TexteInstruction = x.TexteInstruction,
                            TexteDispositif = x.TexteDispositif,
                            TexteAccessoire = x.TexteAccessoire,

                            Thumbnail = x.Thumbnail,

                            TexteRealiser = x.TexteRealiser,
                            PhotoFicheCadenassageId = x.PhotoFicheCadenassageId,

                            StepStatus = ""
                        });

            var model2 = ligneDecadenassageService.GetListeLignesDecadenassage(ficheId).ToList()
                        .Select(x => new InstructionAndDecadenassageViewModel
                        {
                            PKId = x.LigneDecadenassageId,
                            PKType = "Decadenassage",

                            FicheCadenassageId = x.FicheCadenassageId,

                            NoLigne = x.NoLigne,

                            Suppressible = x.Suppressible,
                            CocherColonneCadenas = x.CocherColonneCadenas,
                            InstructionId = x.InstructionId,
                            Realiser = x.Realiser,
                            //TexteSupplementaireDispositifEN = x.TexteSupplementaireDispositifEN,
                            //TexteSupplementaireInstructionEN = x.TexteSupplementaireInstructionEN,
                            TexteSupplementaireDispositif = langue == "fr" ? x.TexteSupplementaireDispositif : x.TexteSupplementaireDispositifEN,
                            TexteSupplementaireInstruction = langue == "fr" ? x.TexteSupplementaireInstruction : x.TexteSupplementaireInstructionEN,

                            TexteInstruction = x.TexteInstruction,
                            TexteDispositif = x.TexteDispositif,
                            TexteAccessoire = x.TexteAccessoire,

                            Thumbnail = x.Thumbnail,

                            TexteRealiser = x.TexteRealiser,
                            PhotoFicheCadenassageId = x.PhotoFicheCadenassageId,

                            StepStatus = ""
                        });

            var model = model1.Concat(model2).ToList();

            // set already saved status
            model.ForEach(x =>
            {
                x.StepStatus = alreadySaved.FirstOrDefault(i => i.InstructionId == x.PKId && i.InstructionType == x.PKType)?.StepStatus;
            });

            //return View(model);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveInstructions(List<Models.SavedInstruction> model, string note)
        {
            savedInstructionService.SaveInstructions(model, note);
            return Json(new { isSuccess = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetEmailAddressForWarning(int client)
        {
            var model = utilisateurService.NotificationNonConformiteActive(client)
                        .Select(x => new { x.Nom, x.Courriel })
                        .ToList();

            return Json(new { isSuccess = true, Users = model }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SendWarningEmails(int client, string subject, string emails, string body)
        {
            try
            {
                // split all emails 
                var allEmails = emails.Split(';');

                // get client logo
                var logoClient = clientService.getClientVM(client);

                // send emails one at a time
                foreach (var Courriel in allEmails)
                {
                    var membreCourriel = Courriel;

                    GeneralService.SendMail(body, subject, membreCourriel, client, logoClient.Logo);
                }

                // return true
                return Json(new { isSuccess = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { isSuccess = false, ErrorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult GetInstructionNote(int ficheId)
        {
            var result = savedInstructionService.GetSavedInstructionNote(ficheId);

            return Json(new { isSuccess = true, Note = result }, JsonRequestBehavior.AllowGet);
        }
    }
}