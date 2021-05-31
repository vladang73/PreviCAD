using OmuModified.Drawing;
using Previgesst.Enums;
using Previgesst.Repositories;
using Previgesst.Services;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Previgesst.Controllers
{
    public class UploadController : Controller
    {
        private EquipementService equipementService;
        private EquipementRepository equipementRepository;
        private LigneInstructionRepository ligneInstructionRepository;
        private LigneInstructionService ligneInstructionService;
        private LigneDecadenassageRepository ligneDecadenassageRepository;
        private ClientRepository clientRepository;
        private UtilisateurService utilisateurService;
        private PhotoFicheCadenassageRepository photoFicheCadenassageRepository;
        private HttpServerUtilityBase server;
        
        public UploadController(EquipementService equipementService, EquipementRepository equipementRepository, LigneInstructionRepository ligneInstructionRepository,
            LigneInstructionService ligneInstructionService, LigneDecadenassageRepository ligneDecadenassageRepository,
            ClientRepository clientRepository, UtilisateurService utilisateurService,
            PhotoFicheCadenassageRepository photoFicheCadenassageRepository)
        {
            this.equipementService = equipementService;
            this.equipementRepository = equipementRepository;
            this.ligneInstructionRepository = ligneInstructionRepository;
            this.ligneInstructionService = ligneInstructionService;
            this.ligneDecadenassageRepository = ligneDecadenassageRepository;
            this.clientRepository = clientRepository;
            this.utilisateurService = utilisateurService;
            this.photoFicheCadenassageRepository = photoFicheCadenassageRepository;

        }

        public ActionResult setEquipPic(int Id)
        {
            var vm = new UploadViewModel();
            vm.ItemId = Id;
            vm.Extensions = "fileSelectJpg";
            vm.Message = "Seuls les fichiers ayant l'extension .jpg ou .jpeg peuvent être utilisés.";
            vm.typeUpload = Enums.TypeUpload.Equipement;

            return View("Index", vm);
        }

        public ActionResult setDECPic(int Id)
        {
            var vm = new UploadViewModel();
            vm.ItemId = Id;
            vm.Extensions = "fileSelectJpg";
            vm.Message = "Seuls les fichiers ayant l'extension .jpg ou .jpeg peuvent être utilisés.";
            vm.typeUpload = Enums.TypeUpload.Decadenassage;

            return View("Index", vm);
        }


        public ActionResult setInstrPic(int Id)
        {
            var vm = new UploadViewModel();
            vm.ItemId = Id;
            vm.Extensions = "fileSelectJpg";
            vm.Message = "Seuls les fichiers ayant l'extension .jpg ou .jpeg peuvent être utilisés.";
            vm.typeUpload = Enums.TypeUpload.Instruction;

            return View("Index", vm);
        }


        public ActionResult setClientPic(int Id)
        {
            var vm = new UploadViewModel();
            vm.ItemId = Id;
            vm.Extensions = "fileSelectJpg";
            vm.Message = "Seuls les fichiers ayant l'extension .jpg ou .jpeg peuvent être utilisés.";
            vm.typeUpload = Enums.TypeUpload.Client;

            return View("Index", vm);
        }
        //   e.data = { DocumentId: @Model.ItemId, TypeDoc:@Model.typeUpload };

        public JsonResult SaveLinkWithContext(IEnumerable<HttpPostedFileBase> file, int DocumentId, TypeUpload TypeDoc, HttpServerUtilityBase server)
        {
            this.server = server;

            return SaveLink(file, DocumentId, TypeDoc);
        }
        public JsonResult SaveLink(IEnumerable<HttpPostedFileBase> file, int DocumentId, TypeUpload TypeDoc)
        {

            if (this.server == null)
                server = this.ControllerContext.HttpContext.Server ;
           

            var fichier = file.ToList()[0];
            if (fichier == null)
                return Json(new { Type = "Erreur" }, JsonRequestBehavior.AllowGet);

            var nomFichier = fichier.FileName.RemoveSpecialCharacters();




            if (TypeDoc == TypeUpload.Equipement)
            {
                
                if (utilisateurService.VerifierBonClientCadenassage_Equipement( DocumentId, true))
                {
                var repertoire = @"~/Images/Cadenassage/Equipements/" + DocumentId + "/";
                createOrCleanRep(repertoire);                
                fichier.SaveAs(System.IO.Path.Combine(server.MapPath(repertoire), nomFichier));
                    
                saveTOXLSize(server.MapPath(repertoire), nomFichier);
                saveThumb(server.MapPath(repertoire), nomFichier);
                var vm = equipementRepository.Get(DocumentId);
                vm.NomFichier = nomFichier;
                equipementRepository.Update(vm);
                equipementRepository.SaveChanges();
                }

                return Json(new { Type = "Done" }, JsonRequestBehavior.AllowGet);
            }
            else if (TypeDoc == TypeUpload.Client)
            {
                if (utilisateurService.VerifierBonClientCadenassage_Client( DocumentId, true))
                {
                var repertoire = @"~/Images/Cadenassage/Clients/" + DocumentId + "/";
                createOrCleanRep(repertoire);
                fichier.SaveAs(System.IO.Path.Combine(server.MapPath(repertoire), nomFichier));
                saveTOXLSize(server.MapPath(repertoire), nomFichier);
                saveThumb(server.MapPath(repertoire), nomFichier);
                var vm = clientRepository.Get(DocumentId);
                vm.Logo = nomFichier;
                clientRepository.Update(vm);
                clientRepository.SaveChanges();
                return Json(new { Type = "Done" }, JsonRequestBehavior.AllowGet);
                }

            }
            else if (TypeDoc == TypeUpload.Instruction)
            {
                if (utilisateurService.VerifierBonClientCadenassage_Instruction( DocumentId,true))
                {
                var repertoire = @"~/Images/Cadenassage/Instructions/" + DocumentId + "/";
                createOrCleanRep(repertoire);
                fichier.SaveAs(System.IO.Path.Combine(server.MapPath(repertoire), nomFichier));
                saveTOXLSize(server.MapPath(repertoire), nomFichier);
                saveThumb(server.MapPath(repertoire), nomFichier);
                var vm = ligneInstructionRepository.Get(DocumentId);
             //   vm.NomFichier = nomFichier;
                ligneInstructionRepository.Update(vm);
                ligneInstructionRepository.SaveChanges();
                }

                return Json(new { Type = "Done" }, JsonRequestBehavior.AllowGet);

            }
            else if (TypeDoc == TypeUpload.ImageCadenassage)
            {
                if (utilisateurService.VerifierBonClientCadenassage_PhotoCadenassage( DocumentId, true))
                {
                    var repertoire = @"~/Images/Cadenassage/Photos/" + DocumentId + "/";
                    createOrCleanRep(repertoire);
                    var photo = photoFicheCadenassageRepository.Get(DocumentId);
                    photo.Fichier = nomFichier;
                    photoFicheCadenassageRepository.SaveChanges();
                    fichier.SaveAs(System.IO.Path.Combine(server.MapPath(repertoire), nomFichier));
                    saveTOXLSize(server.MapPath(repertoire), nomFichier);
                    saveThumb(server.MapPath(repertoire), nomFichier);
                   
                }

                return Json(new { Type = "Done" }, JsonRequestBehavior.AllowGet);

            }


            else if (TypeDoc == TypeUpload.Document)
            {
               

            }
            else if ( TypeDoc == TypeUpload.Decadenassage)

            {
                if (utilisateurService.VerifierBonClientCadenassage_LigneDecadenassage( DocumentId, true))
                {
                var repertoire = @"~/Images/Cadenassage/Decadenassage/" + DocumentId + "/";
                createOrCleanRep(repertoire);
                fichier.SaveAs(System.IO.Path.Combine(server.MapPath(repertoire), fichier.FileName));
                saveTOXLSize(server.MapPath(repertoire), fichier.FileName);
                saveThumb(server.MapPath(repertoire), fichier.FileName);
                var vm = ligneDecadenassageRepository.Get(DocumentId);
               // vm.NomFichier = fichier.FileName;
                ligneDecadenassageRepository.Update(vm);
                ligneDecadenassageRepository.SaveChanges();
                }

                return Json(new { Type = "Done" }, JsonRequestBehavior.AllowGet);
            }


         

            return Json(new { Type = "Upload" }, JsonRequestBehavior.AllowGet);

        }




        // ADD OR REPLACE IMAGE FOLDER
        void createOrCleanRep(string repertoire)
        {
            var mycontroller = new VideController();
            if (!System.IO.Directory.Exists(this.server.MapPath(repertoire)))
                System.IO.Directory.CreateDirectory(this.server.MapPath(repertoire));
            foreach (var f in System.IO.Directory.GetFiles(this.server.MapPath(repertoire)))
            {
                System.IO.File.Delete(f);
            }
        }

        void saveThumb(string repertoire, string filename)
        {
            Image image = Image.FromFile(System.IO.Path.Combine(repertoire, filename));
            Image thumb = image.GetThumbnailImage(200, 200, () => false, IntPtr.Zero);

            thumb.Save(System.IO.Path.Combine(repertoire, "thumb.jpg"));
            image.Dispose();
            thumb.Dispose();
        }




        void saveTOXLSize(string repertoire, string filename)
        {
            var pathItem = System.IO.Path.Combine(repertoire, filename);
            var image = Image.FromFile(pathItem);
            image.ExifRotate();
            {

                if (image.Size.Width > image.HorizontalResolution * 1.07 || image.Size.Height > image.VerticalResolution * 1.07)
                {
                    var resized = Imager.Resize(image, (int)((int)image.HorizontalResolution * 2.07), (int)((int)image.VerticalResolution * 2.07), true);
                    image.Dispose();
                    Imager.SaveJpeg(pathItem, resized);

                }
                else
                {
                    image.Dispose();
                }


            }



        }
    }
}