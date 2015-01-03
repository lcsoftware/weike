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
    public class LinkBLL : ILinkBLL
    {
        #region 列表
        public List<Link> Link_List(Link model, int PageIndex, int PageSize)
        {
            return LinkDAL.Link_List(model, PageIndex, PageSize);
        }
        #endregion

        #region  新增
        public Link Link_ADD(Link model)
        {
            return LinkDAL.Link_ADD(model);
        }

        #endregion

        #region 删除
        public bool Link_Del(Link model)
        {
            return LinkDAL.Link_Del(model);
        }

        #endregion

        #region 更新

        public bool Link_Upd(Link model)
        {
            return LinkDAL.Link_Upd(model);
        }

        #endregion
    }
}