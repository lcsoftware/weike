using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Admin.Views.JW.Class
{
    /// <summary>
    /// Class1 的摘要说明
    /// </summary>
    public class Class1 : IHttpHandler
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

        //获取行政班列表
        public void GetClassList(HttpContext context)
        {
            IES.JW.Model.Class classs = new IES.JW.Model.Class();
            classs.ClassNo = context.Request.Params["ClassNo"];
            classs.ClassName = context.Request.Params["ClassName"];
            int UserID = Convert.ToInt32(context.Request.Params["UserID"]);
            int PageIndex = Convert.ToInt32(context.Request.Params["PageIndex"]);
            int PageSize = Convert.ToInt32(context.Request.Params["PageSize"]);          
            IES.G2S.JW.BLL.ClassBLL coursebll = new IES.G2S.JW.BLL.ClassBLL();
            DataTable dt = IES.Common.ListToDateUtil.ListToDataTable<IES.JW.Model.Class>(coursebll.Class_List(classs,UserID, PageSize, PageIndex));

            if (dt != null && dt.Rows.Count > 0)
            {
                context.Response.Write(Tools.JsonConvert.GetJSON(dt));
            }
            else
            {
                context.Response.Write("empty");
            }

            

        }
        //public List<IES.Portal.Model.Link> Link_List()
        //{
        //    return null;
        //}

    }
}