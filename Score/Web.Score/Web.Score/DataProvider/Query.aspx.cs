using App.Score.Data;
using App.Score.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace App.Web.Score.DataProvider
{
    public partial class Query : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 获得课程
        /// </summary>
        /// <param name="micyear">学年</param>
        /// <param name="teacherid">教师ID</param>
        /// <returns></returns> 
        [WebMethod]
        public static IList<GradeCourse> GetGradeCourseByTeacherId(int micyear, string teacherid)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "select a.CourseCode,b.FullName from tbTeacherClass as A,tdCourseCode as B" +
                           " where A.CourseCode=B.CourseCode and teacherid=@teacherid" +
                           " and  a.Academicyear=@micyear" +
                           " group by A.coursecode,b.FullName";
                return bll.FillListByText<GradeCourse>(sql, new { micyear = micyear, teacherid = teacherid });
            }
        }

        /// <summary>
        /// 获得班级
        /// </summary>
        /// <param name="micyear"></param>
        /// <param name="teacherid"></param>
        /// <returns></returns>
        [WebMethod]
        public static IList<GradeCode> GetGradeCodeByTeacherId(int micyear, string teacherid)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "SELECT a.ClassID as GradeNo, c.GradeBriefName+'('+substring(a.ClassID,3,2)+')班' as GradeName" +
                           " FROM tbTeacherClass a INNER JOIN tbGradeClass b ON a.AcademicYear = b.AcadEmicYear AND " +
                           " a.ClassID = b.ClassNo INNER JOIN tdGradeCode c ON b.GradeNo = c.GradeNo" +
                           " where a.AcademicYear=@micyear and a.teacherID=@teacherid Group by a.ClassID,c.GradeBriefName";
                return bll.FillListByText<GradeCode>(sql, new { micyear = micyear, teacherid = teacherid });
            }
        }

        [WebMethod]
        public static IList<TestLogin> GetTestNo(int micyear, int testtype)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "Select testno from s_tb_Testlogin where Academicyear=@micyear" +
                           " and testtype=@testtype ";
                return bll.FillListByText<TestLogin>(sql, new { micyear = micyear, testtype = testtype });
            }
        }

        /// <summary>
        /// 获得任课教师成绩
        /// </summary>
        /// <param name="micyear">学年</param>
        /// <param name="teacherid">教师ID</param>
        /// <param name="gradeCourse">课程</param>
        /// <param name="gradecode">年级</param>
        /// <param name="testtypes">考试类型</param>
        /// <param name="testno">考试号</param>
        /// <param name="stuId">学生ID</param>
        /// <returns></returns>
        [WebMethod]
        public static string GetQueryTeacher(int? micyear, string teacherid, int? gradeCourse, int? gradecode, int? testtypes, int? testno, string stuId)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "Select GradeName+'('+substring(classCode,3,2)+')班' as class," +
                            "ClassSN," +
                            "StdName," +
                            "NumScore," +
                            "LevelScore," +
                            "StandardScore ," +
                            "GradeOrder," +
                            "ClassOrder," +
                            "markName ," +
                            "TypeName ," +
                            "testno ," +
                            "SchoolID ," +
                            "courseName, " +
                            "markcode " +
                            " from s_vw_ClassScoreNum left join S_tb_SchoolID on s_vw_ClassScoreNum.SRID=S_tb_SchoolID.SRID" +
                            " Where AcademicYear=@micyear" +
                            " and ClassCode=@gradecode and CourseCode=@gradeCourse" +
                            " and  TeacherID=@teacherid";
                if (testtypes != null) sql += " and TestType=" + testtypes + " ";
                if (testno != null) sql += " and TestNo=" + testno + "";
                if (stuId != "") sql += " and s_vw_ClassScoreNum.SRID in(" + stuId + ")";
                if (stuId != "")
                    sql += " Order By ClassCode,ClassSN";
                else
                    sql += " Order By Testno,NumScore DESC";
                return JsonConvert.SerializeObject(bll.FillDataTableByText(sql,
                    new
                    {
                        micyear = micyear,
                        teacherid = teacherid,
                        gradeCourse = gradeCourse,
                        gradecode = gradecode
                    }));
            }
        }
        /// <summary>
        /// 获得班主任成绩
        /// </summary>
        /// <param name="micyear">学年</param>
        /// <param name="gradeCourse">课程</param>
        /// <param name="gradecode">年级</param>
        /// <param name="testtypes">考试类型</param>
        /// <param name="testno">考试号</param>
        /// <param name="stuId">学生ID</param>
        /// <returns></returns>
        [WebMethod]
        public static string GetQueryBTeacher(int? micyear, string gradeCourse, int? gradecode, int? testtypes, int? testno, string stuId, int? order)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "Select GradeName+'('+substring(classCode,3,2)+')班' as class," +
                            "ClassSN," +
                            "CourseName," +
                            "TypeName," +
                            "SRID," +
                            "StdName," +
                            "NumScore," +
                            "LevelScore," +
                            "StandardScore," +
                            "GradeOrder," +
                            "ClassOrder," +
                            "markName, " +
                            "markcode, " +
                            "teacherName,ClassCode,Testno " +
                            "from s_vw_ClassScoreNum " +
                            " Where ClassCode=@gradecode " +
                            " and Academicyear=@micyear";
                if (!string.IsNullOrEmpty(gradeCourse)) sql += " and CourseCode in (" + gradeCourse + ")";
                if (testtypes != null) sql += " and TestType=" + testtypes + " ";
                if (testno != null) sql += " and TestNo=" + testno + "";
                if (stuId != "") sql += " and SRID in(" + stuId + ")";
                if (order == null)
                    sql += " Order By Testno,NumScore desc";
                else if (order == 0)
                    sql += " Order By classSN";
                else
                    sql += " Order By NumScore desc";
                return JsonConvert.SerializeObject(bll.FillDataTableByText(sql,
                    new
                    {
                        gradecode = gradecode,
                        micyear = micyear
                    }));
            }
        }
        /// <summary>
        /// 获得年级领导成绩
        /// </summary>
        /// <param name="micyear">学年</param>
        /// <param name="gradeCourse">课程</param>
        /// <param name="gradecode">年级</param>
        /// <param name="testtypes">考试类型</param>
        /// <param name="testno">考试号</param>
        /// <param name="classCode">班级号</param>
        /// <param name="stuId">学生ID</param>
        /// <returns></returns>
        [WebMethod]
        public static string GetQueryGradeManager(int? micyear, string gradeCourse, int? gradecode, int? testtypes, int? testno, string classCode, string stuId, int? order)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "Select GradeName+'('+substring(Classcode,3,2)+')班' as class," +
                             "ClassSN ," +
                             "stdName , " +
                             "CourseName ," +
                             "TypeName ," +
                             "TestNo ," +
                             "NumScore ," +
                             "GradeOrder," +
                             "ClassOrder," +
                             "teacherName," +
                             "markcode, " +
                             "ClassCode " +
                             "from s_vw_ClassScoreNum " +
                             " Where Academicyear=@micyear ";
                if (gradecode != null) sql += "and GradeNo=" + gradecode + "";
                if (!string.IsNullOrEmpty(gradeCourse)) sql += " and CourseCode in (" + gradeCourse + ")";
                if (testtypes != null) sql += " and TestType=" + testtypes + " ";
                if (testno != null) sql += " and TestNo=" + testno + "";
                if (classCode != "") sql += " and ClassCode in(" + classCode + ")";
                if (stuId != null) sql += " and SRID =" + stuId + "";
                if (order == 1)
                    sql += " Order By Testno,NumScore DESC,courseName";
                else
                    sql += " Order By ClassCode,ClassSN,courseName";
                return JsonConvert.SerializeObject(bll.FillDataTableByText(sql,
                    new
                    {
                        micyear = micyear
                    }));
            }
        }

        /// <summary>
        /// 获得教务员成绩
        /// </summary>
        /// <param name="micyear">学年</param>
        /// <param name="gradeCourse">课程</param>
        /// <param name="gradecode">年级</param>
        /// <param name="testtypes">考试类型</param>
        /// <param name="testno">考试号</param>
        /// <param name="classCode">班级号</param>
        /// <param name="stuId">学生ID</param>
        /// <returns></returns>
        [WebMethod]
        public static string GetQuerySchoolManager(int? micyear, string gradeCourse, int? gradecode, int? testtypes, int? testno, string classCode, string stuId, string teacherId, int? order)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "Select GradeName+'('+substring(Classcode,3,2)+')班' as class," +
                             "ClassSN ," +
                             "stdName , " +
                             "CourseName ," +
                             "TypeName ," +
                             "TestNo ," +
                             "NumScore ," +
                             "GradeOrder," +
                             "ClassOrder," +
                             "teacherName," +
                             "markcode, " +
                             "ClassCode " +
                             "from s_vw_ClassScoreNum " +
                             " Where Academicyear=@micyear ";
                if (gradecode != null) sql += "and GradeNo=" + gradecode + "";
                if (!string.IsNullOrEmpty(gradeCourse)) sql += " and CourseCode in (" + gradeCourse + ")";
                if (testtypes != null) sql += " and TestType=" + testtypes + " ";
                if (testno != null) sql += " and TestNo=" + testno + "";
                if (classCode != "") sql += " and ClassCode in(" + classCode + ")";
                if (stuId != null) sql += " and SRID =" + stuId + "";
                if (!string.IsNullOrEmpty(teacherId)) sql += " and TeacherID=" + teacherId + "";
                if (order == 1)
                    sql += " Order By Testno,NumScore DESC,courseName";
                else
                    sql += " Order By ClassCode,ClassSN,courseName";
                return JsonConvert.SerializeObject(bll.FillDataTableByText(sql,
                    new
                    {
                        micyear = micyear
                    }));
            }
        }

        //获得年级领导年级
        [WebMethod]
        public static string GetGradeScope(string teacherId)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "Select scope,GradeName from s_tb_teacherscope,tdGradeCode " +
                          "Where teachertype=3 and teacherID=@teacherId and scope= Gradeno";
                return JsonConvert.SerializeObject(bll.FillDataTableByText(sql, new { teacherId = teacherId }));
            }
        }

        

        //获得班主任年级
        [WebMethod]
        public static string GetScope(string teacherId)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "select * from s_tb_teacherscope where teacherid=@teacherId and teachertype=2";
                return JsonConvert.SerializeObject(bll.FillDataTableByText(sql, new { teacherId = teacherId }));
                
            }
        }
        //获得班主任课程
        [WebMethod]
        public static IList<GradeCourse> GetBCourse(int teacherScope)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "select a.Coursecode as CourseCode,b.BriefName as FullName from tbCourseUse a,tdCourseCode b " +
                    "Where a.coursecode=b.coursecode and SubString(a.CourseCode,2,1) =1 and a.GradeNo=@teacherScope group by a.Coursecode,BriefName";
                return bll.FillListByText<GradeCourse>(sql, new { teacherScope = teacherScope });
            }
        }
        //根据年级获得该年级的所有班级
        [WebMethod]
        public static string GetGradeByGradeNo(int micyear, int gradeNo)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "select GradeBriefName+'('+substring(tbGradeClass.classNo,3,2)+')班' AS GradeBriefName,classNo " +
                    "from tdGradeCode,tbGradeClass WHERE tbGradeClass.Gradeno=tdGradeCode.GradeNo " +
                    "AND tbGradeClass.academicYear=@micyear and tbGradeClass.GradeNo=@gradeNo";
                return JsonConvert.SerializeObject(bll.FillDataTableByText(sql, new { micyear = micyear, gradeNo = gradeNo }));
            }
        }

    }
}