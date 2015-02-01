using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IES.Cache;
using IES.Security;
using IES.Service;


namespace Admin.Views.Share
{
    public partial class Nav : System.Web.UI.UserControl
    {
        public IES.JW.Model.Menu parentmenu;
        public IES.JW.Model.Menu currentmenu;

        protected void Page_Load(object sender, EventArgs e)
        {
             parentmenu = IES.Service.AuService.ParentMenu(Request.QueryString["PID"]);
             currentmenu = IES.Service.AuService.CurrentMenu(Request.QueryString["PID"]);
        }
    }
}