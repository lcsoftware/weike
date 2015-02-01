using IES.CC.OC.Model;
using IES.G2S.JW.BLL;
using IES.G2S.JW.IBLL;
using IES.G2S.OC.BLL.FC;
using IES.G2S.OC.IBLL.FC;
using IES.JW.Model;
using IES.Security;
using IES.Service;
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
        public static List<OCFC> OCFC_List(int OCID)
        {
            string userid = IESCookie.GetCookieValue("ies");
            IES.JW.Model.User user = new IES.JW.Model.User { UserID = Int32.Parse(userid) };
            user = UserService.User_Get(user);
            
            IFCBLL fcbll = new FCBLL();
            return fcbll.OCFC_List(OCID,user.UserID);
        }

        [WebMethod]
        public static OCFCInfo OCFCInfo_Get(int FCID)
        {
            IFCBLL fcbll = new FCBLL();
            return fcbll.OCFCInfo_Get(FCID);
        }

        [WebMethod]
        public static int OCFC_ADDorEdit(OCFC fc)
        {
            IFCBLL fcbll = new FCBLL();
            return fcbll.OCFC_ADDorEdit(fc);
        }

        [WebMethod]
        public static List<OCFCFile> OCFCFile_List(int fcid)
        {
            OCFC fc = new OCFC();
            fc.FCID = fcid;
            IFCBLL fcbll = new FCBLL();
            return fcbll.OCFCFile_List(fc);
        }

        [WebMethod]
        public static List<OCFCLive> OCFCLive_List(int fcid)
        {
            OCFC fc = new OCFC();
            fc.FCID = fcid;
            IFCBLL fcbll = new FCBLL();
            return fcbll.OCFCLive_List(fc);
        }

        [WebMethod]
        public static OCFC OCFC_Get(int fcid)
        {
            OCFC fc = new OCFC();
            fc.FCID = fcid;
            IFCBLL fcbll = new FCBLL();
            return fcbll.OCFC_Get(fc);
        }

        [WebMethod]
        public static Boolean OCFCFile_Del(OCFCFile file)
        {
            IFCBLL fcbll = new FCBLL();
            return fcbll.OCFCFile_Del(file);
        }

        [WebMethod]
        public static Boolean OCFCLive_Del(OCFCLive live)
        {
            IFCBLL fcbll = new FCBLL();
            return fcbll.OCFCLive_Del(live);
        }

        [WebMethod]
        public static Boolean OCFCFile_Must(OCFCFile file)
        {
            IFCBLL fcbll = new FCBLL();
            return fcbll.OCFCFile_Must(file);
        }
    }
}