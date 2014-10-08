/* **************************************************************
  * Copyright(c) 2014 ScoreAnalyze, All Rights Reserved.   
  * File             : Global.cs
  * Description      : ȫ�����ݼ��ؼ�����
  * Author           : Fenglujian 
  * Created          : 2014-10-01  
  * Revision History : 
******************************************************************/
namespace App.ScoreAnalyze
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
            //���ݷ��ʲ��ʼ�� 
            Nevupo.Data.AppHelper.Init();
        }

        protected void Application_Request(object sender, System.EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(Eastday.Util.SysUserInfo.CName) || string.IsNullOrEmpty(Eastday.Util.SysUserInfo.CName))
            //{
            //    if (!Context.Request.Url.AbsolutePath.ToLower().EndsWith("login"))
            //    {
            //        Context.Response.Redirect("~/login");
            //    }
            //}
        }
    }
}
