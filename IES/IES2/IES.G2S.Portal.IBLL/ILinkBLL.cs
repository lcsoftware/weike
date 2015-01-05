using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.Portal.Model;

namespace IES.G2S.Portal.IBLL
{
    public interface ILinkBLL
    {
        #region 列表
        List<Link> Link_List(Link model, int PageIndex, int PageSize);
    
        #endregion

        #region  新增
        Link Link_ADD(Link model);

        #endregion

        #region 删除
        bool Link_Del(Link model);
 
        #endregion

        #region 更新

        bool Link_Upd(Link model);
    
        #endregion
    }
}
