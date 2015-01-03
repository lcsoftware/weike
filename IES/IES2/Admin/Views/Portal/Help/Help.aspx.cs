using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.Portal.Help
{
    public partial class Help : System.Web.UI.Page
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
            IES.Portal.Model.Help _help = new IES.Portal.Model.Help();
            IES.G2S.Portal.BLL.HelpBLL helpbll = new IES.G2S.Portal.BLL.HelpBLL();

            List<IES.Portal.Model.Help> helplist = helpbll.Help_List(_help, pageindex, AspNetPager1.PageSize);
            if (helplist.Count > 0)
            {
                AspNetPager1.RecordCount = helplist[0].rowscount;
                Repeater1.DataSource = helplist;
                Repeater1.DataBind();
            }
        }
        //删除
        protected void btnInfo_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.hfID.Value);
            IES.Portal.Model.Help _help = new IES.Portal.Model.Help { Helpid = id };
            IES.G2S.Portal.BLL.HelpBLL helpbll = new IES.G2S.Portal.BLL.HelpBLL();
            bool result = helpbll.Help_Del(_help);
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