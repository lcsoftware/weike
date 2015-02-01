using IES.JW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Views.ADD
{
    /// <summary>
    /// AddCourse1 的摘要说明
    /// </summary>
    public class AddCourse1 : IHttpHandler
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
        public void GetCourseList(HttpContext context)
        {
            string Key = context.Request["Key"];
            int TermTypeID = Convert.ToInt32(context.Request["TermTypeID"]);
            int OrganizationID = Convert.ToInt32(context.Request["OrganizationID"]);
            int CourseTypeID = Convert.ToInt32(context.Request["CourseTypeID"]);
            int TeachingTypeID = Convert.ToInt32(context.Request["TeachingTypeID"]);
            int SubjectID1 = Convert.ToInt32(context.Request["SubjectID1"]);
            int SubjectID2 = Convert.ToInt32(context.Request["SubjectID2"]);
            decimal BeginFen = Convert.ToDecimal(context.Request["BeginFen"]);
            decimal EndFen = Convert.ToDecimal(context.Request["EndFen"]);
            int PageIndex = Convert.ToInt32(context.Request["PageIndex"]);
            int PageSize = Convert.ToInt32(context.Request["PageSize"]);
            IES.JW.Model.Course course = new IES.JW.Model.Course { Key = Key, TermTypeID = TermTypeID, OrganizationID = OrganizationID, CourseTypeID = CourseTypeID, TeachingTypeID = TeachingTypeID, SubjectID1 = SubjectID1, SubjectID2 = SubjectID2, BeginFen = BeginFen, EndFen = EndFen };
            IES.G2S.JW.BLL.CourseBLL coursebll = new IES.G2S.JW.BLL.CourseBLL();
            List<Course> course_ = coursebll.Course_List(course, PageIndex,PageSize );
            if (course_ != null)
            {
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(course_));
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