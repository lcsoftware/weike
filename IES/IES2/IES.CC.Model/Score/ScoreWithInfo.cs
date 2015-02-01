using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.CC.Model.Score
{
    /// <summary>
    /// 徐卫
    /// 单个成绩详细信息
    /// 2014年12月31日18:10:41
    /// </summary>
    [Serializable]
    public class ScoreWithInfo
    {
        public int TestID { get; set; }  //成绩编号
        public string UserNo { get; set; }  //学号
        public string UserName { get; set; }  //学生姓名
        public int ClassID { get; set; }  //行政班编号
        public string ClassName { get; set; }  //行政班名称
        public int TeachingClassID { get; set; }  //教学班编号
        public string TeachingClassName { get; set; }  //教学班名称
        public float Score { get; set; }  //学生成核
        public float AvgScore { get; set; }   //平均分
        public int RowsCount { get; set; } //总行数
    }
}
