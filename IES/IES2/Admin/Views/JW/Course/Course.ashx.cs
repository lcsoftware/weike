using IES.G2S.JW.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using IES.JW.Model;
using System.Data;

namespace Admin.Views.JW.Course
{
    /// <summary>
    /// Course1 的摘要说明
    /// </summary>
    public class Course1 : IHttpHandler, IRequiresSessionState
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

        //获取课程列表
        public void GetCourseList(HttpContext context) {
            IES.JW.Model.Course course=new IES.JW.Model.Course ();
            string Key = "";
            course.CourseNo = context.Request.Params["CourseNo"];
            course.CourseName = context.Request.Params["CourseName"];
            int PageSize = Convert.ToInt32(context.Request.Params["PageSize"]);
            int PageIndex = Convert.ToInt32(context.Request.Params["PageIndex"]);
            IES.G2S.JW.BLL.CourseBLL coursebll = new IES.G2S.JW.BLL.CourseBLL();
            DataTable dt=IES.Common.ListToDateUtil.ListToDataTable<IES.JW.Model.Course>(coursebll.Course_List(Key,course, PageSize, PageIndex));
           
            if (dt != null && dt.Rows.Count > 0)
            {
                context.Response.Write(Tools.JsonConvert.GetJSON(dt));
            }
            else {
                context.Response.Write("empty");
            }
              

          
        }
    }
}