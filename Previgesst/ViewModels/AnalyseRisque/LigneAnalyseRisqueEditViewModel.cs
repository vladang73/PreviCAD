using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Previgesst.Helpers;
using Previgesst.Ressources.Analyse;

namespace Previgesst.ViewModels
{
   
    public class LigneAnalyseRisqueEditViewModel
    {
        public int IdLigneAnalyseRisqueEditor { get; set; }


        //[Required]
        //[StringLength(250)]
        //[Display(ResourceType = typeof(ARLignesRES), Name = "NoREF")]
        //public string NoReference { get; set; }

        [Required]
        [Display(ResourceType = typeof(ARLignesRES), Name = "Rang")]
        public decimal Rang { get; set; }

        
        //[Required]
        //[StringLength(250)]
        //[Display(ResourceType = typeof(ARLignesRES), Name = "Equipement")]
        //public string Equipement { get; set; }

        [Required]
        [StringLength(250)]
      
        [Display(ResourceType = typeof(ARLignesRES), Name = "OPTask")]
        public string Operation { get; set; }

        [Required]
        [StringLength(250)]
        [Display(ResourceType = typeof(ARLignesRES), Name = "ZoneDangereuse")]
        public string Zone { get; set; }

    
        [Display(ResourceType = typeof(ARLignesRES), Name = "PhenomeneDangereux")]
        public int? PhenomeneId { get; set; }
        public string PhenomeneDescription { get; set; }

        [Display(ResourceType = typeof(ARLignesRES), Name = "DangerSituation")]
     
        public int? SituationId { get; set; }

        
        [Display(ResourceType = typeof(ARLignesRES), Name = "EvenementPotentiel")]
        public int? EvenementId { get; set; }

        [Display(ResourceType = typeof(ARLignesRES), Name = "DommagePossible")]
        public int? DommagePossibleId { get; set; }

        
        [Display(ResourceType = typeof(ARLignesRES), Name = "IndiceAvant")]

        [Range(0, int.MaxValue, ErrorMessageResourceType =typeof( ARLignesRES), ErrorMessageResourceName = "IndiceRisqueErreur" )]
        public int IndiceFinalAvant { get; set; }



        [Display(ResourceType = typeof(ARLignesRES), Name = "IndiceApres")]
        [Range(0, int.MaxValue, ErrorMessageResourceType = typeof(ARLignesRES), ErrorMessageResourceName = "ErrIndiceRisque")]
        public int IndiceFinalApres { get; set; }



        [Display(ResourceType = typeof(ARLignesRES), Name = "NbPersonnes")]
        [Range(0, int.MaxValue, ErrorMessageResourceType = typeof(ARLignesRES), ErrorMessageResourceName = "ErrNbPersonnes")]
        public int NbPersonnesExposees { get; set; }

        // Can't parse a title for IndiceFinaleAvantTEXTE and IndiceFinalApresTEXTE
        public string IndiceFinalAvantTEXTE 
        {
            get
            {
               return IndiceHelper.getText(IndiceFinalAvant);
            }
           
        }

        public string IndiceFinalApresTEXTE
        {
            get
            {
                return IndiceHelper.getText(IndiceFinalApres);
            }

        }

        [StringLength(250)]
      
        [Display(ResourceType = typeof(ARLignesRES), Name = "Categorie")]
        public string SystemeCommandeAvant { get; set; }

        
        [Display(ResourceType = typeof(ARLignesRES), Name = "Reglement")]
        public string ReglesEtNormes { get; set; }


      
        [Display(ResourceType = typeof(ARLignesRES), Name = "MesurePrevention")]
        public string MesurePrevention { get; set; }

         
        [StringLength(250)]
        [Display(ResourceType = typeof(ARLignesRES), Name = "CategorieSystemeCommande")]
        public string SystemeCommandeInstalles { get; set; }




        [Display(ResourceType = typeof(ARLignesRES), Name = "Conformite")]
        public bool ConformiteAuNormes { get; set; }


        [Display(ResourceType = typeof(ARLignesRES), Name = "Frequence")]
        public int? FrequenceApresId { get; set; }

        [Display(ResourceType = typeof(ARLignesRES), Name = "Frequence")]
        public int? FrequenceAvantId { get; set; }

        [Display(ResourceType = typeof(ARLignesRES), Name = "Gravite")]
        public int? GraviteApresId { get; set; }

        [Display(ResourceType = typeof(ARLignesRES), Name = "Gravite")]
        public int? GraviteAvantId { get; set; }

        [Display(ResourceType = typeof(ARLignesRES), Name = "PossibiliteEvitement")]
        public int? PossibiliteApresId { get; set; }

        [Display(ResourceType = typeof(ARLignesRES), Name = "PossibiliteEvitement")]
        public int? PossibiliteAvantId { get; set; }

        [Display(ResourceType = typeof(ARLignesRES), Name = "ProbabiliteOccurence")]
        public int? ProbabiliteApresId { get; set; }

        [Display(ResourceType = typeof(ARLignesRES), Name = "ProbabiliteOccurence")]
        public int? ProbabiliteAvantId { get; set; }

        [Display(ResourceType = typeof(ARLignesRES), Name = "PlanAction")]
        public string PlanAction { get; set; }

        [Display(ResourceType = typeof(ARLignesRES), Name = "ResponsableAnalyse")]
        public string ResponsableAnalyse { get; set; }

        [Display(ResourceType = typeof(ARLignesRES), Name = "DateDeRealisation")]
        public DateTime? DateDeRealisation { get; set; }

        [Display(ResourceType = typeof(ARLignesRES), Name = "Etat")]
        public int? EtatId { get; set; }
        public string EtatDescription { get; set; }
    }
}