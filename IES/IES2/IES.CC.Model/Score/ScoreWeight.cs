using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.CC.Model.Score
{
    /// <summary>
    /// 徐卫
    /// 2014年12月26日
    /// 成绩加权
    /// </summary>
    public class ScoreWeight
    {
        #region 补充信息
        public string Name { get; set; }  //成绩类别名称 
        #endregion

        #region Model
        public int WeightID { get; set; }  //权重ID
        public int OCID { get; set; }  // 课程ID
        public int TeachingClassID { get; set; }  //教学班ID
        public int ScoreTypeID { get; set; }  //成绩类别ID
        public int JoinNum { get; set; }  //参与次数
        public int Power { get; set; }   //权重系数
        #endregion
    }
}
