using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Previgesst.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Previgesst.Services
{
    public class AccountService
    {
        private readonly UserManager userManager;
        private readonly SignInManager signInManager;
        private readonly IAuthenticationManager authenticationManager;
        //internal static string emailSender;

        public UserManager UserManager
        {
            get
            {
                return userManager;
            }
        }
        public SignInManager SignInManager
        {
            get
            {
                return signInManager;
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return authenticationManager;
            }
        }

        public AccountService(UserManager userManager, SignInManager signInManager, IAuthenticationManager authenticationManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.authenticationManager = authenticationManager;
        }

        public SignInStatus Login(string userName, string password)
        {
            // signin user based on AspNetUsers table
            var result = SignInManager.PasswordSignIn(userName, password, false, false);

            #region ----- if corporate customer -----

            if (result == SignInStatus.Success)
            {
                var currentUser = UserManager.FindByName(userName);

                HttpContext.Current.Session["IsCorporate"] = currentUser.IsCorporate;
                HttpContext.Current.Session["CorporateClients"] = string.IsNullOrEmpty(currentUser.CorporateClients) ? null : currentUser.CorporateClients.Split(',');
            }

            #endregion

            return result;
        }

        public IdentityResult ResetPassword(ResetPasswordViewModel model)
        {
            var user = UserManager.FindByName(model.UserName);
            if (user == null)
            {
                //Do not reveal that the user doesn't exist
                return IdentityResult.Success;
            }

            var result = UserManager.ResetPassword(user.Id, Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(model.Token)), model.Password);

            return result;
        }

        public void SignOut()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        public IdentityResult ChangePassword(ChangePasswordViewModel model)
        {
            var user = UserManager.FindByName(model.UserName);
            if (user == null)
            {
                //Do not reveal that the user doesn't exist
                return IdentityResult.Success;
            }

            var result = UserManager.ChangePassword(user.Id, model.CurrentPassword, model.Password);

            return result;
        }
    }
}