using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IES.JW.Model
{
    public class OrganizationCommon:IOrganization
    {
        public int OrganizationID { get; set; }

        public Organization organization { get; set; }

        public OrganizationType organizationtype { get; set; }
    }
}
