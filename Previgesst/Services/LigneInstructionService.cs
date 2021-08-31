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
    public class LigneInstructionService
    {
        private LigneInstructionRepository ligneInstructionRepository;

        public LigneInstructionService(LigneInstructionRepository ligneInstructionRepository)
        {
            this.ligneInstructionRepository = ligneInstructionRepository;
        }


        public LigneInstructionViewModel getLigneVM(int LigneId)
        {
            var time = DateTime.Now.ToLongTimeString();
            var baseURL = Helpers.URLHelper.GetBaseUrl();
            if (baseURL.Right(1) != "/" && baseURL.Right(1) != @"\")
                baseURL += "/";

            var ligne = ligneInstructionRepository.AsQueryable().Where(x => x.LigneInstructionId == LigneId).Select(x => new LigneInstructionViewModel()
            {
                NoLigne = x.NoLigne ?? 0,
                FicheCadenassageId = x.FicheCadenassageId,

                Suppressible = true,
                CocherColonneCadenas = x.CocherColonneCadenas,
                InstructionId = x.InstructionId,
                LigneInstructionId = x.LigneInstructionId,
                // NomFichier = x.NomFichier,
                Realiser = x.Realiser,
                // TexteLocalisation = x.TexteLocalisation,
                TexteSupplementaireDispositif = x.TexteSupplementaireDispositif,
                TexteSupplementaireInstruction = x.TexteSupplementaireInstruction,
                TexteSupplementaireInstructionEN = x.TexteSupplementaireInstructionEN,
                TexteSupplementaireDispositifEN = x.TexteSupplementaireDispositifEN,
                TexteInstruction = x.Instruction.TexteInstruction,
                TexteRealiser = x.TexteSupplementaireRealiser,
                PhotoFicheCadenassageId = x.PhotoFicheCadenassageId,
                Thumbnail = baseURL + "Images/Cadenassage/Photos/" + (x.PhotoFicheCadenassageId == null ? "vide" : x.PhotoFicheCadenassageId.ToString()) + "/thumb.jpg?time=" + time,



            }).FirstOrDefault();
            return ligne;

        }

        internal IQueryable<LigneInstructionViewModel> GetListeLignesInstruction(int FicheCadenassageId)
        {
            var langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;


            // il se peut qu'on ait une instruction vide: l'usager ajoute un fichier et ne sauve pas la fiche... on ne doit pas la considérer
            var time = DateTime.Now.ToLongTimeString();
            var baseURL = Helpers.URLHelper.GetBaseUrl();
            if (baseURL.Right(1) != "/" && baseURL.Right(1) != @"\")
                baseURL += "/";

            var result = ligneInstructionRepository.AsQueryable()
                .Where(x => x.FicheCadenassageId == FicheCadenassageId && x.InstructionId != null)
                .OrderBy(x => x.NoLigne)
                .Select(x => new LigneInstructionViewModel()
                {
                    NoLigne = x.NoLigne ?? 0,
                    FicheCadenassageId = x.FicheCadenassageId,

                    Suppressible = true,
                    CocherColonneCadenas = x.CocherColonneCadenas,
                    InstructionId = x.InstructionId,
                    LigneInstructionId = x.LigneInstructionId,
                    //  NomFichier = x.NomFichier,
                    Realiser = x.Realiser,
                    //  TexteLocalisation = x.TexteLocalisation,
                    TexteSupplementaireDispositif = x.TexteSupplementaireDispositif,
                    TexteSupplementaireInstruction = x.TexteSupplementaireInstruction,
                    TexteSupplementaireDispositifEN = x.TexteSupplementaireDispositifEN,
                    TexteSupplementaireInstructionEN = x.TexteSupplementaireInstructionEN,
                    
                    TexteInstruction = langue == "fr" ? x.Instruction.TexteInstruction : x.Instruction.TexteInstructionEN,
                    TexteDispositif = langue == "fr" ? x.Instruction.Dispositif.Description : x.Instruction.Dispositif.DescriptionEN,
                    TexteAccessoire = langue == "fr" ? x.Instruction.Accessoire.Description : x.Instruction.Accessoire.DescriptionEN,

                    Thumbnail = baseURL + "Images/Cadenassage/Photos/" + (x.PhotoFicheCadenassageId == null ? "vide" : x.PhotoFicheCadenassageId.ToString()) + "/thumb.jpg?time=" + time,
                    PhotoFicheCadenassageId = x.PhotoFicheCadenassageId,
                    TexteRealiser = x.TexteSupplementaireRealiser                     
                });

            return result;
        }


        internal DataSourceResult GetListeLignesInstruction(DataSourceRequest request, int FicheCadenassageId)
        {
            return GetListeLignesInstruction(FicheCadenassageId).ToDataSourceResult(request);
        }


        public void SaveLigneInstruction(LigneInstructionViewModel model)
        {
            var item = ligneInstructionRepository.Get(model.LigneInstructionId);
            if (item == null)
                item = new Models.LigneInstruction();

            item.CocherColonneCadenas = model.CocherColonneCadenas;
            item.FicheCadenassageId = model.FicheCadenassageId;
            item.InstructionId = model.InstructionId;
            item.NoLigne = model.NoLigne;
            //  item.NomFichier = model.NomFichier;
            item.Realiser = model.Realiser;
            //item.TexteLocalisation = model.TexteLocalisation;
            item.TexteSupplementaireDispositif = model.TexteSupplementaireDispositif;
            item.TexteSupplementaireInstruction = model.TexteSupplementaireInstruction;
            item.TexteSupplementaireDispositifEN = model.TexteSupplementaireDispositifEN;
            item.TexteSupplementaireInstructionEN = model.TexteSupplementaireInstructionEN;
            item.TexteSupplementaireRealiser = model.TexteRealiser;
            item.PhotoFicheCadenassageId = model.PhotoFicheCadenassageId;







            if (item.LigneInstructionId > 0)
                ligneInstructionRepository.Update(item);
            else
                ligneInstructionRepository.Add(item);

            ligneInstructionRepository.SaveChanges();
            model.LigneInstructionId = item.LigneInstructionId;
            item = ligneInstructionRepository.Get(item.LigneInstructionId);
            model.TexteInstruction = item.Instruction.TexteInstruction;



        }

        public bool Supprimer(LigneInstructionViewModel model)
        {
            var x = ligneInstructionRepository.Get(model.LigneInstructionId);
            if (x == null)
                return true;


            ligneInstructionRepository.Delete(x.LigneInstructionId);
            ligneInstructionRepository.SaveChanges();

            return true;
        }

        public int getNextRankInstruction(int id)
        {
            double? value = (double?)ligneInstructionRepository.AsQueryable().Where(x => x.FicheCadenassageId == id).Max(x => x.NoLigne);


            value = value + 1.00001;
            return ((int)Math.Floor(value ?? 1));



        }
    }
}