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
    public class EmployeRegistreService
    {
        private EmployeRegistreRepository employeRegistreRepository;
        private LigneRegistreRepository ligneRegistreRepository;
        private ClientRepository clientRepository;
        public EmployeRegistreService(EmployeRegistreRepository employeRegistreRepository,
            LigneRegistreRepository ligneRegistreRepository,
            ClientRepository clientRepository)
        {
            this.employeRegistreRepository = employeRegistreRepository;
            this.ligneRegistreRepository = ligneRegistreRepository;
            this.clientRepository = clientRepository;

        }

        public IEnumerable<EmployeRegistreViewModel> GetAllAsEmployeRegistre(int ClientId)
        {
            var DDL = employeRegistreRepository.AsQueryable().Where(s => s.ClientId == ClientId).OrderBy(s => s.NoEmploye).Select(s => new EmployeRegistreViewModel()
            {
                DepartementId = s.DepartementId,
                NoEmploye = s.NoEmploye,
                EmployeRegistreId = s.EmployeRegistreId,
                NoCadenas = s.NoCadenas,
                Nom = s.Nom,
                NomDepartement = s.Departement.NomDepartement,
                Password = s.Password,
                ClientId = s.ClientId,
                Actif = s.Actif,
                Suppressible = ligneRegistreRepository.AsQueryable().Where(x => x.EmployeRegistreId == s.EmployeRegistreId).Count() != 0
            });

            return DDL;
        }

        public bool estUnique(EmployeRegistreViewModel model, int IdClient)
        {
            return employeRegistreRepository.AsQueryable().Where(x => x.NoEmploye == model.NoEmploye.Trim() && x.EmployeRegistreId != model.EmployeRegistreId && x.ClientId == IdClient).Count() == 0;

        }

        public bool estMaxActif(EmployeRegistreViewModel model, int IdClient)
        {
            if (model.Actif)
            {
                var nbDejaActifs = employeRegistreRepository.AsQueryable().Count(x => x.ClientId == IdClient && x.EmployeRegistreId != model.EmployeRegistreId && x.Actif == true);
                var clientMaxNbActifs = clientRepository.AsQueryable().Where(x => x.ClientId == IdClient).FirstOrDefault().NbUtilisateursPrevicad;
                if (nbDejaActifs + 1 > clientMaxNbActifs)
                    return true;
            }
            return false;
        }
        public void SaveEmployeRegistre(EmployeRegistreViewModel model)
        {
            var item = employeRegistreRepository.Get(model.EmployeRegistreId);
            if (item == null)
                item = new Models.EmployeRegistre();

            item.ClientId = model.ClientId;
            item.DepartementId = model.DepartementId;
            item.NoCadenas = model.NoCadenas;
            item.NoEmploye = model.NoEmploye;
            item.Nom = model.Nom;
            item.Password = model.Password;
            item.Actif = model.Actif;

            if (item.EmployeRegistreId > 0)
                employeRegistreRepository.Update(item);
            else
                employeRegistreRepository.Add(item);

            employeRegistreRepository.SaveChanges();
            item = employeRegistreRepository.Get(item.EmployeRegistreId);
            model.EmployeRegistreId = item.EmployeRegistreId;
            model.NomDepartement = item.Departement.NomDepartement;

        }

        public bool Supprimer(EmployeRegistreViewModel model)
        {
            var employe = employeRegistreRepository.Get(model.EmployeRegistreId);
            if (employe == null)
                return true;

            if (employe.LignesRegistre.Count == 0)
            {
                employeRegistreRepository.Delete(employe.EmployeRegistreId);
                employeRegistreRepository.SaveChanges();
            }

            return true;
        }

        internal DataSourceResult GetListeEmployes(DataSourceRequest request, int ClientId)
        {
            var langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            var result = employeRegistreRepository.AsQueryable().OrderBy(x => x.NoEmploye).Where(x => x.ClientId == ClientId).Select(s => new EmployeRegistreViewModel()
            {
                DepartementId = s.DepartementId,
                NoEmploye = s.NoEmploye,
                EmployeRegistreId = s.EmployeRegistreId,
                NoCadenas = s.NoCadenas,
                Nom = s.Nom,
                NomDepartement = langue == "fr" ? s.Departement.NomDepartement : s.Departement.NomDepartementEN,
                Password = s.Password,
                ClientId = s.ClientId,
                Actif = s.Actif,
                Suppressible = (s.LignesRegistre.Count == 0)

            }).ToDataSourceResult(request);

            return result;
        }

        public void setSessionLoginCadenassage()
        {

        }

        public Boolean isLoginOK(LoginCadenassageViewModel loginfo)
        {

            return (employeRegistreRepository.AsQueryable().Where(x => x.Client.IdentificateurUnique == loginfo.Identificateur && x.NoEmploye == loginfo.NoEmploye && x.Password == loginfo.Password).Count() > 0);



        }

        public void setSession(LoginCadenassageViewModel loginfo)
        {
            var employe = employeRegistreRepository.AsQueryable().Where(x => x.Client.IdentificateurUnique == loginfo.Identificateur && x.NoEmploye == loginfo.NoEmploye && x.Password == loginfo.Password).FirstOrDefault();
            if (employe != null)
            {
                var vm = new EmployeRegistreViewModel() { NoEmploye = employe.NoEmploye, Nom = employe.Nom, EmployeRegistreId = employe.EmployeRegistreId, ClientId = employe.ClientId };
                HttpContext.Current.Session["EmployeCadenassage"] = vm;
            }

        }

        public EmployeRegistreViewModel getEmployeRegistre()
        {
            if (HttpContext.Current.Session["EmployeCadenassage"] != null)
                return (EmployeRegistreViewModel)HttpContext.Current.Session["EmployeCadenassage"];


            return new EmployeRegistreViewModel();
        }

        public void LogOff()
        {
            HttpContext.Current.Session["EmployeCadenassage"] = null;

        }


        public EmployeRegistreViewModel getEmployeRegistreVM(int employeRegistreId)
        {
            var result = employeRegistreRepository.AsQueryable().Where(x => x.EmployeRegistreId == employeRegistreId).Select(s => new EmployeRegistreViewModel()
            {
                DepartementId = s.DepartementId,
                NoEmploye = s.NoEmploye,
                EmployeRegistreId = s.EmployeRegistreId,
                NoCadenas = s.NoCadenas,
                Nom = s.Nom,
                NomDepartement = s.Departement.NomDepartement,
                Password = s.Password,
                ClientId = s.ClientId,
                Suppressible = s.LignesRegistre.Count() == 0,
                Actif = s.Actif

            }).FirstOrDefault();
            return result;
        }


        public bool ChangePassword(ChangeEmployeePasswordViewModel model)
        {
            var emp = employeRegistreRepository.Get(model.EmpID);

            if (emp != null)
            {
                emp.Password = model.Password;

                employeRegistreRepository.Update(emp);
                employeRegistreRepository.SaveChanges();

                return true;
            }

            return false;
        }
    }
}