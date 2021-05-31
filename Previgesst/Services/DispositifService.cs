using Previgesst.ViewModels.DDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Previgesst.Repositories;

namespace Previgesst.Services
{
    public class DispositifService
    {
        DispositifRepository dispositifRepository;
        public DispositifService(DispositifRepository dispositifRepository)
        {
            this.dispositifRepository = dispositifRepository;

        }

        public IEnumerable<DispositifDDLViewModel> GetAllAsDispositifsDDLViewModel()
        {
            var item = dispositifRepository.AsQueryable().Select(p => new DispositifDDLViewModel()
            {
                DispositifId= p.DispositifId,
                Description = p.Description
            }).OrderBy(p => p.Description);

            return item;
        }
    }
}