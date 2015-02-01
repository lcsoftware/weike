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
   public  class CommonDAL
    {

        /// <summary>
        /// 获取系统的字典表
        /// </summary>
        /// <returns></returns>
        public static List<IES.JW.Model.Dict> Dict_List()
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    return conn.Query<Dict>("Dict_List", null, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
