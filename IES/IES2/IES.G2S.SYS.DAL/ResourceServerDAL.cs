using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using IES.DataBase;
using Dapper;
using IES.SYS.Model;

namespace IES.G2S.SYS.DAL
{
    public class ResourceServerDAL
    {
        /// <summary>
        /// 存储服务器新增

        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ResourceServer ResourceServer_ADD(ResourceServer model)
        {
            try
            {
                using (var conn = DbHelper.SysService())
                {
                    var p = new DynamicParameters();
                    return model;
                }
            }
            catch (Exception e)
            {
                return model;
            }
        }


        /// <summary>
        /// 获取可用存储服务器的列表
        /// </summary>
        /// <returns></returns>
        public static List<ResourceServer> ResourceServer_List( )
        {
            try
            {
                using (var conn = DbHelper.SysService())
                {
                    return conn.Query<ResourceServer>("ResourceServer_List", null, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<ResourceServer>();
            }
        }

        /// <summary>
        /// 存储服务器删除

        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool ResourceServer_Del(ResourceServer model)
        {
            try
            {
                using (var conn = DbHelper.SysService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ServerID", model.ServerID);
                    conn.Execute("ResourceServer_Del", p, commandType: CommandType.StoredProcedure);
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
