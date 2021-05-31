using Previgesst.Repositories;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Previgesst.Services
{
    public class GraviteService
    {
        private GraviteRepository graviteRepository;

        public GraviteService(GraviteRepository graviteRepository)
        {
            this.graviteRepository = graviteRepository;
        }

        public IEnumerable<GraviteDDLViewModel> GetAllAsGraviteDDLViewModel()
        {
            var graviteDDL = graviteRepository.AsQueryable().Select(g => new GraviteDDLViewModel() {
                GraviteId = g.GraviteId,
                No = g.No,
                Explication = g.Explication
            }).OrderBy(g => g.GraviteId);

            return graviteDDL;
        }
    }
}