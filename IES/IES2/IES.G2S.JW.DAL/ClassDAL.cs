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
        public static List<Class> Class_List(Class model, int userID,  int PageIndex,int PageSize)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    //p.Add("@ClassID", model.ClassID);
                    p.Add("@classNo", model.ClassNo);
                    p.Add("@className", model.ClassName);
                    p.Add("@userID", userID);
                    //p.Add("@OrganizationID", model.OrganizationID);
                    //p.Add("@SpecialtyID", model.SpecialtyID);
                    //p.Add("@TeacherID", model.TeacherID);
                    //p.Add("@EntryDate", model.EntryDate);
                    //p.Add("@Classroom", classroom.ClassroomID);
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

        public static IClass Class_Get(IClass model)
        {
            return new ClassCommon();
        }

        public static ClassInfo ClassInfo_Get(IClass model)
        {
            try
            {
                using (IDbConnection conn = DbHelper.JWService())
                {
                    ClassInfo cf = new ClassInfo();
                    var p = new DynamicParameters();
                    p.Add("@ClassID", model.ClassID);
                    var multi = conn.QueryMultiple("ClassInfo_Get", p, commandType: CommandType.StoredProcedure);
                    var classs = multi.Read<Class>().Single();
                    var classroomlist = multi.Read<Classroom>().ToList();
                    cf.classcommon.classs = classs;
                    cf.classcommon.classroom = classroomlist;
                    return cf;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region  新增

        /// <summary>
        /// 新增行政班
        /// </summary>
        public static Class Class_ADD(Class model)
        {
            try
            {
                using (var conn=DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ClassID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@ClassNo", model.ClassNo);
                    p.Add("@ClassName", model.ClassName);
                    p.Add("@OrganizationID", model.OrganizationID);
                    p.Add("@TeacherID", model.TeacherID);
                    p.Add("@EntryDate", DateTime.Now.ToLongDateString().ToString());
                    conn.Execute("Class_ADD", p, commandType: CommandType.StoredProcedure);
                    model.ClassID = p.Get<int>("ClassID");
                    return model;
                }
            }
            catch (Exception)
            {                
                return null ;
            }

        }


        #endregion

        #region 对象更新
        public static bool Class_Upd(Class model ,out string opt )
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
                    p.Add("@TeacherID", model.TeacherID);
                    p.Add("@EntryDate", model.EntryDate);
                    p.Add("@output", dbType: DbType.String, direction: ParameterDirection.Output);
                    conn.Execute("Class_Upd", p, commandType: CommandType.StoredProcedure);
                    opt = p.Get<string>("output");
                    return true;
                }
            }
            catch (Exception)
            {
                opt = "操作失败";
                return false;
            }
        }


        #endregion

        #region 单个批量更新
       



        #endregion

        #region 属性批量操作
        /// <summary>
        /// 行政班数据批量操作
        /// </summary>
        /// <returns></returns>
        public static bool Class_Batch_Del()
        {
            return true;
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
