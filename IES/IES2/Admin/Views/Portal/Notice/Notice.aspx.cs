using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.Portal.Notice
{
    public partial class Notice : System.Web.UI.Page
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
            IES.SYS.Model.Notice _notice = new IES.SYS.Model.Notice { Title=string.Empty };
            IES.G2S.SYS.BLL.NoticeBLL noticebll = new IES.G2S.SYS.BLL.NoticeBLL();

            List<IES.SYS.Model.Notice> noticelist = noticebll.Notice_List(_notice, pageindex, AspNetPager1.PageSize);
            if (noticelist.Count > 0)
            {
                AspNetPager1.RecordCount = noticelist[0].rowscount;
                Repeater1.DataSource = noticelist;
                Repeater1.DataBind();
            }
        }
        //删除
        protected void btnInfo_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.hfID.Value);
            IES.SYS.Model.Notice _notice = new IES.SYS.Model.Notice { NoticeID = id };
            IES.G2S.SYS.BLL.NoticeBLL noticebll = new IES.G2S.SYS.BLL.NoticeBLL();
            bool result = noticebll.Notice_Del(_notice);
            if (result == true)
            {
                DataBinder(1);
            }
        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {

            DataBinder(AspNetPager1.CurrentPageIndex);

        }

    }
}