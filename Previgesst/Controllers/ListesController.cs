using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Previgesst.ActionFilters;
using Previgesst.Services;
using Previgesst.ViewModels;
using System.Web.Mvc;



using System.Collections.Generic;

namespace Previgesst.Controllers
{

    [AuthorizeRoles(Enums.Roles.Administrateur, Enums.Roles.LectureEcriture, Enums.Roles.LectureEcriture)]
    public class ListesController : Controller
    {
        // GET: Listes
        private ListesService listeService;

        public ListesController(ListesService listeService)
        {
            this.listeService = listeService;

        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PhenomeneListRead([DataSourceRequest] DataSourceRequest request)
        {
            return Json(listeService.GetFilteredPhenomene(request));
        }

        public ActionResult ReglementListRead([DataSourceRequest] DataSourceRequest request)
        {
            return Json(listeService.GetFilteredReglement(request));
        }

        public ActionResult DommageListRead([DataSourceRequest] DataSourceRequest request)
        {
            return Json(listeService.GetFilteredDommage(request));
        }

        public ActionResult SituationListRead([DataSourceRequest] DataSourceRequest request)
        {
            return Json(listeService.GetFilteredSituations(request));
        }

        public ActionResult EvenementListRead([DataSourceRequest] DataSourceRequest request)
        {
            return Json(listeService.GetFilteredEvenements(request));
        }


        public ActionResult TypeReductionListRead([DataSourceRequest] DataSourceRequest request)
        {
            return Json(listeService.GetFilteredTypesReduction(request));
        }



        public ActionResult DispositifListRead([DataSourceRequest] DataSourceRequest request)
        {
            return Json(listeService.GetFilteredDispositif(request));
        }

        public ActionResult AccessoireListRead([DataSourceRequest] DataSourceRequest request)
        {
            return Json(listeService.GetFilteredAccessoire(request));
        }

        public ActionResult SourceEnergieListRead([DataSourceRequest] DataSourceRequest request)
        {
            return Json(listeService.GetFilteredSourceEnergie(request));
        }

        public ActionResult MaterielListRead([DataSourceRequest] DataSourceRequest request)
        {
            return Json(listeService.GetFilteredMateriel(request));
        }


        [HttpGet]
        public JsonResult GetListReadForMultiselect(Enums.TypeListe iditem)
        {
            List<SimpleListViewModel> result = null;

            switch (iditem)
            {
                case Enums.TypeListe.accessoire:
                    result = listeService.GetAccessoire();
                    break;
                case Enums.TypeListe.materiel:
                    result = listeService.GetMateriel();
                    break;
                case Enums.TypeListe.sourceEnergie:
                    result = listeService.GetSourceEnergie();
                    break;
                case Enums.TypeListe.dispositif:
                    result = listeService.GetDispositif();
                    break;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetListRead([DataSourceRequest] DataSourceRequest request, Enums.TypeListe iditem)
        {
            if (request.Filters == null)
            {
                request.Filters = new System.Collections.Generic.List<Kendo.Mvc.IFilterDescriptor>();
                request.Sorts = new System.Collections.Generic.List<Kendo.Mvc.SortDescriptor>();
            }

            if (iditem == Enums.TypeListe.phenomene)
                return (PhenomeneListRead(request));
            else if (iditem == Enums.TypeListe.situation)
                return (SituationListRead(request));
            else if (iditem == Enums.TypeListe.evenement)
                return (EvenementListRead(request));
            else if (iditem == Enums.TypeListe.typereduction)
                return (TypeReductionListRead(request));

            else if (iditem == Enums.TypeListe.dispositif)
                return (DispositifListRead(request));
            else if (iditem == Enums.TypeListe.accessoire)
                return (AccessoireListRead(request));
            else if (iditem == Enums.TypeListe.sourceEnergie)
                return (SourceEnergieListRead(request));
            else if (iditem == Enums.TypeListe.materiel)
                return (MaterielListRead(request));
            else if (iditem == Enums.TypeListe.reglement)
                return (ReglementListRead(request));
            else if (iditem == Enums.TypeListe.dommage)
                return (DommageListRead(request));
            else
                return Index();

        }

        [HttpPost]

        public ActionResult Save([DataSourceRequest] DataSourceRequest request,
           SimpleListViewModel item, Enums.TypeListe iditem)
        {
            if (item != null && ModelState.IsValid)
            {
                listeService.Enregistrer(item, iditem);
            }
            return Json(new[] { item }.ToDataSourceResult(request, ModelState));

        }
        [HttpPost]

        public ActionResult Delete([DataSourceRequest] DataSourceRequest request,
            SimpleListViewModel item, Enums.TypeListe iditem)
        {
            if (item != null)
            {
                if (!listeService.Supprimer(item, iditem))
                {
                    ModelState.AddModelError("", "Impossible de supprimer l'élément car il est utilisé");
                    item = null;
                }
            }
            return Json(new[] { item }.ToDataSourceResult(request, ModelState));
        }


        public ActionResult Phenomenes()
        {


            var vm = new ViewSimpleListViewModel() { NomDescriptionEn = "DescriptionEN", NomDescription = "Description", NomId = "Id", NomListe = "Phénomènes dangereux", Type = Enums.TypeListe.phenomene };
            return View("Index", vm);

        }

        public ActionResult Dommages()
        {


            var vm = new ViewSimpleListViewModel() { NomDescriptionEn = "DescriptionEN", NomDescription = "Description", NomId = "Id", NomListe = "Dommages potentiels", Type = Enums.TypeListe.dommage };
            return View("Index", vm);

        }

        public ActionResult Situations()
        {

            var vm = new ViewSimpleListViewModel() { NomDescriptionEn = "DescriptionEN", NomDescription = "Description", NomId = "Id", NomListe = "Situations dangereuses", Type = Enums.TypeListe.situation };
            return View("Index", vm);

        }

        public ActionResult Evenements()
        {

            var vm = new ViewSimpleListViewModel() { NomDescriptionEn = "DescriptionEN", NomDescription = "Description", NomId = "Id", NomListe = "Événements dangereux", Type = Enums.TypeListe.evenement };

            return View("Index", vm);

        }

        public ActionResult TypesReductions()
        {

            var vm = new ViewSimpleListViewModel() { NomDescription = "Description", NomId = "Id", NomListe = "Analyse de risques - Types de réductions", Type = Enums.TypeListe.typereduction };
            return View("Index", vm);

        }

        public ActionResult Mesures()
        {

            var vm = new ViewSimpleListViewModel() { NomDescription = "Description", NomId = "Id", NomListe = "Mesures de prévention", Type = Enums.TypeListe.mesure };
            return View("Index", vm);
        }


        public ActionResult Dispositifs()
        {
            var vm = new ViewSimpleListViewModel() { NomDescriptionEn = "DescriptionEN", NomDescription = "Description", NomId = "Id", NomListe = "Cadenassage - Dispositifs", Type = Enums.TypeListe.dispositif };
            return View("Index", vm);
        }


        public ActionResult Accessoires()
        {
            var vm = new ViewSimpleListViewModel() { NomDescriptionEn = "DescriptionEN", NomDescription = "Description", NomId = "Id", NomListe = "Cadenassage - Accessoires", Type = Enums.TypeListe.accessoire };
            return View("Index", vm);
        }

        public ActionResult SourcesEnergie()
        {
            var vm = new ViewSimpleListViewModel() { NomDescriptionEn = "DescriptionEN", NomDescription = "Description", NomId = "Id", NomListe = "Cadenassage - Sources d'énergie", Type = Enums.TypeListe.sourceEnergie };
            return View("Index", vm);
        }


        public ActionResult Materiel()
        {
            var vm = new ViewSimpleListViewModel() { NomDescriptionEn = "DescriptionEN", NomDescription = "Description", NomId = "Id", NomListe = "Cadenassage - Matériel", Type = Enums.TypeListe.materiel };
            return View("Index", vm);
        }

        public ActionResult Reglements()
        {
            var vm = new ViewSimpleListViewModel() { NomDescription = "Description", NomId = "Id", NomListe = "Réglements / Normes", Type = Enums.TypeListe.reglement };
            return View("Index", vm);
        }

    }
}