using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.G2S.OC.IBLL.Team;
using IES.CC.OC.Model;
using IES.G2S.OC.DAL.Team;
namespace IES.G2S.OC.BLL.Team
{
    public class OCTeamBLL : IOCTeamBLL
    {

        #region IOCTeam 成员


        #region  列表

        public List<OCTeamInfo> OCTeam_Get(int OCID)
        {
            return OCTeamDAL.OCTeam_Get(OCID);
        }
        #endregion
        #region  详细信息

        public OCTeamInfo OCTeam_Get(int OCID, int UserID)
        {
            return OCTeamDAL.OCTeam_Get(OCID, UserID);
        }
        #endregion
        #region  新增
        public OCTeam OCTeam_ADD(OCTeam model)
        {
            return OCTeamDAL.OCTeam_ADD(model);
        }

        public OCTeamInfo OCTeam_ADD(OCTeamInfo model)
        {
            return OCTeamDAL.OCTeam_ADD(model);
        }

        #endregion
        #region  对象更新
        public bool OCTeam_Role_Upd(int TeamID, int Role)
        {
            return OCTeamDAL.OCTeam_Role_Upd(TeamID, Role);
        }
        public bool OCTeam_Brief_Upd(int TeamID, int Brief)
        {
            return OCTeamDAL.OCTeam_Brief_Upd(TeamID, Brief);
        }
        public bool OCTeam_Status_Upd(int TeamID, int Status)
        {
            return OCTeamDAL.OCTeam_Status_Upd(TeamID, Status);
        }
        public bool OCTeam_Upd(OCTeamInfo model)
        {
            return OCTeamDAL.OCTeam_Upd(model);
        }
        #endregion
        #region 单个批量更新
        #endregion

        #region 属性批量操作
        #endregion
        #region 删除

        public bool OCTeam_Del(OCTeam model)
        {
            return OCTeamDAL.OCTeam_Del(model);
        }

        public bool OCTeam_Del(int OCID, int UserID)
        {
            return OCTeamDAL.OCTeam_Del(OCID, UserID);
        }
        #endregion



        #endregion



    }
}
