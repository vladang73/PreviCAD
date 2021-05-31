using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Previgesst.Ressources.Analyse;

namespace Previgesst.ViewModels
{
    public class AnalyseRisqueListViewModel
    {
        public int Id { get; set; }
        public int AnalyseRisqueId { get; set; }
        [DisplayName("Nom du client")]


        public DateTime DateCreation { get; set; }
        public string NomClient { get; set; }
        public string Createur { get; set; }
        public string Participants { get; set; }
        

        [Display(ResourceType = typeof(AREditClientRES), Name = "NoReference")]
        public string NoRef { get; set; }

        [Display(ResourceType = typeof(AREditClientRES), Name = "DerniereMAJ")]
        public DateTime? DateMAJ { get; set; }

        public string UserMAJ { get; set; }

        [DisplayName("Afficher chez le client")]
        public bool AfficherChezClient { get; set; }


        public Boolean? estDocumentPrevigesst { get; set; }

        public Boolean Editable { get; set; }

        public Boolean? Suppressible { get; set; }

        [Display(ResourceType = typeof(AREditClientRES), Name = "Equipement")]
        public string Equipement { get; set; }


    }
}