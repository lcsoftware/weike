using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.CC.Model.Test
{
    /// <summary>
    ///测试成绩分布图
    /// </summary>
    public  class Test_ScoreStatistics
    {
        /// <summary>
        /// 测试的编号
        /// </summary>
        public int TestID { get; set; }

        /// <summary>
        /// 分数段
        /// </summary>
        public string SocreSection { get; set; }


        /// <summary>
        /// 分数段用户数量
        /// </summary>
        public string UserCount { get; set; }



    }
}
