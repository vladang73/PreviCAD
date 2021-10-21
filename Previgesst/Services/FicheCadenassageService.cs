


using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using OfficeOpenXml.Style;
using Previgesst.Models;
using Previgesst.Repositories;
using Previgesst.ViewModels;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using Previgesst.Ressources;
using System.Globalization;

namespace Previgesst.Services
{
    public class FicheCadenassageService
    {

        private FicheCadenassageRepository ficheCadenassageRepository;
        private EquipementRepository equipementRepository;
        private LigneDecadenassageRepository ligneDecadenassageRepository;

        private SourceEnergieCadenassageRepository sourceEnergieCadenassageRepository;
        private MaterielRequisCadenassageRepository materielRequisCadenassageRepository;
        private LigneInstructionRepository ligneInstructionRepository;
        private MaterielRequisCadenassageService materielRequisCadenassageService;
        private LigneInstructionService ligneInstructionService;
        LigneDecadenassageService ligneDecadenassageService;
        private UtilisateurService utilisateurService;
        private PhotoFicheCadenassageService photoFicheCadenassageService;
        private PhotoFicheCadenassageRepository photoFicheCadenassageRepository;
        private ClientRepository clientRepository;

        public FicheCadenassageService(FicheCadenassageRepository ficheCadenassageRepository, EquipementRepository equipementRepository,

            LigneDecadenassageRepository ligneDecadenassageRepository, SourceEnergieCadenassageRepository sourceEnergieCadenassageRepository,
            MaterielRequisCadenassageRepository materielRequisCadenassageRepository, LigneInstructionRepository ligneInstructionRepository,
            MaterielRequisCadenassageService materielRequisCadenassageService, LigneInstructionService ligneInstructionService,
              LigneDecadenassageService ligneDecadenassageService, UtilisateurService utilisateurService,
              PhotoFicheCadenassageService photoFicheCadenassageService, PhotoFicheCadenassageRepository photoFicheCadenassageRepository,
              ClientRepository clientRepository)
        {
            this.ficheCadenassageRepository = ficheCadenassageRepository;
            this.equipementRepository = equipementRepository;
            this.ligneDecadenassageRepository = ligneDecadenassageRepository;

            this.sourceEnergieCadenassageRepository = sourceEnergieCadenassageRepository;
            this.materielRequisCadenassageRepository = materielRequisCadenassageRepository;
            this.ligneInstructionRepository = ligneInstructionRepository;
            this.materielRequisCadenassageService = materielRequisCadenassageService;
            this.ligneInstructionService = ligneInstructionService;
            this.ligneDecadenassageService = ligneDecadenassageService;
            this.utilisateurService = utilisateurService;
            this.photoFicheCadenassageService = photoFicheCadenassageService;
            this.photoFicheCadenassageRepository = photoFicheCadenassageRepository;
            this.clientRepository = clientRepository;
        }

        #region ImpressionFicheCadenassage

        public static double GetDefaultHeight()
        {
            return 103.5;

        }

        internal void ReturnArchive(int id, string langue, string filePath, ref string fileName)
        {
            string noFeche = "";
            var ef = GenerateFile(id, langue, ref noFeche);

            //ef.Save(HttpContext.Current.Response, FicheRES.Fiche + noFeche + ".pdf");

            fileName = string.Format("Archive {0}{1} {2}.pdf", FicheRES.Fiche, noFeche, DateTime.Now.ToString("yyyyMMddhmmss"));
            ef.Save(System.IO.Path.Combine(filePath, fileName));
        }

        internal void ReturnFile2(int id, string langue)
        {
            string noFeche = "";
            var ef = GenerateFile(id, langue, ref noFeche);

            ef.Save(HttpContext.Current.Response, FicheRES.Fiche + noFeche + ".pdf");
        }

        private GemBox.Spreadsheet.ExcelFile GenerateFile(int id, string langue, ref string noFeche)
        {
            //changement de culture en fonction de la langue choisie

            var culture = System.Threading.Thread.CurrentThread.CurrentUICulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(langue + "-CA");

            if (langue == "fr")
                langue = "";

            GemBox.Spreadsheet.SpreadsheetInfo.SetLicense("E5M9-YY9F-HFAG-HI0P");
            string templateName = HttpContext.Current.Server.MapPath("~/Templates/NouvelleFiche" + langue + ".xlsx");
            GemBox.Spreadsheet.ExcelFile ef = GemBox.Spreadsheet.ExcelFile.Load(templateName);
            var fiche = ficheCadenassageRepository.Get(id);

            //Entête

            var ws = ef.Worksheets[0];
            ws.Cells["C1"].Value = langue == "" ? fiche.TitreFiche : fiche.TitreFicheEN;
            ws.Cells["C2"].Value = langue == "" ? fiche.Equipement.NomEquipement : fiche.Equipement.NomEquipementEN;
            ws.Cells["C5"].Value = langue == "" ? fiche.TravailAEffectuer : fiche.TravailAEffectuerEN;

            ws.Cells["J1"].Value = langue == "" ? fiche.Departement.NomDepartement : fiche.Departement.NomDepartementEN;
            ws.Cells["J2"].Value = FicheRES.NoFiche + fiche.NoFiche;
            ws.Cells["J3"].Value = FicheRES.ApprouvePar + fiche.ApprouvePar;


            ws.Cells["J4"].Value = FicheRES.Date + (fiche.DateApproved?.ToShortDateString()) ?? string.Empty;
            ws.Cells["J5"].Value = FicheRES.DateRevision + (fiche.DateUpdated.HasValue ? fiche.DateUpdated?.ToShortDateString() : fiche.DateCreation?.ToShortDateString()) ?? string.Empty;

            //ws.Cells["J4"].Value = FicheRES.Date + (fiche.DateCreation?.ToShortDateString()) ?? string.Empty;
            //ws.Cells["J5"].Value = FicheRES.DateRevision + (fiche.DateRevision?.ToShortDateString()) ?? string.Empty;

            double hauteurLigne = 0;

            if (fiche.TitreFiche != "Fiche de cadenassage") // procedure de travail sécuritaire
            {
                ws.Cells["A12"].Value = langue == "" ? "CONTRÔLE DES SOURCES D'ÉNERGIE" : "CONTROL OF ENERGY SOURCES";



                ws.Cells["A31"].Value = langue == "" ? "Tout intervenant doit avoir la formation  et l'entrainement nécessaire avant d'effectuer cette tâche." :
                    "All participants must have the necessary formation and training before performing this task.";

                ws.Cells["A32"].Value = langue == "" ? "La présente procédure est élaborée à partir des principes énoncés aux articles 188.2, 188.4 et 189 du Règlement sur la santé et sécurité du travail. Une analyse de risques a été effectué avant l'élaboration de cette méthode de travail qui assure une sécurité équivalente au cadenassage." :
                    "This procedure is developed on the basis of the principles set out in sections 188.2, 188.4 and 189 of the Regulation respecting occupational health and safety. A risk analysis was conducted prior to the development of this method of work, which has a safety equivalent to lockout.";


                ws.Cells["A33"].Value = langue == "" ? "Toute personne ayant à intervenir sur la machine en cours de travaux doit d'abord en informer ses collègues travaillant à proximité.	" : "Anyone who has to intervene on the machine during work must first inform his colleagues and affix his personal lock";



                ws.Cells["A35"].Value = langue == "" ? "REMISE EN FONCTION" : "RESET";


            }

            // remise de la culture initiale
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;


            //Ajustement des hauteurs de lignes  (mergées verticalement) en fonction du contenu


            hauteurLigne = ws.Rows[0].Height;
            GetHeight(ws.Cells.GetSubrangeRelative(0, 9, 4, 1), ws.Cells["J1"].Value?.ToString(), "", ws.Cells["J1"].Style.Font, ws.Parent.Worksheets[1], false, ref hauteurLigne);
            ws.Rows[0].Height = (int)hauteurLigne;

            hauteurLigne = ws.Rows[1].Height;
            GetHeight(ws.Cells.GetSubrangeRelative(1, 2, 7, 2), ws.Cells["C2"].Value?.ToString(), "", ws.Cells["C2"].Style.Font, ws.Parent.Worksheets[1], true, ref hauteurLigne);
            GetHeight(ws.Cells.GetSubrangeRelative(1, 9, 4, 1), ws.Cells["J2"].Value?.ToString(), "", ws.Cells["J2"].Style.Font, ws.Parent.Worksheets[1], true, ref hauteurLigne);

            ws.Rows[1].Height = (int)hauteurLigne;


            hauteurLigne = ws.Rows[2].Height;
            GetHeight(ws.Cells.GetSubrangeRelative(2, 9, 4, 1), ws.Cells["J3"].Value?.ToString(), "", ws.Cells["J3"].Style.Font, ws.Parent.Worksheets[1], true, ref hauteurLigne);
            ws.Rows[2].Height = (int)hauteurLigne;

            hauteurLigne = ws.Rows[4].Height;
            GetHeight(ws.Cells.GetSubrangeRelative(4, 2, 7, 1), ws.Cells["C5"].Value?.ToString(), "", ws.Cells["C5"].Style.Font, ws.Parent.Worksheets[1], true, ref hauteurLigne);
            GetHeight(ws.Cells.GetSubrangeRelative(4, 9, 4, 1), ws.Cells["J5"].Value?.ToString(), "", ws.Cells["J5"].Style.Font, ws.Parent.Worksheets[1], true, ref hauteurLigne);

            ws.Rows[4].Height = (int)hauteurLigne;


            hauteurLigne = 0;
            GetHeight(ws.Cells.GetSubrangeRelative(30, 0, 12, 1), ws.Cells["A31"].Value?.ToString(), "", ws.Cells["A31"].Style.Font, ws.Parent.Worksheets[1], false, ref hauteurLigne);
            ws.Rows[30].Height = (int)hauteurLigne;

            hauteurLigne = 0;
            GetHeight(ws.Cells.GetSubrangeRelative(31, 0, 12, 1), ws.Cells["A32"].Value?.ToString(), "", ws.Cells["A32"].Style.Font, ws.Parent.Worksheets[1], false, ref hauteurLigne);
            ws.Rows[31].Height = (int)(hauteurLigne / 1.2);

            hauteurLigne = 0;
            GetHeight(ws.Cells.GetSubrangeRelative(32, 0, 12, 1), ws.Cells["A33"].Value?.ToString(), "", ws.Cells["A33"].Style.Font, ws.Parent.Worksheets[1], false, ref hauteurLigne);
            ws.Rows[32].Height = (int)(hauteurLigne / 1.2);


            //On peut ajouter jusqu'à 10 types de sources d'énergie
            //Si 7 ou 8, on aura une ligne à supprimer
            //Si 6 et moins, on aura 2 lignes à supprimer
            //9 et 10, pas de suppression à faire

            //Idem pour les accessoires

            int aSupprimer;
            if (fiche.MaterielsRequisCadenassage.Count >= 9 || fiche.SourcesEnergieCadenassage.Count >= 9)
                aSupprimer = 0;
            else if (fiche.MaterielsRequisCadenassage.Count >= 7 || fiche.SourcesEnergieCadenassage.Count >= 7)
            {
                aSupprimer = 1;
                ws.Rows[10].Hidden = true;
                ws.Rows[9].Style.Borders.SetBorders(GemBox.Spreadsheet.MultipleBorders.Bottom, GemBox.Spreadsheet.SpreadsheetColor.FromName(GemBox.Spreadsheet.ColorName.Black), GemBox.Spreadsheet.LineStyle.Medium);
            }
            else
            {
                aSupprimer = 2;
                ws.Rows[10].Hidden = true;
                ws.Rows[9].Hidden = true;
                ws.Rows[8].Style.Borders.SetBorders(GemBox.Spreadsheet.MultipleBorders.Bottom, GemBox.Spreadsheet.SpreadsheetColor.FromName(GemBox.Spreadsheet.ColorName.Black), GemBox.Spreadsheet.LineStyle.Medium);

            }


            //Logo A1 à B3
            {
                var imagePath = HttpContext.Current.Server.MapPath("~/Images/Cadenassage/Clients/" + fiche.Client.ClientId + "/" + fiche.Client.Logo);
                AjoutImageGemBOX(ws, imagePath, 0, 0, 2, 1);
            }
            //Image de l'équipement: A4 à B9 ... ou B10 ... ou B11 suivant le matériel requis et les sources d'énergies
            {
                var imagePath = HttpContext.Current.Server.MapPath("~/Images/Cadenassage/Equipements/" + fiche.EquipementId + "/" + fiche.Equipement.NomFichier);
                AjoutImageGemBOX(ws, imagePath, 3, 0, 10 - aSupprimer, 1);
            }



            {  // On remplit les 2 colonnes d'une ligne avant de changer de colonne.
               // La 2e colonne est 3 colonnes+ loin dans XL (merge de cellules)
                Queue<MaterielRequisCadenassage> q = new Queue<MaterielRequisCadenassage>();
                fiche.MaterielsRequisCadenassage.OrderBy(x => x.Materiel.Description).ToList().ForEach(x => q.Enqueue(x));

                int ligne = 6;
                int colonne = 2;
                while (q.Count > 0)
                {
                    var m = q.Dequeue();
                    var materiel = (langue == "" ? m.Materiel.Description : m.Materiel.DescriptionEN);
                    ws.Cells[ligne, colonne].Value = (materiel) + (m.Quantite != 1 ? " (" + m.Quantite.ToString() + ")" : "");
                    if (m.Quantite != 1)
                        ws.Cells[ligne, colonne].GetCharacters(materiel.Length + 2, m.Quantite.ToString().Length).Font.Color = GemBox.Spreadsheet.SpreadsheetColor.FromName(GemBox.Spreadsheet.ColorName.Red);
                    if (colonne == 2)
                        colonne = colonne + 3;
                    else
                    {
                        colonne = 2;
                        ligne++;
                    }
                }
            }


            //Sources énergie:
            {// On remplit les 2 colonnes d'une ligne avant de changer de colonne.
             // La 2e colonne est 2 colonnes+ loin dans XL (merge de cellules)

                Queue<SourceEnergieCadenassage> s = new Queue<SourceEnergieCadenassage>();
                fiche.SourcesEnergieCadenassage.ToList().ForEach(x => s.Enqueue(x));

                int ligne = 6;
                int colonne = 9;

                while (s.Count > 0)
                {
                    var source = (langue == "" ? s.Dequeue().SourceEnergie.Description : s.Dequeue().SourceEnergie.DescriptionEN);
                    ws.Cells[ligne, colonne].Value = source;
                    if (colonne == 9)
                        colonne = colonne + 2;
                    else
                    {
                        colonne = 9;
                        ligne++;
                    }

                }

            }


            var posLignesImages = 16;// basé à 0, en ligne 17
            var posLignesInstruction = 13;// basé à 0, en ligne 14
            //les instructions de décadenassage commencent à l'origine en ligne 39 --> 38 pour GemBOX
            var posLignesDecadenassage = 38;


            var lignesDecadenassage = fiche.LignesDecadenassage.OrderBy(x => x.NoLigne).ToList();
            if (lignesDecadenassage.Count > 1)
            {
                ws.Rows.InsertCopy(posLignesDecadenassage, lignesDecadenassage.Count - 1, ws.Rows[38]);

            }
            //les instructions commencent en ligne 14 --> 13 pour gembox
            var lignesInstruction = fiche.LignesInstruction.OrderBy(x => x.NoLigne).ToList();
            if (lignesInstruction.Count > 1)
            {
                ws.Rows.InsertCopy(posLignesInstruction, lignesInstruction.Count - 1, ws.Rows[posLignesInstruction]);
                posLignesDecadenassage += lignesInstruction.Count - 1;
                posLignesImages += lignesInstruction.Count - 1;

                var range = ws.Rows[posLignesInstruction + lignesInstruction.Count()];
                range.Style.Borders.SetBorders(GemBox.Spreadsheet.MultipleBorders.Top, GemBox.Spreadsheet.SpreadsheetColor.FromName(GemBox.Spreadsheet.ColorName.Black), GemBox.Spreadsheet.LineStyle.Medium);

                var rangeCells = ws.Cells.GetSubrangeRelative(posLignesInstruction, 0, 13, lignesInstruction.Count());
                rangeCells.Style.Borders.SetBorders(GemBox.Spreadsheet.MultipleBorders.Inside, GemBox.Spreadsheet.SpreadsheetColor.FromName(GemBox.Spreadsheet.ColorName.Text1Lighter50Pct), GemBox.Spreadsheet.LineStyle.Hair);


            }

            double defaultHeight = ws.Rows[posLignesInstruction].Height;
            for (int i = 0; i < lignesInstruction.Count; i++)
            {
                PeuplerInstructionGemBOX(ws, posLignesInstruction + i, lignesInstruction.ElementAt(i), defaultHeight, langue);
            }



            for (int i = 0; i < lignesDecadenassage.Count; i++)
            {
                PeuplerLigneDecadenassageGemBOX(ws, posLignesDecadenassage + i, lignesDecadenassage.ElementAt(i), defaultHeight, langue);
            }

            // images


            var photos = fiche.PhotosFicheCadenassage.OrderBy(x => x.Rang).ToList();

            {
                int ligne = posLignesImages;
                int colonne = 0;
                int colonneFin = 1;



                for (int i = 0; i < photos.Count; i++)

                {
                    ws.Cells[ligne, colonne].Value = photos.ElementAt(i).Rang;
                    var imagePath = HttpContext.Current.Server.MapPath("~/Images/Cadenassage/Photos/" + photos.ElementAt(i).PhotoFicheCadenassageId + "/" + photos.ElementAt(i).Fichier);

                    AjoutImageGemBOX(ws, imagePath, ligne + 1, colonne, ligne + 1, colonneFin);
                    ws.Cells[ligne + 2, colonne].Value = langue == "" ? photos.ElementAt(i).Localisation : photos.ElementAt(i).LocalisationEN;
                    switch (colonne)
                    {
                        case 0: colonne = 2; colonneFin = 3; break;
                        case 2: colonne = 4; colonneFin = 6; break;
                        case 4: colonne = 7; colonneFin = 9; break;
                        case 7: colonne = 10; colonneFin = 12; break;
                        case 10: colonne = 0; colonneFin = 1; ligne += 3; break;
                    }
                }
            }



            {//on vérifie les 4 blocs d'images (3 lignes par bloc), on cache ceux inutilisés
                Boolean SautDejaFait = false;
                for (int i = 1; i <= 4; i++)
                {
                    if (ws.Cells[posLignesImages + (i - 1) * 3, 0].Value == null)
                    {
                        ws.Rows[posLignesImages + (i - 1) * 3].Hidden = true;
                        ws.Rows[posLignesImages + (i - 1) * 3 + 1].Hidden = true;
                        ws.Rows[posLignesImages + (i - 1) * 3 + 2].Hidden = true;
                    }
                    else
                    {   // on vérifie le texte de localisation de chaque image de la ligne et on ajuste au plus grand.
                        var ligneCourante = posLignesImages + (i - 1) * 3 + 2;
                        hauteurLigne = ws.Rows[posLignesImages + (i - 1) * 3 + 2].Height;
                        GetHeight(ws.Cells.GetSubrangeRelative(ligneCourante, 0, 2, 1), ws.Cells[ligneCourante, 0].Value?.ToString(), "", ws.Cells[ligneCourante, 0].Style.Font, ws.Parent.Worksheets[1], false, ref hauteurLigne);
                        GetHeight(ws.Cells.GetSubrangeRelative(ligneCourante, 2, 2, 1), ws.Cells[ligneCourante, 2].Value?.ToString(), "", ws.Cells[ligneCourante, 0].Style.Font, ws.Parent.Worksheets[1], false, ref hauteurLigne);
                        GetHeight(ws.Cells.GetSubrangeRelative(ligneCourante, 4, 3, 1), ws.Cells[ligneCourante, 4].Value?.ToString(), "", ws.Cells[ligneCourante, 0].Style.Font, ws.Parent.Worksheets[1], false, ref hauteurLigne);
                        GetHeight(ws.Cells.GetSubrangeRelative(ligneCourante, 7, 3, 1), ws.Cells[ligneCourante, 7].Value?.ToString(), "", ws.Cells[ligneCourante, 0].Style.Font, ws.Parent.Worksheets[1], false, ref hauteurLigne);
                        GetHeight(ws.Cells.GetSubrangeRelative(ligneCourante, 10, 3, 1), ws.Cells[ligneCourante, 10].Value?.ToString(), "", ws.Cells[ligneCourante, 0].Style.Font, ws.Parent.Worksheets[1], false, ref hauteurLigne);

                        ws.Rows[ligneCourante].Height = (int)hauteurLigne;
                        double hauteurTotale = 0;
                        if (!SautDejaFait) // on ne refera pas 2 sauts de page forcés!
                        {
                            for (int n = 0; n < ligneCourante - 2; n++)
                            {
                                if (ws.Rows[i].Hidden == false)
                                    hauteurTotale += ws.Rows[n].Height;
                            }
                            double hauteurNecessaire = ws.Rows[ligneCourante].Height + ws.Rows[ligneCourante - 1].Height + ws.Rows[ligneCourante - 2].Height;
                            if (i == 1)
                                hauteurNecessaire += ws.Rows[ligneCourante - 3].Height;
                            var hauteurNecessaireAvantConvert = hauteurNecessaire;
                            var percent = ws.PrintOptions.AutomaticPageBreakScalingFactor / 100.0;
                            //on convertit en poins pour avoir même unité de mesure.  Le * 0.6 viens du fait qu'on ajuste à une page en largeur dans le template initial
                            hauteurNecessaire = GemBox.Spreadsheet.LengthUnitConverter.Convert(hauteurNecessaire, GemBox.Spreadsheet.LengthUnit.Twip, GemBox.Spreadsheet.LengthUnit.Point) * percent;

                            var hauteurDispoUnePage = GemBox.Spreadsheet.LengthUnitConverter.Convert(ws.PrintOptions.PageHeight -
                                ws.PrintOptions.TopMargin - ws.PrintOptions.BottomMargin -
                                ws.PrintOptions.FooterMargin - ws.PrintOptions.HeaderMargin, GemBox.Spreadsheet.LengthUnit.Inch, GemBox.Spreadsheet.LengthUnit.Point);
                            var hauteurDispoUnePageTwips = GemBox.Spreadsheet.LengthUnitConverter.Convert(hauteurDispoUnePage, GemBox.Spreadsheet.LengthUnit.Point, GemBox.Spreadsheet.LengthUnit.Twip);
                            // on a une section d'instruction qui dépasse une page donc le saut pourrait se faire sur 2e, 3e page...
                            while (hauteurTotale * percent >= hauteurDispoUnePageTwips)
                                hauteurTotale = hauteurTotale - hauteurDispoUnePageTwips / percent;
                            if (GemBox.Spreadsheet.LengthUnitConverter.Convert(hauteurTotale + hauteurNecessaireAvantConvert, GemBox.Spreadsheet.LengthUnit.Twip, GemBox.Spreadsheet.LengthUnit.Point) * percent > hauteurDispoUnePage)

                            {
                                if (i == 1)
                                { // on change la ligne de titre également...
                                    ws.HorizontalPageBreaks.Add(ligneCourante - 3);
                                }
                                else
                                {
                                    ws.HorizontalPageBreaks.Add(ligneCourante - 2);
                                }
                                SautDejaFait = true;

                            }
                        }

                    }
                }
            }

            noFeche = fiche.NoFiche;


            return ef;
        }


        private static string getGemboxAnchor(int ligne, int colonne)
        {
            ligne = ligne + 1;
            colonne = colonne + 1;
            var resultat = "";
            if (colonne / 26 > 0)
            {
                resultat = Convert.ToString(Convert.ToChar(Convert.ToInt32(colonne / 26) + 64));

            }
            resultat = resultat + Convert.ToString(Convert.ToChar((colonne % 26) + 64));
            resultat = resultat + ligne;
            return resultat;
        }



        private static bool AjoutImageGemBOX(GemBox.Spreadsheet.ExcelWorksheet worksheet, string imagePath, int ligneDebut, int colonneDebut, int ligneFin, int colonneFin)
        {
            // CLIENT IMAGE
            FileInfo imageFile = new FileInfo(imagePath);
            if (!imageFile.Exists)
            {
                //  imageFile = new FileInfo(blankPath);
                return false;

            }

            Image img = Image.FromFile(imagePath);

            //   img.ExifRotate();

            var picture = worksheet.Pictures.Add(imagePath, getGemboxAnchor(ligneDebut, colonneDebut), getGemboxAnchor(ligneFin, colonneFin));


            picture.Position.Mode = GemBox.Spreadsheet.PositioningMode.Move;
            // toujours 3 pixels de marge
            var maxWidth = picture.Position.Width - 6;
            var maxHeight = picture.Position.Height - 6;

            var width = (int)Math.Round(img.Width * 96.0 / img.HorizontalResolution);
            var height = (int)Math.Round(img.Height * 96.0 / img.VerticalResolution);

            img.Dispose();

            picture.Position.Width = width;
            picture.Position.Height = height;

            double ratioMax;

            var ratioWidth = width * 1.0 / maxWidth;
            var ratioHeight = height * 1.0 / maxHeight;

            ratioMax = 1.0 / (ratioHeight > ratioWidth ? ratioHeight : ratioWidth);
            ratioMax = 1.0 / (ratioHeight > ratioWidth ? ratioHeight : ratioWidth);

            // redimentionnement
            picture.Position.Width = width * ratioMax;
            picture.Position.Height = height * ratioMax;

            // centrer
            picture.Position.Left += ((maxWidth - picture.Position.Width) / 2) + 3;
            picture.Position.Top += ((maxHeight - picture.Position.Height) / 2) + 3;


            return true;

        }


        private static bool AjoutImage(OfficeOpenXml.ExcelWorksheet ws, string imagePath, string blankPath, int paddingRow, int paddingColumn, string nomImg, OfficeOpenXml.ExcelRange celluleCible, bool redimentionner = true, bool estInstruction = false, double max = 0)
        {
            FileInfo imageFile = new FileInfo(imagePath);
            if (!imageFile.Exists)
            {
                //  imageFile = new FileInfo(blankPath);
                return false;

            }

            var picture = ws.Drawings.AddPicture(nomImg, imageFile);

            //   picture.SetPosition(celluleCible.Start.Row - 1, paddingRow, celluleCible.Start.Column - 1, paddingColumn);
            //if (redimentionner)
            //{
            //    RedimentionExcel(picture);
            //}


            if (estInstruction) // centrer sur 2 colonnes
            {
                var width = (int)Math.Round(picture.Image.Width * 96.0 / picture.Image.HorizontalResolution);
                var height = (int)Math.Round(picture.Image.Height * 96.0 / picture.Image.VerticalResolution);


                paddingColumn = (int)(288 - width) / 2;
                paddingRow = ((int)(max - height) / 2) + 25;
                if (paddingRow < 0) paddingRow = 0;
                if (paddingColumn < 0) paddingColumn = 0;

            }

            picture.SetPosition(celluleCible.Start.Row - 1, paddingRow, celluleCible.Start.Column - 1, paddingColumn);

            if (redimentionner)
            {
                RedimentionExcel(picture);
            }

            return true;

        }

        public static double GetTrueColumnWidth(double width)
        {
            //DEDUCE WHAT THE COLUMN WIDTH WOULD REALLY GET SET TO
            double z = 1d;
            if (width >= (1 + 2 / 3))
            {
                z = Math.Round((Math.Round(7 * (width - 1 / 256), 0) - 5) / 7, 2);
            }
            else
            {
                z = Math.Round((Math.Round(12 * (width - 1 / 256), 0) - Math.Round(5 * width, 0)) / 12, 2);
            }

            //HOW FAR OFF? (WILL BE LESS THAN 1)
            double errorAmt = width - z;

            //CALCULATE WHAT AMOUNT TO TACK ONTO THE ORIGINAL AMOUNT TO RESULT IN THE CLOSEST POSSIBLE SETTING 
            double adj = 0d;
            if (width >= (1 + 2 / 3))
            {
                adj = (Math.Round(7 * errorAmt - 7 / 256, 0)) / 7;
            }
            else
            {
                adj = ((Math.Round(12 * errorAmt - 12 / 256, 0)) / 12) + (2 / 12);
            }

            //RETURN A SCALED-VALUE THAT SHOULD RESULT IN THE NEAREST POSSIBLE VALUE TO THE TRUE DESIRED SETTING
            if (z > 0)
            {
                return width + adj;
            }

            return 0d;
        }

        private static void RedimentionExcel(OfficeOpenXml.Drawing.ExcelPicture picture)
        {
            var width = (int)Math.Round(picture.Image.Width * 96.0 / picture.Image.HorizontalResolution);
            var height = (int)Math.Round(picture.Image.Height * 96.0 / picture.Image.VerticalResolution);

            picture.SetSize(width, height);
        }




        private static void setCellValueRT(OfficeOpenXml.ExcelWorksheet ws, int ligne, int colonne, string texte, string texteSupplementaire)
        {
            ws.Cells[ligne, colonne].IsRichText = true;
            ws.Cells[ligne, colonne].RichText.Text = "";
            ws.Cells[ligne, colonne].Value = "";

            if (texte.Trim() == "N/A")
                texte = "";

            if (texte != "")
                ws.Cells[ligne, colonne].RichText.Add(texte.Trim());
            if (texteSupplementaire != "")
            {
                var addition = ws.Cells[ligne, colonne].RichText.Add(texte.Trim() == "" ? texteSupplementaire : "\r\n" + texteSupplementaire);
                addition.Color = System.Drawing.Color.Red;
            }

        }

        private static string gereNull(string item)
        {
            if (item == null)
                return "";
            if (item.FlushNA() == "")
                return "";
            return item.Trim();
        }

        private static string gereNullInt(int? item)
        {
            if (item == null)
                return "";

            return item.ToString().Trim();
        }


        private static void PeuplerInstructionGemBOX(GemBox.Spreadsheet.ExcelWorksheet ws, int ligne, LigneInstruction instruction, double defaultHeight, string langue)
        {

            if (ws.Parent.Worksheets.Count == 1)
                ws.Parent.Worksheets.Add("tempo");
            double max = defaultHeight;

            ws.Cells[ligne, 0].Value = instruction.NoLigne;
            //on ajoute un Enter entre les instructions et le texte supplémentaire si les 2 sont présents. 

            var texteInstruction = gereNull(langue == "" ? instruction.Instruction.TexteInstruction : instruction.Instruction.TexteInstructionEN);
            // IF texteInstruction is empty, an error will occur
            if (texteInstruction == "")
            {
                texteInstruction = " ";
            }

            var texteSupplementaireInstruction = gereNull(langue == "" ? instruction.TexteSupplementaireInstruction : instruction.TexteSupplementaireInstructionEN);
            var texteDispositif = gereNull(langue == "" ? instruction.Instruction.Dispositif.Description : instruction.Instruction.Dispositif.DescriptionEN);
            var texteSupplementaireDispositif = gereNull(langue == "" ? instruction.TexteSupplementaireDispositif : instruction.TexteSupplementaireDispositifEN);
            var texteAccessoire = gereNull(langue == "" ? instruction.Instruction.Accessoire.Description : instruction.Instruction.Accessoire.DescriptionEN);
            var texteImage = gereNullInt(instruction.PhotoFicheCadenassage?.Rang);

            //traitement spécifique pour Choix2
            var CodeInstruction = gereNull(langue == "" ? instruction.Instruction.Identificateur : instruction.Instruction.IdentificateurEN);
            var estChoix = false;
            if (CodeInstruction.ToUpper() == "CHOIX-2")
            {// on force le passage par le bloc suivant soit les 4 colonnes vides
                texteSupplementaireDispositif = "";
                texteSupplementaireInstruction = "";
                texteImage = "";
                estChoix = true;
            }

            // Si les 4 colonnes sont vides, on met l'instruction sur toute la ligne

            if (texteDispositif == "" && texteSupplementaireDispositif == "" && texteAccessoire == "" && texteImage == "")
            {
                ws.Cells.GetSubrangeRelative(ligne, 1, 5, 1).Merged = false;
                ws.Cells.GetSubrangeRelative(ligne, 6, 3, 1).Merged = false;
                ws.Cells.GetSubrangeRelative(ligne, 9, 3, 1).Merged = false;
                ws.Cells.GetSubrangeRelative(ligne, 1, 12, 1).Merged = true;

                ws.Cells[ligne, 1].Value = texteInstruction + (texteInstruction != "" && texteSupplementaireInstruction != "" ? "\r" : "") + texteSupplementaireInstruction;
                if (texteInstruction.Length > 0)
                    ws.Cells[ligne, 1].GetCharacters(0, texteInstruction.Length).Font.Weight = 700; //gras


                GetHeight(ws.Cells.GetSubrangeRelative(ligne, 1, 12, 1), texteInstruction, texteSupplementaireInstruction, ws.Cells[ligne, 1].Style.Font, ws.Parent.Worksheets[1], true, ref max);
                ws.Rows[ligne].Height = Convert.ToInt32(max);

                if (estChoix && texteInstruction.Length > 0)
                {
                    ws.Cells[ligne, 1].GetCharacters(0, texteInstruction.Length).Font.Color = GemBox.Spreadsheet.SpreadsheetColor.FromName(GemBox.Spreadsheet.ColorName.Red);
                    ws.Cells[ligne, 1].Style.HorizontalAlignment = GemBox.Spreadsheet.HorizontalAlignmentStyle.Center;
                    ws.Cells[ligne, 1].Style.FillPattern.SetSolid(GemBox.Spreadsheet.SpreadsheetColor.FromName(GemBox.Spreadsheet.ColorName.Yellow));
                }


            }
            else
            {
                ws.Cells[ligne, 1].Value = texteInstruction + (texteInstruction != "" && texteSupplementaireInstruction != "" ? "\r\n" : "") + texteSupplementaireInstruction;
                if (texteInstruction.Length > 0)
                    ws.Cells[ligne, 1].GetCharacters(0, texteInstruction.Length).Font.Weight = 700; //gras

                ws.Cells[ligne, 6].Value = texteDispositif +
                   (texteDispositif != "" && texteSupplementaireDispositif != "" ? "\r\n" : "") + texteSupplementaireDispositif;
                if (texteDispositif.Length > 0)
                    ws.Cells[ligne, 6].GetCharacters(0, texteDispositif.Length).Font.Weight = 700;

                ws.Cells[ligne, 9].Value = texteAccessoire;
                if (texteAccessoire.Length > 0)
                    ws.Cells[ligne, 9].GetCharacters(0, texteAccessoire.Length).Font.Weight = 700;

                ws.Cells[ligne, 12].Value = texteImage;


                GetHeight(ws.Cells.GetSubrangeRelative(ligne, 1, 5, 1), texteInstruction, texteSupplementaireInstruction, ws.Cells[ligne, 1].Style.Font, ws.Parent.Worksheets[1], true, ref max);
                var hauteurPoints = GemBox.Spreadsheet.LengthUnitConverter.Convert(max, GemBox.Spreadsheet.LengthUnit.Twip, GemBox.Spreadsheet.LengthUnit.Point);
                GetHeight(ws.Cells.GetSubrangeRelative(ligne, 6, 3, 1), texteDispositif, texteSupplementaireDispositif, ws.Cells[ligne, 1].Style.Font, ws.Parent.Worksheets[1], true, ref max);
                GetHeight(ws.Cells.GetSubrangeRelative(ligne, 9, 3, 1), texteAccessoire, "", ws.Cells[ligne, 1].Style.Font, ws.Parent.Worksheets[1], true, ref max);
                GetHeight(ws.Cells.GetSubrangeRelative(ligne, 12, 1, 1), texteImage, "", ws.Cells[ligne, 1].Style.Font, ws.Parent.Worksheets[1], true, ref max);
                ws.Rows[ligne].Height = Convert.ToInt32(max);

            }

        }


        private static void PeuplerLigneDecadenassageGemBOX(GemBox.Spreadsheet.ExcelWorksheet ws, int ligne, LigneDecadenassage instruction, double defaultHeight, string langue)
        {


            if (ws.Parent.Worksheets.Count == 1)
                ws.Parent.Worksheets.Add("tempo");
            double max = defaultHeight;

            ws.Cells[ligne, 0].Value = instruction.NoLigne;
            //on ajoute un Enter entre les instructions et le texte supplémentaire si les 2 sont présents. 

            var texteInstruction = gereNull(langue == "" ? instruction.Instruction.TexteInstruction : instruction.Instruction.TexteInstructionEN);
            // IF texteInstruction is empty, an error will occur
            if (texteInstruction == "")
            {
                texteInstruction = " ";
            }
            var texteSupplementaireInstruction = gereNull(langue == "" ? instruction.TexteSupplementaireInstruction : instruction.TexteSupplementaireInstructionEN);
            var texteDispositif = gereNull(langue == "" ? instruction.Instruction.Dispositif.Description : instruction.Instruction.Dispositif.DescriptionEN);
            var texteSupplementaireDispositif = gereNull(langue == "" ? instruction.TexteSupplementaireDispositif : instruction.TexteSupplementaireInstructionEN);
            var texteAccessoire = gereNull(langue == "" ? instruction.Instruction.Accessoire.Description : instruction.Instruction.Accessoire.DescriptionEN);
            var texteImage = gereNullInt(instruction.PhotoFicheCadenassage?.Rang);

            //traitement spécifique pour Choix2
            var CodeInstruction = gereNull(instruction.Instruction.Identificateur);
            var estChoix = false;
            if (CodeInstruction.ToUpper() == "CHOIX-2")
            {// on force le passage par le bloc suivant soit les 4 colonnes vides
                texteSupplementaireDispositif = "";
                texteSupplementaireInstruction = "";
                texteImage = "";
                estChoix = true;
            }

            // Si les 4 colonnes sont vides, on met l'instruction sur toute la ligne

            if (texteDispositif == "" && texteSupplementaireDispositif == "" && texteAccessoire == "" && texteImage == "")
            {
                ws.Cells.GetSubrangeRelative(ligne, 1, 5, 1).Merged = false;
                ws.Cells.GetSubrangeRelative(ligne, 6, 3, 1).Merged = false;
                ws.Cells.GetSubrangeRelative(ligne, 9, 3, 1).Merged = false;
                ws.Cells.GetSubrangeRelative(ligne, 1, 12, 1).Merged = true;

                ws.Cells[ligne, 1].Value = texteInstruction + (texteInstruction != "" && texteSupplementaireInstruction != "" ? "\r" : "") + texteSupplementaireInstruction;
                ws.Cells[ligne, 1].GetCharacters(0, texteInstruction.Length).Font.Weight = 700; //gras


                GetHeight(ws.Cells.GetSubrangeRelative(ligne, 1, 12, 1), texteInstruction, texteSupplementaireInstruction, ws.Cells[ligne, 1].Style.Font, ws.Parent.Worksheets[1], true, ref max);
                ws.Rows[ligne].Height = Convert.ToInt32(max);


                if (estChoix)
                {
                    ws.Cells[ligne, 1].GetCharacters(0, texteInstruction.Length).Font.Color = GemBox.Spreadsheet.SpreadsheetColor.FromName(GemBox.Spreadsheet.ColorName.Red);
                    ws.Cells[ligne, 1].Style.HorizontalAlignment = GemBox.Spreadsheet.HorizontalAlignmentStyle.Center;
                    ws.Cells[ligne, 1].Style.FillPattern.SetSolid(GemBox.Spreadsheet.SpreadsheetColor.FromName(GemBox.Spreadsheet.ColorName.Yellow));
                }

            }
            else
            {
                ws.Cells[ligne, 1].Value = texteInstruction + (texteInstruction != "" && texteSupplementaireInstruction != "" ? "\r" : "") + texteSupplementaireInstruction;
                ws.Cells[ligne, 1].GetCharacters(0, texteInstruction.Length).Font.Weight = 700; //gras

                ws.Cells[ligne, 6].Value = texteDispositif +
                   (texteDispositif != "" && texteSupplementaireDispositif != "" ? "\r" : "") + texteSupplementaireDispositif;
                if (texteDispositif.Length > 0)
                    ws.Cells[ligne, 6].GetCharacters(0, texteDispositif.Length).Font.Weight = 700;

                ws.Cells[ligne, 9].Value = texteAccessoire;
                ws.Cells[ligne, 12].Value = texteImage;


                GetHeight(ws.Cells.GetSubrangeRelative(ligne, 1, 5, 1), texteInstruction, texteSupplementaireInstruction, ws.Cells[ligne, 1].Style.Font, ws.Parent.Worksheets[1], true, ref max);
                GetHeight(ws.Cells.GetSubrangeRelative(ligne, 6, 3, 1), texteDispositif, texteSupplementaireDispositif, ws.Cells[ligne, 1].Style.Font, ws.Parent.Worksheets[1], true, ref max);
                GetHeight(ws.Cells.GetSubrangeRelative(ligne, 9, 3, 1), texteAccessoire, "", ws.Cells[ligne, 1].Style.Font, ws.Parent.Worksheets[1], false, ref max);
                GetHeight(ws.Cells.GetSubrangeRelative(ligne, 12, 1, 1), texteImage, "", ws.Cells[ligne, 1].Style.Font, ws.Parent.Worksheets[1], true, ref max);
                ws.Rows[ligne].Height = Convert.ToInt32(max);

            }
        }




        private static void GetHeight(GemBox.Spreadsheet.CellRange myMergedCells, string textA, string textB, GemBox.Spreadsheet.ExcelFont font, GemBox.Spreadsheet.ExcelWorksheet tempoSheet, bool firstBold, ref double max)
        {
            if (textA == null)
                textA = "";
            if (textB == null)
                textB = "";

            var ws = tempoSheet;

            ws.Cells[0, 0].Column.Width = myMergedCells.Sum(x => x.Column.Width);

            if (textA != "" && textB != "")
            {
                textB = "\r\n" + textB;
            }

            ws.Cells[0, 0].Value = textA + textB;

            if (textA.Length > 0 && firstBold)
                ws.Cells[0, 0].GetCharacters(0, textA.Length).Font.Weight = 700; //gras
            ws.Cells[0, 0].Style.Font.Name = font.Name;
            ws.Cells[0, 0].Style.Font.Size = font.Size;

            ws.Cells[0, 0].Style.WrapText = true;

            ws.Cells[0, 0].Row.AutoFit();
            double result = ws.Cells[0, 0].Row.Height;


            if (result > max)
                max = result;

            var hauteurPoints = GemBox.Spreadsheet.LengthUnitConverter.Convert(max, GemBox.Spreadsheet.LengthUnit.Twip, GemBox.Spreadsheet.LengthUnit.Point);

        }




        public static void MeasureTextHeightGemBOX(string text, GemBox.Spreadsheet.ExcelFont font, double width, ref double Max,
            FontStyle fontStyle)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }
            var bitmap = new Bitmap(1, 1);
            var graphics = Graphics.FromImage(bitmap);
            var pixelWidth = Convert.ToInt32(width * 96 / 72);  //points vers pixels
            var drawingFont = new Font(font.Name, font.Size, fontStyle);
            var size = graphics.MeasureString(text, drawingFont, pixelWidth);

            //72 DPI and 96 points per inch.  Excel height in points with max of 409 per Excel requirements.
            var result = Convert.ToDouble(size.Height) * 72 / 96; // pixels vers points
            if (result > Max)
                Max = result;
        }

        #endregion

        #region GridViewFiches

        internal DataSourceResult GetReadCadenas(DataSourceRequest request, int clientId)
        {
            Boolean droitSuppressionClient = false;
            Boolean droitSuppressionPrevi = false;

            var langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            var droits = Helpers.GetDroitsPrevi.ObtenirDroits();

            var session = utilisateurService.GetSession();


            if (session != null && session.AdmPrevicad)
                droitSuppressionClient = true;


            droitSuppressionPrevi = droits.EstAdminPrevi || droits.EstUpdatePrevi;
            //droitSuppressionClient = false;


            var result = ficheCadenassageRepository.AsQueryable().Where(x => x.ClientId == clientId).Select(x => new LigneCadenassageGridViewModel()
            {
                Departement = langue == "fr" ? x.Departement.NomDepartement : x.Departement.NomDepartementEN,
                EquipementId = x.EquipementId,
                NumeroEquipment = x.Equipement.NumeroEquipement,
                FicheCadenassageId = x.FicheCadenassageId,
                NoFiche = x.NoFiche,
                NomEquipement = langue == "fr" ? x.Equipement.NomEquipement : x.Equipement.NomEquipementEN,
                ClientId = x.ClientId,
                TravailAEffectuer = langue == "fr" ? x.TravailAEffectuer : x.TravailAEffectuerEN,
                TitreFiche = langue == "fr" ? x.TitreFiche : x.TitreFicheEN,
                estDocumentPrevigesst = x.estDocumentPrevigesst,
                //RevisionCourante = x.RevisionCourante,
                TexteMateriel = x.MaterielsRequisCadenassage.Select(z => z.Materiel.Description + " (" + z.Quantite.ToString() + ")").ToList(),
                //Suppressible = ((x.estDocumentPrevigesst == true) && droitSuppressionPrevi) || (x.estDocumentPrevigesst == false && droitSuppressionClient)
                Suppressible = (x.estDocumentPrevigesst == true && droitSuppressionPrevi) || (x.estDocumentPrevigesst == false && droitSuppressionClient) || (x.estDocumentPrevigesst == false && droitSuppressionPrevi),

                ApprouvePar = x.ApprouvePar,
                DateApproved = x.DateApproved,

                CreatedPar = x.CreatedPar,
                DateCreation = x.DateCreation,

                UpdatedPar = x.UpdatedPar,
                DateUpdated = x.DateUpdated,

                IsApproved = x.IsApproved

                //DateRevision = x.DateRevision,

            }).ToDataSourceResult(request);

            return result;
        }

        internal DataSourceResult GetReadCadenasForRegistre(DataSourceRequest request, int clientId)
        {
            //Boolean droitSuppressionClient = false;
            Boolean droitSuppressionPrevi = false;
            var langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            var droits = Helpers.GetDroitsPrevi.ObtenirDroits();
            droitSuppressionPrevi = droits.EstAdminPrevi || droits.EstUpdatePrevi;
            //droitSuppressionClient = false;

            var result = ficheCadenassageRepository.AsQueryable()
                        .Where(x => x.ClientId == clientId && x.IsApproved == true)
                        .Select(x => new LigneRegistreViewModel()
                        {
                            FicheCadenassageId = x.FicheCadenassageId,
                            NoFicheCadenassage = x.NoFiche,
                            NomEquipement = langue == "fr" ? x.Equipement.NomEquipement : x.Equipement.NomEquipementEN,
                            NomDepartement = langue == "fr" ? x.Departement.NomDepartement : x.Departement.NomDepartementEN,
                            TravailAEffectuer = langue == "fr" ? x.TravailAEffectuer : x.TravailAEffectuerEN,
                            TitreFiche = langue == "fr" ? x.TitreFiche : x.TitreFicheEN,
                            LigneRegistreId = 0,
                            DateDebut = DateTime.Today,

                            TexteMateriel = x.MaterielsRequisCadenassage.Select(z => langue == "fr" ? z.Materiel.Description + " (" + z.Quantite.ToString() + ")" : z.Materiel.DescriptionEN + " (" + z.Quantite.ToString() + ")").ToList(),
                            EquipementId = x.EquipementId

                        }).ToDataSourceResult(request);

            return result;
        }

        internal DataSourceResult GetReadCadenasForRegistre(DataSourceRequest request, int clientId, int equipementId)
        {
            return GetReadCadenasForRegistre(clientId, equipementId).ToDataSourceResult(request);
        }

        internal IQueryable<LigneRegistreViewModel> GetReadCadenasForRegistre(int clientId, int equipementId)
        {
            //Boolean droitSuppressionClient = false;
            Boolean droitSuppressionPrevi = false;
            var langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            var droits = Helpers.GetDroitsPrevi.ObtenirDroits();
            droitSuppressionPrevi = droits.EstAdminPrevi || droits.EstUpdatePrevi;
            //droitSuppressionClient = false;

            var result = ficheCadenassageRepository.AsQueryable()
                        .Where(x => x.ClientId == clientId && x.IsApproved == true)
                        .Where(x => x.EquipementId == equipementId)
                        .Select(x => new LigneRegistreViewModel()
                        {
                            FicheCadenassageId = x.FicheCadenassageId,
                            NoFicheCadenassage = x.NoFiche,
                            NomEquipement = langue == "fr" ? x.Equipement.NomEquipement : x.Equipement.NomEquipementEN,
                            NomDepartement = langue == "fr" ? x.Departement.NomDepartement : x.Departement.NomDepartementEN,
                            TravailAEffectuer = langue == "fr" ? x.TravailAEffectuer : x.TravailAEffectuerEN,
                            TitreFiche = langue == "fr" ? x.TitreFiche : x.TitreFicheEN,
                            LigneRegistreId = 0,
                            DateDebut = DateTime.Today,

                            TexteMateriel = x.MaterielsRequisCadenassage.Select(z => langue == "fr" ? z.Materiel.Description + " (" + z.Quantite.ToString() + ")" : z.Materiel.DescriptionEN + " (" + z.Quantite.ToString() + ")").ToList(),
                            EquipementId = x.EquipementId

                        });

            return result;
        }

        public EditFicheViewModel getFicheVM(int FicheCadenassageId)
        {
            var langue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;



            var result = ficheCadenassageRepository.Get(FicheCadenassageId);
            var vm = new EditFicheViewModel
            {
                ClientId = result.ClientId,
                //DateRevision = result.DateRevision,
                EquipementId = result.EquipementId,
                DepartementId = result.DepartementId,
                FicheCadenassageId = result.FicheCadenassageId,
                NoFiche = result.NoFiche,
                TravailAEffectuer = result.TravailAEffectuer,
                TravailAEffectuerEN = result.TravailAEffectuerEN,
                SourcesEnergieId = result.SourcesEnergieCadenassage.Select(x => x.SourceEnergieId).ToList(),
                NomClient = result.Client.Nom,
                AfficherClient = result.AfficherClient,
                EstDocumentPrevigesst = result.estDocumentPrevigesst,
                TitreFiche = langue == "fr" ? result.TitreFiche : result.TitreFicheEN,
                //RevisionCourante = result.RevisionCourante,
                IsApproved = result.IsApproved,

                ApprouvePar = result.ApprouvePar,
                DateApproved = result.DateApproved,

                CreatedPar = result.CreatedPar,
                DateCreation = result.DateCreation,

                UpdatedPar = result.UpdatedPar,
                DateUpdated = result.DateUpdated

            };

            return vm;

        }

        public void RenumeroterLignes(int FicheCadenassageId)
        {
            var fiche = ficheCadenassageRepository.Get(FicheCadenassageId);
            int i = 1;
            foreach (var v in fiche.LignesInstruction.Where(x => x.InstructionId != null).OrderBy(x => x.NoLigne))
            {
                v.NoLigne = i;
                i++;
            }
            ficheCadenassageRepository.SaveChanges();
        }


        public void RenumeroterLignesDEC(int FicheCadenassageId)
        {
            var fiche = ficheCadenassageRepository.Get(FicheCadenassageId);
            int i = 1;
            foreach (var v in fiche.LignesDecadenassage.Where(x => x.InstructionId != null).OrderBy(x => x.NoLigne))
            {
                v.NoLigne = i;
                i++;
            }
            ficheCadenassageRepository.SaveChanges();
        }

        public void SaveSources(SourceEnergieCadenassageViewModel sources)
        {
            var SourcesAvant = ficheCadenassageRepository.Get(sources.FicheCadenassageId).SourcesEnergieCadenassage.Select(x => x.SourceEnergieId).ToList().Distinct();
            foreach (var x in SourcesAvant)
            {


            }
        }

        public void SaveFiche(EditFicheViewModel model)
        {
            var item = ficheCadenassageRepository.Get(model.FicheCadenassageId);
            var AjouterEtapesDecadenassage = false;
            if (item == null)
            {
                item = new Models.FicheCadenassage();
                AjouterEtapesDecadenassage = true;

                item.ApprouvePar = null;
                item.DateApproved = null;
                item.IsApproved = false;
            }

            if (AjouterEtapesDecadenassage == false)
            {
                if (item.DepartementId != model.DepartementId || item.EquipementId != model.EquipementId ||
                    item.FicheCadenassageId != model.FicheCadenassageId || item.NoFiche != model.NoFiche ||
                    item.TravailAEffectuer != model.TravailAEffectuer ||
                    item.TravailAEffectuerEN != model.TravailAEffectuerEN ||
                    item.estDocumentPrevigesst != model.EstDocumentPrevigesst ||
                    (item.TitreFiche != model.TitreFiche && item.TitreFicheEN != model.TitreFiche))
                {
                    item.ApprouvePar = null;
                    item.DateApproved = null;
                    item.IsApproved = false;

                    item.UpdatedPar = model.UpdatedPar;
                    item.DateUpdated = model.DateUpdated;
                }
            }

            item.ClientId = model.ClientId;
            item.DepartementId = model.DepartementId;
            item.EquipementId = model.EquipementId;
            item.FicheCadenassageId = model.FicheCadenassageId;
            item.NoFiche = model.NoFiche;
            item.TravailAEffectuer = model.TravailAEffectuer;
            item.TravailAEffectuerEN = model.TravailAEffectuerEN;
            item.estDocumentPrevigesst = model.EstDocumentPrevigesst;
            item.TitreFiche = model.TitreFiche;


            item.CreatedPar = model.CreatedPar;
            item.DateCreation = model.DateCreation;


            //item.DateRevision = model.DateRevision;


            if (model.TitreFiche == "Fiche de cadenassage" || model.TitreFiche == "Lockout procedure")
            {
                item.TitreFiche = "Fiche de cadenassage";
                item.TitreFicheEN = "Lockout procedure";
            }
            else
            {
                item.TitreFiche = "Procédure de travail sécuritaire";
                item.TitreFicheEN = "Safe work procedure";
            }

            item.AfficherClient = model.AfficherClient;
            //item.RevisionCourante = model.RevisionCourante;

            if (item.FicheCadenassageId > 0)
            {
                ficheCadenassageRepository.Update(item);
            }

            else
            {
                ficheCadenassageRepository.Add(item);
            }

            ficheCadenassageRepository.SaveChanges();

            if (AjouterEtapesDecadenassage)
            {
                AjouterLignesDecadenassageDefaut(item.FicheCadenassageId);
            }

            model.FicheCadenassageId = item.FicheCadenassageId;
            //var equipement = equipementRepository.Get(item.EquipementId);
        }

        public void ApproveFiche(EditFicheViewModel model)
        {
            var item = ficheCadenassageRepository.Get(model.FicheCadenassageId);
            var AjouterEtapesDecadenassage = false;
            if (item == null)
            {
                item = new Models.FicheCadenassage();
                AjouterEtapesDecadenassage = true;
            }


            item.ApprouvePar = model.ApprouvePar;
            item.DateApproved = model.DateApproved;
            item.IsApproved = true;

            if (model.TitreFiche == "Fiche de cadenassage" || model.TitreFiche == "Lockout procedure")
            {
                item.TitreFiche = "Fiche de cadenassage";
                item.TitreFicheEN = "Lockout procedure";
            }
            else
            {
                item.TitreFiche = "Procédure de travail sécuritaire";
                item.TitreFicheEN = "Safe work procedure";
            }

            if (item.FicheCadenassageId > 0)
            {
                ficheCadenassageRepository.Update(item);
                ficheCadenassageRepository.SaveChanges();
            }

            if (AjouterEtapesDecadenassage)
            {
                AjouterLignesDecadenassageDefaut(item.FicheCadenassageId);
            }
        }


        private void AjouterLignesDecadenassageDefaut(int ficheCadenassageId)
        {
            return;

            //ligneDecadenassageRepository.Add(new LigneDecadenassage
            //{
            //    FicheCadenassageId = ficheCadenassageId,
            //    NoLigne = 1,
            //    Realiser = true,
            //    //  texteInstruction = "Aviser les personnes concernées et enlever les outils autour de l'équipement"

            //});

            //ligneDecadenassageRepository.Add(new LigneDecadenassage
            //{
            //    FicheCadenassageId = ficheCadenassageId,
            //    NoLigne = 2,
            //    Realiser = true,
            //    //   texteInstruction = "Décadenasser et enlever les étiquettes et moraillons en suivant les étapes inverses du cadenassage"

            //});

            //ligneDecadenassageRepository.Add(new LigneDecadenassage
            //{
            //    FicheCadenassageId = ficheCadenassageId,
            //    NoLigne = 3,
            //    Realiser = true,
            //    //   texteInstruction = "Démarrer la machine ou la section de l'équipement nécessaire à l'essai en procédant avec précaution"

            //});

            //ligneDecadenassageRepository.SaveChanges();

        }

        public bool Supprimer(LigneCadenassageGridViewModel model)
        {
            var fiche = ficheCadenassageRepository.Get(model.FicheCadenassageId);
            if (fiche == null)
                return true;


            sourceEnergieCadenassageRepository.SupprimerDeFiche(fiche.FicheCadenassageId);
            ligneInstructionRepository.SupprimerDeFiche(fiche.FicheCadenassageId);
            ligneDecadenassageRepository.SupprimerDeFiche(fiche.FicheCadenassageId);
            materielRequisCadenassageRepository.SupprimerDeFiche(fiche.FicheCadenassageId);
            photoFicheCadenassageRepository.SupprimerDeFiche(fiche.FicheCadenassageId);

            // var isPrevi = ficheCadenassageRepository.AsQueryable().Select(x => x.estDocumentPrevigesst);

            ficheCadenassageRepository.Delete(fiche.FicheCadenassageId);
            ficheCadenassageRepository.SaveChanges();





            return true;
        }

        public int DupliquerFiche(int FicheInitialeId)
        {
            var itemBase = ficheCadenassageRepository.Get(FicheInitialeId);

            // infos générales 
            var fiche = getFicheVM(FicheInitialeId);
            var session = utilisateurService.GetSession();
            fiche.EstDocumentPrevigesst = (session == null);
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


        void createOrCleanRep(string repertoire)
        {
            if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(repertoire)))
                System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath(repertoire));
            foreach (var f in System.IO.Directory.GetFiles(HttpContext.Current.Server.MapPath(repertoire)))
            {
                System.IO.File.Delete(f);
            }
        }

        void copyFiles(string repInitial, string repFinal)
        {
            var fichiersSource = System.IO.Directory.GetFiles(HttpContext.Current.Server.MapPath(repInitial));
            foreach (var f in fichiersSource)
                System.IO.File.Copy(f, HttpContext.Current.Server.MapPath(repFinal) + Path.GetFileName(f));

        }

        public void UnapproveFiche(int ficheCadenassageId, string updatedPar)
        {
            var item = ficheCadenassageRepository.Get(ficheCadenassageId);
            if (item != null)
            {
                item.UpdatedPar = updatedPar;
                item.DateUpdated = DateTime.Now;

                item.ApprouvePar = null;
                item.DateApproved = null;
                item.IsApproved = false;

                ficheCadenassageRepository.Update(item);
                ficheCadenassageRepository.SaveChanges();
            }
        }
    }

    #endregion

}