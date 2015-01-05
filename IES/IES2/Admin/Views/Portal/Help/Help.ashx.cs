using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Admin.Views.Portal.Help
{
    /// <summary>
    /// 周锡喻
    /// 2014-12-30
    /// </summary>
    public class Help1 : IHttpHandler
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
        public void GetHelpList(HttpContext context)
        {
            //IES.Portal.Model.Help Help = new IES.Portal.Model.Help();
            //int PageIndex = Convert.ToInt32(context.Request.Params["PageIndex"]);
            //int PageSize = Convert.ToInt32(context.Request.Params["PageSize"]);
            //IES.G2S.Portal.BLL.HelpBLL Helpbll = new IES.G2S.Portal.BLL.HelpBLL();
            //DataTable dt = IES.Common.ListToDateUtil.ListToDataTable<IES.Portal.Model.Help>(Helpbll.Help_List(Help, PageSize, PageIndex));

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