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
        public static List<OC> User_OC_Get( User model )
        {
            try
            {
                using (var conn = DbHelper.SysService())
                {
                    var p = new DynamicParameters();
                    p.Add("@userid", model.UserID);
                    p.Add("@role", model.UserType);
                    return conn.Query<OC>("User_OC_Get", p, commandType: CommandType.StoredProcedure).ToList();
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



        #endregion 




    }
}
