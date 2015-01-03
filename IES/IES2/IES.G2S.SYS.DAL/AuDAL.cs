using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using IES.SYS.Model;
using IES.DataBase;
using Dapper;


namespace IES.G2S.SYS.DAL
{
    public class AuDAL
    {
        #region 列表




        public static List<AuModule> AuModule_List()
        {
            try
            {
                using (var conn = DbHelper.SysService())
                {
                    return conn.Query<AuModule>("AuModule_List", null , commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public static List<AuAction> AuAction_List()
        {
            try
            {
                using (var conn = DbHelper.SysService())
                {
                    return conn.Query<AuAction>("AuAction_List", null, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static List<AuRole> AuRole_List()
        {
            try
            {
                using (var conn = DbHelper.SysService())
                {
                    return conn.Query<AuRole>("AuRole_List", null, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public static List<AuRoleModule> AuRoleModule_List()
        {
            try
            {
                using (var conn = DbHelper.SysService())
                {
                    return conn.Query<AuRoleModule>("AuRoleModule_List", null, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }



        public static List<AuUserRoleOrg> AuUserRoleOrg_List()
        {
            try
            {
                using (var conn = DbHelper.SysService())
                {
                    return conn.Query<AuUserRoleOrg>("AuUserRoleOrg_List", null, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public static List<Menu> Menu_List()
        {
            try
            {
                using (var conn = DbHelper.SysService())
                {
                    return conn.Query<Menu>("Menu_List", null, commandType: CommandType.StoredProcedure).ToList();
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
