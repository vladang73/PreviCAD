using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Previgesst.Models
{
    public class SavedInstruction
    {
        [Key]
        public int PKId { get; set; }

        [Required]
        public int InstructionId { get; set; }

        [Required]
        [StringLength(50)]

        public string InstructionType { get; set; }

        [Required]
        public string StepStatus { get; set; }

        [Required]
        public int FicheCadenassageId { get; set; }
    }
}