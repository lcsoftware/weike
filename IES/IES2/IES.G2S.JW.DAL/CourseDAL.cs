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
        public static List<Course> Course_List(string Key,Course model, int PageIndex,int PageSize)
        {
            try
            {
                using (var conn=DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    //p.Add("@CourseNo", model.CourseNo);
                    //p.Add("@CourseName", model.CourseName);
                    p.Add("@Key", Key);
                    p.Add("@OrganizationID", model.OrganizationID);
                    p.Add("@CourseTypeID", model.CourseTypeID);
                    p.Add("@TeachingTypeID", model.TeachingTypeID);
                    p.Add("@SubjectID1", model.SubjectID1);
                    p.Add("@SubjectID2", model.SubjectID2);
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

        //public static CourseInfo CourseInfo_Get(ICourse model)
        //{
        //    try
        //    {
        //        using (IDbConnection conn=new DynamicParameters())
        //        {
        //            CourseInfo cf = new CourseInfo();
        //            var p = new DynamicParameters();
        //            p.Add("@CourseID", model.CourseID);
        //            var multi = conn.QueryMultiple("CourseInfo_Get", p, commandType: CommandType.StoredProcedure);
        //            var classs = multi.Read<Course>().Single();
        //            var classroomlist = multi.Read<Classroom>().ToList();
        //            cf.classcommon.classs = classs;
        //            cf.classcommon.classroom = classroomlist;
        //            return cf;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

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

        #region 对象更新

        public static bool Course_Upd(Course model)
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
                    p.Add("@OrganizationID", model.OrganizationID);
                    p.Add("@SubjectID1", model.SubjectID1);
                    p.Add("@SubjectID2", model.SubjectID2);
                    p.Add("@CourseType", model.CourseTypeID);
                    p.Add("@Hours", model.Hours);
                    p.Add("@Credit", model.Credit);
                    p.Add("@TeachingTypeID", model.TeachingTypeID);
                    conn.Execute("Course_Upd", p, commandType: CommandType.StoredProcedure);
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
    }
}
