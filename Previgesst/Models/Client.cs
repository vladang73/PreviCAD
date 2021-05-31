using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Previgesst.Models
{
    public class Client
    { public int ClientId { get; set; }

        [Required]
        [StringLength(250)]
        public string Nom { get; set; }
        [Required]
        [StringLength(250)]

        [Index("IX_Identificateur", 1, IsUnique = true)]
        public string Identificateur { get; set; }

        public virtual ICollection<AnalyseRisque> AnalysesRisques { get; set; }

        public virtual ICollection<FicheCadenassage> FichesCadenassage { get; set; }

        public Boolean Actif { get; set; }

        public string Logo { get; set; }

        public string Thumb { get; set; }

        public virtual ICollection<Utilisateur> Utilisateurs { get; set; }

        public virtual ICollection<ClientApplicationPrevi> ClientsApplicationPrevi { get; set; }

        public virtual ICollection<DocumentClient> DocumentsClient { get; set; }

        public virtual ICollection<Equipement> Equipements { get; set; }

        public virtual ICollection<Departement> Departements { get; set; }

        public virtual ICollection<EmployeRegistre> EmployesRegistre { get; set; }

        public Guid IdentificateurUnique { get; set; }

        public int NbAdminsPrevicadMax { get; set; }

        public int NbAdminsAnalyseRisqueMax { get; set; }

        public int StatusCadenassage { get; set; }

        public DateTime DateCadenassage { get; set; }

        public int TotalCadenassage { get; set; }

        public int NbAdminsDocumentsMax { get; set; }

        public int NbLimitCadenassage { get; set; }

        public int PeriodeEssai { get; set; }

        public int NbAuditeursMax { get; set; }

        public int NbUtilisateursPrevicad { get; set; }

        public int NbAdminUtilisateurs { get; set; }

    }
}