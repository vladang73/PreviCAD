using Previgesst.ActionFilters;
using Previgesst.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Previgesst.Controllers
{
    [AuthorizeRoles(Enums.Roles.Administrateur, Enums.Roles.LectureEcriture, Enums.Roles.LectureEcriture)]

    public class FicheCadenassageEditController : Controller
    { private SectionService sectionService;
        ApplicationPrevisService applicationPreviService;
        // GET: FicheCadenassageEdit


        public FicheCadenassageEditController(SectionService sectionService, ApplicationPrevisService applicationPreviService)
        {
            this.sectionService = sectionService;
            this.applicationPreviService = applicationPreviService;
        }
        public ActionResult Index()
        {
            PopulateSections();
            return View();
        }


        private void PopulateSections()
        {
            var sectionDDL = sectionService.GetAllAsSectionsDDLViewModel(applicationPreviService.getIdByName(Enums.Applications.Cadenassage));

            ViewData["Sections"] = sectionDDL;
            ViewData["DefaultSection"] = sectionDDL.FirstOrDefault();
        }
    }
}