using System.Web.Optimization;

namespace Web_MVC
{
    public class BundleConfig
    {
        public MvcApplication MvcApplication
        {
            get => default(MvcApplication);
            set
            {
            }
        }

        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Content/js/jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Content/js/jquery/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Content/js/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                        "~/Content/js/bootstrap-datepicker.min.js",
                        "~/Content/js/bootstrap-datepicker.ru.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Content/js/skel.min.js",
                        "~/Content/js/util.js",
                        "~/Content/js/main.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/js/tinymce").Include("~/Content/js/tinymce/tinymce.min.js"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //                        "~/Scripts/modernizr-*"));
            //
            //                        bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //                                  "~/Scripts/bootstrap.js",
            //                                  "~/Scripts/respond.js")
            //            			);
            
            bundles.Add(new StyleBundle("~/Content/fontAwesome").Include("~/Content/css/font-awesome.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/css/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/datepicker").Include("~/Content/css/bootstrap-datepicker.min.css"));

            bundles.Add(new StyleBundle("~/Content/main").Include("~/Content/css/main.css"));
        }
    }
}
