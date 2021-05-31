using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Previgesst.Ressources.Cadenassage;

namespace Previgesst.ViewModels
{
    public class PhotoFicheCadenassageViewModel
    {
        public int PhotoFicheCadenassageId { get; set; }
        [Display(ResourceType = typeof(CadImageRES), Name = "Rang")]
        public int Rang { get; set; }
        public int FicheCadenassageId { get; set; }

        public bool Suppressible { get; set; }

        [Display(ResourceType = typeof(CadImageRES), Name = "LocalisationFR")]

        public string Localisation { get; set; }
        [Display(ResourceType = typeof(CadImageRES), Name = "LocalisationEN")]
        public string LocalisationEN { get; set; }
        public string URL { get; set; }
        public string Fichier { get; set; }

    }
}