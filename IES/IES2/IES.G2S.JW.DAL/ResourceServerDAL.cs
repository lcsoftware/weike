using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using IES.DataBase;
using Dapper;
using IES.JW.Model;

namespace IES.G2S.JW.DAL
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
                using (var conn = DbHelper.JWService())
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
        public static List<ResourceServer> ResourceServer_List()
        {
            try
            {
                using (var conn = DbHelper.JWService())
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
        /// 获取存储服务器详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ResourceServer ResourceServer_Get(ResourceServer model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ServerID", model.ServerID);
                    return conn.Query<ResourceServer>("ResourceServer_Get", p, commandType: CommandType.StoredProcedure).Single();
                }
            }
            catch (Exception e)
            {
                return null;
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
                using (var conn = DbHelper.JWService())
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
        /// <summary>
        /// 存储服务器编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ResourceServer ResourceServer_Edit(ResourceServer model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ServerID", model.ServerID);
                    p.Add("@Host", model.Host);
                    p.Add("@IISFolder", model.IISFolder);
                    p.Add("@IISPort", model.IISPort);
                    p.Add("@MMSFolder", model.MMSFolder);
                    p.Add("@MMSPort", model.MMSPort);
                    p.Add("@NginxFolder", model.NginxFolder);
                    p.Add("@NginxPort", model.NginxPort);
                    p.Add("@PubKey", model.PubKey);
                    p.Add("@Brief", model.Brief);
                    conn.Execute("Resourceserver_Edit", p, commandType: CommandType.StoredProcedure);
                    model.ServerID = p.Get<Int32>("@ServerID");
                    return model;
                }
            }
            catch (Exception e)
            {
                return model;
            }
        }

    }
}
