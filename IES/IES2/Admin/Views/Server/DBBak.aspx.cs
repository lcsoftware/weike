using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.Server
{
    public partial class DBBak : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                AspNetPager1.PageSize = 20;
                this.BeginTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
                this.EndTime.Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")).AddYears(1).ToShortDateString();
                DataBinder(1);
            }
        }
        private void DataBinder(int pageindex)
        {
            //Repeater1.DataSource = null;
            //Repeater1.DataBind();
        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            DataBinder(AspNetPager1.CurrentPageIndex);
        }
    }
}