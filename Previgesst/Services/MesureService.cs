using Previgesst.Repositories;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Previgesst.Services
{
    public class MesureService
    {
        private MesureRepository mesureRepository;

        public MesureService(MesureRepository mesureRepository)
        {
            this.mesureRepository = mesureRepository;
        }

        public IEnumerable<MesureDDLViewModel> GetAllAsMesureDDLViewModel()
        {
            var mesureDDL = mesureRepository.AsQueryable().Select(m => new MesureDDLViewModel() {
                MesureId = m.MesureId,
                Description = m.Description
            }).OrderBy(m => m.Description);

            return mesureDDL;
        }
    }
}