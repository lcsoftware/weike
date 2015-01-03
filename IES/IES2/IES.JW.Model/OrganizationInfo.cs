using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.JW.Model
{
    public class OrganizationInfo:IOrganization
    {
        public int OrganizationID { get; set; }

        public OrganizationCommon organizationcommon { get; set; }
    }
}
