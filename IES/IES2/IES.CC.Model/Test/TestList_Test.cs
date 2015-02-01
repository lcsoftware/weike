using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.CC.Model.Test
{
    public class TestList_Test 
    {
        #region  补充
        public TestList_Test()
        {
             
            this.DSocreSection = new Dictionary<string, string>();
        }

        public IES.CC.Test.Model.Test test { get; set; }
        /// <summary>
        /// 本次测试的习题总数
        /// </summary>
        public int ExerciseCount { get; set; }
        /// <summary>
        /// 迟交数量
        /// </summary>
        public int DelayCount { get; set; }
        /// <summary>
        /// 未提交数量
        /// </summary>
        public int UncommittedCount { get; set; }
        /// <summary>
        /// 已提交数量
        /// </summary>
        public int submittedCount { get; set; }
        /// <summary>
        /// 已批阅数量
        /// </summary>
        public int MarkingCount { get; set; }
        /// <summary>
        ///  已交/已阅 百分比
        /// </summary>
        public double MarkingPercent { get; set; }


        /// <summary>
        /// 作业分数段的统计
        /// </summary>
        public Dictionary<string, string> DSocreSection { get; set; }

        public string[] DSocreSectionKey {  get { return this.DSocreSection.Keys.ToArray(); } }

        public string[] DSocreSectionValue { get { return this.DSocreSection.Values.ToArray(); } }
        ///
        #endregion
    }
}
