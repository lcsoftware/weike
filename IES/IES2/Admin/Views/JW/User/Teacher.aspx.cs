using IES.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.JW.User
{
    public partial class Teacher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AspNetPager1.PageSize = Browse.PageSize;
                DropDownList1.SelectedValue = Browse.PageSize.ToString();
                DataBinder(1);
            }
        }

        private void DataBinder(int pageindex)
        {
            IES.JW.Model.User user = new IES.JW.Model.User { Key = "", Role = 8, modevalue = 32, OrganizationID = -1, SpecialtyID = -1, ClassID = -1, IsLocked = 0, IsRegister = 0 };
            IES.G2S.JW.BLL.UserBLL userbll = new IES.G2S.JW.BLL.UserBLL();
            List<IES.JW.Model.User> userlist = userbll.User_List( user, pageindex, AspNetPager1.PageSize);
            if (userlist.Count > 0)
                AspNetPager1.RecordCount = userlist[0].rowscount;

            Repeater1.DataSource = userlist;
            Repeater1.DataBind();
        }

        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {

            DataBinder(AspNetPager1.CurrentPageIndex);
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            AspNetPager1.PageSize = Int32.Parse(DropDownList1.SelectedValue);
            Browse.SetPageSize(AspNetPager1.PageSize);
            DataBinder(1);
        }
    }
}