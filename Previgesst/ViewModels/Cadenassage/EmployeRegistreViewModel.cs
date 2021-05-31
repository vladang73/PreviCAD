using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Previgesst.Ressources.Cadenassage;

namespace Previgesst.ViewModels
{
    public class EmployeRegistreViewModel
    {

        public int EmployeRegistreId { get; set; }


        [Required]
        [StringLength(250)]
        [Display(ResourceType = typeof(CadEmpRES), Name = "Nom")]
        public string Nom { get; set; }

        [Required]
        [Display(ResourceType = typeof(CadEmpRES), Name = "Departement")]
        public int DepartementId { get; set; }
        [Display(ResourceType = typeof(CadEmpRES), Name = "Departement")]
        public string NomDepartement { get; set; }

        [Required]
        [StringLength(25)]
        [Display(ResourceType = typeof(CadEmpRES), Name = "NoEMP")]
        public string NoEmploye { get; set; }
        
        [Required]
        [StringLength(250,MinimumLength = 6, ErrorMessageResourceType = typeof(CadEmpRES), ErrorMessageResourceName = "ErreurMotPasse")]

        [Display(ResourceType = typeof(CadEmpRES), Name = "MotPasse")]
        public string Password { get; set; }


       
        [StringLength(250)]
        [Display(ResourceType = typeof(CadEmpRES), Name = "Cadenas")]
        public string NoCadenas { get; set; }

        public bool Suppressible { get; set; }

        public int ClientId { get; set; }

        [Display(ResourceType = typeof(CadEmpRES), Name = "Actif")]
        public bool Actif { get; set; }

        public string Langue { get; set; }


    }
}