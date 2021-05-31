using Previgesst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class LigneInstructionRepository:RepositoryBase<LigneInstruction>
    {
        private FicheCadenassageRepository ficheCadenassageRepository;

        public LigneInstructionRepository(DbContext context, FicheCadenassageRepository ficheCadenassageRepository) : base(context) {

            this.ficheCadenassageRepository = ficheCadenassageRepository;
        }

        public void SupprimerDeFiche(int FicheCadenassageId)
        {
            var liste = ficheCadenassageRepository.Get(FicheCadenassageId).LignesInstruction.Select(x => x.LigneInstructionId).ToList();
            foreach (var i in liste)
                this.Delete(i);
            this.SaveChanges();

        }

        public override LigneInstruction Get(int id)
        {
              return this.dbSet.Include(b => b.Instruction).Where(x => x.LigneInstructionId == id).FirstOrDefault();
        }
       
    }

}