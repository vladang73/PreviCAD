using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Previgesst.Repositories;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Previgesst.Services
{
    public class LignesRegistreService
    {
        private LigneRegistreRepository ligneRegistreRepository;
        private UtilisateurService utilisateurService;
        private UtilisateurRepository utilisateurRepository;
        private EmployeRegistreRepository employeRegistreRepository;
        private EmployeRegistreService employeRegistreService;
        private ClientRepository clientRepository;
        private SavedInstructionRepository saveInsRepository;

        public LignesRegistreService(LigneRegistreRepository ligneRegistreRepository, UtilisateurService utilisateurService, UtilisateurRepository utilisateurRepository, EmployeRegistreRepository employeRegistreRepository, EmployeRegistreService employeRegistreService, ClientRepository clientRepository, SavedInstructionRepository saveInsRepository)
        {
            this.ligneRegistreRepository = ligneRegistreRepository;
            this.utilisateurService = utilisateurService;
            this.utilisateurRepository = utilisateurRepository;
            this.employeRegistreRepository = employeRegistreRepository;
            this.employeRegistreService = employeRegistreService;
            this.clientRepository = clientRepository;
            this.saveInsRepository = saveInsRepository;
        }

        public void SaveLigneAudit(LigneRegistreViewModel model)
        {
            var item = ligneRegistreRepository.Get(model.LigneRegistreId);
            if (item == null)
                return;
            item.BonDeTravail = model.BonDeTravail;
            item.Rep1 = model.Rep1;
            item.Rep2 = model.Rep2;
            item.Rep3 = model.Rep3;
            item.Rep4 = model.Rep4;
            item.NoteAudit = model.NoteAudit;
            if (model.NomAuditeur != null)
            {
                item.NomAuditeur = model.NomAuditeur;

            }
            else
            {
                var session = utilisateurService.GetSession();
                item.NomAuditeur = session.NomUtilisateur;
            }
            ligneRegistreRepository.Update(item);
            ligneRegistreRepository.SaveChanges();

        }

        public void SaveLigneRegistre(LigneRegistreViewModel model)
        {
            var item = ligneRegistreRepository.Get(model.LigneRegistreId);
            if (item == null)
                item = new Models.LigneRegistre();

            item.BonDeTravail = model.BonDeTravail;
            item.DateDebut = model.DateDebut;
            item.DateFin = model.DateFin;
            if (model.LuEtEffectue == true)
            {
                item.LuEtEffectue = 1;
            }
            else
            {
                item.LuEtEffectue = 0;
            }
            if (model.LuEtDecadenasse == true)
            {
                item.LuEtDecadenasse = 1;
            }
            else
            {
                item.LuEtDecadenasse = 0;
            }

            if (model.Rep1 != null)
            {
                item.Rep1 = model.Rep1;
            }
            if (model.Rep2 != null)
            {
                item.Rep2 = model.Rep2;
            }
            if (model.Rep3 != null)
            {
                item.Rep3 = model.Rep3;
            }
            if (model.Rep4 != null)
            {
                item.Rep4 = model.Rep4;
            }

            item.EmployeRegistreId = model.EmployeRegistreId;
            item.FicheCadenassageId = model.FicheCadenassageId;
            item.FinPrevue = model.FinPrevue;

            item.Note = model.Note;

            if (item.LigneRegistreId > 0)
                ligneRegistreRepository.Update(item);
            else
                ligneRegistreRepository.Add(item);

            ligneRegistreRepository.SaveChanges();
            item = ligneRegistreRepository.Get(item.LigneRegistreId);
            model.NoFicheCadenassage = item.FicheCadenassage.NoFiche;
            model.Nom = item.EmployeRegistre.Nom;
            model.NomDepartement = item.FicheCadenassage.Departement.NomDepartement;
            model.NomEquipement = item.FicheCadenassage.Equipement.NomEquipement;
            model.NoCadenas = item.EmployeRegistre.NoCadenas;
            model.NoEmploye = item.EmployeRegistre.NoEmploye;

            if (item.DateFin == null)
            {
                var liste = NotificationCourrielCadActive();
                var infoClient = GetCieInformation();

                foreach (var EachMember in liste)
                {
                    foreach (var EachInfo in infoClient)
                    {
                        var ClientId = EachInfo.ClientId;
                        var Logo = EachInfo.Logo;

                        var membreCourriel = EachMember.Courriel;
                        var membreNom = EachMember.Nom;

                        GeneralService.SendMail_v2(
                            "<center><table style=\"width:500px;padding:30px;border:1px solid #efeeef;\">" +
                                "<tr>" +
                                    "<td><center><img style=\"max-height:100px;\" src=\"cid:" + Logo + "\"></img></center></td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td><center><h1 style=\"margin-bottom:-30px\">Cadenassage en cours</h1></center></td>" +
                                "</tr><br/>" +
                                "<tr>" +
                                    "<td>" +
                                        "<p>Bonjour " + membreNom + ",</p>" +
                                        "<p>" +
                                            "<strong>Une interventions nécessitant le contrôle des énergies dangereuses est en cours.</strong><br><br>" +
                                            "Détails de l’intervention<br>" +
                                            "Nom employé : " + model.Nom + "<br>" +
                                            "Identifiant de l’employé : " + model.NoEmploye + "<br>" +
                                            "Département : " + model.NomDepartement + "<br>" +
                                            "Équipement : " + model.NomEquipement + "<br>" +
                                            "Numéro de procédure de cadenassage : " + model.NoFicheCadenassage + "<br>" +
                                        "</p>" +
                                        "<center><table style=\"width:300px;background-color:#337ab7\">" +
                                            "<tr>" +
                                                "<td><center><a style=\"text-decoration:none;color:white;font-weight:bold\" href=\"https://applications.previcad.com\">Accès au compte</a></center></td>" +
                                            "</tr>" +
                                        "</table></center>" +
                                    "</td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td><p><em>IMPORTANT : Ce courriel a été envoyé automatiquement, S.V.P. ne pas « Répondre » sur ce message.</em></p></td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td><img style=\"margin-left:280px;max-height:30px;\" src=\"cid:dark_logo.png\"></img></td>" +
                                "</tr>" +
                            "</table></center>"
                            ,
                            "Cadenassage en cours :  " + model.NomEquipement, membreCourriel, ClientId, Logo, true
                        ); // Sujet du courriel + courriel membre + cliendId + logo
                    }

                }

            }

        }

        internal List<UtilisateurIndexViewModel> NotificationCourrielCadActive()
        {
            var session = employeRegistreService.getEmployeRegistre();
            var NoEmploye = session.NoEmploye;

            // Get compagny ID
            var compagnieId = session.ClientId;

            // Get all contact by compagny ID when the notification is activate
            var resultContacts = utilisateurRepository.AsQueryable()
                                                      .Where(x => x.ClientId == compagnieId)
                                                      .Where(x => x.Actif == true)
                                                      .Where(x => x.NotificationDebutCad == true)
                                                      .Select(x => new UtilisateurIndexViewModel()
                                                      {
                                                          Nom = x.Nom,
                                                          Courriel = x.Courriel
                                                      })
                                                      .ToList();

            return resultContacts;
        }

        internal List<ClientListViewModel> GetCieInformation()
        {
            var session = employeRegistreService.getEmployeRegistre();
            var NoEmploye = session.NoEmploye;

            var compagnieId = employeRegistreRepository.AsQueryable().Where(x => x.NoEmploye == NoEmploye).Where(x => x.ClientId == session.ClientId).Select(x => x.ClientId).FirstOrDefault();
            var logoClient = clientRepository.AsQueryable().Where(x => x.ClientId == compagnieId).Select(x => new ClientListViewModel()
            {
                Logo = x.Logo,
                ClientId = x.ClientId
            }).ToList();

            return logoClient;
        }


        internal DataSourceResult GetListeLignesRegistre(DataSourceRequest request, int ClientId)
        {

            var result = ligneRegistreRepository.AsQueryable()
                .OrderByDescending(x => x.DateDebut)
                .Where(x => x.EmployeRegistre.ClientId == ClientId)
                .Select(x => new LigneRegistreViewModel()
            {
                DateDebut = x.DateDebut,
                DateFin = x.DateFin,


                NoCadenas = x.EmployeRegistre.NoCadenas,
                NoEmploye = x.EmployeRegistre.NoEmploye,
                Nom = x.EmployeRegistre.Nom,
                NomDepartement = x.FicheCadenassage.Departement.NomDepartement,
                Note = x.Note,
                NomEquipement = x.FicheCadenassage.Equipement.NomEquipement,
                Termine = x.DateFin != null,
                NoFicheCadenassage = x.FicheCadenassage.NoFiche,
                FinPrevue = x.FinPrevue,
                LigneRegistreId = x.LigneRegistreId,
                EmployeRegistreId = x.EmployeRegistreId,
                FicheCadenassageId = x.FicheCadenassageId,
                NomAuditeur = x.NomAuditeur,
                BonDeTravail = x.BonDeTravail,
                Rep1 = x.Rep1,
                Rep2 = x.Rep2,
                Rep3 = x.Rep3,
                Rep4 = x.Rep4,
                NoteAudit = x.NoteAudit




            }).ToDataSourceResult(request);

            return result;
        }


        internal DataSourceResult GetListeLignesRegistreParEmploye(DataSourceRequest request, int employeRegistreId)
        {
            var langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            var result = ligneRegistreRepository.AsQueryable()
                .OrderByDescending(x => x.DateDebut)
                .Where(x => x.EmployeRegistre.EmployeRegistreId == employeRegistreId)
                .Where(x => x.FicheCadenassage.IsApproved)
                .Select(x => new LigneRegistreViewModel()
                {
                    BonDeTravail = x.BonDeTravail,
                    DateDebut = x.DateDebut,
                    DateFin = x.DateFin,


                    NoCadenas = x.EmployeRegistre.NoCadenas,
                    NoEmploye = x.EmployeRegistre.NoEmploye,
                    Nom = x.EmployeRegistre.Nom,
                    NomDepartement = langue == "fr" ? x.FicheCadenassage.Departement.NomDepartement : x.FicheCadenassage.Departement.NomDepartementEN,
                    Note = x.Note,
                    NomEquipement = langue == "fr" ? x.FicheCadenassage.Equipement.NomEquipement : x.FicheCadenassage.Equipement.NomEquipementEN,
                    Termine = x.DateFin != null,
                    NoFicheCadenassage = x.FicheCadenassage.NoFiche,
                    FinPrevue = x.FinPrevue,
                    LigneRegistreId = x.LigneRegistreId,
                    EmployeRegistreId = x.EmployeRegistreId,
                    FicheCadenassageId = x.FicheCadenassageId,

                    //PendingSteps = saveInsRepository.AsQueryable().Where(i => i.FicheCadenassageId == x.FicheCadenassageId).Count()
                    //PendingSteps = saveInsRepository.SqlQuery<int>("Select Count(*) From SavedInstructions Where FicheCadenassageId =" + x.FicheCadenassageId)


                }).ToDataSourceResult(request);

            return result;
        }


        internal void GetEtiquette(int id)
        {
            GemBox.Spreadsheet.SpreadsheetInfo.SetLicense("E5M9-YY9F-HFAG-HI0P");
            string templateName = HttpContext.Current.Server.MapPath("~/Templates/EtiquetteCadenassage.xlsx");
            GemBox.Spreadsheet.ExcelFile ef = GemBox.Spreadsheet.ExcelFile.Load(templateName);
            var ligne = ligneRegistreRepository.Get(id);


            //Entête

            var langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            var ws = ef.Worksheets[0];
            ws.Cells["E15"].Value = ligne.EmployeRegistre.Nom;
            ws.Cells["P15"].Value = ligne.EmployeRegistre.Nom;

            ws.Cells["F17"].Value = ligne.EmployeRegistre.Departement.NomDepartement;
            ws.Cells["Q17"].Value = ligne.EmployeRegistre.Departement.NomDepartementEN;

            ws.Cells["F19"].Value = DateTime.Today;
            ws.Cells["R19"].Value = DateTime.Today;

            ws.Cells["F21"].Value = ligne.FicheCadenassage.Equipement.NomEquipement;
            ws.Cells["Q21"].Value = ligne.FicheCadenassage.Equipement.NomEquipementEN;

            ws.Cells["C24"].Value = ligne.FicheCadenassage.TravailAEffectuer;
            ws.Cells["N24"].Value = ligne.FicheCadenassage.TravailAEffectuerEN;

            ws.Cells["C28"].Value = ligne.Note;
            ws.Cells["N28"].Value = ligne.Note;


            ws.Cells["H31"].Value = ligne.FinPrevue;
            ws.Cells["U31"].Value = ligne.FinPrevue;



            ef.Save(HttpContext.Current.Response, (langue == "fr" ? "Etiquette-" : "Label-") + ligne.FicheCadenassage.NoFiche + ".pdf");


        }
    }
}