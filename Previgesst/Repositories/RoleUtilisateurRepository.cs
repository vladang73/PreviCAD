using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class RoleUtilisateurRepository:RepositoryBase<Previgesst.Models.RoleUtilisateur>
    {
        public RoleUtilisateurRepository(DbContext context) : base(context) { }

    }
}