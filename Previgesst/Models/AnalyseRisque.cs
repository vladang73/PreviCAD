using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Previgesst.Models
{
    public class AnalyseRisque
    {
        public int AnalyseRisqueId { get; set; }
        [Required]
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }

        [Required]
        [StringLength(250)]

        public string Description { get; set; }


        [Required]
        [StringLength(250)]
        public string Createur { get; set; }

        public DateTime DateCreation { get; set; }


        [StringLength(250)]
        public string UserMAJ { get; set; }

        public DateTime? DateMiseAJour { get; set; }

        public Boolean AfficherChezClient { get; set; }

        [StringLength(250)]
        public string Participants { get; set; }

        public Boolean? estDocumentPrevigesst { get; set; }


        [Required]
        [StringLength(250)]
        public string Equipement { get; set; }

        public virtual ICollection<LigneAnalyseRisque> LignesAnalyseRisque { get; set; }


    }
}