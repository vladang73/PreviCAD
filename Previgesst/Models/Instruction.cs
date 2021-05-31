using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Previgesst.Models
{
    public class Instruction
    { public int InstructionId { get; set; }

        [Required]
        [StringLength(500)]

        public string TexteInstruction { get; set; }

        public string TexteInstructionEN { get; set; }

        [Required]
        [StringLength(100)]
        public string Identificateur { get; set; }

        [Required]
        [StringLength(100)]
        public string IdentificateurEN { get; set; }

        public int DispositifId { get; set; }
        public virtual Dispositif Dispositif { get; set; }


        public int AccessoireId { get; set; }

        public virtual Accessoire Accessoire { get; set; }


        public virtual ICollection<LigneInstruction> LignesInstruction { get; set; }




    }
}