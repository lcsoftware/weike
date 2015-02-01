using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.CC.Test.Model
{
    /// <summary>
    /// 学生答卷和教师批阅的详细信息
    /// </summary>
   public   class TestUserInfo
   {
       public Test test { get; set; }

       public TestPaper testpaper { get; set; }

       public List<TestExercise> testexerciselist { get; set; }

       public List<TestAnswer> testAnswerlist { get; set; }

       public TestUser testuser { get; set; }
   }
}
