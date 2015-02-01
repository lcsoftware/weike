using IES.JW.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Admin.Views.ADD
{
    /// <summary>
    /// AddStudent1 的摘要说明
    /// </summary>
    public class AddStudent1 : IHttpHandler
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

        //组织机构下的学生
        public void GetStuList(HttpContext context)
        {
            string Key = context.Request["Key"];
            int OrganizationID = Convert.ToInt32(context.Request["OrganizationID"]);
            int SpecialtyID = Convert.ToInt32(context.Request["SpecialtyID"]);
            int ClassID = Convert.ToInt32(context.Request["ClassID"]);
            int IsRegister = Convert.ToInt32(context.Request["IsRegister"]);
            string StudentIDs = context.Request["StudentIDs"];
            int PageSize = Convert.ToInt32(context.Request["PageSize"]);
            int PageIndex = Convert.ToInt32(context.Request["PageIndex"]);
            IES.G2S.JW.BLL.UserBLL userbll = new IES.G2S.JW.BLL.UserBLL();
            List<User> user = userbll.Student_Search(Key, OrganizationID,SpecialtyID,ClassID,IsRegister,StudentIDs,PageSize,PageIndex);
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