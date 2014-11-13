namespace App.Web.Score.DataProvider
{
    using App.Score.Data;
    using App.Score.Db;
    using App.Score.Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Services;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class Util : System.Web.UI.Page
    {
        [WebMethod]
        public static IList<TdNation> GetNations()
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "Select * from tdNation ";
                return bll.FillListByText<TdNation>(sql, null);
            }
        }
        [WebMethod]
        public static IList<TdPolitic> GetPolitics()
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "Select * from tdPolitics ";
                return bll.FillListByText<TdPolitic>(sql, null);
            }
        }
        [WebMethod]
        public static IList<TdResidenceType> GetResidenceType()
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "Select * from tdResidenceType ";
                return bll.FillListByText<TdResidenceType>(sql, null);
            }
        }

        /// <summary>
        /// 获得学年
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static IList<Academicyear> GetAcademicyear()
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "select academicyear as MicYear from tbGradeClass group by academicyear";
                return bll.FillListByText<Academicyear>(sql, null);
            }
        }
        /// <summary>
        /// 获得年级
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static IList<GradeCode> GetGradeCodes()
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "select * from tdGradecode";
                return bll.FillListByText<GradeCode>(sql, null);
            }
        }

        /// <summary>
        /// 获得GradeCourse
        /// </summary>
        /// <param name="flag">-1 全部， 其它值：IsDownLoad=flag</param>
        /// <returns></returns> 
        [WebMethod]
        public static IList<GradeCourse> GetGradeCourse(GradeCode gradeCode, int flag)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "SELECT a.Academicyear,a.GradeNo, a.CourseCode, b.FullName" +
                           " FROM  tbCourseUse a LEFT OUTER JOIN" +
                           " tdCourseCode b ON a.coursecode = b.CourseCode" +
                           " where (@flag=-1 or b.IsDownLoad = @flag)" +
                           " and a.GradeNo=@gradeNo" +
                           " group by a.Academicyear,a.gradeno,a.coursecode, b.FullName";
                return bll.FillListByText<GradeCourse>(sql, new { gradeNo = gradeCode.GradeNo, flag = flag });
            }
        }

        /// <summary>
        /// 获得所有学生
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static IList<Student> GetStudents()
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "Select SRID,stdName from tbStudentBaseInfo where ISdelete=0 order by SRID DESC";
                return bll.FillListByText<Student>(sql, new {});
            }
        }

        /// <summary>
        /// 根据班级获得学生
        /// </summary>
        /// <param name="academicyear">学年</param>
        /// <param name="classcode">班级</param>
        /// <returns></returns>
        [WebMethod]
        public static IList<Student> GetStudent(int academicyear,int classcode)
        {
            using(AppBLL bll = new AppBLL())
            {
                var sql = "Select SRID AS StudentId,StdName from s_vw_ClassStudent" +
                          " Where Academicyear=@academicyear and ClassCode=@classcode";
                return bll.FillListByText<Student>(sql, new { academicyear = academicyear, classcode = classcode });
            }
        }

        [WebMethod]
        public static IList<Student> GetStudentsByGrade(int academicyear, string classNo)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "Select SRID AS StudentId,StdName from s_vw_ClassStudent" +
                          " Where Academicyear=@academicyear and ClassCode in (" + classNo + ")";
                return bll.FillListByText<Student>(sql, new { academicyear = academicyear });
            }
        }

        /// <summary>
        /// 考试类型
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static IList<TestType> GetTestType()
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "select TestType as Code, TypeName as Name from s_tb_testtypeinfo";
                return bll.FillListByText<TestType>(sql, null);
            }
        }

        /// <summary>
        /// 获得考试号
        /// </summary>
        /// <param name="academicyear"></param>
        /// <param name="gradeNo"></param>
        /// <param name="courseCode"></param>
        /// <param name="testType"></param>
        /// <returns></returns>
        [WebMethod]
        public static IList<TestLogin> GetTestLogin(int academicyear, string gradeNo, string courseCode, int testType)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "";
                if (string.IsNullOrEmpty(gradeNo))
                {
                    if (string.IsNullOrEmpty(courseCode))
                    {
                        sql = "select TestLoginNo, AcademicYear, Semester, TestType, TestNo, GradeNo, Coursecode, MarkTypeCode from s_tb_Testlogin where Academicyear=@Academicyear";
                        sql += " and (testtype=@TestType or @TestType = -1)";
                        return bll.FillListByText<TestLogin>(sql, new { Academicyear = academicyear, TestType = testType });
                    }
                    else
                    {
                        sql = "select TestLoginNo, AcademicYear, Semester, TestType, TestNo, GradeNo, Coursecode, MarkTypeCode from s_tb_Testlogin where Academicyear=@Academicyear";
                        sql += " and coursecode in (@CourseCode,'00000')";
                        sql += " and (testtype=@TestType or @TestType = -1)";
                        return bll.FillListByText<TestLogin>(sql, new { Academicyear = academicyear, TestType = testType, CourseCode = courseCode });
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(courseCode))
                    {
                        sql = "select TestLoginNo, AcademicYear, Semester, TestType, TestNo, GradeNo, Coursecode, MarkTypeCode from s_tb_Testlogin where Academicyear=@Academicyear";
                        sql += " and GradeNo in (@GradeNo,'00')";
                        sql += " and (testtype=@TestType or @TestType = -1)";
                        return bll.FillListByText<TestLogin>(sql, new { Academicyear = academicyear, TestType = testType, GradeNo = gradeNo });
                    }
                    else
                    {
                        sql = "select TestLoginNo, AcademicYear, Semester, TestType, TestNo, GradeNo, Coursecode, MarkTypeCode from s_tb_Testlogin where Academicyear=@Academicyear";
                        sql += " and GradeNo in (@GradeNo,'00')";
                        sql += " and coursecode in (@CourseCode,'00000')";
                        sql += " and (testtype=@TestType or @TestType = -1)";
                        return bll.FillListByText<TestLogin>(sql, new { Academicyear = academicyear, TestType = testType, GradeNo = gradeNo, CourseCode = courseCode });
                    }
                }
            }
        }
    }
}