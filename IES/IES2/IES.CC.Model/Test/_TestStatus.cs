using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IES.CC.Test.Model
{
    public class _TestStatus
    {
        /// <summary>
        /// 测试编号
        /// </summary>
        public int TestID { get; set; }

        /// <summary>
        /// 本次测试的习题总数
        /// </summary>
        public int  ExerciseCount  {get;set ;}



        /// <summary>
        /// 提交状态1.未完成, 10 暂存  20 已提交  23 批阅完成  30 已发放
        /// </summary>
        public int Status { get; set; }


        /// <summary>
        /// 迟交状态
        /// </summary>
        public int IsDelay { get; set; }


        /// <summary>
        /// 用户数量
        /// </summary>
        public int UserNum { get; set; }

    }
}
