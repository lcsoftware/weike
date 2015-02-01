using IES.CC.OC.Model;
using IES.JW.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.CC.OC.Model
{
    public class OCFCInfo
    {
        
        /// <summary>
        /// 翻转课堂基本信息
        /// </summary>
        public OCFC ocfc { get; set; }

        /// <summary>
        /// 翻转课堂学生信息
        /// </summary>
        public List<IES.JW.Model.User > fcUserList { get; set; }

        /// <summary>
        /// 翻转课堂小组信息
        /// </summary>
        public List<IES.CC.Model.PBL.Group> GroupList { get; set; }

        /// <summary>
        /// 论题互动列表
        /// </summary>
        public List<OCFCLive> FCLiveList { get; set; }

        /// <summary>
        /// 线下课堂
        /// </summary>
        public List<OCFCOffline> FCOfflineList { get; set; }
    }
}
