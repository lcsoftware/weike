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
    public class NewsBLL : INewsBLL
    {
        #region 列表
        public List<News> News_List(News model, int PageIndex, int PageSize)
        {
            return NewsDAL.News_List(model, PageIndex, PageSize);
        }
        #endregion

        #region  新增
        public News News_ADD(News model)
        {
            return NewsDAL.News_ADD(model);
        }



        #endregion

        #region 删除
        public bool News_Del(News model)
        {
            return NewsDAL.News_Del(model);
        }

        #endregion

        #region 更新

        public bool News_Upd(News model)
        {
            return NewsDAL.News_Upd(model);
        }

        #endregion
    }
}