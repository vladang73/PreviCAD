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
}