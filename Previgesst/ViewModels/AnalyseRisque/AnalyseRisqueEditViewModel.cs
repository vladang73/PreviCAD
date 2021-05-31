using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Previgesst.Ressources.Analyse;

namespace Previgesst.ViewModels
{
    public class AnalyseRisqueEditViewModel
    {
        public int Id { get; set; }

        [Display(ResourceType = typeof(ARCreateRES), Name = "NomClient")]
        public string NomClient { get; set; }

        [Display(ResourceType = typeof(ARCreateRES), Name = "Createur")]
        public string Createur { get; set; }

        [Display(ResourceType = typeof(ARCreateRES), Name = "DateCreation")]
        public string DateCreation { get; set; }

        [Display(ResourceType = typeof(ARCreateRES), Name = "UserMAJ")]
        public string UserMAJ { get; set; }

        [Display(ResourceType = typeof(ARCreateRES), Name = "DateMAJ")]
        public string DateMAJ { get; set; }

        [Display(ResourceType = typeof(ARCreateRES), Name = "Participants")]
        public string Participants { get; set; }

        [Display(ResourceType = typeof(ARCreateRES), Name = "AfficherChezClient")]
        public bool AfficherChezClient { get; set; }

        [Display(ResourceType = typeof(ARCreateRES), Name = "NoReference")]
        public string NoRef { get; set; }

        [Display(ResourceType = typeof(ARCreateRES), Name = "Equipement")]
        public string Equipement { get; set; }

        public int ClientId { get; set; }
    }
}