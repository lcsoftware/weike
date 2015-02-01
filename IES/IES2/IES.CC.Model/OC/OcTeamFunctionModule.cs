using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.CC.OC.Model
{
    [Serializable]
    public partial class OcTeamFunctionModule
    {
        public OcTeamFunctionModule()
        {

        }
        #region Model
        public string ModuleID { get; set; }
        public string Name { get; set; }
        public string ParentID { get; set; }
        public bool IsSelected { get; set; }

        #endregion
    }
}
