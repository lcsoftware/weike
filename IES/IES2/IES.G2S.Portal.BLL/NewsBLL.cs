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
            return  NewsDAL.News_List(model, PageIndex, PageSize);
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

        #region 批量删除
        public bool News_Batch_Del(string IDS)
        {
            return NewsDAL.News_Batch_Del(IDS);
        }

        #endregion

        #region 更新

        public bool News_Upd(News model)
        {
            return NewsDAL.News_Upd(model);
        }

        #endregion

        #region 详细信息
        public News News_Get(News model)
        {
            return NewsDAL.News_Get(model);         
        }
        #endregion

        #region 新闻公告所属板块
        public List<NewsSection> NewsSection_List()
        {
            return NewsDAL.NewsSection_List();
        }
        #endregion
    }
}