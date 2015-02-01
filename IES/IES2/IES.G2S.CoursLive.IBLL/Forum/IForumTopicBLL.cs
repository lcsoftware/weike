using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.CC.Forum.Model;
using IES.CC.OC.Model;

namespace IES.G2S.CourseLive.IBLL.Forum
{
    public interface IForumTopicBLL
    {
        #region  论坛版块的操作

        ForumTopicType ForumTopicType_ADD(ForumTopicType model);

        bool  ForumTopicType_Upd(ForumTopicType model);

        bool ForumTopicType_Del(ForumTopicType model);


        /// <summary>
        /// 获取论坛版块
        /// </summary>
        /// <returns></returns>
        List<ForumTopicType> ForumTopicType_Get(string courseid, string userid);

        #endregion

        #region 对象更新
        /// <summary>
        /// 论坛表题更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool ForumTopicTitle_Upd(OCMoocLive model); 
        #endregion

        #region 列表

        /// <summary>
        /// 获取热门帖子
        /// </summary>
        /// <param name="courseid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        List<ForumTopic> FormTopic_Hot_Get(string courseid, string userid);

        List<ForumTopic> Forum_HotUser_List(int OCID, int Top);
        #endregion

        #region  发帖 论题状态 移动

        /// <summary>
        /// 新增论题
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ForumTopic ForumTopic_Add(ForumTopic model);

        /// <summary>
        /// 新增论题
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        OCMoocLive ForumTopic_Add(OCMoocLive model);
        ///// <summary>
        ///// 获取论题的所有详细信息
        ///// </summary>
        ///// <param name="id">论题编号</param>
        ///// <returns></returns>
        //ForumTopicInfo ForumTopic_Info_Get(ForumTopic model );


        /// <summary>
        /// 添加论题回复
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ForumResponse ForumResponse_ADD(ForumResponse model);



        #endregion


    }
}