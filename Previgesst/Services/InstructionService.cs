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
    public class InstructionService
    {
        InstructionRepository instructionRepository;
        public InstructionService(InstructionRepository instructionRepository)
        {
            this.instructionRepository = instructionRepository;

        }


        internal DataSourceResult GetReadListeInstructions(DataSourceRequest request)
        {
            var result = instructionRepository.AsQueryable().OrderBy(x => x.Identificateur).Select(x => new InstructionViewModel()
            {
                AccessoireId = x.AccessoireId,
                Accessoire =x.Accessoire.Description,
                DispositifId = x.DispositifId,
                Dispositif = x.Dispositif.Description,
                TexteInstruction= x.TexteInstruction,
                InstructionId = x.InstructionId,
                Identificateur = x.Identificateur,
                IdentificateurEN= x.IdentificateurEN,
                TexteInstructionEN = x.TexteInstructionEN,
                Suppressible = (x.LignesInstruction.Count() == 0)



            }).ToDataSourceResult(request);

            return result;
        }


        public void SaveInstruction(InstructionViewModel model)
        {
            var item = instructionRepository.Get(model.InstructionId);
            if (item == null)
                item = new Models.Instruction();

            item.DispositifId = model.DispositifId;
            item.AccessoireId = model.AccessoireId;
            item.Identificateur = model.Identificateur.Replace("\n","").Replace("\r","");
            item.IdentificateurEN = model.IdentificateurEN.Replace("\n", "").Replace("\r", "");
            item.TexteInstruction = model.TexteInstruction.Replace("\n", "").Replace("\r", "");
            item.TexteInstructionEN= model.TexteInstructionEN.Replace("\n", "").Replace("\r", "");



            if (item.InstructionId > 0)
                instructionRepository.Update(item);
            else
                instructionRepository.Add(item);

            instructionRepository.SaveChanges();
            item = instructionRepository.Get(item.InstructionId);
            model.Accessoire = item.Accessoire.Description;
            model.Dispositif = item.Dispositif.Description;

        }

        public bool Supprimer(InstructionViewModel model)
        {
            var item = instructionRepository.Get(model.InstructionId);
            if (item == null)
                return true;
            if (item.LignesInstruction.Count > 0)
                return false;


            instructionRepository.Delete(item.InstructionId);
            instructionRepository.SaveChanges();

            return true;
        }



        internal IEnumerable<InstructionDDLViewModel> GetInstructionDDL()
        {
            var langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            var lst = instructionRepository.GetAll()
                      .Select(c => new InstructionDDLViewModel()
                      {
                          InstructionId= c.InstructionId,
                          Identificateur = langue=="fr"? c.Identificateur:c.IdentificateurEN,
                          Accessoire= langue=="fr"?c.Accessoire.Description:c.Accessoire.DescriptionEN,
                          Description = langue=="fr"? c.TexteInstruction: c.TexteInstructionEN,
                          Dispositif = langue=="fr"?c.Dispositif.Description:c.Dispositif.DescriptionEN
                      }).OrderBy(x=>x.Identificateur);

            return lst;
        }
    }
}