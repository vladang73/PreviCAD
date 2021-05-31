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
    public class DocumentClientService
    {
        private DocumentClientRepository documentClientRepository;
        public DocumentClientService(DocumentClientRepository documentClientRepository)
        {
            this.documentClientRepository = documentClientRepository;

        }
        public DataSourceResult GetReadListDocumentClient(DataSourceRequest request, string Lequel, int ClientId)
        {
            var baseURL = Helpers.URLHelper.GetBaseUrl();
            // on n'affiche pas les éléments avec section null... ils surviennent à la suite d'un ajout partiel d'un document (on a sauvé le fichier MAIS pas vraiment fait de mise à jour)
            var result = documentClientRepository.AsQueryable().Where(x => x.ApplicationPrevi.Nom == Lequel && x.Titre != null && x.ClientId == ClientId).OrderBy(x => x.Titre).Select(x => new DocumentClientViewModel()
            {
                ApplicationPreviId = x.ApplicationPreviId,
                Bidon="",
                ClientId= x.ClientId,
                DocumentClientId= x.DocumentClientId,
                FileName= x.FileName,
                Titre= x.Titre,
                BasePath = baseURL + "File/DownloadFileClient?ID=" + x.DocumentClientId

            }).ToDataSourceResult(request);

            return result;
        }
        internal DocumentClientViewModel getVM( int documentClientId)
        {
            var baseURL = Helpers.URLHelper.GetBaseUrl();
            var mydoc = documentClientRepository.Get(documentClientId);
            var vm = new DocumentClientViewModel
            {
                ApplicationPreviId = mydoc.ApplicationPreviId,
                BasePath = baseURL + "File/DownloadFileClient?ID=" + mydoc.DocumentClientId,
                ClientId = mydoc.ClientId,
                DocumentClientId = mydoc.DocumentClientId,
                FileName = mydoc.FileName,
                Titre = mydoc.Titre
            };
            return vm;
        }


        public void SaveDocumentClient(DocumentClientViewModel model)
        {
            var item = documentClientRepository.Get(model.DocumentClientId);
            if (item == null)
                item = new Models.DocumentClient();

            item.FileName = model.FileName;
            item.ClientId = model.ClientId;
            item.Titre = model.Titre;
            item.ApplicationPreviId = model.ApplicationPreviId;



            if (item.DocumentClientId > 0)
                documentClientRepository.Update(item);
            else
                documentClientRepository.Add(item);

            documentClientRepository.SaveChanges();

            model.DocumentClientId = item.DocumentClientId;
        }

        public bool Supprimer(DocumentClientViewModel model)
        {
            var document = documentClientRepository.Get(model.DocumentClientId);
            if (document == null)
                return true;


            documentClientRepository.Delete(document.DocumentClientId);
            documentClientRepository.SaveChanges();

            return true;
        }
    }
}