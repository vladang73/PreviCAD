using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using Previgesst.ActionFilters;
using Previgesst.Models;
using Previgesst.Repositories;
using Previgesst.Services;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Previgesst.Controllers
{
    // [AuthorizeRoles(Enums.Roles.Administrateur, Enums.Roles.LectureEcriture, Enums.Roles.LectureEcriture)]
    [Authorize]
    public class AnalyseRisqueController : Controller
    {
        private AnalyseRisqueService analyseRisqueService;
        private LigneAnalyseRisqueService ligneAnalyseRisqueService;
        private ClientService clientService;
        private PhenomeneService phenomeneService;
        private SituationService situationService;
        private EvenementService evenementService;
        private ReglementService reglementService;

        private FrequenceService frequenceService;
        private GraviteService graviteService;
        private PossibiliteService possibiliteService;
        private ProbabiliteService probabiliteService;
        private AnalyseRisqueRepository analyseRisqueRepository;
        private UtilisateurService utilisateurService;
        private ApplicationPrevisService applicationPreviService;
        private SectionService sectionService;
        private ClientRepository clientRepository;
        private DommagePossibleService dommagePossibleService;
        private EtatService etatService;

        private string Layout;

        public AnalyseRisqueController(AnalyseRisqueService analyseRisqueService,
                                       LigneAnalyseRisqueService ligneAnalyseRisqueService,
                                       ClientService clientService,
                                       PhenomeneService phenomeneService,
                                       SituationService situationService,
                                       EvenementService evenementService,
                                       ReglementService reglementService,

                                       FrequenceService frequenceService,
                                       GraviteService graviteService,
                                       PossibiliteService possibiliteService,
                                       ProbabiliteService probabiliteService,
                                       AnalyseRisqueRepository analyseRisqueRepository,
                                       UtilisateurService utilisateurService,
                                       ApplicationPrevisService applicationPreviService,
                                       SectionService sectionService,
                                       ClientRepository clientRepository,
                                       DommagePossibleService dommagePossibleService,
                                       EtatService etatService
                                     )
        {
            this.analyseRisqueService = analyseRisqueService;
            this.ligneAnalyseRisqueService = ligneAnalyseRisqueService;
            this.clientService = clientService;
            this.phenomeneService = phenomeneService;
            this.situationService = situationService;
            this.evenementService = evenementService;
            this.reglementService = reglementService;

            this.frequenceService = frequenceService;
            this.graviteService = graviteService;
            this.possibiliteService = possibiliteService;
            this.probabiliteService = probabiliteService;
            this.analyseRisqueRepository = analyseRisqueRepository;
            this.utilisateurService = utilisateurService;
            this.applicationPreviService = applicationPreviService;
            this.sectionService = sectionService;
            this.clientRepository = clientRepository;
            this.dommagePossibleService = dommagePossibleService;
            this.etatService = etatService;

            if (utilisateurService.GetSession() != null)
            {
                this.Layout = "~/Views/Shared/_LayoutClient.cshtml";

            }
            else
                this.Layout = "~/Views/Shared/_Layout.cshtml";
        }


        [HttpGet]
        public ActionResult Index()
        {
            PopulateSections(applicationPreviService.getIdByName(Enums.Applications.Analyse));
            AffichageListesViewModel model= new AffichageListesViewModel();
            // seuls les administrateurs PREVI peuvent modifier les listes de référence, utilisés par TOUS les clients
            model.AfficherListes = User.IsInRole("Administrateur");
            return View("Index", Layout, model);
        }


        [HttpGet]
        public ActionResult Create(int id)
        {
            //ViewBag.ListeClients = clientService.GetAnalyseRisqueCreateClientDDL();
            
            AnalyseRisqueCreateViewModel arcvm = new AnalyseRisqueCreateViewModel();
            var Createur = "";
            if (utilisateurService.GetSession() != null)
            {// on est dans le mode BLEU, accès administratif mais Client
                Createur = utilisateurService.GetSession().NomUtilisateur;
            }
            else
            {// version super admin
                Createur = System.Web.HttpContext.Current.User.Identity.Name;
            }

            arcvm.ClientId = id;
            arcvm.Createur = Createur;
            arcvm.AfficherChezClient = (utilisateurService.GetSession() != null);
            return View("Create",Layout,arcvm);
       }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AnalyseRisqueCreateViewModel arcvm)
        {
           return RedirectToAction("Edit",new { id = analyseRisqueService.CreateAnalyseRisque(arcvm) });
        }

        public ActionResult EditClient(int ID)
        {
            if (utilisateurService.VerifierBonClientAnalyse_Client(ID, false))
            {
                var sessionUtilisateur = utilisateurService.GetSession();

                var client = clientRepository.Get(ID);
                var vm = new EditCadenassageViewModel { ClientId = ID, Nom = client.Nom , estClient = (sessionUtilisateur !=null)};
                vm.estUpdate = (sessionUtilisateur == null && 
                                    (User.IsInRole("Administrateur") ||
                                    User.IsInRole("Lecture-Écriture") )
                                ) 
                || (  sessionUtilisateur != null && sessionUtilisateur.AdmAnalyseRisque);

                return View("EditClient",Layout, vm);
            }
            return Json("");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
          
            if (utilisateurService.VerifierBonClientAnalyse_Risque( id, true))
            {
                // On rempli les combobox pour les EditorTemplate
                PopulatePhenomene();
                PopulateSituation();
                PopulateEvenement();
                PopulateReglement();
                PopulateDommages();

                PopulateFrequence();
                PopulateGravite();
                PopulatePossibilite();
                PopulateProbabilite();
                PopulateEtat();

                return View("Edit",Layout,analyseRisqueService.GetAnalyseRisqueEditViewModel(id));
           }
           return Json("");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AnalyseRisqueEditViewModel arevm)
        {
            analyseRisqueService.UpdateAnalyseRisque(arevm);
            return Redirect("~/" + System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName +  "/AnalyseRisque/Edit?ID=" + arevm.Id);
        }


        public ActionResult EditLigneAnalyseRisque(int Id)
        {
            if (utilisateurService.VerifierBonClientAnalyse_Client(Id, true))
            {
                ViewData["idFiche"] = Id;
                var vm = ligneAnalyseRisqueService.getFicheVM(Id);
                return View("EditLigneAnalyseRisque", Layout, vm);
            }
            return Json("");
        }

        public ActionResult ReadListAnalyseRisque([DataSourceRequest]DataSourceRequest request, int client)
        {
            if (utilisateurService.VerifierBonClientAnalyse_Client(client, false))
            {
                return Json(analyseRisqueService.GetReadListAnalyseRisque(request, client));
            }
            return Json("");
        }

        
        public ActionResult ReadListAnalyseRisqueUnClient([DataSourceRequest]DataSourceRequest request)
        {
            var userInfo = utilisateurService.GetSession();
            return Json(analyseRisqueService.GetReadListAnalyseRisque(request, userInfo.ClientId));
        }


        public ActionResult ReadLigneAnalyseRisque([DataSourceRequest]DataSourceRequest request, int id)
        {
            if (utilisateurService.VerifierBonClientAnalyse_Risque( id, false))
            {
                return Json(analyseRisqueService.GetReadListLigneAnalyseRisque(request, id));
            }
            return Json("");
        }



        public ActionResult CreateLigneAnalyseRisque([DataSourceRequest]DataSourceRequest request, LigneAnalyseRisqueEditViewModel larevm, int idAnalyseRisque)
        {
            if (!ModelState.IsValid)
            {
                foreach (var v in ModelState.Values)
                {
                    if (v.Errors.Count >= 1)
                    {
                        if (v.Value.Culture.TwoLetterISOLanguageName == "en")
                        {
                            decimal newValue;
                            string[] raws = (string[])v.Value.RawValue;
                            if (decimal.TryParse(raws[0].Replace(",", "."), out newValue))
                            {
                                larevm.Rang = newValue;
                                v.Errors.Clear();
                            }
                        }
                    }
                }
            }

            if (utilisateurService.VerifierBonClientAnalyse_Risque( idAnalyseRisque, true))
            {

                if (larevm != null && ModelState.IsValid)
                ligneAnalyseRisqueService.UpdateLigneAnalyseRisque(larevm, idAnalyseRisque);

                return Json(new[] { larevm }.ToDataSourceResult(request, ModelState));
                /*var v = new RouteValueDictionary();
                v.Add("id", idAnalyseRisque);


                return RedirectToAction("Edit", v);*/
            }
            return Json("");

        }


        public ActionResult UpdateLigneAnalyseRisque([DataSourceRequest]DataSourceRequest request, LigneAnalyseRisqueEditViewModel larevm, int idAnalyseRisque)
        {
            if (!ModelState.IsValid)
            {
                foreach (var v in ModelState.Values)
                {
                    if (v.Errors.Count >= 1)
                    {
                        if (v.Value.Culture.TwoLetterISOLanguageName == "en")
                        {

                            decimal newValue;
                            string[] raws = (string[])v.Value.RawValue;
                            if (decimal.TryParse(raws[0].Replace(",", "."), out newValue))
                            {
                                larevm.Rang = newValue;
                                v.Errors.Clear();
                            }
                        }
                    }
                }
            }

            if (utilisateurService.VerifierBonClientAnalyse_Risque( idAnalyseRisque, true))
            {
                if (larevm != null && ModelState.IsValid)
                ligneAnalyseRisqueService.UpdateLigneAnalyseRisque(larevm, idAnalyseRisque);

                return Json(new[] { larevm }.ToDataSourceResult(request, ModelState));
                // return View("Edit", Layout, idAnalyseRisque);
            }
            return Json("");
        }

        public void DuplicateLigneAnalyseRisque(int Id)
        {
            var newFicheId = ligneAnalyseRisqueService.DupliquerLigneAnalyseRisque(Id);
        }

        public ActionResult DeleteLigneAnalyseRisque([DataSourceRequest]DataSourceRequest request, LigneAnalyseRisqueEditViewModel larevm)
        {
            if (larevm != null)
                ligneAnalyseRisqueService.DeleteLigneAnalyseRisque(larevm);

            return Json(new[] { larevm }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult DeleteAnalyse([DataSourceRequest] DataSourceRequest request,
       AnalyseRisqueListViewModel fiche)
        {
            if (utilisateurService.VerifierBonClientAnalyse_Risque( fiche.AnalyseRisqueId, true))
            {
                if (fiche != null)
                {
                    if (!analyseRisqueService.Supprimer(fiche))
                    {

                        fiche = null;
                    }
                }
                return Json(new[] { fiche }.ToDataSourceResult(request, ModelState));
            }
            return Json("");
        }

        public void getXLAnalyseClientEN( int id)
        {
            getXLAnalyseClient(id, "en");
        }

        public void getXLAnalyseClient (int id, string langue="fr")
        {
            //  on doit être authentifié client ou previgesst
            var ClientFromSession = utilisateurService.GetSession();
            if (!Request.IsAuthenticated && ClientFromSession == null)
            {
                utilisateurService.LogOff();
                this.Response.Redirect("~/ClientLogin/Index");
            }

            // si authentifié client, seulement l'analyse qui nous revient!
            if (!Request.IsAuthenticated)
            {
                var ClientIdFromSession = utilisateurService.GetSession().ClientId;
                if (id != ClientIdFromSession)
                {
                    utilisateurService.LogOff();
                    RedirectToAction(Layout, "Index", "ClientLogin");
                }
            }

            analyseRisqueService.ReturnFile(id, true, langue);


            //string templateName = HttpContext.Server.MapPath("~/Templates/AnalyseRisques.xlsx");        
            //using (var source = System.IO.File.OpenRead(templateName))
            //{

            //    using (var excel = new OfficeOpenXml.ExcelPackage(source))
            //    {
            //        var datalist = analyseRisqueRepository.GetAnalyse(id);

            //        var ws = excel.Workbook.Worksheets[1];

            //        for (int i = 0; i < datalist.Count(); i++)
            //        {
            //            ws.Cells[i + 4, 1].Value = datalist[i].NoReference;
            //            ws.Cells[i + 4, 2].Value = datalist[i].Equipement;
            //            ws.Cells[i + 4, 3].Value = datalist[i].Operation;
            //            ws.Cells[i + 4, 4].Value = datalist[i].Zone;

            //            ws.Cells[i + 4, 5].Value = datalist[i].Phenomene;
            //            ws.Cells[i + 4,6].Value = datalist[i].Situation;
            //            ws.Cells[i + 4,7].Value = datalist[i].Evenement;
            //            ws.Cells[i + 4, 8].Value = datalist[i].GraviteAvant;
            //            ws.Cells[i + 4, 9].Value = datalist[i].FrequenceAvant;
            //            ws.Cells[i + 4, 10].Value = datalist[i].ProbabiliteAvant;
            //            ws.Cells[i + 4, 11].Value = datalist[i].PossibiliteAvant;
            //            ws.Cells[i + 4, 12].Value = datalist[i].IndiceFinalAvant;
            //            ws.Cells[i + 4, 13].Value = datalist[i].NbPersonnesExposees;
            //            ws.Cells[i + 4, 14].Value = datalist[i].SystemeCommandeAvant;


            //            ws.Cells[i + 4, 15].Value = datalist[i].Reglement;
            //            ws.Cells[i + 4, 16].Value = datalist[i].Mesure;
            //            ws.Cells[i + 4, 17].Value = datalist[i].GraviteApres;
            //            ws.Cells[i + 4, 18].Value = datalist[i].FrequenceApres;
            //            ws.Cells[i + 4, 19].Value = datalist[i].ProbabiliteApres;
            //            ws.Cells[i + 4, 20].Value = datalist[i].PossibiliteApres;
            //            ws.Cells[i + 4, 21].Value = datalist[i].IndiceFinalApres;
            //            ws.Cells[i + 4, 22].Value = datalist[i].SystemeCommandeInstalles;
            //            ws.Cells[i + 4, 23].Value = datalist[i].ConformiteAuNormes;



            //        }





            //        excel.Workbook.Properties.Title = "Analyse de risques";
            //        this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //        this.Response.AddHeader(
            //                  "content-disposition",
            //                  string.Format("attachment;  filename={0}", "RapportDePlanification.xlsx"));
            //        this.Response.BinaryWrite(excel.GetAsByteArray());
            //    }
            //}


        }

        public void getXLAnalyseEN( int id)
        {
            getXLAnalyse(id, "en");
        }

        public void getXLAnalyse(int id, string langue="fr")
        {
            //  on doit être authentifié client ou previgesst
            var ClientFromSession = utilisateurService.GetSession();
            if (!Request.IsAuthenticated && ClientFromSession == null)
            {
                utilisateurService.LogOff();
                this.Response.Redirect("~/ClientLogin/Index");
            }

            // si authentifié client, seulement l'analyse qui nous revient!
            if (!Request.IsAuthenticated)
            {
                var Analyse = analyseRisqueRepository.Get(id);
                var ClientIdFromSession = utilisateurService.GetSession().ClientId;
                if (Analyse.ClientId != ClientIdFromSession)
                {
                    utilisateurService.LogOff();
                    RedirectToAction(Layout,"Index", "ClientLogin");
                }
            }

            analyseRisqueService.ReturnFile(id,false,langue);


            //string templateName = HttpContext.Server.MapPath("~/Templates/AnalyseRisques.xlsx");        
            //using (var source = System.IO.File.OpenRead(templateName))
            //{

            //    using (var excel = new OfficeOpenXml.ExcelPackage(source))
            //    {
            //        var datalist = analyseRisqueRepository.GetAnalyse(id);

            //        var ws = excel.Workbook.Worksheets[1];

            //        for (int i = 0; i < datalist.Count(); i++)
            //        {
            //            ws.Cells[i + 4, 1].Value = datalist[i].NoReference;
            //            ws.Cells[i + 4, 2].Value = datalist[i].Equipement;
            //            ws.Cells[i + 4, 3].Value = datalist[i].Operation;
            //            ws.Cells[i + 4, 4].Value = datalist[i].Zone;

            //            ws.Cells[i + 4, 5].Value = datalist[i].Phenomene;
            //            ws.Cells[i + 4,6].Value = datalist[i].Situation;
            //            ws.Cells[i + 4,7].Value = datalist[i].Evenement;
            //            ws.Cells[i + 4, 8].Value = datalist[i].GraviteAvant;
            //            ws.Cells[i + 4, 9].Value = datalist[i].FrequenceAvant;
            //            ws.Cells[i + 4, 10].Value = datalist[i].ProbabiliteAvant;
            //            ws.Cells[i + 4, 11].Value = datalist[i].PossibiliteAvant;
            //            ws.Cells[i + 4, 12].Value = datalist[i].IndiceFinalAvant;
            //            ws.Cells[i + 4, 13].Value = datalist[i].NbPersonnesExposees;
            //            ws.Cells[i + 4, 14].Value = datalist[i].SystemeCommandeAvant;


            //            ws.Cells[i + 4, 15].Value = datalist[i].Reglement;
            //            ws.Cells[i + 4, 16].Value = datalist[i].Mesure;
            //            ws.Cells[i + 4, 17].Value = datalist[i].GraviteApres;
            //            ws.Cells[i + 4, 18].Value = datalist[i].FrequenceApres;
            //            ws.Cells[i + 4, 19].Value = datalist[i].ProbabiliteApres;
            //            ws.Cells[i + 4, 20].Value = datalist[i].PossibiliteApres;
            //            ws.Cells[i + 4, 21].Value = datalist[i].IndiceFinalApres;
            //            ws.Cells[i + 4, 22].Value = datalist[i].SystemeCommandeInstalles;
            //            ws.Cells[i + 4, 23].Value = datalist[i].ConformiteAuNormes;



            //        }





            //        excel.Workbook.Properties.Title = "Analyse de risques";
            //        this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //        this.Response.AddHeader(
            //                  "content-disposition",
            //                  string.Format("attachment;  filename={0}", "RapportDePlanification.xlsx"));
            //        this.Response.BinaryWrite(excel.GetAsByteArray());
            //    }
            //}
        }


        #region Populating DDL

        private void PopulatePhenomene()
        {
            var phenomeneDDL = phenomeneService.GetAllAsPhenomeneDDLViewModel();

            ViewData["Phenomenes"] = phenomeneDDL;
            ViewData["DefaultPhenomene"] = phenomeneDDL.FirstOrDefault();
        }

        private void PopulateDommages()
        {
            var dommageDDL = dommagePossibleService.GetAllAsEvenementDDLViewModel();

            ViewData["Dommages"] = dommageDDL;
            ViewData["DefaultDommages"] = dommageDDL.FirstOrDefault();
        }


        private void PopulateSections(int ApplicationId)
        {
            var sectionDDL = sectionService.GetAllAsSectionsDDLViewModel(applicationPreviService.getIdByName(Enums.Applications.Analyse));

            ViewData["Sections"] = sectionDDL;
            ViewData["DefaultSection"] = sectionDDL.FirstOrDefault();
        }


        private void PopulateSituation()
        {
            var situationDDL = situationService.GetAllAsSituationDDLViewModel();

            ViewData["Situations"] = situationDDL;
            ViewData["DefaultSituation"] = situationDDL.FirstOrDefault();
        }


        private void PopulateEvenement()
        {
            var evenementDDL = evenementService.GetAllAsEvenementDDLViewModel();

            ViewData["Evenements"] = evenementDDL;
            ViewData["DefaultEvenement"] = evenementDDL.FirstOrDefault();
        }


        private void PopulateReglement()
        {
            var reglementDDL = reglementService.GetAllAsReglementDDLViewModel();

            ViewData["Reglements"] = reglementDDL;
            ViewData["DefaultReglement"] = reglementDDL.FirstOrDefault();
        }


        private void PopulateFrequence()
        {
            var frequenceDDL = frequenceService.GetAllAsFrequenceDDLViewModel();

            ViewData["Frequences"] = frequenceDDL;
            ViewData["DefaultFrequence"] = frequenceDDL.FirstOrDefault();
        }

        private void PopulateGravite()
        {
            var graviteDDL = graviteService.GetAllAsGraviteDDLViewModel();

            ViewData["Gravites"] = graviteDDL;
            ViewData["DefaultGravite"] = graviteDDL.FirstOrDefault();
        }

        private void PopulatePossibilite()
        {
            var possibiliteDDL = possibiliteService.GetAllAsPossibiliteDDLViewModel();

            ViewData["Possibilites"] = possibiliteDDL;
            ViewData["DefaultPossibilite"] = possibiliteDDL.FirstOrDefault();
        }

        private void PopulateProbabilite()
        {
            var probabiliteDDL = probabiliteService.GetAllAsProbabiliteDDLViewModel();

            ViewData["Probabilites"] = probabiliteDDL;
            ViewData["DefaultProbabilite"] = probabiliteDDL.FirstOrDefault();
        }

        private void PopulateEtat()
        {
            var etatDDL = etatService.GetAllAsEtatDDLViewModel();

            ViewData["Etat"] = etatDDL;
            ViewData["DefaultEtat"] = etatDDL.FirstOrDefault();
        }

        public void RefreshAutoComplete(int id)
        {
            //PopulateEquipements(id);
            PopulateZones(id);
            PopulateOperations(id);
        }
        //private void PopulateEquipements(int id)
        //{
        //    ViewBag.Equipements = ligneAnalyseRisqueService.getEquipements(id);
        //}

        public int GetNewValue(int id)
        {
            var result = ligneAnalyseRisqueService.GetNewValue(id);
            return result;
        }
        //public string RetourneEquipements(int id)
        //{
        //    var result = ligneAnalyseRisqueService.getEquipements(id);
        //    var json = JsonConvert.SerializeObject(result);
        //    return json;
        //}


        public string RetourneZones(int id)
        {
            return JsonConvert.SerializeObject(ligneAnalyseRisqueService.getZones(id));
        }

        //public string RetourneReferences( int id)
        //{
        //    return JsonConvert.SerializeObject(ligneAnalyseRisqueService.getReferences(id));
        //}
        private void PopulateZones(int id)
        {
            ViewBag.Zones=  ligneAnalyseRisqueService.getZones(id);
        }

        public string RetourneOperations(int id)
        {
            return JsonConvert.SerializeObject(ligneAnalyseRisqueService.getOperations(id));
        }

        private void PopulateOperations(int id)
        {
            ViewBag.Operations = ligneAnalyseRisqueService.getOperations(id);
        }

        public string RetournePhenomeneId(int id)
        {
            return JsonConvert.SerializeObject(ligneAnalyseRisqueService.getPhenomeneId(id));
        }

        private void PopulatePhenomeneId(int id)
        {
            ViewBag.PhenomeneId = ligneAnalyseRisqueService.getPhenomeneId(id);
        }

        public string RetourneIndiceFinalAvant(int id)
        {
            return JsonConvert.SerializeObject(ligneAnalyseRisqueService.getIndiceFinalAvant(id));
        }

        private void PopulateIndiceFinalAvant(int id)
        {
            ViewBag.IndiceFinalAvant = ligneAnalyseRisqueService.getIndiceFinalAvant(id);
        }

        public string RetourneIndiceFinalApres(int id)
        {
            return JsonConvert.SerializeObject(ligneAnalyseRisqueService.getIndiceFinalApres(id));
        }

        private void PopulateIndiceFinalApres(int id)
        {
            ViewBag.IndiceFinalApres = ligneAnalyseRisqueService.getIndiceFinalApres(id);
        }

        public string RetourneConformiteAuxNormes(int id)
        {
            return JsonConvert.SerializeObject(ligneAnalyseRisqueService.getConformiteAuxNormes(id));
        }

        private void PopulateConformiteAuxNormes(int id)
        {
            ViewBag.ConformiteAuxNormes = ligneAnalyseRisqueService.getConformiteAuxNormes(id);
        }
        #endregion

        public string RetourneEtatId(int id)
        {
            return JsonConvert.SerializeObject(ligneAnalyseRisqueService.getEtatId(id));
        }

        private void PopulateEtatId(int id)
        {
            ViewBag.PhenomeneId = ligneAnalyseRisqueService.getEtatId(id);
        }

        public int RenumeroterLignes(int AnalyseId)
        {

            analyseRisqueService.RenumeroterLignes(AnalyseId);

            return 0;
        }
    }
}