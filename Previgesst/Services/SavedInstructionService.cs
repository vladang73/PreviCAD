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
        SavedInstructionNoteRepository instructionNoteRepository;

        public SavedInstructionService(SavedInstructionRepository instructionRepository, SavedInstructionNoteRepository instructionNoteRepository)
        {
            this.instructionRepository = instructionRepository;
            this.instructionNoteRepository = instructionNoteRepository;
        }


        internal List<SavedInstruction> GetSavedInstructions(int ficheId)
        {
            var result = instructionRepository.AsQueryable()
                .Where(x => x.FicheCadenassageId == ficheId)
                .ToList();

            return result;
        }

        internal SavedInstructionNote GetSavedInstructionNote(int ficheId)
        {
            return instructionNoteRepository.Get(ficheId);
        }


        public void SaveInstructions(List<SavedInstruction> allItems, string note)
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

            // add | update notes
            if (!string.IsNullOrEmpty(note))
            {
                var ficheId = allItems.FirstOrDefault().FicheCadenassageId;
                var existingNote = instructionNoteRepository.Get(ficheId);

                if (existingNote == null)
                {
                    instructionNoteRepository.Add(new SavedInstructionNote { Notes = note, FicheCadenassageId = ficheId });
                }
                else
                {
                    existingNote.Notes = note;
                }
            }

            instructionRepository.SaveChanges();
        }

        public void ClearInstructions(int ficheId)
        {
            var allItems = GetSavedInstructions(ficheId);


            foreach (var item in allItems)
            {
                instructionRepository.Delete(item.PKId);
            }

            instructionRepository.SaveChanges();
        }
    }
}