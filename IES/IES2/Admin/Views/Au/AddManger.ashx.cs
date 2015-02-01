using IES.JW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Views.Au
{
    /// <summary>
    /// AddManger1 的摘要说明
    /// </summary>
    public class AddManger1 : IHttpHandler
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


        //组织机构下的教师
        public void GetTeacherList(HttpContext context)
        {
            string Key = context.Request["Key"];
            int OrganizationID = Convert.ToInt32(context.Request["OrganizationID"]);
            int PageSize = Convert.ToInt32(context.Request["PageSize"]);
            int PageIndex = Convert.ToInt32(context.Request["PageIndex"]);
            IES.G2S.JW.BLL.UserBLL userbll = new IES.G2S.JW.BLL.UserBLL();
            List<User> user = userbll.Teacher_Search(Key, OrganizationID, PageSize, PageIndex);
            if (user != null)
            {
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(user));
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