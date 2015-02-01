using IES.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.TScheme
{
    public partial class TeachingClass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataParm();
            if (!IsPostBack)
            {
                AspNetPager1.PageSize = Browse.PageSize;
                DropDownList1.SelectedValue = Browse.PageSize.ToString();
                DataBinder(1);
                if (Session["Parms"] == null)
                    Session["Parms"] = "-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,";
            }
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="pageindex"></param>
        private void DataBinder(int pageindex)
        {
            if (Session["Class"] != null)
            { GetSession(); }
            int orgid = 0;
            string key = this.txtKey.Value;
            DateTime strattime = Convert.ToDateTime(this.BeginTime.Value);
            DateTime endtime = Convert.ToDateTime(this.EndTime.Value);
            string parms = this.Parms.Value;
            if (parms != "")
            {
                var ary = parms.Split(',');
                orgid = Convert.ToInt32(ary[9]);
                Session["Parms"] = parms;
            }
            IES.JW.Model.TeachingClass _teacherclass = new IES.JW.Model.TeachingClass { Key = key, OrganizationID = orgid, StartTime = strattime, EndTime = endtime };
            IES.G2S.JW.BLL.TeachingClassBLL teachingclassbll = new IES.G2S.JW.BLL.TeachingClassBLL();
            List<IES.JW.Model.TeachingClass> classlist = teachingclassbll.TeachingClass_List(_teacherclass, pageindex, AspNetPager1.PageSize);
            if (classlist != null)
                if (classlist.Count > 0)
                {
                    AspNetPager1.RecordCount = classlist[0].rowscount;
                    Repeater1.DataSource = classlist;
                    Repeater1.DataBind();
                    this.number.InnerText = "（共" + AspNetPager1.RecordCount + "条）";
                }
                else
                {
                    AspNetPager1.RecordCount = 1;
                    Repeater1.DataSource = classlist;
                    Repeater1.DataBind();
                    this.number.InnerText = "（共0条）";
                }  
        }

        //绑定条件
        public void DataParm()
        {
            IES.G2S.JW.BLL.ParmInfoBLL parmbll = new IES.G2S.JW.BLL.ParmInfoBLL();
            IES.JW.Model.ParmInfo parmlist = parmbll.Parm_Info_List();
            //绑定数据
            rptorg.DataSource = parmlist.orglist;
            rptorg.DataBind();
        }

        public void GetSession()
        {
            IES.JW.Model.TeachingClass _teacherclass = Session["Class"] as IES.JW.Model.TeachingClass;
            this.txtKey.Value = _teacherclass.Key.ToString();
            this.BeginTime.Value = _teacherclass.StartTime.ToString("yyyy-MM-dd ");
            this.EndTime.Value = _teacherclass.EndTime.ToString("yyyy-MM-dd ");
        }

        public string Getclass()
        {
            string parms = Session["Parms"].ToString();
            ClientScript.RegisterStartupScript(ClientScript.GetType(), "../Portal/Edit.js", "<script>Saveclass('" + parms + "');</script>");
            return null;
        }

        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            DataBinder(AspNetPager1.CurrentPageIndex);
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            AspNetPager1.PageSize = Int32.Parse(DropDownList1.SelectedValue);
            Browse.SetPageSize(AspNetPager1.PageSize);
            DataBinder(1);
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            string key = this.txtKey.Value;
            DateTime strattime = Convert.ToDateTime(this.BeginTime.Value);
            DateTime endtime = Convert.ToDateTime(this.EndTime.Value);
            IES.JW.Model.TeachingClass _class = new IES.JW.Model.TeachingClass { Key = key, StartTime = strattime, EndTime = endtime };
            Session["Class"] = _class;
            DataBinder(1);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnInfo_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.hfID.Value);
            IES.JW.Model.TeachingClass _class = new IES.JW.Model.TeachingClass { TeachingClassID = id };
            IES.G2S.JW.BLL.TeachingClassBLL teachingclassbll = new IES.G2S.JW.BLL.TeachingClassBLL();
            bool result = teachingclassbll.TeachingClass_Del(_class);
            if (result == true)
            {
                DataBinder(1);
            }
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BatchDel_Click(object sender, EventArgs e)
        {
            DelBatch();
        }

        public void DelBatch()
        {
            string IDS = this.hfIDS.Value;
            IES.G2S.JW.BLL.ClassBLL classbll = new IES.G2S.JW.BLL.ClassBLL();
            bool result = classbll.Class_Batch_Del(IDS);
            if (result == true)
            {
                DataBinder(1);
            }
        }
    }
}