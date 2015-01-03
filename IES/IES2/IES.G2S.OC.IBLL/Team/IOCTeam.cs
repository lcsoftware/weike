using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.CC.OC.Model;

namespace IES.G2S.OC.IBLL.Team
{
    public  interface IOCTeam
    {
        //添加团队主讲教师
        int OCTeam_ADD(OCTeam model);
        //添加团队成员
        int OCTeam_ADD(OCTeamInfo model);
        //更新团队角色
        bool OCTeam_Role_Upd(int TeamID, int Role);
        //更新团队简介
        bool OCTeam_Brief_Upd(int TeamID, int Brief);
        /// <summary>
        /// 更新团队成员信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool OCTeam_Upd(OCTeamInfo model);
        /// <summary>
        /// 删除团队成员
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool OCTeam_Del(OCTeam model);
        List<OCTeamInfo> OCTeam_Get(int OCID);
        OCTeamInfo OCTeam_Get(int OCID, int UserID);
    }
}
