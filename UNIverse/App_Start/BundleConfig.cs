using System.Web;
using System.Web.Optimization;

namespace UNIverse
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            var bundleJQ = new Bundle("~/bundles/jqueryval");
            bundleJQ
                .Include("~/Scripts/jquery-{version}.js")
                .Include("~/Scripts/jquery.validate*");
            bundles.Add(bundleJQ);

            var bundle = new Bundle("~/bundles/jquery");
            bundle
                .Include("~/Scripts/bootstrap.js")
                .Include("~/Scripts/paging.js")
                // Use the development version of Modernizr to develop with and learn from. Then, when you're
                // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
                .Include("~/Scripts/modernizr-*")
                .Include("~/Scripts/respond.js");
            bundles.Add(bundle);


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/Login.css",
                      "~/Content/Profile.css"));
        }
    }
}
