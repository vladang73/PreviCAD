using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Previgesst.Models
{
    public class Frequence
    {
        public int FrequenceId { get; set; }
        [Required]
        public int No { get; set; }
        [Required]
        [StringLength(5)]
        public string Niveau { get; set; }
        [Required]
        [StringLength(250)]
        public string Explication { get; set; }

        public int? Valeur { get; set; }

        [InverseProperty("FrequenceAvant")]
        public virtual ICollection<LigneAnalyseRisque> FrequenceAvant { get; set; }

        [InverseProperty("FrequenceApres")]
        public virtual ICollection<LigneAnalyseRisque> FrequenceApres { get; set; }
    }
}