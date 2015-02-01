using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using IES.JW.Model;
using IES.DataBase;
using Dapper;

namespace IES.G2S.JW.DAL
{
    public class LogDAL
    {
        #region 列表
        public static List<Log> Log_List(Log model, int PageIndex, int PageSize)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@Key", model.Key);
                    p.Add("@Role", model.Role);
                    p.Add("@StartTime", model.StartTime);
                    p.Add("@EndTime", model.EndTime);
                    p.Add("@PageSize", PageSize);
                    p.Add("@PageIndex", PageIndex);

                    return conn.Query<Log>("Log_List", p, commandType: CommandType.StoredProcedure).ToList();
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
