using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Previgesst.Models
{
    public class Probabilite
    {

        public int ProbabiliteId { get; set; }

        [Required]
        public int No { get; set; }

        [Required]
        [StringLength(5)]
        public string Niveau { get; set; }
        [Required]
        [StringLength(250)]
        public string Explication { get; set; }

        public int? Valeur { get; set; }

        [InverseProperty("ProbabiliteAvant")]
        public virtual ICollection<LigneAnalyseRisque> ProbabiliteAvant { get; set; }

        [InverseProperty("ProbabiliteApres")]
        public virtual ICollection<LigneAnalyseRisque> ProbabiliteApres { get; set; }
    }
}