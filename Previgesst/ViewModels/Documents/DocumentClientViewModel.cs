using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Previgesst.Ressources.Cadenassage;

namespace Previgesst.ViewModels
{
    public class DocumentClientViewModel
    {

        public int DocumentClientId { get; set; }

        public int ClientId { get; set; }

        [Required]
        [StringLength(250)]

        [Display(ResourceType = typeof(CadProgRES), Name = "Titre")]
        public string Titre { get; set; }

        [Required]
        [StringLength(250)]

        [Display(ResourceType = typeof(CadProgRES), Name = "Fichier")]
        public string FileName { get; set; }

        public int? ApplicationPreviId { get; set; }

        public string Bidon { get; set; }

        public string BasePath { get; set; }

        public string Suppressible { get; set; }

    }
}