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
    public class CourseTypeDAL
    {
        #region 课程分类
        public static List<Coursetype> CourseType_Tree()
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    return conn.Query<Coursetype>("CourseType_Tree", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception)
            {
                return new List<Coursetype>();
            }
        }
        #endregion

        #region 上级课程分类
        public static List<Coursetype> CourseType_P_List()
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    return conn.Query<Coursetype>("CourseType_P_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception)
            {
                return new List<Coursetype>();
            }
        }
        #endregion

        #region 课程分类删除

        public static bool CourseType_Del(Coursetype model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@CourseTypeID", model.CourseTypeID);
                    conn.Execute("CourseType_Del", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region 编辑
        public static Coursetype CourseType_Edit(Coursetype model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@CourseTypeID",model.CourseTypeID);
                    p.Add("@CourseTypeNo",model.CourseTypeNo);
                    p.Add("@Name",model.Name);
                    p.Add("@ParentID",model.ParentID);
                    p.Add("@Orde",1);
                    p.Add("@op_CourseTypeID", "", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@output", "", dbType: DbType.String, direction: ParameterDirection.Output);
                    conn.Execute("CourseType_Edit", p, commandType: CommandType.StoredProcedure);
                    model.CourseTypeID = p.Get<Int32>("@op_CourseTypeID");
                    model.output = p.Get<string>("@output");
                    return model;
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }
        #endregion

        #region 取消删除
        /// <summary>
        /// 删除学科
        /// </summary>
        public static bool CourseType_CancelDel(Coursetype model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@CourseTypeID", model.CourseTypeID);
                    conn.Execute("CourseType_CancelDel", p, commandType: CommandType.StoredProcedure);
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
