using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Previgesst.Repositories;
using Previgesst.ViewModels;
using Kendo.Mvc.Extensions;
using Previgesst.Models;
using Previgesst.Helpers;
using Previgesst.Ressources.Analyse;

namespace Previgesst.Services
{
    public class AnalyseRisqueService
    {
        private AnalyseRisqueRepository analyseRisqueRepository;
        private LigneAnalyseRisqueRepository ligneAnalyseRisqueRepository;
        private UtilisateurService utilisateurService;

        public AnalyseRisqueService(AnalyseRisqueRepository analyseRisqueRepository, LigneAnalyseRisqueRepository ligneAnalyseRisqueRepository,
            UtilisateurService utilisateurService)
        {
            this.analyseRisqueRepository = analyseRisqueRepository;
            this.ligneAnalyseRisqueRepository = ligneAnalyseRisqueRepository;
            this.utilisateurService = utilisateurService;
        }

        internal DataSourceResult GetReadListAnalyseRisque(DataSourceRequest request)
        {
            var estUsagerPrevi = (utilisateurService.GetSession() == null);
            var result = analyseRisqueRepository.GetAll().Select(ar => new AnalyseRisqueListViewModel()
            {
                Id = ar.AnalyseRisqueId,
                NomClient = ar.Client.Nom,
                Createur = ar.Createur,
                Participants = ar.Participants,
                NoRef = ar.Description,
                DateCreation = ar.DateCreation,
                DateMAJ = ar.DateMiseAJour,
                UserMAJ = ar.UserMAJ,
                AfficherChezClient = ar.AfficherChezClient,
                estDocumentPrevigesst = ar.estDocumentPrevigesst,
                Editable = ar.estDocumentPrevigesst == false || estUsagerPrevi,
                AnalyseRisqueId = ar.AnalyseRisqueId

            }).ToDataSourceResult(request);

            return result;
        }


        internal DataSourceResult GetReadListAnalyseRisque(DataSourceRequest request, int clientId)
        {
            var utilisateurSession = utilisateurService.GetSession();

            var estAdmin = HttpContext.Current.User.IsInRole("Administrateur") || HttpContext.Current.User.IsInRole("Lecture-Écriture");

            var estUsagerPrevi = (utilisateurSession == null);

            //seul l'usager prévi admin pourra supprimer et modifier une analyse de risques

            DataSourceResult result;
            if (estUsagerPrevi)
            {

                result = analyseRisqueRepository.AsQueryable().Where(x => x.ClientId == clientId).Select(ar => new AnalyseRisqueListViewModel()
                {
                    Id = ar.AnalyseRisqueId,
                    NomClient = ar.Client.Nom,
                    Createur = ar.Createur,
                    DateCreation = ar.DateCreation,
                    Participants = ar.Participants,
                    NoRef = ar.Description,
                    Equipement = ar.Equipement,
                    DateMAJ = ar.DateMiseAJour,
                    UserMAJ = ar.UserMAJ,
                    AfficherChezClient = ar.AfficherChezClient,
                    estDocumentPrevigesst = ar.estDocumentPrevigesst,
                    Editable = estAdmin,
                    Suppressible = estAdmin,
                    AnalyseRisqueId = ar.AnalyseRisqueId
                }).ToDataSourceResult(request);
            }
            else
            {
                var peutSupprimer = utilisateurSession.AdmAnalyseRisque;

                result = analyseRisqueRepository.AsQueryable().Where(x => x.ClientId == clientId && x.AfficherChezClient == true).Select(ar => new AnalyseRisqueListViewModel()
                {
                    Id = ar.AnalyseRisqueId,
                    NomClient = ar.Client.Nom,
                    Createur = ar.Createur,
                    DateCreation = ar.DateCreation,
                    Participants = ar.Participants,
                    NoRef = ar.Description,
                    DateMAJ = ar.DateMiseAJour,
                    UserMAJ = ar.UserMAJ,
                    AfficherChezClient = ar.AfficherChezClient,
                    estDocumentPrevigesst = ar.estDocumentPrevigesst,
                    Equipement = ar.Equipement,
                    Editable = ar.estDocumentPrevigesst == false && peutSupprimer,
                    Suppressible = ar.estDocumentPrevigesst == false && peutSupprimer,
                    AnalyseRisqueId = ar.AnalyseRisqueId
                }).ToDataSourceResult(request);
            }



            return result;
        }

        internal int CreateAnalyseRisque(AnalyseRisqueCreateViewModel arcvm)
        {
            var nouvelleAnalyseRisque = new AnalyseRisque();

            nouvelleAnalyseRisque.AfficherChezClient = arcvm.AfficherChezClient;
            nouvelleAnalyseRisque.ClientId = arcvm.ClientId;
            nouvelleAnalyseRisque.Createur = arcvm.Createur;
            nouvelleAnalyseRisque.Description = arcvm.NoRef;
            nouvelleAnalyseRisque.DateCreation = DateTime.Now;
            // TODO MAX : Save as null the DateMiseAJour
            //nouvelleAnalyseRisque.DateMiseAJour = DateTime.Now;
            nouvelleAnalyseRisque.Equipement = arcvm.Equipement;

            if (utilisateurService.GetSession() == null)
                nouvelleAnalyseRisque.estDocumentPrevigesst = true;
            else
                nouvelleAnalyseRisque.estDocumentPrevigesst = false;


            analyseRisqueRepository.Add(nouvelleAnalyseRisque);
            analyseRisqueRepository.SaveChanges();

            return nouvelleAnalyseRisque.AnalyseRisqueId;
        }

        internal void UpdateAnalyseRisque(AnalyseRisqueEditViewModel arevm)
        {

            var analyseRisque = analyseRisqueRepository.Get(arevm.Id);
            var user = "";
            if (utilisateurService.GetSession() != null)
            {// on est dans le mode BLEU, accès administratif mais Client
                user = utilisateurService.GetSession().NomUtilisateur;
                analyseRisque.AfficherChezClient = true;

            }
            else
            {// version super admin
                user = System.Web.HttpContext.Current.User.Identity.Name;
                analyseRisque.AfficherChezClient = arevm.AfficherChezClient;
            }
            analyseRisque.Description = arevm.NoRef;
            analyseRisque.UserMAJ = user;
            analyseRisque.Participants = arevm.Participants;
            analyseRisque.DateMiseAJour = DateTime.Now;
            analyseRisque.Equipement = arevm.Equipement;

            analyseRisqueRepository.SaveChanges();
        }

        public string getTextFromIndice(int indice)
        {
            if (indice < 7)
                return "--";
            if (indice <= 9)
                return "S";
            if (indice <= 14)
                return "1";
            if (indice <= 24)
                return "2";
            if (indice <= 35)
                return "3";
            if (indice <= 44)
                return "4";
            return "5";
        }

        internal void ReturnFile(int id, bool ToutesAnalyses = false, string langue = "fr")
        {
            // si ToutesAnalyses = false, id= id de l'analyse.  Sinon, id = id du client.

            string templateName = HttpContext.Current.Server.MapPath("~/Templates/AnalyseRisques" + (langue == "fr" ? "" : "en") + ".xlsx");

            List<LigneAnalyseRapport> datalist;
            if (!ToutesAnalyses)
                datalist = analyseRisqueRepository.GetAnalyse(id, langue);
            else
                datalist = analyseRisqueRepository.GetAllAnalyses(id, langue);


            using (var source = System.IO.File.OpenRead(templateName))
            {

                using (var excel = new OfficeOpenXml.ExcelPackage(source))
                {
                    //var datalist = analyseRisqueRepository.GetAnalyse(id);

                    var ws = excel.Workbook.Worksheets[1];
                    var ws2 = excel.Workbook.Worksheets[2];

                    for (int i = 0; i < datalist.Count(); i++)
                    {
                        string tempPhenomene = "";
                        string tempEvenement = "";
                        string tempDommage = "";

                        if (!string.IsNullOrEmpty(datalist[i].Phenomene)) tempPhenomene = datalist[i].Phenomene.Trim();
                        if (!string.IsNullOrEmpty(datalist[i].Evenement)) tempEvenement = datalist[i].Evenement.Trim();
                        if (!string.IsNullOrEmpty(datalist[i].Dommage)) tempDommage = datalist[i].Dommage.Trim();

                        var phenomene = (tempPhenomene.Length >= 4 && tempPhenomene.Substring(4, 1) == "-") ? tempPhenomene.Substring(4) : tempPhenomene;
                        var evenement = (tempEvenement.Length >= 4 && tempEvenement.Substring(4, 1) == "-") ? tempEvenement.Substring(4) : tempEvenement;
                        var dommage = (tempDommage.Length >= 4 && tempDommage.Substring(4, 1) == "-") ? tempDommage.Substring(4) : tempDommage;

                        ws.Cells[i + 4, 1].Value = datalist[i].NoReference;
                        ws.Cells[i + 4, 2].Value = datalist[i].Equipement;
                        ws.Cells[i + 4, 3].Value = datalist[i].Operation;
                        ws.Cells[i + 4, 4].Value = datalist[i].Zone;

                        ws.Cells[i + 4, 5].Value = phenomene;
                        ws.Cells[i + 4, 6].Value = datalist[i].Situation;
                        ws.Cells[i + 4, 7].Value = evenement;
                        ws.Cells[i + 4, 8].Value = dommage;

                        ws.Cells[i + 4, 9].Value = datalist[i].GraviteAvant;
                        ws.Cells[i + 4, 10].Value = datalist[i].FrequenceAvant;
                        ws.Cells[i + 4, 11].Value = datalist[i].ProbabiliteAvant;
                        ws.Cells[i + 4, 12].Value = datalist[i].PossibiliteAvant;
                        ws.Cells[i + 4, 13].Value = getTextFromIndice(datalist[i].IndiceFinalAvant);
                        ws.Cells[i + 4, 14].Value = datalist[i].NbPersonnesExposees;
                        ws.Cells[i + 4, 15].Value = datalist[i].SystemeCommandeAvant;


                        ws.Cells[i + 4, 16].Value = datalist[i].Reglement;
                        ws.Cells[i + 4, 17].Value = datalist[i].Mesure;
                        ws.Cells[i + 4, 18].Value = datalist[i].GraviteApres;
                        ws.Cells[i + 4, 19].Value = datalist[i].FrequenceApres;
                        ws.Cells[i + 4, 20].Value = datalist[i].ProbabiliteApres;
                        ws.Cells[i + 4, 21].Value = datalist[i].PossibiliteApres;
                        ws.Cells[i + 4, 22].Value = getTextFromIndice(datalist[i].IndiceFinalApres);
                        ws.Cells[i + 4, 23].Value = datalist[i].SystemeCommandeInstalles;
                        ws.Cells[i + 4, 24].Value = datalist[i].ConformiteAuNormes;

                    }

                    var ligne = 0;

                    //2 e onglet
                    var datalist2 = datalist.Where(x => (x.PlanAction ?? "") != "").ToList();
                    for (int i = 0; i < datalist2.Count(); i++)
                    {
                        // if ((datalist[i].PlanAction ?? "") != "")
                        {
                            ws2.Cells[ligne + 4, 1].Value = datalist2[i].NoReference;
                            ws2.Cells[ligne + 4, 2].Value = datalist2[i].Equipement;
                            ws2.Cells[ligne + 4, 3].Value = datalist2[i].Operation;
                            ws2.Cells[ligne + 4, 4].Value = datalist2[i].Zone;
                            ws2.Cells[ligne + 4, 5].Value = datalist2[i].PlanAction;
                            ws2.Cells[ligne + 4, 6].Value = datalist2[i].ResponsableAnalyse;
                            if (datalist2[i].DateDeRealisation.HasValue)
                                ws2.Cells[ligne + 4, 7].Value = datalist2[i].DateDeRealisation;
                            ws2.Cells[ligne + 4, 8].Value = datalist2[i].EtatId;
                            ligne++;
                        }

                    }



                    // Merge des lignes pour les colonnes 1 à 3
                    // On ne peut pas re-merger une cellule déjà mergée.  On doit donc aller jusqu'en bas de 
                    // la zone qui se répète.  Si la colonne 1 est mergée de sur ligneA à ligneB, on regarde pour merger la deuxième
                    // colonne.


                    //merge onglet 1

                    var j = datalist.Count - 1;
                    var trouve = false;
                    var debut = datalist.Count - 1;

                    while (j > 0)
                    {
                        debut = j;
                        while (j > 0 && datalist[j].NoReference == datalist[j - 1].NoReference)
                        {
                            trouve = true;
                            j = j - 1;

                        }
                        if (trouve)
                        {
                            ws.Cells[4 + j, 1, 4 + debut, 1].Merge = true;
                            var k = debut;
                            var trouveN2 = false;
                            var debutN2 = debut;

                            while (k > j)
                            {

                                while (k > j && datalist[k].Equipement == datalist[k - 1].Equipement)
                                {
                                    trouveN2 = true;
                                    k = k - 1;

                                }
                                if (trouveN2)
                                {
                                    ws.Cells[4 + k, 2, 4 + debutN2, 2].Merge = true;
                                    // troisieme niveau

                                    var trouveN3 = false;
                                    var debutN3 = debutN2;
                                    var l = debut;

                                    while (l > k)
                                    {
                                        while (l > k && datalist[l].Operation == datalist[l - 1].Operation)
                                        {
                                            trouveN3 = true;
                                            l = l - 1;
                                        }
                                        if (trouveN3)
                                        {
                                            ws.Cells[4 + l, 3, 4 + debutN3, 3].Merge = true;

                                        }
                                        else
                                        {
                                            l = l - 1;
                                        }

                                        trouveN3 = false;
                                        debutN3 = l;
                                    }


                                }


                                trouveN2 = false;
                                debutN2 = k;
                                k = k - 1;
                            }

                            trouve = false;

                        }
                        j = j - 1;
                    }

                    // merge onglet 2, c'est pas joli!

                    j = datalist2.Count() - 1;
                    trouve = false;
                    debut = datalist2.Count - 1;

                    while (j > 0)
                    {
                        debut = j;
                        while (j > 0 && datalist2[j].NoReference == datalist2[j - 1].NoReference)
                        {
                            trouve = true;
                            j = j - 1;

                        }
                        if (trouve)
                        {
                            ws2.Cells[4 + j, 1, 4 + debut, 1].Merge = true;
                            var k = debut;
                            var trouveN2 = false;
                            var debutN2 = debut;

                            while (k > j)
                            {

                                while (k > j && datalist2[k].Equipement == datalist2[k - 1].Equipement)
                                {
                                    trouveN2 = true;
                                    k = k - 1;

                                }
                                if (trouveN2)
                                {
                                    try
                                    {
                                        ws2.Cells[4 + k, 2, 4 + debutN2, 2].Merge = true;
                                    }
                                    catch
                                    {
                                        // TODO: need to fix the error of the merging
                                    }
                                    // troisieme niveau

                                    var trouveN3 = false;
                                    var debutN3 = debutN2;
                                    var l = debut;

                                    while (l > k)
                                    {
                                        while (l > k && datalist2[l].Operation == datalist2[l - 1].Operation)
                                        {
                                            trouveN3 = true;
                                            l = l - 1;
                                        }
                                        if (trouveN3)
                                        {
                                            ws2.Cells[4 + l, 3, 4 + debutN3, 3].Merge = true;

                                        }
                                        else
                                        {
                                            l = l - 1;
                                        }

                                        trouveN3 = false;
                                        debutN3 = l;
                                    }


                                }


                                trouveN2 = false;
                                debutN2 = k;
                                k = k - 1;
                            }

                            trouve = false;

                        }
                        j = j - 1;
                    }









                    excel.Workbook.Properties.Title = ARLignesRES.Analyse;
                    HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    HttpContext.Current.Response.AddHeader(
                              "content-disposition",
                              string.Format("attachment;  filename={0}", ARLignesRES.TitreFichier + ".xlsx"));
                    HttpContext.Current.Response.BinaryWrite(excel.GetAsByteArray());

                }
            }
        }
        internal AnalyseRisqueEditViewModel GetAnalyseRisqueEditViewModel(int id)
        {
            var arevm = new AnalyseRisqueEditViewModel();
            var analyseRisque = GetAnalyseRisque(id);

            arevm.Id = analyseRisque.AnalyseRisqueId;
            arevm.AfficherChezClient = analyseRisque.AfficherChezClient;
            arevm.DateMAJ = analyseRisque.DateMiseAJour.ToString();
            arevm.DateCreation = analyseRisque.DateCreation.ToString();
            arevm.NomClient = analyseRisque.Client.Nom;
            arevm.Createur = analyseRisque.Createur;
            arevm.UserMAJ = analyseRisque.UserMAJ;
            arevm.Participants = analyseRisque.Participants;
            arevm.NoRef = analyseRisque.Description;
            arevm.ClientId = analyseRisque.Client.ClientId;
            arevm.Equipement = analyseRisque.Equipement;
            return arevm;
        }

        internal DataSourceResult GetReadListLigneAnalyseRisque(DataSourceRequest request, int id)
        {
            mapLigneAnalyseRisqueEditViewModel(request);

            var analyseRisque = analyseRisqueRepository.Get(id);

            var result = analyseRisque.LignesAnalyseRisque.OrderBy(x => x.Rang).Select(lar => new LigneAnalyseRisqueEditViewModel()
            {
                ConformiteAuNormes = lar.ConformiteAuxNormes,
                //Equipement = lar.Equipement,
                EvenementId = lar.EvenementId,
                IdLigneAnalyseRisqueEditor = lar.LigneAnalyseRisqueId,
                IndiceFinalApres = lar.IndiceFinalApres,
                IndiceFinalAvant = lar.IndiceFinalAvant,
                MesurePrevention = lar.MesurePrevention,
                NbPersonnesExposees = lar.NbPersonnesExposees,
                Operation = lar.Operation,
                PhenomeneId = lar.PhenomeneId,
                ReglesEtNormes = lar.ReglesEtNormes,
                SituationId = lar.SituationId,
                SystemeCommandeAvant = lar.SystemeCommandeAvant,
                SystemeCommandeInstalles = lar.SystemeCommandeInstalles,
                Zone = lar.Zone,
                FrequenceApresId = lar.FrequenceApresId,
                FrequenceAvantId = lar.FrequenceAvantId,
                GraviteApresId = lar.GraviteApresId,
                GraviteAvantId = lar.GraviteAvantId,
                PossibiliteApresId = lar.PossibiliteApresId,
                PossibiliteAvantId = lar.PossibiliteAvantId,
                ProbabiliteApresId = lar.ProbabiliteApresId,
                ProbabiliteAvantId = lar.ProbabiliteAvantId,
                DommagePossibleId = lar.DommagePossibleId,
                PhenomeneDescription = lar?.Phenomene?.Description ?? "",
                //NoReference = lar.NoReference,
                Rang = lar.Rang,
                PlanAction = lar.PlanAction,
                ResponsableAnalyse = lar.ResponsableAnalyse,
                DateDeRealisation = lar.DateDeRealisation,
                EtatId = lar.EtatId
            }).ToDataSourceResult(request);

            return result;
        }

        private void mapLigneAnalyseRisqueEditViewModel(DataSourceRequest request)
        {
            request.RenameMember(nameof(LigneAnalyseRisqueEditViewModel.PhenomeneDescription),
                nameof(LigneAnalyseRisque.Phenomene) + "." +
                nameof(Phenomene.Description));
        }

        private AnalyseRisque GetAnalyseRisque(int id)
        {
            return analyseRisqueRepository.Get(id);
        }

        public bool Supprimer(AnalyseRisqueListViewModel model)
        {
            var fiche = analyseRisqueRepository.Get(model.Id);
            if (fiche == null)
                return true;

            ligneAnalyseRisqueRepository.SupprimerDeFicheAnalyse(fiche.AnalyseRisqueId);

            analyseRisqueRepository.Delete(fiche.AnalyseRisqueId);

            analyseRisqueRepository.SaveChanges();

            return true;
        }



        public void RenumeroterLignes(int Id)
        {
            var fiche = analyseRisqueRepository.Get(Id);
            int i = 1;
            foreach (var v in fiche.LignesAnalyseRisque.Where(x => x.Zone != null || x.Operation != null).OrderBy(x => x.Rang))
            {
                v.Rang = i;
                i++;
            }
            analyseRisqueRepository.SaveChanges();
        }

    }
}