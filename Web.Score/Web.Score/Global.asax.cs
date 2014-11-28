// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="">
//   Copyright ?2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace App.Web.Score
{
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
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger("ErrLog").Error(ex);
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = this.Context.Server.GetLastError();
            log4net.ILog ErrLog = log4net.LogManager.GetLogger("ErrLog");
            ErrLog.Error(ex);

            //this.Context.Response.Clear();
            //this.Context.Server.Transfer("/Error.aspx");
        }
    }
}
