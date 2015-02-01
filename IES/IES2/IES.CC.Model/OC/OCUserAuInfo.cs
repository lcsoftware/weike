using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.JW.Model;

namespace IES.CC.OC.Model
{

    /// <summary>
    /// 用户课程权限类
    /// </summary>
    public class OCUserAuInfo
    {

        /// <summary>
        /// 用户授权的教学班列表
        /// </summary>
        public List<OCTeamClass> octeamclasslist { get; set; }


        /// <summary>
        ///用户授权信息
        /// </summary>
        public List<UserAuInfo> userauinfolist { get; set; }



    }
}
