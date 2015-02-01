using IES.JW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.JW.IBLL
{
    public interface IOrganizationBLL
    {
        #region  列表

        List<Organization> Organization_List(Organization model);

        #endregion

        #region  详细信息
        #endregion

        #region 新增

        Organization Organization_Edit(Organization model);

        #endregion

        #region 内容更细

        bool Organization_Upd(Organization model);

        #endregion

        #region 删除

        bool Organization_Del(Organization model);

        #endregion

        /// <summary>
        /// 取消删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Organization_CancelDel(Organization model);

        /// <summary>
        /// 移动组织机构
        /// </summary>
        /// <param name="SelfID"></param>
        /// <param name="OptionID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        bool Organization_Move(int SelfID, int OptionID, string type);
    }
}
