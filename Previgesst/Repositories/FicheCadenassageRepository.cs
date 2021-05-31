using Previgesst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class FicheCadenassageRepository:RepositoryBase<FicheCadenassage>
    {
        public FicheCadenassageRepository(DbContext context) : base(context) { }


        public override FicheCadenassage Get(int id)
        {
            return dbSet.Include(b => b.LignesDecadenassage)
                .Include(b => b.LignesInstruction)
                .Include(b => b.Equipement)
                .Include(b => b.MaterielsRequisCadenassage)
                .Include(b=> b.Client)
                .Include(b => b.SourcesEnergieCadenassage).FirstOrDefault(b => b.FicheCadenassageId == id);
 
        }
    }
}