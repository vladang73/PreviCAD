using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Previgesst.Sessions
{
    public class sessionInfo
    {
        public string NomUtilisateur { get; set; }
        public int ClientId { get; set; }
        public int RoleId { get; set; }
 
        public string Nom { get; set; }
        public int UtilisateurId { get; set; }

        public Boolean AdmPrevicad { get; set; }
        public Boolean AdmAnalyseRisque { get; set; }
      //  public Boolean AdmDocuments { get; set; }

        public Boolean ROPrevicad { get; set; }
        public Boolean ROAnalyseRisque { get; set; }
        public Boolean RODocuments { get; set; }

        public Boolean Auditeur { get; set; }

        public Boolean AdmUtilisateurs { get; set; }

        public Boolean NotificationDebutCad { get; set; }


    }

    
}