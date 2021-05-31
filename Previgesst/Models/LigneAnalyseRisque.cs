using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Previgesst.Models
{
    public class LigneAnalyseRisque
    {
        public int LigneAnalyseRisqueId { get; set; }
        public int AnalyseRisqueId { get; set; }

        public virtual AnalyseRisque AnalyseRisque { get; set; }


        //[Required]
        //[StringLength(250)]
        //public string NoReference { get; set; }

        [Required]
        public decimal Rang { get; set; }

        //[Required]
        //[StringLength(250)]
        //public string Equipement { get; set; }

        [Required]
        [StringLength(250)]
        public string Operation { get; set; }

        [Required]
        [StringLength(250)]
        public string Zone { get; set; }


        public int? PhenomeneId { get; set; }
        public virtual Phenomene Phenomene { get; set; }

        public int? SituationId { get; set; }
        public virtual Situation Situation { get; set; }

        public int? EvenementId { get; set; }
        public virtual Evenement Evenement { get; set; }

        [ForeignKey("GraviteAvant")]
        public int? GraviteAvantId { get; set; }
        public virtual Gravite GraviteAvant { get; set; }

        [ForeignKey("FrequenceAvant")]
        public int? FrequenceAvantId { get; set; }
        public virtual Frequence FrequenceAvant { get; set; }

        [ForeignKey("ProbabiliteAvant")]
        public int? ProbabiliteAvantId { get; set; }
        public virtual Probabilite ProbabiliteAvant { get; set; }

        [ForeignKey("PossibiliteAvant")]
        public int? PossibiliteAvantId { get; set; }
        public virtual Possibilite PossibiliteAvant { get; set; }

        public int IndiceFinalAvant { get; set; }
        public int NbPersonnesExposees { get; set; }

      
        [StringLength(250)]
        public string SystemeCommandeAvant { get; set; }

 

        public string ReglesEtNormes { get; set; }

        [StringLength(500)]
        public string MesurePrevention { get; set; }


        [ForeignKey("GraviteApres")]
        public int? GraviteApresId { get; set; }
        public virtual Gravite GraviteApres { get; set; }

        [ForeignKey("FrequenceApres")]
        public int? FrequenceApresId { get; set; }
        public virtual Frequence FrequenceApres { get; set; }

        [ForeignKey("ProbabiliteApres")]
        public int? ProbabiliteApresId { get; set; }
        public virtual Probabilite ProbabiliteApres { get; set; }

        [ForeignKey("PossibiliteApres")]
        public int? PossibiliteApresId { get; set; }
        public virtual Possibilite PossibiliteApres { get; set; }

        public int IndiceFinalApres { get; set; }

       
        [StringLength(250)]
        public string SystemeCommandeInstalles { get; set; }

       
         
        public Boolean ConformiteAuxNormes { get; set; }


        public Boolean estDocumentPrevigesst { get; set; }

        [StringLength(500)]
        public string PlanAction { get; set; }

        public int? DommagePossibleId { get; set; }
        public virtual DommagePossible DommagePossible { get; set; }


        [StringLength(500)]
        public string ResponsableAnalyse { get; set; }

        public DateTime? DateDeRealisation { get; set; }

        
        public int? EtatId { get; set; }
        public virtual Etat Etat { get; set; }
    }
}