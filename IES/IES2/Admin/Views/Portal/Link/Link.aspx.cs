using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IES.Service.Common;
namespace Admin.Views.Portal.Link
{
    public partial class Link : System.Web.UI.Page
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
            if (Session["Link"] != null)
            { GetSession(); }
            string key = this.Key.Value;
            IES.Portal.Model.Link _link = new IES.Portal.Model.Link { Title=key};
            IES.G2S.Portal.BLL.LinkBLL linkbll = new IES.G2S.Portal.BLL.LinkBLL();

            List<IES.Portal.Model.Link> linklist = linkbll.Link_List(_link, pageindex, AspNetPager1.PageSize);
            if (linklist.Count > 0)
            {
                AspNetPager1.RecordCount = linklist[0].rowscount;
                Repeater1.DataSource = linklist;
                Repeater1.DataBind();
                this.number.InnerText = "（共" + AspNetPager1.RecordCount + "条）";
            }
            else
            {
                AspNetPager1.RecordCount = 1;
                Repeater1.DataSource = linklist;
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
            IES.Portal.Model.Link link = Session["Link"] as IES.Portal.Model.Link;
            this.Key.Value = link.Title.ToString();
        }
        #endregion

        #region 操作
        //删除
        protected void btnInfo_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.hfID.Value);
            IES.Portal.Model.Link _link = new IES.Portal.Model.Link { LinkID = id };
            IES.G2S.Portal.BLL.LinkBLL linkbll = new IES.G2S.Portal.BLL.LinkBLL();
            bool result = linkbll.Link_Del(_link);
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
            IES.G2S.Portal.BLL.LinkBLL linkbll = new IES.G2S.Portal.BLL.LinkBLL();
            bool result = linkbll.Link_Batch_Del(IDS);
            if (result == true)
            { DataBinder(1); }
        }
        //搜索
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            string key = this.Key.Value;
            IES.Portal.Model.Link link = new IES.Portal.Model.Link { Title = key };
            Session["Link"] = link;
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