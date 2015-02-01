using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.CC.Model.Statistics
{
    [Serializable]
    public partial class StuTrajector
    {
        #region 补充信息

        #endregion

        public StuTrajector()
        { }

        #region Model
        /// <summary>
        /// 0 本月 1 本学期 2 本年
        /// </summary>
        public int TimeType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 0轨迹，1 轨迹明细
        /// </summary>
        public int SearchType { get; set; }
        /// <summary>
        /// 0互动访问次数  1 学生在线统计 2 学生活跃度
        /// </summary>
        public int SourseType { get; set; }
        /// <summary>
        /// -1 不限  0 月均值 1 周均值 2 日均值
        /// </summary>
        public int AvgType { get; set; }
        #endregion
    }
}
