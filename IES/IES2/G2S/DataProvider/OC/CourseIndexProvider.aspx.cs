using IES.CC.OC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using IES.G2S.OC.BLL.Team;
using IES.G2S.JW.BLL;
using IES.JW.Model;
using IES.Common;
namespace App.G2S.DataProvider.OC
{
    public partial class CourseIndexProvider : System.Web.UI.Page
    {
        /// <summary>
        /// 获取教学团队信息列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<OCTeam> OCTeam_List(int OCID)
        {
            OCTeamBLL oCTeamBLL = new OCTeamBLL();
            return oCTeamBLL.OCTeam_List(OCID);
        }
        /// <summary>
        /// 获取教师列表
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<IES.JW.Model.Teacher> Teacher_List(Teacher teacher, int pageindex, int pagesize)
        {
            UserBLL userbll = new UserBLL();
            return userbll.Teacher_List(teacher, pageindex, pagesize);
        }


        #region 详细信息

        [WebMethod]
        public static OCTeamInfo TeacherInfo(int UserID)
        {
            OCTeamBLL oCTeamBLL = new OCTeamBLL();
            return oCTeamBLL.TeacherInfo_Get(UserID);
        }

        [WebMethod]
        public static OcTeamFunctionInfo GetOcTeamFunctionInfo(int OCID, int UserID)
        {
            OCTeamBLL oCTeamBLL = new OCTeamBLL();
            return oCTeamBLL.OCTeam_Class_Function_Get(OCID, UserID);
        }

        #endregion

        #region  新增
        /// <summary>
        /// 添加教学团队成员信息
        /// </summary>
        /// <param name="motal"></param>
        /// <returns></returns>
        [WebMethod]
        public static OCTeam OCTeam_ADD(OCTeam octeam)
        {
            OCTeamBLL oCTeamBLL = new OCTeamBLL();
            return oCTeamBLL.OCTeam_ADD(octeam);
        }
        [WebMethod]
        public static OCTeam OCTeam_Class_Function_Save(OcTeamFunctionInfo octeamfunctioninfo)
        {
            OCTeamBLL oCTeamBLL = new OCTeamBLL();
            return oCTeamBLL.OCTeam_Class_Function_Save(octeamfunctioninfo);
        }
        #endregion

        #region 对象更新
        [WebMethod]
        public static bool OCTeam_Brief_Upd(OCTeam octeam)
        {
            OCTeamBLL oCTeamBLL = new OCTeamBLL();
            return oCTeamBLL.OCTeam_Brief_Upd(octeam);
            //return true;

        }
        [WebMethod]
        public static bool OCTeam_IsLocked_Upd(OCTeam octeam)
        {
            OCTeamBLL oCTeamBLL = new OCTeamBLL();
            return oCTeamBLL.OCTeam_IsLocked_Upd(octeam);

        }

        #endregion

        #region 删除

        /// <summary>
        /// 删除教学团队成员by TeamID
        /// </summary>
        /// <param name="motal"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool OCTeam_Del(OCTeam motal)
        {
            OCTeamBLL oCTeamBLL = new OCTeamBLL();
            return oCTeamBLL.OCTeam_Del(motal);
            //return true;

        }
        #endregion
    }
}