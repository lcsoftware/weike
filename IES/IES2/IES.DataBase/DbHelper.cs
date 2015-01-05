using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using SqlHelperClass;
using Dapper;

namespace IES.DataBase
{
    public static partial  class DbHelper 
    {

        #region 构造方法，实例化连接字符串

        /// <summary>
        /// 读取WebConfig链接字符串
        /// </summary>
        /// <param name="connectionName">ConnectionString配置名</param>
        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        private static string ies_resourceconn
        {
            get { return ConfigurationManager.ConnectionStrings["ies_resourceconn"].ConnectionString; }
        }

        private static string ies_ccconnn
        {
            get { return ConfigurationManager.ConnectionStrings["ies_ccconnn"].ConnectionString; }
        }

        private static string ies_jwconnn
        {
            get { return ConfigurationManager.ConnectionStrings["ies_jwconnn"].ConnectionString; }
        }

        private static string ies_portalconnn
        {
            get { return ConfigurationManager.ConnectionStrings["ies_portalconnn"].ConnectionString; }
        }

        private static string ies_sysconnn
        {
            get { return ConfigurationManager.ConnectionStrings["ies_sysconnn"].ConnectionString; }
        }

        private static string ies_connn
        {
            get { return ConfigurationManager.ConnectionStrings["ies_connn"].ConnectionString; }
        }


        /// <summary>
        /// 返回一个存储资源的数据库链接
        /// </summary>
        /// <returns></returns>
        public static IDbConnection ResourceService()
        {
            SqlConnection conn = null ;
            try
            {
                conn = new SqlConnection(ies_resourceconn);
                conn.Open();
            }
            catch (Exception e)
            { 
                
            }
            return conn;
        }

        /// <summary>
        /// 师生互动数据库的链接
        /// </summary>
        /// <returns></returns>
        public static IDbConnection CCService()
        {
            SqlConnection conn = new SqlConnection(ies_ccconnn);
            conn.Open();
            return conn;
        }

        /// <summary>
        /// 基础数据的链接
        /// </summary>
        /// <returns></returns>
        public static IDbConnection JWService()
        {
            SqlConnection conn = new SqlConnection(ies_jwconnn);
            conn.Open();
            return conn;
        }

        /// <summary>
        /// 展示门户的数据访问
        /// </summary>
        /// <returns></returns>
        public static IDbConnection PortalService()
        {
            SqlConnection conn = new SqlConnection(ies_portalconnn);
            conn.Open();
            return conn;
        }


        /// <summary>
        /// 系统配置的数据服务
        /// </summary>
        /// <returns></returns>
        public static IDbConnection SysService()
        {
            SqlConnection conn = new SqlConnection(ies_sysconnn);
            conn.Open();
            return conn;
        }

        /// <summary>
        /// 获取通用的数据连接
        /// </summary>
        /// <returns></returns>
        public static IDbConnection CommonService()
        {
            SqlConnection conn = new SqlConnection(ies_connn);
            conn.Open();
            return conn;
        }


        //编译Dapper源码生成的是Net4.0下使用的，可以借助Net4.0新增的dynamic动态类型,
        //SetIdentity的实现将非常方便。如下：
        public static void SetIdentity<T>(IDbConnection conn, Action<T> setId)
        {
            dynamic identity = conn.Query("SELECT @@IDENTITY AS Id").Single();
            T newId = (T)identity.Id;
            setId(newId);
        }

        public static int GetIdentity(IDbConnection conn)
        {
            dynamic identity = conn.Query("SELECT @@IDENTITY AS Id").Single();
            return (int)identity.Id;
        }



        #endregion

    }
}
