using System.Web;
using System.Web.Optimization;
using System;

namespace Patuh.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {



            bundles.Add(new ScriptBundle("~/theme/gentelella/js").Include(
                         "~/Content/vendors/jquery/dist/jquery.js",
                         "~/Content/vendors/bootstrap/dist/js/bootstrap.js",
                         "~/Content/vendors/fastclick/lib/fastclick.js",
                         "~/Content/themes/gentelella/js/custom.js"
                       ));


            bundles.Add(new StyleBundle("~/theme/gentelella/css").Include(
                  "~/Content/vendors/bootstrap/dist/css/bootstrap.css",
                 "~/Content/vendors/font-awesome/css/font-awesome.css",
                 "~/Content/themes/gentelella/css/custom.css"
                ));




        }

    }
}