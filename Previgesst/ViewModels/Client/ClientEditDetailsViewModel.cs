using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Previgesst.Ressources;

namespace Previgesst.ViewModels
{
    public class ClientEditDetailsViewModel
    {
        public int ClientId { get; set; }
        
        [Display(ResourceType = typeof(ApplicationsRES), Name = "Nom")]
        public string Nom { get; set; }

        [Display(ResourceType = typeof(ApplicationsRES), Name = "Applications")]
        public IEnumerable<int> ApplicationIds { get; set; } = new List<int>();

        [Display(ResourceType = typeof(ApplicationsRES), Name = "LienPrevicad")]
        public string LienPrevicad;

        [Display(ResourceType = typeof(ApplicationsRES), Name = "NbAdminsPrevicadMax")]
        public int NbAdminsPrevicadMax { get; set; }


        [Display(ResourceType = typeof(ApplicationsRES), Name = "NbAdminsAnalyseRisqueMax")]
        public int NbAdminsAnalyseRisqueMax { get; set; }

        [Display(ResourceType = typeof(ApplicationsRES), Name = "StatusCadenassage")]
        public int StatusCadenassage { get; set; }


        [Display(ResourceType = typeof(ApplicationsRES), Name = "NbLimitCadenassage")]
        public int NbLimitCadenassage { get; set; }


        [Display(ResourceType = typeof(ApplicationsRES), Name = "PeriodeEssai")]
        public bool PeriodeEssai { get; set; }


        [Display(ResourceType = typeof(ApplicationsRES), Name = "DateCadenassage")]
        public DateTime DateCadenassage { get; set; }
        

        [Display(ResourceType = typeof(ApplicationsRES), Name = "TotalCadenassage")]
        public int TotalCadenassage { get; set; }
        

        //[Display(ResourceType = typeof(ApplicationsRES), Name = "NbAdminsDocumentsMax")]
        //public int NbAdminsDocumentsMax { get; set; }

        [Display(ResourceType = typeof(ApplicationsRES), Name = "NbAuditeursMax")]
        public int NbAuditeursMax { get; set; }


        [Display(ResourceType = typeof(ApplicationsRES), Name = "NbUtilisateursPrevicad")]
        public int NbUtilisateursPrevicad { get; set; }


        [Display(ResourceType = typeof(ApplicationsRES), Name = "NbAdminUtilisateurs")]
        public int NbAdminUtilisateurs { get; set; }


        public string classMaximums { get; set; }

        public string classDefault { get; set; }
    }
}