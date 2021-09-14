using Previgesst.Repositories;
using Previgesst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Previgesst.Services
{
    public class GeneralService
    {
        private LignesRegistreService lignesRegistreService;
        private EmployeRegistreService employeRegistreService;
        private EmployeRegistreRepository employeRegistreRepository;
        private ClientRepository clientRepository;
        public GeneralService(LignesRegistreService lignesRegistreService, EmployeRegistreService employeRegistreService, EmployeRegistreRepository employeRegistreRepository, ClientRepository clientRepository)
        {
            this.lignesRegistreService = lignesRegistreService;
            this.employeRegistreService = employeRegistreService;
            this.employeRegistreRepository = employeRegistreRepository;
            this.clientRepository = clientRepository;
        }

        public const int DEFAULT_ITERATION = 1000;
        public static string Hash(string strToHash, byte[] salt, int iterations)
        {
            var hasher = new Rfc2898DeriveBytes(strToHash, salt, iterations);
            return Encoding.Default.GetString(hasher.GetBytes(50));
        }

        public static byte[] GenerateSalt(int saltLength)
        {
            var random = new RNGCryptoServiceProvider();
            byte[] salt = new byte[saltLength];
            random.GetBytes(salt);
            return salt;
        }

        // Request services for get all email of this compagny whose members want the email notification
        public static bool SendMail(string body, string subject, string courriel, int ClientId, string Logo)
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;


            client.Credentials = new System.Net.NetworkCredential("no-reply@applications.previgesst.com", "59bXXcyUBEVf");
            // La ligne ci-dessous correspond à l'ancienne façon de faire avec Nexucom, qui sera a determiner si elle encore fonctionnelle en local
            //client.Host = "applications.previgesst.com";
            client.Host = "127.0.0.1";

            MailMessage mail = new MailMessage("no-reply@applications.previgesst.com", courriel);

            mail.Subject = subject;
            mail.IsBodyHtml = true;

            // Client logo
            LinkedResource logoClient = new LinkedResource(HttpContext.Current.Server.MapPath("~/Images/Cadenassage/Clients/" + ClientId + "/" + Logo), "image/jpg");
            logoClient.ContentId = Logo;
            logoClient.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
            logoClient.ContentLink = new Uri("cid:" + Logo + "");

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, System.Text.Encoding.UTF8, "text/html");
            htmlView.LinkedResources.Add(logoClient);
            htmlView.TransferEncoding = System.Net.Mime.TransferEncoding.QuotedPrintable;
            mail.AlternateViews.Add(htmlView);


            // Previgesst logo
            LinkedResource logo = new LinkedResource(HttpContext.Current.Server.MapPath("~/Images/dark_logo.png"), "image/png");
            logo.ContentId = "dark_logo.png";
            logo.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
            logo.ContentLink = new Uri("cid:dark_logo.png");

            htmlView.LinkedResources.Add(logo);
            htmlView.TransferEncoding = System.Net.Mime.TransferEncoding.QuotedPrintable;
            mail.AlternateViews.Add(htmlView);



            mail.Body = body;

            client.Send(mail);

            return true;
        }

        public static bool SendMail_v2(string body, string subject, string courriel, int ClientId, string Logo)
        {
            SmtpClient client = new SmtpClient();
            ////client.Port = 587;
            ////client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = false;


            ////client.Credentials = new System.Net.NetworkCredential("no-reply@applications.previgesst.com", "59bXXcyUBEVf");
            ////// La ligne ci-dessous correspond à l'ancienne façon de faire avec Nexucom, qui sera a determiner si elle encore fonctionnelle en local
            //////client.Host = "applications.previgesst.com";
            ////client.Host = "127.0.0.1";

            //MailMessage mail = new MailMessage("no-reply@applications.previgesst.com", courriel);

            MailMessage mail = new MailMessage();

            mail.To.Add(courriel);
            mail.Subject = subject;
            //mail.IsBodyHtml = true;

            /*
            //AlternateView plainView = AlternateView.CreateAlternateViewFromString(body);
            //mail.AlternateViews.Add(plainView);

            
            // Client logo
            LinkedResource logoClient = new LinkedResource(HttpContext.Current.Server.MapPath("~/Images/Cadenassage/Clients/" + ClientId + "/" + Logo), "image/jpg");
            logoClient.ContentId = Logo;
            logoClient.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
            logoClient.ContentLink = new Uri("cid:" + Logo + "");

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, System.Text.Encoding.UTF8, "text/html");
            htmlView.LinkedResources.Add(logoClient);
            htmlView.TransferEncoding = System.Net.Mime.TransferEncoding.QuotedPrintable;
            mail.AlternateViews.Add(htmlView);


            // Previgesst logo
            LinkedResource logo = new LinkedResource(HttpContext.Current.Server.MapPath("~/Images/dark_logo.png"), "image/png");
            logo.ContentId = "dark_logo.png";
            logo.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
            logo.ContentLink = new Uri("cid:dark_logo.png");

            htmlView.LinkedResources.Add(logo);
            htmlView.TransferEncoding = System.Net.Mime.TransferEncoding.QuotedPrintable;
            mail.AlternateViews.Add(htmlView);
            */



            mail.Body = body.Replace("\n", Environment.NewLine);
            mail.IsBodyHtml = false;


            client.Send(mail);

            return true;
        }

        /*internal List<ClientListViewModel> GetCieInformation()
        {
            var session = employeRegistreService.getEmployeRegistre();
            var NoEmploye = session.NoEmploye;

            var compagnieId = employeRegistreRepository.AsQueryable().Where(x => x.NoEmploye == NoEmploye).Select(x => x.ClientId).FirstOrDefault();
            var logoClient = clientRepository.AsQueryable().Where(x => x.ClientId == compagnieId).Select(x => new ClientListViewModel()
            {
                Logo = x.Logo,
                ClientId = x.ClientId
            }).ToList();

            return logoClient;
        }*/

    }
}