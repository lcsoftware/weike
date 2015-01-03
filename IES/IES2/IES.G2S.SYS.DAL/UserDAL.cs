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
    public class UserDAL
    {
        #region 列表

        /// <summary>
        /// 获取用户的在线课程列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<OC> User_OC_List( User model )
        {
            try
            {
                using (var conn = DbHelper.CommonService())
                {
                    var p = new DynamicParameters();
                    p.Add("@userid", model.UserID);
                    p.Add("@role", model.UserType);
                    return conn.Query<OC>("User_OC_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取用户的身份信息及权限信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static AuUserInfo AuUserInfo_Get( User model)
        {
            try
            {
                using (IDbConnection conn = DbHelper.ResourceService())
                {
                    AuUserInfo ai = new AuUserInfo();
                    var p = new DynamicParameters();
                    p.Add("@UserID", model.UserID);

                    var multi = conn.QueryMultiple("AuUserInfo_Get", p, commandType: CommandType.StoredProcedure);
                    var user = multi.Read<User>().Single();
                    var auuserroleorglist = multi.Read<AuUserRoleOrg>().ToList();
                    var aurolemodulelist = multi.Read<AuRoleModule>().ToList();

                    ai.user = user;
                    ai.auuserroleorglist = auuserroleorglist;
                    ai.aurolemodulelist = aurolemodulelist;

                    return ai ;
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }


    

        public static List<User> User_List(User model ,int PageIndex , int PageSize)
        {
            try
            {
                using (var conn = DbHelper.SysService())
                {
                    var p = new DynamicParameters();
                    p.Add("@UserType", model.UserType );
                    p.Add("@OrganizationID", model.OrganizationID);
                    p.Add("@UserNo", model.UserNo);
                    p.Add("@LoginName", model.LoginName);
                    p.Add("@UserName", model.UserName);
                    p.Add("@PageSize", PageSize);
                    p.Add("@PageIndex", PageIndex );

                    return conn.Query<User>("User_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }


        #endregion

        #region 详细信息

        public static User User_Get(User model)
        { 
            try
            {
                using (IDbConnection conn = DbHelper.SysService())
                {
                    var p = new DynamicParameters();
                    p.Add("@UserID", model.UserID);
                    p.Add("@LoginName", model.LoginName);
                    p.Add("@Pwd", model.Pwd);
                    return conn.Query<User>("User_Get", p, commandType: CommandType.StoredProcedure).SingleOrDefault<User>();
                }
            }
            catch (Exception e)
            {
                return null;
            }
     
        }


        #endregion 

        #region  新增
        public static User User_ADD(User model)
        {
            try
            {
                using (var conn = DbHelper.SysService())
                {
                    var p = new DynamicParameters();
                    p.Add("@UserID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@UserNo", model.UserNo);
                    p.Add("@UserName", model.UserName);
                    p.Add("@UserNameEn", model.UserNameEn);
                    p.Add("@LoginName", model.LoginName);
                    p.Add("@Pwd", model.Pwd);
                    p.Add("@Email", model.Email);
                    p.Add("@Tel", model.Tel);
                    p.Add("@Mobile", model.Mobile);
                    p.Add("@OrganizationID", model.OrganizationID);
                    p.Add("@Ranks", model.Ranks);
                    p.Add("@EntryDate", model.EntryDate);
                    p.Add("@SpecialtyID", model.SpecialtyID);
                    p.Add("@ClassID", model.ClassID);
                    p.Add("@UserType", model.UserType);
                    p.Add("@IsRegister", model.IsRegister);
                    p.Add("@Brief", model.Brief);
                    p.Add("@output", dbType: DbType.String, direction: ParameterDirection.Output);

                    conn.Execute("User_ADD", p, commandType: CommandType.StoredProcedure);
                    return model;
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }



        #endregion

        #region 删除
        public static bool User_Del(User model)
        {
            try
            {
                using (var conn = DbHelper.SysService())
                {
                    var p = new DynamicParameters();
                    p.Add("@UserID", model.UserID);
                    conn.Execute("User_Del", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion

        #region 更新

        public static bool User_Upd(User model)
        {
            try
            {
                using (var conn = DbHelper.SysService())
                {
                    var p = new DynamicParameters();
                    p.Add("@UserID", dbType: DbType.Int32 );
                    p.Add("@UserNo", model.UserNo);
                    p.Add("@UserName", model.UserName);
                    p.Add("@UserNameEn", model.UserNameEn);
                    p.Add("@Email", model.Email);
                    p.Add("@Tel", model.Tel);
                    p.Add("@Mobile", model.Mobile);
                    p.Add("@OrganizationID", model.OrganizationID);
                    p.Add("@Ranks", model.Ranks);
                    p.Add("@EntryDate", model.EntryDate);
                    p.Add("@SpecialtyID", model.SpecialtyID);
                    p.Add("@ClassID", model.ClassID);
                    p.Add("@IsRegister", model.IsRegister);
                    p.Add("@Brief", model.Brief);
                    p.Add("@output", dbType: DbType.String, direction: ParameterDirection.Output);
                    conn.Execute("User_Upd", p, commandType: CommandType.StoredProcedure);
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
