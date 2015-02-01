using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.CC.Test.Model
{
  public   class TestInfo
    {
      public List<Test> test { get; set; }


      /// <summary>
      /// 提交状态
      /// </summary>
      public List<_TestStatus> _teststatuslist { get; set; }


      /// <summary>
      /// 成绩分布
      /// </summary>
      public List<_TestScoreStatistics> _testScoreStatisticslist { get; set; }


    }
}
