using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.JW.Course
{
    public partial class Course : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AspNetPager1.PageSize = 10;
                DataBinder(1);
            }
        }

        private void DataBinder(int pageindex)
        {
            IES.JW.Model.Course course = new IES.JW.Model.Course { OrganizationID = -1, CourseTypeID = -1, TeachingTypeID = -1, SubjectID1 = -1, SubjectID2 = -1 };
            IES.G2S.JW.BLL.CourseBLL coursebll = new IES.G2S.JW.BLL.CourseBLL();
            string Key = "";
            List<IES.JW.Model.Course> courselist = coursebll.Course_List(Key, course, pageindex, AspNetPager1.PageSize);
            if (courselist.Count > 0)
                AspNetPager1.RecordCount = courselist[0].rowscount;

            Repeater1.DataSource = courselist;
            Repeater1.DataBind();
        }

        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {

            DataBinder(AspNetPager1.CurrentPageIndex);

        }

        protected void Edit(object sender, CommandEventArgs e)
        {
            Response.Redirect("Edit.aspx?id=" + e.CommandArgument.ToString());
        }

        protected void Look_Click(object sender, EventArgs e)
        {
            string Num = this.hideText.Value;
            //Page.ClientScript.RegisterClientScriptBlock(GetType(), "", "alert(" + Num + ")", true);
            AspNetPager1.PageSize = Convert.ToInt32(Num);
            DataBinder(1);
        }
    }
}