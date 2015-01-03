using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.CC.OC.Model
{
    //网络注册班级
    [Serializable]
    public partial class OCClassRegInfo
    {
        public OCClassRegInfo()
        {

        }
        public OCClass OCClass { get; set; }
        public int StudentCount { get; set; }
        public string TeacherInfo { get; set; }
        public string TeamInfo { get; set; }
        public int LineStudent { get; set; }
        public int OffLineStudent { get; set; }
    }
}
