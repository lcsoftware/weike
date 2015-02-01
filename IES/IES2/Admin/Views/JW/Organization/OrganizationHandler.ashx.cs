using IES.G2S.JW.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Views.JW.Organization
{
    /// <summary>
    /// Organization1 的摘要说明
    /// </summary>
    public class Organization1 : IHttpHandler
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
        /// 组织机构列表
        /// </summary>
        /// <param name="context"></param>
        public void Organization_List(HttpContext context)
        {
            List<IES.JW.Model.Organization> list = new OrganizationBLL().Organization_List(null);
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
        /// 组织机构类别列表
        /// </summary>
        /// <param name="context"></param>
        public void OrganizationType_List(HttpContext context)
        {
            List<IES.JW.Model.OrganizationType> list = new OrganizationTypeBLL().OrganizationType_List();
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
        /// 新增或编辑组织机构
        /// </summary>
        /// <param name="contex"></param>
        public void Organization_Edit(HttpContext context)
        {
            IES.JW.Model.Organization org = new IES.JW.Model.Organization()
            {
                OrganizationID = Convert.ToInt32(context.Request["OrganizationID"]),
                OrganizationNo = context.Request["OrganizationNo"],
                ParentID = Convert.ToInt32(context.Request["ParentID"]),
                OrganizationName = context.Request["OrganizationName"],
                OrganizationNameEn = context.Request["OrganizationNameEn"],
                OrganizationTypeID = Convert.ToInt32(context.Request["OrganizationTypeID"]),
                IsShow = Convert.ToBoolean(context.Request["IsShow"]),
                IsTeaching = Convert.ToBoolean(context.Request["IsTeaching"]),
                Link = context.Request["Link"],
                LinkStatus = Convert.ToBoolean(context.Request["LinkStatus"]),
                Introduction = context.Request["Introduction"],
                IntroductionEn = context.Request["IntroductionEn"]
            };
            org = new OrganizationBLL().Organization_Edit(org);

            if (org != null)
            {
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(org));
            }
            else
            {
                context.Response.Write("False");
            }
        }

        /// <summary>
        /// 删除组织机构
        /// </summary>
        /// <param name="context"></param>
        public void Organization_Del(HttpContext context)
        {
            IES.JW.Model.Organization org = new IES.JW.Model.Organization()
            {
                OrganizationID = Convert.ToInt32(context.Request["OrganizationID"])
            };
            bool flag = new OrganizationBLL().Organization_Del(org);

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
        /// 删除组织机构
        /// </summary>
        /// <param name="context"></param>
        public void Organization_CancelDel(HttpContext context)
        {
            IES.JW.Model.Organization org = new IES.JW.Model.Organization()
            {
                OrganizationID = Convert.ToInt32(context.Request["OrganizationID"])
            };
            bool flag = new OrganizationBLL().Organization_CancelDel(org);

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
        /// 移动组织机构
        /// </summary>
        /// <param name="SelfID"></param>
        /// <param name="OptionID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public void Organization_Move(HttpContext context)
        {
            //int SelfID, int OptionID, string type
            int SelfID = Convert.ToInt32(context.Request["SelfID"]);
            int OptionID = Convert.ToInt32(context.Request["OptionID"]);
            string type = context.Request["type"];

            bool flag=new OrganizationBLL().Organization_Move(SelfID, OptionID, type);

            if (flag)
            {
                context.Response.Write("1");
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