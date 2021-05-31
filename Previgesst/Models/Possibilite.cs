using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Previgesst.Models
{
    public class Possibilite
    {

        public int PossibiliteId { get; set; }

        [Required]
        public int No { get; set; }

        [Required]
        [StringLength(5)]


   

        public string Niveau { get; set; }
        [Required]
        [StringLength(400)]
        public string Explication { get; set; }
     public int? Valeur { get; set; }
        [InverseProperty("PossibiliteAvant")]
        public virtual ICollection<LigneAnalyseRisque> PossibiliteAvant { get; set; }

        [InverseProperty("PossibiliteApres")]
        public virtual ICollection<LigneAnalyseRisque> PossibiliteApres { get; set; }
    }
}