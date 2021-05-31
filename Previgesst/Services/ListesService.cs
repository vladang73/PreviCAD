using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Previgesst.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Previgesst.ViewModels;
using Previgesst.Helpers;

namespace Previgesst.Services
{
    public class ListesService
    {
        private PhenomeneRepository pheneomeneRepository;
        private SituationRepository situationRepository;
        private EvenementRepository evenementRepository;
        private TypeReductionRepository typeReductionRepository;
        private DispositifRepository dispositifRepository;
        private AccessoireRepository accessoireRepository;
        private SourceEnergieRepository sourceEnergieRepository;
        private MaterielRepository materielRepository;
        private ReglementRepository reglementRepository;
        private DommageRepository dommageRepository;


        public ListesService(PhenomeneRepository pheneomeneRepository, SituationRepository situationRepository, EvenementRepository evenementRepository,
            TypeReductionRepository typeReductionRepository, DispositifRepository dispositifRepository,
            AccessoireRepository accessoireRepository, SourceEnergieRepository sourceEnergieRepository, MaterielRepository materielRepository,
            ReglementRepository reglementRepository, DommageRepository dommageRepository
            )
        {
            this.pheneomeneRepository = pheneomeneRepository;
            this.situationRepository = situationRepository;
            this.evenementRepository = evenementRepository;
            this.typeReductionRepository = typeReductionRepository;

            this.dispositifRepository = dispositifRepository;
            this.accessoireRepository = accessoireRepository;
            this.sourceEnergieRepository = sourceEnergieRepository;
            this.materielRepository = materielRepository;
            this.reglementRepository = reglementRepository;
            this.dommageRepository = dommageRepository;
        }


        public DataSourceResult GetFilteredPhenomene(DataSourceRequest request)
        {
            mapPhenomeneViemModel(request);

            var result = pheneomeneRepository.AsQueryable().ToList()
                .ToDataSourceResult(request, a => new SimpleListViewModel
                {
                    Id = a.PhenomeneId,
                    Description = a.Description,
                    DescriptionEN = a.DescriptionEN,
                    Suppressible = !pheneomeneRepository.estUtilise(a.PhenomeneId)
                });
            return result;
        }

        public DataSourceResult GetFilteredDommage(DataSourceRequest request)
        {
            mapReglementViemModel(request);

            var result = dommageRepository.AsQueryable().ToList()
                .ToDataSourceResult(request, a => new SimpleListViewModel
                {
                    Id = a.DommagePossibleId,
                    Description = a.Description,
                    DescriptionEN = a.DescriptionEN,
                    Suppressible = !dommageRepository.estUtilise(a.DommagePossibleId)
                });
            return result;
        }

        public DataSourceResult GetFilteredReglement(DataSourceRequest request)
        {
            mapReglementViemModel(request);

            var result = reglementRepository.AsQueryable().ToList()
                .ToDataSourceResult(request, a => new SimpleListViewModel
                {
                    Id = a.ReglementId,
                    Description = a.Description,

                    Suppressible = !reglementRepository.estUtilise(a.ReglementId)
                });
            return result;
        }
        public DataSourceResult GetFilteredSituations(DataSourceRequest request)
        {
            mapSituationViemModel(request);

            var result = situationRepository.AsQueryable().ToList()
                .ToDataSourceResult(request, a => new SimpleListViewModel
                {
                    Id = a.SituationId,
                    Description = a.Description,
                    DescriptionEN = a.DescriptionEN,
                    Suppressible = !situationRepository.estUtilise(a.SituationId)
                });
            return result;
        }

        public DataSourceResult GetFilteredEvenements(DataSourceRequest request)
        {
            mapEvenementViemModel(request);
            var result = evenementRepository.AsQueryable().ToList()
                .ToDataSourceResult(request, a => new SimpleListViewModel
                {
                    Id = a.EvenementId,
                    Description = a.Description,
                    DescriptionEN = a.DescriptionEN,
                    Suppressible = !evenementRepository.estUtilise(a.EvenementId)
                });
            return result;
        }

        public DataSourceResult GetFilteredTypesReduction(DataSourceRequest request)
        {
            mapTypesReductionsViemModel(request);
            var result = typeReductionRepository.AsQueryable().ToList()
                .ToDataSourceResult(request, a => new SimpleListViewModel
                {
                    Id = a.TypeReductionId,
                    Description = a.Description,
                    Suppressible = !typeReductionRepository.estUtilise(a.TypeReductionId)
                });
            return result;
        }



        public DataSourceResult GetFilteredDispositif(DataSourceRequest request)
        {
            mapDispositifViemModel(request);

            var result = dispositifRepository.AsQueryable().ToList()
                .ToDataSourceResult(request, a => new SimpleListViewModel
                {
                    Id = a.DispositifId,
                    Description = a.Description,
                    DescriptionEN = a.DescriptionEN,
                    Suppressible = !dispositifRepository.estUtilise(a.DispositifId)
                });
            return result;
        }

        public DataSourceResult GetFilteredAccessoire(DataSourceRequest request)
        {
            mapAccessoireViemModel(request);

            var result = accessoireRepository.AsQueryable().ToList()
                .ToDataSourceResult(request, a => new SimpleListViewModel
                {
                    Id = a.AccessoireId,
                    Description = a.Description,
                    DescriptionEN = a.DescriptionEN,
                    Suppressible = !accessoireRepository.estUtilise(a.AccessoireId)
                });
            return result;
        }

        public DataSourceResult GetFilteredSourceEnergie(DataSourceRequest request)
        {
            mapSourceEnergieViemModel(request);

            var result = sourceEnergieRepository.AsQueryable().ToList()
                .ToDataSourceResult(request, a => new SimpleListViewModel
                {
                    Id = a.SourceEnergieId,
                    Description = a.Description,
                    DescriptionEN = a.DescriptionEN,
                    Suppressible = !sourceEnergieRepository.estUtilise(a.SourceEnergieId)
                });
            return result;
        }

        public DataSourceResult GetFilteredMateriel(DataSourceRequest request)
        {
            mapMaterielViemModel(request);

            var result = materielRepository.AsQueryable().ToList()
                .ToDataSourceResult(request, a => new SimpleListViewModel
                {
                    Id = a.MaterielId,
                    Description = a.Description,
                    DescriptionEN = a.DescriptionEN,
                    Suppressible = !materielRepository.estUtilise(a.MaterielId)
                });
            return result;
        }


        private void mapPhenomeneViemModel(DataSourceRequest request)
        {
            request.RenameMember("Id", "PhenomeneId");
        }

        private void mapReglementViemModel(DataSourceRequest request)
        {
            request.RenameMember("Id", "ReglementId");
        }

        private void mapDommageViemModel(DataSourceRequest request)
        {
            request.RenameMember("Id", "DommagePossibleId");
        }

        private void mapSituationViemModel(DataSourceRequest request)
        {
            request.RenameMember("Id", "SituationId");
        }

        private void mapEvenementViemModel(DataSourceRequest request)
        {
            request.RenameMember("Id", "EvenementId");
        }

        private void mapTypesReductionsViemModel(DataSourceRequest request)
        {
            request.RenameMember("Id", "TypeReductionId");
        }

        private void mapMesuresViemModel(DataSourceRequest request)
        {
            request.RenameMember("Id", "MesureId");
        }

        private void mapDispositifViemModel(DataSourceRequest request)
        {
            request.RenameMember("Id", "DispositifId");
        }

        private void mapAccessoireViemModel(DataSourceRequest request)
        {
            request.RenameMember("Id", "AccessoireId");
        }

        private void mapSourceEnergieViemModel(DataSourceRequest request)
        {
            request.RenameMember("Id", "SourceEnergieId");
        }
        private void mapMaterielViemModel(DataSourceRequest request)
        {
            request.RenameMember("Id", "MaterielId");
        }

        private void SavePhenomene(SimpleListViewModel model)
        {
            var item = pheneomeneRepository.Get(model.Id);
            if (item == null)
                item = new Models.Phenomene();

            item.Description = model.Description;
            item.DescriptionEN = model.DescriptionEN;

            if (item.PhenomeneId > 0)
                pheneomeneRepository.Update(item);
            else
                pheneomeneRepository.Add(item);

            pheneomeneRepository.SaveChanges();

            model.Id = item.PhenomeneId;
        }

        private void SaveDommage(SimpleListViewModel model)
        {
            var item = dommageRepository.Get(model.Id);
            if (item == null)
                item = new Models.DommagePossible();

            item.Description = model.Description;
            item.DescriptionEN = model.DescriptionEN;

            if (item.DommagePossibleId > 0)
                dommageRepository.Update(item);
            else
                dommageRepository.Add(item);

            dommageRepository.SaveChanges();

            model.Id = item.DommagePossibleId;
        }

        private void SaveReglement(SimpleListViewModel model)
        {
            var item = reglementRepository.Get(model.Id);
            if (item == null)
                item = new Models.Reglement();

            item.Description = model.Description;

            if (item.ReglementId > 0)
                reglementRepository.Update(item);
            else
                reglementRepository.Add(item);

            reglementRepository.SaveChanges();

            model.Id = item.ReglementId;
        }


        private void SaveEvenement(SimpleListViewModel model)
        {
            var item = evenementRepository.Get(model.Id);
            if (item == null)
                item = new Models.Evenement();

            item.Description = model.Description;
            item.DescriptionEN = model.DescriptionEN;
            if (item.EvenementId > 0)
                evenementRepository.Update(item);
            else
                evenementRepository.Add(item);

            evenementRepository.SaveChanges();

            model.Id = item.EvenementId;
        }



        private void SaveSituation(SimpleListViewModel model)
        {
            var item = situationRepository.Get(model.Id);
            if (item == null)
                item = new Models.Situation();

            item.Description = model.Description;
            item.DescriptionEN = model.DescriptionEN;
            if (item.SituationId > 0)
                situationRepository.Update(item);
            else
                situationRepository.Add(item);

            situationRepository.SaveChanges();

            model.Id = item.SituationId;
        }

        private void SaveTypeReduction(SimpleListViewModel model)
        {
            var item = typeReductionRepository.Get(model.Id);
            if (item == null)
                item = new Models.TypeReduction();

            item.Description = model.Description;

            if (item.TypeReductionId > 0)
                typeReductionRepository.Update(item);
            else
                typeReductionRepository.Add(item);

            typeReductionRepository.SaveChanges();

            model.Id = item.TypeReductionId;
        }

        private void SaveDispositif(SimpleListViewModel model)
        {
            var item = dispositifRepository.Get(model.Id);
            if (item == null)
                item = new Models.Dispositif();

            item.Description = model.Description;
            item.DescriptionEN = model.DescriptionEN;
            if (item.DispositifId > 0)
                dispositifRepository.Update(item);
            else
                dispositifRepository.Add(item);

            dispositifRepository.SaveChanges();

            model.Id = item.DispositifId;
        }

        private void SaveAccessoire(SimpleListViewModel model)
        {
            var item = accessoireRepository.Get(model.Id);
            if (item == null)
                item = new Models.Accessoire();

            item.Description = model.Description;
            item.DescriptionEN = model.DescriptionEN;
            if (item.AccessoireId > 0)
                accessoireRepository.Update(item);
            else
                accessoireRepository.Add(item);

            accessoireRepository.SaveChanges();

            model.Id = item.AccessoireId;
        }

        private void SaveSourceEnergie(SimpleListViewModel model)
        {
            var item = sourceEnergieRepository.Get(model.Id);
            if (item == null)
                item = new Models.SourceEnergie();

            item.Description = model.Description;
            item.DescriptionEN = model.DescriptionEN;
            if (item.SourceEnergieId > 0)
                sourceEnergieRepository.Update(item);
            else
                sourceEnergieRepository.Add(item);

            sourceEnergieRepository.SaveChanges();

            model.Id = item.SourceEnergieId;
        }

        private void SaveMateriel(SimpleListViewModel model)
        {
            var item = materielRepository.Get(model.Id);
            if (item == null)
                item = new Models.Materiel();

            item.Description = model.Description;
            item.DescriptionEN = model.DescriptionEN;
            if (item.MaterielId > 0)
                materielRepository.Update(item);
            else
                materielRepository.Add(item);

            materielRepository.SaveChanges();

            model.Id = item.MaterielId;
        }

        public void Enregistrer(SimpleListViewModel model, Enums.TypeListe type)
        {
            switch (type)
            {
                case Enums.TypeListe.phenomene: SavePhenomene(model); break;
                case Enums.TypeListe.evenement: SaveEvenement(model); break;

                case Enums.TypeListe.situation: SaveSituation(model); break;
                case Enums.TypeListe.typereduction: SaveTypeReduction(model); break;
                case Enums.TypeListe.dispositif: SaveDispositif(model); break;
                case Enums.TypeListe.accessoire: SaveAccessoire(model); break;
                case Enums.TypeListe.sourceEnergie: SaveSourceEnergie(model); break;
                case Enums.TypeListe.materiel: SaveMateriel(model); break;
                case Enums.TypeListe.reglement: SaveReglement(model); break;
                case Enums.TypeListe.dommage: SaveDommage(model); break;


            }


        }

        public bool SupprimerPhenomene(SimpleListViewModel model)
        {
            var item = pheneomeneRepository.Get(model.Id);
            if (item == null)
                return true;

            if (pheneomeneRepository.estUtilise(model.Id))
                return false;
            pheneomeneRepository.Delete(item.PhenomeneId);
            pheneomeneRepository.SaveChanges();
            return true;
        }

        public bool SupprimerDommage(SimpleListViewModel model)
        {
            var item = dommageRepository.Get(model.Id);
            if (item == null)
                return true;

            if (dommageRepository.estUtilise(model.Id))
                return false;
            dommageRepository.Delete(item.DommagePossibleId);
            dommageRepository.SaveChanges();
            return true;
        }

        public bool SupprimerReglement(SimpleListViewModel model)
        {
            var item = reglementRepository.Get(model.Id);
            if (item == null)
                return true;

            if (reglementRepository.estUtilise(model.Id))
                return false;
            reglementRepository.Delete(item.ReglementId);
            reglementRepository.SaveChanges();
            return true;
        }

        public bool SupprimerEvenement(SimpleListViewModel model)
        {
            var item = evenementRepository.Get(model.Id);
            if (item == null)
                return true;

            if (evenementRepository.estUtilise(model.Id))
                return false;
            evenementRepository.Delete(item.EvenementId);
            evenementRepository.SaveChanges();
            return true;

        }


        public bool SupprimerSituation(SimpleListViewModel model)
        {
            var item = situationRepository.Get(model.Id);
            if (item == null)
                return true;

            if (situationRepository.estUtilise(model.Id))
                return false;
            situationRepository.Delete(item.SituationId);
            situationRepository.SaveChanges();
            return true;
        }
        public bool SupprimerTypeReduction(SimpleListViewModel model)
        {
            var item = typeReductionRepository.Get(model.Id);
            if (item == null)
                return true;

            if (typeReductionRepository.estUtilise(model.Id))
                return false;
            typeReductionRepository.Delete(item.TypeReductionId);
            typeReductionRepository.SaveChanges();
            return true;
        }

        public bool SupprimerDispositif(SimpleListViewModel model)
        {
            var item = dispositifRepository.Get(model.Id);
            if (item == null)
                return true;

            if (dispositifRepository.estUtilise(model.Id))
                return false;
            dispositifRepository.Delete(item.DispositifId);
            dispositifRepository.SaveChanges();
            return true;
        }

        public bool SupprimerAccessoire(SimpleListViewModel model)
        {
            var item = accessoireRepository.Get(model.Id);
            if (item == null)
                return true;

            if (accessoireRepository.estUtilise(model.Id))
                return false;
            accessoireRepository.Delete(item.AccessoireId);
            accessoireRepository.SaveChanges();
            return true;
        }

        public bool SupprimerSourceEnergie(SimpleListViewModel model)
        {
            var item = sourceEnergieRepository.Get(model.Id);
            if (item == null)
                return true;

            if (sourceEnergieRepository.estUtilise(model.Id))
                return false;
            sourceEnergieRepository.Delete(item.SourceEnergieId);
            sourceEnergieRepository.SaveChanges();
            return true;
        }


        public bool SupprimerMateriel(SimpleListViewModel model)
        {
            var item = materielRepository.Get(model.Id);
            if (item == null)
                return true;

            if (materielRepository.estUtilise(model.Id))
                return false;
            materielRepository.Delete(item.MaterielId);
            materielRepository.SaveChanges();
            return true;
        }

        public bool Supprimer(SimpleListViewModel model, Enums.TypeListe type)
        {

            switch (type)
            {
                case Enums.TypeListe.phenomene: return (SupprimerPhenomene(model));
                case Enums.TypeListe.evenement: return (SupprimerEvenement(model));

                case Enums.TypeListe.situation: return (SupprimerSituation(model));
                case Enums.TypeListe.typereduction: return (SupprimerTypeReduction(model));
                case Enums.TypeListe.dispositif: return (SupprimerDispositif(model));
                case Enums.TypeListe.accessoire: return (SupprimerAccessoire(model));
                case Enums.TypeListe.sourceEnergie: return (SupprimerSourceEnergie(model));
                case Enums.TypeListe.materiel: return (SupprimerMateriel(model));
                case Enums.TypeListe.reglement: return (SupprimerReglement(model));
                case Enums.TypeListe.dommage: return (SupprimerDommage(model));

            }
            return true;

        }


        #region ----- For Multiselect in Popup ----- 


        public List<SimpleListViewModel> GetSourceEnergie()
        {
            return sourceEnergieRepository.AsQueryable()
                                          .Select(a => new SimpleListViewModel
                                          {
                                              Id = a.SourceEnergieId,
                                              Description = a.Description,
                                              DescriptionEN = a.DescriptionEN,
                                              //Suppressible = !sourceEnergieRepository.estUtilise(a.SourceEnergieId)
                                          })
                                          .ToList();
        }
        public List<SimpleListViewModel> GetDispositif()
        {
            return dispositifRepository.AsQueryable()
                                          .Select(a => new SimpleListViewModel
                                          {
                                              Id = a.DispositifId,
                                              Description = a.Description,
                                              DescriptionEN = a.DescriptionEN,
                                              //Suppressible = !sourceEnergieRepository.estUtilise(a.SourceEnergieId)
                                          })
                                          .ToList();
        }

        public List<SimpleListViewModel> GetAccessoire()
        {
            return accessoireRepository.AsQueryable()
                                          .Select(a => new SimpleListViewModel
                                          {
                                              Id = a.AccessoireId,
                                              Description = a.Description,
                                              DescriptionEN = a.DescriptionEN,
                                              //Suppressible = !sourceEnergieRepository.estUtilise(a.SourceEnergieId)
                                          })
                                          .ToList();
        }

        public List<SimpleListViewModel> GetMateriel()
        {
            return materielRepository.AsQueryable()
                                          .Select(a => new SimpleListViewModel
                                          {
                                              Id = a.MaterielId,
                                              Description = a.Description,
                                              DescriptionEN = a.DescriptionEN,
                                              //Suppressible = !sourceEnergieRepository.estUtilise(a.SourceEnergieId)
                                          })
                                          .ToList();
        }
        #endregion


    }


}