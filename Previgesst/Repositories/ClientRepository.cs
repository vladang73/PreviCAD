using Previgesst.Models;
using Previgesst.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class ClientRepository : RepositoryBase<Client>
    {
        public ClientRepository(DbContext context) : base (context) {
            
        }

        public Client getClientByIdentificateur ( string Identificateur)
        {
            return dbSet.Where(x => x.Identificateur.Trim() == Identificateur).Include(x => x.Utilisateurs).FirstOrDefault();
        }

        public override Client Get(int id)
        {
            return dbSet.Where(x => x.ClientId == id).Include(x => x.EmployesRegistre).Include(x => x.Utilisateurs).Include(x => x.ClientsApplicationPrevi).FirstOrDefault();
        }
    }
}