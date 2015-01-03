// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="Microsoft">
//   Copyright ?2014 Microsoft
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace App.G2S
{
    using System;


    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using System.Collections;
    using System.Collections.Generic;
    using IES.SYS.Model;

    public class Application : HttpApplication
    {
        protected void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            IES.Service.AuService.AuLoad(); //用户授权信息加载
            GetAttachList();
            RegisterRoutes(RouteTable.Routes);
        }

        /// <summary>
        /// 获取附件列表
        /// </summary>
        private void GetAttachList()
        { 
            
        }

         public static void RegisterRoutes(RouteCollection routes)
          {
               routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
               routes.MapRoute(
                    "Default1", // Route name
                    "{controller}/{action}/{id}", // URL with parameters
                    new { controller = "Home", action = "Index", id = "" });
                    // Parameter defaults );
          }



    }
}
