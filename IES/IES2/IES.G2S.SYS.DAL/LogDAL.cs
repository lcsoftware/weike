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
    public class LogDAL
    {
        #region 列表
        public static List<Log> Log_List(Log model, int PageIndex, int PageSize)
        {
            try
            {
                using (var conn = DbHelper.SysService())
                {
                    var p = new DynamicParameters();
                    p.Add("@LoginName", model.Loginname);
                    p.Add("@UserNo", model.Userno);
                    p.Add("@Role", model.Role);                   
                    p.Add("@StartDate", model.Startdate);
                    p.Add("@EndDate", model.Enddate);
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
