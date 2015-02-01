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
using IES.CC.OC.Model;


namespace IES.G2S.CourseLive.DAL.Forum
{
    /// <summary>
    /// 论坛主题操作
    /// </summary>
    public class ForumTopicDAL
    {
        #region  列表

        /// <summary>
        /// 论坛主题查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<ForumTopic> ForumTopic_Search(ForumTopic model, int PageIndex = 1, int PageSize = 20)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@SearchKey", model.SearchKey);
                    p.Add("@OCID", model.OCID);
                    p.Add("@UserID", model.UserID);
                    p.Add("@ForumTypeID", model.ForumTypeID);
                    p.Add("@IsEssence", model.IsEssence);
                    p.Add("@IsMyStart", model.IsMyStart);
                    p.Add("@IsMyJoin", model.IsMyJoin);
                    p.Add("@ResponseStatus", model.ResponseStatus);
                    p.Add("@Order", model.Order);
                    p.Add("@PageSize", 1);
                    p.Add("@PageIndex", 20);
                    return conn.Query<ForumTopic>("ForumTopic_Search", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {

                return null;
            }
        }

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
        /// 牛人榜列表
        /// </summary>
        /// <param name="OCID">在线课程编号</param>
        /// <param name="Top">牛人个数</param>
        /// <returns>很多牛人</returns>
        public static List<ForumTopic> Forum_HotUser_List(int OCID, int Top)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@OCID", OCID);
                    p.Add("@Top", Top);
                    return conn.Query<ForumTopic>("Forum_HotUser_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {

                return null;
            }
        }

        /// <summary>
        /// 活跃的讨论
        /// </summary>
        /// <param name="OCID"></param>
        /// <param name="UserID"></param>
        /// <param name="Top"></param>
        /// <returns></returns>
        public static List<ForumTopic> ForumTopic_Active_List(int OCID, int UserID, int Top = 5)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@OCID", OCID);
                    p.Add("@UserID", UserID);
                    p.Add("@Top", Top);
                    return conn.Query<ForumTopic>("ForumTopic_Active_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {

                return null;
            }
        }
        #endregion

        #region 详细信息

        /// <summary>
        /// 获取论题的所有详细信息
        /// </summary>
        /// <param name="id">论题编号</param>
        /// <returns></returns>
        public static ForumTopic ForumTopic_Get(int TopicID)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@TopicID", TopicID);
                    return conn.Query<ForumTopic>("ForumTopic_Get", p, commandType: CommandType.StoredProcedure).SingleOrDefault<ForumTopic>();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region  新增

        /// <summary>
        /// 发帖
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ForumTopic ForumTopic_Add(ForumTopic model)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@TopicID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@OCID", model.OCID);
                    p.Add("@CourseID", model.CourseID);
                    p.Add("@ForumTypeID", model.ForumTypeID);
                    p.Add("@GroupTaskID", 0);
                    p.Add("@UserID", model.UserID);
                    p.Add("@UserName", model.UserName);
                    p.Add("@Title", model.Title);
                    p.Add("@Conten", model.Conten);
                    p.Add("@TopicType", model.TopicType);
                    p.Add("@Tags", model.Tags);
                    conn.Execute("ForumTopic_Add", p, commandType: CommandType.StoredProcedure);
                    model.TopicID = p.Get<int>("TopicID");
                    return model;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        ///  MOOC 章发帖
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static OCMoocLive ForumTopic_Add(OCMoocLive model)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@TopicID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@OCID", model.OCID);
                    p.Add("@CourseID", 0);
                    p.Add("@ForumTypeID", 0);
                    p.Add("@GroupTaskID", 0);
                    p.Add("@UserID", model.UserID);
                    p.Add("@UserName", model.UserName);
                    p.Add("@Title", model.ForumTitle);
                    p.Add("@Conten", "");
                    p.Add("@TopicType", 2);
                    p.Add("@Tags","");
                    conn.Execute("ForumTopic_Add", p, commandType: CommandType.StoredProcedure);
                    model.TopicID = p.Get<int>("TopicID");
                    return model;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        #endregion

        #region 对象更新

        /// <summary>
        /// 编辑贴子
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool ForumTopic_Upd(ForumTopic model)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@TopicID", model.TopicID);
                    p.Add("@Title", model.Title);
                    p.Add("@ForumTypeID", model.ForumTypeID);
                    p.Add("@Conten", model.Conten);
                    p.Add("@Tags", model.Tags);
                    conn.Execute("ForumTopic_Upd", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 编辑贴子
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool ForumTopicTitle_Upd(OCMoocLive model)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@TopicID", model.TopicID);
                    p.Add("@Title", model.ForumTitle);
                    conn.Execute("ForumTopicTitle_Upd", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }


        #endregion

        #region 单个更新
        /// <summary>
        /// 设置或取消精华
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool ForumTopic_IsEssence_Upd(int TopicID)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@TopicID", TopicID);
                    conn.Execute("ForumTopic_IsEssence_Upd", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 设置或取消置顶
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool ForumTopic_IsTop_Upd(int TopicID)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@TopicID", TopicID);
                    conn.Execute("ForumTopic_IsTop_Upd", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        /// <summary>
        /// 移动帖子
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool ForumTopic_ForumTypeID_Upd(int TopicID, int ForumTypeID)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@TopicID", TopicID);
                    p.Add("@ForumTypeID", ForumTypeID);
                    conn.Execute("ForumTopic_ForumTypeID_Upd", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion

        #region 属性批量操作





        #endregion

        #region 删除

        /// <summary>
        /// 删除帖子
        /// </summary>
        /// <param name="TopicID"></param>
        /// <returns></returns>
        public static bool ForumTopic_Del(int TopicID)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@TopicID", TopicID);
                    conn.Execute("ForumTopic_Del", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion
    }
}
