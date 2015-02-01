using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.CC.Forum.Model;
using IES.G2S.CourseLive.IBLL.Forum;
using IES.G2S.CourseLive.DAL.Forum;
using IES.CC.OC.Model;
using IES.Service;

namespace IES.G2S.CourseRun.BLL
{
    public class ForumTopicBLL : IForumTopicBLL
    {
        #region 删除
        public bool ForumTopicType_Del(ForumTopicType model)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除帖子
        /// </summary>
        /// <param name="TopicID"></param>
        /// <returns></returns>
        public bool ForumTopic_Del(int TopicID)
        {
            return ForumTopicDAL.ForumTopic_Del(TopicID);
        }
        #endregion

        #region 对象更新

        /// <summary>
        /// 编辑贴子
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ForumTopic_Upd(ForumTopic model)
        {
            return ForumTopicDAL.ForumTopic_Upd(model);
        }

        /// <summary>
        /// 更改论题
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ForumTopicTitle_Upd(OCMoocLive model)
        {
            return ForumTopicDAL.ForumTopicTitle_Upd(model);
        }


        public bool ForumTopicType_Upd(ForumTopicType model)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 列表
        public List<ForumTopicType> ForumTopicType_Get(string courseid, string userid)
        {
            throw new NotImplementedException();
        }

        public List<ForumTopic> FormTopic_Hot_Get(string courseid, string userid)
        {
            throw new NotImplementedException();
        }

        public ForumResponse ForumResponse_ADD(ForumResponse model)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 牛人榜
        /// </summary>
        /// <param name="OCID"></param>
        /// <param name="Top"></param>
        /// <returns></returns>
        public List<ForumTopic> Forum_HotUser_List(int OCID, int Top)
        {
            return ForumTopicDAL.Forum_HotUser_List(OCID, Top);
        }

        /// <summary>
        /// 活跃论题列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <param name="UserID"></param>
        /// <param name="Top"></param>
        /// <returns></returns>
        public List<ForumTopic> ForumTopic_Active_List(int OCID, int UserID, int Top = 5)
        {
            return ForumTopicDAL.ForumTopic_Active_List(OCID, UserID, Top);
        }

        /// <summary>
        /// 论坛主题查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ForumTopic> ForumTopic_Search(ForumTopic model, int PageIndex = 1, int PageSize = 20)
        {
            return ForumTopicDAL.ForumTopic_Search(model, PageIndex, PageSize);
        }
        #endregion

        #region 获取对象详细信息
        /// <summary>
        /// 获取论题的所有详细信息
        /// </summary>
        /// <param name="id">论题编号</param>
        /// <returns></returns>
        public ForumTopic ForumTopic_Get(int TopicID)
        {
            return ForumTopicDAL.ForumTopic_Get(TopicID);
        }
        #endregion

        #region 单个属性更新
        /// <summary>
        /// 设置或取消精华
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ForumTopic_IsEssence_Upd(int TopicID)
        {
            return ForumTopicDAL.ForumTopic_IsEssence_Upd(TopicID);
        }

        /// <summary>
        /// 设置或取消置顶
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ForumTopic_IsTop_Upd(int TopicID)
        {
            return ForumTopicDAL.ForumTopic_IsTop_Upd(TopicID);
        }

        /// <summary>
        /// 移动帖子
        /// </summary>
        /// <param name="TopicID"></param>
        /// <param name="ForumTypeID"></param>
        /// <returns></returns>
        public bool ForumTopic_ForumTypeID_Upd(int TopicID, int ForumTypeID)
        {
            return ForumTopicDAL.ForumTopic_ForumTypeID_Upd(TopicID, ForumTypeID);
        }
        #endregion

        #region 新增
        /// <summary>
        /// 发帖
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ForumTopic ForumTopic_Add(ForumTopic model)
        {
            return ForumTopicDAL.ForumTopic_Add(model);
        }

        /// <summary>
        /// 发帖
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OCMoocLive ForumTopic_Add(OCMoocLive model)
        {
            model.UserID = UserService.CurrentUser.UserID;
            model.UserName = UserService.CurrentUser.UserName;
            return ForumTopicDAL.ForumTopic_Add(model);      
        }

        public ForumTopicType ForumTopicType_ADD(ForumTopicType model)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
