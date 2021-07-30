using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Previgesst.Ressources.Cadenassage;

namespace Previgesst.ViewModels
{
    public class EquipementQRViewModel
    {
        public int EquipementId { get; set; }

        [Display(ResourceType = typeof(CadEquipRES), Name = "NomFR")]
        public string NomEquipement { get; set; }

        [Display(ResourceType = typeof(CadEquipRES), Name = "NomEN")]
        public string NomEquipementEN { get; set; }

        public int ClientId { get; set; }
        public string NomClient { get; set; }
        public string QRCode { get; set; }
    }
}