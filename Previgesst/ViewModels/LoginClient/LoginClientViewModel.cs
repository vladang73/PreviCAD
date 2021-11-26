using Previgesst.Ressources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Previgesst.ViewModels
{
    public class LoginClientViewModel
    {

        [Required]
        [StringLength(250)]
        [Display(ResourceType = typeof(ClientLoginRES), Name ="IdentificateurCIE")]
        public string Identificateur { get; set; }

        [Required(ErrorMessageResourceType = typeof(ClientLoginRES), ErrorMessageResourceName = "MissingUsername")]
        [StringLength(250)]
        [Display(ResourceType = typeof(ClientLoginRES), Name = "NomUsager")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ClientLoginRES), ErrorMessageResourceName = "MissingPassword")]
        [StringLength(250)]
        [Display(ResourceType = typeof(ClientLoginRES), Name = "MotPasse")]
        public string Password { get; set; }

        public Boolean MaintenancePrevue { get; set; }
        public string NextUpdateTextFr { get; set; }
        public string NextUpdateTextEn { get; set; }
        public string Langue { get; internal set; }
    }

    public class ChangeClientPasswordViewModel
    {

        [Required]
        [StringLength(250)]
        [Display(ResourceType = typeof(ClientLoginRES), Name = "IdentificateurCIE")]
        public string Identificateur { get; set; }

        [Required(ErrorMessageResourceType = typeof(ClientLoginRES), ErrorMessageResourceName = "MissingUsername")]
        [MinLength(8, ErrorMessageResourceType = typeof(ClientLoginRES), ErrorMessageResourceName = "PasswordValidation")]
        [Display(ResourceType = typeof(ClientLoginRES), Name = "NomUsager")]
        public string UserName { get; set; }


        [Display(ResourceType = typeof(ClientLoginRES), Name = "CurrentPassword")]
        [Required(ErrorMessageResourceType = typeof(ClientLoginRES), ErrorMessageResourceName = "MissingPassword")]
        public string CurrentPassword { get; set; }


        [Required(ErrorMessageResourceType = typeof(ClientLoginRES), ErrorMessageResourceName = "MissingPassword")]
        [Display(ResourceType = typeof(ClientLoginRES), Name = "MotPasse")]
        [MinLength(8, ErrorMessageResourceType = typeof(ClientLoginRES), ErrorMessageResourceName = "PasswordValidation")]
        public string Password { get; set; }


        [MinLength(8, ErrorMessageResourceType = typeof(ClientLoginRES), ErrorMessageResourceName = "PasswordValidation")]
        [Display(ResourceType = typeof(ClientLoginRES), Name = "ConfirmPassword")]
        [Required(ErrorMessageResourceType = typeof(ClientLoginRES), ErrorMessageResourceName = "MissingConfirmPassword")]
        [Compare(nameof(Password), ErrorMessageResourceType = typeof(ClientLoginRES), ErrorMessageResourceName = "PasswordMismatch")]
        public string ConfirmPassword { get; set; }
    }
}