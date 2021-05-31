using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Previgesst.Repositories;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Previgesst.Services
{
    public class DocumentService
    {
        private DocumentRepository documentRepository;
        private ApplicationPrevisService applicationPreviService;
        private DocumentClientRepository documentClientRepository;

        public DocumentService(DocumentRepository documentRepository, ApplicationPrevisService applicationPreviService, DocumentClientRepository documentClientRepository )
        {
            this.documentRepository = documentRepository;
            this.applicationPreviService = applicationPreviService;
            this.documentClientRepository = documentClientRepository;

        }

        internal DocumentViewModel GetVM( int documentId)
        {
            var baseURL = Helpers.URLHelper.GetBaseUrl();
            var doc = documentRepository.Get(documentId);
            var vm = new DocumentViewModel
            { Ordre = doc.Ordre.GetValueOrDefault(),
                BasePath = baseURL +  "File/DownloadFile?ID=" + doc.DocumentId,
                FileName= doc.FileName,
                DisplaySection = doc.Section.Nom,
                DocumentId = doc.DocumentId,
                SectionId= doc.SectionId??0,
                Titre= doc.Titre
             

            };
            return vm;
        }

        internal DataSourceResult GetReadListDocument(DataSourceRequest request, string Lequel)
        {
            var baseURL = Helpers.URLHelper.GetBaseUrl();
            // on n'affiche pas les éléments avec section null... ils surviennent à la suite d'un ajout partiel d'un document (on a sauvé le fichier MAIS pas vraiment fait de mise à jour)
            var result = documentRepository.AsQueryable().Where( x=>x.Section.ApplicationPrevi.Nom== Lequel && x.SectionId != null).OrderBy(x=>x.Section.Nom).ThenBy(x=>x.Ordre).Select(x => new DocumentViewModel()
            {
                DocumentId = x.DocumentId,
                Ordre = x.Ordre??0,
                SectionId = x.SectionId??0,
                Titre = x.Titre,
                FileName = x.FileName,
                DisplaySection= x.Section.Nom,
                BasePath = baseURL +  "File/DownloadFile?ID=" + x.DocumentId



            }).ToDataSourceResult(request);

            return result;
        }


        public  void SaveDocumentAR(DocumentViewModel model)
        {
            var item = documentRepository.Get(model.DocumentId);
            if (item == null)
                item = new Models.Document();

            item.FileName = model.FileName;
            item.Ordre = model.Ordre;
            if (model.SectionId == 0)
                item.SectionId = null;
            else
                item.SectionId = model.SectionId;
          
            item.Titre = model.Titre;
           

         

            if (item.DocumentId > 0)
                documentRepository.Update(item);
            else
                documentRepository.Add(item);

            documentRepository.SaveChanges();

            model.DocumentId = item.DocumentId;
        }

        public void SaveDocumentGEN(DocumentViewModel model)
        {
            var item = documentRepository.Get(model.DocumentId);
            if (item == null)
                item = new Models.Document();

            item.FileName = model.FileName;
            item.Ordre = model.Ordre;
            item.SectionId = model.SectionId;
            item.Titre = model.Titre;
       



            if (item.DocumentId > 0)
                documentRepository.Update(item);
            else
                documentRepository.Add(item);

            documentRepository.SaveChanges();

            model.DocumentId = item.DocumentId;
        }


        public bool Supprimer(DocumentViewModel model)
        {
            var document = documentRepository.Get(model.DocumentId);
            if (document == null)
                return true;

         
            documentRepository.Delete(document.DocumentId);
            documentRepository.SaveChanges();

            return true;
        }
        //internal DataSourceResult GetReadListCadenas(DataSourceRequest request)
        //{
        //    var result = documentRepository.AsQueryable().Where(x=> x.Section.Nom== "Cadenassage").OrderBy(x => x.Ordre).Select(x => new DocumentViewModel()
        //    {
        //        DocumentId = x.DocumentId,
        //        Ordre = x.Ordre??0,
        //        SectionId = x.Section.SectionId,
        //        Titre = x.Titre


        //    }).ToDataSourceResult(request);

        //    return result;
        //}

        public void SaveDocumentClientCadenassage(DocumentClientViewModel model)
        {
            var item = documentClientRepository.Get(model.DocumentClientId);
            if (item == null)
                item = new Models.DocumentClient();

            item.FileName = model.FileName;
            item.ClientId = model.ClientId;
            item.ApplicationPreviId = model.ApplicationPreviId;

            item.Titre = model.Titre;




            if (item.DocumentClientId > 0)
                documentClientRepository.Update(item);
            else
                documentClientRepository.Add(item);

            documentClientRepository.SaveChanges();

            model.DocumentClientId = item.DocumentClientId;
        }
    }
}