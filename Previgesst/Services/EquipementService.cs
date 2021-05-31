using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Previgesst.Repositories;
using Previgesst.ViewModels;
using Previgesst.ViewModels.DDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Previgesst.Services
{
    public class EquipementService
    {



        EquipementRepository equipementRepository;

        public EquipementService(EquipementRepository equipementRepository)
        {
            this.equipementRepository = equipementRepository;
        }

        public IEnumerable<EquipementDDLViewModel> GetAllAsEquipementDDLViewModel(int ClientId)
        {
            var langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            if (langue == "fr")
            {
                var DDL = equipementRepository.AsQueryable().Where(s => s.ClientId == ClientId).OrderBy(s => s.NomEquipement).Select(s => new EquipementDDLViewModel()
                {

                    NomEquipement = s.NomEquipement,
                    EquipementId = s.EquipementId


                });

                return DDL;
            }
            else
            {
                var DDL = equipementRepository.AsQueryable().Where(s => s.ClientId == ClientId).OrderBy(s => s.NomEquipementEN).Select(s => new EquipementDDLViewModel()
                {

                    NomEquipement = s.NomEquipementEN,
                    EquipementId = s.EquipementId


                });
                return DDL;
            }

        }




        internal DataSourceResult GetReadListeEquipements(DataSourceRequest request, int ClientId)
        {
            var baseURL = Helpers.URLHelper.GetBaseUrl();
            if (baseURL.Right(1) != "/" && baseURL.Right(1) != @"\")
                baseURL += "/";
            var time = DateTime.Now.ToLongTimeString();
            var result = equipementRepository.AsQueryable().OrderBy(x => x.NomEquipement).Where(x => x.ClientId == ClientId).OrderBy(x => x.NomEquipement).Select(x => new EquipementViewModel()
            {
                ClientId = x.ClientId,
                EquipementId = x.EquipementId,
                NomEquipement = x.NomEquipement,
                NomEquipementEN = x.NomEquipementEN,
                NomFichier = x.NomFichier,
                NomClient = x.Client.Nom,
                Suppressible = x.FicheCadenassage.Count == 0,
                Thumbnail = baseURL + "Images/Cadenassage/Equipements/" + x.EquipementId.ToString() + "/thumb.jpg?time=" + time



            }).ToDataSourceResult(request);

            return result;
        }


        public void SaveEquipement(EquipementViewModel model)
        {
            var item = equipementRepository.Get(model.EquipementId);
            if (item == null)
                item = new Models.Equipement();

            item.EquipementId = model.EquipementId;
            item.ClientId = model.ClientId;
            item.NomFichier = model.NomFichier;
            item.NomEquipement = model.NomEquipement;
            item.NomEquipementEN = model.NomEquipementEN;




            if (item.EquipementId > 0)
                equipementRepository.Update(item);
            else
                equipementRepository.Add(item);

            equipementRepository.SaveChanges();
            item = equipementRepository.Get(item.EquipementId);
            model.EquipementId = item.EquipementId;
            model.NomClient = item.Client.Nom;

        }

        public bool Supprimer(EquipementViewModel model)
        {
            var equipement = equipementRepository.Get(model.EquipementId);
            if (equipement == null)
                return true;


            equipementRepository.Delete(equipement.EquipementId);
            equipementRepository.SaveChanges();

            return true;
        }
    }
}