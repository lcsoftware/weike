using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

using Dapper;
using IES.DataBase;
using IES.JW.Model;

using IES.CC.OC.Model;

namespace IES.CommonDAL
{
    public class OCCommonDAL
    {

        /// <summary>
        /// 获取我的在线课程列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<OC> My_OC_List(User model)
        {
            try
            {
                using (var conn = DbHelper.SysService())
                {
                    var p = new DynamicParameters();
                    p.Add("@userid", model.UserID);
                    p.Add("@role", model.UserType);
                    return conn.Query<OC>("My_OC_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }







        /// <summary>
        /// 获取数据库表相关记录的最新版本，缓存支持服务
        /// </summary>
        /// <param name="dbname">数据库名称</param>
        /// <param name="tablename">表名称</param>
        /// <param name="fieldname">字段名称</param>
        /// <param name="fieldvalue">字段的值</param>
        /// <returns></returns>
        public static string IES_Table_Version(string dbname, string tablename, string fieldname, string fieldvalue)
        {
            try
            {
                using ( var conn = GetDbConnection(dbname) )
                {
                    var p = new DynamicParameters();
                    p.Add("@tbname", tablename );
                    p.Add("@fieldname", fieldname );
                    p.Add("@fieldvalue", fieldvalue);
                    return conn.Query<string>("IES_Table_Version", p, commandType: CommandType.StoredProcedure).ToList()[0];
                }
            }
            catch (Exception e)
            {
                return string.Empty ;
            }
        }


        private  static IDbConnection GetDbConnection(string dbname)
        {
            if (dbname.ToUpper() == "IES_CC")
                return DbHelper.CCService();
            if (dbname.ToUpper() == "IES_JW")
                return DbHelper.JWService();
            if (dbname.ToUpper() == "IES_Portal")
                return DbHelper.PortalService();
            if (dbname.ToUpper() == "IES_Resource")
                return DbHelper.ResourceService();
            if (dbname.ToUpper() == "IES")
                return DbHelper.CommonService();
            else
                return DbHelper.CCService();
        }

    }
}
