using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.CC.Forum.Model;
using IES.G2S.CourseLive.IBLL.Forum;
using IES.G2S.CourseLive.DAL.Forum;
using IES.G2S.CoursLive.IBLL.Forum;

namespace IES.G2S.CourseLive.BLL.Forum
{
    /// <summary>
    /// 论坛版块
    /// xuwei
    /// 2015年1月7日18:39:20
    /// </summary>
    public class ForumTypeBLL : IForumTypeBLL
    {
        #region 列表
        /// <summary>
        /// 论坛版块列表
        /// xuwe 
        /// 2015年1月7日18:39:46
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ForumType> ForumType_List(ForumType model)
        {
            return ForumTypeDAL.ForumType_List(model);
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增论坛版块
        /// xuwei
        /// 2015年1月8日13:18:20
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ForumType ForumType_ADD(ForumType model)
        {
            return ForumTypeDAL.ForumType_ADD(model);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ForumType_Del(ForumType model)
        {
            return ForumTypeDAL.ForumType_Del(model);
        }
        #endregion

        #region 对像更新
        /// <summary>
        /// 对象更新
        /// xuwei
        /// 2015年1月8日19:12:03
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ForumType_Upd(ForumType model)
        {
            return ForumTypeDAL.ForumType_Upd(model);
        }
        #endregion

        #region 详细信息

        /// <summary>
        /// 获取论坛版块详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ForumTypeInfo ForumTypeInfo_Get(ForumType model)
        {
            return ForumTypeDAL.ForumTypeInfo_Get(model);
        }
        #endregion
    }
}
