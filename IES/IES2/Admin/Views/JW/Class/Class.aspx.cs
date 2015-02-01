using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IES.JW.Model;
using IES.Service.Common;

namespace Admin.Views.JW.Class
{
    public partial class Class : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataParm();           
            if (!Page.IsPostBack)
            {
                AspNetPager1.PageSize = Browse.PageSize;
                DropDownList1.SelectedValue = Browse.PageSize.ToString();
                DataBinder(1);
                if (Session["Parms"]==null)
                    Session["Parms"] = "-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,";
            }
        }
        #region 绑定数据
        private void DataBinder( int pageindex  )
        {
            if (Session["Class"] != null)
            { GetSession(); }
            int orgid = -1;
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
            IES.JW.Model.Class _class = new IES.JW.Model.Class { Key = key, OrganizationID = orgid,StartTime= strattime,EndTime=endtime };
            IES.G2S.JW.BLL.ClassBLL classbll = new IES.G2S.JW.BLL.ClassBLL();         
            List<IES.JW.Model.Class> classlist = classbll.Class_List(_class, pageindex, AspNetPager1.PageSize);
            if(classlist!=null)
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
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            DataBinder(AspNetPager1.CurrentPageIndex );           
        }
        //绑定条件
        public void DataParm()
        {
            IES.G2S.JW.BLL.ParmInfoBLL parmbll = new IES.G2S.JW.BLL.ParmInfoBLL();
            IES.JW.Model.ParmInfo parmlist = parmbll.Parm_Info_List();

            rptorg.DataSource = parmlist.orglist;
            rptorg.DataBind();
        }
        public void GetSession()
        {
            IES.JW.Model.Class _class = Session["Class"] as IES.JW.Model.Class;
            this.txtKey.Value = _class.Key.ToString();
            this.BeginTime.Value = _class.StartTime.ToString("yyyy-MM-dd ");
            this.EndTime.Value = _class.EndTime.ToString("yyyy-MM-dd ");
        }
        public string Getclass()
        {
            string parms = Session["Parms"].ToString();
            ClientScript.RegisterStartupScript(ClientScript.GetType(), "../../Portal/Edit.js", "<script>Saveclass('" + parms + "');</script>");
            return null;
        }
        #endregion
        //删除
        protected void btnInfo_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.hfID.Value);
            IES.JW.Model.Class _class = new IES.JW.Model.Class { ClassID = id };
            IES.G2S.JW.BLL.ClassBLL classbll = new IES.G2S.JW.BLL.ClassBLL();
            bool result = classbll.Class_Del(_class);
            if (result == true)
            {
                DataBinder(1);
            }
        }
        //搜索
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            string key = this.txtKey.Value;
            DateTime strattime = Convert.ToDateTime(this.BeginTime.Value);
            DateTime endtime = Convert.ToDateTime(this.EndTime.Value);
            IES.JW.Model.Class _class = new IES.JW.Model.Class { Key = key, StartTime = strattime, EndTime = endtime };
            Session["Class"] = _class;
            DataBinder(1);
        }

        //批量删除
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

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            AspNetPager1.PageSize = Int32.Parse(DropDownList1.SelectedValue);
            Browse.SetPageSize(AspNetPager1.PageSize);
            DataBinder(1);
        }
    }
}