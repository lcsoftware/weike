using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using IES.CC.OC.Model;
using IES.SYS.Model;
using IES.DataBase;
using Dapper;
namespace IES.G2S.SYS.DAL
{
     public  class NoticeDAL
     {
         #region 列表
         /// <summary>
         /// 获取列表
         /// </summary>
         public static List<Notice> Notice_List(Notice model, int PageIndex, int PageSize)
        {
            try
            {
                using (var conn = DbHelper.SysService())
                {
                    var p = new DynamicParameters();
                    p.Add("@Title", model.Title);
                    p.Add("@PageSize", PageSize);
                    p.Add("@PageIndex", PageIndex);
                    return conn.Query<Notice>("Notice_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
         #endregion

         #region 接收通知
         public static List<Notice> Notice_Receive_List( User model )
         {
             try
             {
                 using (var conn = DbHelper.SysService())
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
                 using (var conn = DbHelper.SysService())
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
                using (var conn = DbHelper.SysService())
                {
                    var p = new DynamicParameters();
                    p.Add("@NoticeID",model.NoticeID);
                    p.Add("@Title",model.Title);
                    p.Add("@Conten",model.Conten);
                    p.Add("@UpdateTime",model.UpdateTime);
                    p.Add("@IsTop",model.IsTop);
                    p.Add("@EndDate",model.EndDate);
                    p.Add("@SysID",model.SysID);
                    p.Add("@UserID",model.UserID);
                    conn.Execute("Notice_ADD", p, commandType: CommandType.StoredProcedure);
                    model.NoticeID = p.Get<int>("NoticeID");
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
         public static List<Notice> Notice_Get(Notice model)
         {
             try
             {
                 using (var conn = DbHelper.SysService())
                 {
                     var p = new DynamicParameters();
                     p.Add("@NoticeID", model.NoticeID);
                     return conn.Query<Notice>("Notice_Get", p, commandType: CommandType.StoredProcedure).ToList();
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
                using (var conn = DbHelper.SysService())
                {
                    var p = new DynamicParameters();
                    p.Add("@NoticeID",model.NoticeID);
                    p.Add("@Title",model.Title);
                    p.Add("@Conten",model.Conten);
                    p.Add("@UpdateTime",model.UpdateTime);
                    p.Add("@IsTop",model.IsTop);
                    p.Add("@EndDate",model.EndDate);
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
                using (var conn = DbHelper.SysService())
                {
                    var p = new DynamicParameters();
                    p.Add("@NoticeID",model.NoticeID);
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


     }
}
