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

namespace Admin.Views.Share
{
    public partial class SubMenuNav : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string PID = "A11";
            if (Request.QueryString["PID"] != null)
            {
                PID = Request.QueryString["PID"];
            }
            if( PID.Length >= 3 )
            { 
                List<IES.SYS.Model.Menu> menulist = AuService.Menu_Left_List(PID.Substring(0,3), 1);
                Repeater1.DataSource = menulist;
            
                Repeater1.DataBind();
            }
        }
    }
}