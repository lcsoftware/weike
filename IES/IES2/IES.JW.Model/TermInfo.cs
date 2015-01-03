using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.JW.Model
{
    public class TermInfo
    {
        /// <summary>
        /// 学年类别名称
        /// </summary>
        public string termTypeName { get; set; }

        /// <summary>
        /// 校历基本属性
        /// </summary>
        public Term term { get; set; }

    }
}
