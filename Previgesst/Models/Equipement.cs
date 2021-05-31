using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Previgesst.Models
{
    public class Equipement
    { public int EquipementId { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        [Required]
        [StringLength(250)]
        public string NomEquipement { get; set; }


        [Required]
        [StringLength(250)]
        public string NomEquipementEN { get; set; }

        [StringLength(250)]
        public string NomFichier { get; set; }

        public virtual ICollection<FicheCadenassage> FicheCadenassage { get; set; }


    }
}