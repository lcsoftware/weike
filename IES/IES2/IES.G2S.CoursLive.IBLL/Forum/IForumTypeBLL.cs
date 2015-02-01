using IES.CC.Forum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.CoursLive.IBLL.Forum
{
   
    /// <summary>
    /// 论坛版块
    /// xuwei
    /// 2015年1月7日18:36:05
    /// </summary>
    public interface IForumTypeBLL
    { 
        #region 列表
        /// <summary>
        /// 论坛版块列表
        /// xuwei
        /// 2015年1月7日18:37:56
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<ForumType> ForumType_List(ForumType model);
         #endregion

        #region 新增
        /// <summary>
        /// 新增论坛版块
        /// xuwei
        /// 2015年1月8日13:16:34
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ForumType ForumType_ADD(ForumType model); 
        #endregion

        #region 删除
        bool ForumType_Del(ForumType model); 
        #endregion

        #region 对象更新
        /// <summary>
        /// 对象更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool ForumType_Upd(ForumType model); 
        #endregion
    } 
}
