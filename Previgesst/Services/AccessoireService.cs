using Previgesst.Repositories;
using Previgesst.ViewModels.DDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Previgesst.Services
{
    public class AccessoireService
    {


        AccessoireRepository accessoireRepository;
        public AccessoireService(AccessoireRepository accessoireRepository)
        {
            this.accessoireRepository = accessoireRepository;

        }

        public IEnumerable<AccessoireDDLViewModel> GetAllAsAccessoireDDLViewModel()
        {
            var item = accessoireRepository.AsQueryable().Select(p => new AccessoireDDLViewModel()
            {
               AccessoireId = p.AccessoireId,
               Description = p.Description
            }).OrderBy(p => p.Description);

            return item;
        }

    }
}