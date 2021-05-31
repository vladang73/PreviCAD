using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Previgesst.Ressources.Cadenassage;


namespace Previgesst.ViewModels
{
    public class MaterielRequisCadenassageViewModel
    {
        public int MaterielRequisCadenassageId { get; set; }


        public int FicheCadenassageId { get; set; }
        [Required]
        [Display(ResourceType = typeof(CadMaterielRES), Name = "Materiel")]
        public int MaterielId { get; set; }

        [Required]
        [Display(ResourceType = typeof(CadMaterielRES), Name = "Quantite")]

        public int Quantite { get; set; }

        public bool Suppressible { get; set; }

        [Display(ResourceType = typeof(CadMaterielRES), Name = "Nom")]
        public string NomMateriel { get; set; }
    }

}