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
            var liste = ficheCadenassageRepository.Get(FicheCadenassageId).SourcesEnergieCadenassage.Select(x => x.SourceEnergieCadenassageId).ToList();
            foreach (var i in liste)
                this.Delete(i);

            this.SaveChanges();
        }

        public void UpdateSources(SourceEnergieCadenassageViewModel item)
        {
            SupprimerDeFiche(item.FicheCadenassageId);

            foreach (int value in item.SourcesEnergieId)
            {
                this.Add(new SourceEnergieCadenassage { FicheCadenassageId = item.FicheCadenassageId, SourceEnergieId = value });
            }
            this.SaveChanges();
        }

        public bool CheckIfChanged(SourceEnergieCadenassageViewModel item)
        {
            // get items from database
            var existing = ficheCadenassageRepository.Get(item.FicheCadenassageId).SourcesEnergieCadenassage.Select(x => x.SourceEnergieId).ToList();

            // if there any items int he database
            if (existing != null)
            {
                // if nothing is changed, return false
                return !(item.SourcesEnergieId.All(existing.Contains) && existing.All(item.SourcesEnergieId.Contains));
            }

            return true;
        }
    }
}