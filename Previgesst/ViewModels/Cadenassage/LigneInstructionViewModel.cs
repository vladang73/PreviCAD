using Previgesst.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Previgesst.Ressources.Cadenassage;

namespace Previgesst.ViewModels
{
    public class LigneInstructionViewModel
    {
        public int LigneInstructionId { get; set; }

        public int FicheCadenassageId { get; set; }
        public virtual FicheCadenassage FicheCadenassage { get; set; }

        [Required]      
        [Display(ResourceType = typeof(CadLigneCadRES), Name = "NoLigne")]
        public decimal NoLigne { get; set; }


        [Required]
        [Display(ResourceType = typeof(CadLigneCadRES), Name = "Instruction")]
        public int? InstructionId { get; set; }
        public virtual Instruction Instruction { get; set; }

   
        
        [Display(ResourceType = typeof(CadLigneCadRES), Name = "TexteSUPInstruction")]
        [MaxLength(250, ErrorMessageResourceType = typeof(CadLigneCadRES), ErrorMessageResourceName = "ErrTexteSUPInstruction")]   
        [StringLength(250)]
        public string TexteSupplementaireInstruction { get; set; }




        [Display(ResourceType = typeof(CadLigneCadRES), Name = "TexteSUPInstructionEN")]
        [MaxLength(250, ErrorMessageResourceType = typeof(CadLigneCadRES), ErrorMessageResourceName = "ErrTexteSUPInstructionEN")]
        [StringLength(250)]
        public string TexteSupplementaireInstructionEN { get; set; }


        [Display(ResourceType = typeof(CadLigneCadRES), Name = "TexteSUPDispositif")]
        [MaxLength(250, ErrorMessageResourceType = typeof(CadLigneCadRES), ErrorMessageResourceName = "ErrTexteSUPDispositif")]

        [StringLength(250)]
        public string TexteSupplementaireDispositif { get; set; }


        [Display(ResourceType = typeof(CadLigneCadRES), Name = "TexteSUPDispositifEN")]
        [MaxLength(250, ErrorMessageResourceType = typeof(CadLigneCadRES), ErrorMessageResourceName = "ErrTexteSUPDispositifEN")]
        [StringLength(250)]
        public string TexteSupplementaireDispositifEN { get; set; }


        [DisplayName("Texte supplémentaire Localisation")]
        [MaxLength(250, ErrorMessage = "Le champ 'Texte supplémentaire Localisation' par ne doit pas dépasser {1} caractères.")]
        [StringLength(250)]
        public string TexteLocalisation { get; set; }


        [DisplayName("Texte supplémentaire Réaliser")]
        [MaxLength(250, ErrorMessage = "Le champ 'Texte supplémentaire Réaliser' par ne doit pas dépasser {1} caractères.")]
        [StringLength(250)]
        public string TexteRealiser { get; set; }




        [Display(ResourceType = typeof(CadLigneCadRES), Name = "Cadenasser")]
        public bool CocherColonneCadenas { get; set; }


        [Display(ResourceType = typeof(CadLigneCadRES), Name = "Realiser")]
        public Boolean Realiser { get; set; }
  
        public Boolean Suppressible { get; set; }

        public string TexteInstruction { get; set; }
        public string TexteDispositif { get; set; }
        public string TexteAccessoire { get; set; }


        [Display(ResourceType = typeof(CadLigneCadRES), Name = "Telecharger" )]
        public string Thumbnail { get; set; }

        [Display(ResourceType = typeof(CadLigneCadRES), Name = "Image")]
        public int? PhotoFicheCadenassageId { get; set; }
        public virtual PhotoFicheCadenassage PhotoFicheCadenassage { get; set; }
     
        public string NoFicheCadenassage { get; set; }
    }
}