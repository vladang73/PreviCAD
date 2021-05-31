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
        private EquipementRepository equipementRepository;
        //private EquipementArticuloRepository equipementArticuloRepository;

        private SourceEnergieRepository sourceEnergieRepository;
        private DispositifRepository dispositifRepository;
        private MaterielRepository materielRepository;


        public EquipementService(EquipementRepository equipementRepository, SourceEnergieRepository sourceEnergieRepository, DispositifRepository dispositifRepository, MaterielRepository materielRepository)
        {
            this.equipementRepository = equipementRepository;
            this.sourceEnergieRepository = sourceEnergieRepository;
            this.dispositifRepository = dispositifRepository;
            this.materielRepository = materielRepository;
            //this.equipementArticuloRepository = equipementArticuloRepository;
        }

        public IEnumerable<EquipementDDLViewModel> GetAllAsEquipementDDLViewModel(int ClientId)
        {
            var langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            if (langue == "fr")
            {
                var DDL = equipementRepository.AsQueryable().Where(s => s.ClientId == ClientId).OrderBy(s => s.NomEquipement).Select(s => new EquipementDDLViewModel()
                {

                    NomEquipement = s.NomEquipement,
                    EquipementId = s.EquipementId,
                    NumeroEquipment = s.NumeroEquipement

                });

                return DDL;
            }
            else
            {
                var DDL = equipementRepository.AsQueryable().Where(s => s.ClientId == ClientId).OrderBy(s => s.NomEquipementEN).Select(s => new EquipementDDLViewModel()
                {

                    NomEquipement = s.NomEquipementEN,
                    EquipementId = s.EquipementId,
                    NumeroEquipment = s.NumeroEquipement


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
                NumeroEquipment = x.NumeroEquipement,
                NomFichier = x.NomFichier,
                NomClient = x.Client.Nom,
                Suppressible = x.FicheCadenassage.Count == 0,
                Thumbnail = baseURL + "Images/Cadenassage/Equipements/" + x.EquipementId.ToString() + "/thumb.jpg?time=" + time,

                Task = x.Task,
                Model = x.Model,
                Manufacturer = x.Manufacturer,
                RiskAnalysis = x.RiskAnalysis ?? false,
                LockoutProcedure = x.LockoutProcedure ?? false,
                SafeWorkProcedure = x.SafeWorkProcedure ?? false,

                //Accessories = x.Accessories,
                //Deposit = x.Deposit,
                //Energy = x.Energy,
                //Nomenclature = x.Nomenclature,
                Function = x.Function,
                YearOfProduction = x.YearOfProduction,
                NumberOfSerie = x.NumberOfSerie

            }).ToDataSourceResult(request);

            return result;
        }

        internal IEnumerable<EquipementArticuloViewModel> GetEquipementArticuloes(int equipementId)
        {
            //var result = equipementRepository.Get(equipementId).EquipementArticulo
            var result = (
                from eq in equipementRepository.Get(equipementId).EquipementArticulo
                join eng in sourceEnergieRepository.AsQueryable() on eq.Energy equals eng.SourceEnergieId
                join dis in dispositifRepository.AsQueryable() on eq.Deposit equals dis.DispositifId
                join mat in materielRepository.AsQueryable() on eq.Accessories equals mat.MaterielId

                select new EquipementArticuloViewModel
                {
                    EquipementArticuloID = eq.EquipementArticuloID,
                    EquipementId = eq.EquipementId,
                    Accessories = eq.Accessories,
                    Deposit = eq.Deposit,
                    Energy = eq.Energy,

                    AccessoriesDescription = mat.Description,
                    DepositDescription = dis.Description,
                    EnergyDescription = eng.Description,
                    Nomenclature = eq.Nomenclature,

                })
                .ToList();

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
            item.NumeroEquipement = model.NumeroEquipment;



            if (item.EquipementId > 0)
                equipementRepository.Update(item);
            else
                equipementRepository.Add(item);

            equipementRepository.SaveChanges();
            item = equipementRepository.Get(item.EquipementId);
            model.EquipementId = item.EquipementId;
            model.NomClient = item.Client.Nom;
        }

        public void SaveEquipementNew(EquipementViewModel model, string mode)
        {
            var item = equipementRepository.Get(model.EquipementId);
            if (item == null)
                item = new Models.Equipement();

            item.EquipementId = model.EquipementId;

            if (mode == "Equipment")
            {
                item.Model = model.Model;
                item.Manufacturer = model.Manufacturer;
                item.Task = model.Task;
                item.RiskAnalysis = model.RiskAnalysis;
                item.LockoutProcedure = model.LockoutProcedure;
                item.SafeWorkProcedure = model.SafeWorkProcedure;
                item.NumberOfSerie = model.NumberOfSerie;
                item.YearOfProduction = model.YearOfProduction;
                item.Function = model.Function;
            }
            //else if (mode == "Energy")
            //{
            //    item.Energy = model.Energy;
            //    item.Deposit = model.Deposit;
            //    item.Accessories = model.Accessories;
            //    item.Nomenclature = model.Nomenclature;
            //}


            if (item.EquipementId > 0)
                equipementRepository.Update(item);
            else
                equipementRepository.Add(item);

            equipementRepository.SaveChanges();
            item = equipementRepository.Get(item.EquipementId);
            model.EquipementId = item.EquipementId;
            model.NomClient = item.Client.Nom;
        }

        //public void SaveEquipementArticula(EquipementArticuloViewModel model)
        //{
        //    var parent = equipementRepository.Get(model.EquipementId);
        //    if (parent == null) return;

        //    var item = parent.EquipementArticulo.FirstOrDefault(x => x.EquipementArticuloID == model.EquipementArticuloID);
        //    if (item == null)
        //        item = new Models.EquipementArticulo();

        //    item.EquipementId = model.EquipementId;


        //    item.Energy = model.Energy;
        //    item.Deposit = model.Deposit;
        //    item.Accessories = model.Accessories;
        //    item.Nomenclature = model.Nomenclature;


        //    if (item.EquipementArticuloID == 0)
        //    {
        //        parent.EquipementArticulo.Add(item);
        //        //equipementRepository.Update(parent);
        //    }

        //    equipementRepository.SaveChanges();
        //}

        //public bool DeleteEquipementArticula(int equipementArticuloID)
        //{
        //    var item = equipementArticuloRepository.Get(equipementArticuloID);
        //    if (item == null) return false;

        //    equipementArticuloRepository.Delete(equipementArticuloID);
        //    equipementRepository.SaveChanges();

        //    return true;
        //}


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