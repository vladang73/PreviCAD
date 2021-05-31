using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Previgesst.Models
{
    public class Equipement
    {
        public int EquipementId { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        [Required]
        [StringLength(250)]
        public string NomEquipement { get; set; }


        [Required]
        [StringLength(250)]
        public string NomEquipementEN { get; set; }

        [Required]
        [StringLength(250)]
        public string NumeroEquipement { get; set; }

        [StringLength(250)]
        public string NomFichier { get; set; }

        [StringLength(250)]
        public string Model { get; set; }

        [StringLength(250)]
        public string Manufacturer { get; set; }

        public string Task { get; set; }

        public bool? RiskAnalysis { get; set; }

        public bool? LockoutProcedure { get; set; }

        public bool? SafeWorkProcedure { get; set; }

        //public string Accessories { get; set; }

        //public string Energy { get; set; }

        //public string Deposit { get; set; }

        //public string Nomenclature { get; set; }

        public string NumberOfSerie { get; set; }

        public int? YearOfProduction { get; set; }

        public string Function { get; set; }

        public virtual ICollection<FicheCadenassage> FicheCadenassage { get; set; }
        public virtual ICollection<EquipementArticulo> EquipementArticulo { get; set; }

    }
}