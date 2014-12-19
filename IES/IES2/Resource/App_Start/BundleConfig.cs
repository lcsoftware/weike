// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BundleConfig.cs" company="">
//   Copyright ?2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace App.Resource
{
    using System.Web;
    using System.Web.Optimization;

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/content/css/app").Include(
                "~/Frameworks/bootstrap/css/bootstrap.min.css",
                "~/Frameworks/bootstrap/css/bootstrap-theme.min.css",
                "~/Css/app.css"));

            bundles.Add(new ScriptBundle("~/js/framework").Include(
                "~/Frameworks/jquery/jquery-1.11.1.min.js",
                "~/Frameworks/bootstrap/js/bootstrap.min.js",
                "~/Frameworks/angular/angular.js",
                "~/Frameworks/angular/angular-cookies.js",
                "~/Frameworks/angular/angular-ui-router.js"
                )); 

            bundles.Add(new ScriptBundle("~/js/app").Include(
                "~/scripts/Common/filters.js",
                "~/scripts/Common/services.js",
                "~/scripts/Common/directives.js",

                "~/scripts/Home/HomeControllers.js",
                "~/scripts/User/UserControllers.js",
                "~/scripts/User/UserService.js",

                "~/scripts/app.js"));
        }
    }
}
