using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.Portal.Model;

namespace IES.G2S.Portal.IBLL
{
    public interface INewsBLL
    {
        #region 列表
        List<News> News_List(News model, int PageIndex, int PageSize);

        #endregion

        #region  新增
        News News_ADD(News model);

        #endregion

        #region 删除
        bool News_Del(News model);

        #endregion

        #region 批量删除
        bool News_Batch_Del(string IDS);

        #endregion

        #region 更新

        bool News_Upd(News model);

        #endregion

        #region 详细信息
        News News_Get(News model);

        #endregion
    }
}
