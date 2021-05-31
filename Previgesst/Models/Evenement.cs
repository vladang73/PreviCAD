using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Previgesst.Models
{
    public class Evenement
    {
        public int EvenementId { get; set; }
        [Required]
        [StringLength(250)]
        public string Description { get; set; }

        [Required]
        [StringLength(250)]
        public string DescriptionEN { get; set; }
        public virtual ICollection<LigneAnalyseRisque> LignesAnalysesRisque { get; set; }


    }
}