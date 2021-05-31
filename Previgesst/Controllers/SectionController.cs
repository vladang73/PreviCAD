using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Previgesst.ActionFilters;
using Previgesst.Services;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Previgesst.Controllers
{

        [AuthorizeRoles(Enums.Roles.Administrateur, Enums.Roles.LectureEcriture, Enums.Roles.LectureEcriture)]

    public class SectionController : Controller
    {

        SectionService sectionService;
        ApplicationPrevisService applicationPrevisService;

        public SectionController(SectionService sectionService, ApplicationPrevisService applicationPrevisService)
        {
            this.sectionService = sectionService;
            this.applicationPrevisService = applicationPrevisService;

        }
        // GET: Section
        public ActionResult Index()
        {
            PopulateApplications();
            return View();
        }


        public ActionResult ReadListSections([DataSourceRequest]DataSourceRequest request)
        {

            return Json(sectionService.GetReadListeSections(request), JsonRequestBehavior.AllowGet);
        }


        public ActionResult SaveSection([DataSourceRequest] DataSourceRequest request,
         SectionViewModel item)
        {
            if (item != null && ModelState.IsValid)
            {

                sectionService.SaveSection(item);
            }
            return Json(new[] { item }.ToDataSourceResult(request, ModelState));

        }

        public ActionResult DeleteSection([DataSourceRequest] DataSourceRequest request,
         SectionViewModel section)
        {
            if (section != null)
            {
                if (!sectionService.Supprimer(section))
                {

                    section = null;
                }
            }
            return Json(new[] { section }.ToDataSourceResult(request, ModelState));

        }



        private void PopulateApplications()
        {
            var applicationDDL = applicationPrevisService.GetAppGenerealeViewModel();

            ViewData["Applications"] = applicationDDL;

        }



    }
}