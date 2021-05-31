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
    public class DocumentsController : Controller
    {
        private DocumentService documentService;
        private DocumentRepository documentRepository;
        private SectionService sectionService;
        private ApplicationPrevisService applicationPrevisService;
        private DocumentClientService documentClientService;
        private DocumentClientRepository documentClientRepository;


        public DocumentsController(DocumentService documentService, DocumentRepository documentRepository, SectionService sectionService,
            ApplicationPrevisService applicationPrevisService, DocumentClientService documentClientService, DocumentClientRepository documentClientRepository)
        {
            this.documentService = documentService;
            this.documentRepository = documentRepository;
            this.sectionService = sectionService;
            this.applicationPrevisService = applicationPrevisService;
            this.documentClientService = documentClientService;
            this.documentClientRepository = documentClientRepository;

        }
        // GET: Documents
        public ActionResult Index()
        {
            var v = new DroitsViewModel();

            v.estUpdate = User.IsInRole("Administrateur") || User.IsInRole("Lecture-Écriture");
                 
             
            PopulateSections(applicationPrevisService.getIdByName(Enums.Applications.Generale));
            return View(v);
        }

        public ActionResult SaveAR([DataSourceRequest] DataSourceRequest request,
               DocumentViewModel item)
        {
            if (item != null && ModelState.IsValid)
            {

                documentService.SaveDocumentAR(item);
            }
            return Json(new[] { item }.ToDataSourceResult(request, ModelState));

        }

        public ActionResult SaveGEN([DataSourceRequest] DataSourceRequest request,
             DocumentViewModel item)
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

        public ActionResult ReadListAnalyseRisque([DataSourceRequest]DataSourceRequest request)
        {
            return Json(documentService.GetReadListDocument(request, Enums.Applications.Analyse));
        }

        public ActionResult ReadListCadenassage([DataSourceRequest]DataSourceRequest request)
        {
            return Json(documentService.GetReadListDocument(request, Enums.Applications.Cadenassage));
        }

        public ActionResult ReadListDocumentsGeneraux([DataSourceRequest]DataSourceRequest request)
        {

            return Json(documentService.GetReadListDocument(request, Enums.Applications.Generale), JsonRequestBehavior.AllowGet);
        }


        //public ActionResult ReadListCadenas([DataSourceRequest]DataSourceRequest request)
        //{
        //    return Json(documentService.GetReadListCadenas(request));
        //}


        public ActionResult DeleteDOC([DataSourceRequest] DataSourceRequest request,
          DocumentViewModel document)
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
        public ActionResult InlineDelete([DataSourceRequest] DataSourceRequest request,
          DocumentViewModel item)
        {
            if (item != null)
            {
                documentRepository.Delete(item.DocumentId);
            }
            return Json(new[] { item }.ToDataSourceResult(request, ModelState));
        }

        private void PopulateSections(int ApplicationId)
        {
            var sectionDDL = sectionService.GetAllAsSectionsDDLViewModel(ApplicationId);

            ViewData["Sections"] = sectionDDL;
            ViewData["DefaultSection"] = sectionDDL.FirstOrDefault();
        }



        public JsonResult SaveLink(IEnumerable<HttpPostedFileBase> file, int DocumentId, string Titre, int? Section, int? Ordre)
        {
            DocumentViewModel vm= new DocumentViewModel();
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
            return Json(new { Type = "Upload", DocumentId = vm.DocumentId, NomFichier = vm.FileName, Section = vm.SectionId, Titre = vm.Titre, Ordre = vm.Ordre, SectionTexte = vm.DisplaySection, BasePath=vm.BasePath }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ReadListDocClientCadenassage([DataSourceRequest]DataSourceRequest request, int id)
        {
            return Json(documentClientService.GetReadListDocumentClient(request, Enums.Applications.Cadenassage, id));

        }

        public ActionResult SaveDocClientCadenassage([DataSourceRequest] DataSourceRequest request,
        DocumentClientViewModel item, int id)
        {
            if (item != null && ModelState.IsValid)
            {
                item.ApplicationPreviId = applicationPrevisService.getIdByName(Enums.Applications.Cadenassage);
                item.ClientId = id;
                documentClientService.SaveDocumentClient(item);
            }
            return Json(new[] { item }.ToDataSourceResult(request, ModelState));

        }



        public ActionResult DeleteDocumentClient([DataSourceRequest] DataSourceRequest request,
         DocumentClientViewModel document)
        {
            if (document != null)
            {
                if (!documentClientService.Supprimer(document))
                {

                    document = null;
                }
            }
            return Json(new[] { document }.ToDataSourceResult(request, ModelState));

        }


        public JsonResult SaveDocumentClientLinkCadenassage(IEnumerable<HttpPostedFileBase> file, int DocumentClientId, string Titre, int ClientId)
        {
           
            return SaveDocumentClientLink(file, DocumentClientId, Titre, ClientId, Enums.Applications.Cadenassage);
        }

        public JsonResult SaveDocumentClientLinkAnalyse(IEnumerable<HttpPostedFileBase> file, int DocumentClientId, string Titre, int ClientId)
        {

            return SaveDocumentClientLink(file, DocumentClientId, Titre, ClientId, Enums.Applications.Analyse);
        }

        public JsonResult SaveDocumentClientLink(IEnumerable<HttpPostedFileBase> file, int DocumentClientId, string Titre, int ClientId, string type)
        {

            var nomFichier = "";

            if (DocumentClientId == 0)
            {
                var d = new DocumentClientViewModel();

                ////////////if (Titre == "")
                ////////////    Titre = "À remplir";


                d.Titre = Titre;
                d.ClientId = ClientId;
                d.ApplicationPreviId = applicationPrevisService.getIdByName(type);
                this.documentClientService.SaveDocumentClient(d);
                DocumentClientId = d.DocumentClientId;


            }


            foreach (var fichier in file)
            {
                if (fichier != null && fichier.ContentLength > 0)
                {

                    var repertoire = @"~/ClientDocFiles/" + DocumentClientId + "/";
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
                    var mydoc = documentClientRepository.Get(DocumentClientId);
                    mydoc.FileName = fichier.FileName;
                    documentClientRepository.Update(mydoc);
                    nomFichier = fichier.FileName;
                   
                    documentRepository.SaveChanges();
                }
            }
            var vm = documentClientService.getVM(DocumentClientId);

            return Json(new { Type = "Upload", DocumentClientId = vm.DocumentClientId, NomFichier = vm.FileName, Titre = vm.Titre, BasePath=vm.BasePath }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ReadListDocClientAnalyse([DataSourceRequest]DataSourceRequest request, int id)
    {
        return Json(documentClientService.GetReadListDocumentClient(request, Enums.Applications.Analyse, id));

    }

    public ActionResult SaveDocClientAnalyse([DataSourceRequest] DataSourceRequest request,
    DocumentClientViewModel item, int id)
    {
        if (item != null && ModelState.IsValid)
        {
            item.ApplicationPreviId = applicationPrevisService.getIdByName(Enums.Applications.Analyse);
            item.ClientId = id;
            documentClientService.SaveDocumentClient(item);
        }
        return Json(new[] { item }.ToDataSourceResult(request, ModelState));

    }




       
    }

}