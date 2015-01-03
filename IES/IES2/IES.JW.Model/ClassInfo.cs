using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.JW.Model
{
    public class ClassInfo : IClass
    {
        /// <summary>
        /// 获取行政班的基本信息
        /// </summary>
        public int ClassID { get; set; }

        /// <summary>
        /// 行政班的基本信息
        /// </summary>
        public ClassCommon classcommon { get; set; }
    }
}
