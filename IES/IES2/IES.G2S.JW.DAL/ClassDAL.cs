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
    /// 班级管理
    /// </summary>
    public  class ClassDAL
    {
        #region  列表
        public static List<Class> Class_List(Class model, int PageIndex,int PageSize)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@Key", model.Key);
                    p.Add("@OrganizationID", model.OrganizationID);
                    p.Add("@StartTime", model.StartTime);
                    p.Add("@EndTime", model.EndTime);
                    p.Add("@PageSize", PageSize);
                    p.Add("@PageIndex", PageIndex);
                    return conn.Query<Class>("Class_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<Class>();
            }
        }
 
        #endregion

        #region 详细信息
        /// <summary>
        /// 获取行政班的详细信息
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public static Class Class_Get(int ClassID)
        {
            try
            {
                using (var conn=DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ClassID", ClassID);
                    return conn.Query<Class>("Class_Get", p, commandType: CommandType.StoredProcedure).Single();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// 获取行政班下的学生列表
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public static List<User> ClassStudent_List(int ClassID)
        {
            try
            {
                using (var conn=DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ClassID", ClassID);
                    return conn.Query<User>("ClassStudent_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        #endregion

        #region 对象修改或删除
        public static Class Class_Edit(Class model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ClassID", model.ClassID);
                    p.Add("@ClassNo", model.ClassNo);
                    p.Add("@ClassName", model.ClassName);
                    p.Add("@OrganizationID", model.OrganizationID);
                    p.Add("@SpecialtyID", model.SpecialtyID);
                    p.Add("@TeacherID", model.TeacherID);
                    p.Add("@EntryDate", model.EntryDate);
                    p.Add("@output","", dbType: DbType.String, direction: ParameterDirection.Output);
                    p.Add("@op_ClassID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    conn.Execute("Class_Edit", p, commandType: CommandType.StoredProcedure);
                    model.output = p.Get<string>("@output");
                    model.op_ClassID = p.Get<int>("@op_ClassID");
                    return model;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        //修改行政班下学生
        public static bool ClassStudent_Save(Class model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ClassID", model.ClassID);
                    p.Add("@StudentIDs", model.StudentIDs);
                    conn.Execute("ClassStudent_Save", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception)
            {
                return false ;
            }
        }
        #endregion

        #region 单个批量更新
       



        #endregion

        #region 批量删除
        /// <summary>
        /// 行政班数据批量操作
        /// </summary>
        /// <returns></returns>
        public static bool Class_Batch_Del( string IDS  )
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ClassIDS", IDS );
                    conn.Execute("Class_Batch_Del", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        #endregion

        #region 删除
        public static bool Class_Del(Class model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ClassID", model.ClassID);
                    conn.Execute("Class_Del", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

    }
}
