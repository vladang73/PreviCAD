using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Previgesst.Models
{
    public class RoleUtilisateur
    {
        public int RoleUtilisateurId { get; set; }
        public string Nom { get; set; }

        public virtual ICollection<Utilisateur> Utilisateurs { get; set; }
    }
}