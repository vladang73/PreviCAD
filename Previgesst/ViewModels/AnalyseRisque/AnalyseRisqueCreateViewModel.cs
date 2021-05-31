using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Previgesst.Ressources.Analyse;

namespace Previgesst.ViewModels
{
    public class AnalyseRisqueCreateViewModel
    {
        [Display(ResourceType = typeof(ARCreateRES), Name = "NomClient")]
        public int ClientId { get; set; }


        [Display(ResourceType = typeof(ARCreateRES), Name = "Createur")]
        public string Createur { get; set; }


        [Display(ResourceType = typeof(ARCreateRES), Name = "NoReference")]
        public string NoRef { get; set; }


        [Display(ResourceType = typeof(ARCreateRES), Name = "Equipement")]
        public string Equipement { get; set; }


        [Display(ResourceType = typeof(ARCreateRES), Name = "AfficherChezClient")]
        public bool AfficherChezClient { get; set; }

        
    }
}