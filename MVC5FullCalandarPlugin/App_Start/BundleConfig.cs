using System.Web;
using System.Web.Optimization;

namespace MVC5FullCalandarPlugin
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            // Custom Calendar.
            bundles.Add(new ScriptBundle("~/bundles/Script-calendar").Include(
                                 "~/Scripts/script-custom-calendar.js"));

            var authenticationScriptBundle =
                new ScriptBundle("~/bundles/ScriptAuthentication").IncludeDirectory("~/Scripts/ScriptsAuthenication/",
                    "*.js", true);
            authenticationScriptBundle.Include("~/Scripts/ScriptsAuthenication/ShowOrHideButtons.js");

            bundles.Add(authenticationScriptBundle);

            var scriptsEventBundle = new ScriptBundle("~/bundles/ScriptEvents").IncludeDirectory(
                "~/Scripts/ScriptsEvents", "*.js", true);
            scriptsEventBundle.IncludeDirectory("~/Scripts/ScriptsEvents/ButtonEvents", "*.js", true);
            bundles.Add(scriptsEventBundle);

            bundles.Add(new ScriptBundle("~/Content/Global").Include(
                "~/Content/Global.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/AuthenticationStyle.css"));



        }
    }
}
