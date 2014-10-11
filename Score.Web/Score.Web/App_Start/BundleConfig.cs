namespace App.Score.Web
{
    using System.Web;
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("/content/css/app").Include(
                    "~/content/app.css",
                    "~/content/bootstrap/bootstrap.min.css",
                    "~/content/bootstrap/bootstrap-theme.min.css"
                ));

            bundles.Add(new ScriptBundle("/js/vendor").Include(
                    "~/scripts/vendor/jquery-1.11.1.min.js",
                    "~/content/bootstrap/js/bootstrap.min.js",
                    "~/scripts/vendor/angular.min.js",
                    "~/scripts/vendor/angular-ui-router.min.js",
                    "~/scripts/vendor/angular-cookies.min.js"
                ));

            bundles.Add(new ScriptBundle("/js/app").Include(
                    "~/scripts/filters.js",
                    "~/scripts/services.js",
                    "~/scripts/directives.js",
                    "~/scripts/controllers.js",
                    "~/scripts/utils.js",
                    "~/scripts/app.admin.js",
                    "~/scripts/app.base.js",
                    "~/scripts/app.exam.js",
                    "~/scripts/app.query.jy",
                    "~/scripts/app.score.js",
                    "~/scripts/app.statistic.js",
                    "~/scripts/app.js"
                ));
        }
    }
}
