using Previgesst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class MaterielRequisCadenassageRepository:RepositoryBase<MaterielRequisCadenassage>
    {
        private FicheCadenassageRepository ficheCadenassageRepository;
        public MaterielRequisCadenassageRepository(DbContext context, FicheCadenassageRepository ficheCadenassageRepository) : base(context) {
            this.ficheCadenassageRepository = ficheCadenassageRepository;
        }



        public void SupprimerDeFiche(int FicheCadenassageId)
        {
            var liste = ficheCadenassageRepository.Get(FicheCadenassageId).MaterielsRequisCadenassage.Select(x => x.MaterielRequisCadenassageId).ToList();
            foreach (var i in liste)
                this.Delete(i);
            this.SaveChanges();

        }

        public override MaterielRequisCadenassage Get(int id)
        {
            return this.dbSet.Include(b => b.Materiel).Where(x => x.MaterielRequisCadenassageId == id).FirstOrDefault();
        }
    }
}