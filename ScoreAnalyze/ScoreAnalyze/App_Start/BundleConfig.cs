/* **************************************************************
  * Copyright(c) 2014 ScoreAnalyze, All Rights Reserved.   
  * File             : SystemService.aspx.cs
  * Description      : 系统级别的服务器处理程序
  * Author           : Fenglujian 
  * Created          : 2014-10-01  
  * Revision History : 
******************************************************************/
namespace App.ScoreAnalyze
{
    using System.Web;
    using System.Web.Optimization;

    /// <summary>
    /// 绑定配置
    /// </summary>
    public class BundleConfig
    {
        /// <summary>
        /// 执行注册(MVC)
        /// </summary>
        /// <param name="bundles">BundleCollection</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/content/css/app").Include(
                "~/content/app.css",
                "~/content/bootstrap-theme.min.css",
                "~/content/datepicker/css/datepicker3.css",
                "~/content/tree/angular-ui-tree.min.css",
                "~/content/menu.css",
                "~/content/style.css"
            ));

            bundles.Add(new ScriptBundle("~/js/vendor").Include(
                "~/scripts/vendor/jquery-1.11.1.min.js",
                "~/Scripts/vendor/bootstrap.min.js",
                 "~/content/datepicker/js/bootstrap-datepicker.js",
                 "~/content/datepicker/js/locales/bootstrap-datepicker.zh-CN.js",
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
