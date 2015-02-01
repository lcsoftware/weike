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


        /// <summary>
        /// 获取在线课程教学团队列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        List<OCTeam> OCTeam_List(int OCID);
        /// <summary>
        /// 获取在线课程教学团队教学班列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        List<OCTeamClass> OCTeamClass_List(OCTeamClass model);

        #endregion

        #region 详细信息
        #endregion

        #region  新增
        //添加团队主讲教师
        OCTeam OCTeam_ADD(OCTeam model);
        //添加团队成员
        OCTeamInfo OCTeam_ADD(OCTeamInfo model);
        //更新团队角色
        #endregion

        #region 对象更新
        bool OCTeam_Role_Upd(OCTeam model);
        //更新团队简介
        bool OCTeam_Brief_Upd(OCTeam model);
        bool OCTeam_Status_Upd(int OCID, OCTeam model);

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
        #endregion


    }
}
