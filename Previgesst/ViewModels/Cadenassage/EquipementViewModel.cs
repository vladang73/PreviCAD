using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Previgesst.Ressources.Cadenassage;

namespace Previgesst.ViewModels
{
    public class EquipementViewModel
    {
        public int EquipementId { get; set; }

        [Required]
        [StringLength(250)]
        [Display(ResourceType = typeof(CadEquipRES), Name = "NomFR")]
        public string NomEquipement { get; set; }

        [Required]
        [StringLength(250)]
        [Display(ResourceType = typeof(CadEquipRES), Name = "NomEN")]
        public string NomEquipementEN { get; set; }

        [Required]
        [StringLength(250)]
        [Display(ResourceType = typeof(CadEquipRES), Name = "Num")]
        public string NumeroEquipment { get; set; }

        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public string Task { get; set; }
        public bool RiskAnalysis { get; set; }
        public bool LockoutProcedure { get; set; }
        public bool SafeWorkProcedure { get; set; }

        //public string Accessories { get; set; }

        //public string Energy { get; set; }

        //public string Deposit { get; set; }

        //public string Nomenclature { get; set; }

        public string NumberOfSerie { get; set; }

        public int? YearOfProduction { get; set; }

        public string Function { get; set; }

        public int ClientId { get; set; }
        public string NomClient { get; set; }
        public string Thumbnail { get; set; }
        public bool Suppressible { get; set; }

        public string NomFichier { get; set; }
        public string QRCode { get; set; }


    }
}