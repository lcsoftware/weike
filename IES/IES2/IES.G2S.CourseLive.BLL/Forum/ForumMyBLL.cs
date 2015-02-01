using IES.CC.Forum.Model;
using IES.G2S.CourseLive.Forum.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.CourseLive.BLL.Forum
{
    /// <summary>
    /// 我关注的
    /// </summary>
    public class ForumMyBLL
    {
        #region 对象更新
        /// <summary>
        /// 为论坛主题或回复点赞  
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ForumMy_IsGood_Upd(ForumMy model)
        {
            return ForumMyDAL.ForumMy_IsGood_Upd(model);
        }
        #endregion
    }
}
