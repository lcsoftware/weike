using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  IES.CC.OC.Model
{
    /// <summary>
    /// 课程通知对象详细信息
    /// </summary>
    public class OCNoticeInfo
    {
        public OCNotice ocnotice { get; set; }
        public List<OCNoticeObj> ocnoticeobjlist { get; set; }

    }
}
