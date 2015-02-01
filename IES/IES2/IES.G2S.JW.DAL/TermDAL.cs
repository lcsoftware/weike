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
        #region  学期类型列表
        /// <summary>
        /// 学期类型列表
        /// </summary>
        /// <returns></returns>
        public static List<TermType> TermType_List()
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    return conn.Query<TermType>("TermType_List", null, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<TermType>();
            }
        }
        #endregion

        #region 校历列表
        /// <summary>
        /// 校历列表
        /// </summary>
        /// <returns></returns>
        public static List<Term> Term_List(Term model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@Key", model.Key);
                    return conn.Query<Term>("Term_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<Term>();
            }
        }

     
        #endregion

        #region  新增或修改
        /// <summary>
        /// 校历添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Term Term_Edit(Term model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@TermID", model.TermID);
                    p.Add("@TermYear", model.TermYear);
                    p.Add("@TermTypeID", model.TermTypeID);
                    p.Add("@StartDate", model.StartDate);
                    p.Add("@EndDate", model.EndDate);
                    p.Add("@op_TermID", "", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    conn.Execute("Term_Edit", p, commandType: CommandType.StoredProcedure);
                    model.op_TermID = p.Get<Int32>("@op_TermID");
                    return model;
                }
            }
            catch (Exception e)
            {
                return new Term();
            }
        }
        #endregion

        #region  删除
        /// <summary>
        /// 校历删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Term_Del(Term model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
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
        #endregion

        #region  根据学期获取课节
        public static List<Lesson> Lesson_List(Term model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@TermID", model.TermID);
                    return conn.Query<Lesson>("Lesson_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<Lesson>(); ;
            }
        }
        #endregion

        #region  校历详细信息
        public static TermInfo TermInfo_Get(Term model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    TermInfo tf = new TermInfo();
                    var p = new DynamicParameters();
                    p.Add("@TermID", model.TermID);
                    var multi = conn.QueryMultiple("TermInfo_Get", p, commandType: CommandType.StoredProcedure);
                    var termd = multi.Read<Term>().Single();
                    var less = multi.Read<Lesson>().ToList();
                    tf.term = termd;
                    tf.lesslist = less;
                    return tf;
                }
            }
            catch (Exception e)
            {
                return new TermInfo(); ;
            }
        }
        #endregion

        #region  Lesson新增或修改
        /// <summary>
        /// 课节添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Lesson Lesson_Edit(Lesson model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@LessonID", model.LessonID);
                    p.Add("@TermID", model.TermID);
                    p.Add("@LessonName", model.LessonName);
                    p.Add("@StartTime", model.StartTime);
                    p.Add("@EndTime", model.EndTime);
                    p.Add("@Duration", model.Duration);
                    conn.Execute("Lesson_Edit", p, commandType: CommandType.StoredProcedure);
                    model.LessonID = p.Get<int>("@LessonID");
                    return model;
                }
            }
            catch (Exception e)
            {
                return new Lesson();
            }
        }
        #endregion

        #region  Lesson删除
        /// <summary>
        /// 课节删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Lesson_Del(Lesson model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@LessonID", model.LessonID);
                    conn.Execute("Lesson_Del", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion
    }
}
