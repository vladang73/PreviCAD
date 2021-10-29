

using Previgesst.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Previgesst.Ressources.Cadenassage;

namespace Previgesst.ViewModels
{
    public class EditFicheViewModel
    {
        public string sauvegarderMaFicheDeCadenassage { get; set; }

        public int FicheCadenassageId { get; set; }


        [Required]

        [Display(ResourceType = typeof(CadInfoGeneralesRES), Name = "Equipement")]
        public int EquipementId { get; set; }

        [Required]

        [Display(ResourceType = typeof(CadInfoGeneralesRES), Name = "Departement")]
        public int DepartementId { get; set; }

        [Required]
        public int ClientId { get; set; }


        [Required]
        [StringLength(250)]
        [MaxLength(250, ErrorMessageResourceType = typeof(CadInfoGeneralesRES), ErrorMessageResourceName = "ErreurTravail")]
        [Display(ResourceType = typeof(CadInfoGeneralesRES), Name = "TravailAEffectuer")]

        public string TravailAEffectuer { get; set; }


        [StringLength(250)]
        [MaxLength(250, ErrorMessageResourceType = typeof(CadInfoGeneralesRES), ErrorMessageResourceName = "ErreurTravailEN")]
        [Display(ResourceType = typeof(CadInfoGeneralesRES), Name = "TravailAEffectuerEN")]
        public string TravailAEffectuerEN { get; set; }


        [Required]
        [StringLength(250)]
        [MaxLength(250, ErrorMessageResourceType = typeof(CadInfoGeneralesRES), ErrorMessageResourceName = "ErreurNoFiche")]

        [Display(ResourceType = typeof(CadInfoGeneralesRES), Name = "NoFiche")]
        public string NoFiche { get; set; }



        [StringLength(250)]
        [Display(ResourceType = typeof(CadInfoGeneralesRES), Name = "ApprouvePar")]
        [MaxLength(250, ErrorMessageResourceType = typeof(CadInfoGeneralesRES), ErrorMessageResourceName = "ErreurApprouvePar")]
        public string ApprouvePar { get; set; }

        [Display(ResourceType = typeof(CadInfoGeneralesRES), Name = "DateApproved")]
        public DateTime? DateApproved { get; set; }


        [Display(ResourceType = typeof(CadInfoGeneralesRES), Name = "DateCreation")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? DateCreation { get; set; }


        [StringLength(250)]
        [Display(ResourceType = typeof(CadInfoGeneralesRES), Name = "Createur")]
        public string CreatedPar { get; set; }


        [Display(ResourceType = typeof(CadInfoGeneralesRES), Name = "UpdatedDate")]
        public DateTime? DateUpdated { get; set; }

        [StringLength(250)]
        [Display(ResourceType = typeof(CadInfoGeneralesRES), Name = "UpdatedBy")]
        public string UpdatedPar { get; set; }






        public string BidonErreurCreation { get; set; }


        //[Display(ResourceType = typeof(CadInfoGeneralesRES), Name = "DateRevision")]
        //public DateTime? DateRevision { get; set; }

        public bool EstDocumentPrevigesst { get; set; }

        public IEnumerable<int> SourcesEnergieId { get; set; } = new List<int>();

        public string NomClient { get; set; }

        public Boolean DisplaySourceTab { get; set; }



        [Display(ResourceType = typeof(CadInfoGeneralesRES), Name = "AfficherClient")]

        public Boolean AfficherClient { get; set; }

        [Display(ResourceType = typeof(CadInfoGeneralesRES), Name = "TypeFiche")]
        [Required]
        public String TitreFiche { get; set; }

        [Display(ResourceType = typeof(CadInfoGeneralesRES), Name = "Actif")]

        //public Boolean RevisionCourante { get; set; }
        public Boolean IsApproved { get; set; }

        public Boolean DroitAjout { get; set; }

        public Boolean estUpdate { get; set; }

        public Boolean estClient { get; set; }


        [Display(ResourceType = typeof(CadInfoGeneralesRES), Name = "ValidatedDate")]
        public DateTime? DateValidated { get; set; }

        [StringLength(250)]
        [Display(ResourceType = typeof(CadInfoGeneralesRES), Name = "ValidatedBy")]
        public string ValidatedPar { get; set; }
    }
}