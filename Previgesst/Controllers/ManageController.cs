using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Previgesst.Enums;
using Previgesst.Services;
using Previgesst.ViewModels;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity;
using NLog;
using Previgesst.ActionFilters;
using Previgesst.Repositories;

namespace Previgesst.Controllers
{
    [AuthorizeRoles(Enums.Roles.Administrateur, Enums.Roles.LectureEcriture, Enums.Roles.System)]
    public class ManageController : ControllerBase

    {

        UtilisateurService utilisateurService;
        private string Layout;

        private readonly ManageService manageService;
        public ManageService ManageService
        {
            get
            {
                return manageService;
            }
        }

        public ManageController(ILogger logger, ManageService manageService, UtilisateurService utilisateurService) : base(logger)
        {
            this.manageService = manageService;
            this.utilisateurService = utilisateurService;

            if (utilisateurService.GetSession() != null)
            {
                this.Layout = "~/Views/Shared/_LayoutClient.cshtml";

            }
            else
                this.Layout = "~/Views/Shared/_Layout.cshtml";
       
    }

    /// <summary>
    /// Liste des utilisateurs
    /// </summary>
    [AuthorizeRoles(Enums.Roles.Administrateur, Enums.Roles.LectureEcriture)]
        public ActionResult Index()
        {
            ShowSavedMessage();

            return View();
        }

        /// <summary>
        /// Liste des utilisateurs pour le DataSource Telerik
        /// </summary>
        /// <param name="request">Requête read Telerik</param>
        /// <returns>Resultat de la requête en JSON</returns>
     //   [Authorize(Roles = Roles.Administration)]
        public ActionResult UserRead([DataSourceRequest]DataSourceRequest request)
        {
            return Json(ManageService.GetFiltered(request));
        }

        [Authorize(Roles = Roles.Administrateur)]
        public ActionResult GenerateResetPassword(string id)
        {
            var vm = ManageService.GeneratePasswordResetVM(id);
            return View("GenerateResetPassword","_Layout", vm);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Administrateur)]
        public ActionResult GenerateResetPassword(GenerateResetPasswordViewModel resetPassword)
        {
            var vm = ManageService.GeneratePasswordResetToken(resetPassword.UserId);
            return View(vm);
        }

        [Authorize(Roles = Roles.Administrateur)]
        [ImportModelStateFromTempData]
        public ActionResult EditUser(string id)
        {
            ShowSavedMessage();
            var vm = ManageService.GetUserVM(id);

            PopulateRoles();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Administrateur)]
        public ActionResult EditUser(UserViewModel user)
        {
            if (manageService.estUserNameExistant(user.UserName, user.UserId))
                ModelState.AddModelError("UserName", "Utilisateur existant");
            if (manageService.estCourrielExistant(user.Email, user.UserName))
                ModelState.AddModelError("Email", "Courriel existant");
            if (ModelState.IsValid)
            {
                var result = ManageService.SaveUser(user);
                if (result.Succeeded)
                {
                    SaveSuccessMessage("Enregistrement réussi");
                    return RedirectToAction(nameof(EditUser), new { id = user.UserId });
                }
                else
                {
                    AddErrors(result);
                }
            }
            PopulateRoles();
            return View(user);
        }

        protected void PopulateRoles()
        {
            ViewBag.Roles = ManageService.GetRoleList();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Administrateur)]
        [ExportModelStateToTempData]
        public ActionResult DeleteConfirmed(UserViewModel user)
        {
            var result = ManageService.Delete(user.UserId);
            if (result.Succeeded)
            {
                SaveSuccessMessage("Suppression réussie");
                ModelState.Clear();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                AddErrors(result);
                return RedirectToAction(nameof(EditUser), new { id = user.UserId });
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public ActionResult MyAccount()
        {
            var vm = ManageService.GetMyAccountVM();
            return View("MyAccount", Layout,vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]       
        public ActionResult MyAccount(MyAccountViewModel myAccount)
        {
            if (ModelState.IsValid)
            {
                var result = ManageService.SaveUser(myAccount);
                if (result.Succeeded)
                {
                    ShowSuccess("Enregistrement réussi");
                    myAccount = ManageService.GetMyAccountVM();
                }
                else
                {
                    AddErrors(result);
                }

            }

            return View("MyAccount",Layout, myAccount);
        }
    }
}