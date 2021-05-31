using Kendo.Mvc.UI;
using Previgesst.ActionFilters;
using Previgesst.Repositories;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Previgesst.Controllers
{
    [AuthorizeRoles(Enums.Roles.Administrateur, Enums.Roles.LectureEcriture, Enums.Roles.LectureEcriture)]
    public class CadenassageController : Controller
    {
        ClientRepository clientRepository;
        public CadenassageController(ClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;

        }
        // GET: Cadenassage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit( int Id)
        {
            var vm = new EditCadenassageViewModel();
            var client = clientRepository.Get(Id);

            vm.ClientId = Id;
            vm.Nom = client.Nom;
            vm.Logo = client.Thumb;


            return View(vm);
        }

        public ActionResult Fiches()
        {
            return View();
        }




    }
}