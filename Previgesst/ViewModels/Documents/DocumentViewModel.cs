using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Previgesst.Ressources;
namespace Previgesst.ViewModels
{
    public class DocumentViewModel
    {
        public int DocumentId { get; set; }

        public int Bidon { get; set; }

        [Required]
        [Display(ResourceType = typeof(DocumentRES), Name = "Titre")]
        [MaxLength(255, ErrorMessageResourceType = typeof(DocumentRES), ErrorMessageResourceName = "TitreErreur")]

        [StringLength(255, ErrorMessage = "Le titre ne doit pas dépasser {1} caractères.")]
        public string Titre { get; set; }

        [Required]
        [DisplayName("Section")]
        [Display(ResourceType = typeof(DocumentRES), Name = "Section")]
        public int SectionId { get; set; }



        [DisplayName("Section")]
        [Display(ResourceType = typeof(DocumentRES), Name = "Section")]
        public string DisplaySection { get; set; }


        [Display(ResourceType = typeof(DocumentRES), Name = "Ordre")]
        public int Ordre { get; set; }


        [Required]
        [Display(ResourceType = typeof(DocumentRES), Name = "Fichier")]
        public string FileName { get; set; }


        public string BasePath { get; set; }
    }
}