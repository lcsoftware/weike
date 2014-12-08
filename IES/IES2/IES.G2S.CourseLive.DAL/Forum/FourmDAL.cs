﻿using System;
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
    public class FourmDAL
    {
        #region  论坛版块的操作

        public static ForumTopicType ForumTopicType_ADD(ForumTopicType model)
        {
            return new ForumTopicType();
        }

        public static bool ForumTopicType_Upd(ForumTopicType model)
        {
            return true;
        }

        /// <summary>
        /// 论坛版块删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool ForumTopicType_Del(ForumTopicType model)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ForumTopicTypeID", model.ForumTypeID );
                    conn.Execute("ForumTopicType_Del", p, commandType: CommandType.StoredProcedure);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }


        /// <summary>
        /// 获取论坛版块
        /// </summary>
        /// <returns></returns>
        public static List<ForumTopicType> ForumTopicType_Get(string courseid, string userid)
        {
            return new List<ForumTopicType>();
        }

        #endregion

        #region 论题列表

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

        #region  发帖 论题状态 移动

        /// <summary>
        /// 新增论题
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ForumTopic ForumTopic_ADD(ForumTopic model)
        {
            return new ForumTopic();
        }


        /// <summary>
        /// 获取论题的所有详细信息
        /// </summary>
        /// <param name="id">论题编号</param>
        /// <returns></returns>
        public static ForumTopicInfo ForumTopic_Info_Get(ForumTopicInfo model)
        {
            return new ForumTopicInfo();
        }


        /// <summary>
        /// 添加论题回复
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ForumResponse ForumResponse_ADD(ForumResponse model)
        {
            return new ForumResponse();
        }



        #endregion
    }
}
