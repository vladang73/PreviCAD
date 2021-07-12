using Kendo.Mvc.UI;
using Previgesst.ActionFilters;
using Previgesst.Repositories;
using Previgesst.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Previgesst.Controllers
{
    public class MesApplicationsController : Controller
    {
        string initialRequest = "";

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            initialRequest = Request.Url.AbsolutePath;

            var currentUser = utilisateurService.GetSession();

            ViewBag.UserName = currentUser?.Nom;


        }




        private CadenasService cadenasService;
        private UtilisateurService utilisateurService;
        private AnalyseRisqueService analyseRisqueService;
        private AnalyseRisqueRepository analyseRisqueRepository;
        private DocumentService documentService;

        private ClientService clientService;

        public MesApplicationsController(CadenasService cadenasService, UtilisateurService utilisateurService,
            AnalyseRisqueService analyseRisqueService, AnalyseRisqueRepository analyseRisqueRepository,
            DocumentService documentService, ClientService clientService)
        {
            this.cadenasService = cadenasService;
            this.utilisateurService = utilisateurService;
            this.analyseRisqueService = analyseRisqueService;
            this.analyseRisqueRepository = analyseRisqueRepository;
            this.documentService = documentService;
            this.clientService = clientService;
        }
        // GET: MesApplications
        [CheckSession]
        public ActionResult Index()
        {

            var ClientIdFromSession = utilisateurService.GetSession().ClientId;

            ViewBag.idClient = ClientIdFromSession;
            var session = utilisateurService.GetSession();
            var vm = utilisateurService.getAccesClientVM(session.ClientId);
            vm.AccessUtilisateurs = session.AdmUtilisateurs;
            return View(vm);
        }


        public ActionResult Login()
        {
            return View();
        }

        [CheckSession]
        public ActionResult Analyses()
        {
            return View("~/MesApplications/Analyses");
        }


        [CheckSession]
        public ActionResult Documents()
        {
            return View("~/MesApplications/Documents");
        }

        [CheckSession]
        public void getXLAnalyseClient(int id)
        {
            var Analyse = analyseRisqueRepository.Get(id);
            if (Analyse == null)
            {
                //utilisateurService.LogOff();
                //this.Response.Redirect("~/ClientLogin/Index");

                this.Response.Redirect("/Default/AccessDenied");
            }
            var ClientIdFromSession = utilisateurService.GetSession().ClientId;
            if (Analyse.ClientId != ClientIdFromSession)
            {
                //utilisateurService.LogOff();
                //this.Response.Redirect("~/ClientLogin/Index");
            
                this.Response.Redirect("/Default/AccessDenied");
            }
            else
            {
                analyseRisqueService.ReturnFile(id);
            }

        }

        [Authorize]
        public ActionResult ReadListDocumentsGeneraux([DataSourceRequest]DataSourceRequest request)
        {
            if (utilisateurService.VerifierBonClientDocuments_Client( false))

                return Json(documentService.GetReadListDocument(request, "Générale"), JsonRequestBehavior.AllowGet);
            return Json("");
        }


        [Authorize]
        public ActionResult GetClientAnalyse()
        {


            var ClientIdFromSession = utilisateurService.GetSession().ClientId;

            return RedirectToAction("EditClient", "AnalyseRisque", new { ID = ClientIdFromSession });

        }

        [Authorize]
        public ActionResult GetClientCadenassage()
        {
            var session = utilisateurService.GetSession();
            if (session != null)
            {
                var ClientIdFromSession = utilisateurService.GetSession().ClientId;
                if (utilisateurService.VerifierBonClientCadenassage_Client(ClientIdFromSession, false))
                    return RedirectToAction("Edit", "FicheCadenassage", new { Id = ClientIdFromSession });
            }

            return Json("");
        }

        [Authorize]
        public ActionResult ReadListAnalyseRisqueUnClient([DataSourceRequest]DataSourceRequest request)
        {
            var userInfo = utilisateurService.GetSession();
            return Json(analyseRisqueService.GetReadListAnalyseRisque(request, userInfo.ClientId));
        }

        public ActionResult Cadenassage()
        {
            return View("~/MesApplications/Cadenassage/Index.cshtml");
        }
        [Authorize]
        public ActionResult ReadListAnalyseRisqueDocument([DataSourceRequest]DataSourceRequest request)
        {
            return Json(documentService.GetReadListDocument(request, "Analyse de risques"));
        }


        //   public ActionResult ReadListAnalyseRisque([DataSourceRequest]DataSourceRequest request)
        // {
        //  return Json(cadenasService.GetReadCadenas(request,1));
        // }
    }
}