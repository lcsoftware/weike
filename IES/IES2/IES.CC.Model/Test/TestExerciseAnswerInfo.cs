using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.CC.Test.Model
{
    public class TestExerciseAnswerInfo
    {

        /// <summary>
        /// 测试的基本信息
        /// </summary>
        public Test  test { get; set; }


        /// <summary>
        /// 习题的基本信息
        /// </summary>
        public TestExercise testexercise { get; set; }




        /// <summary>
        /// 参与考试的学生列表
        /// </summary>
        public List<IES.JW.Model.User> userlist { get; set; }

        /// <summary>
        /// 习题的答案列表
        /// </summary>
        public List<TestAnswer> testanswerlist { get; set; }

    }
}
