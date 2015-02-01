using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using IES.JW.Model;
using Dapper;
using IES.DataBase;

namespace IES.G2S.JW.DAL
{
    /// <summary>
    /// TeachingClass ,TeachingClassStudent , TeachingClassTeacher
    /// </summary>
    public  class TeachingClassDAL
    {

        #region  列表

        public static List<TeachingClass> TeachingClass_List(TeachingClass model, int PageIndex, int PageSize)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@Searchkey", model.Key);
                    p.Add("@OrganizationID", model.OrganizationID);
                    p.Add("@StartDate", model.StartTime);
                    p.Add("@EndDate", model.EndTime);
                    p.Add("@PageSize", PageSize);
                    p.Add("@PageIndex", PageIndex);
                    return conn.Query<TeachingClass>("TeachingClass_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<TeachingClass>();
            }
        }

        //获取添加的学生信息
        public static List<TeachingClassStudent> TeachingClassStudent_List(int TeachingClassID, string StudentIDs, int PageIndex, int PageSize)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@TeachingClassID", TeachingClassID);
                    p.Add("@StudentIDs", StudentIDs);
                    p.Add("@PageSize", PageSize);
                    p.Add("@PageIndex", PageIndex);
                    return conn.Query<TeachingClassStudent>("TeachingClassStudent_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<TeachingClassStudent>();
            }
        }


        public static TeachingClassStudent TeachingClassStudent_Edit(TeachingClassStudent model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@UserIDS", model.UserIDS);
                    p.Add("@TeachingClassID",model.TeachingClassID);
                    conn.Execute("TeachingClassStudent_Edit", p, commandType: CommandType.StoredProcedure);
                    return model;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region 详细信息


        public static TeachingClassInfo TeachingClassInfo_Get(TeachingClassInfo model)
        {

            try
            {
                using (IDbConnection conn = DbHelper.JWService())
                {
                    TeachingClassInfo tc = new TeachingClassInfo();
                    var p = new DynamicParameters();
                    p.Add("@TeachingClassID", model.TeachingClassID);

                    var multi = conn.QueryMultiple("TeachingClassInfo_Get", p, commandType: CommandType.StoredProcedure);

                    var teachingclass = multi.Read<TeachingClass>().Single();
                    var teachingclassstudentlist = multi.Read<TeachingClassStudent>().ToList();
                    var teachingclassteacherlist = multi.Read<TeachingClassTeacher>().ToList();

                    tc.teachingclass = teachingclass ;
                    tc.teachingclassstudentlist = teachingclassstudentlist ;
                    tc.teachingclassteacherlist = teachingclassteacherlist;
  
                    return tc ;
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }


        #endregion

        #region  新增

        public static TeachingClass TeachingClass_ADD(TeachingClass model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@TeachingClassID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@ClassNo", model.ClassNo);
                    p.Add("@ClassName", model.ClassName);
                    p.Add("@StartDate", model.StartDate);
                    p.Add("@EndDate", model.EndDate);
                    p.Add("@CourseID", model.CourseID);
                    p.Add("@TermID", model.TermID);
                    p.Add("@OrganizationID", model.OrganizationID);
                    p.Add("@Source", model.Source);
                    p.Add("@MainUserID", model.MainUserID);
                    p.Add("@OtherUserIDS", model.OtherUserIDS);
                    conn.Execute("TeachingClass_ADD", p, commandType: CommandType.StoredProcedure);
                    model.TeachingClassID = p.Get<Int32>("@TeachingClassID");
                    return model;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region 对象更新

        public static bool TeachingClass_Upd(TeachingClass model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@TeachingClassID", model.TeachingClassID);
                    p.Add("@ClassName", model.ClassName);
                    p.Add("@StartDate", model.StartDate);
                    p.Add("@EndDate", model.EndDate);
                    p.Add("@CourseID", model.CourseID);
                    p.Add("@TermID", model.TermID);
                    p.Add("@OrganizationID", model.OrganizationID);
                    p.Add("@MainUserID", model.MainUserID);
                    p.Add("@OtherUserIDS", model.OtherUserIDS);
                    conn.Execute("TeachingClass_Upd", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region 单个批量更新





        #endregion

        #region 属性批量操作





        #endregion

        #region 删除

        public static bool TeachingClass_Del(TeachingClass model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@TeachingClassID", model.TeachingClassID);
                    conn.Execute("TeachingClass_Del", p, commandType: CommandType.StoredProcedure);
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
