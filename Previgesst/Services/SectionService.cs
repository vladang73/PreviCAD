using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Previgesst.Repositories;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Previgesst.Services
{
    public class SectionService
    {
        SectionRepository sectionRepository;
        public SectionService(SectionRepository sectionRepository)
        {
            this.sectionRepository = sectionRepository;
        }

        public IEnumerable<SectionsDDLViewModel> GetAllAsSectionsDDLViewModel(int ApplicationId)
        {
            var sectionDDL = sectionRepository.AsQueryable().Where(s=>s.ApplicationPreviId==ApplicationId).OrderBy(s=>s.Ordre).Select(s => new SectionsDDLViewModel()
            {
                
                SectionId= s.SectionId,
                Description= s.Nom
                
                
            });

            return sectionDDL;
        }




        internal DataSourceResult GetReadListeSections(DataSourceRequest request)
        {// on va seulement chercher les sections générales
               var result = sectionRepository.AsQueryable().Where(x=>x.ApplicationPreviId==1).OrderBy(x=>x.ApplicationPreviId).Select(x => new SectionViewModel()
            {
                ApplicationName = x.ApplicationPrevi.Nom,
                ApplicationPreviId = x.ApplicationPreviId,
                Nom = x.Nom,
                SectionId = x.SectionId,
                Ordre = x.Ordre,
                Suppressible = (x.Document.Count() == 0)
                


            }).ToDataSourceResult(request);

            return result;
        }


        public void SaveSection(SectionViewModel model)
        {
            var item = sectionRepository.Get(model.SectionId);
            if (item == null)
                item = new Models.Section();

            item.ApplicationPreviId = model.ApplicationPreviId;
            item.Nom = model.Nom;
            item.Ordre = model.Ordre;
          



            if (item.SectionId > 0)
                sectionRepository.Update(item);
            else
                sectionRepository.Add(item);

            sectionRepository.SaveChanges();
            item = sectionRepository.Get(item.SectionId);
            model.SectionId = item.SectionId;
            model.ApplicationName = item.ApplicationPrevi.Nom;
         
        }

        public bool Supprimer(SectionViewModel model)
        {
            var section = sectionRepository.Get(model.SectionId);
            if (section == null)
                return true;


            sectionRepository.Delete(section.SectionId);
            sectionRepository.SaveChanges();

            return true;
        }
    }
}