using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Previgesst.ActionFilters;
using Previgesst.Repositories;
using Previgesst.Services;
using Previgesst.ViewModels;

//using Previgesst.ViewModels.Cadenassage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Previgesst.Ressources.Cadenassage;
using System.Globalization;

namespace Previgesst.Controllers
{
    [Authorize]

    public class FicheCadenassageController : Controller
    {
        private FicheCadenassageService ficheCadenassageService;

        private SectionService sectionService;
        private ApplicationPrevisService applicationPreviService;
        private ClientRepository clientRepository;
        private EquipementService equipementService;
        private LigneDecadenassageService ligneDecadenassageService;
        private MaterielRequisCadenassageService materielRequisCadenassageService;
        private MaterielService materielService;
        private SourceEnergieService sourceEnergieService;
        private SourceEnergieCadenassageRepository sourceEnergieCadenassageRepository;
        private LigneInstructionService ligneInstructionService;
        private InstructionService instructionService;
        private DepartementService departementService;
        private CadenasService cadenasService;
        private UtilisateurService utilisateurService;
        private PhotoFicheCadenassageService photoFicheCadenassageService;
        private FicheCadenassageRepository ficheCadenassageRepository;
        private UploadController uploadController;
        private PhotoFicheCadenassageRepository photoFicheCadenassageRepository;
        private EmployeRegistreService employeRegistreService;
        private LignesRegistreService lignesRegistreService;

        private EquipementArticuloeService equipementArticuloeService;
        private DispositifService dispositifService;
        private DocumentFicheNoteService documentFicheNoteService;

        private string Layout;

        public FicheCadenassageController(ClientRepository clientRepository, FicheCadenassageService ficheCadenassageService,
            SectionService sectionService, ApplicationPrevisService applicationPreviService, EquipementService equipementService,
            LigneDecadenassageService ligneDecadenassageService, MaterielRequisCadenassageService materielRequisCadenassageService,
            MaterielService materielService, SourceEnergieService sourceEnergieService, SourceEnergieCadenassageRepository sourceEnergieCadenassageRepository,
            LigneInstructionService ligneInstructionService, InstructionService instructionService, DepartementService departementService,
            CadenasService cadenasService, UtilisateurService utilisateurService,
            PhotoFicheCadenassageService photoFicheCadenassageService, FicheCadenassageRepository ficheCadenassageRepository,
            UploadController uploadController, DocumentFicheNoteService documentFicheNoteService,
            PhotoFicheCadenassageRepository photoFicheCadenassageRepository,
            EmployeRegistreService employeRegistreService, LignesRegistreService lignesRegistreService,
            EquipementArticuloeService equipementArticuloeService, DispositifService dispositifService)
        {
            this.ficheCadenassageService = ficheCadenassageService;
            this.sectionService = sectionService;
            this.applicationPreviService = applicationPreviService;
            this.clientRepository = clientRepository;
            this.equipementService = equipementService;
            this.ligneDecadenassageService = ligneDecadenassageService;
            this.materielRequisCadenassageService = materielRequisCadenassageService;
            this.materielService = materielService;
            this.sourceEnergieService = sourceEnergieService;
            this.sourceEnergieCadenassageRepository = sourceEnergieCadenassageRepository;
            this.ligneInstructionService = ligneInstructionService;
            this.instructionService = instructionService;
            this.departementService = departementService;
            this.cadenasService = cadenasService;
            this.utilisateurService = utilisateurService;
            this.photoFicheCadenassageService = photoFicheCadenassageService;
            this.ficheCadenassageRepository = ficheCadenassageRepository;
            this.uploadController = uploadController;
            this.photoFicheCadenassageRepository = photoFicheCadenassageRepository;
            this.employeRegistreService = employeRegistreService;
            this.lignesRegistreService = lignesRegistreService;

            this.equipementArticuloeService = equipementArticuloeService;
            this.dispositifService = dispositifService;

            this.documentFicheNoteService = documentFicheNoteService;

            if (utilisateurService.GetSession() != null)
            {
                this.Layout = "~/Views/Shared/_LayoutClient.cshtml";

            }
            else
                this.Layout = "~/Views/Shared/_Layout.cshtml";
        }



        public void GetFiche(int id)
        {
            //ficheCadenassageService.ReturnFile2();

            if (utilisateurService.VerifierBonClientCadenassage_Fiche(id, false))

                ficheCadenassageService.ReturnFile2(id, "fr");

        }
        public void GetFicheEN(int id)
        {
            //ficheCadenassageService.ReturnFile2();

            if (utilisateurService.VerifierBonClientCadenassage_Fiche(id, false))

                ficheCadenassageService.ReturnFile2(id, "en");

        }


        private void PopulateSections()
        {
            var sectionDDL = sectionService.GetAllAsSectionsDDLViewModel(applicationPreviService.getIdByName(Enums.Applications.Cadenassage));

            ViewData["Sections"] = sectionDDL;
            ViewData["DefaultSection"] = sectionDDL.FirstOrDefault();
        }

        public ActionResult ReadLignesRegistreCadenassage([DataSourceRequest] DataSourceRequest request, int client)
        {
            if (utilisateurService.VerifierBonClientCadenassage_Client(client, false))
                return Json(lignesRegistreService.GetListeLignesRegistre(request, client), JsonRequestBehavior.AllowGet);
            else
                return Json("");
        }


        public ActionResult ReadListFiches([DataSourceRequest] DataSourceRequest request, int client)
        {
            if (utilisateurService.VerifierBonClientCadenassage_Client(client, false))
                return Json(ficheCadenassageService.GetReadCadenas(request, client), JsonRequestBehavior.AllowGet);
            else
                return Json("");
        }


        public ActionResult SaveFiche(EditFicheViewModel item)
        {
            if (utilisateurService.VerifierBonClientCadenassage_Client(item.ClientId, true))
            {
                ViewData["Layout"] = Layout;

                // Initialize VM, create DroitAjout and parser it
                var vm = new EditFicheViewModel();
                vm.DroitAjout = true;

                if (utilisateurService.VerifierStatusCadenassageSave(item.ClientId, true))
                {
                    PopulateLists(item.ClientId);

                    if (item != null && ModelState.IsValid)
                    {
                        ModelState.Clear();

                        if (item.ApprouvePar != null && item.DateCreation == null)
                        {
                            ModelState.AddModelError("BidonErreurCreation", CadInfoGeneralesRES.DateCreationObligatoire);

                            return View("EditFiche", Layout, item);
                        }

                        var user = GetCurrentUser();


                        item.CreatedPar = user;
                        item.UpdatedPar = user;

                        item.DateCreation = DateTime.Now;
                        item.DateUpdated = DateTime.Now;

                        ficheCadenassageService.SaveFiche(item);

                        ViewData["idFiche"] = item.FicheCadenassageId;
                        // return View("EditFiche", item);
                        return RedirectToAction("EditFiche", new { Id = item.FicheCadenassageId, DroitAjout = "true" });
                    };
                    return View("EditFiche", Layout, vm);
                }
                return View("EditFiche", Layout, vm);
            }
            else
                return Json("");
        }

        public ActionResult EditerFiche(EditFicheViewModel item, string save, string approve)
        {
            if (utilisateurService.VerifierBonClientCadenassage_Client(item.ClientId, true))
            {
                ViewData["Layout"] = Layout;

                // Initialize VM, create DroitAjout and parser it
                var vm = new EditFicheViewModel();
                vm.DroitAjout = true;

                PopulateLists(item.ClientId);

                if (item != null && ModelState.IsValid)
                {
                    ModelState.Clear();

                    if (item.ApprouvePar != null && item.DateCreation == null)
                    {
                        ModelState.AddModelError("BidonErreurCreation", CadInfoGeneralesRES.DateCreationObligatoire);

                        return View("EditFiche", Layout, item);
                    }

                    var user = GetCurrentUser();


                    if (!string.IsNullOrEmpty(approve))
                    {
                        item.ApprouvePar = user;
                        item.DateApproved = DateTime.Now;

                        ficheCadenassageService.ApproveFiche(item);
                    }
                    else if (!string.IsNullOrEmpty(save))
                    {
                        item.UpdatedPar = user;
                        item.DateUpdated = DateTime.Now;

                        ficheCadenassageService.SaveFiche(item);
                    }

                    ViewData["idFiche"] = item.FicheCadenassageId;
                    // return View("EditFiche", item);
                    return RedirectToAction("EditFiche", new { Id = item.FicheCadenassageId, DroitAjout = "true" });
                };
                return View("EditFiche", Layout, vm);
            }
            else
                return Json("");
        }



        public ActionResult DeleteFiche([DataSourceRequest] DataSourceRequest request, LigneCadenassageGridViewModel fiche)
        {
            if (utilisateurService.VerifierBonClientCadenassage_Fiche(fiche.FicheCadenassageId, true))
            {

                if (fiche != null)
                {
                    if (!ficheCadenassageService.Supprimer(fiche))
                    {

                        fiche = null;
                    }
                }
                return Json(new[] { fiche }.ToDataSourceResult(request, ModelState));
            }

            return Json("");


        }


        public ActionResult Index()
        {
            PopulateSections();

            bool isCorporate = false;
            bool.TryParse(Convert.ToString(HttpContext.Session["IsCorporate"]), out isCorporate);


            AffichageListesViewModel model = new AffichageListesViewModel();
            // seuls les administrateurs PREVI peuvent modifier les listes de référence, utilisés par TOUS les clients
            model.AfficherListes = User.IsInRole("Administrateur") && !isCorporate;

            ViewData["Layout"] = Layout;
            return View("Index", Layout, model);
        }


        //public ActionResult IndexTempo()
        //{

        //    return View();
        //}


        public ActionResult Edit(int Id)
        {
            ViewData["LeClient"] = Id;

            if (utilisateurService.VerifierBonClientCadenassage_Client(Id, false))
            {
                var vm = new EditCadenassageViewModel();
                var client = clientRepository.Get(Id);

                var sessionUtilisateur = utilisateurService.GetSession();


                vm.ClientId = Id;
                vm.Nom = client.Nom;
                vm.Logo = client.Thumb;


                vm.estClient = (sessionUtilisateur != null);
                vm.estUpdate = (
                      (sessionUtilisateur == null && (User.IsInRole("Administrateur") || User.IsInRole("Lecture-Écriture")))
                      || (sessionUtilisateur != null && sessionUtilisateur.AdmPrevicad)
                      );
                // par le droit d'update, pas le droit de cocher la révision active du côté client
                if (!vm.estUpdate)
                    vm.RCDisabled = "disabled";
                else
                    vm.RCDisabled = "";

                vm.estAudit = (sessionUtilisateur != null && sessionUtilisateur.Auditeur);
                vm.LienPrevicad = getLienPrevicad(Id);
                vm.IdentificateurUnique = client.IdentificateurUnique;
                if (sessionUtilisateur != null)
                {
                    vm.NotificationDebutCad = sessionUtilisateur.NotificationDebutCad;
                }

                // TODO MAX : FILTRE PAR DEPARTEMENT ET ÉQUIPEMENT EN STAND BY
                //var departements = utilisateurService.filtreDepartement();
                //var equipements = utilisateurService.filtreEquipement();

                PopulateDepartement(Id);
                ViewData["Layout"] = Layout;
                return View("Edit", Layout, vm);
            }


            return Json("");
        }

        public string getLienPrevicad(int id)
        {
            var client = this.clientRepository.Get(id);


            var urlBuilder =
            new System.UriBuilder(Request.Url.AbsoluteUri)
            {
                Path = Url.Content("~/Registre/Index?ClientID=" + client.IdentificateurUnique.ToString()),
                Query = null,
            };

            Uri uri = urlBuilder.Uri;
            string url = urlBuilder.ToString().Replace("%3F", "?");


            return url;
        }
        public ActionResult EditFiche(int Id)
        {
            if (utilisateurService.VerifierBonClientCadenassage_Fiche(Id, true))
            {
                ViewData["idFiche"] = Id;
                var vm = ficheCadenassageService.getFicheVM(Id);
                PopulateLists(vm.ClientId);

                vm.DroitAjout = true;


                var sessionUtilisateur = utilisateurService.GetSession();
                vm.estClient = (sessionUtilisateur != null);
                vm.estUpdate = (
                      (sessionUtilisateur == null && (User.IsInRole("Administrateur") || User.IsInRole("Lecture-Écriture")))
                      || (sessionUtilisateur != null && sessionUtilisateur.AdmPrevicad)
                      );


                ViewBag.Comments = "";
                var model = documentFicheNoteService.GetNotes(Id);
                if (model != null)
                {
                    ViewBag.Comments = model.Notes;
                }

                ViewData["Layout"] = Layout;
                return View("EditFiche", Layout, vm);
            }

            return Json("");


        }

        public void PopulateLists(int ClientId)
        {
            if (utilisateurService.VerifierBonClientCadenassage_Client(ClientId, false))
            {
                PopulateEquipements(ClientId);
                PopulateMateriel();
                PopulateSourceEnergie();
                PopulateInstructions();
                PopulateDepartement(ClientId);
                PopulateTitresCadenassage();
            }

        }

        public ActionResult CreerFiche(int Id)
        {
            if (utilisateurService.VerifierBonClientCadenassage_Client(Id, true))
            {
                ViewData["Layout"] = Layout;

                var vm = new EditFicheViewModel();
                vm.FicheCadenassageId = 0;
                vm.ClientId = Id;

                var user = GetCurrentUser();

                vm.CreatedPar = user;
                vm.DateCreation = DateTime.Now;

                vm.UpdatedPar = user;
                vm.DateUpdated = DateTime.Now;

                var client = clientRepository.Get(Id);
                vm.NomClient = client.Nom;


                vm.EstDocumentPrevigesst = (Request.IsAuthenticated) && (User.IsInRole("Administrateur") || User.IsInRole("Lecture-Écriture"));
                vm.TitreFiche = "Fiche de cadenassage";
                //vm.RevisionCourante = true;
                vm.AfficherClient = true;
                //  vm.DateCreation = DateTime.Today;
                vm.SourcesEnergieId = new List<int>();
                PopulateLists(Id);
                if (utilisateurService.VerifierStatusCadenassage(Id, true))
                {
                    // WELCOME
                    vm.DroitAjout = true;
                    return View("EditFiche", Layout, vm);
                }
                else
                {
                    vm.DroitAjout = false;
                    return View("EditFiche", Layout, vm);
                }
            }

            return Json("");

        }

        private void PopulateEquipements(int ClientId)
        {
            if (utilisateurService.VerifierBonClientCadenassage_Client(ClientId, false))
            {

                var DDL = equipementService.GetAllAsEquipementDDLViewModel(ClientId);

                ViewData["Equipements"] = DDL;
            }

        }

        //private void PopulatePhoto(int FicheCadenassageId)
        //{
        //    if (utilisateurService.VerifierBonClientCadenassage_Fiche(FicheCadenassageId, false))
        //    {
        //        var DDL = photoFicheCadenassageService.GetAllAsPhotoDDLViewModel(FicheCadenassageId);
        //        ViewData["Photos"] = DDL;
        //    }

        //}



        private void PopulateDepartement(int ClientId)
        {
            if (utilisateurService.VerifierBonClientCadenassage_Client(ClientId, false))
            {
                var DDL = departementService.GetAllAsDepartementDDLViewModel(ClientId);

                ViewData["Departements"] = DDL;
            }


        }



        #region Photos

        public JsonResult getPhotos(int FicheCadenassageId)
        {
            //   PopulatePhoto(FicheCadenassageId);

            if (utilisateurService.VerifierBonClientCadenassage_Fiche(FicheCadenassageId, false))
            {
                var DDL = photoFicheCadenassageService.GetAllAsPhotoDDLViewModel(FicheCadenassageId);
                // ViewData["Photos"] = DDL;

                return Json(DDL, JsonRequestBehavior.AllowGet);
            }

            return Json("");
        }

        public JsonResult getDepartements(int IDClient)
        {

            if (utilisateurService.VerifierBonClientCadenassage_Client(IDClient, false))
            {
                var DDL = departementService.GetAllAsDepartementDDLViewModel(IDClient);
                return Json(DDL, JsonRequestBehavior.AllowGet);
            }


            return Json("");
        }


        public ActionResult ReadListPhoto([DataSourceRequest] DataSourceRequest request, int ficheId)
        {
            if (utilisateurService.VerifierBonClientCadenassage_Fiche(ficheId, false))
            {
                return Json(photoFicheCadenassageService.GetListeLignesPhotos(request, ficheId), JsonRequestBehavior.AllowGet);

            }
            return Json("");
        }

        [ValidateInput(false)]
        public ActionResult SaveItemPhoto([DataSourceRequest] DataSourceRequest request, PhotoFicheCadenassageViewModel item, int ficheId)
        {
            if (utilisateurService.VerifierBonClientCadenassage_Fiche(ficheId, true))
            {
                if (item != null && ModelState.IsValid)
                {
                    item.FicheCadenassageId = ficheId;
                    photoFicheCadenassageService.SaveLigneCadenassagePhoto(item);

                    // update approve and modified fields
                    ficheCadenassageService.UnapproveFiche(ficheId, GetCurrentUser());
                }
                //PopulatePhoto(ficheId);
                return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            }
            //PopulatePhoto(ficheId);
            return Json("");

        }

        [ValidateInput(false)]
        public ActionResult DeleteItemPhoto([DataSourceRequest] DataSourceRequest request, PhotoFicheCadenassageViewModel item)
        {
            if (utilisateurService.VerifierBonClientCadenassage_Fiche(item.FicheCadenassageId, true))
            {
                if (item != null)
                {
                    if (!photoFicheCadenassageService.Supprimer(item))
                    {
                        item = null;
                    }
                    else
                    {
                        // update approve and modified fields
                        ficheCadenassageService.UnapproveFiche(item.FicheCadenassageId, GetCurrentUser());
                    }

                    //  PopulatePhoto(item.FicheCadenassageId);
                    return Json(new[] { item }.ToDataSourceResult(request, ModelState));
                }
            }

            return Json("");

        }

        public JsonResult SaveLinkPhoto(IEnumerable<HttpPostedFileBase> file, int PhotoFicheCadenassageId, int Rang, int FicheCadenassageId, string Localisation, string LocalisationEN)
        {


            if (PhotoFicheCadenassageId == 0)
            {
                var d = new PhotoFicheCadenassageViewModel();

                var photos = ficheCadenassageRepository.Get(FicheCadenassageId).PhotosFicheCadenassage.ToList();
                if (Rang == 0)
                {
                    if (photos.Count == 0)
                        d.Rang = 1;
                    else
                        d.Rang = photos.Max(x => x.Rang) + 1;
                }
                else
                {
                    d.Rang = Rang;
                }

                d.FicheCadenassageId = FicheCadenassageId;
                d.Localisation = Localisation;
                d.LocalisationEN = LocalisationEN;
                photoFicheCadenassageService.SaveLigneCadenassagePhoto(d);
                PhotoFicheCadenassageId = d.PhotoFicheCadenassageId;

                // update approve and modified fields
                ficheCadenassageService.UnapproveFiche(FicheCadenassageId, GetCurrentUser());
            }
            else
            {
                var photo = photoFicheCadenassageService.getVM(PhotoFicheCadenassageId);
                photo.Rang = Rang;
                photo.Localisation = Localisation;
                photo.LocalisationEN = LocalisationEN;
                photoFicheCadenassageService.SaveLigneCadenassagePhoto(photo);

                // update approve and modified fields
                ficheCadenassageService.UnapproveFiche(FicheCadenassageId, GetCurrentUser());
            }

            uploadController.SaveLinkWithContext(file, PhotoFicheCadenassageId, Enums.TypeUpload.ImageCadenassage, this.ControllerContext.HttpContext.Server);



            var item = photoFicheCadenassageService.getVM(PhotoFicheCadenassageId);

            //    PopulatePhoto(FicheCadenassageId);
            return Json(new
            {
                PhotoFicheCadenassageId = item.PhotoFicheCadenassageId,
                FicheCadenassageId = item.FicheCadenassageId,
                Rang = item.Rang,
                URL = item.URL,
                Suppressible = item.Suppressible,
                Localisation = item.Localisation,
                LocalisationEN = item.LocalisationEN

            }, JsonRequestBehavior.AllowGet);


        }

        #endregion

        #region Ligne_decadenassage


        public ActionResult ReadListDecadenassage([DataSourceRequest] DataSourceRequest request, int ficheId)
        {
            if (utilisateurService.VerifierBonClientCadenassage_Fiche(ficheId, false))
                return Json(ligneDecadenassageService.GetListeLignesDecadenassage(request, ficheId), JsonRequestBehavior.AllowGet);
            else
                return Json("");
        }


        public ActionResult SaveItemDecadenassage([DataSourceRequest] DataSourceRequest request, LigneDecadenassageViewModel item, int ficheId)
        {// bug du numeric updown, ne s'ajuste pas bien sur changemetn de locale
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
                                item.NoLigne = newValue;
                                v.Errors.Clear();
                            }

                        }
                    }
                }


            }

            if (utilisateurService.VerifierBonClientCadenassage_Fiche(ficheId, true))
            {

                if (item != null && ModelState.IsValid)
                {
                    item.FicheCadenassageId = ficheId;
                    ligneDecadenassageService.SaveLigneDecadenassage(item);
                    item = ligneDecadenassageService.getLigneVM(item.LigneDecadenassageId);

                    // update approve and modified fields
                    ficheCadenassageService.UnapproveFiche(ficheId, GetCurrentUser());
                }
                return Json(new[] { item }.ToDataSourceResult(request, ModelState));

            }
            return Json("");

        }

        public ActionResult DeleteItemDecadenassage([DataSourceRequest] DataSourceRequest request, LigneDecadenassageViewModel item)
        {
            if (utilisateurService.VerifierBonClientCadenassage_Fiche(item.FicheCadenassageId, true))
            {
                if (item != null)
                {
                    if (!ligneDecadenassageService.Supprimer(item))
                    {

                        item = null;
                    }
                    else
                    {
                        // update approve and modified fields
                        ficheCadenassageService.UnapproveFiche(item.FicheCadenassageId, GetCurrentUser());
                    }
                }
                foreach (var e in ModelState.Values)
                {
                    e.Errors.Clear();
                }
                return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            }

            return Json("");


        }

        #endregion

        #region Materiel


        public ActionResult ReadListMateriel([DataSourceRequest] DataSourceRequest request, int ficheId)
        {
            if (utilisateurService.VerifierBonClientCadenassage_Fiche(ficheId, false))
            {
                return Json(materielRequisCadenassageService.GetListeLignesMateriel(request, ficheId), JsonRequestBehavior.AllowGet);

            }
            return Json("");
        }


        public ActionResult SaveItemMateriel([DataSourceRequest] DataSourceRequest request, MaterielRequisCadenassageViewModel item, int ficheId)
        {
            if (utilisateurService.VerifierBonClientCadenassage_Fiche(ficheId, true))
            {
                if (item != null && ModelState.IsValid)
                {
                    item.FicheCadenassageId = ficheId;
                    materielRequisCadenassageService.SaveLigneCadenassageMateriel(item);

                    // update approve and modified fields
                    ficheCadenassageService.UnapproveFiche(ficheId, GetCurrentUser());
                }
                return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            }
            return Json("");

        }

        public ActionResult DeleteItemMateriel([DataSourceRequest] DataSourceRequest request, MaterielRequisCadenassageViewModel item)
        {
            if (utilisateurService.VerifierBonClientCadenassage_Fiche(item.FicheCadenassageId, true))
            {
                if (item != null)
                {
                    if (!materielRequisCadenassageService.Supprimer(item))
                    {
                        item = null;
                    }
                    else
                    {
                        // update approve and modified fields
                        ficheCadenassageService.UnapproveFiche(item.FicheCadenassageId, GetCurrentUser());
                    }

                    return Json(new[] { item }.ToDataSourceResult(request, ModelState));
                }
            }

            return Json("");
        }


        private void PopulateMateriel()
        {
            var DDL = materielService.GetAllMaterielDDLViewModel();

            ViewData["Materiel"] = DDL;
        }




        private void PopulateTitresCadenassage()
        {
            var DDL = cadenasService.GetAllTitresCadenassageDDLViewModel();
            ViewData["Titres"] = DDL;
        }

        #endregion

        #region SourceEnergie
        private void PopulateSourceEnergie()
        {
            var DDL = sourceEnergieService.GetAllAsSourceEnergieDDLViewModel();

            ViewData["Sources"] = DDL;

        }

        public int SaveSourcesAjax(IEnumerable<int> Sources, int FicheCadenassageId)
        {
            if (utilisateurService.VerifierBonClientCadenassage_Fiche(FicheCadenassageId, true))
            {
                var itemSources = new SourceEnergieCadenassageViewModel { FicheCadenassageId = FicheCadenassageId };
                var listeInt = new List<int>();
                foreach (var s in Sources)
                    listeInt.Add(s);

                itemSources.SourcesEnergieId = listeInt;

                if (this.sourceEnergieCadenassageRepository.CheckIfChanged(itemSources))
                {
                    this.sourceEnergieCadenassageRepository.UpdateSources(itemSources);

                    // update approve and modified fields
                    ficheCadenassageService.UnapproveFiche(FicheCadenassageId, GetCurrentUser());
                    return 1;
                }
            }

            return 0;
        }

        public ActionResult SaveSources(SourceEnergieCadenassageViewModel item)
        {
            if (utilisateurService.VerifierBonClientCadenassage_Fiche(item.FicheCadenassageId, true))
            {
                if (item != null && ModelState.IsValid)
                {

                    this.sourceEnergieCadenassageRepository.UpdateSources(item);

                    // update approve and modified fields
                    ficheCadenassageService.UnapproveFiche(item.FicheCadenassageId, GetCurrentUser());
                };

                var vm = ficheCadenassageService.getFicheVM(item.FicheCadenassageId);
                vm.DisplaySourceTab = true;
                PopulateLists(vm.ClientId);

                return View("EditFiche", vm);
            }
            return Json("");

        }
        #endregion

        #region LignesInstruction


        public ActionResult ReadListCadenassage([DataSourceRequest] DataSourceRequest request, int ficheId)
        {
            if (utilisateurService.VerifierBonClientCadenassage_Fiche(ficheId, false))
                return Json(ligneInstructionService.GetListeLignesInstruction(request, ficheId), JsonRequestBehavior.AllowGet);
            else
                return Json("");
        }


        public ActionResult SaveItemCadenassage([DataSourceRequest] DataSourceRequest request, LigneInstructionViewModel item, int ficheId)
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
                                item.NoLigne = newValue;
                                v.Errors.Clear();
                            }

                        }
                    }
                }


            }
            if (utilisateurService.VerifierBonClientCadenassage_Fiche(ficheId, true))
            {
                if (item != null && ModelState.IsValid)
                {
                    item.FicheCadenassageId = ficheId;
                    ligneInstructionService.SaveLigneInstruction(item);
                    item = ligneInstructionService.getLigneVM(item.LigneInstructionId);

                    // update approve and modified fields
                    ficheCadenassageService.UnapproveFiche(ficheId, GetCurrentUser());
                }
                return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            }

            return Json("");


        }

        public ActionResult DeleteItemCadenassage([DataSourceRequest] DataSourceRequest request, LigneInstructionViewModel item)
        {
            if (utilisateurService.VerifierBonClientCadenassage_Fiche(item.FicheCadenassageId, true))
            {
                if (item != null)
                {
                    if (!ligneInstructionService.Supprimer(item))
                    {
                        item = null;
                    }
                    else
                    {
                        // update approve and modified fields
                        ficheCadenassageService.UnapproveFiche(item.FicheCadenassageId, GetCurrentUser());
                    }
                }

                foreach (var e in ModelState.Values)
                {
                    e.Errors.Clear();
                }
                return Json(new[] { item }.ToDataSourceResult(request, ModelState));

            }

            return Json("");
        }


        private void PopulateInstructions()
        {
            var DDL = instructionService.GetInstructionDDL();

            ViewData["Instructions"] = DDL;

        }

        #endregion


        #region Utilitaire
        [HttpPost]
        public int RenumeroterLignes(int FicheCadenassageId)
        {
            if (utilisateurService.VerifierBonClientCadenassage_Fiche(FicheCadenassageId, true))
            {
                ficheCadenassageService.RenumeroterLignes(FicheCadenassageId);

            }


            return 0;
        }

        public int RenumeroterLignesDEC(int FicheCadenassageId)
        {
            if (utilisateurService.VerifierBonClientCadenassage_Fiche(FicheCadenassageId, true))
                ficheCadenassageService.RenumeroterLignesDEC(FicheCadenassageId);


            return 0;
        }

        public ActionResult DuplicateFiche(int Id)
        {
            if (utilisateurService.VerifierBonClientCadenassage_Fiche(Id, true))
            {

                var newFicheId = ficheCadenassageService.DupliquerFiche(Id);
                return RedirectToAction("EditFiche", new { Id = newFicheId });
            }
            return Json("");
        }


        #endregion

        public int GetNewValueInstruction(int id)
        {
            return ligneInstructionService.getNextRankInstruction(id);
        }

        public int GetNewValueDecadenassage(int id)
        {
            return ligneDecadenassageService.getNextRankDecadenassage(id);
        }



        public ActionResult ReadListEmployesCadenassage([DataSourceRequest] DataSourceRequest request, int IDClient)
        {
            PopulateDepartement(IDClient);
            if (utilisateurService.VerifierBonClientCadenassage_Client(IDClient, false))
                return Json(employeRegistreService.GetListeEmployes(request, IDClient), JsonRequestBehavior.AllowGet);
            else
                return Json("");
        }


        public ActionResult SaveItemEmployesCadenassage([DataSourceRequest] DataSourceRequest request, EmployeRegistreViewModel item, int IDClient)
        {
            if (utilisateurService.VerifierBonClientCadenassage_Client(IDClient, false))
            {
                if (item != null && ModelState.IsValid)
                {
                    if (!employeRegistreService.estUnique(item, IDClient))
                    {
                        ModelState.AddModelError("NoEmploye", "Ce numéro d'employé existe déjà");
                        return Json(new[] { item }.ToDataSourceResult(request, ModelState));
                    }
                    else if (employeRegistreService.estMaxActif(item, IDClient))
                    {
                        ModelState.AddModelError("Actif", "Dépassement du nombre d'employés actifs alloué");
                        return Json(new[] { item }.ToDataSourceResult(request, ModelState));
                    }
                    else
                    {
                        item.ClientId = IDClient;
                        employeRegistreService.SaveEmployeRegistre(item);
                        item = employeRegistreService.getEmployeRegistreVM(item.EmployeRegistreId);
                    }
                }
                return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            }
            return Json("");
        }

        public void ActiverDesactiver(int ficheId, bool etat)
        {
            // not needed with approved functionality 

            //if (utilisateurService.VerifierBonClientCadenassage_Fiche(ficheId, false))
            //{
            //    if (utilisateurService.VerifierLimiteFicheCadenassage(etat) == true)
            //    {
            //        var vm = ficheCadenassageService.getFicheVM(ficheId);
            //        vm.RevisionCourante = etat;
            //        ficheCadenassageService.SaveFiche(vm);
            //    }
            //}
        }

        public bool activeFilter()
        {
            // FUNCTION TO SERVICES
            return true;
        }

        public ActionResult DeleteItemEmployesCadenassage([DataSourceRequest] DataSourceRequest request, EmployeRegistreViewModel item)
        {
            if (utilisateurService.VerifierBonClientCadenassage_Client(item.ClientId, false))
            {
                if (item != null)
                {
                    if (!employeRegistreService.Supprimer(item))
                    {
                        item = null;
                    }
                }
                return Json(new[] { item }.ToDataSourceResult(request, ModelState));
            }
            return Json("");
        }


        public ActionResult SaveParametersCadAdmin([DataSourceRequest] DataSourceRequest request, EditCadenassageViewModel item)
        {

            var verificationUtilisateurEnLigne = utilisateurService.VerificationUtilisateurEnLigne(item.ClientId);
            var session = utilisateurService.GetSession();

            if (verificationUtilisateurEnLigne != null)
            {

                if (item != null)
                {
                    utilisateurService.SaveParametersCadAdmin(item, session.UtilisateurId);

                    return RedirectToAction("Edit", new { Id = item.ClientId });

                }
            }

            return Json("");
        }


        #region ----- Equipement Articuloe -----

        public ActionResult EquipementArticuloes(int id)
        {
            PopulateMateriel();
            PopulateDispositifs();
            PopulateSourceEnergie();


            ViewData["EqID"] = id;
            ViewData["Layout"] = Layout;

            var session = utilisateurService.GetSession();
            if (session != null)
            {
                ViewData["LeClient"] = session.ClientId;
            }
            return View();
        }



        public ActionResult ReadListArticuloe([DataSourceRequest] DataSourceRequest request, int equipementId)
        {
            return Json(equipementArticuloeService.GetReadListeEquipementArticuloes(request, equipementId), JsonRequestBehavior.AllowGet);
        }


        public ActionResult SaveArticuloe([DataSourceRequest] DataSourceRequest request, EquipementArticuloViewModel item)
        {
            if (item != null && ModelState.IsValid)
            {
                equipementArticuloeService.SaveEquipementArticula(item);
            }
            return Json(new[] { item }.ToDataSourceResult(request, ModelState));
        }


        public ActionResult DeleteArticuloe([DataSourceRequest] DataSourceRequest request, EquipementArticuloViewModel item)
        {
            if (item != null)
            {
                if (!equipementArticuloeService.Supprimer(item))
                {

                    item = null;
                }
            }
            return Json(new[] { item }.ToDataSourceResult(request, ModelState));
        }


        private void PopulateDispositifs()
        {
            var dispositifDDL = dispositifService.GetAllAsDispositifsDDLViewModel();

            ViewData["Dispositifs"] = dispositifDDL;
        }


        #endregion

        private string GetCurrentUser()
        {
            var user = "";

            if (utilisateurService.GetSession() != null)
            {
                // on est dans le mode BLEU, accès administratif mais Client
                user = utilisateurService.GetSession().NomUtilisateur;
            }
            else
            {
                // version super admin
                user = System.Web.HttpContext.Current.User.Identity.Name;
            }

            return user;
        }

        public JsonResult GetUpdateInfo(int id)
        {
            var model = ficheCadenassageRepository.Get(id);

            if (model != null)
            {
                return Json(new { Success = "1", model.UpdatedPar, DateUpdated = model.DateUpdated.Value.ToString(), model.ApprouvePar, model.DateApproved }, JsonRequestBehavior.AllowGet);
            }


            return Json(new { Success = "0" }, JsonRequestBehavior.AllowGet);
        }


        //public ActionResult PrintQR(int id)
        //{
        //    var equipments = equipementService.GetEquipementsWithQR(id);
        //    return View(equipments);
        //}

        public ActionResult PrintQR(int id, string opt, string ori, string equ)
        {
            var equipments = equipementService.GetEquipementsWithQR(id, equ);
            return View(equipments);
        }

        public ActionResult OnlyWithQR(int id)
        {
            var equipments = equipementService.GetEquipementsWithQR(id);
            return View(equipments);
        }

        public JsonResult OnlyWithQRData(int id)
        {
            var equipments = equipementService.GetEquipementsWithQR(id);
            return Json(equipments, JsonRequestBehavior.AllowGet);
        }
    }
}