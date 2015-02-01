using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.JW.Model
{
    /// <summary>
    /// 系统字典表
    /// </summary>
    public   class  Dict
    {
        public int  ID {get ;set ;}

        /// <summary>
        /// 字典中文名
        /// </summary>
        public string  Name {get ;set ;}

        /// <summary>
        /// 字典英文名称
        /// </summary>
        public string NameEn { get; set; }

        /// <summary>
        /// 来源表的字段
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 上级编号
        /// </summary>
        public int  ParentID {get ;set ;}

    }
}
