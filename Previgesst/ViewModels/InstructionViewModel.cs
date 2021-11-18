using Previgesst.Ressources;
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
        [Display(ResourceType = typeof(InstructionRES), Name = "Texte")]
        [MaxLength(500, ErrorMessageResourceType = typeof(InstructionRES), ErrorMessageResourceName = "TexteMaxLength")]
        [StringLength(500)]
        public string TexteInstruction { get; set; }


        [Required]
        [Display(ResourceType = typeof(InstructionRES), Name = "TexteEn")]
        [MaxLength(500, ErrorMessageResourceType = typeof(InstructionRES), ErrorMessageResourceName = "TexteMaxLengthEn")]
        [StringLength(500)]
        public string TexteInstructionEN { get; set; }


        [Required]
        [StringLength(100)]
        [Display(ResourceType = typeof(InstructionRES), Name = "Identificateur")]
        [MaxLength(100, ErrorMessageResourceType = typeof(InstructionRES), ErrorMessageResourceName = "IdentificateurMaxLength")]
        public string Identificateur { get; set; }


        [Required]
        [StringLength(100)]
        [Display(ResourceType = typeof(InstructionRES), Name = "IdentificateurEN")]
        [MaxLength(100, ErrorMessageResourceType = typeof(InstructionRES), ErrorMessageResourceName = "IdentificateurMaxLengthEN")]
        public string IdentificateurEN { get; set; }


        [Required]
        [Display(ResourceType = typeof(InstructionRES), Name = "Dispositif")]
        public int DispositifId { get; set; }



        [Required]
        [Display(ResourceType = typeof(InstructionRES), Name = "Accessoire")]
        public int AccessoireId { get; set; }


        [Display(ResourceType = typeof(InstructionRES), Name = "Accessoire")]
        public string Accessoire { get; set; }


        [Display(ResourceType = typeof(InstructionRES), Name = "Dispositif")]
        public string Dispositif { get; set; }

        public bool Suppressible { get; set; }
    }
}