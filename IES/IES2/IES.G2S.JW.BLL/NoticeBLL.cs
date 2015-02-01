using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IES.G2S.JW.DAL;
using IES.JW.Model;
using IES.AOP.G2S;
using IES.G2S.JW.IBLL;
using IES.Cache;
using IES.Security;

namespace IES.G2S.JW.BLL
{
    public class NoticeBLL : INoticeBLL
    {
        #region 列表
        public List<Notice> Notice_List(Notice model, int PageIndex, int PageSize)
        {
            return NoticeDAL.Notice_List(model, PageIndex, PageSize);
        }
        //通知评论列表
        public List<NoticeResponse> NoticeResponse_List(NoticeResponse model, int PageIndex, int PageSize)
        {
            return NoticeDAL.NoticeResponse_List(model, PageIndex, PageSize);
        }

        #endregion

        #region 接收通知
        public List<Notice> Notice_Receive_List(User model)
        {
            return NoticeDAL.Notice_Receive_List(model);
        }
        #endregion

        #region 发送通知
        public bool Notice_Send_List(NoticeObject model)
        {
            return NoticeDAL.Notice_Send_List(model);
        }
        #endregion

        #region 新增通知
        /// <summary>
        /// 新增一条数据
        /// </summary>
        public Notice Notice_ADD(Notice model)
        {
            return NoticeDAL.Notice_ADD(model);
        }

        public NoticeResponse NoticeResponse_ADD(NoticeResponse model)
        {
            return NoticeDAL.NoticeResponse_ADD(model);
        }
        #endregion

        #region 通知详细
        public Notice Notice_Get(Notice model)
        {
            return NoticeDAL.Notice_Get(model);
        }
        #endregion

        #region 更新
        public bool Notice_Upd(Notice model)
        {
            return NoticeDAL.Notice_Upd(model);
        }
        #endregion

        #region 删除
        public bool Notice_Del(Notice model)
        {
            return NoticeDAL.Notice_Del(model);
        }
        #endregion

        #region 批量删除
        /// <summary>
        /// 删除多条数据
        /// </summary>
        public bool Notice_Batch_Del(string IDS)
        {
            return NoticeDAL.Notice_Batch_Del(IDS);
        }
        #endregion

    }
}
