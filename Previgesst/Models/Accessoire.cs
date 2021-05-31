﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Previgesst.Models
{
    public class Accessoire
    {
        public int AccessoireId { get; set; }
        [Required]
        [StringLength(250)]

        public string Description { get; set; }

        [Required]
        [StringLength(250)]
        public string DescriptionEN { get; set; }
        public virtual ICollection<Instruction> Instructions { get; set; }
    }
}