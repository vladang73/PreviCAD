using Previgesst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class UtilisateurRepository: RepositoryBase <Utilisateur>
    {
        public UtilisateurRepository(DbContext context) : base(context) { }


        public override Utilisateur Get(int id)
        {
            return dbSet.Where(x => x.UtilisateurId == id).FirstOrDefault();
        }
        public List<Utilisateur> GetAllUsersForClient(int clientId)
        {
            return dbSet.Where(x => x.ClientId == clientId).ToList();
        }
    }
}