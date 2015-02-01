using IES.CC.Forum.Model;
using IES.G2S.CourseLive.DAL.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.CourseLive.BLL.Forum
{
    /// <summary>
    /// 帖子回复
    /// </summary>
    public class ResponseBLL
    {
        #region 列表
        /// <summary>
        /// 回复列表
        /// </summary>
        /// <returns></returns>
        public ForumResponseInfo ForumResponseInfo_List(ForumResponse model, int PageIndex = 1, int PageSize = 10)
        {
            return ResponseDAL.ForumResponseInfo_List(model, PageIndex, PageSize);
        }
        #endregion

        #region 新增
        /// <summary>
        /// 添加论题回复
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ForumResponse ForumResponse_ADD(ForumResponse model)
        {
            return ResponseDAL.ForumResponse_ADD(model);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除回复
        /// </summary>
        /// <returns></returns>
        public bool ForumResponse_Del(int ResponseID)
        {
            return ResponseDAL.ForumResponse_Del(ResponseID);
        }

        #endregion
    }
}
