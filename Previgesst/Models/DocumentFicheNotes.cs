using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Previgesst.Models
{
    public class DocumentFicheNote
    {
        [Key]
        public int FicheCadenassageId { get; set; }

        [ForeignKey("FicheCadenassageId")]
        public virtual FicheCadenassage FicheCadenassage { get; set; }

        public string Notes { get; set; }
    }
}