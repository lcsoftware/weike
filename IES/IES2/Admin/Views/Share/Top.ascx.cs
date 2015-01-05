using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IES.SYS.Model;
using IES.Cache;
using IES.Security;
using IES.G2S.SYS.BLL;
using IES.Service;
using Admin;

namespace Admin.Views.Share
{
    public partial class Top : System.Web.UI.UserControl
    {
         
        protected void Page_Load(object sender, EventArgs e)
        {

            List<IES.SYS.Model.Menu> menulist = AuService.Menu_Top_List(1);
            Repeater1.DataSource = menulist;
            Repeater1.DataBind();

            List<IES.SYS.Model.Menu> dropmenulist = AuService.Menu_UserDropDown_List();
            Repeater2.DataSource = dropmenulist;
            Repeater2.DataBind();


        }

        

    }
}