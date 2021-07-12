using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Previgesst.Repositories;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Previgesst.Services
{
    public class UtilisateurService
    {
        UtilisateurRepository utilisateurRepository;
        ClientRepository clientRepository;
        private FicheCadenassageRepository ficheCadenassageRepository;
        private AnalyseRisqueRepository analyseRisqueRepository;
        private AccountService accountService;
        private ClientApplicationPreviRepository clientApplicationPreviRepository;
        private ApplicationPrevisService applicationPreviService;
        private EquipementRepository equipementRepository;
        private LigneInstructionRepository ligneInstructionRepository;
        private LigneDecadenassageRepository ligneDecadenassageRepository;
        private PhotoFicheCadenassageRepository photoFicheCadenassageRepository;
        private DepartementRepository departementRepository;
        // private DataSourceRequest request;

        //    private FicheCadenassageService ficheCadenassageService;


        public UtilisateurService(UtilisateurRepository utilisateurRepository, ClientRepository clientRepository,
            FicheCadenassageRepository ficheCadenassageRepository, AccountService accountService,
            AnalyseRisqueRepository analyseRisqueRepository,
            ClientApplicationPreviRepository clientApplicationPreviRepository,
         ApplicationPrevisService applicationPreviService,
         EquipementRepository equipementRepository,
         LigneInstructionRepository ligneInstructionRepository,
         LigneDecadenassageRepository ligneDecadenassageRepository,
         PhotoFicheCadenassageRepository photoFicheCadenassageRepository,
         DepartementRepository departementRepository



            )
        {
            this.utilisateurRepository = utilisateurRepository;
            this.clientRepository = clientRepository;
            this.ficheCadenassageRepository = ficheCadenassageRepository;
            this.accountService = accountService;
            this.analyseRisqueRepository = analyseRisqueRepository;
            this.applicationPreviService = applicationPreviService;
            this.clientApplicationPreviRepository = clientApplicationPreviRepository;
            this.equipementRepository = equipementRepository;
            this.ligneDecadenassageRepository = ligneDecadenassageRepository;
            this.ligneInstructionRepository = ligneInstructionRepository;
            this.photoFicheCadenassageRepository = photoFicheCadenassageRepository;
            this.departementRepository = departementRepository;
            // this.ficheCadenassageService = ficheCadenassageService;



        }


        public Sessions.sessionInfo GetSession()
        {
            Sessions.sessionInfo s = (Sessions.sessionInfo)HttpContext.Current.Session["utilisateur"];
            return s ;
        }

        public void LogOff()
        {

            HttpContext.Current.Session["utilisateur"] = null;
            accountService.SignOut();
        }

        public bool ForceRedirect()
        {
            var nomUser = System.Web.HttpContext.Current.User.Identity.Name;
            if (nomUser == "CDEUser" &&  GetSession()== null)
            {
                return true;
            }
            return false;
       
        }
        [Authorize]
        public Boolean VerifierBonClientCadenassage_Equipement(int equipementId, bool ModeUpdateSeulement)
        {
            //Redirection si CDEUser et pas de session
            if (ForceRedirect())
            {
                RedirectToClientLogin();
                return false;
            }

            var session = GetSession();
            if (session == null)
         
            {
                //  RedirectToClientLogin(res);
                //  return false;
                return true;
            }
            var equipement = equipementRepository.AsQueryable().Where(x => x.EquipementId == equipementId).FirstOrDefault();
            if (equipement == null)
            {
                RedirectToClientLogin();
                return false;
            }
            var vm = getAccesClientVM(equipement.ClientId);
            if (equipement.ClientId == session.ClientId && vm.AccessCadenassage && (ModeUpdateSeulement == false || (ModeUpdateSeulement && (session.AdmPrevicad))))
            {
                return true;
            }
            RedirectToClientLogin();
            return false;
        }

        [Authorize]
        public Boolean VerifierBonClientCadenassage_Instruction(int ligneInstructionId, bool ModeUpdateSeulement)
        {
            //Redirection si CDEUser et pas de session
            if (ForceRedirect())
            {
                RedirectToClientLogin();
                return false;
            }

            var session = GetSession();
            if (session == null)
            {
                //  RedirectToClientLogin(res);
                //  return false;
                return true;
            }
            var ligne = ligneInstructionRepository.AsQueryable().Where(x => x.LigneInstructionId == ligneInstructionId).FirstOrDefault();
            if (ligne == null)
            {
                RedirectToClientLogin();
                return false;
            }
            var vm = getAccesClientVM(ligne.FicheCadenassage.ClientId);
            if (ligne.FicheCadenassage.ClientId == session.ClientId && vm.AccessCadenassage && (ModeUpdateSeulement == false || (ModeUpdateSeulement && (session.AdmPrevicad ))))
            {
                return true;
            }
            RedirectToClientLogin();
            return false;
        }

        [Authorize]
        public Boolean VerifierBonClientCadenassage_PhotoCadenassage(int photoFicheCadenassageId, bool ModeUpdateSeulement)
        {
            //Redirection si CDEUser et pas de session
            if (ForceRedirect())
            {
                RedirectToClientLogin();
                return false;
            }

            var session = GetSession();
            if (session == null)
            {
                //  RedirectToClientLogin(res);
                //  return false;
                return true;
            }
            var ligne = photoFicheCadenassageRepository.AsQueryable().Where(x => x.PhotoFicheCadenassageId == photoFicheCadenassageId).FirstOrDefault();
            if (ligne == null)
            {
                RedirectToClientLogin();
                return false;
            }
            var vm = getAccesClientVM(ligne.FicheCadenassage.ClientId);
            if (ligne.FicheCadenassage.ClientId == session.ClientId && vm.AccessCadenassage && (ModeUpdateSeulement == false || (ModeUpdateSeulement && (session.AdmPrevicad ))))
            {
                return true;
            }
            RedirectToClientLogin();
            return false;
        }


        [Authorize]
        public Boolean VerifierBonClientCadenassage_LigneDecadenassage(int ligneDecadenassageId, bool ModeUpdateSeulement)
        {
            //Redirection si CDEUser et pas de session
            if (ForceRedirect())
            {
                RedirectToClientLogin();
                return false;
            }

            var session = GetSession();
            if (session == null)
            {
                //  RedirectToClientLogin(res);
                //  return false;
                return true;
            }
            var ligne = ligneDecadenassageRepository.AsQueryable().Where(x => x.LigneDecadenassageId == ligneDecadenassageId).FirstOrDefault();
            if (ligne == null)
            {
                RedirectToClientLogin();
                return false;
            }
            var vm = getAccesClientVM(ligne.FicheCadenassage.ClientId);
            if (ligne.FicheCadenassage.ClientId == session.ClientId && vm.AccessCadenassage && (ModeUpdateSeulement == false || (ModeUpdateSeulement && (session.AdmPrevicad ))))
            {
                return true;
            }
            RedirectToClientLogin();
            return false;
        }



        [Authorize]
        public Boolean VerifierBonClientCadenassage_Fiche(int ficheId, bool ModeUpdateSeulement)
        {
            //Redirection si CDEUser et pas de session
            if (ForceRedirect())
            {
                RedirectToClientLogin();
                return false;
            }

            var session = GetSession();
            if (session == null)
            {
                //  RedirectToClientLogin(res);
                //  return false;
                return true;
            }
            var fiche = ficheCadenassageRepository.AsQueryable().Where(x => x.FicheCadenassageId == ficheId).FirstOrDefault();
            if (fiche == null)
            {
                RedirectToClientLogin();
                return false;
            }
            var vm = getAccesClientVM(fiche.ClientId);
            if (fiche.ClientId == session.ClientId && vm.AccessCadenassage && (ModeUpdateSeulement == false || (ModeUpdateSeulement && (session.AdmPrevicad ))))
            {
                return true;
            }
            RedirectToClientLogin();
            return false;
        }


        [Authorize]
        public Boolean VerifierBonClientAnalyse_Client(int clientId, bool ModeUpdateSeulement)
        { //Redirection si CDEUser et pas de session
            if (ForceRedirect())
            {
                RedirectToClientLogin();
                return false;
            }

            var session = GetSession();

            if (session == null)
            {
                return true;
                //    RedirectToClientLogin(res);
                //  return false;
            }

            var vm = getAccesClientVM(clientId);
            if (clientId == session.ClientId && vm.AccessAnalyse && (ModeUpdateSeulement == false || (ModeUpdateSeulement && (session.AdmPrevicad ))))
                return true;


            RedirectToClientLogin();
            return false;

        }
        [Authorize]
        public Boolean VerifierBonClientAnalyse_Risque(int analysteId, bool ModeUpdateSeulement)
        { //Redirection si CDEUser et pas de session
            if (ForceRedirect())
            {
                RedirectToClientLogin();
                return false;
            }
            var session = GetSession();
            if (session == null)
            {
                return true;
            }
            var analyse = analyseRisqueRepository.AsQueryable().Where(x => x.AnalyseRisqueId == analysteId).FirstOrDefault();
            if (analyse == null)
            {
                RedirectToClientLogin();
                return false;

            }
            var vm = getAccesClientVM(analyse.ClientId);
            if (ModeUpdateSeulement)
            {

            }

            if (analyse.ClientId == session.ClientId && analyse.AfficherChezClient == true && vm.AccessAnalyse &&
                   (ModeUpdateSeulement == false || (ModeUpdateSeulement && (session.AdmAnalyseRisque))))
            {
                return true;
            }
            RedirectToClientLogin();
            return false;
        }


        public void RedirectToClientLogin()
        {
            //Redirection si CDEUser et pas de session

            //LogOff();
            //HttpContext.Current.Server.TransferRequest("~/ClientLogin/Index", false);
            //  HttpContext.Current.ApplicationInstance.CompleteRequest();

            HttpContext.Current.Server.TransferRequest("~/Default/AccessDenied", false);
        }



        [Authorize]
        public Boolean VerifierBonClientCadenassage_Client(int clientId, bool ModeUpdateSeulement)
        {
            //Redirection si CDEUser et pas de session
            if (ForceRedirect())
            {
                RedirectToClientLogin();
                return false;
            }
            var session = GetSession();

            if (session == null)
            {
                return true;
                //    RedirectToClientLogin(res);
                //  return false;
            }
            var vm = getAccesClientVM(clientId);

            if (clientId == session.ClientId && vm.AccessCadenassage && (ModeUpdateSeulement == false || (ModeUpdateSeulement && (session.AdmPrevicad ))))
                return true;

            RedirectToClientLogin();
            return false;
        }


        public Boolean VerifierLimiteFicheCadenassage(bool etat)
        {
            // IF ADMIN / SYSTEM OR READ-WRITE
            var User = HttpContext.Current.User;
            if (User.IsInRole("Administrateur"))
            {
                return true;
            }
            else
            {
                if(etat == false)
                {
                    return true;
                }
                else if (etat == true)
                {
                    var session = GetSession();
                    if (session != null)
                    {
                        // CHECK TRIAL NUMBER
                        var periodeEssai = clientRepository.AsQueryable().Where(x => x.ClientId == session.ClientId).Select(x => x.PeriodeEssai).FirstOrDefault();
                        // LIMIT BY THE SUPER ADMIN
                        var nbLimitCadenassage = clientRepository.AsQueryable().Where(x => x.ClientId == session.ClientId).Select(x => x.NbLimitCadenassage).FirstOrDefault();
                        // WHAT WAS COUNTED
                        var totalCadenassage = clientRepository.AsQueryable().Where(x => x.ClientId == session.ClientId).Select(x => x.TotalCadenassage).FirstOrDefault();

                        // CUSTOMER
                        if (periodeEssai == 0)
                        {
                            // CHECK approved SHEET
                            var activeSheet = ficheCadenassageRepository.AsQueryable().Where(x => x.ClientId == session.ClientId).Where(x => x.IsApproved == true).Select(x => x.IsApproved);
                            var countActiveSheet = activeSheet.Count();

                            if (countActiveSheet < nbLimitCadenassage)
                            {
                                // EXECUTE
                                return true;
                            }
                            return false;
                        }
                        // TRIAL VERSION
                        else if (periodeEssai == 1)
                        {
                            if (totalCadenassage < nbLimitCadenassage)
                            {
                                return true;
                            }
                            return false;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (session == null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public Boolean VerifierStatusCadenassage(int clientId, bool ModeUpdateSeulement)
        {

            // IF ADMIN / SYSTEM OR READ-WRITE
            var User = HttpContext.Current.User;
            if (User.IsInRole("Administrateur"))
            {
                return true;
            }
            else
            {
                var session = GetSession();
                if (session != null)
                {
                    // var session = GetSession();
                    var accessCadenassage = clientRepository.AsQueryable().Where(x => x.ClientId == session.ClientId).Select(x => x.StatusCadenassage).FirstOrDefault();

                    if (accessCadenassage == 0)
                    {
                        return false;
                    }
                    else if (accessCadenassage == 1)
                    {
                        // CHECK DEADLINE
                        var limiteDateCadenassage = clientRepository.AsQueryable().Where(x => x.ClientId == session.ClientId).Select(x => x.DateCadenassage).FirstOrDefault();
                        DateTime currentDate = DateTime.Today;

                        if (limiteDateCadenassage >= currentDate)
                        {
                            // CHECK TRIAL NUMBER
                            var periodeEssai = clientRepository.AsQueryable().Where(x => x.ClientId == session.ClientId).Select(x => x.PeriodeEssai).FirstOrDefault();
                            // LIMIT BY THE SUPER ADMIN
                            var nbLimitCadenassage = clientRepository.AsQueryable().Where(x => x.ClientId == session.ClientId).Select(x => x.NbLimitCadenassage).FirstOrDefault();
                            // WHAT WAS COUNTED
                            var totalCadenassage = clientRepository.AsQueryable().Where(x => x.ClientId == session.ClientId).Select(x => x.TotalCadenassage).FirstOrDefault();

                            // CUSTOMER
                            if (periodeEssai == 0)
                            {
                                // CHECK approved SHEET
                                var activeSheet = ficheCadenassageRepository.AsQueryable().Where(x => x.ClientId == session.ClientId).Where(x => x.IsApproved == true).Select(x => x.IsApproved);
                                var countActiveSheet = activeSheet.Count();

                                if (countActiveSheet < nbLimitCadenassage)
                                {
                                    // EXECUTE
                                    return true;
                                }
                                return false;
                            }
                            // TRIAL VERSION
                            else if (periodeEssai == 1)
                            {
                                if (totalCadenassage < nbLimitCadenassage)
                                {
                                    return true;
                                }
                                // REQUEST DB StatusCadenassage = 0
                                RemoveAccessCadenassageSheet(session.ClientId);
                                return false;
                            }
                            else
                            {
                                // REQUEST DB StatusCadenassage = 0
                                RemoveAccessCadenassageSheet(session.ClientId);
                                return false;
                            }
                        }
                        // REQUEST DB StatusCadenassage = 0
                        RemoveAccessCadenassageSheet(session.ClientId);
                        return false;
                    }/*
                    else if (accessCadenassage == 2)
                    {
                        return true;
                    }*/
                    else
                    {
                        // REQUEST DB StatusCadenassage = 0
                        RemoveAccessCadenassageSheet(session.ClientId);
                        return false;
                    }

                }
                else if (session == null)
                {
                    return true;
                }
            }
            return false;
        }



        public bool VerifierStatusCadenassageSave(int clientId, bool ModeUpdateSeulement)
        {
            // IF ADMIN / SYSTEM OR READ-WRITE
            var User = HttpContext.Current.User;
            if (User.IsInRole("Administrateur"))
            {
                return true;
            }
            else
            {
                var session = GetSession();
                if (session != null)
                {
                    // var session = GetSession();
                    //var vm = getAccesClientVM(clientId);
                    //var client = clientRepository.Get(model.ClientId);

                    var accessCadenassage = clientRepository.AsQueryable().Where(x => x.ClientId == session.ClientId).Select(x => x.StatusCadenassage).FirstOrDefault();

                    if (accessCadenassage == 0)
                    {
                        return false;
                    }
                    else if (accessCadenassage == 1)
                    {
                        // CHECK DEADLINE
                        var limiteDateCadenassage = clientRepository.AsQueryable().Where(x => x.ClientId == session.ClientId).Select(x => x.DateCadenassage).FirstOrDefault();
                        DateTime currentDate = DateTime.Today;

                        if (limiteDateCadenassage >= currentDate)
                        {
                            // CHECK TRIAL NUMBER
                            var periodeEssai = clientRepository.AsQueryable().Where(x => x.ClientId == session.ClientId).Select(x => x.PeriodeEssai).FirstOrDefault();
                            // LIMIT BY THE SUPER ADMIN
                            var nbLimitCadenassage = clientRepository.AsQueryable().Where(x => x.ClientId == session.ClientId).Select(x => x.NbLimitCadenassage).FirstOrDefault();
                            // WHAT WAS COUNTED
                            var totalCadenassage = clientRepository.AsQueryable().Where(x => x.ClientId == session.ClientId).Select(x => x.TotalCadenassage).FirstOrDefault();

                            // CUSTOMER
                            if (periodeEssai == 0)
                            {
                                // CHECK Approved SHEET
                                var activeSheet = ficheCadenassageRepository.AsQueryable().Where(x => x.ClientId == session.ClientId).Where(x => x.IsApproved == true).Select(x => x.IsApproved);
                                var countActiveSheet = activeSheet.Count();

                                if (countActiveSheet < nbLimitCadenassage)
                                {

                                    // COUNT
                                    var nouveauTotalFichesCadenassage = totalCadenassage + 1;
                                    UpdateNbOfCadenassageSheet(session.ClientId, nouveauTotalFichesCadenassage);

                                    // EXECUTE
                                    return true;
                                }
                                return false;
                            }
                            // TRIAL VERSION
                            else if (periodeEssai == 1)
                            {
                                if (totalCadenassage < nbLimitCadenassage)
                                {
                                    // COUNT
                                    var nouveauTotalFichesCadenassage = totalCadenassage + 1;
                                    UpdateNbOfCadenassageSheet(session.ClientId, nouveauTotalFichesCadenassage);

                                    // EXECUTE
                                    return true;
                                }
                                // REQUEST DB StatusCadenassage = 0
                                RemoveAccessCadenassageSheet(session.ClientId);
                                return false;
                            }
                            else
                            {
                                // REQUEST DB StatusCadenassage = 0
                                RemoveAccessCadenassageSheet(session.ClientId);
                                return false;
                            }
                        }
                        else
                        {
                            // REQUEST DB StatusCadenassage = 0
                            RemoveAccessCadenassageSheet(session.ClientId);
                            return false;
                        }
                    }
                    /*else if (accessCadenassage == 2)
                    {
                        // COMPTABILISATION
                        var totalCadenassage = clientRepository.AsQueryable().Where(x => x.ClientId == session.ClientId).Select(x => x.TotalCadenassage).FirstOrDefault();
                        var nouveauTotalFichesCadenassage = totalCadenassage + 1;
                        UpdateNbOfCadenassageSheet(session.ClientId, nouveauTotalFichesCadenassage);

                        // EXECUTE
                        return true;
                    }*/
                    else
                    {
                        // REQUEST DB StatusCadenassage = 0
                        RemoveAccessCadenassageSheet(session.ClientId);
                        return false;
                    }
                }
                else if (session == null)
                {
                    return true;
                }
            }
            return true;
        }



        public void UpdateNbOfCadenassageSheet(int ClientId, int nouveauTotalFichesCadenassage)
        { 
            var client = clientRepository.Get(ClientId);
            client.TotalCadenassage = nouveauTotalFichesCadenassage;
            clientRepository.Update(client);
            clientRepository.SaveChanges();
        }

        public void RemoveAccessCadenassageSheet(int ClientId)
        {
            var client = clientRepository.Get(ClientId);
            client.StatusCadenassage = 0;
            clientRepository.Update(client);
            clientRepository.SaveChanges();
        }

        [Authorize]
        public Boolean VerifierBonClientDocuments_Client(bool ModeUpdateSeulement)
        {
            //Redirection si CDEUser et pas de session
            if (ForceRedirect())
            {
                RedirectToClientLogin();
                return false;
            }
            var session = GetSession();

            if (session == null)
            {
                return true;
                //    RedirectToClientLogin(res);
                //  return false;
            }
            var vm = getAccesClientVM(session.ClientId);

            if (vm.AccessDocuments)
                return true;


            RedirectToClientLogin();
            return false;

        }

        [Authorize]
        public bool Supprimer(UtilisateurIndexViewModel model)
        {
            var utilisateur = utilisateurRepository.Get(model.UtilisateurId);
            if (utilisateur == null)
                return true;

            utilisateurRepository.Delete(model.UtilisateurId);
            utilisateurRepository.SaveChanges();

            return true;
        }

        [Authorize]
        public void SaveUtilisateur(UtilisateurIndexViewModel model)
        {
            var item = utilisateurRepository.Get(model.UtilisateurId);
            if (item == null)
                item = new Models.Utilisateur();

            item.Nom = model.Nom;
            item.NomUtilisateur = model.NomUtilisateur.Trim();
            item.Password = Helpers.Encryption.Encrypt(model.MotDePasse.Trim(), true);
            
            item.Actif = model.Actif;
            item.ClientId = model.ClientId;
            item.Courriel = model.Courriel;
            item.NotificationDebutCad = model.NotificationDebutCad;
            item.AdmAnalyseRisque = model.AdmAnalyseRisque;
            item.AdmPrevicad = model.AdmPrevicad;
            //item.AdmDocuments = model.AdmDocuments;
            item.ROAnalyseRisque = model.ROAnalyseRisque;
            item.RODocuments = model.RODocuments;
            item.ROPrevicad = model.ROPrevicad;
            item.Auditeur = model.Auditeur;
            item.AdmUtilisateurs = model.AdmUtilisateurs;




            if (item.UtilisateurId > 0)
                utilisateurRepository.Update(item);
            else
                utilisateurRepository.Add(item);


            utilisateurRepository.SaveChanges();
            item = utilisateurRepository.Get(item.UtilisateurId);
            model.UtilisateurId = item.UtilisateurId;
            

        }
        public bool isLoginOk(LoginClientViewModel vm)
        {
            var client = clientRepository.getClientByIdentificateur(vm.Identificateur.Trim());
            if (client == null)
                return false;

            var utilisateur = client.Utilisateurs.Where(x => x.NomUtilisateur == vm.UserName.Trim() && x.Password == Helpers.Encryption.Encrypt(vm.Password.Trim(), true)).FirstOrDefault();

            if (utilisateur != null)
            {
                var session = new Sessions.sessionInfo();
                session.ClientId = client.ClientId;
                session.Nom = utilisateur.Nom;
                session.NomUtilisateur = utilisateur.NomUtilisateur;
                
                session.UtilisateurId = utilisateur.UtilisateurId;
                session.AdmAnalyseRisque = utilisateur.AdmAnalyseRisque;
               
                session.AdmPrevicad = utilisateur.AdmPrevicad;
                session.Auditeur = utilisateur.Auditeur;
                session.ROAnalyseRisque = utilisateur.ROAnalyseRisque;
                session.RODocuments = utilisateur.RODocuments;
                session.ROPrevicad = utilisateur.ROPrevicad;
                session.AdmUtilisateurs = utilisateur.AdmUtilisateurs;
                session.NotificationDebutCad = utilisateur.NotificationDebutCad;
                


                HttpContext.Current.Session["utilisateur"] = session;
            }
            else
            {
                HttpContext.Current.Session["utilisateur"] = null;
            }




            // HttpContext.Current.Session.Timeout = 180;
            //HttpContext.Current.Session.Timeout = 131400;
            return (utilisateur != null);

        }




        public Boolean EstNomExistant(UtilisateurIndexViewModel model)
        {

            var UserExist = utilisateurRepository.AsQueryable().Where(x => x.NomUtilisateur == model.NomUtilisateur && x.ClientId == model.ClientId && x.UtilisateurId != model.UtilisateurId).Count() != 0;

            return UserExist;


        }



        public Boolean EstCourrielExistant(UtilisateurIndexViewModel model)
        {

            var UserExist = utilisateurRepository.AsQueryable().Where(x => x.Courriel == model.Courriel && x.ClientId == model.ClientId && x.UtilisateurId != model.UtilisateurId).Count() != 0;


            return UserExist;


        }

        public Boolean DepassementAdminPrevicad(UtilisateurIndexViewModel model)
        {
          
            var client = clientRepository.AsQueryable().Where(x => x.ClientId == model.ClientId).FirstOrDefault();
            if (model.AdmPrevicad)
            {
                var nbDejaPrevi = utilisateurRepository.AsQueryable().Count(x => x.ClientId == model.ClientId && x.UtilisateurId != model.UtilisateurId && x.AdmPrevicad == true);
                if (client.NbAdminsPrevicadMax < nbDejaPrevi + 1)
                {
                    return true;
                }
            }
            return false;
        }

        public Boolean DepassementAdminAnalyse(UtilisateurIndexViewModel model)
        {
          
            var client = clientRepository.AsQueryable().Where(x => x.ClientId == model.ClientId).FirstOrDefault();
           if (model.AdmAnalyseRisque)
            {
                var nbDejaPrevi = utilisateurRepository.AsQueryable().Count(x => x.ClientId == model.ClientId && x.UtilisateurId != model.UtilisateurId && x.AdmAnalyseRisque == true);
                if (client.NbAdminsAnalyseRisqueMax < nbDejaPrevi + 1)
                {
                    return true;
                }
            }
            return false;
        }


        //public Boolean DepassementAdminDocuments(UtilisateurIndexViewModel model)
        //{
         
        //    var client = clientRepository.AsQueryable().Where(x => x.ClientId == model.ClientId).FirstOrDefault();

        //    if (model.AdmDocuments)
        //    {
        //        var nbDejaPrevi = utilisateurRepository.AsQueryable().Count(x => x.ClientId == model.ClientId && x.UtilisateurId != model.UtilisateurId && x.AdmDocuments == true);
        //        if (client.NbAdminsDocumentsMax < nbDejaPrevi + 1)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        public Boolean DepassementAdminUtilisateurs(UtilisateurIndexViewModel model)
        {

            var client = clientRepository.AsQueryable().Where(x => x.ClientId == model.ClientId).FirstOrDefault();

            if (model.AdmUtilisateurs)
            {
                var nbDejaPrevi = utilisateurRepository.AsQueryable().Count(x => x.ClientId == model.ClientId && x.UtilisateurId != model.UtilisateurId && x.AdmUtilisateurs == true);
                if (client.NbAdminUtilisateurs < nbDejaPrevi + 1)
                {
                    return true;
                }
            }
            return false;
        }



        public Boolean DepassementAuditeurs(UtilisateurIndexViewModel model)
        {
            if (model.Auditeur)
            {
                var client = clientRepository.AsQueryable().Where(x => x.ClientId == model.ClientId).FirstOrDefault();
                var nbDejaPrevi = utilisateurRepository.AsQueryable().Count(x => x.ClientId == model.ClientId && x.UtilisateurId != model.UtilisateurId && x.Auditeur == true);
                if (client.NbAuditeursMax < nbDejaPrevi + 1)
                {
                    return true;
                }
            }
            return false;
        }
        

        /*
        public Boolean verificationNbTotalCadenassage(UtilisateurIndexViewModel model)
        {
            if (model.Auditeur)
            {
                var client = clientRepository.AsQueryable().Where(x => x.ClientId == model.ClientId).FirstOrDefault();
                // TOTO MAX - CHECK CURRENT DATE IN THE NEXT DB REQUEST
                var nbCadenassage = clientRepository.AsQueryable(x => x.ClientId == model.ClientId && x.TotalCadenassage  && x.StatusCadenassage);
                if (25 < nbCadenassage + 1)
                {
                    return true;
                }
            }
            return false;
        }

        */

        public ClientAccessVM getAccesClientVM(int ClientId)
        {
            var vm = new ClientAccessVM { AccessAnalyse = false, AccessCadenassage = false, AccessDocuments = false };


            var appClient = clientApplicationPreviRepository.getClientApplications(ClientId);
            var codeGeneral = applicationPreviService.getIdByName(Enums.Applications.Generale);
            var codeCadenassage = applicationPreviService.getIdByName(Enums.Applications.Cadenassage);
            var codeAnalyse = applicationPreviService.getIdByName(Enums.Applications.Analyse);

            var session = GetSession();

            if (appClient.Contains(codeGeneral) && (session.RODocuments ))
                vm.AccessDocuments = true;
            if (appClient.Contains(codeCadenassage) && ( session.AdmPrevicad || session.ROPrevicad || session.Auditeur))
                vm.AccessCadenassage = true;
            if (appClient.Contains(codeAnalyse) && (session.AdmAnalyseRisque || session.ROAnalyseRisque))
                vm.AccessAnalyse = true;
            

            return vm;
        }


        /* TOTO MAX : index display informations

        public void SaveParametersCadAdmin(UtilisateurIndexViewModel model)
        {
            // Initialization by the model with UtilisateurId field in the utilisateurRepository file
            var item = utilisateurRepository.Get(model.UtilisateurId);
            // If item is null, create object and initialization by the Model file Utilisateur.cs
            if (item == null)
                item = new Models.Utilisateur();


            item.NotificationDebutCad = model.NotificationDebutCad;
            item.NomUtilisateur = model.NomUtilisateur;
            item.Password = model.MotDePasse;
            item.Nom = model.Nom;
            item.Courriel = model.Courriel;
            item.UtilisateurId = model.UtilisateurId;

            utilisateurRepository.Update(item);
            utilisateurRepository.SaveChanges();
            item = utilisateurRepository.Get(item.UtilisateurId);
            model.UtilisateurId = item.UtilisateurId;

        }
        */


        public void SaveParametersCadAdmin(EditCadenassageViewModel model, int UtilisateurId)
        {
          
            var item = utilisateurRepository.Get(UtilisateurId);


            if (item != null)

            {
                var session = GetSession();
                session.NotificationDebutCad = model.NotificationDebutCad;
                item.NotificationDebutCad = model.NotificationDebutCad;
                utilisateurRepository.Update(item);
                utilisateurRepository.SaveChanges();
            }

        }


        // CHECK IF THE PARSER USER IS THE SAME AS THE DATA BASE
        public UtilisateurIndexViewModel VerificationUtilisateurEnLigne(int clientId)
        {
            var session = GetSession();

            var accountDetails = utilisateurRepository.AsQueryable().Where(x => x.UtilisateurId == session.UtilisateurId).Where(x => x.ClientId == clientId).FirstOrDefault();
            var verificationUtilisateurEnLigne = new UtilisateurIndexViewModel
            {
                NomUtilisateur = accountDetails.NomUtilisateur,
                MotDePasse = accountDetails.Password,
                Nom = accountDetails.Nom,
                Courriel = accountDetails.Courriel,
                UtilisateurId = accountDetails.UtilisateurId,
                ClientId = accountDetails.ClientId,
                NotificationDebutCad = accountDetails.NotificationDebutCad
            };

            return verificationUtilisateurEnLigne;
        }


        // TODO MAX : FILTRE PAR DEPARTEMENT ET ÉQUIPEMENT EN STAND BY
        /* public IEnumerable<DepartementDDLViewModel> filtreDepartement()
        {
            var langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            var session = GetSession();
            var compagnieId = session.ClientId;

            var departements = departementRepository.AsQueryable().Where(x => x.ClientId == compagnieId).Select(x => new DepartementDDLViewModel()
            {
                NomDepartement = langue == "fr" ? x.NomDepartement : x.NomDepartementEN,
                DepartementId = x.DepartementId

            });

            return departements;

        }

        public IEnumerable<EquipementDDLViewModel> filtreEquipement()
        {
            var langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            var session = GetSession();
            var compagnieId = session.ClientId;

            var equipements = equipementRepository.AsQueryable().Where(x => x.ClientId == compagnieId).Select(x => new EquipementDDLViewModel()
            {
                NomEquipement = langue == "fr" ? x.NomEquipement : x.NomEquipementEN,
                EquipementId = x.EquipementId

            });

            return equipements;
        }*/

    }
}