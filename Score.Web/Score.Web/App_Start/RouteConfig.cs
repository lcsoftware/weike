// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RouteConfig.cs" company="">
//   Copyright ?2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace App.Score.Web
{
    using System.Web.Routing;

    using App.Score.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Add("Default", new DefaultRoute());
        }
    }
}
