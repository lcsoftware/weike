using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.CC.OC.Model
{
    [Serializable]
    public partial class OcTeamFunctionClass
    {
        public OcTeamFunctionClass()
        { }
        #region Model
        public int OCClassID { get; set; }
        public string TeachingClassName { get; set; }
        public int StudentCount { get; set; }
        public string TeamInfo { get; set; }
        public bool IsSelected { get; set; }

        #endregion
    }
}
