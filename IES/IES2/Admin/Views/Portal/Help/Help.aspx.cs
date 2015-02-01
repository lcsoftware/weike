using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IES.Service.Common;
namespace Admin.Views.Portal.Help
{
    public partial class Help : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                AspNetPager1.PageSize = Browse.PageSize;
                DropDownList1.SelectedValue = Browse.PageSize.ToString();
                DataBinder(1);
            }
        }

        #region 绑定数据
        private void DataBinder(int pageindex)
        {
            if (Session["Help"] != null)
            { GetSession(); }
            string key = this.Key.Value;
            IES.Portal.Model.Help _help = new IES.Portal.Model.Help { Title=key};
            IES.G2S.Portal.BLL.HelpBLL helpbll = new IES.G2S.Portal.BLL.HelpBLL() ;

            List<IES.Portal.Model.Help> helplist = helpbll.Help_List(_help, pageindex, AspNetPager1.PageSize);
            if (helplist.Count > 0)
            {
                AspNetPager1.RecordCount = helplist[0].rowscount;
                Repeater1.DataSource = helplist;
                Repeater1.DataBind();
                this.number.InnerText = "（共" + AspNetPager1.RecordCount + "条）";
            }
            else
            {
                AspNetPager1.RecordCount = 1;
                Repeater1.DataSource = helplist;
                Repeater1.DataBind();
                this.number.InnerText = "（共0条）";
            }  
        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {

            DataBinder(AspNetPager1.CurrentPageIndex);

        }
        public void GetSession()
        {
            IES.Portal.Model.Help help = Session["Help"] as IES.Portal.Model.Help;
            this.Key.Value = help.Title.ToString();
        }
        #endregion

        #region 操作
        //删除
        protected void btnInfo_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.hfID.Value);
            IES.Portal.Model.Help _help = new IES.Portal.Model.Help { HelpID = id };
            IES.G2S.Portal.BLL.HelpBLL helpbll = new IES.G2S.Portal.BLL.HelpBLL();
            bool result = helpbll.Help_Del(_help);
            if (result == true)
            { DataBinder(1); }
        }
        //批量删除
        protected void BatchDel_Click(object sender, EventArgs e)
        {
            DelBatch();
        }
        public void DelBatch()
        {
            string IDS = this.hfIDS.Value;
            IES.G2S.Portal.BLL.HelpBLL helpbll = new IES.G2S.Portal.BLL.HelpBLL();
            bool result = helpbll.Help_Batch_Del(IDS);
            if (result == true)
            {
                DataBinder(1);
            }
        }
        //搜索
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            string key = this.Key.Value;
            IES.Portal.Model.Help help = new IES.Portal.Model.Help { Title = key };
            Session["Help"] = help;
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