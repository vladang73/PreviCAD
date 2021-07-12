using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;

namespace Previgesst.Controllers
{
    [Authorize]
    public class DefaultController : ControllerBase
    {
        public DefaultController(ILogger logger) : base(logger)
        {
        }

        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}