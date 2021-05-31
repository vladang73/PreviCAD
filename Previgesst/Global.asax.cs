using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NLog;
using Previgesst.Localisation;
using Previgesst.Validation;

namespace Previgesst
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);




            using (var dbContext = DataContexts.AppDbContext.Create())
            {
                LogConfig.RegisterLog(dbContext.Database.Connection.ConnectionString);
            }

            ControllerBuilder.Current.SetControllerFactory(new DefaultControllerFactory(new LocalizedControllerActivator()));

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //Log de l'exception...
            var exception = Server.GetLastError();
            logger.Error(exception, "Application_Error: {0}", exception.Message);
        }

        protected void Session_End(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Session_End");
           
           
        }
    }
}
