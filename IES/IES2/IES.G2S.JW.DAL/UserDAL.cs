using Dapper;
using IES.DataBase;
using IES.JW.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.CC.OC.Model;

namespace IES.G2S.JW.DAL
{
    public class UserDAL
    {
        #region 列表

        /// <summary>
        /// 获取用户的在线课程列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<OC> User_OC_List(User model)
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
        /// 获取站内用户教师列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<Teacher> Teacher_List(Teacher model, int PageIndex, int PageSize)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@key", model.Key);
                    p.Add("@OrganizationID","-1");
                    p.Add("@PageIndex", PageIndex);
                    p.Add("@PageSize", PageSize);
                    return conn.Query<Teacher>("Teacher_Search", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        
        }

        public static List<User> User_Cache_List()
        {
            try
            {
                using (var conn = DbHelper.CommonService())
                {
                    return conn.Query<User>("User_Cache_List", null, commandType: CommandType.StoredProcedure).ToList();
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
        public static AuUserInfo AuUserInfo_Get(User model)
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

                    return ai;
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }




        public static List<User> User_List(User model, int PageIndex, int PageSize)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@Key", model.Key);
                    p.Add("@Role", model.Role);
                    p.Add("@modevalue", model.modevalue);
                    p.Add("@OrganizationID", model.OrganizationID);
                    p.Add("@SpecialtyID", model.SpecialtyID);
                    p.Add("@ClassID", model.ClassID);
                    p.Add("@IsLocked", model.IsLocked);
                    p.Add("@IsRegister", model.IsRegister);
                    p.Add("@IsAssistant", model.IsAssistant);
                    p.Add("@IsShow", model.IsShow);
                    p.Add("@IsInSchool", model.IsInSchool);
                    p.Add("@EntryDate", model.EntryDate);
                    p.Add("@PageIndex", PageIndex);
                    p.Add("@PageSize", PageSize);                  
                    return conn.Query<User>("User_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public static List<User> User_DiskSpace_List(User model, int PageIndex, int PageSize)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    //int Count = Convert.ToInt32(model.DiskFreeze);
                    var p = new DynamicParameters();
                    p.Add("@UserName", model.UserName);
                    p.Add("@UserType", model.UserType);
                    p.Add("@DiskFreeze",Convert.ToInt32(model.DiskFreeze));
                    p.Add("@DiskSize", model.DiskSize);
                    p.Add("@PageIndex", PageIndex);
                    p.Add("@PageSize", PageSize);
                    return conn.Query<User>("User_DiskSpace_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception)
            {
                return new List<User>();
            }
        }

        //根据组织机构ID获取学生列表
        public static List<User> Student_Search(string key,int OrganizationID,int SpecialtyID,int ClassID,int IsRegister,string StudentIDs,int PageSize,int PageIndex)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@Key",key);
                    p.Add("@OrganizationID", OrganizationID);
                    p.Add("@SpecialtyID", SpecialtyID);
                    p.Add("@ClassID", ClassID);
                    p.Add("@IsRegister", IsRegister);
                    p.Add("@StudentIDs", StudentIDs);
                    p.Add("@PageSize", PageSize);
                    p.Add("@PageIndex", PageIndex);
                    return conn.Query<User>("Student_Search", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //根据组织机构ID获取教师列表
        public static List<User> Teacher_Search(string Key, int OrganizationID, int PageSize, int PageIndex)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@Key", Key);
                    p.Add("@OrganizationID", OrganizationID);
                    p.Add("@PageSize", PageSize);
                    p.Add("@PageIndex", PageIndex);
                    return conn.Query<User>("Teacher_Search", p, commandType: CommandType.StoredProcedure).ToList();
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
                using (IDbConnection conn = DbHelper.JWService())
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

        #region 批量删除
        public static bool User_Batch_Del(string IDS)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@UserIDS", IDS);
                    conn.Execute("User_Batch_Del", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion 

        #region  新增
        public static User User_ADD(User model)
        {
            try
            {
                using (var conn = DbHelper.JWService() )
                {
                    var p = new DynamicParameters();
                    p.Add("@UserID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@UserNo", model.UserNo);
                    p.Add("@UserName", model.UserName);
                    p.Add("@UserNameEn", model.UserNameEn);
                    p.Add("@LoginName", model.LoginName);
                    p.Add("@Pwd", model.Pwd);
                    p.Add("@Gender", model.Gender);
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
                    p.Add("@IsInSchool", model.IsInSchool);
                    p.Add("@IsShow", model.IsShow);
                    p.Add("@output","", dbType: DbType.String, direction: ParameterDirection.Output);

                    conn.Execute("User_ADD", p, commandType: CommandType.StoredProcedure);
                    model.output = p.Get<string>("@output");
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
                using (var conn = DbHelper.JWService())
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
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@UserID", model.UserID);
                    p.Add("@UserNo", model.UserNo);
                    p.Add("@UserName", model.UserName);
                    p.Add("@UserNameEn", model.UserNameEn);
                    p.Add("@Gender", model.Gender);
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
                    p.Add("@IsInSchool", model.IsInSchool);
                    p.Add("@IsShow", model.IsShow);
                    p.Add("@output", "", dbType: DbType.String, direction: ParameterDirection.Output);
                    conn.Execute("User_Upd", p, commandType: CommandType.StoredProcedure);
                    model.output = p.Get<string>("@output");
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion

        public static bool ChangePassword(User model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@UserID", model.UserID);
                    p.Add("@Pwd", model.Pwd);
                    conn.Execute("Password_Upd", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
