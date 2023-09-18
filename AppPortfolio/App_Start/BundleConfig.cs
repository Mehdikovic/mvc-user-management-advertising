using System.Web.Optimization;

namespace AppPortfolio {
    public class BundleConfig {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles) {

            bundles.Add(new ScriptBundle("~/bundles/mainJS").Include(
                "~/wwwroot/js/jquery-2.1.4.min.js",
                "~/wwwroot/bootstrap/js/bootstrap.min.js",
                 "~/wwwroot/js/jquery.scripts.min.js",
                 "~/wwwroot/rs-plugin/js/jquery.themepunch.tools.min.js",
                 "~/wwwroot/rs-plugin/js/jquery.themepunch.revolution.min.js",
                 "~/wwwroot/js/jquery.magnific-popup.min.js",
                 "~/wwwroot/owl-carousel/owl.carousel.min.js",
                 "~/wwwroot/js/include.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/wwwroot/js/jquery-2.1.4.min.js",
                "~/wwwroot/js/clipboard.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/wwwroot/Manage/js/jquery.validate.min.js",
                        "~/wwwroot/Manage/js/jquery.validate.unobtrusive.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/wwwroot/Manage/js/modernizr-2.6.2.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/wwwroot/Manage/js/bootstrap.min.js",
                      "~/wwwroot/Manage/js/respond.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/wwwroot/Manage/css/bootstrap.css",
                      "~/wwwroot/Manage/css/site.css",
                      "~/wwwroot/Manage/css/PagedList.css"));

            bundles.Add(new StyleBundle("~/Styles/css").Include(
                        "~/wwwroot/bootstrap/css/bootstrap.min.css",
                        "~/wwwroot/bootstrap/css/bootstrap-theme.min.css",
                        "~/wwwroot/css/color-default.css",
                        "~/wwwroot/css/responsive.css",
                        "~/wwwroot/css/animate.css",
                        "~/wwwroot/owl-carousel/owl.carousel.css",
                        "~/wwwroot/owl-carousel/owl.theme.css",
                        "~/wwwroot/rs-plugin/css/settings.css",
                        "~/wwwroot/rs-plugin/css/builder.css",
                        "~/wwwroot/css/magnific-popup.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
