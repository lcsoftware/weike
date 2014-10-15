namespace App.Score.Web
{
    using System.Web;
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/content/css/app").Include(
                    "~/content/bootstrap/css/bootstrap.min.css",
                    "~/content/bootstrap/css/bootstrap-theme.min.css",
                    "~/content/bootstrap-dialog/css/bootstrap-dialog.min.css",
                    "~/content/datepicker/css/datepicker.css",
                    "~/content/app.css",
                    "~/content/menu.css",
                    "~/content/font-awesome.css"
                ));

            //bundles.Add(new ScriptBundle("~/js/vendor").Include(
            //        "~/scripts/vendor/jquery-1.11.1.min.js",
            //        "~/content/bootstrap/js/bootstrap.min.js",
            //        "~/content/bootstrap-dialog/js/bootstrap.dialog.min.js",
            //        "~/content/datepicker/js/bootstrap-datepicker.js",
            //        "~/scripts/vendor/angular.min.js",
            //        "~/scripts/vendor/angular-ui-router.min.js",
            //        "~/scripts/vendor/angular-cookies.min.js"
            //    ));

            //bundles.Add(new ScriptBundle("~/js/app").Include(
            //        "~/scripts/filters.js",
            //        "~/scripts/services.js",
            //        "~/scripts/directives.js",
            //        "~/scripts/controllers.js",
            //        "~/scripts/utils.js",
            //        "~/scripts/app.admin.js",
            //        "~/scripts/app.base.js",
            //        "~/scripts/app.exam.js",
            //        "~/scripts/app.query.js",
            //        "~/scripts/app.score.js",
            //        "~/scripts/app.statistic.js",
            //        "~/scripts/app.js"
            //    ));
        }
    }
}
