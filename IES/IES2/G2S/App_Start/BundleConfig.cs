// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BundleConfig.cs" company="Microsoft">
//   Copyright ?2014 Microsoft
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace App.G2S
{
    using System.Web;
    using System.Web.Optimization;

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
           
            //�������js �������Ҫ���ص�
            bundles.Add(new ScriptBundle("~/js/framework").Include(
                    "~/js/jquery-1.8.3.min.js",
                    //"~/Frameworks/jquery/jquery-1.11.1.min.js",  //���jq�汾������ ������һЩ��Ҫjq�Ŀ��
                    "~/Frameworks/bootstrap/js/bootstrap.min.js",
                    "~/Frameworks/angular/angular.js",
                    "~/Frameworks/angular/angular-ui-router.min.js",
                    "~/Frameworks/angular/angular-cookies.min.js",
                    "~/Frameworks/bootstrap/js/bootstrap-modal.js",
                     "~/Frameworks/bootstrap/js/angular-ui-tree.js",
                    "~/Frameworks/bootstrap/js/bootstrap-transition.js"
                    
                ));
            //�������js ����Ҫ�е�
            bundles.Add(new ScriptBundle("~/js/app").Include(
                  "~/scripts/common/directives.js",
                  "~/scripts/common/filters.js",
                  "~/scripts/common/services.js",
                  "~/scripts/controllers/OC/Site/SiteController.js",
                  "~/scripts/controllers/OC/FC/FCController.js",
                  "~/scripts/controllers/home/HomeController.js",
                  "~/scripts/controllers/user/UserController.js",
                  "~/scripts/services/user/UserService.js",
                  "~/scripts/controllers/CourseLive/forum/forumcontrol.js",
                  "~/scripts/controllers/OC/Team/TeamController.js",
                  "~/scripts/controllers/CourseLive/Score/ScoreController.js",
                  "~/scripts/controllers/OC/Class/ClassController.js",
                  "~/scripts/app.js"
                 
              ));
            //��Ӧ_Layout.cshtml ����ʽ
            bundles.Add(new StyleBundle("~/content/css/Layout").Include(
                   "~/Css/footer.css",
                   "~/Css/header.css",
                   //"~/Css/index.css",
                   "~/Css/side_left.css",
                   "~/Css/reverse.css",
                   "~/Css/common.css"
               ));
            //��Ӧ_Layout.cshtml ��js
            bundles.Add(new ScriptBundle("~/js/Layout").Include(
                  
                  "~/js/G2S.js",
                 // "~/js/TopMaster.js",
               //   "~/js/leftMaster.js",
               //   "~/js/FootMaster.js",
                  "~/js/index.js"
                ));


            //��Ӧ_Layout2.cshtml ����ʽ
            bundles.Add(new StyleBundle("~/content/css/Layout2").Include(
                 "~/Frameworks/bootstrap/js/angular-ui-tree.min.css",
                   "~/Css/construction.css",
                   "~/Frameworks/bootstrap/css/bootstrap.css"
               ));

            //��Ӧ_Layout2.cshtml  ��js
            bundles.Add(new ScriptBundle("~/js/Layout2").Include(
              
                "~/js/G2S.js",
                "~/js/construction.js"
            ));
        }
    }
}
