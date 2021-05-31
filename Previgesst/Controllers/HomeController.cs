using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Previgesst.ActionFilters;
using Previgesst.Repositories;
using NLog;

namespace Previgesst.Controllers
{
    public class HomeController : ControllerBase
    {
        private LogEntryRepository logRepo;
        public HomeController(ILogger logger, LogEntryRepository logRepo) : base(logger)
        {
            this.logRepo = logRepo;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            
            return View();
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
