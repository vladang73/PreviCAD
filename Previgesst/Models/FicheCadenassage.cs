
using Previgesst.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Previgesst.Models
{
    public class FicheCadenassage
    {
        public int FicheCadenassageId { get; set; }


        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        public int EquipementId { get; set; }
        public virtual Equipement Equipement { get; set; }



        public int DepartementId { get; set; }
        public virtual Departement Departement { get; set; }


        [Required]
        [StringLength(250)]
        public string TravailAEffectuer { get; set; }


        //[Required]
        [StringLength(250)]
        public string TravailAEffectuerEN { get; set; }


        [StringLength(250)]
        public string NoFiche { get; set; }

        /*
        [StringLength(250)]
        public string NumeroEquipment { get; set; }
        */

        [StringLength(250)]
        public string ApprouvePar { get; set; }

        public DateTime? DateApproved { get; set; }


        public DateTime? DateCreation { get; set; }

        [StringLength(250)]
        public string CreatedPar { get; set; }


        public DateTime? DateUpdated { get; set; }

        [StringLength(250)]
        public string UpdatedPar { get; set; }


        //public DateTime? DateRevision { get; set; }

        public virtual ICollection<MaterielRequisCadenassage> MaterielsRequisCadenassage { get; set; }
        public virtual ICollection<SourceEnergieCadenassage> SourcesEnergieCadenassage { get; set; }

        public virtual ICollection<LigneInstruction> LignesInstruction { get; set; }

        public virtual ICollection<LigneDecadenassage> LignesDecadenassage { get; set; }

        public virtual ICollection<PhotoFicheCadenassage> PhotosFicheCadenassage { get; set; }

        public Boolean estDocumentPrevigesst { get; set; }

        public string TitreFiche { get; set; }
        public string TitreFicheEN { get; set; }

        public Boolean AfficherClient { get; set; }


        public Boolean IsApproved { get; set; }

        //public Boolean RevisionCourante { get; set; }
    }

    public class MaterielRequisCadenassage
    {
        public int MaterielRequisCadenassageId { get; set; }

        public int FicheCadenassageId { get; set; }
        public virtual FicheCadenassage FicheCadenassage { get; set; }

        public int MaterielId { get; set; }
        public virtual Materiel Materiel { get; set; }
        public int Quantite { get; set; }

    }

    public class SourceEnergieCadenassage
    {
        public int SourceEnergieCadenassageId { get; set; }
        public int FicheCadenassageId { get; set; }

        public virtual FicheCadenassage FicheCadenassage { get; set; }

        public int SourceEnergieId { get; set; }
        public virtual SourceEnergie SourceEnergie { get; set; }

    }

    public class LigneInstruction
    {
        public int LigneInstructionId { get; set; }
        public int FicheCadenassageId { get; set; }
        public virtual FicheCadenassage FicheCadenassage { get; set; }


        public decimal? NoLigne { get; set; }

        public int? InstructionId { get; set; }
        public virtual Instruction Instruction { get; set; }

       
        [StringLength(250)]
        public string TexteSupplementaireInstruction { get; set; }
       
        [StringLength(250)]

        public string TexteSupplementaireDispositif { get; set; }
       
        //[StringLength(250)]
        //public string TexteLocalisation { get; set; }

       
        public bool CocherColonneCadenas { get; set; }
        public Boolean Realiser { get; set; }

        //[StringLength(500)]
        //public string NomFichier { get; set; }

        public string TexteSupplementaireRealiser { get; set; }

        public int? PhotoFicheCadenassageId { get; set; }
        public virtual PhotoFicheCadenassage PhotoFicheCadenassage { get; set; }


        [StringLength(250)]
        public string TexteSupplementaireInstructionEN { get; set; }

        [StringLength(250)]

        public string TexteSupplementaireDispositifEN { get; set; }
    }

    public class LigneDecadenassage
    {
        public int LigneDecadenassageId { get; set; }

        public int FicheCadenassageId { get; set; }
        public virtual FicheCadenassage FicheCadenassage { get; set; }



        public decimal? NoLigne { get; set; }

        public int? InstructionId { get; set; }
        public virtual Instruction Instruction { get; set; }


        [StringLength(250)]
        public string TexteSupplementaireInstruction { get; set; }

        [StringLength(250)]

        public string TexteSupplementaireDispositif { get; set; }

        //[StringLength(250)]
        //public string TexteLocalisation { get; set; }


        public bool CocherColonneCadenas { get; set; }
        public Boolean Realiser { get; set; }

        //[StringLength(500)]
        //public string NomFichier { get; set; }

        public string TexteSupplementaireRealiser { get; set;  }

        
        public int? PhotoFicheCadenassageId { get; set; }
        public virtual PhotoFicheCadenassage PhotoFicheCadenassage { get; set; }

        [StringLength(250)]
        public string TexteSupplementaireInstructionEN { get; set; }

        [StringLength(250)]

        public string TexteSupplementaireDispositifEN { get; set; }

    }

    public class PhotoFicheCadenassage
    {
        public int PhotoFicheCadenassageId { get; set; }
        public int Rang { get; set; }
        public int FicheCadenassageId { get; set; }
        public string Localisation { get; set; }

        public string LocalisationEN { get; set; }
        public string Fichier { get; set; }
        public virtual FicheCadenassage FicheCadenassage { get; set; }

        public virtual ICollection<LigneInstruction> LignesInstruction { get; set; }
        public virtual ICollection<LigneDecadenassage> LignesDecadenassage { get; set; }
    }
}