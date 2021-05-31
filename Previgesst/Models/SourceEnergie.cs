using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Previgesst.Models
{
    public class SourceEnergie
    {
        public int SourceEnergieId { get; set; }
        [Required]
        [StringLength(250)]

        public string Description { get; set; }
        [Required]
        [StringLength(250)]
        public string DescriptionEN { get; set; }
        public virtual ICollection<SourceEnergieCadenassage> SourceEnergieCadenassage { get; set; }
    }
}