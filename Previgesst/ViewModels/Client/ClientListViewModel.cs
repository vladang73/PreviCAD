using Previgesst.Ressources.Analyse;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Previgesst.ViewModels
{
    public class ClientListViewModel
    {
        public int ClientId { get; set; }

        [Required]
        [StringLength(250)]
        [Display(ResourceType = typeof(ARRES), Name = "Nom")]
        public string Nom { get; set; }


        [Required]
        [StringLength(250)]
        [Display(ResourceType = typeof(ARRES), Name = "Identificateur")]
        public string Identificateur { get; set; }



        [Display(ResourceType = typeof(ARRES), Name = "Actif")]
        public Boolean Actif { get; set; }

        public Boolean EstSupprimable { get; set; }

        public string Bidon { get; set; }

        [Display(ResourceType = typeof(ARRES), Name = "Logo")]
        public string Logo { get; set; }

        public string Thumb { get; set; }

        public Guid? UniqueIdentifier { get; set; }

        public string Lien { get; set; }
    }
}