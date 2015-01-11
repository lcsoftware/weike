using IES.CC.OC.Model;
using IES.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App.Resource.DataProvider.Shared
{
    public partial class ContentProvider : System.Web.UI.Page
    {
        [WebMethod]
        public static IList<OC> User_OC_List()
        {
            var user = UserService.CurrentUser;
            return UserService.User_OC_List(user);
        }
    }
}