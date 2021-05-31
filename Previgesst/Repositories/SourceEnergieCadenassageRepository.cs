using Previgesst.Models;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class SourceEnergieCadenassageRepository : RepositoryBase<SourceEnergieCadenassage>
    {
        private FicheCadenassageRepository ficheCadenassageRepository;

        public SourceEnergieCadenassageRepository(System.Data.Entity.DbContext context, FicheCadenassageRepository ficheCadenassageRepository) : base(context)
        {

            this.ficheCadenassageRepository = ficheCadenassageRepository;
        }

        public void SupprimerDeFiche(int FicheCadenassageId)
        {
            var liste = ficheCadenassageRepository.Get(FicheCadenassageId).SourcesEnergieCadenassage.Select( x=>x.SourceEnergieCadenassageId).ToList();
            foreach (var i in liste)
                this.Delete(i);
            this.SaveChanges();

        }

        public void UpdateSources(SourceEnergieCadenassageViewModel item )
        {


            SupprimerDeFiche(item.FicheCadenassageId);

            foreach ( int value in item.SourcesEnergieId)
            {
                this.Add(new SourceEnergieCadenassage { FicheCadenassageId = item.FicheCadenassageId, SourceEnergieId = value });
            }
            this.SaveChanges();
       

           
           

        }


    }
}