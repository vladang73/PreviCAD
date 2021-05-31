using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Previgesst.Controllers;
using Previgesst.Helpers;
using Previgesst.Models;
using Previgesst.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using NLog;

namespace Previgesst.Services
{
    public class ManageService
    {
        private readonly RoleManager roleManager;
        private readonly UserManager userManager;
        private readonly ILogger logger;

        protected UserManager UserManager
        {
            get
            {
                return userManager;
            }
        }

        public RoleManager RoleManager
        {
            get
            {
                return roleManager;
            }
        }

        protected string CurrentUserId => HttpContext.Current.User.Identity.GetUserId() ?? "";

        public ILogger Logger
        {
            get
            {
                return logger;
            }
        }

        public ManageService(UserManager userManager, RoleManager roleManager, ILogger logger)
        {
            this.userManager = userManager;
            this.logger = logger;
            this.roleManager = roleManager;
        }

        public List<UserListViewModel> GetAll()
        {
            var lst = UserManager.Users.Select(u => new UserListViewModel
            {
                UserId = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                Inactive = u.Inactive
            }).ToList();
            return lst;
        }

        public DataSourceResult GetFiltered(DataSourceRequest request)
        {
           // request.SetDefaultSort(nameof(UserListViewModel.UserName));

            var result = UserManager.Users.Where(x=>x.UserName!= "CDEUser").ToDataSourceResult(request, u => new UserListViewModel
            {
                UserId = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                Inactive = u.Inactive
            });
            return result;
        }

        public GenerateResetPasswordViewModel GeneratePasswordResetVM(string userId)
        {
            var user = UserManager.FindById(userId);
            if (user == null) throw new ArgumentException("L'utilisateur n'existe pas.", nameof(userId));

            var vm = new GenerateResetPasswordViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                TokenExpiryHour = UserManager.TOKEN_EXPIRY.ToString(),
                Link = ""
            };

            return vm;
        }

        public GenerateResetPasswordViewModel GeneratePasswordResetToken(string userId)
        {
            var vm = GeneratePasswordResetVM(userId);

            var strCode = UserManager.GeneratePasswordResetToken(vm.UserId);
            var encCode = HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(strCode));
            var url = new UrlHelper(System.Web.HttpContext.Current.Request.RequestContext);
            vm.Link = url.RouteUrl(RouteConfig.Default, new
                        {
                            action = nameof(AccountController.ResetPassword),
                            controller = typeof(AccountController).GetUrlName(),
                            token = encCode
                        },
                        System.Web.HttpContext.Current.Request.Url.Scheme);
            return vm;
        }

        /// <summary>
        /// Save the user. If the user is saved successfully, set the UserId in the UserViewModel passed as parameter
        /// </summary>
        /// <param name="userVM">UserViewModel to save</param>
        /// <returns>Result</returns>
        public IdentityResult SaveUser(UserViewModel userVM)
        {
            IdentityResult result;
            User user;
            if (string.IsNullOrWhiteSpace(userVM.UserId))
            {
                //New user only can set UserName
                user = new User
                {
                    UserName = userVM.UserName
                };
            }
            else
            {
                user = UserManager.FindById(userVM.UserId);
            }

            //Map all fields
            user.Email = userVM.Email;
            user.Inactive = userVM.Inactive;

            //Update user and roles in a transaction
            using (var transaction = UserManager.DbContext.Database.BeginTransaction())
            {
                if (string.IsNullOrWhiteSpace(userVM.UserId))
                {
                    result = UserManager.Create(user);
                }
                else
                {
                    result = UserManager.Update(user);
                }

                var userRoles = UserManager.GetRoles(user.Id);
                if (result == IdentityResult.Success)
                {
                    result = UserManager.AddToRoles(user.Id, userVM.RoleNames.Except(userRoles).ToArray());
                }
                if (result == IdentityResult.Success)
                {
                    result = UserManager.RemoveFromRoles(user.Id, userRoles.Except(userVM.RoleNames).ToArray());
                }
                if (result == IdentityResult.Success)
                {
                    transaction.Commit();
                    userVM.UserId = user.Id;
                }
            }

            return result;
        }

        public IdentityResult SaveUser(MyAccountViewModel myAccount)
        {
            IdentityResult result;
            var user = UserManager.FindById(CurrentUserId);

            //Map all fields
            user.Email = myAccount.Email;

            //Update user in a transaction
            using (var transaction = UserManager.DbContext.Database.BeginTransaction())
            {
                result = UserManager.Update(user);
                
                if (result == IdentityResult.Success 
                    && !string.IsNullOrWhiteSpace(myAccount.OldPassword)
                    && !string.IsNullOrWhiteSpace(myAccount.NewPassword))
                {
                    result = UserManager.ChangePassword(user.Id, myAccount.OldPassword, myAccount.NewPassword);
                }
                if (result == IdentityResult.Success)
                {
                    transaction.Commit();
                }
            }
            return result;
        }

        public MyAccountViewModel GetMyAccountVM()
        {
            var user = UserManager.FindById(CurrentUserId);

            var vm = new MyAccountViewModel
            {
                Email = user.Email
            };

            return vm;
        }

        public  bool estUserNameExistant ( string UserName, string UserId)
        {
            
            var user = UserManager.FindByName("UserName");
            if (user == null)
                return false;
            if (user.Id == UserId)
                return false;
            return true;
        }

        public bool estCourrielExistant(string courriel, string UserName)
        { var user = UserManager.FindByEmail(courriel);
            if (user == null)
                return false;
            if (user.UserName.ToUpper() == UserName.ToUpper())
                return false;
            return true;
        }
        public UserViewModel GetUserVM(string id)
        {
            var user = UserManager.FindById(id);
            var isCreate = false;
            if (user == null)
            {
                user = new User
                {
                    Id = ""
                };
                isCreate = true;
            }

            var vm = new UserViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Inactive = user.Inactive,
                RoleNames = (isCreate ? new List<string>() : UserManager.GetRoles(user.Id)),
                IsCreate = isCreate,
                CanDelete = (!isCreate && CurrentUserId != user.Id)     //Pas supprimer un nouveau ou l'utilisateur en cours
            };
            return vm;
        }


        public List<RoleViewModel> GetRoleList()
        {
            return RoleManager.Roles.Where(x=>x.Name !="System").Select(r => new RoleViewModel { Name = r.Name, Description = r.Description }).ToList();
        }

        public IdentityResult Delete(string userId)
        {
            IdentityResult result;
            User user = null;
            if (string.IsNullOrWhiteSpace(userId))
            {
                result = IdentityResult.Success;
            }
            else if (userId == CurrentUserId)
            {
                result = new IdentityResult("Impossible de supprimer l'utilisateur en cours.");
            }
            else
            {
                user = UserManager.FindById(userId);
                if (user == null)
                {
                    result = IdentityResult.Success;
                }
                else
                {
                    result = UserManager.Delete(user);
                }
            }

            if (result.Errors.Count() > 0)
            {
                Logger.Warn($"Erreur dans {this.GetType().Name}.{System.Reflection.MethodBase.GetCurrentMethod().Name}(). {JsonConvert.SerializeObject(user ?? new User { Id = userId })}. {JsonConvert.SerializeObject(result.Errors, Formatting.Indented)}.");
            }

            return result;
        }



        public User GetUserByNameName(string username)
        {
            var user = UserManager.FindByName(username);
           
            return user;
        }
    }
}