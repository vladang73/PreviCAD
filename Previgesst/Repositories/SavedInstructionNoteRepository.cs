using Previgesst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class SavedInstructionNoteRepository : RepositoryBase<SavedInstructionNote>
    {
        public SavedInstructionNoteRepository(DbContext context) : base(context)
        {
        }

        public override SavedInstructionNote Get(int id)
        {
            return dbSet.FirstOrDefault(b => b.FicheCadenassageId == id);
        }
    }

}