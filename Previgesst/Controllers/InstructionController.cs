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

    public class InstructionController : Controller
    {
        // GET: Instruction

        private InstructionService instructionService;
        private AccessoireService accessoireService;
        private DispositifService dispositifService;

        public InstructionController(InstructionService instructionService, AccessoireService accessoireService, DispositifService dispositifService)
        {
            this.instructionService = instructionService;
            this.accessoireService = accessoireService;
            this.dispositifService = dispositifService;

        }
        public ActionResult Index()
        {
            PopulateAccessoires();
            PopulateDispositifs();
            return View();
        }



        public ActionResult ReadListInstructions([DataSourceRequest]DataSourceRequest request)
        {

            return Json(instructionService.GetReadListeInstructions(request), JsonRequestBehavior.AllowGet);
        }


        public ActionResult SaveInstruction([DataSourceRequest] DataSourceRequest request,
         InstructionViewModel item)
        {
            if (item != null && ModelState.IsValid)
            {

                instructionService.SaveInstruction(item);
            }
            return Json(new[] { item }.ToDataSourceResult(request, ModelState));

        }

        public ActionResult DeleteInstruction([DataSourceRequest] DataSourceRequest request,
         InstructionViewModel instruction)
        {
            if (instruction != null)
            {
                if (!instructionService.Supprimer(instruction))
                {

                    instruction = null;
                }
            }
            return Json(new[] { instruction }.ToDataSourceResult(request, ModelState));

        }


        private void PopulateAccessoires()
        {
            var DDL = accessoireService.GetAllAsAccessoireDDLViewModel();

            ViewData["Accessoires"] = DDL;

        }

        private void PopulateDispositifs()
        {
            var dispositifDDL = dispositifService.GetAllAsDispositifsDDLViewModel();

            ViewData["Dispositifs"] = dispositifDDL;

        }
    }
}