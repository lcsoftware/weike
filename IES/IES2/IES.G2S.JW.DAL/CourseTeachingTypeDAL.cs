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
    public class CourseTeachingTypeDAL
    {

        #region 授课方式列表
        /// <summary>
        /// 授课方式列表
        /// </summary>
        public static List<CourseTeachingType> CourseTeachingType_List()
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    return conn.Query<CourseTeachingType>("CourseTeachingType_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增一条数据
        /// </summary>
        public static CourseTeachingType CourseTeachingType_ADD(CourseTeachingType model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    conn.Execute("CourseTeachingType_ADD", p, commandType: CommandType.StoredProcedure);
                    return model;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool CourseTeachingType_Upd(CourseTeachingType model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    conn.Execute("CourseTeachingType_Upd", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool CourseTeachingType_Del(CourseTeachingType model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    conn.Execute("CourseTeachingType_Del", p, commandType: CommandType.StoredProcedure);
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
