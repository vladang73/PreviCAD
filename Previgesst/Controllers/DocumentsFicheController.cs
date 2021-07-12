using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Previgesst.ActionFilters;
using Previgesst.Repositories;
using Previgesst.Services;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Previgesst.Controllers
{
    [Authorize]
    public class DocumentsFicheController : Controller
    {
        private DocumentService documentService;
        private DocumentRepository documentRepository;
        private SectionService sectionService;
        private ApplicationPrevisService applicationPrevisService;
        private DocumentFicheService documentFicheService;
        private DocumentFicheRepository documentFicheRepository;
        private UtilisateurService utilisateurService;
        private FicheCadenassageService ficheCadenassageService;
        private DocumentFicheNoteService documentFicheNoteService;


        public DocumentsFicheController(DocumentService documentService, DocumentRepository documentRepository, SectionService sectionService, UtilisateurService utilisateurService,
            ApplicationPrevisService applicationPrevisService, DocumentFicheService documentFicheService, DocumentFicheRepository documentFicheRepository,
            FicheCadenassageService ficheCadenassageService, DocumentFicheNoteService documentFicheNoteService)
        {
            this.documentService = documentService;
            this.documentRepository = documentRepository;
            this.sectionService = sectionService;
            this.applicationPrevisService = applicationPrevisService;
            this.documentFicheService = documentFicheService;
            this.documentFicheRepository = documentFicheRepository;

            this.utilisateurService = utilisateurService;
            this.ficheCadenassageService = ficheCadenassageService;
            this.documentFicheNoteService = documentFicheNoteService;
        }

        // GET: Documents
        public ActionResult Index()
        {
            var v = new DroitsViewModel();

            v.estUpdate = User.IsInRole("Administrateur") || User.IsInRole("Lecture-Écriture");


            PopulateSections(applicationPrevisService.getIdByName(Enums.Applications.Generale));
            return View(v);
        }

        private void PopulateSections(int ApplicationId)
        {
            var sectionDDL = sectionService.GetAllAsSectionsDDLViewModel(ApplicationId);

            ViewData["Sections"] = sectionDDL;
            ViewData["DefaultSection"] = sectionDDL.FirstOrDefault();
        }


        /*

        public ActionResult SaveAR([DataSourceRequest] DataSourceRequest request, DocumentViewModel item)
        {
            if (item != null && ModelState.IsValid)
            {

                documentService.SaveDocumentAR(item);
            }
            return Json(new[] { item }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult SaveGEN([DataSourceRequest] DataSourceRequest request, DocumentViewModel item)
        {
            if (item != null && ModelState.IsValid)
            {

                documentService.SaveDocumentGEN(item);
            }
            return Json(new[] { item }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult Create()
        {
            return View();
        }

        

        public ActionResult ReadListCadenassage([DataSourceRequest] DataSourceRequest request)
        {
            return Json(documentService.GetReadListDocument(request, Enums.Applications.Cadenassage));
        }

        public ActionResult ReadListDocumentsGeneraux([DataSourceRequest] DataSourceRequest request)
        {
            return Json(documentService.GetReadListDocument(request, Enums.Applications.Generale), JsonRequestBehavior.AllowGet);
        }


        public ActionResult DeleteDOC([DataSourceRequest] DataSourceRequest request, DocumentViewModel document)
        {
            if (document != null)
            {
                if (!documentService.Supprimer(document))
                {

                    document = null;
                }
            }

            return Json(new[] { document }.ToDataSourceResult(request, ModelState));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult InlineDelete([DataSourceRequest] DataSourceRequest request, DocumentViewModel item)
        {
            if (item != null)
            {
                documentRepository.Delete(item.DocumentId);
            }

            return Json(new[] { item }.ToDataSourceResult(request, ModelState));
        }

        



        public JsonResult SaveLink(IEnumerable<HttpPostedFileBase> file, int DocumentId, string Titre, int? Section, int? Ordre)
        {
            DocumentViewModel vm = new DocumentViewModel();
            var sectionTexte = "";


            if (DocumentId == 0)
            {
                var d = new DocumentViewModel();

                ////////////if (Titre == "")
                ////////////    Titre = "À remplir";

                d.SectionId = Section ?? 0;
                d.Titre = Titre;
                d.Ordre = Ordre ?? 0;
                documentService.SaveDocumentAR(d);
                DocumentId = d.DocumentId;
            }


            foreach (var fichier in file)
            {
                if (fichier != null && fichier.ContentLength > 0)
                {

                    var repertoire = @"~/docFiles/" + DocumentId + "/";
                    if (!System.IO.Directory.Exists(Server.MapPath(repertoire)))
                        System.IO.Directory.CreateDirectory(Server.MapPath(repertoire));
                    else
                    {
                        foreach (var f in System.IO.Directory.GetFiles(Server.MapPath(repertoire)))
                        {
                            System.IO.File.Delete(f);
                        }
                    }

                    fichier.SaveAs(System.IO.Path.Combine(Server.MapPath(repertoire), fichier.FileName));
                    var mydoc = documentRepository.Get(DocumentId);
                    mydoc.FileName = fichier.FileName;
                    documentRepository.Update(mydoc);
                    documentRepository.SaveChanges();

                    sectionTexte = mydoc.Section.Nom;
                }
            }

            vm = documentService.GetVM(DocumentId);
            return Json(new { Type = "Upload", DocumentId = vm.DocumentId, NomFichier = vm.FileName, Section = vm.SectionId, Titre = vm.Titre, Ordre = vm.Ordre, SectionTexte = vm.DisplaySection, BasePath = vm.BasePath }, JsonRequestBehavior.AllowGet);
        }

        */

        public ActionResult ReadListDocFicheCadenassage([DataSourceRequest] DataSourceRequest request, int id)
        {

            return Json(documentFicheService.GetReadListDocumentFiche(request, Enums.Applications.Cadenassage, id));
        }

        public ActionResult SaveDocFicheCadenassage([DataSourceRequest] DataSourceRequest request, DocumentFicheViewModel item, int id)
        {
            if (item != null && ModelState.IsValid)
            {
                item.ApplicationPreviId = applicationPrevisService.getIdByName(Enums.Applications.Cadenassage);
                item.FicheCadenassageId = id;
                documentFicheService.SaveDocumentFiche(item);

                // update approve and modified fields
                ficheCadenassageService.UnapproveFiche(item.FicheCadenassageId, GetCurrentUser());
            }
            return Json(new[] { item }.ToDataSourceResult(request, ModelState));
        }



        public ActionResult DeleteDocumentFiche([DataSourceRequest] DataSourceRequest request, DocumentFicheViewModel document)
        {
            if (document != null)
            {
                if (!documentFicheService.Supprimer(document))
                {
                    document = null;
                }
                else
                {
                    // update approve and modified fields
                    ficheCadenassageService.UnapproveFiche(document.FicheCadenassageId, GetCurrentUser());
                }
            }
            return Json(new[] { document }.ToDataSourceResult(request, ModelState));
        }


        public JsonResult SaveDocumentFicheLinkCadenassage(IEnumerable<HttpPostedFileBase> file, int DocumentFicheId, string Titre, int FicheId)
        {
            return SaveDocumentFicheLink(file, DocumentFicheId, Titre, FicheId, Enums.Applications.Cadenassage);
        }




        public JsonResult SaveDocumentFicheLink(IEnumerable<HttpPostedFileBase> file, int DocumentFicheId, string Titre, int FicheId, string type)
        {
            var nomFichier = "";

            if (DocumentFicheId == 0)
            {
                var d = new DocumentFicheViewModel();

                ////////////if (Titre == "")
                ////////////    Titre = "À remplir";


                d.Titre = Titre;
                d.FicheCadenassageId = FicheId;
                d.ApplicationPreviId = applicationPrevisService.getIdByName(type);
                this.documentFicheService.SaveDocumentFiche(d);
                DocumentFicheId = d.DocumentFicheId;

                if (DocumentFicheId > 0)
                {
                    // update approve and modified fields
                    ficheCadenassageService.UnapproveFiche(FicheId, GetCurrentUser());
                }
            }


            foreach (var fichier in file)
            {
                if (fichier != null && fichier.ContentLength > 0)
                {

                    var repertoire = @"~/FicheDocFiles/" + DocumentFicheId + "/";
                    if (!System.IO.Directory.Exists(Server.MapPath(repertoire)))
                        System.IO.Directory.CreateDirectory(Server.MapPath(repertoire));
                    else
                    {
                        foreach (var f in System.IO.Directory.GetFiles(Server.MapPath(repertoire)))
                        {
                            System.IO.File.Delete(f);
                        }
                    }

                    fichier.SaveAs(System.IO.Path.Combine(Server.MapPath(repertoire), fichier.FileName));
                    var mydoc = documentFicheRepository.Get(DocumentFicheId);
                    mydoc.FileName = fichier.FileName;
                    documentFicheRepository.Update(mydoc);
                    nomFichier = fichier.FileName;

                    documentFicheRepository.SaveChanges();
                }
            }
            var vm = documentFicheService.getVM(DocumentFicheId);

            return Json(new { Type = "Upload", DocumentFicheId = vm.DocumentFicheId, NomFichier = vm.FileName, Titre = vm.Titre, BasePath = vm.BasePath }, JsonRequestBehavior.AllowGet);
        }

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

        //[HttpPost]
        public JsonResult SaveDocNotesAjax(int FicheCadenassageId, string Notes)
        {
            if (documentFicheNoteService.isChanged(FicheCadenassageId, Notes))
            {
                documentFicheNoteService.SaveNotes(new Models.DocumentFicheNote { FicheCadenassageId = FicheCadenassageId, Notes = Notes });

                // update approve and modified fields
                ficheCadenassageService.UnapproveFiche(FicheCadenassageId, GetCurrentUser());

                return Json("1", JsonRequestBehavior.AllowGet);
            }

            return Json("0", JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateArchive(int id)
        {
            if (utilisateurService.VerifierBonClientCadenassage_Fiche(id, false))
            {
                var d = new DocumentFicheViewModel();


                d.Titre = "Archive " + DateTime.Now.ToString();
                d.FicheCadenassageId = id;
                d.ApplicationPreviId = applicationPrevisService.getIdByName(Enums.Applications.Cadenassage);
                this.documentFicheService.SaveDocumentFiche(d);
                int DocumentFicheId = d.DocumentFicheId;

                if (DocumentFicheId > 0)
                {
                    // update approve and modified fields
                    ficheCadenassageService.UnapproveFiche(id, GetCurrentUser());
                }


                if (DocumentFicheId > 0)
                {
                    var repertoire = @"~/FicheDocFiles/" + DocumentFicheId + "/";

                    if (!System.IO.Directory.Exists(Server.MapPath(repertoire)))
                        System.IO.Directory.CreateDirectory(Server.MapPath(repertoire));

                    string fileName = "";
                    ficheCadenassageService.ReturnArchive(id, "fr", Server.MapPath(repertoire), ref fileName);

               
                    var mydoc = documentFicheRepository.Get(DocumentFicheId);
                    mydoc.FileName = fileName;
                    mydoc.Titre = fileName.Replace(".pdf", "");

                    documentFicheRepository.Update(mydoc);
                    documentFicheRepository.SaveChanges();
                }


                return Json("1", JsonRequestBehavior.AllowGet);
            }

            return Json("0", JsonRequestBehavior.AllowGet);
        }

        #region ----- AnalyseRisque -----

        /*
        public ActionResult ReadListAnalyseRisque([DataSourceRequest] DataSourceRequest request)
        {
            return Json(documentService.GetReadListDocument(request, Enums.Applications.Analyse));
        }

        public JsonResult SaveDocumentFicheLinkAnalyse(IEnumerable<HttpPostedFileBase> file, int DocumentFicheId, string Titre, int FicheId)
        {
            return SaveDocumentFicheLink(file, DocumentFicheId, Titre, FicheId, Enums.Applications.Analyse);
        }

        public ActionResult ReadListDocFicheAnalyse([DataSourceRequest] DataSourceRequest request, int id)
        {
            return Json(documentFicheService.GetReadListDocumentFiche(request, Enums.Applications.Analyse, id));
        }

        public ActionResult SaveDocFicheAnalyse([DataSourceRequest] DataSourceRequest request, DocumentFicheViewModel item, int id)
        {
            if (item != null && ModelState.IsValid)
            {
                item.ApplicationPreviId = applicationPrevisService.getIdByName(Enums.Applications.Analyse);
                item.FicheCadenassageId = id;
                documentFicheService.SaveDocumentFiche(item);
            }
            return Json(new[] { item }.ToDataSourceResult(request, ModelState));
        }
        */

        #endregion
    }

}