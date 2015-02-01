using IES.G2S.JW.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Views.Au
{
    /// <summary>
    /// Account1 的摘要说明
    /// </summary>
    public class Account1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = context.Request["action"];
        }

        public bool UpdAccount(HttpContext context)
        {
            IES.JW.Model.User user = new IES.JW.Model.User()
            {
                UserID = Convert.ToInt32(context.Request["UserID"]),
                Pwd = context.Request["Pwd"]
            };
            bool Judge = new UserBLL().ChangePassword(user);

            if (Judge == true)
            {
                //context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(user));
                return true;
            }
            else
            {
                //context.Response.Write("False");
                return false;
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