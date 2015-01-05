using IES.CC.OC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App.AngularMvc.DataProvider
{
    public partial class SiteProvider : System.Web.UI.Page
    {
        [WebMethod]
        public static int OCSiteColumn_ADD(OCSiteColumn column) {
            return 1;
        }
    }
}