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
                          + " and ClassCode in (@classCode)"
                          + " and Numscore<200"
                          + " and Numscore is not Null";
                DataTable table = bll.FillDataTableByText(sql, new { micYear = micYear, testNo = testNo, courseCode = courseCode, classCode = classCode });
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
                       + " and classcode in (@classcode)"
                       + " and CourseCode=@eourseCode";

                DataTable table = bll.FillDataTableByText(sql, new { micYear = micYear, testNo = testNo, courseCode = courseCode, classCode = classCode });
                if (table.Rows.Count == 0) return;
                var XT = float.Parse(table.Rows[0]["maxBZScore"].ToString());
                var YT = float.Parse(table.Rows[0]["minBZScore"].ToString());
                if (XT == 0 || YT == 0) return;
                int k = (int) Math.Floor(25 / XT);
                if (k> Math.Abs(Math.Floor(75 / YT))) k = (int) Math.Abs(Math.Floor(75 / YT ));

                sql = " UPDATE b" 
                        + " SET b.standardScore = (75+b.Normalscore*({0}))"
                        + " FROM s_vw_ClassScoreNum as a INNER JOIN s_tb_normalscore as b"
                        + " ON a.SRID = b.SRID"
                        + " and a.Academicyear= b.Academicyear"
                        + " and a.TestNo=b.testno"
                        + " and a.coursecode=b.coursecode"
                        + " where a.Academicyear=@micYear"
                        + " and a.Testno=@testNo"
                        + " and a.classcode in (@classcode)"
                        + " and a.CourseCode=@eourseCode";;
                sql = string.Format(sql, k);
                bll.ExecuteNonQueryByText(sql, new { micYear = micYear, testNo = testNo, courseCode = courseCode, classCode = classCode });
            }
        }


        [WebMethod]
        public static IList<ResultEntry> AnalyzeSuper(int micYear, 
            GradeCode gradeCode, 
            IList<GradeClass> gradeClasses, 
            IList<GradeCourse> gradeCourses, 
            TestType testType,
            TestLogin testLogin, 
            int ValueA,
            int ValueB,
            int ValueC,
            int ValueD,
            int ValueE
            )
        {
            IList<ResultEntry> results = new List<ResultEntry>();
            using (AppBLL bll = new AppBLL())
            {
            }
            return results;
        }
    }
}