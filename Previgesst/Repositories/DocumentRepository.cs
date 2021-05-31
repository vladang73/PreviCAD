using Previgesst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class DocumentRepository:RepositoryBase<Document>
    {

        public DocumentRepository(DbContext context) : base(context) { }

        public override Document Get(int id)
        {
            return dbSet.Include(b => b.Section).FirstOrDefault(b => b.DocumentId == id);

        }

    }
}