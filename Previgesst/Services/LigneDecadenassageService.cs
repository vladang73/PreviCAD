using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Previgesst.Repositories;
using Previgesst.ViewModels;
//using Previgesst.ViewModels.Cadenassage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Previgesst.Services
{
    public class LigneDecadenassageService
    {
        private LigneDecadenassageRepository ligneDecadenassageRepository;

        public LigneDecadenassageService(LigneDecadenassageRepository ligneDecadenassageRepository)
        {
            this.ligneDecadenassageRepository = ligneDecadenassageRepository;

        }


        public LigneDecadenassageViewModel getLigneVM(int LigneId)
        {
            var baseURL = Helpers.URLHelper.GetBaseUrl();
            if (baseURL.Right(1) != "/" && baseURL.Right(1) != @"\")
                baseURL += "/";
            var time = DateTime.Now.ToLongTimeString();
            var ligne = ligneDecadenassageRepository.AsQueryable().Where(x => x.LigneDecadenassageId == LigneId).Select(x => new LigneDecadenassageViewModel()
            {
                NoLigne = x.NoLigne ?? 0,
                FicheCadenassageId = x.FicheCadenassageId,
                TexteSupplementaireDispositifEN = x.TexteSupplementaireDispositifEN,
                TexteSupplementaireInstructionEN = x.TexteSupplementaireInstructionEN,
                Suppressible = true,
                CocherColonneCadenas = x.CocherColonneCadenas,
                InstructionId = x.InstructionId,
                LigneDecadenassageId = x.LigneDecadenassageId,
                //NomFichier = x.NomFichier,
                Realiser = x.Realiser,
               // TexteLocalisation = x.TexteLocalisation,
                TexteSupplementaireDispositif = x.TexteSupplementaireDispositif,
                TexteSupplementaireInstruction = x.TexteSupplementaireInstruction,
                TexteInstruction = x.Instruction.TexteInstruction,
                TexteRealiser = x.TexteSupplementaireRealiser,
                PhotoFicheCadenassageId = x.PhotoFicheCadenassageId,
                Thumbnail = baseURL + "Images/Cadenassage/Photos/" + (x.PhotoFicheCadenassageId == null ? "vide" : x.PhotoFicheCadenassageId.ToString()) + "/thumb.jpg?time=" + time,



            }).FirstOrDefault();
            return ligne;

        }


        internal DataSourceResult GetListeLignesDecadenassage(DataSourceRequest request, int FicheCadenassageId)
        {// il se peut qu'on ait une instruction vide: l'usager ajoute un fichier et ne sauve pas la fiche... on ne doit pas la considérer
            var baseURL = Helpers.URLHelper.GetBaseUrl();
            if (baseURL.Right(1) != "/" && baseURL.Right(1) != @"\")
                baseURL += "/";
            var time = DateTime.Now.ToLongTimeString();
            var result = ligneDecadenassageRepository.AsQueryable().Where(x => x.FicheCadenassageId == FicheCadenassageId && x.InstructionId != null).OrderBy(x => x.NoLigne).
              Select(x => new LigneDecadenassageViewModel()
              {
                  NoLigne = x.NoLigne ?? 0,
                  FicheCadenassageId = x.FicheCadenassageId,

                  Suppressible = true,
                  CocherColonneCadenas = x.CocherColonneCadenas,
                  InstructionId = x.InstructionId,
                  LigneDecadenassageId = x.LigneDecadenassageId,
                  //NomFichier = x.NomFichier,
                  Realiser = x.Realiser,
                  TexteSupplementaireDispositifEN = x.TexteSupplementaireDispositifEN,
                  TexteSupplementaireInstructionEN = x.TexteSupplementaireInstructionEN,
                //  TexteLocalisation = x.TexteLocalisation,
                  TexteSupplementaireDispositif = x.TexteSupplementaireDispositif,
                  TexteSupplementaireInstruction = x.TexteSupplementaireInstruction,
                  TexteInstruction = x.Instruction.TexteInstruction,
               //   Thumbnail = baseURL+ "Images/Cadenassage/Decadenassage/" + x.LigneDecadenassageId.ToString() + "/thumb.jpg?time=" + time,
                  Thumbnail = baseURL + "Images/Cadenassage/Photos/" + (x.PhotoFicheCadenassageId == null ? "vide" : x.PhotoFicheCadenassageId.ToString()) + "/thumb.jpg?time=" + time,

                  TexteRealiser = x.TexteSupplementaireRealiser,
                  PhotoFicheCadenassageId= x.PhotoFicheCadenassageId



              }).ToDataSourceResult(request);

            return result;


        }


        public void SaveLigneDecadenassage(LigneDecadenassageViewModel model)
        {
            var item = ligneDecadenassageRepository.Get(model.LigneDecadenassageId);
            if (item == null)
                item = new Models.LigneDecadenassage();

            item.CocherColonneCadenas = model.CocherColonneCadenas;
            item.FicheCadenassageId = model.FicheCadenassageId;
            item.InstructionId = model.InstructionId;
            item.NoLigne = model.NoLigne;
            //item.NomFichier = model.NomFichier;
            item.Realiser = model.Realiser;
         //   item.TexteLocalisation = model.TexteLocalisation;
            item.TexteSupplementaireDispositif = model.TexteSupplementaireDispositif;
            item.TexteSupplementaireInstruction = model.TexteSupplementaireInstruction;
            item.TexteSupplementaireRealiser = model.TexteRealiser;

            item.TexteSupplementaireInstructionEN = model.TexteSupplementaireInstructionEN;
            item.TexteSupplementaireDispositifEN = model.TexteSupplementaireDispositifEN;
            item.PhotoFicheCadenassageId = model.PhotoFicheCadenassageId;



            if (item.LigneDecadenassageId > 0)
                ligneDecadenassageRepository.Update(item);
            else
                ligneDecadenassageRepository.Add(item);

            
            ligneDecadenassageRepository.SaveChanges();
            model.LigneDecadenassageId = item.LigneDecadenassageId;
            item = ligneDecadenassageRepository.Get(item.LigneDecadenassageId);
            model.TexteInstruction = item.Instruction.TexteInstruction;
            


        }

        public bool Supprimer(LigneDecadenassageViewModel model)
        {
            var x = ligneDecadenassageRepository.Get(model.LigneDecadenassageId);
            if (x == null)
                return true;


            ligneDecadenassageRepository.Delete(x.LigneDecadenassageId);
            ligneDecadenassageRepository.SaveChanges();

            return true;
        }


        public int getNextRankDecadenassage(int id)
        {
            double? value = (double?)ligneDecadenassageRepository.AsQueryable().Where(x => x.FicheCadenassageId == id).Max(x => x.NoLigne);

        
            value = value + 1.00001;
            return ((int)Math.Floor(value ?? 1));


          

        }


        //internal DataSourceResult GetListeLignesDecadenassage(DataSourceRequest request, int FicheCadenassageId)
        //{
        //    var result = ligneDecadenassageRepository.AsQueryable().Where(x => x.FicheCadenassageId == FicheCadenassageId).OrderBy(x => x.NoLigne).
        //        Select(x => new LigneDecadenassageViewModel()
        //        {
        //            NoLigne = x.NoLigne ??0,
        //            Realiser = x.Realiser,
        //           // TexteInstruction = x.texteInstruction,
        //            FicheCadenassageId = x.FicheCadenassageId,
        //            LigneDecadenassageId = x.LigneDecadenassageId,
        //            Suppressible = true



        //    }).ToDataSourceResult(request);

        //    return result;
        //}


        //public void SaveLigneDecadenassage(LigneDecadenassageViewModel model)
        //{
        //    var item = ligneDecadenassageRepository.Get(model.LigneDecadenassageId);
        //    if (item == null)
        //        item = new Models.LigneDecadenassage();

        //    item.FicheCadenassageId = model.FicheCadenassageId;
        //    item.LigneDecadenassageId = model.LigneDecadenassageId;
        //    item.NoLigne = model.NoLigne;
        //    item.Realiser = model.Realiser;

        //    //item.texteInstruction = model.TexteInstruction;




        //    if (item.LigneDecadenassageId > 0)
        //        ligneDecadenassageRepository.Update(item);
        //    else
        //        ligneDecadenassageRepository.Add(item);

        //    ligneDecadenassageRepository.SaveChanges();
        //    model.LigneDecadenassageId = item.LigneDecadenassageId;



        //}

        //public bool Supprimer(LigneDecadenassageViewModel model)
        //{
        //    var section = ligneDecadenassageRepository.Get(model.LigneDecadenassageId);
        //    if (section == null)
        //        return true;


        //    ligneDecadenassageRepository.Delete(section.LigneDecadenassageId);
        //    ligneDecadenassageRepository.SaveChanges();

        //    return true;
        //}
    }
}