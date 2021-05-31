using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Previgesst.Ressources.Cadenassage;

namespace Previgesst.ViewModels
{
    public class DepartementViewModel
    {
        public int DepartementId { get; set; }

        [Required]
        [StringLength(250)]
        [Display(ResourceType = typeof(CadDeptRES), Name = "NomFR")]
        public string NomDepartement { get; set; }

        [Required]
        [StringLength(250)]
        [Display(ResourceType = typeof(CadDeptRES), Name = "NomEN")]
        public string NomDepartementEN { get; set; }
        public int ClientId { get; set; }
        public string NomClient { get; set; }
      
        public bool Suppressible { get; set; }

   
    }
}