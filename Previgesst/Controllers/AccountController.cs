using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Previgesst.ActionFilters;
using Previgesst.Helpers;
using Previgesst.Services;
using Previgesst.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using NLog;

namespace Previgesst.Controllers
{
    public class AccountController : ControllerBase
    {

        private string Layout;

        UtilisateurService utilisateurService;
        private readonly AccountService accountService;

        protected AccountService AccountService
        {
            get
            {
                return accountService;
            }
        }

        public AccountController(ILogger logger, AccountService accountService, UtilisateurService utilisateurService) : base(logger)
        {
            this.accountService = accountService;
            this.utilisateurService = utilisateurService;


            if (utilisateurService.GetSession() != null)
            {
                this.Layout = "~/Views/Shared/_LayoutClient.cshtml";

            }
            else
                this.Layout = "~/Views/Shared/_Layout.cshtml";
        }

        public ActionResult Login()
        {
            ShowSavedMessage();

            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = AccountService.Login(model.UserName, model.Password);
                //CDEUser
                //6k^3U6+8_2N-FvprQXs>


                switch (result)
                {
                    case SignInStatus.Success:
                        return RedirectToLocal(ReturnUrl);
                    /*case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:   //Aucune vérifications pour ce site
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });*/
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "Tentative de connexion non valide.");
                        return View(model);
                }
            }

            return View("Login", null, model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(nameof(AdminPreviController.Index), typeof(AdminPreviController).GetUrlName());
        }

        public ActionResult ResetPassword(string token)
        {
            var vm = new ResetPasswordViewModel
            {
                Token = token
            };
            return View("ResetPassword", "_Layout", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Throttle(Message = "Attendez {n} secondes avant d'exécuter cette action de nouveau.", Name = nameof(ResetPassword), Seconds = 5)]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = AccountService.ResetPassword(model);
                if (result.Succeeded)
                {
                    SaveSuccessMessage("Mot de passe réinitialisé avec succès");
                    return RedirectToAction(nameof(Login));
                }

                AddErrors(result);
            }

            return View(model);
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            AccountService.SignOut();
            Session.Abandon();
            Session.Clear();
            return RedirectToAction(nameof(Login));
        }
    }
}