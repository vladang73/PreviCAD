using Previgesst.Models;
using Previgesst.Repositories;
using Previgesst.Services;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Previgesst.Controllers
{
    public class FileController : Controller

    {
        // GET: File
        private DocumentRepository documentRepository;
        private UtilisateurService utilisateurService;
        private DocumentClientRepository documentClientRepository;
        private EmployeRegistreService employeRegistreService;
        private DocumentFicheRepository documentFicheRepository;

        public FileController(DocumentRepository documentRepository, UtilisateurService utilisateurService, DocumentFicheRepository documentFicheRepository,
            DocumentClientRepository documentClientRepository, EmployeRegistreService employeRegistreService)
        {
            this.utilisateurService = utilisateurService;
            this.documentRepository = documentRepository;
            this.documentClientRepository = documentClientRepository;
            this.employeRegistreService = employeRegistreService;
            this.documentFicheRepository = documentFicheRepository;
        }


        public string getDocType(string fileName)
        {
            if (fileName.ToLower().EndsWith(".pdf"))
                return "application/pdf";
            if (fileName.ToLower().EndsWith(".xlsx") || fileName.ToLower().EndsWith(".xls"))
                return "application/vnd.ms-excel";
            if (fileName.ToLower().EndsWith(".docx") || fileName.ToLower().EndsWith(".doc"))
                return "application/vnd.ms-word";
            return "";



        }
        public ActionResult DownloadFile(int ID)
        {

            var ClientFromSession = utilisateurService.GetSession();
            if (!Request.IsAuthenticated && ClientFromSession == null)
            {
                //utilisateurService.LogOff();
                //this.Response.Redirect("~/ClientLogin/Index");

                this.Response.Redirect("/Default/AccessDenied");
            }



            var file = documentRepository.Get(ID);


            var strFilePath = Server.MapPath("~/docFiles/" + ID + "/" + file.FileName);
            var docType = getDocType(strFilePath);
            byte[] filePath = System.IO.File.ReadAllBytes(strFilePath);
            return File(filePath, docType, Path.GetFileName(strFilePath));
        }

        public ActionResult DownloadFileClient(int ID)
        {
            var ClientFromSession = utilisateurService.GetSession();

            var empRegistre = employeRegistreService.getEmployeRegistre();



            if (!Request.IsAuthenticated && ClientFromSession == null && empRegistre.ClientId==0)
            {
                //utilisateurService.LogOff();
                //this.Response.Redirect("~/ClientLogin/Index");

                this.Response.Redirect("/Default/AccessDenied");
            }



            var file = documentClientRepository.Get(ID);

            if (empRegistre.ClientId != 0)
            { if ( file.ClientId != empRegistre.ClientId)
                {
                    //employeRegistreService.LogOff();
                    //this.Response.Redirect("~/ClientLogin/Index");

                    this.Response.Redirect("/Default/AccessDenied");
                }
            }

            if (ClientFromSession!=null)
            {
                if (file.ClientId != ClientFromSession.ClientId)
                {
                    //employeRegistreService.LogOff();
                    //this.Response.Redirect("~/ClientLogin/Index");

                    this.Response.Redirect("/Default/AccessDenied");
                }
            }

            var strFilePath = Server.MapPath("~/ClientDocFiles/" + ID + "/" + file.FileName);
            var docType = getDocType(strFilePath);
            byte[] filePath = System.IO.File.ReadAllBytes(strFilePath);
            return File(filePath, docType, Path.GetFileName(strFilePath));
        }

        public ActionResult CreateARDoc()
        {
            var v = new AjoutFichierViewModel();
            v.TypeFichier = "Analyse de risques";
            return View(v);
        }

        public ActionResult DownloadFileFiche(int ID)
        {
            var ClientFromSession = utilisateurService.GetSession();

            var empRegistre = employeRegistreService.getEmployeRegistre();



            if (!Request.IsAuthenticated && ClientFromSession == null && empRegistre.ClientId == 0)
            {
                //utilisateurService.LogOff();
                //this.Response.Redirect("~/ClientLogin/Index");

                this.Response.Redirect("/Default/AccessDenied");
            }



            var file = documentFicheRepository.Get(ID);

            /*
            TODO: Not sure about permissions here


            if (empRegistre.ClientId != 0)
            {
                if (file.ClientId != empRegistre.ClientId)
                {
                    employeRegistreService.LogOff();
                    this.Response.Redirect("~/ClientLogin/Index");
                }
            }

            if (ClientFromSession != null)
            {
                if (file.ClientId != ClientFromSession.ClientId)
                {
                    employeRegistreService.LogOff();
                    this.Response.Redirect("~/ClientLogin/Index");
                }
            }

            */

            var strFilePath = Server.MapPath("~/FicheDocFiles/" + ID + "/" + file.FileName);
            var docType = getDocType(strFilePath);
            byte[] filePath = System.IO.File.ReadAllBytes(strFilePath);
            return File(filePath, docType, Path.GetFileName(strFilePath));
        }

    }
}
