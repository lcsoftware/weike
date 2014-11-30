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

namespace App.Web.Score.DataProvider
{
    public partial class Analyze : System.Web.UI.Page
    {
        [WebMethod]
        public static IList<GradeCourse> GetCourses(int micYear, GradeCode gradeCode, TestLogin testLogin)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "Select testno,coursecode from s_tb_testlogin"
                       + " where academicyear=@micYear"
                       + " and testno=@testNo";
                DataTable table = bll.FillDataTableByText(sql, new { micYear = micYear, testNo = testLogin.TestLoginNo });
                if (string.IsNullOrEmpty(table.Rows[0]["coursecode"].ToString()) || table.Rows[0]["coursecode"].ToString() == "00000")
                {
                    sql = "select a.Academicyear, a.GradeNo, a.CourseCode, b.FullName from tbcourseuse a,tdcoursecode b"
                               + " where academicyear=@micYear"
                               + " and gradeno=@gradeNo"
                               + " and a.coursecode=b.coursecode";
                    return bll.FillListByText<GradeCourse>(sql, new { micYear = micYear, gradeNo = gradeCode.GradeNo });
                }
                else
                {
                    sql = "select * from tdcourseCode where coursecode=@courseCode";
                    return bll.FillListByText<GradeCourse>(sql, new { courseCode = table.Rows[0]["coursecode"].ToString() });
                }
            }
        }

        [WebMethod]
        public static IList<GradeCourse> GetCoursesByTestLogin(int micYear, TestLogin testLogin)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "Select * from s_tb_testlogin"
                       + " where academicyear=@micYear"
                       + " and testno=@testNo";
                DataTable table = bll.FillDataTableByText(sql, new { micYear = micYear, testNo = testLogin.TestLoginNo });

                var mTestType = string.IsNullOrEmpty(table.Rows[0]["testtype"].ToString()) ? "0" : table.Rows[0]["testtype"].ToString();
                var mGradeNo1 = string.IsNullOrEmpty(table.Rows[0]["GradeNo"].ToString()) ? "" : table.Rows[0]["GradeNo"].ToString();
                var testMark = string.IsNullOrEmpty(table.Rows[0]["Marktypecode"].ToString()) ? "1100" : table.Rows[0]["Marktypecode"].ToString();
                var testCourse = table.Rows[0]["CourseCode"].ToString();
                if (testCourse == "00000")
                {
                    sql = " SELECT {0} as Academicyear, b.GradeNo, b.CourseCode, a.FullName "
                          + " FROM  tdCourseCode a INNER JOIN "
                          + "  tbCourseUse b ON a.CourseCode = b.coursecode"
                          + " where b.AcademicYear=@micYear"
                          + " and gradeno=@gradeNo"
                          + " group by a.BriefName, b.coursecode, b.GradeNo, a.FullName ";
                    sql = string.Format(sql, micYear);
                    return bll.FillListByText<GradeCourse>(sql, new { micYear = micYear, gradeno = mGradeNo1 });
                }
                else
                {
                    sql = "select {0} as Academicyear, FullName, CourseCode  from tdcourseCode where coursecode=@courseCode";
                    return bll.FillListByText<GradeCourse>(sql, new { courseCode = table.Rows[0]["coursecode"].ToString() });
                } 
            }
        }
        private static void gf_ScoreOrderA(string OrderSQL, string courseCode, string writeTableName, string fieldName, int writeBack)
        {
            using (AppBLL bll = new AppBLL())
            {
                var tempTableName = App.Score.Db.UtilBLL.mf_getTable();
                try
                {
                    var sql = "create table {0}(academicYear char(4),Semester char(2),TestType char(1),TestNo char(5),CourseCode char(5),srid char(19),Score numeric(5,1),OrderNO integer)";
                    sql = string.Format(sql, tempTableName);
                    bll.ExecuteNonQueryByText(sql);

                    sql = "insert into {0}(academicYear,Semester,TestType,TestNo,CourseCode,srid,Score,OrderNO) {1}";
                    sql = string.Format(sql, tempTableName, OrderSQL);
                    bll.ExecuteNonQueryByText(sql);

                    //开始排名
                    sql = string.Format("select row_number() over(order by score desc) as num, score from (select distinct score from {0}) t", tempTableName);
                    DataTable table = bll.FillDataTableByText(sql);
                    var length = table.Rows.Count;
                    var orderNo = 1;
                    for (int i = 0; i < length; i++)
                    {
                        sql = string.Format("update {0} set OrderNO={1} where score=@score", tempTableName, orderNo++);
                        var score = float.Parse(table.Rows[i]["score"].ToString());
                        bll.ExecuteNonQueryByText(sql, new { score = score });
                    }
                    if (writeBack == 9)
                    {
                        //写回原表
                        sql = "UPDATE {0}"
                               + " SET {1} = a.OrderNo"
                               + " FROM {2} as a INNER JOIN {0} as b "
                               + " ON a.SRID = b.SRID "
                               + " and a.Academicyear= b.Academicyear"
                               + " and a.TestNo=b.testno";
                        sql = string.Format(sql, writeTableName, fieldName, tempTableName);
                        bll.ExecuteNonQueryByText(sql);
                        //写成绩表
                        sql = " UPDATE s_tb_normalscore"
                                    + " SET GradeOrder = a.OrderNo "
                                    + " FROM {0} as a INNER JOIN s_tb_normalscore as b"
                                    + " ON a.SRID = b.SRID"
                                    + " and a.Academicyear= b.Academicyear"
                                    + " and a.TestNo=b.testno"
                                    + " and b.coursecode =@courseCode";
                        sql = string.Format(sql, tempTableName);
                        bll.ExecuteNonQueryByText(sql, new { courseCode = courseCode });
                    }
                    else if (writeBack == 0)
                    {
                        sql = " UPDATE {0}"
                                   + " SET {1} = a.OrderNo "
                                   + " FROM {2} as a INNER JOIN {0} as b "
                                   + " ON a.SRID = b.SRID"
                                   + " and a.Academicyear= b.Academicyear"
                                   + " and a.TestNo=b.testno";
                        sql = string.Format(sql, writeTableName, fieldName, tempTableName);
                        bll.ExecuteNonQueryByText(sql);
                    }
                }
                finally
                {
                    //删除临时表
                    var sql = "if exists(select * from sysobjects where name = '{0}' and xtype='U') drop table {0}";
                    sql = string.Format(sql, tempTableName);
                    bll.ExecuteNonQueryByText(sql);
                }
            }
        }

        private static int gf_GetStdScoreB(int micYear, string testNo, string courseCode, string classCode)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "select avg(Numscore) as AvgScore,stdevp(numscore) as stdScore"
                          + " from s_vw_ClassScoreNum"
                          + " where Academicyear=@micYear"
                          + " and TestNo=@testNo"
                          + " and CourseCode=@courseCode"
                          + " and ClassCode in ({0})"
                          + " and Numscore<200"
                          + " and Numscore is not Null";
                sql = string.Format(sql, classCode);
                DataTable table = bll.FillDataTableByText(sql, new { micYear = micYear, testNo = testNo, courseCode = courseCode });
                var avgScore = float.Parse(table.Rows[0]["AvgScore"].ToString());
                var s = float.Parse(table.Rows[0]["stdScore"].ToString());
                //计算标准分
                if (s == 0)
                {
                    //方差为零，标准分为零
                    sql = " Update s_tb_Normalscore Set NormalScore = 0"
                                + " FROM s_vw_ClassScoreNum as a INNER JOIN s_tb_normalscore as b"
                                + " ON a.SRID = b.SRID"
                                + " and a.Academicyear= b.Academicyear"
                                + " and a.TestNo=b.testno"
                                + " and a.coursecode=b.coursecode"
                                + " where a.ClassCode in (@classCode)"
                                + " and b.Academicyear=@micYear"
                                + " and b.TestNo=@testNo"
                                + " and b.CourseCode=@courseCode"
                                + " and b.Numscore is not Null";
                    bll.ExecuteNonQueryByText(sql, new { micYear = micYear, testNo = testNo, courseCode = courseCode, classCode = classCode });
                }
                else
                {
                    sql = " update s_tb_normalscore "
                               + " set NormalScore=(b.NumScore-@avgScore)/(@Sscore)"
                               + " FROM s_vw_ClassScoreNum as a INNER JOIN s_tb_normalscore as b"
                               + " ON a.SRID = b.SRID"
                               + " and a.Academicyear= b.Academicyear"
                               + " and a.TestNo=b.testno"
                               + " and a.coursecode=b.coursecode"
                               + " where a.Academicyear=@micYear"
                               + " and a.TestNo=@testNo"
                               + " and a.ClassCode in (@classCode)"
                               + " and a.CourseCode=@courseCode"
                               + " and b.Numscore<200 "
                               + " and b.Numscore is not Null ";
                    bll.ExecuteNonQueryByText(sql, new
                    {
                        micYear = micYear,
                        testNo = testNo,
                        courseCode = courseCode,
                        classCode = classCode,
                        avgScore = avgScore,
                        Sscore = s
                    });
                }
                return 0;
            }
        }

        private static void gp_GetTScoreB(int micYear, string testNo, string courseCode, string classCode)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = " select max(normalscore) maxBZScore,min(normalscore) minBZScore "
                       + " from s_vw_ClassScoreNum "
                       + " where Academicyear=@micYear"
                       + " and Testno=@testNo"
                       + " and classcode in ({0})"
                       + " and CourseCode=@courseCode";
                sql = string.Format(sql, classCode);

                DataTable table = bll.FillDataTableByText(sql, new { micYear = micYear, testNo = testNo, courseCode = courseCode });
                if (table.Rows.Count == 0) return;
                var XT = float.Parse(table.Rows[0]["maxBZScore"].ToString());
                var YT = float.Parse(table.Rows[0]["minBZScore"].ToString());
                if (XT == 0 || YT == 0) return;
                int k = (int)Math.Floor(25 / XT);
                if (k > Math.Abs(Math.Floor(75 / YT))) k = (int)Math.Abs(Math.Floor(75 / YT));

                sql = " UPDATE b"
                        + " SET b.standardScore = (75+b.Normalscore*({0}))"
                        + " FROM s_vw_ClassScoreNum as a INNER JOIN s_tb_normalscore as b"
                        + " ON a.SRID = b.SRID"
                        + " and a.Academicyear= b.Academicyear"
                        + " and a.TestNo=b.testno"
                        + " and a.coursecode=b.coursecode"
                        + " where a.Academicyear=@micYear"
                        + " and a.Testno=@testNo"
                        + " and a.classcode in ({1})"
                        + " and a.CourseCode=@courseCode"; ;
                sql = string.Format(sql, k, classCode);
                bll.ExecuteNonQueryByText(sql, new { micYear = micYear, testNo = testNo, courseCode = courseCode });
            }
        }


        [WebMethod]
        public static IList<ResultEntry> AnalyzeSuper(
            int micYear,
            GradeCode gradeCode,
            IList<GradeClass> gradeClasses,
            IList<GradeCourse> gradeCourses,
            TestType testType,
            TestLogin testLogin,
            int outItem,
            int cksr,
            int cbDC,
            int setting,
            int valueA,
            int valueB,
            int valueC,
            int valueD,
            int valueE,
            string strlevel,
            string strlevel1
            )
        {
            IList<ResultEntry> results = new List<ResultEntry>();
            using (AppBLL bll = new AppBLL())
            {
                var classes = "";
                var courses = "";
                foreach (var gradeClass in gradeClasses)
                {
                    classes += string.Format("'{0}',", gradeClass.ClassNo);
                }
                classes = classes.Substring(0, classes.Length - 1);

                foreach (var gradeCourse in gradeCourses)
                {
                    courses += gradeCourse.CourseCode + ",";
                }
                courses = courses.Substring(0, courses.Length - 1);

                //clear old data
                var sql = "delete from s_tb_scorerep";
                bll.ExecuteNonQueryByText(sql);
                if (gradeCode.GradeNo == "33")
                {
                    sql = "Insert into s_tb_scorerep(academicyear,srid,stdname,classcode,classsn,testno,"
                               + " yw,sx,wy,zz,wl,hx,dl,ls,sw,jsj,ty,zzx)"
                               + " select AcademicYear,srid,stdName,gradename + '('+substring(classCode,3,2)+')班' classcode, classsn,testno,"
                               + " sum(case When CourseCode='21001' then numscore else 0 end) 'yw',"
                               + " sum(case When CourseCode='21002' then numscore else 0 end) 'sx',"
                               + " sum(case When CourseCode='21003' then numscore else 0 end) 'wy',"
                               + " sum(case When CourseCode='21004' then numscore else 0 end) 'zz',"
                               + " sum(case When CourseCode='21005' then numscore else 0 end) 'wl',"
                               + " sum(case When CourseCode='21006' then numscore else 0 end) 'hx',"
                               + " sum(case When CourseCode='21007' then numscore else 0 end) 'dl',"
                               + " sum(case When CourseCode='21008' then numscore else 0 end) 'ls',"
                               + " sum(case When CourseCode='21009' then numscore else 0 end) 'sw',"
                               + " sum(case When CourseCode='21010' then numscore else 0 end) 'jsj',"
                               + " sum(case When CourseCode='21013' then numscore else 0 end) 'ty',"
                               + " sum(case When CourseCode='31017' then numscore else 0 end) 'ZZX'"
                               + " from s_vw_classScoreNum "
                               + " where Academicyear=@micYear"
                               + " and testno=@testNo"
                               + " and classCode in ({0})";
                    sql = string.Format(sql, classes);
                }
                else
                {
                    sql = "Insert into s_tb_scorerep(academicyear,srid,stdname,classcode,classsn,testno,"
                                                  + " yw,sx,wy,zz,wl,hx,dl,ls,sw,jsj,yy,ms,ty) "
                                                  + " select AcademicYear,srid,stdName,gradename+'('+substring(classCode,3,2)+')班' classcode, classsn,testno,"
                                                  + " sum(case When CourseCode='21001' then numscore else 0 end) 'yw',"
                                                  + " sum(case When CourseCode='21002' then numscore else 0 end) 'sx',"
                                                  + " sum(case When CourseCode='21003' then numscore else 0 end) 'wy',"
                                                  + " sum(case When CourseCode='21004' then numscore else 0 end) 'zz',"
                                                  + " sum(case When CourseCode='21005' then numscore else 0 end) 'wl',"
                                                  + " sum(case When CourseCode='21006' then numscore else 0 end) 'hx',"
                                                  + " sum(case When CourseCode='21007' then numscore else 0 end) 'dl',"
                                                  + " sum(case When CourseCode='21008' then numscore else 0 end) 'ls',"
                                                  + " sum(case When CourseCode='21009' then numscore else 0 end) 'sw',"
                                                  + " sum(case When CourseCode='21010' then numscore else 0 end) 'jsj',"
                                                  + " sum(case When CourseCode='21011' then numscore else 0 end) 'yy',"
                                                  + " sum(case When CourseCode='21012' then numscore else 0 end) 'ms',"
                                                  + " sum(case When CourseCode='21013' then numscore else 0 end) 'ty' "
                                                  + " from s_vw_classScoreNum "
                                                  + " where Academicyear=@micYear"
                                                  + " and testno=@testNo"
                                                  + " and classCode in ({0})";
                    sql = string.Format(sql, classes);
                }

                if (cksr == 1) sql = sql + " and State is null";
                sql += " group by AcademicYear,srid,stdName,gradename,classcode, classsn,testno";
                bll.ExecuteNonQueryByText(sql, new { micYear = micYear, testNo = testLogin.TestLoginNo });

                var length = gradeCourses.Count();
                for (int i = 0; i < length; i++)
                {
                    string str_kc = "";
                    GradeCourse gradeCourse = gradeCourses[i];
                    if (int.Parse(gradeCourse.CourseCode) > 21009 && int.Parse(gradeCourse.CourseCode) != 31017) continue;
                    switch (gradeCourse.CourseCode)
                    {
                        case "21001": str_kc = "yw"; break;
                        case "21002": str_kc = "sx"; break;
                        case "21003": str_kc = "wy"; break;
                        case "21004": str_kc = "zz"; break;
                        case "21005": str_kc = "wl"; break;
                        case "21006": str_kc = "hx"; break;
                        case "21007": str_kc = "dl"; break;
                        case "21008": str_kc = "ls"; break;
                        case "21009": str_kc = "sw"; break;
                        case "31017": str_kc = "zzx"; break;
                        default: break;
                    }
                    sql = "select academicYear,'1' Semester,'1' TestType,TestNo,{0} CourseCode,srid,"
                                                + "{1},0 as OrderNO from s_tb_scorerep "
                                                + " where Academicyear={2}"
                                                + " and testno={3} and {1}>0";
                    sql = string.Format(sql, gradeCourse.CourseCode, str_kc, micYear, testLogin.TestLoginNo);
                    gf_ScoreOrderA(sql, gradeCourse.CourseCode, "s_tb_scorerep", str_kc + "M", 9);
                    gf_GetStdScoreB(micYear, testLogin.TestLoginNo.ToString(), gradeCourse.CourseCode, classes);
                    gp_GetTScoreB(micYear, testLogin.TestLoginNo.ToString(), gradeCourse.CourseCode, classes);

                    if (setting == 1)
                    {
                        sql = "Select count(*) as RS from s_vw_ClassScoreNum"
                                                   + " where Academicyear=@micYear"
                                                   + " and testno=@testNo"
                                                   + " and classcode in ({0})"
                                                   + " and CourseCode=@courseCode";
                        if (cksr == 0) sql += " and state is null";
                        sql = string.Format(sql, classes);
                        DataTable table = bll.FillDataTableByText(sql, new { micYear = micYear, testNo = testLogin.TestLoginNo, courseCode = gradeCourse.CourseCode });
                        var renshu = int.Parse(table.Rows[0]["RS"].ToString());

                        var tempSql = " UPDATE s_tb_normalscore SET levelscore = '{0}'"
                                                    + " FROM s_vw_ClassScoreNum as a INNER JOIN s_tb_normalscore as b"
                                                    + " ON a.SRID = b.SRID"
                                                    + " and a.Academicyear= b.Academicyear"
                                                    + " and a.TestNo=b.testno"
                                                    + " and a.coursecode=b.coursecode"
                                                    + " where a.classcode in ({1})"
                                                    + " and b.Academicyear=@micYear"
                                                    + " and b.TestNo=@testNo"
                                                    + " and b.CourseCode=@courseCode";

                        sql = tempSql + " and b.GradeOrder<=@iNumA";
                        sql = string.Format(sql, "A", classes);
                        var iNumA = renshu * valueA / 100;
                        var iNumB = -1;
                        bll.ExecuteNonQueryByText(sql, new { micYear = micYear, testNo = testLogin.TestLoginNo, courseCode = gradeCourse.CourseCode, iNumA = iNumA });

                        sql = tempSql + " and b.GradeOrder<=@iNumB and b.GradeOrder>@iNumA";
                        sql = string.Format(sql, "B", classes);
                        iNumA = renshu * valueA / 100;
                        iNumB = renshu * valueB / 100;
                        bll.ExecuteNonQueryByText(sql, new { micYear = micYear, testNo = testLogin.TestLoginNo, courseCode = gradeCourse.CourseCode, iNumA = iNumA, iNumB = iNumB });

                        sql = tempSql + " and b.GradeOrder<=@iNumB and b.GradeOrder>@iNumA";
                        sql = string.Format(sql, "C", classes);
                        iNumA = renshu * valueB / 100;
                        iNumB = renshu * valueC / 100;
                        bll.ExecuteNonQueryByText(sql, new { micYear = micYear, testNo = testLogin.TestLoginNo, courseCode = gradeCourse.CourseCode, iNumA = iNumA, iNumB = iNumB });

                        sql = tempSql + " and b.GradeOrder<=@iNumB and b.GradeOrder>@iNumA";
                        sql = string.Format(sql, "D", classes);
                        iNumA = renshu * valueC / 100;
                        iNumB = renshu * valueD / 100;
                        bll.ExecuteNonQueryByText(sql, new { micYear = micYear, testNo = testLogin.TestLoginNo, courseCode = gradeCourse.CourseCode, iNumA = iNumA, iNumB = iNumB });

                        sql = tempSql + " and b.GradeOrder<=@iNumB and b.GradeOrder>@iNumA";
                        sql = string.Format(sql, "E", classes);
                        iNumA = renshu * valueD / 100;
                        iNumB = renshu;
                        bll.ExecuteNonQueryByText(sql, new { micYear = micYear, testNo = testLogin.TestLoginNo, courseCode = gradeCourse.CourseCode, iNumA = iNumA, iNumB = iNumB });

                        if (cbDC == 1)
                        {
                            sql = " UPDATE s_tb_normalscore"
                                   + "  SET levelscore = '{0}'"
                                   + "  FROM s_vw_ClassScoreNum as a INNER JOIN s_tb_normalscore as b"
                                   + "  ON a.SRID = b.SRID "
                                   + "  and a.Academicyear= b.Academicyear "
                                   + "  and a.TestNo=b.testno "
                                   + "  and a.coursecode=b.coursecode "
                                   + "  where a.classcode in ({2})"
                                   + "  and b.Academicyear=@micYear"
                                   + "  and b.TestNo=@testNo"
                                   + "  and b.CourseCode=@courseCode"
                                   + "  and b.NumScore>= cast(right(b.markcode,3) as int)*0.6"
                                   + "  and b.LevelScore='{1}'";
                            sql = string.Format(sql, strlevel, strlevel1, classes);
                            bll.ExecuteNonQueryByText(sql, new { micYear = micYear, testNo = testLogin.TestLoginNo, courseCode = gradeCourse.CourseCode });
                        }
                    }
                    else
                    {
                        var tempSql = "UPDATE s_tb_normalscore SET levelscore = '{0}'"
                                                     + "  FROM s_vw_ClassScoreNum as a INNER JOIN s_tb_normalscore as b"
                                                     + "  ON a.SRID = b.SRID"
                                                     + "  and a.Academicyear= b.Academicyear"
                                                     + "  and a.TestNo=b.testno"
                                                     + "  and a.coursecode=b.coursecode"
                                                     + "  where a.classcode in ({1})"
                                                     + "  and b.Academicyear=@micYear"
                                                     + "  and b.TestNo=@testNo"
                                                     + "  and b.CourseCode=@courseCode";

                        sql = tempSql + " and b.NumScore>=@iNumA";
                        sql = string.Format(sql, "A", classes);
                        var iNumA = valueA;
                        var iNumB = -1;
                        bll.ExecuteNonQueryByText(sql, new { micYear = micYear, testNo = testLogin.TestLoginNo, courseCode = gradeCourse.CourseCode, iNumA = iNumA });

                        sql = tempSql + " and b.NumScore>=@iNumB and b.NumScore<@iNumA";
                        sql = string.Format(sql, "B", classes);
                        iNumA = valueA;
                        iNumB = valueB;
                        bll.ExecuteNonQueryByText(sql, new { micYear = micYear, testNo = testLogin.TestLoginNo, courseCode = gradeCourse.CourseCode, iNumA = iNumA, iNumB = iNumB });

                        sql = tempSql + " and b.NumScore>=@iNumB and b.NumScore<@iNumA";
                        sql = string.Format(sql, "C", classes);
                        iNumA = valueB;
                        iNumB = valueC;
                        bll.ExecuteNonQueryByText(sql, new { micYear = micYear, testNo = testLogin.TestLoginNo, courseCode = gradeCourse.CourseCode, iNumA = iNumA, iNumB = iNumB });

                        sql = tempSql + " and b.NumScore>=@iNumB and b.NumScore<@iNumA";
                        sql = string.Format(sql, "D", classes);
                        iNumA = valueC;
                        iNumB = valueD;
                        bll.ExecuteNonQueryByText(sql, new { micYear = micYear, testNo = testLogin.TestLoginNo, courseCode = gradeCourse.CourseCode, iNumA = iNumA, iNumB = iNumB });

                        sql = tempSql + " and b.NumScore>=0 and b.NumScore<@iNumA";
                        sql = string.Format(sql, "E", classes);
                        iNumA = valueD;
                        bll.ExecuteNonQueryByText(sql, new { micYear = micYear, testNo = testLogin.TestLoginNo, courseCode = gradeCourse.CourseCode, iNumA = iNumA });
                    }
                }

                //三总排名
                sql = " select academicYear,'1' Semester,'1' TestType,TestNo,'30000' CourseCode,srid,"
                        + "yw+sx+wy as Score,0 as OrderNO from s_tb_scorerep"
                        + " where Academicyear={0}"
                        + " and testno={1}";
                sql = string.Format(sql, micYear, testLogin.TestLoginNo);
                gf_ScoreOrderA(sql, "", "s_tb_scorerep", "YSWM", 0);

                //六总排名
                sql = "select academicYear,'1' Semester,'1' TestType,TestNo,'30000' CourseCode,srid,"
                         + "yw+sx+wy+zz+wl+hx as Score,0 as OrderNO from s_tb_scorerep"
                         + " where Academicyear={0}"
                         + " and testno={1}";
                sql = string.Format(sql, micYear, testLogin.TestLoginNo);
                gf_ScoreOrderA(sql, "", "s_tb_scorerep", "lzm", 0);

                //总分排名
                sql = "select academicYear,'1' Semester,'1' TestType,TestNo,'30000' CourseCode,srid,"
                        + "yw+sx+wy+zz+wl+hx+ls+dl+sw as Score,0 as OrderNO from s_tb_scorerep "
                        + " where Academicyear={0}"
                        + " and testno={1}";

                sql = string.Format(sql, micYear, testLogin.TestLoginNo);
                gf_ScoreOrderA(sql, "", "s_tb_scorerep", "bzm", 0);

                //获得学校编号
                sql = "update A set a.SchoolID=b.SchoolID"
                       + " from s_tb_scorerep as A,s_tb_schoolID as B"
                       + " where A.SRID = B.SRID";
                bll.ExecuteNonQueryByText(sql);

                //总分完成
                sql = "update s_tb_scorerep set "
                    + " ysw=yw+sx+wy,"
                    + " lz=yw+sx+wy+zz+wl+hx,"
                    + " bz=yw+sx+wy+zz+wl+hx+dl+ls+sw";
                bll.ExecuteNonQueryByText(sql);

                sql = "update s_tb_scorerep set wl=null where wl=0 ";
                bll.ExecuteNonQueryByText(sql);

                sql = "update s_tb_scorerep set hx=null where hx=0 ";
                bll.ExecuteNonQueryByText(sql);

                sql = "update s_tb_scorerep set sw=null where sw=0 ";
                bll.ExecuteNonQueryByText(sql);

                sql = "update s_tb_scorerep set zzx=null where zzx=0 ";
                bll.ExecuteNonQueryByText(sql);

                if (outItem == 1)
                {
                    sql = " Select Academicyear,testno,classcode,ClassSN,SchoolID,stdName,ysw,yswm,lz,lzm,bz,bzm ";
                    var length1 = gradeCourses.Count();
                    for (int i = 0; i < length1; i++)
                    {
                        string str_kc = "";
                        GradeCourse gradeCourse = gradeCourses[i];
                        switch (gradeCourse.CourseCode)
                        {
                            case "21001": str_kc = "yw"; break;
                            case "21002": str_kc = "sx"; break;
                            case "21003": str_kc = "wy"; break;
                            case "21004": str_kc = "zz"; break;
                            case "21005": str_kc = "wl"; break;
                            case "21006": str_kc = "hx"; break;
                            case "21007": str_kc = "dl"; break;
                            case "21008": str_kc = "ls"; break;
                            case "21009": str_kc = "sw"; break;
                            case "21010": str_kc = "jsj"; break;
                            case "21011": str_kc = "yy"; break;
                            case "21012": str_kc = "ms"; break;
                            case "21013": str_kc = "ty"; break;
                            case "31017": str_kc = "zzx"; break;
                            default: break;
                        }
                        if (str_kc == "jsj" || str_kc == "yy" || str_kc == "ms" || str_kc == "ty")
                        {
                            sql += "," + str_kc + " ";
                        }
                        else
                        {
                            sql += "," + str_kc + "," + str_kc + "m ";
                        }
                        mpClear(gradeCourse);
                    }
                    sql += " from s_tb_scorerep order by ClassCode,ClassSN ";
                    DataTable table = bll.FillDataTableByText(sql);
                    ResultEntry entry = new ResultEntry() { Code = 0, Message = Newtonsoft.Json.JsonConvert.SerializeObject(table) };
                    results.Add(entry);
                }
                else
                {
                    sql = " Select Academicyear,testno,classcode,ClassSN,SchoolID,stdName,ysw,yswm,lz,lzm,bz,bzm ";
                    var length1 = gradeCourses.Count();
                    for (int i = 0; i < length1; i++)
                    {
                        string str_kc = "";
                        GradeCourse gradeCourse = gradeCourses[i];
                        switch (gradeCourse.CourseCode)
                        {
                            case "21001": str_kc = "yw"; break;
                            case "21002": str_kc = "sx"; break;
                            case "21003": str_kc = "wy"; break;
                            case "21004": str_kc = "zz"; break;
                            case "21005": str_kc = "wl"; break;
                            case "21006": str_kc = "hx"; break;
                            case "21007": str_kc = "dl"; break;
                            case "21008": str_kc = "ls"; break;
                            case "21009": str_kc = "sw"; break;
                            default: break;
                        }
                        var tempSql = " update A set {0}= normalscore"
                                                   + " from s_tb_scorerep a,s_tb_normalscore b"
                                                   + " where a.Academicyear=b.Academicyear"
                                                   + " and a.testno=b.testno"
                                                   + " and a.srid=b.srid"
                                                   + " and b.coursecode='{1}'";
                        tempSql = string.Format(tempSql, str_kc, gradeCourse.CourseCode);

                        bll.ExecuteNonQueryByText(tempSql);
                        sql += "," + str_kc + "," + str_kc + "m ";
                        mpClear(gradeCourse);
                    }
                    sql += " from s_tb_scorerep order by ClassCode,ClassSN ";
                    DataTable table = bll.FillDataTableByText(sql);
                    ResultEntry entry = new ResultEntry() { Code = 0, Message = Newtonsoft.Json.JsonConvert.SerializeObject(table) };
                    results.Add(entry);
                }
            }
            return results;
        }

        private static void mpClear(GradeCourse gradeCourse)
        {
            string str_kc = "";
            if (int.Parse(gradeCourse.CourseCode) > 21009) return;
            switch (gradeCourse.CourseCode)
            {
                case "21001": str_kc = "yw"; break;
                case "21002": str_kc = "sx"; break;
                case "21003": str_kc = "wy"; break;
                case "21004": str_kc = "zz"; break;
                case "21005": str_kc = "wl"; break;
                case "21006": str_kc = "hx"; break;
                case "21007": str_kc = "dl"; break;
                case "21008": str_kc = "ls"; break;
                case "21009": str_kc = "sw"; break;
                default: break;
            }
            using (AppBLL bll = new AppBLL())
            {
                var sql = " update s_tb_scorerep set " + str_kc + "= null where " + str_kc + "=0";
                bll.ExecuteNonQueryByText(sql);

                sql = " update s_tb_scorerep set " + str_kc + "M = null where " + str_kc + "=0";
                bll.ExecuteNonQueryByText(sql);
            }
        }

        [WebMethod]
        public static string GetTestLoginByYear(int micYear)
        {
            DataTable table = App.Score.Db.UtilBLL.GetTestLoginByYear(micYear);
            return Newtonsoft.Json.JsonConvert.SerializeObject(table);
        }
    }
}