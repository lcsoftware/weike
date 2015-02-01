using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IES.Portal.Model;
using IES.Service.Common;
namespace Admin.Views.Portal.News
{
    public partial class News : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {           
            if (!Page.IsPostBack)
            {
                DataSection();     
                AspNetPager1.PageSize = Browse.PageSize;
                DropDownList1.SelectedValue = Browse.PageSize.ToString();
                DataBinder(1);
            }
        }        

        #region 绑定数据
        private void DataBinder(int pageindex)
        {
            if (Session["News"] != null)
            { GetSession(); }
            string beginTime = this.BeginTime.Value;
            string endTime = this.EndTime.Value;
            string newsKey = this.Key.Value;
            int sectionID = Convert.ToInt32(this.Section.SelectedValue);

            IES.Portal.Model.News _news = new IES.Portal.Model.News { Key = newsKey, StartTime = Convert.ToDateTime(beginTime), EndTime = Convert.ToDateTime(endTime), SectionID = sectionID };
            IES.G2S.Portal.BLL.NewsBLL newsbll = new IES.G2S.Portal.BLL.NewsBLL();
            List<IES.Portal.Model.News> newslist = newsbll.News_List(_news, pageindex, AspNetPager1.PageSize);
            if (newslist.Count > 0)
            {
                AspNetPager1.RecordCount = newslist[0].rowscount;
                Repeater1.DataSource = newslist;
                Repeater1.DataBind();
                this.number.InnerText = "（共" + AspNetPager1.RecordCount + "条）";
            }
            else
            {
                AspNetPager1.RecordCount = 1;
                Repeater1.DataSource = newslist;
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
            IES.Portal.Model.News news = Session["News"] as IES.Portal.Model.News;
            this.BeginTime.Value = news.StartTime.ToString("yyyy-MM-dd ");
            this.EndTime.Value = news.EndTime.ToString("yyyy-MM-dd ");
            this.Key.Value = news.Key;
            this.Section.SelectedValue = news.SectionID.ToString();
        }
        public void DataSection()
        {
            List<IES.Portal.Model.NewsSection> sectionlist = new IES.G2S.Portal.BLL.NewsBLL().NewsSection_List();
            Section.DataSource = sectionlist;
            Section.DataTextField = "SectionName";
            Section.DataValueField = "SectionID";
            Section.DataBind();
            Section.Items.Insert(0, new ListItem("全部", "-1"));
        }
        #endregion

        #region 操作
        //删除
        protected void btnInfo_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.hfID.Value);
            IES.Portal.Model.News _news = new IES.Portal.Model.News { NewsID = id };
            IES.G2S.Portal.BLL.NewsBLL newsbll = new IES.G2S.Portal.BLL.NewsBLL();
            bool result = newsbll.News_Del(_news);
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
            IES.G2S.Portal.BLL.NewsBLL newsbll = new IES.G2S.Portal.BLL.NewsBLL();
            bool result = newsbll.News_Batch_Del(IDS);
            if (result == true)
            { DataBinder(1); }
        }
        //搜索
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            string beginTime = this.BeginTime.Value;
            string endTime = this.EndTime.Value;
            string newsKey = this.Key.Value;
            int sectionID = Convert.ToInt32(this.Section.SelectedValue);
            IES.Portal.Model.News _news = new IES.Portal.Model.News { Key = newsKey, StartTime = Convert.ToDateTime(beginTime), EndTime = Convert.ToDateTime(endTime), SectionID = sectionID };
            Session["News"] = _news;
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