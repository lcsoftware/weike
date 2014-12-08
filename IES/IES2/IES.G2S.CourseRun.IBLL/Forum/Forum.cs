using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.CC.Forum.Model ;

namespace IES.G2S.CourseRun.IBLL.Forum
{
    public interface Forum
    {
        #region  论坛版块的操作

        public ForumTopicType ForumTopicType_ADD(ForumTopicType model);

        public ForumTopicType ForumTopicType_Upd(ForumTopicType model);

        public bool ForumTopicType_Del( int id );


        /// <summary>
        /// 获取论坛版块
        /// </summary>
        /// <returns></returns>
        public List<ForumTopicType> ForumTopicType_Get(string courseid, string userid);

        #endregion 

        #region 论题列表

        /// <summary>
        /// 获取热门帖子
        /// </summary>
        /// <param name="courseid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<ForumTopic> FormTopic_Hot_Get(string courseid, string userid);

        /// <summary>
        /// 论题查询列表
        /// </summary>
        /// <param name="courseid">课程编号</param>
        /// <param name="userid">用户编号</param>
        /// <param name="scope">0全部，1精华，2我参与的，3我发起的</param>
        /// <param name="key">查询关键字</param>
        /// <param name="status">0 全部， 1已回答 ， 2 未回答</param>
        /// <param name="order">1 最新 ， 2 最热 </param>
        /// <param name="pagesize">分页大小</param>
        /// <param name="page">第几页</param>
        /// <returns></returns>
        public List<ForumTopic> FormTopic_Search(string courseid , int forumyypeid , string userid , int scope, string key, int status, int order, int pagesize, int page);


        #endregion

        #region 论题状态 移动 



        #endregion


    }
}