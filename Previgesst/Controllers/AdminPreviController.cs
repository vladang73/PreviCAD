using Previgesst.ActionFilters;
using Previgesst.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Previgesst.Controllers
{
    [AuthorizeRoles(Enums.Roles.Administrateur, Enums.Roles.LectureEcriture, Enums.Roles.LectureSeule)]
    public class AdminPreviController : Controller
    {
        // GET: AdminPrevi

        public ActionResult Index()
        {
            ViewData["Culture"] = System.Threading.Thread.CurrentThread.CurrentCulture;
            ViewData["UICulture"] = System.Threading.Thread.CurrentThread.CurrentUICulture;

            if (HttpContext.Session["IsCorporate"] == null)
            {
                Response.Redirect("/Account/LogOff");
            }

            return View();
        }
    }
}