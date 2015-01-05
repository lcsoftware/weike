using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Admin.Views.Portal.News
{
    /// <summary>
    /// 周锡喻
    /// 2014-12-30
    /// </summary>
    public class News1 : IHttpHandler
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
        public void GetNewsList(HttpContext context)
        {
            //IES.Portal.Model.News News = new IES.Portal.Model.News();
            //int PageIndex = Convert.ToInt32(context.Request.Params["PageIndex"]);
            //int PageSize = Convert.ToInt32(context.Request.Params["PageSize"]);
            //IES.G2S.Portal.BLL.NewsBLL Newsbll = new IES.G2S.Portal.BLL.NewsBLL();
            //DataTable dt = IES.Common.ListToDateUtil.ListToDataTable<IES.Portal.Model.News>(Newsbll.News_List(News, PageSize, PageIndex));

            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    context.Response.Write(Tools.JsonConvert.GetJSON(dt));
            //}
            //else
            //{
            //    context.Response.Write("empty");
            //}
        }
    }
}