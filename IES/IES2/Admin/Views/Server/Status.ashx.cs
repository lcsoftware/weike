using IES.G2S.JW.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Views.Server
{
    /// <summary>
    /// Status1 的摘要说明
    /// </summary>
    public class Status1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = context.Request["action"];
            if (!string.IsNullOrEmpty(action))
            {
                try
                {
                    System.Reflection.MethodInfo method = this.GetType().GetMethod(action);
                    method.Invoke(this, new object[] { context });
                }
                catch (Exception ex)
                {
                    context.Response.Write("False");
                    context.Response.End();
                }
            }
        }

        //存储服务器详细
        public void GetServer(HttpContext context)
        {
            IES.JW.Model.ResourceServer _server = new IES.JW.Model.ResourceServer { ServerID = Convert.ToInt32(context.Request["ServerID"]) };
            IES.G2S.JW.BLL.ResourceServerBLL serverbll = new IES.G2S.JW.BLL.ResourceServerBLL();
            IES.JW.Model.ResourceServer server = serverbll.ResourceServer_Get(_server);
            if (server != null)
            {
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(server));
            }
            else
            {
                context.Response.Write("False");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}