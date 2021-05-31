using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Previgesst.Ressources;
using Previgesst.Ressources.Previcad;

namespace Previgesst.ViewModels
{
    public class ParametresAppViewModel
    {
        public int ParametresAppId { get; set; }

        [Display(ResourceType = typeof(ParametresAppRES), Name = "NextUpdateCase")]
        public bool NextUpdateCase { get; set; }

        [Display(ResourceType = typeof(ParametresAppRES), Name = "NextUpdateTextFr")]
        public string NextUpdateTextFr { get; set; }

        [Display(ResourceType = typeof(ParametresAppRES), Name = "NextUpdateTextEn")]

        public string NextUpdateTextEn { get; set; }

        public Boolean MaintenancePrevue { get; set; }
    }
}