using IES.CC.OC.Model;
using IES.G2S.OC.BLL.FC;
using IES.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App.AngularMvc.DataProvider.OC.FC
{
    public partial class FCProvider : System.Web.UI.Page
    {
        [WebMethod]
        public static int OCFC_ADD(OCFC fc)
        {
            return fc.FCID;
        }

        [WebMethod]
        public static List<OCFC> OCFC_List(int OCID)
        {
            //string userid = IESCookie.GetCookieValue("ies");
            //IES.SYS.Model.User user = new IES.SYS.Model.User { UserID = Int32.Parse(userid) };
            //user = UserService.User_Get(user);
            //List<OCFC> fclist = new List<OCFC>();
            //FCBLL fcbll = new FCBLL();
            //fclist = fcbll.OCFC_Get(OCID, user.UserID);
            //return fclist;
            FCBLL fcbll = new FCBLL();
            return fcbll.OCFC_List(OCID,0);
        }

        [WebMethod]
        public static List<OCFCInfo> OCFCInfo_List(int OCID)
        {
            string userid = IESCookie.GetCookieValue("ies");
            IES.SYS.Model.User user = new IES.SYS.Model.User { UserID = Int32.Parse(userid) };
            //user = 
            List<OCFCInfo> fcinfolist = new List<OCFCInfo>();
            FCBLL fcbll = new FCBLL();
            fcinfolist = fcbll.OCFCInfo_List(OCID,0);
            return fcinfolist;
        }
    }
}