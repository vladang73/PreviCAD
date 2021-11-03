using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using NLog;
using OmuModified.Drawing;
using Previgesst.ActionFilters;
using Previgesst.DataContexts;
using Previgesst.Repositories;
using Previgesst.Services;
using Previgesst.ViewModels;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Previgesst.Ressources;

namespace Previgesst.Controllers
{

    [AuthorizeRoles(Enums.Roles.Administrateur, Enums.Roles.LectureEcriture, Enums.Roles.System, Enums.Roles.LectureSeule)]
    public class ClientController : ControllerBase
    {
        private ClientService clientService;
        private ClientRepository clientRepository;
        private RoleUtilisateurService roleUtilisateurService;
        private UtilisateurService utilisateurService;
        private ApplicationPrevisService applicationPrevisService;
        private ClientApplicationPreviRepository clientApplicationPreviRepository;
        private UtilisateurRepository utilisateurRepository;

        private string Layout;
        public ClientController(ClientService clientService, ILogger logger, ClientRepository clientRepository, RoleUtilisateurService roleUtilisateurService,
            UtilisateurService utilisateurService, ApplicationPrevisService applicationPreviService, ClientApplicationPreviRepository clientApplicationPreviRepository, UtilisateurRepository utilisateurRepository) : base(logger)
        {
            this.clientService = clientService;
            this.clientRepository = clientRepository;
            this.roleUtilisateurService = roleUtilisateurService;
            this.utilisateurService = utilisateurService;
            this.applicationPrevisService = applicationPreviService;
            this.clientApplicationPreviRepository = clientApplicationPreviRepository;
            this.utilisateurRepository = utilisateurRepository;


            if (utilisateurService.GetSession() != null)
            {
                this.Layout = "~/Views/Shared/_LayoutClient_v2.cshtml";

            }
            else
                this.Layout = "~/Views/Shared/_Layout_v2.cshtml";
        }


        // GET: Client
        public ActionResult Index()
        {
            return View("Index", this.Layout);
        }




        public ActionResult Details(int id)
        {
            PopulateRolesUtilisateurs();
            PopulateApplication();
            var vm = getClientVM(id);
            return View("Details", Layout, vm);
        }

        public ClientEditDetailsViewModel getClientVM(int id)
        {
            var client = this.clientRepository.Get(id);
            var vm = new ClientEditDetailsViewModel { ClientId = id, Nom = client.Nom };
            vm.LienPrevicad = getLienPrevicad(id);
            vm.classDefault = "active";
            vm.classMaximums = "";
            vm.NbAdminsAnalyseRisqueMax = client.NbAdminsAnalyseRisqueMax;
            vm.StatusCadenassage = client.StatusCadenassage;
            vm.NbLimitCadenassage = client.NbLimitCadenassage;

            if (client.PeriodeEssai == 0)
            {
                vm.PeriodeEssai = false;
            }
            else
            {
                vm.PeriodeEssai = true;
            }

            vm.DateCadenassage = client.DateCadenassage;
            vm.TotalCadenassage = client.TotalCadenassage;

            //  vm.NbAdminsDocumentsMax = client.NbAdminsDocumentsMax;
            vm.NbAdminsPrevicadMax = client.NbAdminsPrevicadMax;
            vm.NbAuditeursMax = client.NbAuditeursMax;
            vm.NbUtilisateursPrevicad = client.NbUtilisateursPrevicad;
            vm.NbAdminUtilisateurs = client.NbAdminUtilisateurs;


            vm.ApplicationIds = clientApplicationPreviRepository.getClientApplications(id);
            return vm;

        }

        public string getLienPrevicad(int id)
        {
            var client = this.clientRepository.Get(id);


            var urlBuilder =
            new System.UriBuilder(Request.Url.AbsoluteUri)
            {
                Path = Url.Content("~/Registre/Index?ClientID=" + client.IdentificateurUnique.ToString()),
                Query = null,
            };

            Uri uri = urlBuilder.Uri;
            string url = urlBuilder.ToString().Replace("%3F", "?");


            return url;
        }

        public ActionResult Edit(int id)
        {
            PopulateRolesUtilisateurs();


            return View(clientService.getClientVM(id));
        }

        public ActionResult Utilisateurs(int id)
        {
            var vm = new UtilisateurIndexViewModel();
            var client = clientService.getClientVM(id);
            vm.ClientId = id;
            vm.NomCIE = client.Nom;
            PopulateRolesUtilisateurs();

            return View("Utilisateurs", vm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClientListViewModel item)
        {
            clientService.SaveClient(item);
            return Redirect("~/Client/Edit?ID=" + item.ClientId);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request,
           ClientListViewModel item)
        {
            if (item != null)
            {
                clientService.Supprimer(item);
            }
            return Json(new[] { item }.ToDataSourceResult(request, ModelState));

        }





        public ActionResult ReadListClients([DataSourceRequest] DataSourceRequest request)
        {
            return Json(clientService.GetReadListeClient(request));
        }

        public ActionResult ReadListClientsActifs([DataSourceRequest] DataSourceRequest request)
        {
            return Json(clientService.GetReadListeClientActifs(request));
        }

        public ActionResult ReadListUtilisateurs([DataSourceRequest] DataSourceRequest request, int clientId)
        {
            return Json(clientService.ReadListUtilisateurs(request, clientId));
        }

        private void PopulateRolesUtilisateurs()
        {
            var DDL = roleUtilisateurService.GetAllAsRolesUtilisateursDDLViewModel();


            ViewData["Roles"] = DDL;

        }
        public ActionResult SaveMaximums(ClientEditDetailsViewModel model)
        {
            if (ModelState.IsValid)
            {

                var client = clientRepository.Get(model.ClientId);

                if (model.NbAdminsAnalyseRisqueMax < client.Utilisateurs.Count(x => x.AdmAnalyseRisque == true))
                    ModelState.AddModelError("NbAdminsAnalyseRisqueMax", "Nombre inférieur au nombre d'administrateurs existants");
                //if (model.NbAdminsDocumentsMax < client.Utilisateurs.Count(x => x.AdmDocuments == true))
                //    ModelState.AddModelError("NbAdminsDocumentsMax", "Nombre inférieur au nombre d'administrateurs existants");
                if (model.NbAdminsPrevicadMax < client.Utilisateurs.Count(x => x.AdmPrevicad == true))
                    ModelState.AddModelError("NbAdminsPrevicadMax", "Nombre inférieur au nombre d'administrateurs existants");
                if (model.NbAuditeursMax < client.Utilisateurs.Count(x => x.Auditeur == true))
                    ModelState.AddModelError("NbAuditeursMax", "Nombre inférieur au nombre d'auditeurs existants");
                if (model.NbUtilisateursPrevicad < client.EmployesRegistre.Count(x => x.Actif == true))
                    ModelState.AddModelError("NbUtilisateursPrevicad", "Nombre inférieur au nombre d'utilisateurs Prévicad existants");
                if (model.NbAdminUtilisateurs < client.Utilisateurs.Count(x => x.AdmUtilisateurs == true))
                    ModelState.AddModelError("NbAdminUtilisateurs", "Nombre inférieur au nombre d'administrateurs des utilisateurs existants");


                PopulateRolesUtilisateurs();
                PopulateApplication();
                var vm = getClientVM(model.ClientId); // on va charger les applications, le lien prévicad...

                model.classMaximums = "active";
                model.classDefault = "";
                model.Nom = vm.Nom;
                model.LienPrevicad = vm.LienPrevicad;
                model.ApplicationIds = vm.ApplicationIds;


                if (!ModelState.IsValid)
                {
                    return View("Details", model);
                }
                else
                {
                    //on enregistre.
                    clientService.SaveMaximums(model);
                    SaveSuccessMessage("Maximums sauvegardés");
                    return Redirect("~/Client/Details?id=" + model.ClientId);
                }

            }
            return Json(model);
        }

        public ActionResult SaveUser([DataSourceRequest] DataSourceRequest request,
        UtilisateurIndexViewModel item, int z)
        {
            if (ModelState.IsValid)
            {
                if (item != null)
                    item.ClientId = z;
                if (utilisateurService.EstNomExistant(item))
                    ModelState.AddModelError("NomUtilisateur", ClientRES.DejaExistant);


                else if (utilisateurService.EstCourrielExistant(item))
                    ModelState.AddModelError("Courriel", ClientRES.CourrielExistant);
                else if (utilisateurService.EstCourrielExistant(item))
                {
                    ModelState.AddModelError("NotificationDebutCad", ClientRES.NotificationDebutCad);
                    ModelState.AddModelError("NotificationNonConformite", ClientRES.NotificationNonConformite);
                }
                else if (utilisateurService.DepassementAdminPrevicad(item))
                    ModelState.AddModelError("AdmPrevicad", ClientRES.DépassementMax);
                else if (utilisateurService.DepassementAdminAnalyse(item))
                    ModelState.AddModelError("AdmAnalyseRisque", ClientRES.DépassementMax);

                else if (utilisateurService.DepassementAuditeurs(item))
                    ModelState.AddModelError("Auditeur", ClientRES.DépassementMax);
                else if (utilisateurService.DepassementAdminUtilisateurs(item))
                    ModelState.AddModelError("AdmUtilisateurs", ClientRES.DépassementMax);
                //ModelState.AddModelError("AdmUtilisateurs", ClientRES.DépassementMax);
                else if (item.AdmAnalyseRisque && item.ROAnalyseRisque)
                    ModelState.AddModelError("AdmAnalyseRisque", ClientRES.PasAdminEtLecture);
                else if (item.AdmPrevicad && item.ROPrevicad)
                    ModelState.AddModelError("AdmPrevicad", ClientRES.PasAdminEtLecture);
                else if (item.AdmDocuments && item.RODocuments)
                    ModelState.AddModelError("AdmDocuments", ClientRES.PasAdminEtLecture);
                else if (item != null)
                {

                    item.ClientId = z;
                    utilisateurService.SaveUtilisateur(item);

                }
            }
            return Json(new[] { item }.ToDataSourceResult(request, ModelState));
        }


        public ActionResult DeleteUser([DataSourceRequest] DataSourceRequest request,
        UtilisateurIndexViewModel item)
        {
            if (item != null)
            {
                utilisateurService.Supprimer(item);
            }
            return Json(new[] { item }.ToDataSourceResult(request, ModelState));

        }




        private void PopulateApplication()
        {
            var DDL = applicationPrevisService.GetAllApplicationDDLViewModel();

            ViewData["Applications"] = DDL;

        }



        public ActionResult Save([DataSourceRequest] DataSourceRequest request,
       ClientListViewModel item)
        {
            var erreur = false;
            using (var context = AppDbContext.Create())
            {
                var getExistant = context.Client.Where(x => x.Identificateur == item.Identificateur && x.ClientId != item.ClientId).ToList();
                if (getExistant.Count > 0)
                {
                    ModelState.AddModelError("Identificateur", ClientRES.IdentificateurDeja);
                    erreur = true;

                }
            }

            if (item != null && ModelState.IsValid && erreur == false)
            {
                clientService.SaveClient(item);
            }

            return Json(new[] { item }.ToDataSourceResult(request, ModelState));

        }


        public JsonResult SaveLink(IEnumerable<HttpPostedFileBase> file, int ClientId, string Nom, string Identificateur, Boolean Actif)
        {

            Models.Client myClient;

            if (ClientId == 0)
            {
                var d = new ClientListViewModel()
                {

                    Nom = Nom,
                    Identificateur = Identificateur,
                    Actif = Actif

                };

                if (d.Nom == "")
                    d.Nom = ClientRES.ARemplir;
                if (d.Identificateur == "")
                    d.Identificateur = d.ClientId.ToString() + ClientRES.ARemplir;


                clientService.SaveClient(d);

                ClientId = d.ClientId;



            }

            myClient = clientRepository.Get(ClientId);

            foreach (var fichier in file)
            {
                if (fichier != null && fichier.ContentLength > 0)
                {

                    var repertoire = @"~/Images/Cadenassage/Clients/" + ClientId + "/";
                    if (!System.IO.Directory.Exists(Server.MapPath(repertoire)))
                        System.IO.Directory.CreateDirectory(Server.MapPath(repertoire));
                    else
                    {
                        foreach (var f in System.IO.Directory.GetFiles(Server.MapPath(repertoire)))
                        {
                            System.IO.File.Delete(f);
                        }



                    }

                    fichier.SaveAs(System.IO.Path.Combine(Server.MapPath(repertoire), fichier.FileName));


                    saveTOXLSize(Server.MapPath(repertoire), fichier.FileName);
                    saveThumb(Server.MapPath(repertoire), fichier.FileName);




                    myClient.Logo = fichier.FileName;
                    myClient.Nom = Nom;
                    myClient.Identificateur = Identificateur;
                    myClient.Actif = Actif;
                    myClient.Thumb = "/Images/Cadenassage/Clients/" + myClient.ClientId + "/thumb.jpg";

                    clientRepository.Update(myClient);
                    clientRepository.SaveChanges();

                }
            }


            return Json(new { Thumb = myClient.Thumb, Type = "Upload", Logo = myClient.Logo, Nom = myClient.Nom, Identificateur = myClient.Identificateur, Actif = myClient.Actif }, JsonRequestBehavior.AllowGet);
        }

        void saveThumb(string repertoire, string filename)
        {
            Image image = Image.FromFile(System.IO.Path.Combine(repertoire, filename));
            Image thumb = image.GetThumbnailImage(120, 120, () => false, IntPtr.Zero);
            thumb.Save(System.IO.Path.Combine(repertoire, "thumb.jpg"));
            image.Dispose();
            thumb.Dispose();
        }




        void saveTOXLSize(string repertoire, string filename)
        {
            var pathItem = System.IO.Path.Combine(repertoire, filename);
            var image = Image.FromFile(pathItem);

            {

                if (image.Size.Width > image.HorizontalResolution * 1.5 || image.Size.Height > image.VerticalResolution * 1.5)
                {
                    var resized = Imager.Resize(image, (int)((int)image.HorizontalResolution * 1.5), (int)((int)image.VerticalResolution * 1.5), true);
                    image.Dispose();
                    Imager.SaveJpeg(pathItem, resized);

                }
                else
                {
                    image.Dispose();
                }


            }



        }


        public ActionResult SaveApplications(
    ClientEditDetailsViewModel item)
        {
            if (item != null && ModelState.IsValid)
            {

                this.clientApplicationPreviRepository.UpdateApplications(item);

            };


            return Redirect("~/Client/Details?id=" + item.ClientId);

        }



        public ActionResult IsExist(string NomUtilisateur)
        {
            bool UserExist = true;


            var result = utilisateurRepository.AsQueryable().Where(x => x.NomUtilisateur == NomUtilisateur).FirstOrDefaultAsync();

            if (result == null) UserExist = false;
            else UserExist = true;

            if (UserExist == true)
            {
                return Content("false");
            }
            else
            {
                return Content("true");
            }
        }



    }

}