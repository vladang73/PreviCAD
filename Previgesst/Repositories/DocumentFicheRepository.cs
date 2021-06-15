using Previgesst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class DocumentFicheRepository : RepositoryBase<DocumentFiche>
    {
        public DocumentFicheRepository(DbContext context) : base(context) { }

        public override DocumentFiche Get(int id)
        {
            return dbSet.Include(Z => Z.FicheCadenassage).Where(x => x.DocumentFicheId == id).FirstOrDefault();
        }

    }
}