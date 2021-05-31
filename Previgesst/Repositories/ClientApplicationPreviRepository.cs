using Previgesst.Models;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class ClientApplicationPreviRepository:RepositoryBase<ClientApplicationPrevi>
    {
        public ClientApplicationPreviRepository(DbContext context) : base(context) { }

        public override ClientApplicationPrevi Get(int id)
        {
            return dbSet.Include(b=>b.Client).Include(b=>b.ApplicationPrevi).Where(b => b.ApplicationPreviId == id).FirstOrDefault();

        }


        private void SupprimerApplicationsClient ( int clientId)
        {
            var liste = dbSet.Where(x => x.ClientId == clientId).Select(x => x.ClientApplicationPreviId).ToList();
            foreach (var i in liste)
                this.Delete(i);
            this.SaveChanges();

        }

        public void UpdateApplications(ClientEditDetailsViewModel item)
        {


            SupprimerApplicationsClient(item.ClientId);

            foreach (int value in item.ApplicationIds)
            {
                this.Add(new ClientApplicationPrevi { ApplicationPreviId = value, ClientId = item.ClientId });
            }
            this.SaveChanges();


        }

        public List<int> getClientApplications( int clientId)
        {
            return dbSet.Where(x => x.ClientId == clientId).Select(x => x.ApplicationPreviId).ToList();
        }
    }
}