using Dapper;
using IES.DataBase;
using IES.JW.Model;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.JW.DAL
{
    public class AuRoleDAL
    {
        #region 新增角色
        public static AuRole AuRole_ADD(AuRole model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@RoleID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@Title", model.Title);
                    p.Add("@Brief", model.Brief);
                    p.Add("@SysID", model.SysID);
                    conn.Execute("AuRole_ADD", p, commandType: CommandType.StoredProcedure);
                    model.RoleID = p.Get<Int32>("@RoleID");
                    return model;
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }
        #endregion

        #region 删除角色
        public static bool AuRole_Del(AuRole model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@RoleID", model.RoleID);
                    conn.Execute("AuRole_Del", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }

        }
        #endregion

        #region 角色重命名
        public static bool AuRole_Upd(AuRole model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@RoleID", model.RoleID);
                    p.Add("@Title", model.Title);
                    conn.Execute("AuRole_Title_Upd", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }

        }
        #endregion

        #region 角色下用户列表
        public static List<User> AuUserRoleOrg_List(User model, int PageIndex, int PageSize)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@RoleID", model.Role);
                    p.Add("@SearchKey", model.Key);
                    return conn.Query<User>("AuUserRoleOrg_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<User>();
            }
        }
        #endregion

        #region 角色下平台列表
        public static List<Sys> AuRoleSys_List(AuRole model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@RoleID", model.RoleID);
                    return conn.Query<Sys>("AuRoleSys_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<Sys>();
            }
        }
        #endregion

        #region 子系统下权限列表
        public static List<AuModule> AuModelAction_Tree(AuRole model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@SysID", model.SysID);
                    return conn.Query<AuModule>("AuModelAction_Tree", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<AuModule>();
            }
        }
        #endregion

        #region 子系统下角色拥有权限列表
        public static List<AuRoleModule> AuRoleModule_List(AuRole model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@RoleID", model.RoleID);
                    return conn.Query<AuRoleModule>("AuRoleModule_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<AuRoleModule>();
            }
        }
        #endregion

        #region 新增角色下用户
        public static bool AuRoleUser_ADD(User model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@RoleID", model.Role);
                    p.Add("@UserIDS", model.UserIDS);
                    conn.Execute("AuRoleUser_Batch_ADD", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }

        }
        #endregion

        #region 删除角色下用户
        public static bool AuRoleUser_Del(User model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@RoleID", model.Role);
                    p.Add("@UserIDS", model.UserIDS);
                    conn.Execute("AuRoleUser_Batch_Del", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }

        }
        #endregion

        #region 保存角色在子系统中权限
        public static bool AuRoleModule_Edit(AuRole model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@IDS", model.ModIDS);
                    conn.Execute("AuRoleModule_Edit", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region 模块操作列表
        public static List<AuAction> AuAction_List()
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    return conn.Query<AuAction>("AuAction_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<AuAction>();
            }
        }
        #endregion
    }
}
