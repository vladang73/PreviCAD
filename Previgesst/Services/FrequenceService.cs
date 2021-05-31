using Previgesst.Repositories;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Previgesst.Services
{
    public class FrequenceService
    {
        private FrequenceRepository frequenceRepository;

        public FrequenceService(FrequenceRepository frequenceRepository)
        {
            this.frequenceRepository = frequenceRepository;
        }

        public IEnumerable<FrequenceDDLViewModel> GetAllAsFrequenceDDLViewModel()
        {
            var frequenceDDL = frequenceRepository.AsQueryable().Select(f => new FrequenceDDLViewModel() {
                FrequenceId = f.FrequenceId,
                No = f.No,
                Explication = f.Explication
            }).OrderBy(f => f.FrequenceId);

            return frequenceDDL;
        }
    }
}