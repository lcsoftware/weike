using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.CC.OC.Model
{
    public class OCTeamInfo
    {
        /// <summary>
        /// 成员信息
        /// </summary>
        public OCTeam OcTeam { get; set; }

        /// <summary>
        /// 班级授权
        /// </summary>
        public List<OCTeamClass> OcTeamClass { get; set; }
       
        ///
        //功能授权
        //public List<OCTeamClassAuInfo> octeamclassinfo { get; set; }



    }
}
