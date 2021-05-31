using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Previgesst.Models
{
    public class TypeReduction
    { public int TypeReductionId { get; set; }

        [Required]
        [StringLength(250)]
        public string Description { get; set; }
    }
}