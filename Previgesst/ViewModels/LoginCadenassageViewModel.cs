using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Previgesst.Ressources;
using Previgesst.Ressources.Previcad;

namespace Previgesst.ViewModels
{
    public class LoginCadenassageViewModel
    {
        [Required]
        [StringLength(250)]

        [Display(ResourceType = typeof(PrevIndexRES), Name = "NoEmploye")]
        public string NoEmploye { get; set; }

        [Required]
        [StringLength(250)]
        [Display(ResourceType = typeof(PrevIndexRES), Name = "MotPasse")]
        public string Password { get; set; }

        public Guid Identificateur { get; set; }
        public Boolean MaintenancePrevue { get; set; }
        public string NextUpdateTextFr { get; set; }
        public string NextUpdateTextEn { get; set; }
        public string Langue { get; set; }
    }


    public class ChangeEmployeePasswordViewModel
    {
        public int EmpID { get; set; }

        [Display(ResourceType = typeof(PrevIndexRES), Name = "NoEmploye")]
        public string NoEmploye { get; set; }


        [Display(ResourceType = typeof(ClientLoginRES), Name = "NewPassword")]
        [Required(ErrorMessageResourceType = typeof(ClientLoginRES), ErrorMessageResourceName = "MissingNewPassword")]
        [MinLength(6, ErrorMessageResourceType = typeof(ClientLoginRES), ErrorMessageResourceName = "PasswordValidation")]
        public string Password { get; set; }


        [Display(ResourceType = typeof(ClientLoginRES), Name = "ConfirmPassword")]
        [Required(ErrorMessageResourceType = typeof(ClientLoginRES), ErrorMessageResourceName = "MissingConfirmPassword")]
        [Compare(nameof(Password), ErrorMessageResourceType = typeof(ClientLoginRES), ErrorMessageResourceName = "PasswordMismatch")]
        public string ConfirmPassword { get; set; }
    }
}