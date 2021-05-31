using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Previgesst.Repositories;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Previgesst.Services
{
    public class PhotoFicheCadenassageService
    {
        private PhotoFicheCadenassageRepository photoFicheCadenassageRepository;
        public PhotoFicheCadenassageService(PhotoFicheCadenassageRepository photoFicheCadenassageRepository)
        {
            this.photoFicheCadenassageRepository = photoFicheCadenassageRepository;
        }


        internal DataSourceResult GetListeLignesPhotos(DataSourceRequest request, int FicheCadenassageId)
        {
            var time = DateTime.Now.ToLongTimeString();
            var baseURL = Helpers.URLHelper.GetBaseUrl();
            if (baseURL.Right(1) != "/" && baseURL.Right(1) != @"\")
                baseURL += "/";
            var langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            var result = photoFicheCadenassageRepository.AsQueryable().Where(x => x.FicheCadenassageId == FicheCadenassageId).OrderBy(x => x.Rang).
                Select(x => new PhotoFicheCadenassageViewModel
                {
                    Rang = x.Rang,
                    PhotoFicheCadenassageId = x.PhotoFicheCadenassageId,
                    FicheCadenassageId = x.FicheCadenassageId,
                    Suppressible = x.LignesInstruction.Count == 0 && x.LignesDecadenassage.Count == 0,
                    Localisation = x.Localisation,
                    LocalisationEN = x.LocalisationEN,
                    Fichier = x.Fichier,
                    URL = "<img src='" + baseURL + "Images/Cadenassage/Photos/" + x.PhotoFicheCadenassageId.ToString() + "/thumb.jpg?time=" + time + "'/>",

                }).ToDataSourceResult(request);

            return result;
        }

        public PhotoFicheCadenassageViewModel getVM(int PhotoFicheCadenassageId)
        {
            var langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            var baseURL = Helpers.URLHelper.GetBaseUrl();
            if (baseURL.Right(1) != "/" && baseURL.Right(1) != @"\")
                baseURL += "/";
            var time = DateTime.Now.ToLongTimeString();
            var item = photoFicheCadenassageRepository.AsQueryable().
                 Where(x => x.PhotoFicheCadenassageId == PhotoFicheCadenassageId).Select(x => new PhotoFicheCadenassageViewModel()
                 {
                     FicheCadenassageId = x.FicheCadenassageId,
                     PhotoFicheCadenassageId = x.PhotoFicheCadenassageId,
                     Rang = x.Rang,
                     Localisation =  x.Localisation,
                     LocalisationEN = x.LocalisationEN,
                     Fichier = x.Fichier,
                     Suppressible = x.LignesInstruction.Count == 0 && x.LignesDecadenassage.Count == 0,
                     URL = "<img src='" + baseURL + "Images/Cadenassage/Photos/" + x.PhotoFicheCadenassageId.ToString() + "/thumb.jpg?time=" + time + "'/>",

                 }).FirstOrDefault();
            return item;


        }


        public void SaveLigneCadenassagePhoto(PhotoFicheCadenassageViewModel model, Boolean withFile = false)
        {// note: on ne sauve pas le fichier dans cette méthode, seulement par le saveLink
            var item = photoFicheCadenassageRepository.Get(model.PhotoFicheCadenassageId);
            if (item == null)
                item = new Models.PhotoFicheCadenassage();

            item.PhotoFicheCadenassageId = model.PhotoFicheCadenassageId;
            item.FicheCadenassageId = model.FicheCadenassageId;
            item.Rang = model.Rang;
            item.Localisation = model.Localisation;
            item.LocalisationEN = model.LocalisationEN;
            if (withFile)
                item.Fichier = model.Fichier;
         


            if (item.PhotoFicheCadenassageId > 0)
                photoFicheCadenassageRepository.Update(item);
            else
                photoFicheCadenassageRepository.Add(item);

            photoFicheCadenassageRepository.SaveChanges();
            item = photoFicheCadenassageRepository.Get(item.PhotoFicheCadenassageId);
            model.PhotoFicheCadenassageId = item.PhotoFicheCadenassageId;




        }

    
        public bool Supprimer(PhotoFicheCadenassageViewModel model)
        {
            var item = photoFicheCadenassageRepository.Get(model.PhotoFicheCadenassageId);
            if (item == null)
                return true;


            photoFicheCadenassageRepository.Delete(model.PhotoFicheCadenassageId);
            photoFicheCadenassageRepository.SaveChanges();

            return true;
        }

        public IEnumerable<PhotoFicheCadenassageDDLViewModel> GetAllAsPhotoDDLViewModel(int FicheCadenassageId)
        {
            var DDL = photoFicheCadenassageRepository.AsQueryable().Where(s => s.FicheCadenassageId == FicheCadenassageId).OrderBy(s => s.Rang).Select(s => new PhotoFicheCadenassageDDLViewModel()
            {

                PhotoFicheCadenassageId = s.PhotoFicheCadenassageId,
                Rang = s.Rang


            });

            return DDL;
        }


    }
}