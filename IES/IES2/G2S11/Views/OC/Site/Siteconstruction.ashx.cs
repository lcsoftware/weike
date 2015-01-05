using IES.CC.OC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using IES.G2S.OC.BLL.OC.OCBLL;
using IES.G2S.OC.IBLL.OC;
using IES.AOP.G2S;
using System.Data;
namespace G2S.Views.OC.Site
{
    /// <summary>
    /// Siteconstruction 的摘要说明
    /// </summary>
    public class Siteconstruction : IHttpHandler
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

        public void SaveOCSiteColumn_ADD(HttpContext context) {
            OCSiteColumn column = new OCSiteColumn();
            column.ColumnID = Convert.ToInt32(context.Request.Params["ColumnID"]);
            column.OCID = Convert.ToInt32(context.Request.Params["OCID"]);
            column.UserID = 1;
            column.ParentID = Convert.ToInt32(context.Request.Params["ParentID"]);
            column.Title = context.Request.Params["Title"];
            column.ContentType = Convert.ToInt32(context.Request.Params["ContentType"]);
           // int ColumnID = IES.G2S.OC.BLL.OC.OCBLL.OCSiteColumn_ADD(column);
            //context.Response.Write(ColumnID.ToString());

            //IOCBLL oc = AopServerFactory.getExerciseServer().GetServer<IOCBLL>();
            //int flag= oc.OCSiteColumn_ADD(column);
            //context.Response.Write(flag.ToString());



        }

        //修改网站风格
        public void SaveOCSite_DisplayStyle_Upd(HttpContext context) {
            int SiteID = Convert.ToInt32(context.Request.Params["SiteID"]);
            int DisplayStyle = Convert.ToInt32(context.Request.Params["DisplayStyle"]);
           // bool flag = IES.G2S.OC.BLL.OC.OCBLL.OCSite_DisplayStyle_Upd(SiteID,DisplayStyle);
           // context.Response.Write(flag.ToString());
        }


        //获取网站的栏目列表
        public void GetOCSite(HttpContext context) {
            int SiteID = Convert.ToInt32(context.Request.Params["SiteID"]);
            int UserID = 1;
         //   List<OCSite> ocsite = IES.G2S.OC.BLL.OC.OCBLL.OCSite_Get(SiteID, UserID);
            //DataTable dt = IES.Common.ListToDateUtil.ListToDataTable<OCSite>(ocsite);
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