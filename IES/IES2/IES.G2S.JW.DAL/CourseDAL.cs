using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using IES.JW.Model;
using Dapper;
using IES.DataBase;

namespace IES.G2S.JW.DAL
{
    public class CourseDAL
    {
        #region  列表
        public static List<Course> Course_List(Course model, int PageIndex,int PageSize)
        {
            try
            {
                using (var conn=DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@Key", model.Key);
                    p.Add("@TermTypeID", model.TermTypeID);
                    p.Add("@OrganizationID", model.OrganizationID);
                    p.Add("@CourseTypeID", model.CourseTypeID);
                    p.Add("@TeachingTypeID", model.TeachingTypeID);
                    p.Add("@SubjectID1", model.SubjectID1);
                    p.Add("@SubjectID2", model.SubjectID2);
                    p.Add("@BeginFen", model.BeginFen);
                    p.Add("@EndFen", model.EndFen);
                    p.Add("@PageIndex", PageIndex);
                    p.Add("@PageSize", PageSize);
                    return conn.Query<Course>("Course_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception)
            {
                return new List<Course>();
            }
        }

        #endregion

        #region 详细信息

        public static Course Course_Get(int CourseID)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@CourseID", CourseID);
                    return conn.Query<Course>("Course_Get", p, commandType: CommandType.StoredProcedure).Single();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region  新增

        public static Course Course_ADD(Course model)
        {
            try
            {
                using (var conn=DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@CourseID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@CourseNo", model.CourseNo);
                    p.Add("@CourseName", model.CourseName);
                    p.Add("@CourseNameEn", model.CourseNameEn);
                    p.Add("@OrganizationID", model.OrganizationID);
                    p.Add("@SubjectID1", model.SubjectID1);
                    p.Add("@SubjectID2", model.SubjectID2);
                    p.Add("@CourseType", model.CourseTypeID);
                    p.Add("@Hours", model.Hours);
                    p.Add("@Credit", model.Credit);
                    p.Add("@TeachingTypeID", model.TeachingTypeID);
                    conn.Execute("Course_ADD", p, commandType: CommandType.StoredProcedure);
                    model.CourseID = p.Get<int>("CourseID");
                    return model;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        #endregion

        #region 对象新增或更新

        public static Course Course_Edit(Course model)
        {
            try
            {
                using (var conn=DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@CourseID", model.CourseID);
                    p.Add("@CourseNo", model.CourseNo);
                    p.Add("@CourseName", model.CourseName);
                    p.Add("@CourseNameEn", model.CourseNameEn);
                    p.Add("@TermTypeID", model.TermTypeID);
                    p.Add("@OrganizationID", model.OrganizationID);
                    p.Add("@SubjectID1", model.SubjectID1);
                    p.Add("@SubjectID2", model.SubjectID2);
                    p.Add("@CourseTypeID", model.CourseTypeID);
                    p.Add("@Hours", model.Hours);
                    p.Add("@Credit", model.Credit);
                    p.Add("@TeachingTypeID", model.TeachingTypeID);
                    p.Add("@Introduction", model.Introduction);
                    p.Add("@OutLine", model.OutLine);
                    p.Add("@Team", model.Team);
                    p.Add("@Schedule", model.Schedule);
                    p.Add("@output","", dbType: DbType.String, direction: ParameterDirection.Output);
                    p.Add("@op_CourseID", "",dbType: DbType.Int32, direction: ParameterDirection.Output);
                    conn.Execute("Course_Edit", p, commandType: CommandType.StoredProcedure);
                    model.output = p.Get<string>("@output");
                    model.op_CourseID = p.Get<int>("@op_CourseID");
                    return model;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion       

        #region 单个批量更新





        #endregion

        #region 属性批量操作





        #endregion

        #region 删除

        public static bool Course_Del(Course model)
        {
            try
            {
                using (var conn=DbHelper.JWService() )
                {
                    var p = new DynamicParameters();
                    p.Add("@CourseID", model.CourseID);
                    conn.Execute("Course_Del", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region 批量删除
        public static bool Course_Batch_Del(string IDS)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@CourseIDS", IDS);
                    conn.Execute("Course_Batch_Del", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        #endregion

        #region  获取全部信息

        public static List<CourseTeachingType> NewTeacherType_List()
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();

                    return conn.Query<CourseTeachingType>("NewTeacherType_List", p, commandType: CommandType.StoredProcedure).ToList();
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
