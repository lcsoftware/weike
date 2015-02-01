using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using IES.JW.Model;
using IES.Cache;
using IES.Security;
using IES.G2S.JW.BLL;
using IES.Service;

namespace App.AngularMvc.DataProvider.User
{
    public partial class UserProvider : System.Web.UI.Page
    {
        [WebMethod]
        public static List<IES.JW.Model.Menu> Menu_Top_List()
        {
            string userid = IESCookie.GetCookieValue("ies");
            IES.JW.Model.User user = new IES.JW.Model.User { UserID = Int32.Parse(userid) };
            user = UserService.User_Get(user);
            List<IES.JW.Model.Menu> topmeunlist = AuService.Menu_Top_List(2);
            return topmeunlist;
        }
    }
}