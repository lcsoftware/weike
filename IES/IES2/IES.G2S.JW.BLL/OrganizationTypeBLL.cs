using IES.G2S.JW.DAL;
using IES.G2S.JW.IBLL;
using IES.JW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.JW.BLL
{

    /// <summary>
    /// 组织机构类别
    /// </summary>
    public class OrganizationTypeBLL : IOrganizationTypeBLL
    {
        /// <summary>
        /// 组织机构类别列表
        /// </summary>
        /// <returns></returns>
        public List<OrganizationType> OrganizationType_List()
        {
            return OrganizationTypeDAL.OrganizationType_List();
        }
    }
}
