using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Previgesst.Models
{
    public class SavedInstructionNote
    {
        [Key]
        public int PKId { get; set; }


        [Required]
        public string Notes { get; set; }

        [Required]
        public int FicheCadenassageId { get; set; }
    }
}