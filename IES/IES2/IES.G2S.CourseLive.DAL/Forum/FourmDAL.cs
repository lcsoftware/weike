using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using IES.CC.Forum.Model;
using IES.DataBase;
using Dapper;


namespace IES.G2S.CourseLive.DAL.Forum
{
    /// <summary>
    /// 论坛主题操作
    /// </summary>
    public class FourmDAL
    {
        #region  列表

        /// <summary>
        /// 获取热门帖子
        /// </summary>
        /// <param name="courseid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static List<ForumTopic> FormTopic_Hot_Get(string courseid, string userid)
        {
            return new List<ForumTopic>();
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
        public static List<ForumTopic> FormTopic_Search(string courseid, int forumyypeid, string userid, int scope, string key, int status, int order, int pagesize, int page)
        {
            return new List<ForumTopic>();
        }

        #endregion

        #region 详细信息

        /// <summary>
        /// 获取论题的所有详细信息
        /// </summary>
        /// <param name="id">论题编号</param>
        /// <returns></returns>
        public static ForumTopicInfo ForumTopic_Info_Get(ForumTopicInfo model)
        {
            return new ForumTopicInfo();
        }

        #endregion

        #region  新增

        /// <summary>
        /// 新增论题
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ForumTopic ForumTopic_ADD(ForumTopic model)
        {
            return new ForumTopic();
        }


        #endregion

        #region 对象更新



        #endregion

        #region 单个批量更新





        #endregion

        #region 属性批量操作




        #endregion

        #region 删除


        #endregion
    }
}
