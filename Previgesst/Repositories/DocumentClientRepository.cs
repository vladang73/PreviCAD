using Previgesst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class DocumentClientRepository:RepositoryBase<DocumentClient>
    {
        public DocumentClientRepository(DbContext context) : base(context) { }

        public override DocumentClient Get(int id)
        {
            return dbSet.Include(Z => Z.Client).Where(x => x.DocumentClientId == id).FirstOrDefault();
        }

    }
}