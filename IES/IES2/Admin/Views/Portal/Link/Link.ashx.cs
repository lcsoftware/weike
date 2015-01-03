using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Admin.Views.Portal.Link
{
    /// <summary>
    /// 周锡喻
    /// 2014-12-30
    /// </summary>
    public class Link1 : IHttpHandler
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
        public void GetLinkList(HttpContext context)
        {
            //IES.Portal.Model.Link Link = new IES.Portal.Model.Link();
            //int PageIndex = Convert.ToInt32(context.Request.Params["PageIndex"]);
            //int PageSize = Convert.ToInt32(context.Request.Params["PageSize"]);
            //IES.G2S.Portal.BLL.LinkBLL Linkbll = new IES.G2S.Portal.BLL.LinkBLL();
            //DataTable dt = IES.Common.ListToDateUtil.ListToDataTable<IES.Portal.Model.Link>(Linkbll.Link_List(Link, PageSize, PageIndex));

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