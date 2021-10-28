using System.Web;
using System.Web.Optimization;

namespace Previgesst
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-migrate-{version}.js",
                        "~/Scripts/jquery.cookie.js"));

            bundles.Add(new ScriptBundle("~/bundles/cldr").Include(
                        "~/Scripts/cldr.js",
                        "~/Scripts/cldr/event.js",
                        "~/Scripts/cldr/supplemental.js"));

            bundles.Add(new ScriptBundle("~/bundles/globalize").Include(
                        "~/Scripts/globalize.js",
                        "~/Scripts/globalize/number.js",
                        "~/Scripts/globalize/date.js",
                        "~/Scripts/globalize/message.js",
                        "~/Scripts/globalize/plural.js",
                        "~/Scripts/globalize/relative-time.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js",
                        "~/Scripts/jquery.validate.bootstrap.js",
                        "~/Scripts/jquery.validate.globalize.js",
                        "~/Scripts/Cultures/jquery.validate.messages.fr.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryajax").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.*"));

            bundles.Add(new ScriptBundle("~/bundles/underscore").Include(
                        "~/Scripts/underscore.min.js"));

            /*// Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));*/

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                        /*"~/Scripts/kendo/2016.2.714/jquery.min.js",*/     //jquery déjà inclus dans un autre bundle
                        "~/Scripts/kendo/2016.2.714/jszip.min.js",
                        "~/Scripts/kendo/2016.2.714/kendo.all.min.js",
                        "~/Scripts/kendo/Cultures/kendo.culture.fr-CA.min.js",
                        "~/Scripts/kendo/Cultures/kendo.culture.en-CA.min.js",
                        "~/Scripts/kendo/2016.2.714/kendo.aspnetmvc.min.js",
                        "~/Scripts/kendo.modernizr.custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                      "~/Scripts/site.js"));

            var themeBootstrap = "bootstrap";      // Nom du thème kendo (telerik) utilisé
            //Pour utiliser un autre thème boostrap

            //var mobileSkinKendo = "flat";
            bundles.Add(new StyleBundle("~/content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.before.css"));

            //INFORMATION : Lors du déploiement créer un répertoire virtuel de /Content/themeUtilise vers /Content/kendo/2016.2.714/themeUtilise
            //              Exemple : /Content/Kendo/Bootstrap  -->  /Content/kendo/2016.2.714/Bootstrap
            bundles.Add(new StyleBundle("~/Content/csskendo").Include(
                      //"~/Content/kendo/2016.2.714/kendo.common.min.css",
                      "~/Content/kendo/2016.2.714/kendo.common-" + themeBootstrap + ".min.css",
                      "~/Content/kendo/2016.2.714/kendo." + themeBootstrap + ".min.css",
                      //"~/Content/kendo/2016.2.714/kendo.mobile.all.min.css",
                      //"~/Content/kendo/2016.2.714/kendo." + themeBootstrap + ".mobile.min.css",
                      //"~/Content/kendo/2016.2.714/kendo.mobile." + mobileSkinKendo + ".min.css",
                      "~/Content/kendo/2016.2.714/kendo.dataviz.min.css",
                      "~/Content/kendo/2016.2.714/kendo.dataviz." + themeBootstrap + ".min.css"));   // Toujours garder site.css en dernier

            bundles.Add(new StyleBundle("~/content/cssAfter").Include(
                      "~/Content/site.after.css"));   // Toujours garder site.css en dernier




            bundles.Add(new StyleBundle("~/Content/mobile/css").Include(
                      "~/Content/mobile/remixicon.css",
                      "~/Content/mobile/bootstrap.min.css",
                      "~/Content/mobile/boostrap_override.css",
                      "~/Content/mobile/style.css"));


            //BundleTable.EnableOptimizations = true;
            
        }
    }
}
