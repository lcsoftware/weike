using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.Resource.Model ;
using IES.CC.Model ;
using IES.CC.OC.Model ;


namespace IES.CC.Model.OC
{
    public class OCInfo
    {
        public   IES.CC.OC.Model.OC oc {get;set;}

        public List<IES.Resource.Model.Attachment> attachmentlist { get; set; }

        public List<OCTeam> octeamlist { get; set; }


    }
}
