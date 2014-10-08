// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BundleConfig.cs" company="">
//   Copyright ?2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace App.Score
{
    using System.Web;
    using System.Web.Optimization;

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/content/css/app").Include(
                "~/content/css/bootstrap-theme.min.css",
                "~/content/css/bootstrap.min.css",
                "~/content/css/datepicker/css/datepicker3.css",
                "~/content/css/angular-ui-tree.min.css",
                "~/content/app.css",
                "~/content/menu.css"
            ));

            bundles.Add(new ScriptBundle("~/js/vendor").Include(
                "~/scripts/vendor/jquery-2.0.3.min.js",
                "~/content/js/bootstrap.min.js",
                "~/content/js/bootstrap-datepicker.js",
                "~/content/js/locales/bootstrap-datepicker.zh-CN.js",
                "~/scripts/vendor/angular.min.js",
                "~/scripts/vendor/angular-animate.js",
                "~/scripts/vendor/angular-ui-router.js",
                "~/scripts/vendor/angular-cookies.min.js",
                "~/scripts/vendor/angular-ui-tree.min.js",
                "~/scripts/vendor/checklist-model.js"
                ));

            bundles.Add(new ScriptBundle("~/js/app").Include(
                "~/scripts/common.js",
                 "~/scripts/filters/filters.js",
                 "~/scripts/services/services.js",
                 "~/scripts/directives/directives.js",
                 "~/scripts/controllers/controllers.js",
                 "~/scripts/controllers/system.js",
                 "~/scripts/app.js"
                 ));
        }
    }
}
