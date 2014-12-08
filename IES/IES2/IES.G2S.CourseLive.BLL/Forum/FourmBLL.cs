using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.CC.Forum.Model;
using IES.G2S.CourseLive.IBLL.Forum;
using IES.G2S.CourseLive.DAL.Forum;

namespace IES.G2S.CourseRun.BLL
{
    public class FourmBLL : IForumBLL
    {

        #region  论坛版块的操作
        public ForumTopicType ForumTopicType_ADD(ForumTopicType model)
        {
            return FourmDAL.ForumTopicType_ADD(model);
        }

        public bool ForumTopicType_Upd(ForumTopicType model)
        {
            return FourmDAL.ForumTopicType_Upd(model);
        }

        public bool ForumTopicType_Del(ForumTopicType model )
        {
            return FourmDAL.ForumTopicType_Del(model);
        }


        /// <summary>
        /// 获取论坛版块
        /// </summary>
        /// <returns></returns>
        public List<ForumTopicType> ForumTopicType_Get( string courseid, string userid )
        {
            return FourmDAL.ForumTopicType_Get(courseid, userid);
        }

        #endregion

        #region 论题列表

        /// <summary>
        /// 获取热门帖子
        /// </summary>
        /// <param name="courseid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<ForumTopic> FormTopic_Hot_Get(string courseid, string userid)
        {
            return FourmDAL.FormTopic_Hot_Get(courseid, userid);
        }

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
        public List<ForumTopic> FormTopic_Search(string courseid, int forumyypeid, string userid, int scope, string key, int status, int order, int pagesize, int page)
        {
            return new List<ForumTopic>();
        }


        #endregion

        #region  发帖 论题状态 移动

        /// <summary>
        /// 新增论题
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ForumTopic ForumTopic_ADD(ForumTopic model)
        {
            return FourmDAL.ForumTopic_ADD(model);
        }


        /// <summary>
        /// 获取论题的所有详细信息
        /// </summary>
        /// <param name="id">论题编号</param>
        /// <returns></returns>
        public ForumTopicInfo ForumTopic_Info_Get(ForumTopicInfo model )
        {
            return FourmDAL.ForumTopic_Info_Get(model);
        }


        /// <summary>
        /// 添加论题回复
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ForumResponse ForumResponse_ADD(ForumResponse model)
        {
            return FourmDAL.ForumResponse_ADD(model);
        }



        #endregion

    }
}
