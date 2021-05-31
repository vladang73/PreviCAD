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
    public class DepartementService
    {


        DepartementRepository departementRepository;

        public DepartementService(DepartementRepository departementRepository)
        {
            this.departementRepository = departementRepository;
        }

        public IEnumerable<DepartementDDLViewModel> GetAllAsDepartementDDLViewModel(int ClientId)
        {
            var langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            var DDL = departementRepository.AsQueryable().Where(s => s.ClientId == ClientId).Select(s => new DepartementDDLViewModel()
            {

                NomDepartement = langue=="fr"? s.NomDepartement: s.NomDepartementEN,
                DepartementId = s.DepartementId


            }).OrderBy(x=>x.NomDepartement);

            return DDL;
        }




        internal DataSourceResult GetReadListeDepartements(DataSourceRequest request, int ClientId)
        {
            var time = DateTime.Now.ToLongTimeString();
            var result = departementRepository.AsQueryable().OrderBy(x => x.NomDepartement).Where(x => x.ClientId == ClientId).Select(x => new DepartementViewModel()
            {
                ClientId = x.ClientId,
                DepartementId = x.DepartementId,
                NomDepartement = x.NomDepartement,
                NomDepartementEN = x.NomDepartementEN,
                NomClient = x.Client.Nom,
                Suppressible = x.FicheCadenassage.Count == 0 && x.EmployesRegistres.Count==0
              
            }).ToDataSourceResult(request);

            return result;
        }


        public void SaveDepartement(DepartementViewModel model)
        {
            var item = departementRepository.Get(model.DepartementId);
            if (item == null)
                item = new Models.Departement();

            item.DepartementId = model.DepartementId;
            item.ClientId = model.ClientId;
            item.NomDepartement = model.NomDepartement;
            item.NomDepartementEN = model.NomDepartementEN;

            if (item.DepartementId > 0)
                departementRepository.Update(item);
            else
                departementRepository.Add(item);

            departementRepository.SaveChanges();
            item = departementRepository.Get(item.DepartementId);
            model.DepartementId = item.DepartementId;
            model.NomClient = item.Client.Nom;

        }

        public bool Supprimer(DepartementViewModel model)
        {
            var equipement = departementRepository.Get(model.DepartementId);
            if (equipement == null)
                return true;


            departementRepository.Delete(equipement.DepartementId);
            departementRepository.SaveChanges();

            return true;
        }
    }
}
