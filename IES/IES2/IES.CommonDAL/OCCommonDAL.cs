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
using IES.SYS.Model;
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
       

    }
}
