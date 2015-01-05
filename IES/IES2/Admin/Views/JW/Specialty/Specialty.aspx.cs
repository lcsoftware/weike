using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.JW.Specialty
{
    public partial class Specialty : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AspNetPager1.PageSize = 20;
                DataBinder(1);
            }
        }

        private void DataBinder(int pageindex)
        {
            IES.JW.Model.Specialty specialty = new IES.JW.Model.Specialty { SpecialtyName = string.Empty, SpecialtyNo = string.Empty };
            IES.G2S.JW.BLL.SpecialtyBLL specialtybll = new IES.G2S.JW.BLL.SpecialtyBLL();

            List<IES.JW.Model.Specialty> specialtylist = specialtybll.Specialty_List(specialty, pageindex, AspNetPager1.PageSize);
            if (specialtylist.Count > 0)
                AspNetPager1.RecordCount = specialtylist[0].rowscount;

            Repeater1.DataSource = specialtylist;
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
            string Num = Request["sText"];
            AspNetPager1.PageSize = Convert.ToInt32(Num);
            DataBinder(1);
        }


    }
}