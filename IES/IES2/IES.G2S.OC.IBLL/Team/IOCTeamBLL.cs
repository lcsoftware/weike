using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.CC.OC.Model;

namespace IES.G2S.OC.IBLL.Team
{
    public interface IOCTeamBLL
    {

        #region  列表
        List<OCTeamInfo> OCTeam_Get(int OCID);
        #endregion

        #region 详细信息

        OCTeamInfo OCTeam_Get(int OCID, int UserID);
        #endregion

        #region  新增
        //添加团队主讲教师
        OCTeam OCTeam_ADD(OCTeam model);
        //添加团队成员
        OCTeamInfo OCTeam_ADD(OCTeamInfo model);
        //更新团队角色
        #endregion

        #region 对象更新
        bool OCTeam_Role_Upd(int TeamID, int Role);
        //更新团队简介
        bool OCTeam_Brief_Upd(int TeamID, int Brief);
        /// <summary>
        /// 更新团队成员信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool OCTeam_Upd(OCTeamInfo model);

        #endregion

        #region 单个批量更新
        #endregion

        #region 属性批量操作
        #endregion
        #region 删除
        /// <summary>
        /// 删除团队成员
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool OCTeam_Del(OCTeam model);
        /// <summary>
        /// 删除团队成员
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool OCTeam_Del(int OCID, int UserID);
        #endregion

    }
}
