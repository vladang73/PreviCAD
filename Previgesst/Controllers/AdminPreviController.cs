using Previgesst.ActionFilters;
using Previgesst.Enums;
using Previgesst.Services;
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
        LogEntryService logService;

        public AdminPreviController(LogEntryService logService) //: base(logger)
        {
            this.logService = logService;
        }



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

        #region ----- NLog Errors -----

        [AuthorizeRoles(Enums.Roles.Administrateur)]
        public ActionResult Errors()
        {
            return View();
        }

        public ActionResult ReadListLogs([Kendo.Mvc.UI.DataSourceRequest] Kendo.Mvc.UI.DataSourceRequest request)
        {
            return Json(logService.GetReadListeLogs(request));
        }

        #endregion
    }
}