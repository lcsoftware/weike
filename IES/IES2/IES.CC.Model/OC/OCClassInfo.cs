using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.CC.OC.Model
{
    [Serializable]
    public partial class OCClassInfo
    {
        public int OCID { get; set; }
        public int TeachingClassID { get; set; }
        public string RegNum { get; set; }
        public int RegStatus { get; set; }
        public string ClassName { get; set; }
        public int StartWeek { get; set; }
        public int EndWeek { get; set; }
        public int StudentCount { get; set; }
        public string Teachers { get; set; }
        public string Teams { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

    }
}
