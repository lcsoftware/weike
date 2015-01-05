using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.Portal.Model;

namespace IES.G2S.Portal.IBLL
{
    public interface IHelpBLL
    {
        #region 列表
        List<Help> Help_List(Help model, int PageIndex, int PageSize);
    
        #endregion

        #region  新增
        Help Help_ADD(Help model);

        #endregion

        #region 删除
        bool Help_Del(Help model);

        #endregion

        #region 更新

        bool Help_Upd(Help model);

        #endregion
    }
}
