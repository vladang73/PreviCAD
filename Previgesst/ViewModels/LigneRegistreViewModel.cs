using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Previgesst.Ressources.Cadenassage;
using Previgesst.Ressources.Previcad;

namespace Previgesst.ViewModels
{
    public class LigneRegistreViewModel
    {
        public int LigneRegistreId { get; set; }
        public string NoEmploye { get; set; }
        public string Nom { get; set; }
        public string NomDepartement { get; set; }

        [Display(ResourceType = typeof(PrevFicheRES), Name = "Debut")]
        public DateTime DateDebut { get; set; }

        public bool LuEtEffectue { get; set; }
        public bool LuEtDecadenasse { get; set; }


        [Display(ResourceType = typeof(PrevFicheRES), Name = "Fin")]
        public DateTime? DateFin { get; set; }
       
        public string NoCadenas { get; set; }

        [Display(ResourceType = typeof(PrevFicheRES), Name = "Details")]
       
        public string Note { get; set; }

        public bool Termine { get; set; }
        public string NomEquipement { get; set; }
        public string NoFicheCadenassage { get; set; }

        [Display(ResourceType = typeof(PrevFicheRES), Name = "FinPrevue")]
        public DateTime? FinPrevue { get; set; }

        public int EmployeRegistreId { get; set; }
        public int FicheCadenassageId { get; set; }

        public string TitreFiche { get; set; }
        public string TravailAEffectuer { get; set; }
        public List<string> TexteMateriel { get; set; }


        //section d'audit

        public string NomAuditeur { get; set; }

  
        [Display(ResourceType = typeof(CadAuditRES), Name = "Rep1")]
        [StringLength(3)]
        public string Rep1 { get; set; }

        [Display(ResourceType = typeof(CadAuditRES), Name = "Rep2")]
        [StringLength(3)]
        public string Rep2 { get; set; }
        [StringLength(3)]
        [Display(ResourceType = typeof(CadAuditRES), Name = "Rep3")]
        public string Rep3 { get; set; }
        [Display(ResourceType = typeof(CadAuditRES), Name = "Rep4")]
        [StringLength(3)]
        public string Rep4 { get; set; }

        [Display(ResourceType = typeof(CadAuditRES), Name = "Note")]
        public string NoteAudit { get; set; }

        [Display(ResourceType = typeof(PrevFicheRES), Name = "BonDeTravail")]
        public string BonDeTravail { get; set; }

        public int EquipementId { get; set; }
    }
}