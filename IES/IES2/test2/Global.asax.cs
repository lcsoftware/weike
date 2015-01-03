// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="Microsoft">
//   Copyright ?2014 Microsoft
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace App.test2
{
    using System.Web;
    using System.Web.Optimization;
    using System.Web.Routing;

    public class Application : HttpApplication
    {
        protected void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
