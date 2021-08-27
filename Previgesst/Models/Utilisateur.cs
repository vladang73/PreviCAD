using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Previgesst.Models
{
    public class Utilisateur
    {
        public int UtilisateurId { get; set; }

        [Required]
        [StringLength(250)]
        public string NomUtilisateur { get; set; }

        [Required]
        [StringLength(500)]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        public string Nom { get; set; }

        //[Required]

        //public int RoleUtilisateurId { get; set; }

        public Boolean Actif { get; set; }

        //public virtual RoleUtilisateur RoleUtilisateur { get; set; }

        [Required]

        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        [Required]
        [StringLength(250)]
        public string Courriel { get; set; }

        public Boolean AdmPrevicad { get; set; }
        public Boolean AdmAnalyseRisque { get; set; }
        //public Boolean AdmDocuments { get; set; }

        public Boolean ROPrevicad { get; set; }
        public Boolean ROAnalyseRisque { get; set; }
        public Boolean RODocuments { get; set; }

        public Boolean Auditeur { get; set; }

        public Boolean AdmUtilisateurs { get; set; }

        public Boolean NotificationDebutCad { get; set; }
        
        public Boolean NotificationNonConformite { get; set; }
    }
}