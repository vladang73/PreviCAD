using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Previgesst.ViewModels
{
    public class InstructionViewModel
    {

        public int InstructionId { get; set; }

        [Required]
        [DisplayName("Texte")]
        [MaxLength(500, ErrorMessage = "Le texte ne doit pas dépasser {1} caractères.")]
        [StringLength(500)]

        public string TexteInstruction { get; set; }

        [Required]
        [DisplayName("Texte EN")]
        [MaxLength(500, ErrorMessage = "Le texte ne doit pas dépasser {1} caractères.")]
        [StringLength(500)]

        public string TexteInstructionEN { get; set; }


        [Required]
        [StringLength(100)]
        [MaxLength(100, ErrorMessage = "L'identificateur ne doit pas dépasser {1} caractères.")]
        [DisplayName("Identificateur")]

        public string Identificateur { get; set; }


        [Required]
        [StringLength(100)]
        [MaxLength(100, ErrorMessage = "L'identificateur ne doit pas dépasser {1} caractères.")]
        [DisplayName("Identificateur EN")]

        public string IdentificateurEN { get; set; }

        [Required]
        [DisplayName("Dispositif")]
        public int DispositifId { get; set; }



        [Required]
        [DisplayName("Accessoire")]

        public int AccessoireId { get; set; }

   public string Accessoire { get; set; }
        public string Dispositif { get; set; }

        public bool Suppressible { get; set; }
    
    }
}