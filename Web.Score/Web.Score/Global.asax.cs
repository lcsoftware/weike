// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="">
//   Copyright ?2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace App.Web.Score
{
    using App.Score.Util;
    using System;
    using System.Web;
    using System.Web.Optimization;
    using System.Web.Routing;

    public class Application : HttpApplication
    {
        protected void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            try
            {
                App.Score.Data.AppHelper.Init();
                //UtilHelper.WriteContent("c:/Log/log.txt", "连接数据库OK");
            }
            catch (Exception ex)
            {
                UtilHelper.WriteContent("c:/Log/log.txt", "1111111111\n" + ex.StackTrace);
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = this.Context.Server.GetLastError();
            log4net.LogManager.GetLogger("ErrLog").Error(ex); 
            //this.Context.Response.Clear();
            //this.Context.Server.Transfer("/Error.aspx");
        }
    }
}
