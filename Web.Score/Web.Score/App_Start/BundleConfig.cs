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
                    "~/content/angular-tree-control/tree-control.css",
                    "~/content/angularTree/angular-ui-tree.min.css",
                    "~/content/app.css"
                ));

            bundles.Add(new ScriptBundle("~/js/vendor").Include(
                    "~/scripts/vendor/jquery-1.11.1.min.js",
                    "~/content/bootstrap/js/bootstrap.min.js",
                    "~/content/bootstrap-dialog/js/bootstrap-dialog.min.js",
                    "~/content/datepicker/js/bootstrap-datepicker.js",
                    "~/content/datepicker/js/locales/bootstrap-datepicker.zh-CN.js",
                    "~/scripts/vendor/angular.js",
                    "~/scripts/vendor/angular-ui-router.min.js",
                    "~/content/angular-tree-control/angular-tree-control.js",
                    "~/content/angularTree/angular-ui-tree.min.js",
                    "~/scripts/vendor/angular-cookies.min.js",
                    "~/scripts/vendor/checklist-model.js",
                    "~/content/uploader/angular-file-upload.js",
                    "~/content/charts/esl/esl.js"
                ));

            bundles.Add(new ScriptBundle("~/js/app").Include(
                    "~/scripts/filters/filters.js",
                    "~/scripts/services/services.js",
                    "~/scripts/services/school.js",
                    "~/scripts/services/query.js",
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
