using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Previgesst.Ressources;
using Previgesst.Validation;


namespace Previgesst.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceType = typeof(ClientLoginRES), ErrorMessageResourceName = "MissingUsername")]
        [Display(ResourceType = typeof(ClientLoginRES), Name = "NomUsager")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ClientLoginRES), ErrorMessageResourceName = "MissingPassword")]
        [Display(ResourceType = typeof(ClientLoginRES), Name = "MotPasse")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class GenerateResetPasswordViewModel
    {
        public string UserId { get; set; }

        [Required(ErrorMessageResourceType = typeof(ClientLoginRES), ErrorMessageResourceName = "MissingUsername")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ClientLoginRES), ErrorMessageResourceName = "MissingLink")]
        public string Link { get; set; }

        [Required(ErrorMessageResourceType = typeof(ClientLoginRES), ErrorMessageResourceName = "TokenExpiryHour")]
        public string TokenExpiryHour { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Display(ResourceType = typeof(ClientLoginRES), Name = "NomUsager")]
        [Required(ErrorMessageResourceType = typeof(ClientLoginRES), ErrorMessageResourceName = "MissingUsername")]
        public string UserName { get; set; }

        [Display(ResourceType = typeof(ClientLoginRES), Name = "NewPassword")]
        [Required(ErrorMessageResourceType = typeof(ClientLoginRES), ErrorMessageResourceName = "MissingNewPassword")]
        [MinLength(8, ErrorMessageResourceType = typeof(ClientLoginRES), ErrorMessageResourceName = "PasswordValidation")]
        public string Password { get; set; }


        [Display(ResourceType = typeof(ClientLoginRES), Name = "ConfirmPassword")]
        [Required(ErrorMessageResourceType = typeof(ClientLoginRES), ErrorMessageResourceName = "MissingConfirmPassword")]
        [Compare(nameof(Password), ErrorMessageResourceType = typeof(ClientLoginRES), ErrorMessageResourceName = "PasswordMismatch")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }

    public class UserListViewModel
    {
        [Display(Name = "")]
        public string UserId { get; set; }

        [Display(ResourceType = typeof(ManageRES), Name = "User")]
        public string UserName { get; set; }

        [Display(ResourceType = typeof(ManageRES), Name = "Courriel")]
        public string Email { get; set; }

        [Display(ResourceType = typeof(ManageRES), Name = "Inactif")]
        public bool Inactive { get; set; }
    }

    public class MyAccountViewModel
    {
        [Display(ResourceType = typeof(ManageRES), Name = "Courriel")]
        [Required(ErrorMessageResourceType = typeof(ManageRES), ErrorMessageResourceName = "CourrielRequired")]
        [MaxLength(256, ErrorMessageResourceType = typeof(ManageRES), ErrorMessageResourceName = "CourrielMaxLength")]
        public string Email { get; set; }


        [Display(ResourceType = typeof(ManageRES), Name = "OldPassword")]
        [MinLength(8, ErrorMessageResourceType = typeof(ManageRES), ErrorMessageResourceName = "OldPasswordMinLength")]
        public string OldPassword { get; set; }


        [Display(ResourceType = typeof(ManageRES), Name = "NewPassword")]
        [MinLength(8, ErrorMessageResourceType = typeof(ManageRES), ErrorMessageResourceName = "NewPasswordMinLength")]
        public string NewPassword { get; set; }


        [Display(ResourceType = typeof(ManageRES), Name = "ConfirmPassword")]
        [Compare(nameof(NewPassword), ErrorMessageResourceType = typeof(ManageRES), ErrorMessageResourceName = "ConfirmPasswordCompare")]
        public string ConfirmPassword { get; set; }
    }

    public class UserViewModel
    {
        public string UserId { get; set; }

        [Display(ResourceType = typeof(ManageRES), Name = "Inactif")]
        public bool Inactive { get; set; }

        [Display(ResourceType = typeof(ManageRES), Name = "Courriel")]
        public string Email { get; set; }


        [Display(ResourceType = typeof(ManageRES), Name = "UserName")]
        [Required(ErrorMessageResourceType = typeof(ManageRES), ErrorMessageResourceName = "UserNameRequired")]
        public string UserName { get; set; }


        public bool IsCreate { get; set; }

        public bool CanDelete { get; set; }

        [Display(ResourceType = typeof(ManageRES), Name = "RoleNames")]
        [NotEmpty]
        public IEnumerable<string> RoleNames { get; set; }


        [Display(ResourceType = typeof(ManageRES), Name = "IsCorporate")]
        public bool IsCorporate { get; set; }

        [Display(ResourceType = typeof(ManageRES), Name = "CorporateClients")]
        public IEnumerable<string> CorporateClients { get; set; }
    }

    public class RoleViewModel
    {
        [Display(ResourceType = typeof(ManageRES), Name = "RoleName")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(ManageRES), Name = "RoleDescription")]
        public string Description { get; set; }
    }
}