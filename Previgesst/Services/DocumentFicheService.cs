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
    public class DocumentFicheService
    {
        private DocumentFicheRepository documentFicheRepository;
        public DocumentFicheService(DocumentFicheRepository documentFicheRepository)
        {
            this.documentFicheRepository = documentFicheRepository;
        }

        public DataSourceResult GetReadListDocumentFiche(DataSourceRequest request, string Lequel, int FicheId)
        {
            var baseURL = Helpers.URLHelper.GetBaseUrl();
            // on n'affiche pas les éléments avec section null... ils surviennent à la suite d'un ajout partiel d'un document (on a sauvé le fichier MAIS pas vraiment fait de mise à jour)
            var result = documentFicheRepository.AsQueryable()
                        .Where(x => x.ApplicationPrevi.Nom == Lequel && x.Titre != null && x.FicheCadenassageId == FicheId)
                        .OrderBy(x => x.Titre)
                        .Select(x => new DocumentFicheViewModel()
                        {
                            ApplicationPreviId = x.ApplicationPreviId,
                            Bidon = "",
                            FicheCadenassageId = x.FicheCadenassageId,
                            DocumentFicheId = x.DocumentFicheId,
                            FileName = x.FileName,
                            Titre = x.Titre,
                            BasePath = baseURL + "File/DownloadFileFiche?ID=" + x.DocumentFicheId
                        })
                        .ToDataSourceResult(request);

            return result;
        }

        internal DocumentFicheViewModel getVM(int documentFicheId)
        {
            var baseURL = Helpers.URLHelper.GetBaseUrl();
            var mydoc = documentFicheRepository.Get(documentFicheId);
            var vm = new DocumentFicheViewModel
            {
                ApplicationPreviId = mydoc.ApplicationPreviId,
                BasePath = baseURL + "File/DownloadFileFiche?ID=" + mydoc.DocumentFicheId,
                FicheCadenassageId = mydoc.FicheCadenassageId,
                DocumentFicheId = mydoc.DocumentFicheId,
                FileName = mydoc.FileName,
                Titre = mydoc.Titre
            };
            return vm;
        }


        public void SaveDocumentFiche(DocumentFicheViewModel model)
        {
            var item = documentFicheRepository.Get(model.DocumentFicheId);
            if (item == null)
                item = new Models.DocumentFiche();

            item.FileName = model.FileName;
            item.FicheCadenassageId = model.FicheCadenassageId;
            item.Titre = model.Titre;
            item.ApplicationPreviId = model.ApplicationPreviId;



            if (item.DocumentFicheId > 0)
                documentFicheRepository.Update(item);
            else
                documentFicheRepository.Add(item);

            documentFicheRepository.SaveChanges();

            model.DocumentFicheId = item.DocumentFicheId;
        }

        public bool Supprimer(DocumentFicheViewModel model)
        {
            var document = documentFicheRepository.Get(model.DocumentFicheId);
            if (document == null)
                return true;


            documentFicheRepository.Delete(document.DocumentFicheId);
            documentFicheRepository.SaveChanges();

            return true;
        }
    }
}