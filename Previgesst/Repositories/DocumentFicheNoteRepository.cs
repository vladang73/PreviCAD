using Previgesst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class DocumentFicheNoteRepository : RepositoryBase<DocumentFicheNote>
    {
        public DocumentFicheNoteRepository(DbContext context) : base(context) { }

        public override DocumentFicheNote Get(int id)
        {
            return dbSet.Where(x => x.FicheCadenassageId == id).FirstOrDefault();
        }
    }
}