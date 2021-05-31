using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;

namespace Previgesst.Controllers
{
    public class ErrorController : ControllerBase
    {
        public ErrorController(ILogger logger) : base(logger)
        {
        }

        // GET: Error
        public ActionResult Index()
        {
            return View();
        }
    }
}