using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;
using Previgesst.Services;

namespace Previgesst.Controllers
{
    [Authorize]
    public class DefaultController : ControllerBase
    {
        private UtilisateurService utilisateurService;
        private string Layout;

        public DefaultController(ILogger logger, UtilisateurService utilisateurService) : base(logger)
        {
            this.utilisateurService = utilisateurService;

            if (utilisateurService.GetSession() != null)
            {
                this.Layout = "~/Views/Shared/_LayoutClient.cshtml";

            }
            else
                this.Layout = "~/Views/Shared/_Layout.cshtml";
        }

        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult AccessDenied()
        {
            return View("AccessDenied", this.Layout);
        }
    }
}