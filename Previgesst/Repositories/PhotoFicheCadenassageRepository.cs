using Previgesst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class PhotoFicheCadenassageRepository:RepositoryBase<PhotoFicheCadenassage>
    {
        private FicheCadenassageRepository ficheCadenassageRepository;
        public PhotoFicheCadenassageRepository(DbContext context, FicheCadenassageRepository ficheCadenassageRepository) : base(context) {

            this.ficheCadenassageRepository = ficheCadenassageRepository;
        }


        public void SupprimerDeFiche(int FicheCadenassageId)
        {
            var liste = ficheCadenassageRepository.Get(FicheCadenassageId).PhotosFicheCadenassage.Select(x => x.PhotoFicheCadenassageId).ToList();
            foreach (var i in liste)
                this.Delete(i);
            this.SaveChanges();

        }

    }
}