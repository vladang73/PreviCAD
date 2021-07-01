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
    public class DocumentFicheNoteService
    {
        private DocumentFicheNoteRepository documentFicheNoteRepository;
        public DocumentFicheNoteService(DocumentFicheNoteRepository documentFicheNoteRepository)
        {
            this.documentFicheNoteRepository = documentFicheNoteRepository;
        }

        internal Models.DocumentFicheNote GetNotes(int FicheId)
        {
            return documentFicheNoteRepository.Get(FicheId);
        }


        public void SaveNotes(Models.DocumentFicheNote model)
        {
            var item = documentFicheNoteRepository.Get(model.FicheCadenassageId);
            if (item == null)
            {
                documentFicheNoteRepository.Add(new Models.DocumentFicheNote
                {
                    FicheCadenassageId = model.FicheCadenassageId,
                    Notes = model.Notes
                });
            }
            else
            {
                item.Notes = model.Notes;

                documentFicheNoteRepository.Update(item);
            }

            documentFicheNoteRepository.SaveChanges();
        }

        public bool Supprimer(int ficheCadenassageId)
        {
            documentFicheNoteRepository.Delete(ficheCadenassageId);
            documentFicheNoteRepository.SaveChanges();

            return true;
        }

        public bool isChanged(int ficheCadenassageId, string notes)
        {
            var model = GetNotes(ficheCadenassageId);

            if (model == null) return true;
            else
                return model.Notes != notes;
        }
    }
}