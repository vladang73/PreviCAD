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
    public class MaterielRequisCadenassageService
    {
       private MaterielRequisCadenassageRepository materielRequisCadenassageRepository;

        public MaterielRequisCadenassageService(MaterielRequisCadenassageRepository materielRequisCadenassageRepository)
        {
            this.materielRequisCadenassageRepository = materielRequisCadenassageRepository;
        }


        internal DataSourceResult GetListeLignesMateriel(DataSourceRequest request, int FicheCadenassageId)
        {
            var langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            var result = materielRequisCadenassageRepository.AsQueryable().Where(x => x.FicheCadenassageId == FicheCadenassageId).
                Select(x => new MaterielRequisCadenassageViewModel()
                {
                    FicheCadenassageId= x.FicheCadenassageId,
                    MaterielId = x.MaterielId,
                    MaterielRequisCadenassageId= x.MaterielRequisCadenassageId,
                    Suppressible = true,
                    NomMateriel = langue=="fr"? x.Materiel.Description: x.Materiel.DescriptionEN,
                    Quantite= x.Quantite



                }).OrderBy(x=>x.NomMateriel).ToDataSourceResult(request);

            return result;
        }


        public void SaveLigneCadenassageMateriel(MaterielRequisCadenassageViewModel model)
        {
            var item = materielRequisCadenassageRepository.Get(model.MaterielRequisCadenassageId);
            if (item == null)
                item = new Models.MaterielRequisCadenassage();

            item.MaterielRequisCadenassageId = model.MaterielRequisCadenassageId;
            item.FicheCadenassageId = model.FicheCadenassageId;
            item.MaterielId = model.MaterielId;
            item.Quantite = model.Quantite;
                
            

            if (item.MaterielRequisCadenassageId > 0)
                materielRequisCadenassageRepository.Update(item);
            else
                materielRequisCadenassageRepository.Add(item);

            materielRequisCadenassageRepository.SaveChanges();
            item = materielRequisCadenassageRepository.Get(item.MaterielRequisCadenassageId);
            model.MaterielRequisCadenassageId = item.MaterielRequisCadenassageId;

            model.NomMateriel = item.Materiel.Description;



        }

        public bool Supprimer(MaterielRequisCadenassageViewModel model)
        {
            var item = materielRequisCadenassageRepository.Get(model.MaterielRequisCadenassageId);
            if (item == null)
                return true;


            materielRequisCadenassageRepository.Delete(model.MaterielRequisCadenassageId);
            materielRequisCadenassageRepository.SaveChanges();

            return true;
        }

    }
}