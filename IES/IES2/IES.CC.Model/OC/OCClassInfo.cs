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

        public OCClass OcClass { get; set; }
        public List<OCClassStudent> OcClassStudent { get; set; }
    
    }

    [Serializable]
    public partial class OCTeamDropdownList
    {
        public int OCClassID { get; set; }
        public int OCID { get; set; }
        public int Role { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public bool IsSelected { get; set; }
    }

}
