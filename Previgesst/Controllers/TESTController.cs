using Previgesst.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;

namespace Previgesst.Controllers
{
    public class TESTController : Controller
    {
        private ClientService clientService;
        public TESTController(ClientService clientService)
        {
            this.clientService = clientService;
        }

        // GET: TEST
        public ActionResult Index()
        {
            ViewBag.item = Helpers.URLHelper.GetBaseUrl();
            return View();
        }

        [HttpGet]
        public ActionResult Email()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Email(string ToEmail, string EmailBody)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            int client = 30;
            var logoClient = clientService.getClientVM(client);

            GeneralService.SendMail_v2(
                            "<center><table style='width:500px;padding:30px;border:1px solid #efeeef;font-family:\"IBM Plex Sans\" !important;'>" +
                                "<tr>" +
                                    "<td><center><img style=\"max-height:80px;\" src=\"cid:" + logoClient.Logo + "\"></img></center></td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td><center><h1 style=\"margin-bottom:-30px\">Cadenassage en cours</h1></center></td>" +
                                "</tr><br/>" +
                                "<tr>" +
                                    "<td>" +
                                        "<p>Bonjour " + "Atta H." + ",</p>" +
                                        "<p>" +
                                            "<strong>Une interventions nécessitant le contrôle des énergies dangereuses est en cours.</strong><br><br>" +
                                            "Détails de l’intervention<br>" +
                                            "Nom employé : " + "model.Nom" + "<br>" +
                                            "Identifiant de l’employé : " + "model.NoEmploye" + "<br>" +
                                            "Département : " + "model.NomDepartement" + "<br>" +
                                            "Équipement : " + "model.NomEquipement" + "<br>" +
                                            "Numéro de procédure de cadenassage : " + "model.NoFicheCadenassage" + "<br>" +
                                        "</p>" +
                                        "<center><table style=\"width:300px;background-color:#337ab7\">" +
                                            "<tr>" +
                                                "<td><center><a style=\"text-decoration:none;color:white;font-weight:bold\" href=\"https://applications.previcad.com\">Accès au compte</a></center></td>" +
                                            "</tr>" +
                                        "</table></center>" +
                                    "</td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td><p><em>IMPORTANT : Ce courriel a été envoyé automatiquement, S.V.P. ne pas « Répondre » sur ce message.</em></p></td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td><img style=\"margin-left:280px;max-height:30px;\" src=\"cid:dark_logo.png\"></img></td>" +
                                "</tr>" +
                            "</table></center>"
                            ,
                            "Cadenassage en cours :  " + "NomEquipement", ToEmail, client, logoClient.Logo, true
                        ); // Sujet du courriel + courriel membre + cliendId + logo

            return View();
        }
    }
}