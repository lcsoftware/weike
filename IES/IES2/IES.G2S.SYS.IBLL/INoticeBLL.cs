using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.SYS.Model;

namespace IES.G2S.SYS.IBLL
{
    public interface INoticeBLL
    {
        #region 列表
        List<Notice> Notice_List(Notice model, int PageIndex, int PageSize);
        #endregion

        #region 接收通知
        List<Notice> Notice_Receive_List(User model);

        #endregion

        #region 发送通知
        bool Notice_Send_List(NoticeObject model);
        #endregion

        #region 新增通知
        /// <summary>
        /// 新增一条数据
        /// </summary>
        Notice Notice_ADD(Notice model);
   
        #endregion

        #region 通知详细
        List<Notice> Notice_Get(Notice model);

        #endregion

        #region 更新
        bool Notice_Upd(Notice model);
        #endregion

        #region 删除
        bool Notice_Del(Notice model);
        #endregion


    }
}
