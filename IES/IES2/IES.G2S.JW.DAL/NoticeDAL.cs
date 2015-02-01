using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using IES.CC.OC.Model;
using IES.JW.Model;
using IES.DataBase;
using Dapper;

namespace IES.G2S.JW.DAL
{
    public class NoticeDAL
    {
        #region 列表
        /// <summary>
        /// 获取列表
        /// </summary>
        public static List<Notice> Notice_List(Notice model, int PageIndex, int PageSize)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@UserID", model.UserID);
                    p.Add("@ModuleID", model.ModuleID);
                    p.Add("@SysID", model.SysID); 
                    p.Add("@PageIndex", PageIndex);
                    p.Add("@PageSize", PageSize);
                    return conn.Query<Notice>("Notice_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取通知回复列表
        /// </summary>
        public static List<NoticeResponse> NoticeResponse_List(NoticeResponse model, int PageIndex, int PageSize)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@NoticeID", model.NoticeID);
                    p.Add("@PageIndex", PageIndex);
                    p.Add("@PageSize", PageSize);
                    return conn.Query<NoticeResponse>("NoticeResponse_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region 接收通知
        public static List<Notice> Notice_Receive_List(User model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@UserID", model.UserID);
                    return conn.Query<Notice>("Notice_Receive_List", null, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region 发送通知
        public static bool Notice_Send_List(NoticeObject model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@NoticeID", model.NoticeID);
                    p.Add("@Source", model.Source);
                    p.Add("@SourceID", model.SourceID);
                    conn.Execute("Notice_Send_List", null, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region 新增通知
        /// <summary>
        /// 新增一条数据
        /// </summary>
        public static Notice Notice_ADD(Notice model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@Title", model.Title);
                    p.Add("@Conten", model.Conten);
                    p.Add("@IsTop", model.IsTop);
                    p.Add("@IsForMail", model.IsForMail);
                    p.Add("@IsForSMS", model.IsForSMS);
                    p.Add("@EndDate", model.EndDate);
                    p.Add("@SysID", model.SysID);
                    p.Add("@ModuleID", model.ModuleID);
                    p.Add("@UserID", model.UserID);
                    p.Add("@Source", model.Source);
                    p.Add("@SourceIDs", model.SourceIDs);
                    p.Add("@Source2", model.Source2);
                    p.Add("@SourceIDs2", model.SourceIDs2);
                    p.Add("@EntryDates", model.EndDate);
                    p.Add("@op_IsCanSendMsg", model.IsCanSendMsg, DbType.Int32, ParameterDirection.InputOutput);
                    p.Add("@op_NoticeID", model.NoticeID, DbType.Int32, ParameterDirection.InputOutput);
                    conn.Execute("Notice_ADD", p, commandType: CommandType.StoredProcedure);
                    model.IsCanSendMsg = p.Get<int>("op_IsCanSendMsg");
                    model.NoticeID = p.Get<int>("op_NoticeID");
                    return model;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        //新增通知回复评论
        public static NoticeResponse NoticeResponse_ADD(NoticeResponse model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ResponseID", model.ResponseID, DbType.Int32, ParameterDirection.InputOutput);
                    p.Add("@NoticeID", model.NoticeID);
                    p.Add("@UserID", model.UserID);
                    p.Add("@Conten", model.Conten);
                    conn.Execute("NoticeResponse_ADD", p, commandType: CommandType.StoredProcedure);
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

        #region 通知详细
        public static Notice Notice_Get(Notice model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@NoticeID", model.NoticeID);
                    return conn.Query<Notice>("Notice_Get", p, commandType: CommandType.StoredProcedure).Single();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Notice_Upd(Notice model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@NoticeID", model.NoticeID);
                    p.Add("@Title", model.Title);
                    p.Add("@Conten", model.Conten);
                    p.Add("@IsTop", model.IsTop);
                    conn.Execute("Notice_Upd", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool Notice_Del(Notice model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@NoticeID", model.NoticeID);
                    conn.Execute("Notice_Del", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region 批量删除
        /// <summary>
        /// 删除多条数据
        /// </summary>
        public static bool Notice_Batch_Del(string IDS)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@NoticeIDS", IDS);
                    conn.Execute("Notice_Batch_Del", p, commandType: CommandType.StoredProcedure);
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
