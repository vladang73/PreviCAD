using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Previgesst.Repositories;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Previgesst.Services
{
    public class ClientService
    {
        private ClientRepository clientRepository;
        private UtilisateurRepository utilisateurRepository;
        private ClientApplicationPreviRepository clientApplicationPreviRepository;
        private ApplicationPrevisService applicationPrevisService;
        private UtilisateurService utilisateurService;

        public ClientService(ClientRepository clientRepository, UtilisateurRepository utilisateurRepository, ClientApplicationPreviRepository clientApplicationPreviRepository,
          ApplicationPrevisService applicationPrevisService, UtilisateurService utilisateurService   )
        {
            this.clientRepository = clientRepository;
            this.utilisateurRepository = utilisateurRepository;
            this.clientApplicationPreviRepository = clientApplicationPreviRepository;
            this.applicationPrevisService = applicationPrevisService;
            this.utilisateurService = utilisateurService;

        }

        internal IEnumerable<ClientDDLViewModel> GetAnalyseRisqueCreateClientDDL()
        {
            var lst = clientRepository.GetAll()
                      .OrderBy(c => c.Nom).Where(c => c.Actif == true)
                      .Select(c => new ClientDDLViewModel()
                      {
                          ClientId = c.ClientId,
                          ClientNom = c.Nom
                      });

            return lst;
        }


        public void SaveClient(ClientListViewModel model)
        {
            var item = clientRepository.Get(model.ClientId);
            if (item == null)
            {
                item = new Models.Client();
                item.IdentificateurUnique = Guid.NewGuid();

            }


            item.Nom = model.Nom;
            item.Identificateur = model.Identificateur;
            item.Actif = model.Actif;
            item.Logo = model.Logo;

            item.Thumb = model.Thumb;
            if (model.Thumb == null)
                model.Thumb = "/images/cadenassage/blank.jpg";

            if (item.ClientId > 0)
                clientRepository.Update(item);
            else
                clientRepository.Add(item);

            clientRepository.SaveChanges();

            model.ClientId = item.ClientId;

            if (item.Logo == null)
            {
                //On crée le répertoire du client et on copie le blank
                var repertoire = @"~/Images/Cadenassage/Clients/" + item.ClientId + "/";
                if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(repertoire)))
                    System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath(repertoire));
                var sourceFileName = HttpContext.Current.Server.MapPath(@"~/Images/Cadenassage/Clients/blank.jpg");
                var destFileName = HttpContext.Current.Server.MapPath(@"~/Images/Cadenassage/Clients/" + item.ClientId + "/blank.jpg");
                System.IO.File.Copy(sourceFileName, destFileName, true);
                saveThumb(HttpContext.Current.Server.MapPath(@"~/Images/Cadenassage/Clients/" + item.ClientId + "/"), "blank.jpg");

                model.Logo = "blank.jpg";
                model.Thumb = "/Images/Cadenassage/Clients/" + item.ClientId.ToString() + "/thumb.jpg";
                item.Logo = model.Logo;
                item.Thumb = model.Thumb;

                clientRepository.Update(item);
                clientRepository.SaveChanges();

            }

        }


        void saveThumb(string repertoire, string filename)
        {
            Image image = Image.FromFile(System.IO.Path.Combine(repertoire, filename));
            Image thumb = image.GetThumbnailImage(120, 120, () => false, IntPtr.Zero);
            thumb.Save(System.IO.Path.Combine(repertoire, "thumb.jpg"));
            image.Dispose();
            thumb.Dispose();
        }

        internal DataSourceResult ReadListUtilisateurs([DataSourceRequest]DataSourceRequest request, int clientId)
        {


            var result = utilisateurRepository.AsQueryable().Where(x => x.ClientId == clientId).OrderBy(x => x.NomUtilisateur).ToList().Select(u => new UtilisateurIndexViewModel()
            {
                ClientId = u.ClientId,
                UtilisateurId = u.UtilisateurId,
                NomUtilisateur = u.NomUtilisateur,
                Nom = u.Nom,
                MotDePasse = Helpers.Encryption.Decrypt(u.Password, true),
                Courriel = u.Courriel,
               
                Actif = u.Actif,
                NotificationDebutCad = u.NotificationDebutCad,
                Suppressible = true,
                AdmAnalyseRisque= u.AdmAnalyseRisque,
                //AdmDocuments=u.AdmDocuments,
                AdmPrevicad= u.AdmPrevicad,
                Auditeur=u.Auditeur,
                ROAnalyseRisque = u.ROAnalyseRisque,
                RODocuments= u.RODocuments,
                ROPrevicad=u.ROPrevicad,
                AdmUtilisateurs = u.AdmUtilisateurs

            }).ToDataSourceResult(request);
            return result;

        }


        internal DataSourceResult GetReadListeClient(DataSourceRequest request)
        {
            var baseURL = Helpers.URLHelper.GetBaseUrl();
          
            var time = DateTime.Now.ToLongTimeString();
            var result = clientRepository.GetAll().Select(c => new ClientListViewModel()
            {
                Actif = c.Actif,
                ClientId = c.ClientId,
                Identificateur = c.Identificateur,
                Nom = c.Nom,
                EstSupprimable = (c.AnalysesRisques.Count == 0 && c.FichesCadenassage.Count == 0 && c.DocumentsClient.Count == 0 && c.Equipements.Count == 0 && c.Departements.Count == 0),
                Logo = c.Logo,
                //Thumb = c.Thumb + "?t=" + DateTime.Now.ToLongTimeString()
                Thumb = baseURL + "Images/Cadenassage/Clients/" + c.ClientId.ToString() + "/thumb.jpg?time=" + time,
               



            }).ToDataSourceResult(request);




            return result;
        }


        internal DataSourceResult GetReadListeClientActifs(DataSourceRequest request)
        {
            var baseURL = Helpers.URLHelper.GetBaseUrl();
            var time = DateTime.Now.ToLongTimeString();
            var result = clientRepository.AsQueryable().Where(x => x.Actif == true).ToList().Select(c => new ClientListViewModel()
            {
                Actif = c.Actif,
                ClientId = c.ClientId,
                Identificateur = c.Identificateur,
                Nom = c.Nom,
                EstSupprimable = (c.AnalysesRisques.Count == 0 && c.FichesCadenassage.Count == 0),
                Logo = c.Logo,
                //Thumb = c.Thumb + "?t=" + DateTime.Now.ToLongTimeString()
                Thumb = baseURL + "Images/Cadenassage/Clients/" + c.ClientId.ToString() + "/thumb.jpg?time=" + time,



            }).ToDataSourceResult(request);




            return result;

        }





        public string getLogoPath(int clientId)
        {
            var client = clientRepository.Get(clientId);
            if (client == null)

                return "<img src='/Images/Cadenassage/Clients/blank2.jpg'/>";
            else
            {
                var thumbExistant = System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/Images/Cadenassage/Clients/" + clientId.ToString() + "/thumb.jpg"));
                if (!thumbExistant)
                    return "<img src='~/Images/Cadenassage/Clients/blank2.jpg'/>";
                else
                    return "<img src='~/Images/Cadenassage/Clients/" + clientId.ToString() + "/thumb.jpg'/>";
            }

        }

        

        public bool Supprimer(ClientListViewModel model)
        {
            var client = clientRepository.Get(model.ClientId);

            if (client == null)
                return true;

            var listeApplications = client.ClientsApplicationPrevi.Select(x => x.ClientApplicationPreviId).ToList();
            var listeUtilisateurs = client.Utilisateurs.Select(x => x.UtilisateurId).ToList();

            foreach (var v in listeApplications)
                clientApplicationPreviRepository.Delete(v);
            foreach (var v in listeUtilisateurs)
                utilisateurRepository.Delete(v);

            clientRepository.Delete(client.ClientId);
            clientRepository.SaveChanges();

            return true;
        }

    

        internal ClientListViewModel getClientVM(int id)
        {
            var client = clientRepository.Get(id);

            try
            {
                string Path = HttpContext.Current.Server.MapPath(client.Thumb);
                if (!System.IO.File.Exists(HttpContext.Current.Server.MapPath(client.Thumb)))
                {
                    client.Thumb = "/images/cadenassage/blank.jpg";
                    clientRepository.Update(client);
                    clientRepository.SaveChanges();
                }
            }
            catch
            {
                client.Thumb = "/images/cadenassage/blank.jpg";
                clientRepository.Update(client);
                clientRepository.SaveChanges();
            }


            var vm = new ClientListViewModel
            {
                Actif = client.Actif,
                Nom = client.Nom,
                ClientId = client.ClientId,
                Identificateur = client.Identificateur,
                EstSupprimable = (client.AnalysesRisques.Count == 0 && client.FichesCadenassage.Count == 0),
                Logo = client.Logo,
                Thumb = client.Thumb,
                UniqueIdentifier= client.IdentificateurUnique




            };


            return vm;

        }

        public void SaveMaximums (ClientEditDetailsViewModel vm)
        {
            var item = clientRepository.Get(vm.ClientId);
            if (item !=null)
            {
                item.NbAdminsAnalyseRisqueMax = vm.NbAdminsAnalyseRisqueMax;
               // item.NbAdminsDocumentsMax = vm.NbAdminsDocumentsMax;
                item.NbAdminsPrevicadMax = vm.NbAdminsPrevicadMax;
                item.NbUtilisateursPrevicad = vm.NbUtilisateursPrevicad;
                item.NbAuditeursMax = vm.NbAuditeursMax;
                item.NbAdminUtilisateurs = vm.NbAdminUtilisateurs;
                item.StatusCadenassage = vm.StatusCadenassage;
                item.NbLimitCadenassage = vm.NbLimitCadenassage;
                if(vm.PeriodeEssai == true)
                {
                    item.PeriodeEssai = 1;
                }
                else
                {
                    item.PeriodeEssai = 0;
                }
                item.DateCadenassage = vm.DateCadenassage;

                clientRepository.Update(item);
                clientRepository.SaveChanges();
            }
        }
    }
}