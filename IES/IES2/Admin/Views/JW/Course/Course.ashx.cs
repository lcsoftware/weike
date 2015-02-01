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
            string action = context.Request["action"];
            if (!string.IsNullOrEmpty(action)) this.GetType().GetMethod(action).Invoke(this, new object[] { context });
            context.Response.End();
        }

        /// <summary>
        /// 课程分类列表
        /// </summary>
        /// <param name="context"></param>
        public void GetSpTypeList(HttpContext context)
        {
            IES.G2S.JW.BLL.CourseTypeBLL ctybll = new IES.G2S.JW.BLL.CourseTypeBLL();
            List<IES.JW.Model.Coursetype> list = ctybll.CourseType_Tree();
            if (list != null)
            {
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(list));
            }
            else
            {
                context.Response.Write("False");
            }
        }
        /// <summary>
        /// 上级课程分类列表
        /// </summary>
        /// <param name="context"></param>
        public void CourseType_P_List(HttpContext context)
        {
            IES.G2S.JW.BLL.CourseTypeBLL ctybll = new IES.G2S.JW.BLL.CourseTypeBLL();
            List<IES.JW.Model.Coursetype> ctyp = ctybll.CourseType_P_List();
            if (ctyp != null)
            {
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(ctyp));
            }
            else
            {
                context.Response.Write("False");
            }
        }
        /// <summary>
        /// 删除学科
        /// </summary>
        /// <param name="context"></param>
        public void CourseType_Del(HttpContext context)
        {
            IES.JW.Model.Coursetype _cty = new IES.JW.Model.Coursetype { CourseTypeID = Convert.ToInt32(context.Request["CourseTypeID"]) };
            IES.G2S.JW.BLL.CourseTypeBLL ctybll = new IES.G2S.JW.BLL.CourseTypeBLL();
            bool flag = ctybll.CourseType_Del(_cty);
            if (flag)
            {
                context.Response.Write("1");
            }
            else
            {
                context.Response.Write("False");
            }
        }
        /// <summary>
        /// 取消删除学科
        /// </summary>
        /// <param name="context"></param>
        public void CourseType_CancelDel(HttpContext context)
        {
            IES.JW.Model.Coursetype _cty = new IES.JW.Model.Coursetype { CourseTypeID = Convert.ToInt32(context.Request["CourseTypeID"]) };
            IES.G2S.JW.BLL.CourseTypeBLL ctybll = new IES.G2S.JW.BLL.CourseTypeBLL();
            bool flag = ctybll.CourseType_CancelDel(_cty);
            if (flag)
            {
                context.Response.Write("1");
            }
            else
            {
                context.Response.Write("False");
            }
        }

        /// <summary>
        /// 编辑学科
        /// </summary>
        /// <param name="context"></param>
        public void CourseType_Edit(HttpContext context)
        {
            IES.JW.Model.Coursetype _cty = new IES.JW.Model.Coursetype
            {
                CourseTypeID = Convert.ToInt32(context.Request["CourseTypeID"]),
                CourseTypeNo = context.Request["CourseTypeNo"],
                Name = context.Request["Name"],
                ParentID = Convert.ToInt32(context.Request["ParentID"]),
                Orde = Convert.ToInt32(context.Request["Orde"])
            };
            IES.G2S.JW.BLL.CourseTypeBLL ctybll = new IES.G2S.JW.BLL.CourseTypeBLL();
            IES.JW.Model.Coursetype flag = ctybll.CourseType_Edit(_cty);
            if (flag != null)
            {
                context.Response.Write("1");
            }
            else
            {
                context.Response.Write("False");
            }
        }
        /// <summary>
        /// 学期列表
        /// </summary>
        /// <param name="context"></param>
        public void GetTermList(HttpContext context)
        {
            IES.JW.Model.Term term = new IES.JW.Model.Term { Key = "", TermYear = "" };
            IES.G2S.JW.BLL.TermBLL Trbll = new IES.G2S.JW.BLL.TermBLL();
            List<IES.JW.Model.Term> list = Trbll.Term_List(term);
            if (list != null)
            {
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(list));
            }
            else
            {
                context.Response.Write("False");
            }
        }
        /// <summary>
        /// 授课方式列表
        /// </summary>
        /// <param name="context"></param>
        public void GetTeachTypeList(HttpContext context)
        {
            IES.G2S.JW.BLL.CourseTeachingTypeBLL TcTybll = new IES.G2S.JW.BLL.CourseTeachingTypeBLL();
            List<IES.JW.Model.CourseTeachingType> list = TcTybll.CourseTeachingType_List();
            if (list != null)
            {
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(list));
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