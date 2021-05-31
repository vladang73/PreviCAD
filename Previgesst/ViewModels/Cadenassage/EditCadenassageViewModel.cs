using Kendo.Mvc.UI;
using Previgesst.Ressources.Cadenassage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace Previgesst.ViewModels
{
    public class EditCadenassageViewModel
    {
        public int ClientId { get; set; }
        public string Logo { get; set; }
        public string Nom { get; set; }
        public Boolean estClient { get; set; }

        public Boolean estUpdate { get; set; }

        public Boolean estAudit { get; set; }

        public Guid IdentificateurUnique { get; set; }

        public string RCDisabled { get; set; }

        public string LienPrevicad { get; set; }

        [Display(ResourceType = typeof(CadEditRES), Name = "NotificationDebutCad")]

        public Boolean NotificationDebutCad { get; set; }
        public DataSourceResult departements { get; internal set; }
    }
}