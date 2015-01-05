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
    public class OrganizationBLL : IOrganizationBLL
    {
        #region  列表
        public List<Organization> Organization_List(Organization model)
        {
            return OrganizationDAL.Organization_List(model);
        }
        #endregion

        #region 新增
        public Organization Organization_ADD(Organization model)
        {
            return OrganizationDAL.Organization_ADD(model);
        }
        #endregion

        #region  对象更新
        public bool Organization_Upd(Organization model)
        {
            return OrganizationDAL.Organization_Upd(model);
        }
        #endregion

        #region  删除
        public bool Organization_Del(Organization model)
        {
            return OrganizationDAL.Organization_Del(model);
        }
        #endregion
    }
}
