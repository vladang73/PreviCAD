using Previgesst.Repositories;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Previgesst.Services
{
    public class ApplicationPrevisService
    {
        ApplicationPreviRepository applicationPreviRepository;
        public ApplicationPrevisService(ApplicationPreviRepository applicationPreviRepository)
        {
            this.applicationPreviRepository = applicationPreviRepository;

        }

        public int getIdByName( string Nom)
        {
            return applicationPreviRepository.GetAll().Where(x => x.Nom == Nom).FirstOrDefault().ApplicationPreviId;

        }


        public IEnumerable<ApplicationPreviDDLViewModel> GetAllApplicationDDLViewModel()
        {
            var applicationDDL = applicationPreviRepository.AsQueryable().OrderBy(s => s.ApplicationPreviId).Select(s => new ApplicationPreviDDLViewModel()
            {

                ApplicationPreviId = s.ApplicationPreviId,
                Nom = s.Nom


            });

            return applicationDDL;
        }

        public IEnumerable<ApplicationPreviDDLViewModel> GetAppGenerealeViewModel()
        {
            var applicationDDL = applicationPreviRepository.AsQueryable().Where(s=>s.ApplicationPreviId== 1).OrderBy(s => s.ApplicationPreviId).Select(s => new ApplicationPreviDDLViewModel()
            {

                ApplicationPreviId = s.ApplicationPreviId,
                Nom = s.Nom

            });

            return applicationDDL;
        }
    }
}