using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using IES.SYS.Model;
using IES.CC.OC.Model;

namespace G2S.Exercise
{
    /// <summary>
    /// Exercise 的摘要说明
    /// </summary>
    public class Exercise : IHttpHandler
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

        //获取适用课程
        public void GetApplyCourse(HttpContext context) {
            int webSite = Convert.ToInt32(context.Request.Params["webSite"]);
            User user = new User();
            user.UserID = 1;
            user.UserType = 2;
            user.UserNo = "1";
            List<OC> user_oc = IES.Common.Data.UserCommonData.User_OC_Get(user);
            DataTable dt= IES.Common.ListToDateUtil.ListToDataTable<OC>(user_oc);
            if (dt != null && dt.Rows.Count > 0)
            {
                context.Response.Write(Tools.JsonConvert.GetJSON(dt));
            }
            else {
                context.Response.Write("empty");
            }
        }

        public DataTable dtApplyCourse(int count) {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ID"));
            dt.Columns.Add(new DataColumn("name"));
            DataRow dr;
            for (int i = 0; i < count; i++)
            {
                dr = dt.NewRow();
                dr["ID"] = count.ToString();
                dr["name"] = "适用课程" + i.ToString();
                dt.Rows.Add(dr);
            }
            return dt;
        }

          
        
    }
}