// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BundleConfig.cs" company="">
//   Copyright ?2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace App.Web.Score
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
                    "~/content/datepicker/css/datepicker3.css",
                    "~/content/app.css"
                ));

            bundles.Add(new ScriptBundle("~/js/vendor").Include(
                    "~/scripts/vendor/jquery-1.11.1.min.js",
                    "~/content/bootstrap/js/bootstrap.min.js",
                    "~/content/bootstrap-dialog/js/bootstrap.dialog.min.js",
                    "~/content/datepicker/js/bootstrap-datepicker.js",
                    "~/scripts/vendor/angular.min.js",
                    "~/scripts/vendor/angular-ui-router.min.js",
                    "~/scripts/vendor/angular-cookies.min.js"
                ));

            bundles.Add(new ScriptBundle("~/js/app").Include(
                    "~/scripts/filters/filters.js",
                    "~/scripts/services/services.js",
                    "~/scripts/directives/directives.js",
                    "~/scripts/controllers/controllers.js",
                    //"~/scripts/controllers/base.js",
                    //"~/scripts/controllers/exam.js",
                    //"~/scripts/controllers/score.js",
                    "~/scripts/controllers/query.js",
                    "~/scripts/controllers/statistic.js",
                    "~/scripts/controllers/admin.js",
                    "~/scripts/utils.js",
                    "~/scripts/app.js"
                ));
        }
    }
}
