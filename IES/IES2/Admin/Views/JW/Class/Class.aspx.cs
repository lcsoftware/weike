using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IES.JW.Model;

namespace Admin.Views.JW.Class
{
    public partial class Class : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                AspNetPager1.PageSize = 20;
                DataBinder(1);
       
                
            }
        }

        private void DataBinder( int pageindex  )
        {
            IES.JW.Model.Class _class = new IES.JW.Model.Class { ClassName = string.Empty, ClassNo = string.Empty };
            IES.G2S.JW.BLL.ClassBLL classbll = new IES.G2S.JW.BLL.ClassBLL();

            List<IES.JW.Model.Class> classlist = classbll.Class_List(_class, 1, pageindex, AspNetPager1.PageSize );
            if (classlist.Count > 0)
                AspNetPager1.RecordCount = classlist[0].rowscount;

            Repeater1.DataSource = classlist;
            Repeater1.DataBind();
        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {

            DataBinder(AspNetPager1.CurrentPageIndex );
            
        }

        protected void Edit(object sender, CommandEventArgs e)
        {
            Response.Redirect( "Edit.aspx?id="+e.CommandArgument.ToString());
        }
    }
}