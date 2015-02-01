using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.G2S.Portal.DAL;
using IES.Portal.Model;
using IES.G2S.Portal.IBLL;

namespace IES.G2S.Portal.BLL
{
    public class HelpBLL : IHelpBLL
    {

        #region 列表
        public List<Help> Help_List(Help model, int PageIndex, int PageSize)
        {
            return HelpDAL.Help_List(model, PageIndex, PageSize);
        }
        #endregion

        #region  新增
        public Help Help_ADD(Help model)
        {
            return HelpDAL.Help_ADD(model);
        }



        #endregion

        #region 删除
        public bool Help_Del(Help model)
        {
            return HelpDAL.Help_Del(model);
        }

        #endregion

        #region 批量删除
        public bool Help_Batch_Del(string IDS)
        {
            return HelpDAL.Help_Batch_Del(IDS);
        }

        #endregion

        #region 更新

        public bool Help_Upd(Help model)
        {
            return HelpDAL.Help_Upd(model);
        }

        #endregion

        #region 详细信息
        public Help Help_Get(Help model)
        {
            return HelpDAL.Help_Get(model);
        }
        #endregion
    }
}
