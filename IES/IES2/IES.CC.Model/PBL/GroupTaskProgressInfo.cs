using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.CC.Model.PBL
{
    /// <summary>
    /// 小组任务进度实体类
    /// </summary>
    public class GroupTaskProgressInfo
    {
        /// <summary>
        /// 小组编号
        /// </summary>
        public string groupid { get; set; }

        /// <summary>
        /// 小组名称
        /// </summary>
        public string groupname { get; set; }

        /// <summary>
        /// 提交数
        /// </summary>
        public int submitcount { get; set; }

    }
}
