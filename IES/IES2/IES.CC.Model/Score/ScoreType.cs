using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.CC.Model.Score
{
    /// <summary>
    /// 成绩类别
    /// xuwei
    /// 2014年12月25日
    /// </summary>
    [Serializable]
    public class ScoreType
    {
        public int ScoreTypeID { get; set; }  //类型ID
        public int OCID { get; set; }  //课程编号
        public string Name { get; set; }  //类型名称
        public int ParentID { get; set; }  //父ID
        public int IsSystem { get; set; }  //是否系统类型
        public int IsSingle { get; set; }  //是否允许添加子级
        public int Status { get; set; }    //状态
    }
}
