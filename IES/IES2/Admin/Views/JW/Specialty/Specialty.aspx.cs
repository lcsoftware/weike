using IES.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.JW.Specialty
{
    public partial class Specialty : System.Web.UI.Page
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

        #region 绑定数据
        private void DataBinder(int pageindex)
        {
            if (Session["Specialty"] != null)
            { GetSession(); }
            int orgid = -1;
            decimal schlength = -1;
            string key = this.Key.Value;
            string parms = this.Parms.Value;
            if (parms != "")
            {
                var ary = parms.Split(',');
                orgid = Convert.ToInt32(ary[9]);
                schlength = Convert.ToDecimal(ary[5]);
                Session["Parms"] = parms;
            }

            IES.JW.Model.Specialty _specialty = new IES.JW.Model.Specialty { Key = key, OrganizationID = orgid, SchoolingLength = schlength };           
            IES.G2S.JW.BLL.SpecialtyBLL specialtybll = new IES.G2S.JW.BLL.SpecialtyBLL();
            List<IES.JW.Model.Specialty> specialtylist = specialtybll.Specialty_List(_specialty, pageindex, AspNetPager1.PageSize);
            if (specialtylist != null)
            if (specialtylist.Count > 0)
            {
                AspNetPager1.RecordCount = specialtylist[0].rowscount;
                Repeater1.DataSource = specialtylist;
                Repeater1.DataBind();
                this.number.InnerText = "（共" + AspNetPager1.RecordCount + "条）";
            }
            else
            {
                AspNetPager1.RecordCount = 1;
                Repeater1.DataSource = specialtylist;
                Repeater1.DataBind();
                this.number.InnerText = "（共0条）";
            }
        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            DataBinder(AspNetPager1.CurrentPageIndex);
        }
        //绑定条件
        public void DataParm()
        {
            IES.G2S.JW.BLL.ParmInfoBLL parmbll = new IES.G2S.JW.BLL.ParmInfoBLL();
            IES.JW.Model.ParmInfo parmlist = parmbll.Parm_Info_List();

            rptorg.DataSource = parmlist.orglist;
            rptorg.DataBind();
            rptschlen.DataSource = parmlist.schlenlist;
            rptschlen.DataBind();
        }
        public void GetSession()
        {
            IES.JW.Model.Specialty specialty = Session["Specialty"] as IES.JW.Model.Specialty;
            this.Key.Value = specialty.Key.ToString();
        }
        public string Getclass()
        {
            string parms = Session["Parms"].ToString();
            ClientScript.RegisterStartupScript(ClientScript.GetType(), "../../Portal/Edit.js", "<script>Saveclass('" + parms + "');</script>");
            return null;
        }
        #endregion

        #region 操作
        //删除
        protected void btnInfo_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.hfID.Value);
            IES.JW.Model.Specialty specialty = new IES.JW.Model.Specialty { SpecialtyID = id };
            IES.G2S.JW.BLL.SpecialtyBLL speciabll = new IES.G2S.JW.BLL.SpecialtyBLL();
            bool result = speciabll.Specialty_Del(specialty);
            if (result == true)
            {
                DataBinder(1);
            }
        }
        //批量删除
        protected void BatchDel_Click(object sender, EventArgs e)
        {
            DelBatch();
        }

        public void DelBatch()
        {
            string IDS = this.hfIDS.Value;
            IES.G2S.JW.BLL.SpecialtyBLL specialtybll = new IES.G2S.JW.BLL.SpecialtyBLL();
            bool result = specialtybll.Specialty_Batch_Del(IDS);
            if (result == true)
            {
                DataBinder(1);
            }
        }

        //搜索
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            string key = this.Key.Value;
            IES.JW.Model.Specialty _specialty = new IES.JW.Model.Specialty { Key = key };
            Session["Specialty"] = _specialty;
            DataBinder(1);
        }
        //切换PageSize
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            AspNetPager1.PageSize = Int32.Parse(DropDownList1.SelectedValue);
            Browse.SetPageSize(AspNetPager1.PageSize);
            DataBinder(1);
        }
        #endregion
    }
}