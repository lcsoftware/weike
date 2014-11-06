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
            var gradeClasses = from v in ClassList where v.GradeNo == grade.GradeNo && v.ClassNo != null select v;
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
                    var systemIdBegin = UtilBLL.BuildSystemIdBegin();
                    var indexForStudentClass = UtilBLL.GetStartIndex("tbStudentClass");
                    var indexForGradeClass = UtilBLL.GetStartIndex("tbGradeClass");
                    var indexForStudentStatus = UtilBLL.GetStartIndex("tbStudentStatus");
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
                            var sysID = UtilBLL.CreateSystemID(systemIdBegin, indexForStudentClass++);
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
                        var sysID = UtilBLL.CreateSystemID(systemIdBegin, indexForGradeClass++);
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
                        var studentClass = clsStudents[i];
                        string sRID = studentClass.SRID;
                        int downLength = downStudents.Count();
                        var isKeep = false;
                        for (int j = 0; j < downLength; j++)
                        {
                            if (downStudents[j].StudentId.Equals(sRID))
                            {
                                isKeep = true;
                                break;
                            }
                        }
                        if (isKeep) continue;
                        var sysID = UtilBLL.CreateSystemID(systemIdBegin, indexForStudentClass++);
                        var classCode = int.Parse(studentClass.ClassCode) + 100;
                        if ((classCode > 3000 && classCode < 3400) || (classCode >= 2000 && classCode < 2400) || (classCode > 1000 && classCode < 1600))
                        {
                            sql = " if not exists (select * from tbStudentClass where SRID=@SRID and Academicyear=@S_Academicyear)"
                                        + " Insert Into tbStudentClass(systemID,SRID,Academicyear,ClassCode,ClassSN)"
                                        + " values(@SYSID, @SRID, @S_Academicyear, @s_ClassCode,@s_ClassSN)";

                            inputParams = new
                            {
                                SRID = sRID,
                                SYSID = sysID,
                                S_Academicyear = nextAcademicYear,
                                s_ClassCode = classCode,
                                s_ClassSN = studentClass.ClassSN
                            };
                            bll.ExecuteNonQueryByText(sql, inputParams);
                        }
                    }

                    //改变状态
                    sql = "Select * from tbstudentClass where Isdelete=0 and Academicyear=@CurrentYear";
                    table = bll.FillDataTableByText(sql, new { CurrentYear = acadeMicYear });
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        var sysID = UtilBLL.CreateSystemID(systemIdBegin, indexForStudentStatus++);
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
                        var classCode = int.Parse(table.Rows[i]["ClassCode"].ToString()) + 100;
                        var subClassCode = classCode.ToString().Substring(0, 2);
                        if ((classCode > 3000 && classCode < 3400) || (classCode >= 2000 && classCode < 2400) || (classCode > 1000 && classCode < 1600))
                        {
                            var sysID = UtilBLL.CreateSystemID(systemIdBegin, indexForGradeClass++);
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


        /**************************************************转换至学籍************************************/
        /// <summary>
        /// 试算
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static float TryCalculate(int micYear, string gradeNo, string courseCode, int testType)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sumRen = 0;
                var stdRen = 0;

                var sql = "select count(*) as SumRen from s_vw_ClassScoreNum"
                             + " where GradeNo=@gradeno"
                             + " and AcademicYear=@micYear"
                             + " and coursecode=@courseCode"
                             + " and testno=@testno"
                             + " and State is null";
                DataTable table = bll.FillDataTableByText(sql, new { gradeno = gradeNo, micYear = micYear, courseCode = courseCode, testno = testType });
                sumRen = int.Parse(table.Rows[0][0].ToString());
                if (sumRen == 0) return -1; //您选择的考试无人参加！
                //看标准分
                sql = "select count(*) as SumRen from s_vw_ClassScoreNum"
                             + " where GradeNo=@gradeno"
                             + " and AcademicYear=@micYear"
                             + " and normalscore is not null"
                             + " and coursecode=@CourseCode"
                             + " and testno=@testno"
                             + " and State is null";
                table = bll.FillDataTableByText(sql, new { gradeno = gradeNo, micYear = micYear, courseCode = courseCode, testno = testType });
                stdRen = int.Parse(table.Rows[0][0].ToString());
                if (stdRen == 0) return -2; //您对本次考试还未进行统计！

                if (sumRen - stdRen > 25) return -3; //在您统计本次考试后有可能有新成绩录入，请再次统计！

                return Math.Abs(sumRen - stdRen); //有标准分的人数与总人数不一致！相差 abs(sumRen - stdRen) 人，您继续吗?
            }
        }

        [WebMethod]
        public static float TryCalculateAgain(float c, float k, int micYear, string gradeNo, string courseCode, int testType)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "Select Max(normalscore) as MaxScore, min(normalscore) as MinScore from s_vw_ClassScoreNum"
                   + " where GradeNo=@gradeno"
                   + " and AcademicYear=@micYear"
                   + " and normalscore is not null"
                   + " and coursecode=@CourseCode"
                   + " and testno=@testno"
                   + " and State is null";
                DataTable table = bll.FillDataTableByText(sql, new { gradeno = gradeNo, micYear = micYear, courseCode = courseCode, testno = testType });
                var maxScore = float.Parse(table.Rows[0]["MaxScore"].ToString());
                var minScore = float.Parse(table.Rows[0]["MinScore"].ToString());

                float K1, K2;
                int K;
                if (maxScore > 0)
                    K1 = (100 - c) / maxScore;
                else
                    K1 = 9999;
                if (minScore < 0)
                    K2 = c / Math.Abs(minScore);
                else
                    K2 = 9999;

                return K1 > K2 ? (float)Math.Round(K2) : (float)Math.Round(K1);
            }
        }

        /// <summary>
        /// 执行试算
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static int TryOk(float c, float K, int micYear, string gradeNo, string courseCode, int testType)
        {
            int iK = (int)Math.Truncate(K);
            if (iK > K) return -1; //您设置K值不能大于试算的K值！
            using (AppBLL bll = new AppBLL())
            {
                var sql = "Update s_tb_Normalscore set StandardScore=b.NormalScore*(@K)+(@C)"
                    + " FROM s_vw_ClassScoreNum as a INNER JOIN s_tb_normalscore as b"
                    + " ON a.SRID = b.SRID"
                    + " and a.Academicyear=b.Academicyear"
                    + " and a.TestNo=b.testno"
                    + " and a.coursecode=b.coursecode"
                    + " where a.gradeno=@gradeNo"
                    + "` and b.Academicyear=@micYear"
                    + " and b.TestNo=@TestNo"
                    + " and b.CourseCode=@CourseCode"
                    + " and a.Normalscore is not Null"
                    + " and State is null";

                return bll.ExecuteNonQueryByText(sql, new { gradeno = gradeNo, micYear = micYear, courseCode = courseCode, testno = testType, C = c, K = iK });
            }
        }
        [WebMethod]
        public static IList<StudentScore> viewOriginData(int micYear, string semester, string gradeNo, string courseCode, int testType, int testNo)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "";
                if (testType == 0)
                {
                    sql = " Select  SRID, GradeName, ClassCode, ClassSN, StdName, Coursecode, CourseName, avg(NumScore) NumScore, avg(StandardScore) as StandardScore, MarkName"
                   + " from s_vw_ClassScoreNum"
                   + " where GradeNo=@GradeNo"
                   + " and Academicyear=@micYear"
                   + " and Semester=@Semester"
                   + " and CourseCode=@CourseCode"
                   + " and Testtype=@Testtype"
                   + " and STATE is NULL"
                   + " Group by SRID,GradeName,ClassCode,ClassSN,stdName,coursecode,CourseName,MarkName";
                    return bll.FillListByText<StudentScore>(sql, new { gradeno = gradeNo, Semester = semester, micYear = micYear, courseCode = courseCode, Testtype = testType });
                }
                else
                {
                    sql = " Select  SRID, GradeName, ClassCode, ClassSN, StdName, Coursecode, CourseName, NumScore, StandardScore, MarkName"
                   + " from s_vw_ClassScoreNum"
                   + " where GradeNo=@gradeno"
                   + " and AcademicYear=@micYear"
                   + " and coursecode=@courseCode"
                   + " and testno=@testno"
                   + " and STATE is Null";
                }
                return bll.FillListByText<StudentScore>(sql, new { gradeno = gradeNo, micYear = micYear, courseCode = courseCode, testno = testNo });
            }
        }

        [WebMethod]
        public static SumDecEntry SumDec(int micYear, string semester, string gradeNo, string courseCode, int testType, int testNo)
        {
            using (AppBLL bll = new AppBLL())
            {
                SumDecEntry entry = new SumDecEntry();
                var sql = "";
                var whereSql = "";
                var param = new object { };
                if (testType == 0)
                {
                    whereSql = " Where AcademicYear=@micYear and GradeNo=@gradeno and Testtype=@testtype and CourseCode=@courseCode and STATE is NULL";
                    param = new { gradeno = gradeNo, micYear = micYear, courseCode = courseCode, Testtype = testType };
                }
                else
                {
                    whereSql = " Where AcademicYear=@micYear and GradeNo=@gradeno and Testno=@testtype and CourseCode=@courseCode and STATE is NULL";
                    param = new { gradeno = gradeNo, micYear = micYear, courseCode = courseCode, testno = testNo };
                }
                sql = "Select Avg(NumScore) as Avg1,Avg(StandardScore) as Avg2 from s_vw_ClassScoreNum";
                DataTable table = bll.FillDataTableByText(sql + whereSql, param);
                if (table.Rows.Count > 0)
                {
                    entry.Avg1 = string.IsNullOrEmpty(table.Rows[0]["avg1"].ToString()) ? "无" : table.Rows[0]["avg1"].ToString();
                    entry.Avg2 = string.IsNullOrEmpty(table.Rows[0]["avg2"].ToString()) ? "无" : table.Rows[0]["avg2"].ToString();
                }
                sql = "Select Count(*) as Cunt1 from s_vw_ClassScoreNum";
                table = bll.FillDataTableByText(sql + whereSql + " and Numscore is Null", param);
                entry.Count1 = int.Parse(table.Rows[0]["cunt1"].ToString());

                sql = "Select Count(*) as Cunt1 from s_vw_ClassScoreNum";
                table = bll.FillDataTableByText(sql + whereSql + " and StandardScore is Null", param);
                entry.Count2 = int.Parse(table.Rows[0]["cunt1"].ToString());

                sql = "Select Count(*) as Cunt1 from s_vw_ClassScoreNum";
                table = bll.FillDataTableByText(sql + whereSql + " and NumScore < 60", param);
                entry.Count3 = int.Parse(table.Rows[0]["cunt1"].ToString());

                sql = "Select Count(*) as Cunt1 from s_vw_ClassScoreNum";
                table = bll.FillDataTableByText(sql + whereSql + " and Standardscore < 60", param);
                entry.Count4 = int.Parse(table.Rows[0]["cunt1"].ToString());

                sql = "Select Count(*) as Cunt1 from s_vw_ClassScoreNum";
                table = bll.FillDataTableByText(sql + whereSql + " and NumScore >= 200", param);
                entry.Count5 = int.Parse(table.Rows[0]["cunt1"].ToString());

                sql = "Select Count(*) as Cunt1 from s_vw_ClassScoreNum";
                table = bll.FillDataTableByText(sql + whereSql + " and Standardscore >= 200", param);
                entry.Count6 = int.Parse(table.Rows[0]["cunt1"].ToString());
                return entry;
            }
        }

        [WebMethod]
        public static int ConvertToXJ(int micYear, string semester, string gradeNo, string courseCode, int testType, int testNo, int scoreSort, bool ckTeacherOp)
        {
            try
            {
                using (AppBLL bll = new AppBLL())
                {
                    IList<XjEntry> xjEntries = null;
                    var sql = "Select Academicyear,SRID,CourseCode, CourseName,TeacherID,MarkCode";
                    if (testType == 1)
                    {
                        sql += scoreSort == 1 ? ",avg(Numscore) as Score,operator" : ",avg(standardscore) as Score,operator";
                        sql += " from s_vw_ClassScoreNum "
                               + " where Gradeno=@gradeNo"
                               + " and Academicyear=@micYear"
                               + " and semester=@semester"
                               + " and CourseCode=@courseCode"
                               + " and TestType=@testType"
                               + " and STATE is NULL"
                               + " group by Academicyear,SRID,CourseCode,Teacherid,MarkCode,operator";
                        xjEntries = bll.FillListByText<XjEntry>(sql, new { gradeno = gradeNo, micYear = micYear, semester = semester, courseCode = courseCode, testType = testType });
                    }
                    else
                    {
                        sql += scoreSort == 1 ? ",Numscore as Score,operator" : ",standardscore as Score,operator";
                        sql += " from s_vw_ClassScoreNum"
                               + " Where GradeNo=@gradeNo"
                               + " and Academicyear=@micYear"
                               + " and CourseCode=@courseCode"
                               + " and TestNo=@testNo"
                               + " and STATE is NULL";
                        xjEntries = bll.FillListByText<XjEntry>(sql, new { gradeno = gradeNo, micYear = micYear, courseCode = courseCode, testno = testNo });
                    }
                    var systemIdBegin = UtilBLL.BuildSystemIdBegin();
                    var indexForScore = UtilBLL.GetStartIndex("tbScore");
                    var MinSysID = UtilBLL.CreateSystemID(systemIdBegin, indexForScore);
                    foreach (var xjEntry in xjEntries)
                    {
                        var sysID = UtilBLL.CreateSystemID(systemIdBegin, indexForScore++);
                        var tempTeacher = xjEntry.TeacherID;
                        var tempScore = xjEntry.Score;
                        var strOperator = xjEntry.Operator;

                        sql = "if exists(select * from tbScore where Academicyear=@micYear and CourseCode=@courseCode and SRID=@srid)"
                                + " Update tbScore set ";
                        switch (testType)
                        {
                            case 0:
                                sql += semester.Equals("1") ? "FstAverageScore=" : " SndAverageScore=";
                                break;
                            case 1:
                                sql += semester.Equals("1") ? "FstMidScore=" : " SndMidScore=";
                                break;
                            default:
                                sql += semester.Equals("1") ? "FstEndScore=" : " SndEndScore=";
                                break;
                        }
                        sql += tempScore;
                        sql += " where Academicyear=@micYear"
                               + " and SRID=@srid"
                               + " and CourseCode=@courseCode"
                               + " else"
                               + " Insert Into tbscore(SystemID,AcademicYear,srid,ScoreTypeCode, Coursecode,CourseName,TeacherID,";
                        switch (testType)
                        {
                            case 0:
                                sql += semester.Equals("1") ? "FstAverageScore, operator)" : " SndAverageScore, operator)";
                                break;
                            case 1:
                                sql += semester.Equals("1") ? "FstMidScore, operator)" : " SndMidScore, operator)";
                                break;
                            default:
                                sql += semester.Equals("1") ? "FstEndScore, operator)" : " SndEndScore, operator)";
                                break;
                        }
                        sql += " values(@sysID,@micYear,@srid,'1100',@courseCode,@courseName,@teacher,@score, @Operator)";
                        bll.ExecuteNonQueryByText(sql, new
                        {
                            sysID = @sysID,
                            srid = xjEntry.SRID,
                            micYear = micYear,
                            courseCode = courseCode,
                            courseName = xjEntry.CourseName,
                            teacher = xjEntry.TeacherID,
                            score = xjEntry.Score,
                            Operator = string.IsNullOrEmpty(xjEntry.Operator) ? "" : xjEntry.Operator
                        });
                    }
                    var indexForMoraCol = UtilBLL.GetStartIndex("tbMoralCol");
                    var minSysId1 = UtilBLL.CreateSystemID(systemIdBegin, indexForMoraCol);
                    if (ckTeacherOp)
                    {
                        sql = " SELECT b.SRID, b.AcademicYear, a.TeacherOP FROM s_tb_TeacherOption a INNER JOIN"
                             + " tbStudentClass b ON a.srid = b.SRID AND "
                             + " a.Academicyear = b.AcademicYear"
                             + " WHERE b.AcademicYear = @micYear"
                             + " AND LEFT(b.ClassCode, 2) = @gradeNo"
                             + " and a.semester=@semester";
                        IList<TeacherOption> teacherOptions = bll.FillListByText<TeacherOption>(sql, new { micYear = micYear, gradeNo = gradeNo, semester = semester });
                        foreach (var teacherOption in teacherOptions)
                        {
                            sql = "select count(*) as cnt from tbMoralCol where academicyear =@micYear and semester=@semester and srid =@srid";
                            DataTable table = bll.FillDataTableByText(sql, new { micYear = micYear, semester = semester, srid = teacherOption.SRID });
                            if (table.Rows.Count > 0)
                            {
                                sql = " UPDATE tbMoralCol set Remark =@remark where Academicyear=@micYear, srid=@srid and semester=@semester";
                                bll.ExecuteNonQueryByText(sql, new { remark = teacherOption.TeacherOP, micYear = micYear, semester = semester });
                            }
                            else
                            {
                                var sysID = UtilBLL.CreateSystemID(systemIdBegin, indexForMoraCol++);
                                sql = " insert into tbMoralCol(SystemID,SRID,AcademicYear,Semester,MoralRank,Remark)"
                                   + " values(@sysID, @srid, @micYear, @semester,'良好', @remark)";
                                bll.ExecuteNonQueryByText(sql, new { sysID = sysID, srid = teacherOption.SRID, remark = teacherOption.TeacherOP, micYear = micYear, semester = semester });
                            }
                        }
                    }

                    sql = " Insert into tbSchoolOutBox(TableCode,SystemID,RequestDatetime,ThresholdDatetime)"
                            + " Select 'tbscore',SystemID, GETDATE(), DATEADD(day, 1, GETDATE()) from tbscore "
                            + " where systemid>=@minSysID order by SystemID ";
                    bll.ExecuteNonQueryByText(sql, new { minSysID = MinSysID });
                    if (ckTeacherOp)
                    {
                        sql = " Insert into tbSchoolOutBox(TableCode,SystemID,RequestDatetime,ThresholdDatetime)"
                               + " Select 'tbMoralCol',SystemID, GETDATE(), DATEADD(day, 1, GETDATE()) from tbMoralCol"
                               + " where systemid>=@MinSysID order by SystemID";
                        bll.ExecuteNonQueryByText(sql, new { minSysID = minSysId1 });
                    }
                }//using 
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public static int Export(int micYear, string semester, string gradeNo, string courseCode, int testType, int testNo, int scoreSort)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "";
                DataTable table = null;
                if (testType == 1)
                {
                    sql += scoreSort == 1 ? ",avg(Numscore) as Score,operator" : ",avg(standardscore) as Score,operator";
                    sql += " from s_vw_ClassScoreNum "
                           + " where Gradeno=@gradeNo"
                           + " and Academicyear=@micYear"
                           + " and semester=@semester"
                           + " and CourseCode=@courseCode"
                           + " and TestType=@testType"
                           + " and STATE is NULL"
                           + " group by Academicyear,SRID,CourseCode,Teacherid,MarkCode,operator";
                    table = bll.FillDataTableByText(sql, new { gradeno = gradeNo, micYear = micYear, semester = semester, courseCode = courseCode, testType = testType });
                }
                else
                {
                    sql += scoreSort == 1 ? ",Numscore as Score,operator" : ",standardscore as Score,operator";
                    sql += " from s_vw_ClassScoreNum"
                           + " Where GradeNo=@gradeNo"
                           + " and Academicyear=@micYear"
                           + " and CourseCode=@courseCode"
                           + " and TestNo=@testNo"
                           + " and STATE is NULL";
                    table = bll.FillDataTableByText(sql, new { gradeno = gradeNo, micYear = micYear, courseCode = courseCode, testno = testNo });
                }
                return 1;
            }
        }

        /****************************************从学籍转换过来*****************************/
        [WebMethod]
        public static DataTable ViewData(int micYear, int chkAll, int cbTestType)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "";
                if (chkAll == 1)
                {
                    sql = "SELECT a.Academicyear, b.StdName, c.BriefName, d.Name,"
                           + " a.FstMidScore, a.FstEndScore, a.SndMidScore, a.SndEndScore"
                           + " FROM  TbScore a INNER JOIN"
                           + " tdCourseCode c ON a.Coursecode = c.CourseCode INNER JOIN"
                           + " tbStudentBaseInfo b ON a.SRID = b.SRID LEFT OUTER JOIN"
                           + " tbUserGroupInfo d ON a.TeacherID = d.TeacherID"
                           + " WHERE SUBSTRING(a.Coursecode, 2, 1) = '1'"
                           + " AND (a.Academicyear =@micYear";
                }
                else
                {
                    var ScoreField = "";
                    switch (cbTestType)
                    {
                        case 0:
                            ScoreField = "FstMidScore as Score";
                            break;
                        case 1:
                            ScoreField = "FstEndScore as Score";
                            break;
                        case 2:
                            ScoreField = "SndMidScore as Score";
                            break;
                        default:
                            ScoreField = "SndEndScore as Score";
                            break;
                    }

                    sql = "SELECT a.Academicyear, b.StdName, c.BriefName," + ScoreField + ",d.Name"
                           + " FROM  TbScore a INNER JOIN"
                           + " tdCourseCode c ON a.Coursecode = c.CourseCode INNER JOIN"
                           + " tbStudentBaseInfo b ON a.SRID = b.SRID LEFT OUTER JOIN"
                           + " tbUserGroupInfo d ON a.TeacherID = d.TeacherID"
                           + " WHERE SUBSTRING(a.Coursecode, 2, 1) = '1'"
                           + " AND (a.Academicyear =@micYear)";
                }
                return bll.FillDataTableByText(sql, new { micYear = micYear });
            }
        }

        private static int ConvertAll(int micYear, bool canContinue)
        {
            var teacherId = "99999999990888";
            int testLoginNo, micYear1, testNo1, testNo2, testNo3, testNo4, testNo5;
            using (AppBLL bll = new AppBLL())
            {
                var sql = "";
                DataTable table = null;
                sql = "Select Count(*) as Pcount from s_tb_testlogin Where AcademicYear=@micYear";
                table = bll.FillDataTableByText(sql, new { micYear = micYear });
                if (int.Parse(table.Rows[0][0].ToString()) == 0)
                {
                    testNo1 = 1;
                }
                else
                {
                    if (!canContinue) return -1;
                    sql = "Select Max(convert(integer,testNo)) as testnum from  s_tb_Testlogin where AcademicYear=@micYear";
                    table = bll.FillDataTableByText(sql, new { micYear = micYear });
                    testNo1 = table.Rows.Count == 0 ? 1 : int.Parse(table.Rows[0][0].ToString()) + 1;
                }
                testNo2 = testNo1 + 1;
                testNo3 = testNo1 + 1;
                testNo4 = testNo1 + 1;
                testNo5 = testNo1;

                sql = "select Max(convert(integer,testloginNo)) as testnum from  s_tb_Testlogin";
                table = bll.FillDataTableByText(sql, null);
                testLoginNo = table.Rows.Count == 0 ? 1 : int.Parse(table.Rows[0][0].ToString());
                //判断是否有上学期的期中考试
                sql = " Insert Into s_tb_Testlogin(TestloginNo,TestNo,AcademicYear,Semester,TestType,Gradeno,CourseCode,TestTime,MarkTypeCode)"
                        + " Values(@testloginNo,@testNo1,@micYear,'1','1','00','00000',@testTime,'1100')";
                var testTime = new DateTime(micYear, 1, 15);
                bll.ExecuteNonQueryByText(sql, new { testloginNo = testLoginNo, testNo1 = testNo1, @micYear = micYear, testTime = testTime });

                sql = "insert into s_tb_TestloginUser(TestloginNo,Teacherid) values(@testloginNo,@teacherid)";
                bll.ExecuteNonQueryByText(sql, new { testloginNo = testLoginNo, teacherid = teacherId });
                //第一学期期末考试
                testLoginNo++;
                testNo1++;
                micYear1 = micYear + 1;
                testTime = new DateTime(micYear1, 4, 15);
                sql = "Insert Into s_tb_Testlogin(TestloginNo,TestNo,AcademicYear,Semester,TestType,Gradeno,CourseCode,TestTime,marktypecode)"
                        + " Values(@testloginNo,@testNo1,@micYear,'1','2','00','00000',@testTime,'1100')";
                bll.ExecuteNonQueryByText(sql, new { testloginNo = testLoginNo, testNo1 = testNo1, @micYear = micYear, testTime = testTime });

                sql = "insert into s_tb_TestloginUser(TestloginNo,Teacherid) values(@testloginNo,@teacherid)";
                bll.ExecuteNonQueryByText(sql, new { testloginNo = testLoginNo, teacherid = teacherId });

                testLoginNo++;
                testNo1++;
                testTime = new DateTime(micYear1, 6, 15);

                sql = "Insert Into s_tb_Testlogin(TestloginNo,TestNo,AcademicYear,Semester,TestType,Gradeno,CourseCode,TestTime,MarkTypeCode)"
                        + " Values(@testloginNo,@testNo1,@micYear,'2','2','00','00000',@testTime,'1100')";
                bll.ExecuteNonQueryByText(sql, new { testloginNo = testLoginNo, testNo1 = testNo1, @micYear = micYear, testTime = testTime });

                sql = "insert into s_tb_TestloginUser(TestloginNo,Teacherid) values(@testloginNo,@teacherid)";
                bll.ExecuteNonQueryByText(sql, new { testloginNo = testLoginNo, teacherid = teacherId });

                sql = "Select AcademicYear,srid,Coursecode,Teacherid,FstMidScore,FstEndScore,SndMidScore,SndEndScore,Operator"
                        + " from tbScore"
                        + " where AcademicYear=@micYear and substring(CourseCode,2,1)='1'";
                table = bll.FillDataTableByText(sql, new { micYear = micYear });
                //以下这段在delphi程序中未看懂，猜测应为如下实现方式
                sql = "Insert Into s_tb_normalscore(AcademicYear,Semester,srid,CourseCode,TeacherID,MarkCode,TestType,TestNo,NumScore,Operator)"
                             + " Values(@YEAR,@SEMESTER,@srid,@COURSECODE,@TEACHERID,@MARKTYPECODE,@TESTTYPE,@TESTNO,@NUMSCORE,@OPERATOR)";
                var length = table.Rows.Count;
                for (int i = 0; i < length; i++)
                {
                    var YEAR = table.Rows[i]["AcademicYear"].ToString();
                    var srid = table.Rows[i]["srid"].ToString();
                    var COURSECODE = table.Rows[i]["CourseCode"].ToString();
                    var TEACHERID = string.IsNullOrEmpty(table.Rows[i]["Teacherid"].ToString()) ? teacherId : table.Rows[i]["Teacherid"].ToString();
                    var OPERATOR = string.IsNullOrEmpty(table.Rows[i]["Operator"].ToString()) ? teacherId : table.Rows[i]["Operator"].ToString();
                    var TESTTYPE = "1";
                    var TESTNO = testNo5;
                    var MARKTYPECODE = "1100";
                    var NUMSCORE = 1.0f;
                    var SEMESTER = "1";

                    string score = table.Rows[i]["FstMidScore"].ToString();
                    if (!string.IsNullOrEmpty(score))
                    {
                        NUMSCORE = float.Parse(table.Rows[i]["FstMidScore"].ToString());
                        bll.ExecuteNonQueryByText(sql, new
                        {
                            YEAR = YEAR,
                            SEMESTER = SEMESTER,
                            srid = srid,
                            COURSECODE = COURSECODE,
                            TEACHERID = TEACHERID,
                            MARKTYPECODE = MARKTYPECODE,
                            TESTTYPE = TESTTYPE,
                            TESTNO = TESTNO,
                            NUMSCORE = NUMSCORE,
                            OPERATOR = OPERATOR
                        });
                    }

                    score = table.Rows[i]["FstEndScore"].ToString();
                    if (!string.IsNullOrEmpty(score))
                    {
                        TESTTYPE = "2";
                        TESTNO = testNo2;
                        NUMSCORE = float.Parse(table.Rows[i]["FstEndScore"].ToString());
                        bll.ExecuteNonQueryByText(sql, new
                        {
                            YEAR = YEAR,
                            SEMESTER = SEMESTER,
                            srid = srid,
                            COURSECODE = COURSECODE,
                            TEACHERID = TEACHERID,
                            MARKTYPECODE = MARKTYPECODE,
                            TESTTYPE = TESTTYPE,
                            TESTNO = TESTNO,
                            NUMSCORE = NUMSCORE,
                            OPERATOR = OPERATOR
                        });
                    }
                    score = table.Rows[i]["SndMidScore"].ToString();
                    if (!string.IsNullOrEmpty(score))
                    {
                        TESTTYPE = "1";
                        TESTNO = testNo3;
                        SEMESTER = "2";
                        NUMSCORE = float.Parse(table.Rows[i]["SndMidScore"].ToString());
                        bll.ExecuteNonQueryByText(sql, new
                        {
                            YEAR = YEAR,
                            SEMESTER = SEMESTER,
                            srid = srid,
                            COURSECODE = COURSECODE,
                            TEACHERID = TEACHERID,
                            MARKTYPECODE = MARKTYPECODE,
                            TESTTYPE = TESTTYPE,
                            TESTNO = TESTNO,
                            NUMSCORE = NUMSCORE,
                            OPERATOR = OPERATOR
                        });
                    }

                    score = table.Rows[i]["SndEndScore"].ToString();
                    if (!string.IsNullOrEmpty(score))
                    {
                        TESTTYPE = "2";
                        TESTNO = testNo4;
                        SEMESTER = "2";
                        NUMSCORE = float.Parse(table.Rows[i]["SndEndScore"].ToString());
                        bll.ExecuteNonQueryByText(sql, new
                        {
                            YEAR = YEAR,
                            SEMESTER = SEMESTER,
                            srid = srid,
                            COURSECODE = COURSECODE,
                            TEACHERID = TEACHERID,
                            MARKTYPECODE = MARKTYPECODE,
                            TESTTYPE = TESTTYPE,
                            TESTNO = TESTNO,
                            NUMSCORE = NUMSCORE,
                            OPERATOR = OPERATOR
                        });
                    }
                }
                return 1;
            }
        }

        private static int Convert(int micYear, int cbTestType, bool canContinue)
        {
            var teacherId = "99999999990888";
            int testLoginNo, testNo1;
            using (AppBLL bll = new AppBLL())
            {
                var changeScore = "";
                var Semester = 1;
                var testType = 1;
                var partSql = "";
                switch (cbTestType)
                {
                    case 0:
                        Semester = 1;
                        testType = 1;
                        changeScore = " FstMidScore as scorexj ";
                        partSql = " and FstMidScore is not null and FstMidScore<>''''";
                        break;
                    case 1:
                        Semester = 1;
                        testType = 1;
                        changeScore = " FstEndScore as scorexj ";
                        partSql = " and FstEndScore is not null and FstMidScore<>''''";
                        break;
                    case 2:
                        Semester = 1;
                        testType = 2;
                        changeScore = " SndMidScore as scorexj ";
                        partSql = " and SndMidScore is not null and FstMidScore<>''''";
                        break;
                    default:
                        Semester = 1;
                        testType = 2;
                        changeScore = " SndEndScore as scorexj ";
                        partSql = " and SndEndScore is not null and FstMidScore<>''''";
                        break;
                }

                var sql = "Select Max(cast(testNo as int)) as testnum from  s_tb_TestLogin where AcademicYear=@micYear";
                DataTable table = bll.FillDataTableByText(sql, new { micYear = micYear });
                testNo1 = table.Rows.Count == 0 || string.IsNullOrEmpty(table.Rows[0][0].ToString()) ? 1 : int.Parse(table.Rows[0][0].ToString());
                sql = "select Max(convert(integer,testloginNo)) as testnum from  s_tb_Testlogin";
                table = bll.FillDataTableByText(sql);
                testLoginNo = table.Rows.Count == 0 || string.IsNullOrEmpty(table.Rows[0][0].ToString()) ? 1 : int.Parse(table.Rows[0][0].ToString());

                sql = " Insert Into s_tb_Testlogin(TestloginNo,TestNo,AcademicYear,Semester,"
                       + " TestType,Gradeno,CourseCode,testtime,MarkTypeCode,startdate,enddate)"
                       + " Values(@testLoginNo,@testNo,@micYear,@semester,@testType,@gradeNo,@courseCode,@testTime,'1100',@startDate,@endDate)";

                bll.ExecuteNonQueryByText(sql, new
                {
                    testLoginNo = testLoginNo,
                    testNo = testNo1,
                    micYear = micYear,
                    semester = Semester,
                    testType = testType,
                    gradeNo = "00",
                    courseCode = "00000",
                    testTime = new DateTime(micYear, 12, 15),
                    startDate = new DateTime(micYear, 12, 16),
                    endDate = new DateTime(micYear, 12, 25)
                });

                sql = "insert into s_tb_testloginUser(TestloginNo,Teacherid) values(:TestloginNo,:Teacherid)";
                bll.ExecuteNonQueryByText(sql, new { TestloginNo = testLoginNo, Teacherid = teacherId });

                sql = "Select AcademicYEAR,srid,COURSECODE,TEACHERID,Operator," + changeScore + " from tbScore where AcademicYear=@micYear and substring(coursecode,2,1)='1'";
                sql += partSql;
                table = bll.FillDataTableByText(sql, new { micYear = micYear });
                sql = "Insert Into s_tb_normalscore(AcademicYear,Semester,srid,CourseCode,TeacherID,MarkCode,TestType,TestNo,NumScore,Operator)"
                             + " Values(@YEAR,@SEMESTER,@srid,@COURSECODE,@TEACHERID,@MARKTYPECODE,@TESTTYPE,@TESTNO,@NUMSCORE,@OPERATOR)";
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var YEAR = table.Rows[i]["AcademicYear"].ToString();
                    var srid = table.Rows[i]["srid"].ToString();
                    var COURSECODE = table.Rows[i]["CourseCode"].ToString();
                    var TEACHERID = string.IsNullOrEmpty(table.Rows[i]["Teacherid"].ToString()) ? teacherId : table.Rows[i]["Teacherid"].ToString();
                    var OPERATOR = string.IsNullOrEmpty(table.Rows[i]["Operator"].ToString()) ? teacherId : table.Rows[i]["Operator"].ToString();
                    var TESTTYPE = testType;
                    var TESTNO = testNo1;
                    var MARKTYPECODE = "1100";
                    var SEMESTER = Semester;
                    var NUMSCORE = string.IsNullOrEmpty(table.Rows[i]["scorexj"].ToString()) ? 0 : float.Parse(table.Rows[i]["scorexj"].ToString());

                    bll.ExecuteNonQueryByText(sql, new
                    {
                        YEAR = YEAR,
                        SEMESTER = SEMESTER,
                        srid = srid,
                        COURSECODE = COURSECODE,
                        TEACHERID = TEACHERID,
                        MARKTYPECODE = MARKTYPECODE,
                        TESTTYPE = TESTTYPE,
                        TESTNO = TESTNO,
                        NUMSCORE = NUMSCORE,
                        OPERATOR = OPERATOR
                    });
                }
                return 1;
            }
        }
        /// <summary>
        /// 转入本系统
        /// </summary>
        /// <param name="micYear"></param>
        /// <param name="chkAll"></param>
        /// <param name="testType"></param>
        /// <param name="canContinue">默认false</param>
        /// <returns></returns>
        [WebMethod]
        public static int ConvertToCJ(int micYear, int chkAll, int cbTestType, bool canContinue)
        {
            using (AppBLL bll = new AppBLL())
            {
                if (chkAll == 1)
                {
                    return ConvertAll(micYear, canContinue);
                }
                else
                {
                    return Convert(micYear, cbTestType, canContinue);
                }
            }
        }
        /****************************************end 从学籍转换过来*****************************/
    }
}