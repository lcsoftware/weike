using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.Config
{
    public partial class SelTeacher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder(1);
                AspNetPager1.PageSize = 10;
            }
        }
        private void DataBinder(int pageindex)
        {
            Repeater2.DataSource = new List<IES.JW.Model.User>();
            Repeater2.DataBind();
        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            DataBinder(1);
        }
        protected void SelTeach_Click(object sender, EventArgs e)
        {
            DataBinder(1);
        }
    }
}