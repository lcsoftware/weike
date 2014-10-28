/* **************************************************************
  * Copyright(c) 2014 Score.web, All Rights Reserved.   
  * File             : School.aspx.cs
  * Description      : 学校相关数据处理
  * Author           : shujianhua 
  * Created          : 2014-10-05  
  * Revision History : 
******************************************************************/
namespace App.Web.Score.DataProvider
{
    using App.Score.Data;
    using App.Score.Db;
    using App.Score.Entity;
    using App.Score.Util;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web;
    using System.Web.Services;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    public partial class School : System.Web.UI.Page
    {
        //private const string COOKIE_NAME = "ScoreSchool";
        [WebMethod]
        public static SchoolBaseInfo LoadSchool()
        {
            //string schoolCookieValue = CookieHelper.GetCookieValue(COOKIE_NAME);
            //if (!string.IsNullOrEmpty(schoolCookieValue))
            //{
            //    return Newtonsoft.Json.JsonConvert.DeserializeObject<SchoolBaseInfo>(COOKIE_NAME);
            //}
            using (AppBLL bll = new AppBLL())
            {
                var sql = "select * FROM tbSchoolBaseInfo";
                var schools = bll.FillListByText<SchoolBaseInfo>(sql, null);
                if (schools.Any())
                {
                    SchoolBaseInfo schoolInfo = schools.First();
                    //CookieHelper.SetCookie(COOKIE_NAME, Newtonsoft.Json.JsonConvert.SerializeObject(schoolInfo), DateTime.Now.AddDays(1));
                    return schoolInfo;
                }
                return null;
            }
        }

        private static IList<GradeCode> BuildGrades(IList<GradeAndClass> ClassList)
        {
            IList<GradeCode> gradeList = new List<GradeCode>();
            foreach (var gradeAndClass in ClassList)
            {
                if (!gradeList.Contains(gradeAndClass.Grade))
                {
                    gradeList.Add(gradeAndClass.Grade);
                }
            }
            return gradeList;
        }

        private static void BuildGradeClass(GradeCode grade, IList<GradeAndClass> ClassList)
        {
            var gradeClasses = from v in ClassList where v.GradeNo == grade.GradeNo select v;
            foreach (var gradeClass in gradeClasses)
            {
                if (!grade.GradeClasses.Contains(gradeClass.GClass))
                {
                    grade.GradeClasses.Add(gradeClass.GClass);
                }
            }
        }

        [WebMethod]
        public static IList<GradeCode> LoadGradeClass(int academicYear, bool andStudent)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "select a.SystemID GradeSystemID, a.GradeNo,a.GradeName, a.GradeBriefName," +
                            " b.SystemID as ClassSystemID, b.ClassNo, b.AcadEmicYear, b.ClassType, b.IsDelete" +
                            " from tdGradeCode a left join tbGradeClass b on a.GradeNo=b.GradeNo and b.AcademicYear=@Year AND b.ClassType='0'" +
                            " ORDER BY a.GradeNo, b.ClassNo";
                IList<GradeAndClass> ClassList = bll.FillListByText<GradeAndClass>(sql, new { Year = academicYear });
                IList<GradeCode> gradeList = BuildGrades(ClassList);
                foreach (var grade in gradeList)
                {
                    BuildGradeClass(grade, ClassList);
                }
                if (andStudent)
                {
                    sql = "SELECT tbStudentClass.SRID StudentId," +
                        " tbStudentBaseInfo.StdName StdName," +
                        " tbStudentBaseInfo.Sex," +
                        " tbStudentClass.ClassCode ClassCode," +
                        " tbStudentClass.ClassSN ClassSN " +
                        " FROM tbStudentClass LEFT JOIN tbStudentBaseInfo ON " +
                        " tbStudentClass.SRID = tbStudentBaseInfo.SRID " +
                        " LEFT JOIN tbStudentStatus ON " +
                        " tbStudentClass.SRID = tbStudentStatus.SRID AND " +
                        " tbStudentClass.AcademicYear = tbStudentStatus.AcademicYear " +
                        " WHERE tbStudentClass.AcademicYear =@Year" +
                        " AND tbStudentStatus.Status IN ('01','02','03') " +
                        " AND tbStudentBaseInfo.IsDelete = '0' ";
                    IList<Student> allStudents = bll.FillListByText<Student>(sql, new { Year = academicYear });
                    BuildClassStudent(gradeList, allStudents);
                }
                return gradeList;
            }
        }

        public static void BuildClassStudent(IList<GradeCode> gradeList, IList<Student> allStudents)
        {
            foreach (var grade in gradeList)
            {
                foreach (var gradeClass in grade.GradeClasses)
                {
                    var classStudents = from v in allStudents where v.ClassCode.Equals(gradeClass.ClassNo) select v;
                    foreach (var student in classStudents)
                    {
                        gradeClass.Students.Add(student);
                    }
                }
            }
        }

        /// <summary>
        /// 升留级
        /// </summary>
        /// <param name="academicyear"></param>
        /// <returns></returns>
        [WebMethod]
        public static int UpDown(int acadeMicYear, Student[] downStudents, GradeCode[] grades)
        {
            try
            {
                using (AppBLL bll = new AppBLL())
                {
                    var sql = "";
                    object inputParams = null;
                    DataTable table;
                    var nextAcademicYear = acadeMicYear + 1;
                    foreach (var student in downStudents)
                    {
                        //留级
                        sql = "Select * from tbStudentClass where Academicyear=@Academicyear and SRID = @SRID and isdelete<>'1'";
                        table = bll.FillDataTableByText(sql, new { Academicyear = acadeMicYear, SRID = student.StudentId });
                        if (table.Rows.Count == 0) continue;
                        var classCode = int.Parse(student.ClassCode);
                        if ((classCode > 3000 && classCode < 3400) || (classCode >= 2000 && classCode < 2400) || (classCode > 1000 && classCode < 1600))
                        {
                            var classSN = "00";
                            var sysID = UtilBLL.CreateSystemID("tbStudentClass");
                            var SRID = student.StudentId;
                            sql = " if not exists (select * from tbStudentClass where SRID=@SRID";
                            sql += " and Academicyear=@S_Academicyear) Begin";
                            sql += " Insert Into tbStudentClass(systemID,SRID,Academicyear,ClassCode,ClassSN) values(";
                            sql += " @s_SYSID,@SRID,@S_Academicyear,@s_ClassCode,@s_ClassSN) end";

                            inputParams = new
                            {
                                SRID = SRID,
                                s_SYSID = sysID,
                                S_Academicyear = nextAcademicYear,
                                s_ClassCode = classCode,
                                s_ClassSN = classSN
                            };
                            bll.ExecuteNonQueryByText(sql, inputParams);
                        }
                    }
                    //开留级学生班级
                    sql = "Select ClassCode from tbStudentClass where Academicyear=@S_Academicyear group by ClassCode";
                    table = bll.FillDataTableByText(sql, new { S_Academicyear = nextAcademicYear });
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        var sysID = UtilBLL.CreateSystemID("tbGradeClass");
                        var classCode = table.Rows[i][0].ToString();

                        sql = " if not exists (select * from tbGradeClass where Academicyear=@S_Academicyear and Classno=@ClassCode)";
                        sql += " Insert into tbGradeClass(systemid,Academicyear,Gradeno,classno,classType)";
                        sql += " values(@SYSID,@S_Academicyear,@subClassCode,@ClassCode,0)";
                        inputParams = new
                        {
                            SYSID = sysID,
                            S_Academicyear = nextAcademicYear,
                            subClassCode = classCode.Substring(0, 2),
                            ClassCode = classCode
                        };
                        bll.ExecuteNonQueryByText(sql, inputParams);
                    }
                    sql = "Select * from tbStudentClass where Academicyear=@academicyear";
                    table = bll.FillDataTableByText(sql, new { academicyear = acadeMicYear });
                    if (table.Rows.Count == 0) return 1;

                    sql = " Select SystemID, SRID, ClassSN, ClassCode, AcademicYear, IsDelete from tbStudentClass where Academicyear=@academicyear";
                    IList<StudentClass> clsStudents = bll.FillListByText<StudentClass>(sql, new { academicyear = acadeMicYear });
                    int length = clsStudents.Count();
                    for (int i = 0; i < length; i++)
                    {
                        var sysID = UtilBLL.CreateSystemID("tbStudentClass");
                        var studentClass = clsStudents[i];
                        string sRID = studentClass.SRID;
                        int downLength = downStudents.Count();
                        var isKeep = false;
                        for (int j = 0; j < downLength; j ++)
                        {
                             if (downStudents[j].StudentId.Equals(sRID))
                             { 
                                 isKeep = true;
                                 break;
                             } 
                        }
                        if (isKeep) continue;
                        sql = " if not exists (select * from tbStudentClass where SRID=@SRID and Academicyear=@S_Academicyear)"
                                    + " Insert Into tbStudentClass(systemID,SRID,Academicyear,ClassCode,ClassSN)"
                                    + " values(@SYSID, @SRID, @S_Academicyear, @s_ClassCode,@s_ClassSN)";

                        inputParams = new
                        {
                            SRID = sRID,
                            SYSID = sysID,
                            S_Academicyear = nextAcademicYear,
                            s_ClassCode = studentClass.ClassCode,
                            s_ClassSN = studentClass.ClassSN
                        };
                        bll.ExecuteNonQueryByText(sql, inputParams);
                    } 

                    //改变状态
                    sql = "Select * from tbstudentClass where Isdelete=0 and Academicyear=@CurrentYear";
                    table = bll.FillDataTableByText(sql, new { CurrentYear = acadeMicYear });
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        var sysID = UtilBLL.CreateSystemID("tbStudentStatus");
                        sql = "if not exists (select * from tbStudentStatus where Academicyear=@S_Academicyear and SRID=@SRID)";
                        sql += " Insert Into tbStudentStatus(SystemID,Academicyear,SRID,status)";
                        sql += " values(@s_SYSID,@S_Academicyear,@SRID,'01')";
                        inputParams = new
                        {
                            SRID = table.Rows[i]["SRID"].ToString(),
                            s_SYSID = sysID,
                            S_Academicyear = nextAcademicYear
                        };
                        bll.ExecuteNonQueryByText(sql, inputParams);
                    }

                    //开班级
                    sql = "Select * from tbstudentclass where Academicyear=@CurrentYear";
                    table = bll.FillDataTableByText(sql, new { CurrentYear = acadeMicYear });
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        var classCode = int.Parse(table.Rows[i]["ClassCode"].ToString());
                        var subClassCode = classCode.ToString().Substring(0, 2);
                        if ((classCode > 3000 && classCode < 3400) || (classCode >= 2000 && classCode < 2400) || (classCode > 1000 && classCode < 1600))
                        {
                            var sysID = UtilBLL.CreateSystemID("tbGradeClass");
                            sql = "if not exists (select * from tbGradeClass where Academicyear=@S_Academicyear and classno=@s_ClassCode)";
                            sql += " Insert into tbGradeClass(systemid,Academicyear,Gradeno,classno,classType)";
                            sql += " values(@s_SYSID,@S_Academicyear,@subClassCode,@s_ClassCode,'0')";

                            inputParams = new
                            {
                                s_ClassCode = classCode,
                                subClassCode = subClassCode,
                                s_SYSID = sysID,
                                S_Academicyear = nextAcademicYear
                            };
                            bll.ExecuteNonQueryByText(sql, inputParams);
                        }
                    }
                    sql = "Update tbSchoolBaseInfo Set AcademicYear=@S_Academicyear, Semester='1'";
                    inputParams = new
                            {
                                S_Academicyear = nextAcademicYear
                            };
                    bll.ExecuteNonQueryByText(sql, inputParams);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       
    }
}