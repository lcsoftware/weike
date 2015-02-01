using IES.CC.Forum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using IES.G2S.CourseLive.BLL.Forum;
using IES.G2S.CourseRun.BLL;
using IES.CC.OC.Model;
using IES.G2S.CourseLive.BLL.OC;

namespace App.G2S.DataProvider.CourseLive.Forum
{
    public partial class ForumProvider : System.Web.UI.Page
    {
        #region 列表

        /// <summary>
        /// 回复列表
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static ForumResponseInfo ForumResponseInfo_List(ForumResponse model, int PageIndex = 1, int PageSize = 10)
        {
            return new ResponseBLL().ForumResponseInfo_List(model, PageIndex, PageSize);
        }

        /// <summary>
        /// 牛人榜
        /// </summary>
        /// <param name="OCID"></param>
        /// <param name="Top"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<ForumTopic> Forum_HotUser_List(int OCID, int Top)
        {
            return new ForumTopicBLL().Forum_HotUser_List(OCID, Top);
        }

        /// <summary>
        /// 论坛版块列表
        /// xuwei
        /// 2015年1月7日18:43:59
        /// </summary>
        /// <param name="ft"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<ForumType> ForumType_List(ForumType ft)
        {
            return new ForumTypeBLL().ForumType_List(ft);
        }

        /// <summary>
        /// 论题搜索
        /// </summary>
        /// <param name="model"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<ForumTopic> ForumTopic_Search(ForumTopic model, int PageIndex = 1, int PageSize = 20)
        {
            return new ForumTopicBLL().ForumTopic_Search(model, PageIndex, PageSize);
        }

        /// <summary>
        /// 网络教学班下拉列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<OCClass> OCClass_Dropdown_List(int OCID)
        {
            return new OCClassBLL().OCClass_Dropdown_List(OCID);
        }

        /// <summary>
        /// 活跃论题列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <param name="UserID"></param>
        /// <param name="Top"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<ForumTopic> ForumTopic_Active_List(int OCID, int UserID, int Top = 5)
        {
            return new ForumTopicBLL().ForumTopic_Active_List(OCID, UserID, Top);
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
        [WebMethod]
        public static ForumType ForumType_ADD(ForumType model)
        {
            return new ForumTypeBLL().ForumType_ADD(model);
        }

        /// <summary>
        /// 发帖
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [WebMethod]
        public static ForumTopic ForumTopic_Add(ForumTopic model)
        {
            return new ForumTopicBLL().ForumTopic_Add(model);
        }

        /// <summary>
        /// 添加论题回复
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [WebMethod]
        public static ForumResponse ForumResponse_ADD(ForumResponse model)
        {
            return new ResponseBLL().ForumResponse_ADD(model);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除版块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool ForumType_Del(ForumType model)
        {
            return new ForumTypeBLL().ForumType_Del(model);
        }

        /// <summary>
        /// 删除帖子
        /// </summary>
        /// <param name="TopicID"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool ForumTopic_Del(int TopicID)
        {
            return new ForumTopicBLL().ForumTopic_Del(TopicID);
        }

        /// <summary>
        /// 删除回复
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static bool ForumResponse_Del(int ResponseID)
        {
            return new ResponseBLL().ForumResponse_Del(ResponseID);
        }
        #endregion

        #region 对象更新
        /// <summary>
        /// 编辑论坛版块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool ForumType_Upd(ForumType model)
        {
            return new ForumTypeBLL().ForumType_Upd(model);
        }

        /// <summary>
        /// 编辑贴子
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool ForumTopic_Upd(ForumTopic model)
        {
            return new ForumTopicBLL().ForumTopic_Upd(model);
        }
        #endregion

        #region 详细信息
        /// <summary>
        /// 获取论题的所有详细信息
        /// </summary>
        /// <param name="id">论题编号</param>
        /// <returns></returns>
        [WebMethod]
        public static ForumTopic ForumTopic_Get(int TopicID)
        {
            return new ForumTopicBLL().ForumTopic_Get(TopicID);
        }
        #endregion

        #region 单个属性更新
        /// <summary>
        /// 设置或取消精华
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool ForumTopic_IsEssence_Upd(int TopicID)
        {
            return new ForumTopicBLL().ForumTopic_IsEssence_Upd(TopicID);
        }

        /// <summary>
        /// 设置或取消置顶
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool ForumTopic_IsTop_Upd(int TopicID)
        {
            return new ForumTopicBLL().ForumTopic_IsTop_Upd(TopicID);
        }

        /// <summary>
        /// 为论坛主题或回复点赞  
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool ForumMy_IsGood_Upd(ForumMy model)
        {
            return new ForumMyBLL().ForumMy_IsGood_Upd(model);
        }

        /// <summary>
        /// 为论坛主题或回复点赞  
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool ForumTopic_ForumTypeID_Upd(int TopicID, int ForumTypeID)
        {
            return new ForumTopicBLL().ForumTopic_ForumTypeID_Upd(TopicID, ForumTypeID);
        }
        #endregion

        #region 详细信息

        /// <summary>
        /// 获取论坛版块详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [WebMethod]
        public static ForumTypeInfo ForumTypeInfo_Get(ForumType model)
        {
            return new ForumTypeBLL().ForumTypeInfo_Get(model);
        }
        #endregion
    }
}