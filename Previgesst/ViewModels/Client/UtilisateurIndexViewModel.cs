using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Previgesst.Ressources;

namespace Previgesst.ViewModels
{
    public class UtilisateurIndexViewModel
    {
        public int ClientId { get; set; }
        public string NomCIE { get; set; }


        public int UtilisateurId { get; set; }
      
        [MaxLength(100, ErrorMessageResourceType = typeof(UtilisateursRES), ErrorMessageResourceName = "ErreurNomUtilisateur")]
        [Display(ResourceType = typeof(UtilisateursRES), Name = "NomUsager")]
        [Required]
        [StringLength(500)]
        public string NomUtilisateur { get; set; }


        [MaxLength(100, ErrorMessageResourceType = typeof(UtilisateursRES), ErrorMessageResourceName = "ErreurNom")]
        [Display(ResourceType = typeof(UtilisateursRES), Name = "Nom")]
        [Required]
        [StringLength(500)]
        public string Nom { get; set; }
        [Required]
        [StringLength(500)]
        [Display(ResourceType = typeof(UtilisateursRES), Name = "MotDePasse")]
        [MaxLength(100, ErrorMessageResourceType = typeof(UtilisateursRES), ErrorMessageResourceName = "ErreurMotPasse")]
        public string MotDePasse { get; set; }


        [Display(ResourceType = typeof(UtilisateursRES), Name = "Actif")]
        public bool Actif { get; set; }
        [Required]
        [StringLength(250)]
        [MaxLength(255, ErrorMessageResourceType = typeof(UtilisateursRES), ErrorMessageResourceName = "ErreurCourriel")]
        public string Courriel { get; set; }


        public bool Suppressible { get; set; }

        [Display(ResourceType = typeof(UtilisateursRES), Name = "ADMPrevicad")]
        public Boolean AdmPrevicad { get; set; }
        [Display(ResourceType = typeof(UtilisateursRES), Name = "ADMAnalyse")]
        public Boolean AdmAnalyseRisque { get; set; }
        [Display(ResourceType = typeof(UtilisateursRES), Name = "ADMDocument")]
        public Boolean AdmDocuments { get; set; }

        [Display(ResourceType = typeof(UtilisateursRES), Name = "LecturePrevicad")]
        public Boolean ROPrevicad { get; set; }
        [Display(ResourceType = typeof(UtilisateursRES), Name = "LectureAnalyse")]
        public Boolean ROAnalyseRisque { get; set; }
        [Display(ResourceType = typeof(UtilisateursRES), Name = "LectureDocument")]
        public Boolean RODocuments { get; set; }

        [Display(ResourceType = typeof(UtilisateursRES), Name = "Auditeur")]
        public Boolean Auditeur { get; set; }

        public string MessageErreur { get; set; }

        [Display(ResourceType = typeof(UtilisateursRES), Name = "AdmUtilisateurs")]
        public Boolean AdmUtilisateurs { get; set; }

        [Display(ResourceType = typeof(UtilisateursRES), Name = "NotificationDebutCad")]
        public Boolean NotificationDebutCad { get; set; }

    }
}