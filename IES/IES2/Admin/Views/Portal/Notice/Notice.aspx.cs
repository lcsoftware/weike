using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IES.Service.Common;
namespace Admin.Views.Portal.Notice
{
    public partial class Notice : System.Web.UI.Page
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
            if (Session["Notice"] != null)
            { GetSession(); }
            string key = this.Key.Value;

            IES.JW.Model.Notice _notice = new IES.JW.Model.Notice { Key=key };
            IES.G2S.JW.BLL.NoticeBLL noticebll = new IES.G2S.JW.BLL.NoticeBLL();
            List<IES.JW.Model.Notice> noticelist = noticebll.Notice_List(_notice, pageindex, AspNetPager1.PageSize);
            if (noticelist.Count > 0)
            {
                AspNetPager1.RecordCount = noticelist[0].rowscount;
                Repeater1.DataSource = noticelist;
                Repeater1.DataBind();
                this.number.InnerText = "（共" + AspNetPager1.RecordCount + "条）";
            }
            else
            {
                AspNetPager1.RecordCount = 1;
                Repeater1.DataSource = noticelist;
                Repeater1.DataBind();
                this.number.InnerText = "（共0条）";
            }  
            
        }
        public void GetSession()
        {
            IES.JW.Model.Notice news = Session["Notice"] as IES.JW.Model.Notice;
            this.Key.Value = news.Key.ToString();
        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            DataBinder(AspNetPager1.CurrentPageIndex);
        }
        #endregion

        #region 操作
        //删除
        protected void btnInfo_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.hfID.Value);
            IES.JW.Model.Notice _notice = new IES.JW.Model.Notice { NoticeID = id };
            IES.G2S.JW.BLL.NoticeBLL noticebll = new IES.G2S.JW.BLL.NoticeBLL();
            bool result = noticebll.Notice_Del(_notice);
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
            IES.G2S.JW.BLL.NoticeBLL noticebll = new IES.G2S.JW.BLL.NoticeBLL();
            bool result = noticebll.Notice_Batch_Del(IDS);
            if (result == true)
            {
                DataBinder(1);
            }
        }
        //搜索
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            string key = this.Key.Value;
            IES.JW.Model.Notice _notice = new IES.JW.Model.Notice { Key = key };
            Session["Notice"] = _notice;
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