using Previgesst.Repositories;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Previgesst.Services
{
    public class PossibiliteService
    {
        private PossibiliteRepository possibiliteRepository;

        public PossibiliteService(PossibiliteRepository possibiliteRepository)
        {
            this.possibiliteRepository = possibiliteRepository;
        }

        public IEnumerable<PossibiliteDDLViewModel> GetAllAsPossibiliteDDLViewModel()
        {
            var possibiliteDDL = possibiliteRepository.AsQueryable().Select(p => new PossibiliteDDLViewModel(){
                PossibiliteId = p.PossibiliteId,
                No = p.No,
                Explication = p.Explication
            }).OrderBy(p => p.PossibiliteId);

            return possibiliteDDL;
        }
    }
}