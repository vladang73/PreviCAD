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
    public class EquipementArticuloeService
    {
        private EquipementArticuloRepository equipementArticuloRepository;

        private SourceEnergieRepository sourceEnergieRepository;
        private DispositifRepository dispositifRepository;
        private MaterielRepository materielRepository;


        public EquipementArticuloeService(EquipementRepository equipementRepository, SourceEnergieRepository sourceEnergieRepository, DispositifRepository dispositifRepository, MaterielRepository materielRepository, EquipementArticuloRepository equipementArticuloRepository)
        {
            this.sourceEnergieRepository = sourceEnergieRepository;
            this.dispositifRepository = dispositifRepository;
            this.materielRepository = materielRepository;
            this.equipementArticuloRepository = equipementArticuloRepository;
        }


        internal DataSourceResult GetReadListeEquipementArticuloes(DataSourceRequest request, int eqID)
        {
            var baseURL = Helpers.URLHelper.GetBaseUrl();
            if (baseURL.Right(1) != "/" && baseURL.Right(1) != @"\")
                baseURL += "/";

            var time = DateTime.Now.ToLongTimeString();
            var result = (
                from eq in equipementArticuloRepository.AsQueryable()
                join eng in sourceEnergieRepository.AsQueryable() on eq.Energy equals eng.SourceEnergieId
                join dis in dispositifRepository.AsQueryable() on eq.Deposit equals dis.DispositifId
                join mat in materielRepository.AsQueryable() on eq.Accessories equals mat.MaterielId

                where eq.EquipementId == eqID
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
                .OrderBy(x => x.EquipementArticuloID)
                .ToDataSourceResult(request);

            return result;
        }

        internal IEnumerable<EquipementArticuloViewModel> GetEquipementArticuloes(int equipementId)
        {
            //var result = equipementRepository.Get(equipementId).EquipementArticulo
            var result = (
                from eq in equipementArticuloRepository.AsQueryable()
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




        public void SaveEquipementArticula(EquipementArticuloViewModel model)
        {
            var item = equipementArticuloRepository.Get(model.EquipementArticuloID);
            if (item == null)
                item = new Models.EquipementArticulo();

            item.EquipementId = model.EquipementId;

            item.Energy = model.Energy;
            item.Deposit = model.Deposit;
            item.Accessories = model.Accessories;
            item.Nomenclature = model.Nomenclature;


            if (item.EquipementArticuloID == 0)
            {
                equipementArticuloRepository.Add(item);
            }
            else
            {
                equipementArticuloRepository.Update(item);
            }

            equipementArticuloRepository.SaveChanges();
        }

        public bool DeleteEquipementArticula(int equipementArticuloID)
        {
            var item = equipementArticuloRepository.Get(equipementArticuloID);
            if (item == null) return false;

            equipementArticuloRepository.Delete(equipementArticuloID);
            equipementArticuloRepository.SaveChanges();

            return true;
        }


        public bool Supprimer(EquipementArticuloViewModel model)
        {
            var equipement = equipementArticuloRepository.Get(model.EquipementArticuloID);
            if (equipement == null)
                return true;


            equipementArticuloRepository.Delete(equipement.EquipementArticuloID);
            equipementArticuloRepository.SaveChanges();

            return true;
        }
    }
}