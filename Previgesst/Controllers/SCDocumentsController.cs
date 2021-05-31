using Previgesst.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Previgesst.Controllers
{
    public class SCDocumentsController : Controller
    {
        private UtilisateurService utilisateurService;
        public SCDocumentsController(UtilisateurService utilisateurService)
        {
            this.utilisateurService = utilisateurService;
        }
        // GET: SCDocuments
        public ActionResult Index()
        {
            if (utilisateurService.VerifierBonClientDocuments_Client( false))
                return View();
            return Json("");
        }
    }
}