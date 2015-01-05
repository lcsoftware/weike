using IES.CC.OC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using IES.G2S.OC.BLL.Team;
namespace App.G2S.DataProvider
{
    public partial class TeamProvider : System.Web.UI.Page
    {
        [WebMethod]
        public static List<OCTeamInfo> GetTeamList(int OCID)
        {
            OCTeamBLL oCTeamBLL = new OCTeamBLL();
            return oCTeamBLL.OCTeam_Get(OCID);
        }

        [WebMethod]
        public static OCTeamInfo TeamInfo(int OCID, int UserID)
        {
            OCTeamBLL oCTeamBLL = new OCTeamBLL();
            return oCTeamBLL.OCTeam_Get(OCID, UserID);
        }

        [WebMethod]
        public static OCTeam TeamAdd(OCTeam motal)
        {
            OCTeamBLL oCTeamBLL = new OCTeamBLL();
            return oCTeamBLL.OCTeam_ADD(motal);
        }

        [WebMethod]
        public static bool OCTeam_Del(OCTeam motal)
        {
            OCTeamBLL oCTeamBLL = new OCTeamBLL();
            return oCTeamBLL.OCTeam_Del(motal);
            //return true;

        }
        [WebMethod]
        public static bool OCTeam_Del(int OCID, int UserID)
        {
            OCTeamBLL oCTeamBLL = new OCTeamBLL();
            return oCTeamBLL.OCTeam_Del(OCID, UserID);
        }



    }
}