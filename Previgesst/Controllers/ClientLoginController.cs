using Newtonsoft.Json;
using Previgesst.ActionFilters;
using Previgesst.Helpers;
using Previgesst.Repositories;
using Previgesst.Ressources;
using Previgesst.Services;
using Previgesst.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Previgesst.Controllers
{
    public class ClientLoginController : Controller
    {
        UtilisateurService utilisateurService;
        AccountService accountService;
        ClientRepository clientRepository;
        ParametresAppService parametresAppService;
        ParametresAppController parametresAppController;

        public ClientLoginController(UtilisateurService utilisateurService, AccountService accountService, ClientRepository clientRepository, ParametresAppService parametresAppService, ParametresAppController parametresAppController)
        {
            this.utilisateurService = utilisateurService;
            this.accountService = accountService;
            this.clientRepository = clientRepository;
            this.parametresAppService = parametresAppService;
            this.parametresAppController = parametresAppController;
        }

        public string Layout { get; private set; }

        // GET: ClientLogin
        public ActionResult Index()
        {
            // Initialize VM, create MaintenancePrevue and parser it
            var vm = new LoginClientViewModel();
            vm.MaintenancePrevue = false;

            if (parametresAppService.VerifierStatusMaintenance())
            {
                utilisateurService.LogOff();
                vm.MaintenancePrevue = true;
                var parametresAppIdRequest = JsonConvert.SerializeObject(parametresAppService.GetparametresAppId());
                var parametresAppConvert = parametresAppIdRequest.Replace("[", "").Replace("]", "");
                var parametresAppId = int.Parse(parametresAppConvert);
                var elementsMaintenance = parametresAppController.GetReadListParametresApp(parametresAppId);
                var NextUpdateTextFr = elementsMaintenance.NextUpdateTextFr;
                var NextUpdateTextEn = elementsMaintenance.NextUpdateTextEn;
                var langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

                vm.NextUpdateTextFr = NextUpdateTextFr;
                vm.NextUpdateTextEn = NextUpdateTextEn;
                vm.Langue = langue;


                return View("Index", null, vm);
            }
            else
            {
                utilisateurService.LogOff();
                vm.MaintenancePrevue = false;
                return View("Index", null, vm);
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Index(LoginClientViewModel vm, string ReturnUrl)
        {
            // check into utilisateur table
            var isLogOK = utilisateurService.isLoginOk(vm);
            if (isLogOK)
            {
                var result = accountService.Login("CDEUser", "6k^3U6+8_2N-FvprQXs>");
                
                //var result = accountService.Login(vm.UserName, vm.Password);

                // if logged in
                if (result == Microsoft.AspNet.Identity.Owin.SignInStatus.Success)
                {
                    return RedirectToLocal(ReturnUrl);
                }
                else
                {
                    //this is new code here
                    System.Web.HttpContext.Current.Session.Abandon();

                    ModelState.AddModelError("", "Tentative de connexion non valide.");
                    return View(vm);
                }
            }
            else
            {
                ModelState.AddModelError("", "Tentative de connexion non valide.");
                return View(vm);
            }
        }

        public ActionResult LogOff()
        {
            if (System.Web.HttpContext.Current.Session["EmployeCadenassage"] == null)
            {// site administratif du client
                utilisateurService.LogOff();
                return RedirectToAction(nameof(ClientLoginController.Index), typeof(ClientLoginController).GetUrlName());
            }
            else
            {
                EmployeRegistreViewModel employe = (EmployeRegistreViewModel)System.Web.HttpContext.Current.Session["EmployeCadenassage"];
                var client = clientRepository.Get(employe.ClientId);
                var urlBuilder =
                                   new System.UriBuilder(Request.Url.AbsoluteUri)
                                   {
                                       Path = Url.Content("~/Registre/Index?ClientID=" + client.IdentificateurUnique.ToString()),
                                       Query = null,
                                   };

                Uri uri = urlBuilder.Uri;
                string url = urlBuilder.ToString().Replace("%3F", "?");

                System.Web.HttpContext.Current.Session.Abandon();
                return Redirect(url);

            }

        }


        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(nameof(MesApplicationsController.Index), typeof(MesApplicationsController).GetUrlName());
        }



        [HttpGet]
        public ActionResult ChangePassword()
        {
            var session = utilisateurService.GetSession();

            if (session != null)
            {
                var client = clientRepository.Get(session.ClientId);
                return View(new ChangeClientPasswordViewModel { UserName = session.NomUtilisateur, Identificateur = client.Identificateur });
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Throttle(Message = "Attendez {n} secondes avant d'exécuter cette action de nouveau.", Name = nameof(ChangePassword), Seconds = 5)]
        public ActionResult ChangePassword(ChangeClientPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = utilisateurService.ChangePassword(model);

                if (result)
                {
                    return RedirectToAction(nameof(AdminPreviController.Index), typeof(AdminPreviController).GetUrlName());
                }
            }

            ModelState.AddModelError("", LayoutRES.ChangePasswordError);
            return View(model);
        }
    }
}