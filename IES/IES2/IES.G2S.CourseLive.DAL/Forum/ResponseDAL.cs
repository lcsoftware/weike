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
    public class ResponseDAL
    {
        #region  列表

        /// <summary>
        /// 回复列表
        /// </summary>
        /// <returns></returns>
        public static ForumResponseInfo ForumResponseInfo_List(ForumResponse model, int PageIndex = 1, int PageSize = 10)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@TopicID", model.TopicID);
                    p.Add("@UserID", model.UserID);
                    var mutil = conn.QueryMultiple("ForumResponseInfo_List", p, commandType: CommandType.StoredProcedure);

                    var forumresponseinfo = new ForumResponseInfo()
                    {
                        forumresponselist = mutil.Read<ForumResponse>().ToList(),
                        forummylist = mutil.Read<ForumMy>().ToList()
                    };
                    return forumresponseinfo;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        #endregion


        #region 详细信息

        #endregion


        #region  新增


        /// <summary>
        /// 添加论题回复
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ForumResponse ForumResponse_ADD(ForumResponse model)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ResponseID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@TopicID", model.TopicID);
                    p.Add("@ParentID", model.ParentID);
                    p.Add("@Conten", model.Conten);
                    p.Add("@UserID", model.UserID);
                    p.Add("@UserName", model.UserName);
                    conn.Execute("ForumResponse_ADD", p, commandType: CommandType.StoredProcedure);
                    model.ResponseID = p.Get<int>("ResponseID");
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



        #endregion


        #region 单个批量更新





        #endregion


        #region 属性批量操作





        #endregion

        #region 删除
        /// <summary>
        /// 删除回复
        /// </summary>
        /// <returns></returns>
        public static bool ForumResponse_Del(int ResponseID) {
            try
            {
                using (var conn=DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ResponseID", ResponseID);
                    conn.Execute("ForumResponse_Del", p, commandType: CommandType.StoredProcedure);
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
