using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Previgesst.ViewModels
{
    public class LigneAnalyseRapport
    { public int AnalyseRisqueId { get; set; }
        public string NoReference { get; set; }
        public decimal Rang { get; set; }
        public string Equipement { get; set; }
        public string Operation { get; set; }
        public string Zone { get; set; }

        public string Evenement { get; set; }
        public string Phenomene { get; set; }
        public string Situation { get; set; }

        public string Dommage { get; set; }

        public int? GraviteAvant { get; set; }
        public int? GraviteApres { get; set; }
        public int? FrequenceAvant { get; set; }

        public int? FrequenceApres { get; set; }
        public int? ProbabiliteAvant { get; set; }
        public int? ProbabiliteApres { get; set; }

        public int? PossibiliteAvant { get; set; }
        public int? PossibiliteApres { get; set; }
        public int IndiceFinalAvant { get; set; }
        public int IndiceFinalApres { get; set; }


        public int NbPersonnesExposees { get; set; }

        public string SystemeCommandeAvant { get; set; }

        public string Reglement { get; set; }
        public string Mesure { get; set; }

        public string SystemeCommandeInstalles { get; set; }
        public string ConformiteAuNormes { get; set; }

        public string PlanAction { get; set; }
        public string ResponsableAnalyse { get; set; }
        public DateTime? DateDeRealisation { get; set; }
        public string EtatId { get; set; }
    }
}