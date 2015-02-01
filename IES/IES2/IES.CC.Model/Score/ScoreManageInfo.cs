using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.CC.Model.Score
{
    /// <summary>
    /// 成绩管理
    /// 徐卫
    /// 2014年12月31日10:58:01
    /// </summary>
    [Serializable]
    public class ScoreManageInfo
    {
        public int OCID { get; set; }  //课程编号
        public int TermID { get; set; }  //学期编号
        public int TestID { get; set; }  //成绩编号
        public string Name { get; set; }  //成绩名称
        public int ScoreTypeID { get; set; }  //成绩类别编号
        public string ScoreTypeName { get; set; }  //成绩类别名称
        public int JoinStudent { get; set; }  //参与学生总数
        public int C { get; set; }  //60分以下学生人数
        public int B { get; set; }  //60~80分学生人数
        public int A { get; set; }  //80分以上学生人数
        public float AvgScore { get; set; }  //平均分
        public int RowsCount { get; set; }   //行数

    }
}
