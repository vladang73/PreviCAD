using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Previgesst.Ressources.Cadenassage;

namespace Previgesst.ViewModels
{
    public class LigneCadenassageGridViewModel
    {
        public int FicheCadenassageId { get; set; }
        public int EquipementId { get; set; }
        [DisplayName("Équipement")]
        [Display(ResourceType = typeof(CadFichesRES), Name = "Equipement")]
        public String NomEquipement { get; set; }

        [Display(ResourceType = typeof(CadFichesRES), Name = "Departement")]
        [Required]
        [StringLength(250)]
        public string Departement { get; set; }

        [Required]
        [StringLength(250)]
        [Display(ResourceType = typeof(CadFichesRES), Name = "Num")]
        public string NumeroEquipment { get; set; }

        [Display(ResourceType = typeof(CadFichesRES), Name = "NoFiche")]
        [Required]
        [StringLength(250)]
        public string NoFiche { get; set; }
        public int ClientId { get; set; }


        //public DateTime? DateRevision { get; set; }

        [Display(ResourceType = typeof(CadFichesRES), Name = "ApprouvePar")]
        [StringLength(250)]
        public string ApprouvePar { get; set; }
        public DateTime? DateApproved { get; set; }

        [StringLength(250)]
        public string CreatedPar { get; set; }

        public DateTime? DateCreation { get; set; }



        [StringLength(250)]
        public string UpdatedPar { get; set; }
        public DateTime? DateUpdated { get; set; }



        [Display(ResourceType = typeof(CadFichesRES), Name = "Travail")]
        [Required]
        [StringLength(250)]
        public string TravailAEffectuer { get; set; }

        public Boolean Suppressible { get; set; }
        public Boolean Editable { get; set; }
        [Display(ResourceType = typeof(CadFichesRES), Name = "Previgesst")]
        public Boolean estDocumentPrevigesst { get; set; }

        [Display(ResourceType = typeof(CadFichesRES), Name = "IsApproved")]
        public Boolean IsApproved { get; set; }

        public string TitreFiche { get; set; }

        public List<string> TexteMateriel { get; set; }


        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        //public bool IsApproved
        //{
        //    get
        //    {
        //        return !string.IsNullOrEmpty(ApprouvePar);
        //    }
        //}
    }
}