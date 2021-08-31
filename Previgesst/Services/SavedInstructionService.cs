using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Previgesst.Models;
using Previgesst.Repositories;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Previgesst.Services
{
    public class SavedInstructionService
    {
        SavedInstructionRepository instructionRepository;

        public SavedInstructionService(SavedInstructionRepository instructionRepository)
        {
            this.instructionRepository = instructionRepository;

        }


        internal List<SavedInstruction> GetSavedInstructions(int ficheId)
        {
            var result = instructionRepository.AsQueryable()
                .Where(x => x.FicheCadenassageId == ficheId)
                .ToList();

            return result;
        }


        public void SaveInstructions(List<SavedInstruction> allItems)
        {
            foreach (var model in allItems)
            {
                if (string.IsNullOrEmpty(model.StepStatus)) model.StepStatus = "1";

                var item = instructionRepository.AsQueryable()
                                                .Where(x => x.InstructionId == model.InstructionId)
                                                .Where(x => x.InstructionType == model.InstructionType)
                                                .FirstOrDefault();

                if (item == null)
                    item = new Models.SavedInstruction();

                item.InstructionId = model.InstructionId;
                item.InstructionType = model.InstructionType;
                item.FicheCadenassageId = model.FicheCadenassageId;
                item.StepStatus = model.StepStatus;


                if (item.PKId > 0)
                    instructionRepository.Update(item);
                else
                    instructionRepository.Add(item);
            }
            instructionRepository.SaveChanges();
        }
    }
}