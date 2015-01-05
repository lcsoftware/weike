using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.Portal.Link
{
    public partial class Link : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                AspNetPager1.PageSize = Convert.ToInt32(Select1.Items[Select1.SelectedIndex].Text);
                DataBinder(1);

            }
        }

        private void DataBinder(int pageindex)
        {
            IES.Portal.Model.Link _link = new IES.Portal.Model.Link ();
            IES.G2S.Portal.BLL.LinkBLL linkbll = new IES.G2S.Portal.BLL.LinkBLL();

            List<IES.Portal.Model.Link> linklist = linkbll.Link_List(_link,pageindex, AspNetPager1.PageSize);
            if (linklist.Count > 0)
            { 
                AspNetPager1.RecordCount = linklist[0].rowscount;
                Repeater1.DataSource = linklist;
                Repeater1.DataBind();
            }
        }
        //删除
        protected void btnInfo_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.hfID.Value);
            IES.Portal.Model.Link _link = new IES.Portal.Model.Link {Linkid=id };
            IES.G2S.Portal.BLL.LinkBLL linkbll = new IES.G2S.Portal.BLL.LinkBLL();
            bool result=linkbll.Link_Del(_link);
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