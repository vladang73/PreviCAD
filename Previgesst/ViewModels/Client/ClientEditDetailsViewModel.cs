using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Previgesst.ViewModels
{
    public class ClientEditDetailsViewModel
    {
        public int ClientId { get; set; }
        public string Nom { get; set; }

        [Display(Name = "Applications")]
        public IEnumerable<int> ApplicationIds { get; set; } = new List<int>();

        public string LienPrevicad;

        [Display(Name = "Nombre d'administrateurs Prévicad")]
        public int NbAdminsPrevicadMax { get; set; }
        [Display(Name = "Nombre d'administrateurs Analyse de risques")]
        public int NbAdminsAnalyseRisqueMax { get; set; }

        [Display(Name = "Autorisation")]
        public int StatusCadenassage { get; set; }


        [Display(Name = "Limite fiches de cadenassage")]
        public int NbLimitCadenassage { get; set; }


        [Display(Name = "Période d'essai")]
        public bool PeriodeEssai { get; set; }


        [Display(Name = "Date limite")]
        public DateTime DateCadenassage { get; set; }
        

        [Display(Name = "Total des fiches enregistrées")]
        public int TotalCadenassage { get; set; }
        

        //[Display(Name = "Nombre d'administrateurs Documents")]
        //public int NbAdminsDocumentsMax { get; set; }
        [Display(Name = "Nombre d'auditeurs")]
        public int NbAuditeursMax { get; set; }
        [Display(Name = "Nombre d'utilisateurs Prévicad")]
        public int NbUtilisateursPrevicad { get; set; }

        public string classMaximums { get; set; }
        public string classDefault { get; set; }
        [Display(Name = "Nombre d'administrateurs des utilisateurs")]
        public int NbAdminUtilisateurs { get; set; }

        
    }
}