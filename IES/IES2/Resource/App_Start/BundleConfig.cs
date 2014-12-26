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
                //"~/Frameworks/bootstrap/css/bootstrap.min.css",
                //"~/Frameworks/bootstrap/css/bootstrap-theme.min.css",
                "~/Css/common.css",
                "~/Css/footer.css",
                "~/Css/forum.css",
                "~/Css/header.css",
                "~/Css/index.css",
                "~/Css/side_left.css"));

            bundles.Add(new ScriptBundle("~/js/framework").Include(
                "~/Frameworks/jquery/jquery-1.7.2.min.js",
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
                "~/scripts/Authority/AuthService.js", 
                "~/scripts/Shared/ContentControllers.js",
                "~/scripts/Exercise/ExerciseService.js",
                "~/scripts/Exercise/ExerciseControllers.js", 
                "~/scripts/Resource/ResourceService.js",
                "~/scripts/Resource/ResourceControllers.js", 
                "~/scripts/Knowledge/KnowService.js",
                "~/scripts/Knowledge/KnowControllers.js", 
                "~/scripts/Paper/PaperService.js",
                "~/scripts/Paper/PaperControllers.js", 
                "~/scripts/User/UserService.js", 
                "~/scripts/User/UserControllers.js",
                "~/scripts/app.js"));
        }
    }
}
