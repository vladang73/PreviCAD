using Previgesst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class LigneDecadenassageRepository:RepositoryBase<LigneDecadenassage>
    {
        private FicheCadenassageRepository ficheCadenassageRepository;

        public LigneDecadenassageRepository(DbContext context,   FicheCadenassageRepository ficheCadenassageRepository) : base(context) {
            this.ficheCadenassageRepository = ficheCadenassageRepository;

        }
        public override LigneDecadenassage Get(int id)
        {
            return this.dbSet.Include(b => b.Instruction).Where(x => x.LigneDecadenassageId == id).FirstOrDefault();
        }

        public void SupprimerDeFiche(int FicheCadenassageId)
        {
            var liste = ficheCadenassageRepository.Get(FicheCadenassageId).LignesDecadenassage.Select(x => x.LigneDecadenassageId).ToList() ;
            foreach (var i in liste)
                this.Delete(i);
            this.SaveChanges();

        }
    }
}