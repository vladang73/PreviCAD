using Previgesst.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Previgesst.ViewModels;
using Previgesst.Models;
using Previgesst.DataContexts;
using System.Data.Common;

namespace Previgesst.Services
{
    public class LigneAnalyseRisqueService
    {
        private LigneAnalyseRisqueRepository ligneAnalyseRisqueRepository;
        private UtilisateurService utilisateurService;
        private PhenomeneRepository phenomeneRepository;

        public LigneAnalyseRisqueService(LigneAnalyseRisqueRepository ligneAnalyseRisqueRepository, PhenomeneRepository phenomeneRepository)
        {
            this.ligneAnalyseRisqueRepository = ligneAnalyseRisqueRepository;
            this.phenomeneRepository = phenomeneRepository;
        }

        internal void CreateLigneAnalyseRisque(LigneAnalyseRisqueEditViewModel larevm, int id)
        {
            var nouvelleLigneAnalyseRisque = new LigneAnalyseRisque();

            nouvelleLigneAnalyseRisque.AnalyseRisqueId          = id;
            nouvelleLigneAnalyseRisque.ConformiteAuxNormes      = larevm.ConformiteAuNormes;
           // nouvelleLigneAnalyseRisque.Equipement             = larevm.Equipement;
            nouvelleLigneAnalyseRisque.EvenementId              = larevm.EvenementId;
            nouvelleLigneAnalyseRisque.MesurePrevention = larevm.MesurePrevention;
            nouvelleLigneAnalyseRisque.NbPersonnesExposees      = larevm.NbPersonnesExposees;
            nouvelleLigneAnalyseRisque.Operation                = larevm.Operation;
            nouvelleLigneAnalyseRisque.PhenomeneId              = larevm.PhenomeneId;
            nouvelleLigneAnalyseRisque.ReglesEtNormes           = larevm.ReglesEtNormes;
            nouvelleLigneAnalyseRisque.SituationId              = larevm.SituationId;
            nouvelleLigneAnalyseRisque.Zone                     = larevm.Zone;
            nouvelleLigneAnalyseRisque.SystemeCommandeAvant     = larevm.SystemeCommandeAvant;
            nouvelleLigneAnalyseRisque.SystemeCommandeInstalles = larevm.SystemeCommandeInstalles;
            nouvelleLigneAnalyseRisque.IndiceFinalApres         = larevm.IndiceFinalApres;
            nouvelleLigneAnalyseRisque.IndiceFinalAvant         = larevm.IndiceFinalAvant;
            nouvelleLigneAnalyseRisque.FrequenceApresId         = larevm.FrequenceApresId;
            nouvelleLigneAnalyseRisque.FrequenceAvantId         = larevm.FrequenceAvantId;
            nouvelleLigneAnalyseRisque.GraviteApresId           = larevm.GraviteApresId;
            nouvelleLigneAnalyseRisque.GraviteAvantId           = larevm.GraviteAvantId;
            nouvelleLigneAnalyseRisque.PossibiliteApresId       = larevm.PossibiliteApresId;
            nouvelleLigneAnalyseRisque.PossibiliteAvantId       = larevm.PossibiliteAvantId;
            nouvelleLigneAnalyseRisque.ProbabiliteApresId       = larevm.ProbabiliteApresId;
            nouvelleLigneAnalyseRisque.ProbabiliteAvantId       = larevm.ProbabiliteAvantId;
            nouvelleLigneAnalyseRisque.Rang                     = larevm.Rang;
            nouvelleLigneAnalyseRisque.PlanAction               = larevm.PlanAction;
            nouvelleLigneAnalyseRisque.ResponsableAnalyse       = larevm.ResponsableAnalyse;
            nouvelleLigneAnalyseRisque.DateDeRealisation        = larevm.DateDeRealisation;
            nouvelleLigneAnalyseRisque.EtatId = larevm.EtatId;
            //   nouvelleLigneAnalyseRisque.NoReference = larevm.NoReference;

            ligneAnalyseRisqueRepository.Add(nouvelleLigneAnalyseRisque);
            ligneAnalyseRisqueRepository.SaveChanges();
            larevm.IdLigneAnalyseRisqueEditor = nouvelleLigneAnalyseRisque.AnalyseRisqueId;
        }

        public int getIndiceRisque( int gravite, int frequence, int probabilite, int possibilite)
        {
            using (var context = AppDbContext.Create())
            {
                var result = context.Database.SqlQuery<int>( @"select dbo.getIndiceRisque ({0},{1},{2},{3})", gravite, frequence , probabilite, possibilite);
                return result.First();
            }
        }

        public LigneAnalyseRisqueEditViewModel getFicheVM(int ligneAnalyseRisqueId)
        {
            var langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;



            var result = ligneAnalyseRisqueRepository.Get(ligneAnalyseRisqueId);
            var vm = new LigneAnalyseRisqueEditViewModel
            {
                ConformiteAuNormes = result.ConformiteAuxNormes,
                EvenementId = result.EvenementId,
                MesurePrevention = result.MesurePrevention,
                NbPersonnesExposees = result.NbPersonnesExposees,
                Operation = result.Operation,
                PhenomeneId = result.PhenomeneId,
                ReglesEtNormes = result.ReglesEtNormes,
                SituationId = result.SituationId,
                Zone = result.Zone,
                SystemeCommandeAvant = result.SystemeCommandeAvant,
                SystemeCommandeInstalles = result.SystemeCommandeInstalles,
                IndiceFinalApres = result.IndiceFinalApres,
                IndiceFinalAvant = result.IndiceFinalAvant,
                FrequenceApresId = result.FrequenceApresId,
                FrequenceAvantId = result.FrequenceAvantId,
                GraviteApresId = result.GraviteApresId,
                GraviteAvantId = result.GraviteAvantId,
                PossibiliteApresId = result.PossibiliteApresId,
                PossibiliteAvantId = result.PossibiliteAvantId,
                ProbabiliteApresId = result.ProbabiliteApresId,
                ProbabiliteAvantId = result.ProbabiliteAvantId,
                Rang = result.Rang,
                PlanAction = result.PlanAction,
                ResponsableAnalyse = result.ResponsableAnalyse,
                DateDeRealisation = result.DateDeRealisation,
                EtatId = result.EtatId
            };
            return vm;

        }

        internal void UpdateLigneAnalyseRisque(LigneAnalyseRisqueEditViewModel larevm, int id)
        {
            var ligneAModifier = ligneAnalyseRisqueRepository.Get(larevm.IdLigneAnalyseRisqueEditor);
            var newLine = false;
            if (ligneAModifier==null)
            {
                ligneAModifier = new LigneAnalyseRisque();
                newLine = true;
            }

            ligneAModifier.AnalyseRisqueId          = id;
            ligneAModifier.ConformiteAuxNormes       = larevm.ConformiteAuNormes;
           // ligneAModifier.Equipement               = larevm.Equipement;
            ligneAModifier.EvenementId              = larevm.EvenementId;
            ligneAModifier.MesurePrevention = larevm.MesurePrevention;
            ligneAModifier.NbPersonnesExposees      = larevm.NbPersonnesExposees;
            ligneAModifier.Operation                = larevm.Operation;
            ligneAModifier.PhenomeneId              = larevm.PhenomeneId;
            ligneAModifier.ReglesEtNormes           = larevm.ReglesEtNormes;
            ligneAModifier.SituationId              = larevm.SituationId;
            ligneAModifier.Zone                     = larevm.Zone;
            ligneAModifier.SystemeCommandeAvant     = larevm.SystemeCommandeAvant;
            ligneAModifier.SystemeCommandeInstalles = larevm.SystemeCommandeInstalles;
            ligneAModifier.IndiceFinalApres         = larevm.IndiceFinalApres;
            ligneAModifier.IndiceFinalAvant         = larevm.IndiceFinalAvant;
            ligneAModifier.FrequenceApresId         = larevm.FrequenceApresId;
            ligneAModifier.FrequenceAvantId         = larevm.FrequenceAvantId;
            ligneAModifier.GraviteApresId           = larevm.GraviteApresId;
            ligneAModifier.GraviteAvantId           = larevm.GraviteAvantId;
            ligneAModifier.PossibiliteApresId       = larevm.PossibiliteApresId;
            ligneAModifier.PossibiliteAvantId       = larevm.PossibiliteAvantId;
            ligneAModifier.ProbabiliteApresId       = larevm.ProbabiliteApresId;
            ligneAModifier.ProbabiliteAvantId       = larevm.ProbabiliteAvantId;
            ligneAModifier.DommagePossibleId        = larevm.DommagePossibleId;
            ligneAModifier.PlanAction               = larevm.PlanAction;
          //  ligneAModifier.NoReference            = larevm.NoReference;
            ligneAModifier.Rang                     = larevm.Rang;
            ligneAModifier.ResponsableAnalyse       = larevm.ResponsableAnalyse;
            ligneAModifier.DateDeRealisation        = larevm.DateDeRealisation;
            ligneAModifier.EtatId                   = larevm.EtatId;

            if (newLine)
            {
                ligneAnalyseRisqueRepository.Add(ligneAModifier);
            }
           
            ligneAnalyseRisqueRepository.SaveChanges();
            larevm.IdLigneAnalyseRisqueEditor = ligneAModifier.LigneAnalyseRisqueId;
            ligneAModifier = ligneAnalyseRisqueRepository.Get(larevm.IdLigneAnalyseRisqueEditor);

            var gravite = ligneAModifier.GraviteAvant == null ? 0 : ligneAModifier.GraviteAvant.Valeur;
            var frequence = ligneAModifier.FrequenceAvant == null ? 0 : ligneAModifier.FrequenceAvant.Valeur;
            var probabilite = ligneAModifier.ProbabiliteAvant == null ? 0 : ligneAModifier.ProbabiliteAvant.Valeur;
            var possibilite = ligneAModifier.PossibiliteAvant == null ? 0 : ligneAModifier.PossibiliteAvant.Valeur;

            ligneAModifier.IndiceFinalAvant = gravite.GetValueOrDefault() + frequence.GetValueOrDefault() +
                probabilite.GetValueOrDefault() + possibilite.GetValueOrDefault();

             gravite = ligneAModifier.GraviteApres == null ? 0 : ligneAModifier.GraviteApres.Valeur;
             frequence = ligneAModifier.FrequenceApres == null ? 0 : ligneAModifier.FrequenceApres.Valeur;
             probabilite = ligneAModifier.ProbabiliteApres == null ? 0 : ligneAModifier.ProbabiliteApres.Valeur;
             possibilite = ligneAModifier.PossibiliteApres == null ? 0 : ligneAModifier.PossibiliteApres.Valeur;

            ligneAModifier.IndiceFinalApres = gravite.GetValueOrDefault() + frequence.GetValueOrDefault() +
                probabilite.GetValueOrDefault() + possibilite.GetValueOrDefault();

            larevm.IndiceFinalApres = ligneAModifier.IndiceFinalApres;
            larevm.IndiceFinalAvant = ligneAModifier.IndiceFinalAvant;
            larevm.ResponsableAnalyse = ligneAModifier.ResponsableAnalyse;
            larevm.DateDeRealisation = ligneAModifier.DateDeRealisation;
            larevm.EtatId = ligneAModifier.EtatId;
            if ( ligneAModifier.PhenomeneId.HasValue)
            {
            var phenomene = phenomeneRepository.Get(ligneAModifier.PhenomeneId.Value);
            larevm.PhenomeneDescription = phenomene.Description;
            }

            ligneAnalyseRisqueRepository.SaveChanges();


        }

        internal void DeleteLigneAnalyseRisque(LigneAnalyseRisqueEditViewModel larevm)
        {
            ligneAnalyseRisqueRepository.Delete(larevm.IdLigneAnalyseRisqueEditor);
            ligneAnalyseRisqueRepository.SaveChanges();
        }
        internal int DupliquerLigneAnalyseRisque(int LigneAnalyseRisqueId)
        {
            {
                var itemBase = ligneAnalyseRisqueRepository.Get(LigneAnalyseRisqueId);

                // infos générales 
                var fiche = getFicheVM(LigneAnalyseRisqueId);
                //var session = utilisateurService.GetSession();
                //fiche.EstDocumentPrevigesst = (session == null);
                fiche.Rang = fiche.Rang;

                fiche.IdLigneAnalyseRisqueEditor = 0;
                UpdateLigneAnalyseRisque(fiche, itemBase.AnalyseRisqueId);

            }

            /* Lignes à dupliquer
            var vm = ligneInstructionService.getLigneVM(lc.LigneInstructionId);

            // var repertoireInitial = @"~/Images/Cadenassage/Instructions/" + lc.LigneInstructionId + "/";

            int valeur = 0;
            vm.FicheCadenassageId = fiche.FicheCadenassageId;

            // on met le pk  de l'image dupliquée
            if (listeImages.ContainsKey(vm.PhotoFicheCadenassageId ?? 0))
                if (listeImages.TryGetValue(vm.PhotoFicheCadenassageId ?? 0, out valeur))
                    vm.PhotoFicheCadenassageId = valeur;

            vm.LigneInstructionId = 0;

            ligneInstructionService.SaveLigneInstruction(vm);
            */
            return LigneAnalyseRisqueId; //fiche.FicheCadenassageId;
            
        }


        // var newRep = @"~/Images/Cadenassage/Instructions/" + vm.LigneInstructionId + "/";
        //createOrCleanRep(newRep);
        // copyFiles(repertoireInitial, newRep);


        /*
        public int DupliquerAnalyse(int AnalyseRisqueId)
        {
            var itemBase = ligneAnalyseRisqueRepository.Get(AnalyseRisqueId);

            // infos générales 
            var fiche = getFicheVM(AnalyseRisqueId);
            var session = utilisateurService.GetSession();
            //fiche.EstDocumentPrevigesst = (session == null);
            fiche.NoFiche = "Copie de " + fiche.NoFiche;

            fiche.FicheCadenassageId = 0;

            SaveFiche(fiche);

            // images
            var listeImages = new Dictionary<int, int>();
            foreach (var li in itemBase.PhotosFicheCadenassage)
            {
                var vm = photoFicheCadenassageService.getVM(li.PhotoFicheCadenassageId);
                var repertoireInitial = @"~/Images/Cadenassage/Photos/" + li.PhotoFicheCadenassageId + "/";
                vm.FicheCadenassageId = fiche.FicheCadenassageId;
                vm.PhotoFicheCadenassageId = 0;
                photoFicheCadenassageService.SaveLigneCadenassagePhoto(vm, true);
                listeImages.Add(li.PhotoFicheCadenassageId, vm.PhotoFicheCadenassageId);
                var newRep = @"~/Images/Cadenassage/Photos/" + vm.PhotoFicheCadenassageId + "/";
                createOrCleanRep(newRep);
                copyFiles(repertoireInitial, newRep);
            }

            // lignes cadenassage

            foreach (var lc in itemBase.LignesInstruction)
            {
                var vm = ligneInstructionService.getLigneVM(lc.LigneInstructionId);

                // var repertoireInitial = @"~/Images/Cadenassage/Instructions/" + lc.LigneInstructionId + "/";

                int valeur = 0;
                vm.FicheCadenassageId = fiche.FicheCadenassageId;

                // on met le pk  de l'image dupliquée
                if (listeImages.ContainsKey(vm.PhotoFicheCadenassageId ?? 0))
                    if (listeImages.TryGetValue(vm.PhotoFicheCadenassageId ?? 0, out valeur))
                        vm.PhotoFicheCadenassageId = valeur;

                vm.LigneInstructionId = 0;

                ligneInstructionService.SaveLigneInstruction(vm);

                // var newRep = @"~/Images/Cadenassage/Instructions/" + vm.LigneInstructionId + "/";
                //createOrCleanRep(newRep);
                // copyFiles(repertoireInitial, newRep);
            }

            //lignes décadenassage

            foreach (var lc in itemBase.LignesDecadenassage)
            {
                var vm = ligneDecadenassageService.getLigneVM(lc.LigneDecadenassageId);

                // var repertoireInitial = @"~/Images/Cadenassage/Decadenassage/" + lc.LigneDecadenassageId + "/";


                vm.FicheCadenassageId = fiche.FicheCadenassageId;
                vm.LigneDecadenassageId = 0;
                // on met le pk  de l'image dupliquée
                int valeur = 0;
                if (listeImages.ContainsKey(vm.PhotoFicheCadenassageId ?? 0))
                    if (listeImages.TryGetValue(vm.PhotoFicheCadenassageId ?? 0, out valeur))
                        vm.PhotoFicheCadenassageId = valeur;


                ligneDecadenassageService.SaveLigneDecadenassage(vm);

                //var newRep = @"~/Images/Cadenassage/Decadenassage/" + vm.LigneDecadenassageId + "/";
                // createOrCleanRep(newRep);
                // copyFiles(repertoireInitial, newRep);

            }

            // matériel

            foreach (var m in itemBase.MaterielsRequisCadenassage)
            {
                var mr = new MaterielRequisCadenassageViewModel { FicheCadenassageId = fiche.FicheCadenassageId, MaterielId = m.MaterielId, Quantite = m.Quantite };
                materielRequisCadenassageService.SaveLigneCadenassageMateriel(mr);

            }


            //sources d'énergie
            var itemSources = new SourceEnergieCadenassageViewModel { FicheCadenassageId = fiche.FicheCadenassageId };
            var listeInt = new List<int>();
            foreach (var s in itemBase.SourcesEnergieCadenassage)
                listeInt.Add(s.SourceEnergieId);
            itemSources.SourcesEnergieId = listeInt;
            this.sourceEnergieCadenassageRepository.UpdateSources(itemSources);

            return fiche.FicheCadenassageId;
        }
        */


        //internal List<String>getEquipements(int AnalyseRisqueId)
        //{
        //   return ligneAnalyseRisqueRepository.AsQueryable().Where(x => x.AnalyseRisqueId == AnalyseRisqueId).OrderBy(x => x.).Select(x => x.Equipement).Distinct().ToList();
        //}

        internal int GetNewValue ( int id)
        {
            var Lignes = ligneAnalyseRisqueRepository.AsQueryable().Where(x => x.AnalyseRisqueId == id).ToList();
            if (Lignes.Count == 0)
                return 1;
            double value = (double) Lignes.Max(x => x.Rang);
            value = value + 1.00001;
            return  ((int)Math.Floor(value));
        }

        internal List<String> getZones(int AnalyseRisqueId)
        {
            return ligneAnalyseRisqueRepository.AsQueryable().Where(x => x.AnalyseRisqueId == AnalyseRisqueId).OrderBy(x => x.Zone).Select(x => x.Zone).Distinct().ToList();
        }

        //internal List<String> getReferences(int AnalyseRisqueId)
        //{
        //    return ligneAnalyseRisqueRepository.AsQueryable().Where(x => x.AnalyseRisqueId == AnalyseRisqueId).OrderBy(x => x.NoReference).Select(x => x.NoReference).Distinct().ToList();
        //}

        internal List<String> getOperations(int AnalyseRisqueId)
        {
            return ligneAnalyseRisqueRepository.AsQueryable().Where(x => x.AnalyseRisqueId == AnalyseRisqueId).OrderBy(x => x.Operation).Select(x => x.Operation).Distinct().ToList();
        }
        internal List<int?> getPhenomeneId(int AnalyseRisqueId)
        {
            return ligneAnalyseRisqueRepository.AsQueryable().Where(x => x.AnalyseRisqueId == AnalyseRisqueId).OrderBy(x => x.PhenomeneId).Select(x => x.PhenomeneId).Distinct().ToList();

        }
        internal List<int> getIndiceFinalAvant(int AnalyseRisqueId)
        {
            return ligneAnalyseRisqueRepository.AsQueryable().Where(x => x.AnalyseRisqueId == AnalyseRisqueId).OrderBy(x => x.IndiceFinalAvant).Select(x => x.IndiceFinalAvant).Distinct().ToList();

        }
        internal List<int> getIndiceFinalApres(int AnalyseRisqueId)
        {
            return ligneAnalyseRisqueRepository.AsQueryable().Where(x => x.AnalyseRisqueId == AnalyseRisqueId).OrderBy(x => x.IndiceFinalApres).Select(x => x.IndiceFinalApres).Distinct().ToList();

        }
        internal List<bool> getConformiteAuxNormes(int AnalyseRisqueId)
        {
            return ligneAnalyseRisqueRepository.AsQueryable().Where(x => x.AnalyseRisqueId == AnalyseRisqueId).OrderBy(x => x.ConformiteAuxNormes).Select(x => x.ConformiteAuxNormes).Distinct().ToList();
        }

        internal List<int?> getEtatId(int AnalyseRisqueId)
        {
            return ligneAnalyseRisqueRepository.AsQueryable().Where(x => x.AnalyseRisqueId == AnalyseRisqueId).OrderBy(x => x.EtatId).Select(x => x.EtatId).Distinct().ToList();

        }
    }
}