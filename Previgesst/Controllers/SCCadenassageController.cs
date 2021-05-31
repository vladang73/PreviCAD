using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Previgesst.Controllers
{
    public class SCCadenassageController : Controller
    {
        // GET: SCCadenassage
        public ActionResult Index()
        {
            return RedirectToAction("Index", "FicheCadenassage");
            
        }

        
    }
}