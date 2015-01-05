using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.CC.Model.Score
{
    /// <summary>
    /// 成绩类别加权
    /// </summary>
    [Serializable]
    public class ScoreWeightInfo
    {
        public ScoreWeight scoreweight { get; set; }  //成绩类别权重
        public ScoreType scoretype { get; set; }  //成绩类别
    }
}
 