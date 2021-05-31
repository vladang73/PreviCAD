using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Previgesst.Models
{
    public class LigneRegistre
    {
        public int LigneRegistreId { get; set; }

        public int FicheCadenassageId { get; set; }
        public virtual FicheCadenassage FicheCadenassage { get; set; }

        public int EmployeRegistreId { get; set; }

        public virtual EmployeRegistre EmployeRegistre { get; set; }

        [Required]
        public DateTime DateDebut { get; set; }

        public int LuEtEffectue { get; set; }

        public DateTime? DateFin { get; set; }
        public int LuEtDecadenasse { get; set; }
        public string Note { get; set; }

        public DateTime? FinPrevue { get; set; }

        public string NomAuditeur { get; set; }
        public string Rep1 { get; set; }
        public string Rep2 { get; set; }
        public string Rep3 { get; set; }
        public string Rep4 { get; set; }

        public string NoteAudit { get; set; }

        public string BonDeTravail { get; set; }

   }

}