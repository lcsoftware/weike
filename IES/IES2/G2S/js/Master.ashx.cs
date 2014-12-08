using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G2S.js
{
    /// <summary>
    /// Master 的摘要说明
    /// </summary>
    public class Master : IHttpHandler
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

        public void GetUserName(HttpContext context) {
            context.Response.Write("张三");
        }


         
    }
}