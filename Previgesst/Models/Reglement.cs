using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Previgesst.Models
{
    public class Reglement
    { public int ReglementId { get; set; }

        [Required]
        [StringLength(250)]
        public string Description { get; set; }

    //    public virtual ICollection<LigneAnalyseRisque> LigneAnalyseRisque { get; set; }

    }
}