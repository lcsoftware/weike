using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IES.JW.Model
{
    public class ClassCommon:IClass
    {
        public int ClassID { get; set; }

        /// <summary>
        /// 行政班基本信息
        /// </summary>
        public Class classs {get;set;}

        /// <summary>
        /// 学生列表
        /// </summary>
        public List<Classroom> classroom { get; set; }
    }
}
