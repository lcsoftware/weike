using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IES.Portal.Model;

namespace Admin.Views.Portal.News
{
    public partial class News : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                AspNetPager1.PageSize = 20;
                DataBinder(1);

            }
        }

        private void DataBinder(int pageindex)
        {
            string beginTime = this.BeginTime.Value;
            string endTime = this.EndTime.Value;
            IES.Portal.Model.News _news = new IES.Portal.Model.News { Title = string.Empty,Startdate=Convert.ToDateTime(beginTime),Enddatef=Convert.ToDateTime(endTime)};
            IES.G2S.Portal.BLL.NewsBLL newsbll = new IES.G2S.Portal.BLL.NewsBLL();

            List<IES.Portal.Model.News> newslist = newsbll.News_List(_news, pageindex, AspNetPager1.PageSize);
            if (newslist.Count > 0)
            {
                AspNetPager1.RecordCount = newslist[0].rowscount;
                Repeater1.DataSource = newslist;
                Repeater1.DataBind();
            }
        }
        //删除
        protected void btnInfo_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.hfID.Value);
            IES.Portal.Model.News _news = new IES.Portal.Model.News { Newsid = id };
            IES.G2S.Portal.BLL.NewsBLL newsbll = new IES.G2S.Portal.BLL.NewsBLL();
            bool result = newsbll.News_Del(_news);
            if (result == true)
            {
                DataBinder(1);
            }
        }
        //搜索
        public string DataSelect()
        {
            DataBinder(1);
            return null;
        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {

            DataBinder(AspNetPager1.CurrentPageIndex);

        }
    }
}