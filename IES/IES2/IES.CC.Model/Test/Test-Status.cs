using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.CC.Model.Test
{
    public class Test_Status
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
        /// 本次测试的已批阅数量
        /// </summary>
        public int  Checked  {get;set ;}

        /// <summary>
        /// 本次测试的已提交数量
        /// </summary>
        public int  Submit  {get;set ;}

        /// <summary>
        /// 本次测试的未提交数量
        /// </summary>
        public int  NotSubmit  {get;set ;}


        /// <summary>
        /// 本次测试的迟交数量
        /// </summary>
        public int  DelaySubmit  {get;set ;}

    }
}
