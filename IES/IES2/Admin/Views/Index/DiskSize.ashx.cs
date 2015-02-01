using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using IES.G2S.JW.BLL;

namespace Admin.Views.Index
{
    /// <summary>
    /// DiskSize1 的摘要说明
    /// </summary>
    public class DiskSize1 : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.AddHeader("Cache-Control", "no-cache,must-revalidate");
            string action = context.Request.Params["action"];

            if (!string.IsNullOrEmpty(action)) this.GetType().GetMethod(action).Invoke(this, new object[] { context });
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        //获取通知列表
        public void LoadServerState(HttpContext context)
        {
            NoticeBLL noticeBLL = new NoticeBLL();
            IES.JW.Model.User user = IES.Service.UserService.CurrentUser;
            context.Response.Write("");           
        }
    }
}