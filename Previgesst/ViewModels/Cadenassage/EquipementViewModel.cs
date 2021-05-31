using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Previgesst.Ressources.Cadenassage;

namespace Previgesst.ViewModels
{
    public class EquipementViewModel
    {public int EquipementId { get; set; }

        [Required]
        [StringLength(250)]
        [Display(ResourceType = typeof(CadEquipRES), Name = "NomFR")]
        public string NomEquipement { get; set; }

        [Required]
        [StringLength(250)]
        [Display(ResourceType = typeof(CadEquipRES), Name = "NomEN")]
        public string NomEquipementEN { get; set; }

        public int ClientId { get; set; }
        public string NomClient { get; set; }
        public string Thumbnail { get; set; }
        public bool Suppressible { get; set; }

        public string NomFichier { get; set; }


    }
}