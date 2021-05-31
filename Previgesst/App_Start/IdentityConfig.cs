using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Previgesst.DataContexts;
using Previgesst.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;

namespace Previgesst
{
    // Configurer l'application que le gestionnaire des utilisateurs a utilisée dans cette application. UserManager est défini dans ASP.NET Identity et est utilisé par l'application.
    public class UserManager : UserManager<User>
    {
        /// <summary>
        /// Durée d'expiration du token pour réinitialiser le mot de passe (en heures).
        /// </summary>
        public const int TOKEN_EXPIRY = 3;

        private DbContext dbContext;
        internal virtual DbContext DbContext
        {
            get { return dbContext; }
        }

        public UserManager(IUserStore<User> store, IDataProtectionProvider dataProtectionProvider, EmailService emailService, SmsService smsService)
            : base(store)
        {
            if (store is UserStore<User>)
            {
                dbContext = (store as UserStore<User>).Context;
            }

            // Configurer la logique de validation pour les noms d'utilisateur
            this.UserValidator = new UserValidator<User>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false
            };
            // Configurer la logique de validation pour les mots de passe
            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 8,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configurer les valeurs par défaut du verrouillage de l'utilisateur
            this.UserLockoutEnabledByDefault = false;
            this.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            this.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Inscrire les fournisseurs d'authentification à 2 facteurs. Cette application utilise le téléphone et les e-mails comme procédure de réception de code pour confirmer l'utilisateur
            // Vous pouvez écrire votre propre fournisseur et le connecter ici.
            this.RegisterTwoFactorProvider("Code téléphonique ", new PhoneNumberTokenProvider<User>
            {
                MessageFormat = "Votre code de sécurité est {0}"
            });
            this.RegisterTwoFactorProvider("Code d'e-mail", new EmailTokenProvider<User>
            {
                Subject = "Code de sécurité",
                BodyFormat = "Votre code de sécurité est {0}"
            });
            this.EmailService = emailService;
            this.SmsService = smsService;

            this.UserTokenProvider =
                new DataProtectorTokenProvider<User>
                    (dataProtectionProvider.Create("Identité du portail"))
                    {
                        TokenLifespan = TimeSpan.FromHours(TOKEN_EXPIRY)       // 3 heures pour reseter le mot de passe
                    };
        }
        
        public IdentityResult SetSingleRole(string userId, string roleName)
        {
            var user = this.FindById(userId);
            var currentRoles = this.GetRoles(user.Id);
            IdentityResult result;

            //On enlève tous les autres rôles
            result = this.RemoveFromRoles(userId, currentRoles.Except(new List<string> { roleName }).ToArray());
            if (!result.Succeeded) return result;           //On arrête si erreur

            if (currentRoles.Contains(roleName))
            {
                return IdentityResult.Success;              //Le rôle est déjà présent
            }
            else
            {
                return this.AddToRole(userId, roleName);    //On ajoute le rôle
            }
        }
    }

    public class UserStore : UserStore<User>
    {
        public UserStore(DbContext context) : base(context) { }
    }

    public class RoleManager : RoleManager<Role>
    {
        private DbContext dbContext;
        internal virtual DbContext DbContext
        {
            get { return dbContext; }
        }

        public RoleManager(IRoleStore<Role, string> roleStore)
            : base(roleStore)
        {
            if (roleStore is RoleStore<Role>)
            {
                dbContext = (roleStore as RoleStore<Role>).Context;
            }
        }
    }

    public class RoleStore : RoleStore<Role>
    {
        public RoleStore(DbContext context) : base(context) { }
    }

    // Configurer le gestionnaire de connexion d'application qui est utilisé dans cette application.
    public class SignInManager : SignInManager<User, string>
    {
        public SignInManager(UserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return user.GenerateUserIdentityAsync((UserManager)UserManager);
        }
    }
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Indiquez votre service de messagerie ici pour envoyer un e-mail.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Connectez votre service SMS ici pour envoyer un message texte.
            return Task.FromResult(0);
        }
    }
}