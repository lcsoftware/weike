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
        public List<ShortOranization> Organization_S_List(ShortOranization model)
        {
            return OrganizationDAL.Organization_S_List(model);
        }
        #endregion

        #region 新增
        public Organization Organization_Edit(Organization model)
        {
            return OrganizationDAL.Organization_Edit(model);
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

        #region  获取全部信息

        public List<Organization> NewsOrganization_List()
        {
            return OrganizationDAL.NewsOrganization_List();
        }

        #endregion

        /// <summary>
        /// 取消删除组织机构
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Organization_CancelDel(Organization model)
        {
            return OrganizationDAL.Organization_CancelDel(model);
        }

        /// <summary>
        /// 移动组织机构
        /// </summary>
        /// <param name="SelfID"></param>
        /// <param name="OptionID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool Organization_Move(int SelfID, int OptionID, string type)
        {
            return OrganizationDAL.Organization_Move(SelfID, OptionID, type);
        }
    }
}
