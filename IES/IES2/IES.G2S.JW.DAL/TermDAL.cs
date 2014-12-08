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
    /// <summary>
    /// 校历 Term ,  TermType（不维护，系统配置）
    /// </summary>
    public class TermDAL
    {

        /// <summary>
        /// 学期类型列表
        /// </summary>
        /// <returns></returns>
        public static List<TermType> TermType_List()
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    return conn.Query<TermType>("TermType_List", null, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<TermType>();
            }
        }

        /// <summary>
        /// 校历列表
        /// </summary>
        /// <returns></returns>
        public static List<Term> Term_List()
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    return conn.Query<Term>("Term_List", null, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<Term>();
            }
        }




        /// <summary>
        /// 校历添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Term Term_ADD(Term model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@TermID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@TermNo", model.TermNo);
                    p.Add("@TermYear", model.TermYear);
                    p.Add("@TermTypeID", model.TermTypeID);
                    p.Add("@StartDate", model.StartDate);
                    p.Add("@EndDate", model.EndDate);
                    conn.Execute("Term_ADD", p, commandType: CommandType.StoredProcedure);

                    model.TermID = p.Get<int>("TermID");
                    return model;
                }
            }
            catch (Exception e)
            {
                return model;
            }
        }

        /// <summary>
        /// 校历删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Term_Del(Term model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@TermID", model.TermID);
                    conn.Execute("Term_Del", p, commandType: CommandType.StoredProcedure);
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
