using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Views.JW.Specialty
{
    /// <summary>
    /// SpecialtyType1 的摘要说明
    /// </summary>
    public class SpecialtyType1 : IHttpHandler
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
        /// 学科列表
        /// </summary>
        /// <param name="context"></param>
        public void GetSpTypeList(HttpContext context)
        {
            IES.G2S.JW.BLL.SpecialtyTypeBLL sptybll = new IES.G2S.JW.BLL.SpecialtyTypeBLL();
            List<IES.JW.Model.SpecialtyType> list = sptybll.SpecialtyType_Tree_List();
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
        /// 上级学科列表
        /// </summary>
        /// <param name="context"></param>
        public void SpecialtyType_P_List(HttpContext context)
        {
            IES.G2S.JW.BLL.SpecialtyTypeBLL sptybll = new IES.G2S.JW.BLL.SpecialtyTypeBLL();
            List<IES.JW.Model.SpecialtyType> sptyp = sptybll.SpecialtyType_P_List();
            if (sptyp != null)
            {
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(sptyp));
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
        public void SpecialtyType_Del(HttpContext context)
        {
            IES.JW.Model.SpecialtyType _spty = new IES.JW.Model.SpecialtyType { SpecialtyTypeID = Convert.ToInt32(context.Request["SpecialtyTypeID"]) };
            IES.G2S.JW.BLL.SpecialtyTypeBLL sptybll = new IES.G2S.JW.BLL.SpecialtyTypeBLL();
            bool flag = sptybll.SpecialtyType_Del(_spty);
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
        public void SpecialtyType_CancelDel(HttpContext context)
        {
            IES.JW.Model.SpecialtyType _spty = new IES.JW.Model.SpecialtyType { SpecialtyTypeID = Convert.ToInt32(context.Request["SpecialtyTypeID"]) };
            IES.G2S.JW.BLL.SpecialtyTypeBLL sptybll = new IES.G2S.JW.BLL.SpecialtyTypeBLL();
            bool flag = sptybll.SpecialtyType_CancelDel(_spty);
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
        public void SpecialtyType_Edit(HttpContext context)
        {
            IES.JW.Model.SpecialtyType _spty = new IES.JW.Model.SpecialtyType 
            { 
                SpecialtyTypeID = Convert.ToInt32(context.Request["SpecialtyTypeID"]),
                SpecialtyTypeNo = context.Request["SpecialtyTypeNo"],
                SpecialtyTypeName = context.Request["SpecialtyTypeName"],
                ParentID = Convert.ToInt32(context.Request["ParentID"])
            };
            IES.G2S.JW.BLL.SpecialtyTypeBLL sptybll = new IES.G2S.JW.BLL.SpecialtyTypeBLL();
            IES.JW.Model.SpecialtyType flag = sptybll.SpecialtyType_Edit(_spty);
            if (flag!=null)
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