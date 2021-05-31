using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Previgesst.Controllers
{
    public class TESTController : Controller
    {
        // GET: TEST
        public ActionResult Index()
        {
            ViewBag.item= Helpers.URLHelper.GetBaseUrl();
            return View();
        }
    }
}