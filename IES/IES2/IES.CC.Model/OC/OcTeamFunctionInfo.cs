using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.CC.OC.Model
{
    [Serializable]
    public partial class OcTeamFunctionInfo
    {
        public OcTeamFunctionInfo()
        { }
        #region model
        public OCTeam OCTeam { get; set; }
        public List<OcTeamFunctionClass> OcTeamFunctionClass { get; set; }
        public List<OcTeamFunctionModule> OcTeamFunctionModule { get; set; }
  
        #endregion
    }
}
