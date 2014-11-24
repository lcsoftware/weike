﻿/* **************************************************************
  * Copyright(c) 2014 Score.web, All Rights Reserved.   
  * File             : Statistic.aspx.cs
  * Description      : 老师学生统计分析
  * Author           : zhaotianyu 
  * Created          : 2014-11-14  
  * Revision History : 
******************************************************************/
namespace App.Web.Score.DataProvider
{
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
    public partial class Statistic : System.Web.UI.Page
    {
        #region 学生统计

        /// <summary>
        ///考试情况统计 (基本情况)  
        /// </summary>
        /// <param name="micYear"></param>
        /// <param name="testNo"></param>
        /// <param name="gradeCourse"></param>
        /// <param name="gradeClass"></param>
        /// <param name="student"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetStat07Base(int micYear, TestLogin testNo, GradeCourse gradeCourse, GradeClass gradeClass, Student student)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "";
                var tempSql = "";
                DataTable table = new DataTable();
                //参加考试人数
                sql = "Select count(*) as Renshu, round(Avg(Numscore), 4) as AvgScore,Max(Numscore) as MaxScore,Min(Numscore) as MinScore,round(stdevp(NumScore), 4) as Fangcha"
                 + " from s_vw_ClassScoreNum "
                 + " where Academicyear=@micYear"
                 + " and TestNo=@testNo"
                 + " and CourseCode=@courseCode"
                 + " and ClassCode=@classNo";
                table = bll.FillDataTableByText(sql, new { micYear = micYear, testNo = testNo.TestLoginNo, courseCode = gradeCourse.CourseCode, classNo = gradeClass.ClassNo });
                string RenShu = table.Rows[0]["RenShu"].ToString();
                string AvgScore = table.Rows[0]["AvgScore"].ToString();
                string MaxScore = table.Rows[0]["MaxScore"].ToString();
                string MinScore = table.Rows[0]["MinScore"].ToString();
                string Fangcha = table.Rows[0]["Fangcha"].ToString();
                tempSql = string.Format("Select {0} as RenShu, {1} as AvgScore, {2} as MaxScore, {3} as MinScore, {4} as Fangcha",
                                            RenShu, AvgScore, MaxScore, MinScore, Fangcha);

                //学生的分数
                sql = "Select NumScore,NormalScore,ClassOrder,GradeOrder from s_tb_normalscore"
                + " where Academicyear=@micYear"
                + " and TestNo=@testNo"
                + " and CourseCode=@courseCode"
                + " and SRID=@SRID";
                table = bll.FillDataTableByText(sql, new { micYear = micYear, testNo = testNo.TestLoginNo, courseCode = gradeCourse.CourseCode, SRID = student.StudentId });
                string NumScore = table.Rows[0]["NumScore"].ToString();
                string NormalScore = table.Rows[0]["NormalScore"].ToString();
                string ClassOrder = table.Rows[0]["ClassOrder"].ToString();
                string GradeOrder = table.Rows[0]["GradeOrder"].ToString();
                tempSql += string.Format(",{0} as NumScore, {1} as NormalScore, {2} as ClassOrder, {3} as GradeOrder",
                                            NumScore, NormalScore, ClassOrder, GradeOrder);

                //学生的历史平均分
                sql = " Select round(Avg(NumScore), 4) as AvgScore from s_tb_normalscore"
                + " where CourseCode=@courseCode"
                + " and SRID=@SRID";
                table = bll.FillDataTableByText(sql, new { courseCode = gradeCourse.CourseCode, SRID = student.StudentId });
                string HisAvgScore = table.Rows[0]["AvgScore"].ToString();
                tempSql += string.Format(",{0} as HisAvgScore", HisAvgScore);

                //考试时间
                sql = " Select convert(varchar(19),TestTime, 25) as TestTime from s_tb_testlogin"
                       + " where Academicyear=@micYear"
                       + " and testno=@testNo";
                table = bll.FillDataTableByText(sql, new { micYear = micYear, testNo = testNo.TestLoginNo });
                string TestTime = table.Rows.Count == 0 ? "不详" : table.Rows[0]["TestTime"].ToString();
                tempSql += string.Format(",\'{0}\' as TestTime", TestTime);
                //任课教师
                sql = " Select a.teacherid,b.Name from tbTeacherClass as a,tbUsergroupinfo as b"
                       + " where a.teacherid=b.teacherid"
                       + " and a.Academicyear=@micYear"
                       + " and a.ClassID=@classCode"
                       + " and a.CourseCode=@courseCode";
                table = bll.FillDataTableByText(sql, new { micYear = micYear, courseCode = gradeCourse.CourseCode, classCode = gradeClass.ClassNo });
                string TeacherName = table.Rows.Count == 0 ? "不详" : table.Rows[0]["Name"].ToString();
                tempSql += string.Format(",\'{0}\' as TeacherName", TeacherName);
                table = bll.FillDataTableByText(tempSql, null);
                return Newtonsoft.Json.JsonConvert.SerializeObject(table);
            }
        }

        [WebMethod]
        public static IList<ChartOption> GetStat07Charts(int micYear, TestLogin testNo, GradeCourse gradeCourse, GradeClass gradeClass, Student student, int scoreType)
        {
            using (AppBLL bll = new AppBLL())
            {
                IList<ChartOption> options = new List<ChartOption>();
                ChartOption option1 = new ChartOption() { legend = new Legend(), xAxis = new XAxis() };
                ChartOption option2 = new ChartOption() { legend = new Legend(), xAxis = new XAxis() };
                ChartOption option3 = new ChartOption() { legend = new Legend(), xAxis = new XAxis() };
                options.Add(option1);
                options.Add(option2);
                options.Add(option3);

                option1.legend.data.Add(student.StdName);
                option1.legend.data.Add("班级平均分");

                option1.series.Add(new SeriesItem() { type = "line", name = student.StdName });
                option1.series.Add(new SeriesItem() { type = "line", name = "班级平均分" });

                option2.legend.data.Add(student.StdName);
                option2.legend.data.Add("年级平均分");
                option2.legend.data.Add("班级平均分");

                option2.series.Add(new SeriesItem() { type = "line", name = student.StdName });
                option2.series.Add(new SeriesItem() { type = "line", name = "年级平均分" });
                option2.series.Add(new SeriesItem() { type = "line", name = "班级平均分" });

                option3.legend.data.Add(student.StdName);
                option3.legend.data.Add("年级总分平均");
                option3.legend.data.Add("班级总分平均");

                option3.series.Add(new SeriesItem() { type = "line", name = student.StdName });
                option3.series.Add(new SeriesItem() { type = "line", name = "年级总分平均" });
                option3.series.Add(new SeriesItem() { type = "line", name = "班级总分平均" });

                var sql = "";
                DataTable table = new DataTable();

                sql = "Select Academicyear,TypeName,TestNo,ClassCode from s_vw_ClassScoreNum"
                 + " Where CourseCode=@courseCode"
                 + " and SRID =@SRID"
                 + " group by Academicyear,TypeName,TestNo,ClassCode"
                 + " Order by Academicyear,Cast(TestNo as Int)";
                table = bll.FillDataTableByText(sql, new { micYear = micYear, testNo = testNo.TestLoginNo, courseCode = gradeCourse.CourseCode, SRID = student.StudentId });
                int length = table.Rows.Count;
                for (int i = 0; i < length; i++)
                {
                    var tempType = table.Rows[i]["TypeName"].ToString().Substring(0, 2);
                    var temptestno = table.Rows[i]["TestNo"].ToString();
                    var tempyear = table.Rows[i]["Academicyear"].ToString();
                    var tempClass = table.Rows[i]["ClassCode"].ToString();

                    option1.xAxis.data.Add(string.Format("{0}{1}({2})", tempType.Trim(), temptestno.Trim(), tempyear));
                    option2.xAxis.data.Add(string.Format("{0}{1}({2})", tempType.Trim(), temptestno.Trim(), tempyear));
                    option3.xAxis.data.Add(string.Format("{0}{1}({2})", tempType.Trim(), temptestno.Trim(), tempyear));

                    option1.yAxis.name = "名次";
                    option2.yAxis.name = "学科成绩";
                    option3.yAxis.name = "总分成绩";

                    //加入学生的名次
                    sql = "Select classorder from s_tb_normalscore "
                           + " where AcademicYear=@micYear"
                           + " and TestNo=@testNo"
                           + " and SRID=@SRID"
                           + " and CourseCode=@courseCode";
                    DataTable tempTable = bll.FillDataTableByText(sql, new { micYear = tempyear, testNo = temptestno, courseCode = gradeCourse.CourseCode, SRID = student.StudentId });
                    ((SeriesItem)option1.series[0]).data.Add(tempTable.Rows.Count == 0 ? "0" : tempTable.Rows[0]["classorder"].ToString());
                    //if (tempTable.Rows.Count > 0)
                    //{
                    //    int clsOrder = int.Parse(tempTable.Rows[0]["classorder"].ToString());
                    //    option1.yAxis.min = Math.Min(option1.yAxis.min, clsOrder);
                    //    option1.yAxis.max = Math.Max(option1.yAxis.max, clsOrder);
                    //}
                    //加入班级平均成绩的排名
                    sql = " Select Avg(NumScore) as AvgScore from s_vw_ClassScoreNum"
                            + " Where CourseCode=@courseCode"
                            + " and ClassCode=@tempClass"
                            + " and Academicyear =@tempYear"
                            + " and TestNo=@tempTestNo";
                    tempTable = bll.FillDataTableByText(sql, new
                    {
                        courseCode = gradeCourse.CourseCode,
                        tempClass = tempClass,
                        tempYear = tempyear,
                        tempTestNo = temptestno
                    });
                    var tempClassAvg = Math.Round(float.Parse(tempTable.Rows[0]["AvgScore"].ToString()));

                    sql = " Select * from s_vw_ClassScoreNum "
                     + " where Numscore>=@NumScore"
                     + " and ClassCode=@tempClass"
                     + " and Academicyear=@tempYear"
                     + " and TestNo=@temptestno"
                     + " and CourseCode=@courseCode"
                     + " order by Numscore";
                    tempTable = bll.FillDataTableByText(sql, new
                    {
                        NumScore = tempClassAvg,
                        courseCode = gradeCourse.CourseCode,
                        tempClass = tempClass,
                        tempYear = tempyear,
                        temptestno = temptestno
                    });
                    if (tempTable.Rows.Count == 0)
                    {
                        ((SeriesItem)option1.series[1]).data.Add("0");
                    }
                    else
                    {
                        ((SeriesItem)option1.series[1]).data.Add(tempTable.Rows[0]["ClassOrder"].ToString());
                    }
                    //加学生
                    sql = "Select typename,{0} as score from s_vw_ClassScoreNum"
                           + " where CourseCode=@courseCode"
                           + " and SRID=@SRID"
                           + " and Academicyear=@tempYear"
                           + " and TestNo=@temptestno";
                    sql = string.Format(sql, scoreType == 1 ? "NumScore" : "NormalScore");

                    tempTable = bll.FillDataTableByText(sql, new
                    {
                        courseCode = gradeCourse.CourseCode,
                        SRID = student.StudentId,
                        tempYear = tempyear,
                        temptestno = temptestno
                    });
                    ((SeriesItem)option2.series[0]).data.Add(tempTable.Rows.Count == 0 || string.IsNullOrEmpty(tempTable.Rows[0]["score"].ToString()) ? "0" : tempTable.Rows[0]["score"].ToString());

                    //根据条件查班级
                    sql = "Select avg({0}) as score from s_vw_ClassScoreNum"
                          + " where CourseCode=@courseCode"
                          + " and ClassCode=@classCode"
                          + " and Academicyear=@tempYear"
                          + " and TestNo=@temptestno";
                    sql = string.Format(sql, scoreType == 1 ? "NumScore" : "NormalScore");
                    tempTable = bll.FillDataTableByText(sql, new
                    {
                        courseCode = gradeCourse.CourseCode,
                        classCode = gradeClass.ClassNo,
                        tempYear = tempyear,
                        temptestno = temptestno
                    });
                    ((SeriesItem)option2.series[2]).data.Add(tempTable.Rows.Count == 0 || string.IsNullOrEmpty(tempTable.Rows[0]["score"].ToString()) ? "0" : tempTable.Rows[0]["score"].ToString());
                    //年级
                    sql = " Select Avg({0}) as score from s_vw_ClassScoreNum"
                       + " where CourseCode=@courseCode"
                       + " and GradeNo=@gradeNo"
                       + " and Academicyear=@tempyear"
                       + " and TestNo=@temptestno";
                    sql = string.Format(sql, scoreType == 1 ? "NumScore" : "NormalScore");
                    tempTable = bll.FillDataTableByText(sql, new
                    {
                        courseCode = gradeCourse.CourseCode,
                        gradeNo = gradeClass.GradeNo,
                        tempYear = tempyear,
                        temptestno = temptestno
                    });
                    ((SeriesItem)option2.series[1]).data.Add(tempTable.Rows.Count == 0 || string.IsNullOrEmpty(tempTable.Rows[0]["score"].ToString()) ? "0" : tempTable.Rows[0]["score"].ToString());

                    //加入总分图
                    //先年级
                    sql = " Select Avg({0}) as score from s_vw_SumScore"
                    + " where GradeNo=@gradeNo"
                    + " and Academicyear=@tempYear"
                    + " and TestNo=@temptestno";

                    sql = string.Format(sql, scoreType == 1 ? "sumScore" : "SumBZ");
                    tempTable = bll.FillDataTableByText(sql, new
                    {
                        gradeNo = gradeClass.GradeNo,
                        tempYear = tempyear,
                        temptestno = temptestno
                    });
                    ((SeriesItem)option3.series[0]).data.Add(tempTable.Rows.Count == 0 || string.IsNullOrEmpty(tempTable.Rows[0]["score"].ToString()) ? "0" : tempTable.Rows[0]["score"].ToString());

                    //根据条件查班级
                    sql = " Select Avg({0}) as score from s_vw_SumScore"
                           + " where ClassCode=@classCode"
                           + " and Academicyear=@tempyear"
                           + " and TestNo=@temptestno";
                    sql = string.Format(sql, scoreType == 1 ? "sumScore" : "SumBZ");
                    tempTable = bll.FillDataTableByText(sql, new
                    {
                        classCode = gradeClass.ClassNo,
                        tempYear = tempyear,
                        temptestno = temptestno
                    });
                    ((SeriesItem)option3.series[1]).data.Add(tempTable.Rows.Count == 0 || string.IsNullOrEmpty(tempTable.Rows[0]["score"].ToString()) ? "0" : tempTable.Rows[0]["score"].ToString());

                    //加学生
                    sql = " Select {0} as score from s_vw_SumScore"
                           + " where SRID=@SRID"
                           + " and Academicyear=@tempYear"
                           + " and TestNo=@temptestno";
                    sql = string.Format(sql, scoreType == 1 ? "sumScore" : "SumBZ");
                    tempTable = bll.FillDataTableByText(sql, new
                    {
                        SRID = student.StudentId,
                        tempYear = tempyear,
                        temptestno = temptestno
                    });
                    ((SeriesItem)option3.series[2]).data.Add(tempTable.Rows.Count == 0 || string.IsNullOrEmpty(tempTable.Rows[0]["score"].ToString()) ? "0" : tempTable.Rows[0]["score"].ToString());
                }
                return options;
            }
        }

        [WebMethod]
        public static string GetStat07Data(int micYear, GradeCourse gradeCourse, GradeClass gradeClass, Student student)
        {
            using (AppBLL bll = new AppBLL())
            {
                DataTable table = bll.FillDataTable("s_p_StudentAllTestScore", new { AcademicYear = micYear, CourseCode = gradeCourse.CourseCode, ClassNo = gradeClass.ClassNo, SRID = student.StudentId });
                return Newtonsoft.Json.JsonConvert.SerializeObject(table);
            }
        }

        [WebMethod]
        public static IList<ResultEntry> GetStat08Charts(int micYear, GradeCourse gradeCourse, GradeClass gradeClass, Student student, int checkValue)
        {
            IList<ResultEntry> results = new List<ResultEntry>();
            using (AppBLL bll = new AppBLL())
            {
                ChartOption chartOption = new ChartOption() { legend = new Legend(), xAxis = new XAxis() };
                //chartOption.title.text = string.Format("{0}{1}的年级排名图", student.StdName, gradeCourse.FullName);
                chartOption.title.x = "center";
                chartOption.legend.x = "left";
                chartOption.legend.data.Add(student.StdName);
                chartOption.legend.data.Add("年级名次上线");
                chartOption.legend.data.Add("年级名次下线");
                SeriesItem studentSeries = new SeriesItem() { type = "line", name = student.StdName };
                SeriesItem topSeries = new SeriesItem() { type = "line", name = "年级名次上线" };
                SeriesItem bottomSeries = new SeriesItem() { type = "line", name = "年级名次下线" };
                chartOption.series.Add(studentSeries);
                chartOption.series.Add(topSeries);
                chartOption.series.Add(bottomSeries);
                ResultEntry entry = null;
                var sql = "Select count(*) as njrs from tbStudentClass"
                           + " where substring(ClassCode,1,2)=@gradeNo"
                           + " and Academicyear=@micYear";
                DataTable table = bll.FillDataTableByText(sql, new { micYear = micYear, gradeNo = gradeClass.GradeNo });
                var njrs = int.Parse(table.Rows[0]["njrs"].ToString());
                if (njrs == 0)
                {
                    entry = new ResultEntry() { Code = -1, Message = "未发现数据,可能您未录入成绩" };
                    results.Insert(0, entry);
                    return results;
                }

                entry = new ResultEntry() { Code = 0, Message = chartOption };
                results.Add(entry);

                var tempXMC = njrs / 6;
                var tempSMC = njrs - tempXMC;

                sql = "SELECT AcademicYear,typename,testno,convert(varchar(10), testtime, 25) as testtime, round(NumScore, 4) as NumScore,classorder,gradeorder"
                       + " FROM s_vw_ClassScoreNum  where srid=@srid"
                       + " and coursecode=@courseCode";

                if (checkValue == 1)
                {
                    sql += " and Academicyear=" + micYear.ToString();
                }
                sql += " order by Academicyear,cast(testno as int)";
                table = bll.FillDataTableByText(sql, new { courseCode = gradeCourse.CourseCode, srid = student.StudentId });
                var row1 = "";
                var row2 = "";
                var row3 = "";
                var row4 = "";
                var row5 = "";
                var length = table.Rows.Count;
                for (int i = 0; i < length; i++)
                {
                    chartOption.xAxis.data.Add(string.Format("{0}{1}", table.Rows[i]["Academicyear"].ToString(), table.Rows[i]["TypeName"].ToString().Substring(0, 2)));
                    studentSeries.data.Add(table.Rows[i]["gradeOrder"].ToString());
                    topSeries.data.Add(tempXMC.ToString());
                    bottomSeries.data.Add(tempSMC.ToString());
                    row1 += string.Format("'{0}{1}' as S_{2},", table.Rows[i]["TypeName"].ToString().Substring(0, 2), table.Rows[i]["testno"].ToString(), i + 1);
                    row2 += string.Format("'{0}',", table.Rows[i]["testtime"].ToString());
                    row3 += string.Format("'{0}',", float.Parse(table.Rows[i]["NumScore"].ToString()).ToString("f4"));
                    row4 += string.Format("'{0}',", table.Rows[i]["gradeOrder"].ToString());
                    row5 += string.Format("'{0}',", table.Rows[i]["classOrder"].ToString());
                }
                row1 = "SELECT '历次考试' as S_0," + row1.Substring(0, row1.Length - 1);
                row2 = " union all SELECT ' ' as S_0," + row2.Substring(0, row2.Length - 1);
                row3 = " union all SELECT '成绩' as S_0," + row3.Substring(0, row3.Length - 1);
                row4 = " union all SELECT '年级排名' as S_0," + row4.Substring(0, row4.Length - 1);
                row5 = " union all SELECT '班级排名' as S_0," + row5.Substring(0, row5.Length - 1);
                sql = row1 + row2 + row3 + row4 + row5;
                table = bll.FillDataTableByText(sql, null);
                entry = new ResultEntry() { Code = 1, Message = Newtonsoft.Json.JsonConvert.SerializeObject(table) };
                results.Add(entry);
            }
            return results;
        }

        private static string mf_getTable()
        {
            using (AppBLL bll = new AppBLL())
            {
                int tempStr = 1;
                DataTable table = bll.FillDataTable("p_getTableName", null);
                if (table.Rows.Count > 0)
                {
                    tempStr = int.Parse(table.Rows[0][0].ToString()) + 1;
                }
                return string.Format("s_tb_TempScore{0}", tempStr);
            }
        }

        private static void mp_ScoreOrder(string orderSql)
        {
            using (AppBLL bll = new AppBLL())
            {
                var tempTableName = mf_getTable();
                var sql = "create table {0}(academicYear char(4),Semester char(2),TestType char(1),TestNo char(5),CourseCode char(5),srid char(19),Score numeric(5,1),OrderNO integer)";
                sql = string.Format(sql, tempTableName);
                bll.ExecuteNonQueryByText(sql);

                sql = "insert into {0}(academicYear,Semester,TestType,TestNo,CourseCode,srid,Score,OrderNO) {1}";
                sql = string.Format(sql, tempTableName, orderSql);
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
            }
        }

        [WebMethod]
        public static IList<ResultEntry> GetStat09Charts(int micYear, GradeCode gradeCode, GradeClass gradeClass, Student student, IList<GradeCourse> courseChecks, int otherCheckValue)
        {
            ChartOption chartOption = new ChartOption() { legend = new Legend(), xAxis = new XAxis() };
            chartOption.title.x = "center";
            chartOption.legend.x = "left";

            var courseCodes = "";
            for (int i = 0; i < courseChecks.Count; i++)
            {
                var course = courseChecks[i];
                courseCodes += course.CourseCode + ",";
                chartOption.legend.data.Add(course.FullName);
                SeriesItem courseSeries = new SeriesItem() { type = "line", name = course.FullName };
                chartOption.series.Add(courseSeries);
            }
            courseCodes = courseCodes.Substring(0, courseCodes.Length - 1);

            chartOption.legend.data.Add("所选课程总分");
            SeriesItem sumSeries = new SeriesItem() { type = "line", name = "所选课程总分" };
            chartOption.series.Add(sumSeries);

            IList<ResultEntry> results = new List<ResultEntry>();
            using (AppBLL bll = new AppBLL())
            {
                var tempTableName = mf_getTable();
                try
                {

                    ResultEntry entry = null;
                    var sql = "SELECT a.AcademicYear,a.typename,a.testno, "
                                                   + " convert(varchar(10), b.testtime, 25) as testtime,b.GradeNo "
                                                   + " FROM  s_tb_testlogin b inner JOIN "
                                                   + " s_vw_ClassScoreNum a ON b.AcademicYear = a.AcademicYear AND "
                                                   + " b.TestNo = a.testno"
                                                   + " where a.srid=@srid"
                                                   + " and b.CourseCode='00000'";
                    if ((2 & otherCheckValue) == 2)
                    {
                        sql += " and b.testtype<>'0'";
                    }
                    if ((1 & otherCheckValue) == 0)
                    {
                        sql += string.Format(" and a.Academicyear={0}", micYear);
                    }
                    sql += "group by a.AcademicYear,a.typename,a.testno,b.GradeNo, b.testtime order by a.Academicyear ,cast(a.testno as int)";

                    DataTable table = bll.FillDataTableByText(sql, new { srid = student.StudentId });
                    var length = table.Rows.Count;

                    for (int i = 0; i < length; i++)
                    {
                        DataRow dr = table.Rows[i];
                        var tempYear = dr["Academicyear"].ToString();
                        var tempgrade = dr["GradeNo"].ToString();
                        var tempstr = dr["TestTime"].ToString();
                        var testNo = dr["TestNo"].ToString();
                        var OrderSql = "select AcademicYear,semester,testType,testno,'多门' as coursecode,srid,sum(numscore) as numscore,0 as OrderNO"
                          + " from s_vw_ClassScoreNum where AcademicYear={0}"
                          + " and testno={1}"
                          + " and GradeNo={2}"
                          + " and CourseCode in ({3}) Group by AcademicYear,semester,testType,testno,srid";        //进行排名
                        mp_ScoreOrder(string.Format(OrderSql, tempYear, testNo, tempgrade, courseCodes));
                        sql = string.Format("Select srid,score,OrderNO from {0} Where SRID=@srid", tempTableName);
                        DataTable tempTable = bll.FillDataTableByText(sql, new { srid = student.StudentId });

                        chartOption.xAxis.data.Add(tempstr);
                        sumSeries.data.Add(tempTable.Rows[0]["OrderNo"].ToString());

                        for (int j = 0; j < courseChecks.Count; j++)
                        {
                            var course = courseChecks[j];
                            SeriesItem courseSeries = (SeriesItem)chartOption.series[j];

                            sql = " SELECT b.TestNo, b.AcademicYear, a.numscore, a.classorder,"
                                          + " a.gradeorder, b.TestTime"
                                          + " FROM s_vw_ClassScoreNum a INNER JOIN"
                                          + " s_tb_testlogin b ON a.AcademicYear = b.AcademicYear AND a.testno = b.TestNo"
                                          + " where a.coursecode=@courseCode"
                                          + " and a.SRID=@srid"
                                          + " and a.testno=@testNo";

                            if ((2 & otherCheckValue) == 2)
                            {
                                sql += " and a.testtype<>'0'";
                            }
                            if ((1 & otherCheckValue) == 0)
                            {
                                sql += string.Format(" and a.Academicyear={0}", micYear);
                            }
                            sql += " order by a.academicyear,cast(a.testno as int)";
                            tempTable = bll.FillDataTableByText(sql, new { srid = student.StudentId, courseCode = course.CourseCode, testNo = testNo });
                            courseSeries.data.Add(tempTable.Rows[0]["GradeOrder"].ToString());
                        }
                    }
                    entry = new ResultEntry() { Code = 0, Message = chartOption };
                    results.Add(entry);
                    if ((4 & otherCheckValue) == 4)
                    {
                        //出报表
                        table = bll.FillDataTable("s_p_StudentAllTestScoreM", new { AcademicYear = micYear, ClassNo = gradeClass.ClassNo, SRID = student.StudentId });
                        entry = new ResultEntry() { Code = 1, Message = Newtonsoft.Json.JsonConvert.SerializeObject(table) };
                        results.Add(entry);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    //删除临时表
                    var sql = "if exists(select * from sysobjects where name = '{0}' and xtype='U') drop table {0}";
                    sql = string.Format(sql, tempTableName);
                    bll.ExecuteNonQueryByText(sql);
                }
            }
            return results;
        }

        [WebMethod]
        public static IList<ResultEntry> GetStat10Base(int micYear, IList<Student> studentChecks, TestLogin testLogin)
        {
            var studentIds = "";
            foreach (var student in studentChecks)
            {
                studentIds += student.StudentId + ",";
            }
            studentIds = studentIds.Substring(0, studentIds.Length - 1);

            IList<ResultEntry> results = new List<ResultEntry>();
            using (AppBLL bll = new AppBLL())
            {
                var sql = "Select stdname, courseName,TypeName, NumScore, levelscore," +
                          " case semester when 1 then '第一学期'" +
                          " when 2 then '第二学期' end semester," +
                          " academicyear From s_vw_ClassScoreNum Where academicyear=@micYear And testno=@testNo And SRid in ({0})";
                sql = string.Format(sql, studentIds);
                DataTable table = bll.FillDataTableByText(sql, new { micYear = micYear, testNo = testLogin.TestLoginNo });
                ResultEntry entry = new ResultEntry() { Code = 0, Message = Newtonsoft.Json.JsonConvert.SerializeObject(table) };
                results.Add(entry);
            }
            return results;
        }

        [WebMethod]
        public static IList<ResultEntry> GetStat10Data1(int micYear, GradeCode gradeCode, GradeClass gradeClass, IList<Student> studentChecks, TestType testType, TestLogin testLogin, int printMethod, int semester)
        {
            var studentIds = "";
            foreach (var student in studentChecks)
            {
                studentIds += student.StudentId + ",";
            }
            studentIds = studentIds.Substring(0, studentIds.Length - 1);

            IList<ResultEntry> results = new List<ResultEntry>();
            using (AppBLL bll = new AppBLL())
            {
                if (printMethod == 2) { 
                    DataTable table1 = bll.FillDataTableByText("s_p_classScore", new { DateYear = micYear, ClassCode = gradeClass.ClassNo, semester = semester, Flag = 0});
                    ResultEntry entry1 = new ResultEntry() { Code = 0, Message = Newtonsoft.Json.JsonConvert.SerializeObject(table1) };
                    results.Add(entry1);
                    return results;
                }

                //var sql = "SELECT '{0}({1})班' as S_0, '{2}' as S_1, '语文' as S_2,'数学' as S_3,'外语' as S_4,'政治' as S_5,'物理' as S_6,'化学' as S_7,'地理' as S_8,'历史' as S_9,'生物' as S_10,'电脑' as S_11, '总分' as S_12";
                //sql = string.Format(sql, gradeCode.GradeBriefName, gradeClass.ClassNo, "种类");

                var sql = " select '原始分' S_0"
                          + " ,sum(case When CourseCode='21001' then numscore else 0 end) 'Yuwen'"
                          + " ,sum(case When CourseCode='21002' then numscore else 0 end) 'ShuXue'"
                          + " ,sum(case When CourseCode='21003' then numscore else 0 end) 'WaiYu'"
                          + " ,sum(case When CourseCode='21004' then numscore else 0 end) 'Zhengzhi'"
                          + " ,sum(case When CourseCode='21005' then numscore else 0 end) 'WuLi'"
                          + " ,sum(case When CourseCode='21006' then numscore else 0 end) 'HuaXue'"
                          + " ,sum(case When CourseCode='21007' then numscore else 0 end) 'DiLi'"
                          + " ,sum(case When CourseCode='21008' then numscore else 0 end) 'lishi'"
                          + " ,sum(case When CourseCode='21009' then numscore else 0 end) 'sw'"
                          + " ,sum(case When CourseCode='{0}' then numscore else 0 end) 'diannao'"
                          + " ,sum(numscore) as numscore";
                sql = string.Format(sql, gradeCode.GradeNo.Equals("33") ? "31017" : "21010");

                sql += ", '标准分' S_1"
                          + " ,sum(case When CourseCode='21001' then normalscore else 0 end) 'TYuwen'"
                          + " ,sum(case When CourseCode='21002' then normalscore else 0 end) 'TShuXue'"
                          + " ,sum(case When CourseCode='21003' then normalscore else 0 end) 'TWaiYu'"
                          + " ,sum(case When CourseCode='21004' then normalscore else 0 end) 'TZhengzhi'"
                          + " ,sum(case When CourseCode='21005' then normalscore else 0 end) 'TWuLi'"
                          + " ,sum(case When CourseCode='21006' then normalscore else 0 end) 'THuaXue'"
                          + " ,sum(case When CourseCode='21007' then normalscore else 0 end) 'TDiLi'"
                          + " ,sum(case When CourseCode='21008' then normalscore else 0 end) 'Tlishi'"
                          + " ,sum(case When CourseCode='21009' then normalscore else 0 end) 'Tsw'"
                          + " ,sum(case When CourseCode='{0}' then normalscore else 0 end) 'Tdiannao'";
                sql = string.Format(sql, "姓名", gradeCode.GradeNo.Equals("33") ? "31017" : "21010");

                sql += " ,stdname as StdName, '等第分' as S_2"
                         + " ,max(case When CourseCode='21001' then Levelscore else null end) 'LYuwen'"
                         + " ,max(case When CourseCode='21002' then Levelscore else null end) 'LShuXue'"
                         + " ,max(case When CourseCode='21003' then Levelscore else null end) 'LWaiYu'"
                         + " ,max(case When CourseCode='21004' then Levelscore else null end) 'LZhengzhi'"
                         + " ,max(case When CourseCode='21005' then Levelscore else null end) 'LWuLi'"
                         + " ,max(case When CourseCode='21006' then Levelscore else null end) 'LHuaXue'"
                         + " ,max(case When CourseCode='21007' then Levelscore else null end) 'LDiLi'"
                         + " ,max(case When CourseCode='21008' then Levelscore else null end) 'Llishi'"
                         + " ,max(case When CourseCode='21009' then Levelscore else null end) 'Lsw'"
                         + " ,max(case When CourseCode='{0}' then Levelscore else null end) 'Ldiannao'";
                sql = string.Format(sql, gradeCode.GradeNo.Equals("33") ? "31017" : "21010"); 

                sql += " FROM  s_vw_ClassScoreNum a ";
                sql += " where a.Testno=@testNo and a.AcademicYear=@micYear";
                sql += " and classCode=@classNo";
                if (studentChecks.Any())
                {
                    sql += string.Format(" and a.srid in ({0})", studentIds);
                }
                sql += " group by a.srid,stdname,a.AcademicYear,semester,gradename,classCode,TypeName";

                DataTable table = bll.FillDataTableByText(sql, new { micYear = micYear, testNo = testLogin.TestLoginNo, classNo = gradeClass.ClassNo });
                ResultEntry entry = new ResultEntry() { Code = 0, Message = Newtonsoft.Json.JsonConvert.SerializeObject(table) };
                results.Add(entry);
            }
            return results;
        }
        [WebMethod]
        public static IList<ResultEntry> GetStat10Data2(int micYear, GradeClass gradeClass, int semester)
        {
            IList<ResultEntry> results = new List<ResultEntry>();
            using (AppBLL bll = new AppBLL())
            {
                DataTable table = bll.FillDataTable("s_p_classScore", new { DateYear = micYear, ClassCode = gradeClass.ClassNo, semester = semester, Flag = 0 });
                ResultEntry entry = new ResultEntry() { Code = 0, Message = Newtonsoft.Json.JsonConvert.SerializeObject(table) };
                results.Add(entry);
            }
            return results;
        }
        #endregion

        #region 教师教课情况报表（不分班，分班）
        //取当前年级
        [WebMethod]
        public static string GetCurrentGrade(int micyear, string teacherId)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "select top 1 scope AS GradeNo from s_tb_teacherscope where teacherid=@teacherId order by teachertype desc";
                DataTable dt = bll.FillDataTableByText(sql, new { teacherId = teacherId });
                if (dt.Rows.Count <= 0)
                {
                    sql = "select left(ClassID,2) as GradeNo from tbteacherclass" +
                          "where academicyear=@micyear" +
                          " and  teacherid=@teacherId " +
                          " group by left(ClassID,2)";
                    dt = bll.FillDataTableByText(sql, new { micyear = micyear, teacherId = teacherId });
                }
                return JsonConvert.SerializeObject(dt);
            }
        }
        //取得当前学年
        [WebMethod]
        public static IList<Academicyear> GetCurrentYear()
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "SELECT DISTINCT AcademicYear as MicYear FROM tbGradeClass ORDER BY AcademicYear";
                return bll.FillListByText<Academicyear>(sql, new { });
            }
        }
        //取当前课程
        [WebMethod]
        public static IList<GradeCourse> GeCurrentCourse(int micyear, int gradeNo)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "SELECT a.coursecode, b.FullName " +
                        "FROM  tbCourseUse a LEFT OUTER JOIN " +
                        "tdCourseCode b ON a.coursecode = b.CourseCode " +
                        "where a.Academicyear=@micyear and a.GradeNo=@gradeNo" +
                        " and b.IsDownLoad=1" +
                        " group by A.coursecode,b.FullName order by A.Coursecode";
                return bll.FillListByText<GradeCourse>(sql, new { micyear = micyear, gradeNo = gradeNo });
            }
        }

        //获取当前考试号
        [WebMethod]
        public static IList<TestLogin> GetCurrentTestNo(int? gradeNo, int micyear, int? courseCode)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "select * from s_tb_testLogin " +
                        "where academicyear=@micyear";
                if (gradeNo != null) sql += " and Gradeno in ('00'," + gradeNo + ")";
                if (courseCode != null) sql += " and Coursecode in (" + courseCode + ",'00000')";
                return bll.FillListByText<TestLogin>(sql, new { micyear = micyear });
            }
        }

        //获取教师情况统计表(不分班)
        [WebMethod]
        public static string GetTeacherAnalysisGrade(int micyear, GradeCode gradeNo, GradeCourse courseCode, int testNo, int? ck)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "SELECT  SUM(a.numscore)/COUNT(a.numscore) strGradeAverageScore ,STDEVP(a.numScore) strStdDev ," +
                            " COUNT(a.SRID) stdCount" +
                            " FROM s_tb_NormalScore a LEFT JOIN tbStudentBaseInfo b ON " +
                            " a.SRID=b.SRID LEFT JOIN tbStudentClass c ON " +
                            " a.AcademicYear=c.AcademicYear  AND a.SRID=c.SRID " +
                            " WHERE a.AcademicYear=@micyear  AND SUBSTRING(c.ClassCode,1,2)=@gradeNo " +
                            " AND a.CourseCode=@courseCode AND a.testno=@testNo";
                if (ck != null) sql += " and a.state is null";

                DataTable dt = bll.FillDataTableByText(sql,
                    new
                    {
                        micyear = micyear,
                        gradeNo = gradeNo.GradeNo,
                        courseCode = courseCode.CourseCode,
                        testNo = testNo
                    });

                var strCourseName = courseCode.FullName;//课程
                var strGradeName = gradeNo.GradeName;//年级
                var strTestNo = testNo;//考试号
                var strGradeAverageScore = dt.Rows[0]["strGradeAverageScore"];//年级平均分
                var strStdDev = dt.Rows[0]["strStdDev"];//标准差
                var strGradeNumCount = dt.Rows[0]["stdCount"];//考试人数                


                //查询考试时间
                sql = "SELECT TestTime as strTestTime FROM s_tb_TestLogin WHERE AcademicYear=@micyear AND TestNo=@testNo";
                dt = bll.FillDataTableByText(sql, new { micyear = micyear, testNo = testNo });
                var strTestTime = dt.Rows[0]["strTestTime"];//考试时间

                //老师，学生数，平均分
                sql = "SELECT ltrim(rtrim(d.Name)) TeacherName,count(b.ClassCode) ClassCode,round((SUM(a.numscore)/COUNT(*)),2) Score" +
                            " FROM s_tb_NormalScore a LEFT JOIN tbStudentClass b ON " +
                            " a.AcademicYear=b.AcademicYear " +
                            " AND a.SRID=b.SRID " +
                            " LEFT JOIN tbTeacherClass c ON " +
                            " a.AcademicYear=c.AcademicYear AND " +
                            " a.CourseCode=c.CourseCode  AND" +
                            " b.ClassCode=c.classID  " +
                            " LEFT JOIN tbUserGroupInfo d ON " +
                            " c.TeacherID=d.TeacherID " +
                            " WHERE a.AcademicYear=@micyear" +
                            " AND SUBSTRING(b.ClassCode,1,2)=@gradeNo" +
                            " AND a.CourseCode=@courseCode" +
                            " AND a.testno=@testNo" +
                            " GROUP BY d.Name" +
                            " ORDER BY Score desc ";
                dt = bll.FillDataTableByText(sql,
                    new
                    {
                        micyear = micyear,
                        gradeNo = gradeNo.GradeNo,
                        courseCode = courseCode.CourseCode,
                        testNo = testNo
                    });

                //班级数
                sql = "select TeacherName,COUNT(*) as GradeNum from (" +
                        " SELECT ltrim(rtrim(d.Name)) TeacherName,b.ClassCode" +
                        " FROM s_tb_NormalScore a LEFT JOIN tbStudentClass b ON " +
                        " a.AcademicYear=b.AcademicYear " +
                        " AND a.SRID=b.SRID " +
                        " LEFT JOIN tbTeacherClass c ON " +
                        " a.AcademicYear=c.AcademicYear AND " +
                        " a.CourseCode=c.CourseCode  AND" +
                        " b.ClassCode=c.classID  " +
                        " LEFT JOIN tbUserGroupInfo d ON " +
                        " c.TeacherID=d.TeacherID " +
                        " WHERE a.AcademicYear=@micyear" +
                        " AND SUBSTRING(b.ClassCode,1,2)=@gradeNo" +
                        " AND a.CourseCode=@courseCode" +
                        " AND a.testno=@testNo" +
                        " GROUP BY d.Name,b.ClassCode  ) t" +
                        " group by TeacherName";
                DataTable dtGradeNum = bll.FillDataTableByText(sql,
                    new
                    {
                        micyear = micyear,
                        gradeNo = gradeNo.GradeNo,
                        courseCode = courseCode.CourseCode,
                        testNo = testNo
                    });
                dt.Columns.Add("pkey");
                dt.Columns.Add("devScore");
                dt.Columns.Add("strCourseName");
                dt.Columns.Add("strGradeName");
                dt.Columns.Add("strTestNo");
                dt.Columns.Add("strGradeAverageScore");
                dt.Columns.Add("strStdDev");
                dt.Columns.Add("strGradeNumCount");
                dt.Columns.Add("strTestTime");
                dt.Columns.Add("Ranking");
                dt.Columns.Add("GradeNum");
                dt.Columns.Add("datetime");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    for (int n = 0; n < dtGradeNum.Rows.Count; n++)
                    {
                        if (dr["TeacherName"].ToString().Equals(dtGradeNum.Rows[n]["TeacherName"]))
                        {
                            dr["GradeNum"] = dtGradeNum.Rows[n]["GradeNum"];//班级数
                        }
                    }

                    dr["pkey"] = i + 1;
                    dr["devScore"] = Math.Round(((Convert.ToDouble(dr["Score"]) - Convert.ToDouble(strGradeAverageScore)) / Convert.ToDouble(strStdDev)), 2);
                    dr["devScore"] = Math.Round(((Convert.ToDouble(dr["Score"]) - Convert.ToDouble(strGradeAverageScore)) / Convert.ToDouble(strStdDev)), 2);
                    dr["strCourseName"] = strCourseName;
                    dr["strGradeName"] = strGradeName;
                    dr["strTestNo"] = strTestNo;
                    dr["strGradeAverageScore"] = Math.Round(Convert.ToDouble(strGradeAverageScore), 2);
                    dr["strStdDev"] = Math.Round(Convert.ToDouble(strStdDev), 2);
                    dr["strGradeNumCount"] = strGradeNumCount;
                    dr["strTestTime"] = strTestTime;
                    dr["Ranking"] = i + 1;
                    dr["datetime"] = DateTime.Now.ToString("yyyy-MM-dd");
                }
                return JsonConvert.SerializeObject(dt);
            }
        }

        //获取教师情况统计表(分班)
        [WebMethod]
        public static string GetTeacherAnalysisClass(int micyear, GradeCode gradeNo, GradeCourse courseCode, int testNo, int? ck)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "SELECT  SUM(a.numscore)/COUNT(a.numscore) strGradeAverageScore ,STDEVP(a.numScore) strStdDev ," +
                            " COUNT(a.SRID) stdCount" +
                            " FROM s_tb_NormalScore a LEFT JOIN tbStudentBaseInfo b ON " +
                            " a.SRID=b.SRID LEFT JOIN tbStudentClass c ON " +
                            " a.AcademicYear=c.AcademicYear  AND a.SRID=c.SRID " +
                            " WHERE a.AcademicYear=@micyear  AND SUBSTRING(c.ClassCode,1,2)=@gradeNo " +
                            " AND a.CourseCode=@courseCode AND a.testno=@testNo";
                if (ck != null) sql += " and a.state is null";

                DataTable dt = bll.FillDataTableByText(sql,
                    new
                    {
                        micyear = micyear,
                        gradeNo = gradeNo.GradeNo,
                        courseCode = courseCode.CourseCode,
                        testNo = testNo
                    });

                var strCourseName = courseCode.FullName;//课程
                var strGradeName = gradeNo.GradeName;//年级
                var strTestNo = testNo;//考试号
                var strGradeAverageScore = dt.Rows[0]["strGradeAverageScore"];//年级平均分
                var strStdDev = dt.Rows[0]["strStdDev"];//标准差
                var strGradeNumCount = dt.Rows[0]["stdCount"];//考试人数                


                //查询考试时间
                sql = "SELECT TestTime as strTestTime FROM s_tb_TestLogin WHERE AcademicYear=@micyear AND TestNo=@testNo";
                dt = bll.FillDataTableByText(sql, new { micyear = micyear, testNo = testNo });
                var strTestTime = dt.Rows[0]["strTestTime"];//考试时间

                //老师，学生数，平均分
                sql = "SELECT ltrim(rtrim(d.Name)) TeacherName,'('+substring(b.ClassCode,3,2)+')班' ClassCode," +
                            " COUNT(*) ClassCount,round((SUM(a.numscore)/COUNT(*)),2) Score" +
                            " FROM s_tb_NormalScore a LEFT JOIN tbStudentClass b ON " +
                            " a.AcademicYear=b.AcademicYear " +
                            " AND a.SRID=b.SRID " +
                            " LEFT JOIN tbTeacherClass c ON " +
                            " a.AcademicYear=c.AcademicYear AND " +
                            " a.CourseCode=c.CourseCode  AND" +
                            " b.ClassCode=c.classID  " +
                            " LEFT JOIN tbUserGroupInfo d ON " +
                            " c.TeacherID=d.TeacherID " +
                            " WHERE a.AcademicYear=@micyear" +
                            " AND SUBSTRING(b.ClassCode,1,2)=@gradeNo" +
                            " AND a.CourseCode=@courseCode" +
                            " AND a.testno=@testNo" +
                            " GROUP BY d.Name,b.ClassCode" +
                            " ORDER BY Score desc ";
                dt = bll.FillDataTableByText(sql,
                    new
                    {
                        micyear = micyear,
                        gradeNo = gradeNo.GradeNo,
                        courseCode = courseCode.CourseCode,
                        testNo = testNo
                    });
                dt.Columns.Add("pkey");
                dt.Columns.Add("devScore");
                dt.Columns.Add("strCourseName");
                dt.Columns.Add("strGradeName");
                dt.Columns.Add("strTestNo");
                dt.Columns.Add("strGradeAverageScore");
                dt.Columns.Add("strStdDev");
                dt.Columns.Add("strGradeNumCount");
                dt.Columns.Add("strTestTime");
                dt.Columns.Add("Ranking");
                dt.Columns.Add("GradeNum");
                dt.Columns.Add("datetime");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    dr["pkey"] = i + 1;
                    dr["devScore"] = Math.Round(((Convert.ToDouble(dr["Score"]) - Convert.ToDouble(strGradeAverageScore)) / Convert.ToDouble(strStdDev)), 2);
                    dr["strCourseName"] = strCourseName;
                    dr["strGradeName"] = strGradeName;
                    dr["strTestNo"] = strTestNo;
                    dr["strGradeAverageScore"] = Math.Round(Convert.ToDouble(strGradeAverageScore), 2);
                    dr["strStdDev"] = Math.Round(Convert.ToDouble(strStdDev), 2);
                    dr["strGradeNumCount"] = strGradeNumCount;
                    dr["strTestTime"] = strTestTime;
                    dr["Ranking"] = i + 1;
                    dr["datetime"] = DateTime.Now.ToString("yyyy-MM-dd");
                }
                return JsonConvert.SerializeObject(dt);
            }
        }
        #endregion

        #region 教师教学情况比较图表（历次）
        //根据课程，获得年级
        [WebMethod]
        public static IList<GradeCode> GetGradeCodeByGradeNo(Academicyear micyear, GradeCourse gradeCourse)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "SELECT b.* FROM dbo.s_vw_CourseUse a,tdGradeCode b" +
                          " WHERE a.gradeno=b.GradeNo AND a.coursecode=@gradeCourse AND a.AcademicYear=@micyear";
                return bll.FillListByText<GradeCode>(sql, new { micyear = micyear.MicYear, gradeCourse = gradeCourse.CourseCode });
            }
        }
        //根据学年，获取课程
        [WebMethod]
        public static IList<GradeCourse> GetGradeCourseByMicYear(Academicyear micyear)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "SELECT a.coursecode, b.FullName FROM  tbCourseUse a LEFT OUTER JOIN " +
                          "tdCourseCode b ON a.coursecode = b.CourseCode where a.Academicyear=2013";
                return bll.FillListByText<GradeCourse>(sql, new { micyear = micyear.MicYear });
            }
        }

        //获得教师
        [WebMethod]
        public static IList<UserGroupInfo> GetTeachers(Academicyear micyear, GradeCode gradeNo, GradeCourse courseCode)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "SELECT a.TeacherID, b.Name FROM  tbTeacherClass a INNER JOIN " +
                          "tbUserGroupInfo b ON a.TeacherID = b.TeacherID " +
                          " where substring(a.ClassID,1,2)=@gradeNo and a.Academicyear=@micyear" +
                          " and a.coursecode= @courseCode group by a.TeacherID, b.Name";
                return bll.FillListByText<UserGroupInfo>(sql,
                    new
                    {
                        micyear = micyear.MicYear,
                        gradeNo = gradeNo.GradeNo,
                        courseCode = courseCode.CourseCode
                    });
            }
        }
        [WebMethod]
        public static IList<ChartOption> GetTeacherStyle(Academicyear micyear, GradeCode gradeNo, GradeCourse courseCode, bool only, bool year, int numScore, IList<UserGroupInfo> teacher)
        {
            using (AppBLL bll = new AppBLL())
            {
                //计算截止学年
                var sql = "select top 1 gradeno from tdgradecode order by gradeno";
                DataTable dt = bll.FillDataTableByText(sql, new { });
                //截止学年
                var endYear = micyear.MicYear - Convert.ToInt32(gradeNo.GradeNo) + Convert.ToInt32(dt.Rows[0][0]);
                IList<ChartOption> options = new List<ChartOption>();

                ChartOption option1 = new ChartOption() { legend = new Legend(), xAxis = new XAxis() };
                options.Add(option1);
                option1.yAxis.name = courseCode.FullName + "各教师比较图";

                //循环老师
                int length = teacher.Count;
                for (int i = 0; i < length; i++)
                {


                    option1.legend.data.Add(teacher[i].Name);
                    SeriesItem item = new SeriesItem() { type = "bar", name = teacher[i].Name };
                    option1.series.Add(item);
                    if (year)//跨学年
                    {
                        sql = "SELECT  a.AcademicYear,a.testtype, e.TypeName, a.testno ";
                        if (numScore == 0) sql += " ,AVG(a.NumScore) AS AvgScore ";//原始分
                        else sql += " ,AVG(a.NormalScore) AS AvgScore ";//标准分
                        sql += " FROM tbTeacherClass c INNER JOIN "
                            + " tbStudentClass b ON c.AcademicYear = b.AcademicYear AND "
                            + " c.ClassID = b.ClassCode INNER JOIN "
                            + " s_tb_NormalScore a ON b.SRID = a.SRID INNER JOIN "
                            + " s_tb_TestTypeInfo e ON a.testtype = e.TestType LEFT OUTER JOIN "
                            + " s_tb_StudentXW d ON b.SRID = d.SRID "
                            + " WHERE (a.coursecode = " + courseCode.CourseCode + ") "
                            + " AND (c.TeacherID = " + teacher[i].TeacherID + ")"
                            + " and a.testtype<>0 ";
                        if (only) sql += " AND (d.STATE IS NULL) ";//仅在籍
                        sql += " and substring(a.srid,12,4)=" + endYear + " "
                            + " and c.academicyear=" + micyear.MicYear + " "
                            + " GROUP BY a.AcademicYear, a.semester, a.testtype, e.TypeName, a.testno "
                            + " ORDER BY a.AcademicYear , a.semester , a.testtype , CAST(a.testno AS int)  ";
                    }
                    else//不跨学年
                    {
                        sql = "Select Academicyear,TestType,TypeName,TestNo ";
                        if (numScore == 0) sql += " ,AVG(NumScore) AS AvgScore ";//原始分
                        else sql += " ,AVG(NormalScore) AS AvgScore ";//标准分
                        sql += " from s_vw_ClassScoreNum "
                        + " where Academicyear=" + micyear.MicYear + " "
                        + " and teacherid=" + teacher[i].TeacherID + " "
                        + " and CourseCode=" + courseCode.CourseCode + " ";
                        if (only) sql += " AND (STATE IS NULL) ";//仅在籍
                        sql += " group by Academicyear,testType,TypeName,testno "
                            + " order by Academicyear ,cast(testno as Int)  ";
                    }

                    dt = bll.FillDataTableByText(sql, new { });
                    for (int n = 0; n < dt.Rows.Count; n++)
                    {
                        var tempType = dt.Rows[n]["TypeName"].ToString().Substring(0, 2);
                        var temptestno = dt.Rows[n]["TestNo"].ToString();
                        var xName = string.Format("{0}{1}", tempType.Trim(), temptestno.Trim());
                        if (!option1.xAxis.data.Contains(xName))
                            option1.xAxis.data.Add(xName);
                        item.data.Add(dt.Rows.Count == 0 ? "0" : dt.Rows[n]["AvgScore"].ToString());
                    }

                }

                return options;
            }

        }

        [WebMethod]
        public static IList<ChartOption> GetTeacherStyle1(Academicyear micyear, GradeCode gradeNo, GradeCourse courseCode, int numScore, int starYear, int endYear, IList<UserGroupInfo> teacher)
        {
            using (AppBLL bll = new AppBLL())
            {
                IList<ChartOption> options = new List<ChartOption>();

                ChartOption option1 = new ChartOption() { legend = new Legend(), xAxis = new XAxis() };
                options.Add(option1);
                option1.yAxis.name = courseCode.FullName + "各教师比较图";

                //循环老师
                int length = teacher.Count;
                for (int i = 0; i < length; i++)
                {
                    option1.legend.data.Add(teacher[i].Name);
                    SeriesItem item = new SeriesItem() { type = "bar", name = teacher[i].Name };
                    option1.series.Add(item);
                    //获取期中，期末
                    var sql = "SELECT a.AcademicYear,a.Semester,a.Testtype,b.TypeName "
                           + " FROM s_tb_testlogin a INNER JOIN "
                           + " s_tb_TestTypeInfo b ON a.TestType = b.TestType "
                           + " where a.Academicyear>=" + starYear + " "
                           + " and a.Academicyear<=" + endYear + " "
                           + " and a.TestType<>0 "
                           + " group by a.AcademicYear,a.Semester,a.Testtype,b.TypeName "
                           + " order by a.AcademicYear,a.Semester,a.Testtype ";
                    DataTable dtAll = bll.FillDataTableByText(sql, new { });
                    if (dtAll.Rows.Count <= 0) continue;
                    for (int n = 0; n < dtAll.Rows.Count; n++)
                    {
                        //查找各老师的平均分
                        sql = "Select Academicyear,Semester,TestType,TypeName,TestNo";
                        if (numScore == 0) sql += " ,AVG(NumScore) AS AvgScore ";//原始分
                        else sql += " ,AVG(NormalScore) AS AvgScore ";//标准分
                        sql += " from s_vw_ClassScoreNum" +
                        " where Academicyear=" + Convert.ToInt32(dtAll.Rows[n]["AcademicYear"]) + "" +
                        " and teacherid=" + teacher[i].TeacherID + "" +
                        " and CourseCode=" + courseCode.CourseCode + "" +
                        " and semester=" + Convert.ToInt32(dtAll.Rows[n]["semester"]) + "" +
                        " and testtype=" + Convert.ToInt32(dtAll.Rows[n]["testtype"]) + "" +
                        " group by Academicyear,Semester,testType,TypeName,testno" +
                        " order by Academicyear ,semester,cast(testno as Int)";

                        DataTable dt = bll.FillDataTableByText(sql, new { });

                        var tempYear = dtAll.Rows[0]["Academicyear"].ToString();
                        var tempType = dtAll.Rows[0]["TypeName"].ToString().Substring(0, 2);
                        var temptestno = dtAll.Rows[0]["TestType"].ToString();
                        var xName = string.Format("{0}{1}{2}", tempYear.Trim(), tempType.Trim(), temptestno.Trim());
                        if (!option1.xAxis.data.Contains(xName))
                            option1.xAxis.data.Add(xName);
                        item.data.Add(dt.Rows.Count == 0 ? "0" : dt.Rows[0]["AvgScore"].ToString());
                    }
                }

                return options;
            }

        }

        #endregion

        #region 教师横向纵向比较图
        /// <summary>
        /// 考试类型
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static IList<TestLogin> GetTestNoByCourse(Academicyear micYear, int gradeCode)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = " select a.typename,b.testno,(SUBSTRING(a.typename,1,2)+b.testno) Name from " +
                            " s_tb_testtypeinfo as a inner join s_tb_testlogin as b" +
                            " on a.testtype=b.testtype " +
                            " where b.Academicyear=@micYear " +
                            " and GradeNo in (00,@gradeCode) " +
                            " order by cast(testno as int)";
                return bll.FillListByText<TestLogin>(sql, new { micYear = micYear.MicYear, gradeCode = gradeCode });
            }
        }
        #endregion

        #region 班级统计
        /// <summary>
        ///考试统计分析
        /// </summary>
        /// <param name="micYear"></param>
        /// <param name="testNo"></param>
        /// <param name="gradeCourse"></param>
        /// <param name="gradeClass"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetStat20Base(int micYear, TestLogin testNo, GradeCourse gradeCourse, GradeClass gradeClass, int scoreOption)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "";
                var tempSql = "";
                DataTable table = new DataTable();
                //参加考试人数
                sql = "Select count(*) as Renshu, round(Avg(Numscore), 4) as AvgScore,Max(Numscore) as MaxScore,Min(Numscore) as MinScore,round(stdevp(NumScore), 4) as Fangcha"
                 + " from s_vw_ClassScoreNum "
                 + " where Academicyear=@micYear"
                 + " and TestNo=@testNo"
                 + " and CourseCode=@courseCode"
                 + " and ClassCode=@classNo";
                if (scoreOption == 1)
                {
                    sql += "and state is null";
                }
                table = bll.FillDataTableByText(sql, new { micYear = micYear, testNo = testNo.TestLoginNo, courseCode = gradeCourse.CourseCode, classNo = gradeClass.ClassNo });
                string RenShu = table.Rows[0]["RenShu"].ToString();
                string AvgScore = table.Rows[0]["AvgScore"].ToString();
                string MaxScore = table.Rows[0]["MaxScore"].ToString();
                string MinScore = table.Rows[0]["MinScore"].ToString();
                string Fangcha = table.Rows[0]["Fangcha"].ToString();
                tempSql = string.Format("Select {0} as RenShu, {1} as AvgScore, {2} as MaxScore, {3} as MinScore, {4} as Fangcha",
                                            RenShu, AvgScore, MaxScore, MinScore, Fangcha);
                if (RenShu.Equals("0")) return "-1"; //查此班级无此课程成绩!

                //考试时间
                sql = " Select convert(varchar(19),TestTime, 25) as TestTime from s_tb_testlogin"
                       + " where Academicyear=@micYear"
                       + " and testno=@testNo";
                table = bll.FillDataTableByText(sql, new { micYear = micYear, testNo = testNo.TestLoginNo });
                string TestTime = table.Rows.Count == 0 ? "不详" : table.Rows[0]["TestTime"].ToString();
                tempSql += string.Format(",\'{0}\' as TestTime", TestTime);
                //任课教师
                sql = " Select a.teacherid,b.Name from tbTeacherClass as a,tbUsergroupinfo as b"
                       + " where a.teacherid=b.teacherid"
                       + " and a.Academicyear=@micYear"
                       + " and a.ClassID=@classCode"
                       + " and a.CourseCode=@courseCode";
                table = bll.FillDataTableByText(sql, new { micYear = micYear, courseCode = gradeCourse.CourseCode, classCode = gradeClass.ClassNo });
                string TeacherName = table.Rows.Count == 0 ? "不详" : table.Rows[0]["Name"].ToString();
                tempSql += string.Format(",\'{0}\' as TeacherName", TeacherName);

                //等第A至E的人数
                sql = " Select SUM(CASE WHEN LevelScore='A' THEN 1 ELSE 0 END) as ARenshu,"
                          + " SUM(CASE WHEN LevelScore='B' THEN 1 ELSE 0 END) as BRenshu,"
                          + " SUM(CASE WHEN LevelScore='C' THEN 1 ELSE 0 END) as CRenshu,"
                          + " SUM(CASE WHEN LevelScore='D' THEN 1 ELSE 0 END) as DRenshu,"
                          + " SUM(CASE WHEN LevelScore='E' THEN 1 ELSE 0 END) as ERenshu  from s_vw_ClassScoreNum "
                          + " where Academicyear=@micYear"
                          + " and TestNo=@testNo"
                          + " and CourseCode=@courseCode"
                          + " and ClassCode=@classCode";
                table = bll.FillDataTableByText(sql, new { micYear = micYear, courseCode = gradeCourse.CourseCode, classCode = gradeClass.ClassNo, testNo = testNo.TestLoginNo });
                string ARenShu = table.Rows[0]["ARenShu"].ToString();
                string BRenShu = table.Rows[0]["BRenShu"].ToString();
                string CRenShu = table.Rows[0]["CRenShu"].ToString();
                string DRenShu = table.Rows[0]["DRenShu"].ToString();
                string ERenShu = table.Rows[0]["ERenShu"].ToString();
                tempSql += string.Format(",{0} as ARenShu, {1} as BRenShu, {2} as CRenShu, {3} as DRenShu, {4} as ERenShu",
                                            ARenShu, BRenShu, CRenShu, DRenShu, ERenShu);
                table = bll.FillDataTableByText(tempSql, null);
                return Newtonsoft.Json.JsonConvert.SerializeObject(table);
            }
        }

        [WebMethod]
        public static IList<CommonOption> GetStat20Charts(int micYear, TestLogin testNo, GradeCode gradeCode, GradeCourse gradeCourse, GradeClass gradeClass, int scoreType, int scoreOption)
        {
            using (AppBLL bll = new AppBLL())
            {
                IList<CommonOption> options = new List<CommonOption>();
                ChartOption option1 = new ChartOption() { legend = new Legend(), xAxis = new XAxis() };
                PieOption option2 = new PieOption() { legend = new PieLegend() };
                ChartOption option3 = new ChartOption() { legend = new Legend(), xAxis = new XAxis() };
                options.Add(option1);
                options.Add(option2);
                options.Add(option3);

                string legend = string.Format("{0}({1})班{2}成绩正态图", gradeCode.GradeBriefName, gradeClass.ClassNo.Substring(2), gradeCourse.FullName);
                option1.title.text = legend;
                option1.title.x = "center";
                option1.legend.x = "left";
                option1.series.Add(new SeriesItem() { type = "line", name = legend });

                legend = string.Format("{0}({1})班{2}成绩分布饼图", gradeCode.GradeBriefName, gradeClass.ClassNo.Substring(2), gradeCourse.FullName);
                option2.title.text = legend;
                option2.title.x = "center";

                ((PieLegend)option2.legend).x = "left";
                ((PieLegend)option2.legend).orient = "vertical";
                PieItem pieItem = new PieItem();
                pieItem.type = "pie";
                pieItem.name = legend;
                pieItem.radius = "55%";
                pieItem.center.Add("50%");
                pieItem.center.Add("60%");
                option2.series.Add(pieItem);

                legend = string.Format("{0}({1})班与年级平均{2}成绩", gradeCode.GradeBriefName, gradeClass.ClassNo.Substring(2), gradeCourse.FullName);
                option3.title.text = legend;
                option3.title.x = "center";
                option3.legend.x = "left";
                option3.legend.data.Add("年级");
                option3.legend.data.Add("班级");
                option3.series.Add(new SeriesItem() { type = "line", name = "年级" });
                option3.series.Add(new SeriesItem() { type = "line", name = "班级" });

                var sql = "";
                DataTable table = new DataTable();
                //产生正太图
                sql = " SELECT c.TypeName, b.BriefName AS CourseName, a.TestNo,"
                  + " a.ClassNo, a.S_5+a.S_10+a.S_15+a.S_20+a.S_25+a.S_30+a.S_35+a.S_40 S_40,"
                  + " a.S_45+a.S_50 S_50,a.S_55,a.S_60,a.S_65,a.S_70,a.S_75,a.S_80,a.S_85,"
                  + " a.S_90,a.S_95,a.S_100 "
                  + " FROM  tdCourseCode b INNER JOIN "
                  + " s_tb_ClassStat a ON b.CourseCode = a.CourseCode INNER JOIN "
                  + "  s_tb_TestTypeInfo c ON a.TestType = c.TestType "
                  + " Where a.Academicyear=@micYear"
                  + " and a.CourseCode=@courseCode"
                  + " and a.TestNo=@testNo"
                  + " and a.ClassNo=@classNo";
                table = bll.FillDataTableByText(sql, new { micYear = micYear, testNo = testNo.TestLoginNo, courseCode = gradeCourse.CourseCode, classNo = gradeClass.ClassNo });
                if (table.Rows.Count == 0) return null; //无统计数据,请您确认是否做过统计图形!
                option1.xAxis.data.Add("0-0.4");
                option1.xAxis.data.Add("0.4-0.5");
                option1.xAxis.data.Add("0.5-0.55");
                option1.xAxis.data.Add("0.55-0.60");
                option1.xAxis.data.Add("0.6-0.55");
                option1.xAxis.data.Add("0.65-0.70");
                option1.xAxis.data.Add("0.7-0.75");
                option1.xAxis.data.Add("0.75-0.80");
                option1.xAxis.data.Add("0.8-0.85");
                option1.xAxis.data.Add("0.8-0.9");
                option1.xAxis.data.Add("0.9-0.95");
                option1.xAxis.data.Add("0.9-1");
                SeriesItem seriesItem = (SeriesItem)option1.series[0];
                seriesItem.data.Add(table.Rows[0]["S_40"].ToString());
                seriesItem.data.Add(table.Rows[0]["S_50"].ToString());
                seriesItem.data.Add(table.Rows[0]["S_55"].ToString());
                seriesItem.data.Add(table.Rows[0]["S_60"].ToString());
                seriesItem.data.Add(table.Rows[0]["S_65"].ToString());
                seriesItem.data.Add(table.Rows[0]["S_70"].ToString());
                seriesItem.data.Add(table.Rows[0]["S_75"].ToString());
                seriesItem.data.Add(table.Rows[0]["S_80"].ToString());
                seriesItem.data.Add(table.Rows[0]["S_85"].ToString());
                seriesItem.data.Add(table.Rows[0]["S_90"].ToString());
                seriesItem.data.Add(table.Rows[0]["S_95"].ToString());
                seriesItem.data.Add(table.Rows[0]["S_100"].ToString());

                //产生饼图
                sql = " SELECT c.TypeName, b.BriefName AS CourseName, a.TestNo,"
                 + " a.ClassNo, a.S_5+a.S_10+a.S_15+a.S_20+a.S_25+a.S_30+a.S_35+a.S_40 S_40,"
                 + " a.S_45+a.S_50+a.S_55+a.S_60 S_60,a.S_65+a.S_70+a.S_75 s_75,a.S_80+a.S_85 s_85, "
                 + " a.S_90+a.S_95+a.S_100 s_100 "
                 + " FROM  tdCourseCode b INNER JOIN "
                 + " s_tb_ClassStat a ON b.CourseCode = a.CourseCode INNER JOIN "
                 + " s_tb_TestTypeInfo c ON a.TestType = c.TestType"
                 + " Where a.Academicyear=@micYear"
                 + " and a.CourseCode=@courseCode"
                 + " and a.TestNo=@testNo"
                 + " and a.classno=@classNo";
                table = bll.FillDataTableByText(sql, new { micYear = micYear, testNo = testNo.TestLoginNo, courseCode = gradeCourse.CourseCode, classNo = gradeClass.ClassNo });
                if (table.Rows.Count == 0) return null; //您要的不能生成,请重新确定条件!
                if (int.Parse(table.Rows[0]["S_100"].ToString()) > 0)
                {
                    pieItem.data.Add(new PieDataItem() { name = "大于0.85", value = table.Rows[0]["S_100"].ToString() });
                    ((PieLegend)option2.legend).data.Add("大于0.85");
                }
                if (int.Parse(table.Rows[0]["S_85"].ToString()) > 0)
                {
                    pieItem.data.Add(new PieDataItem() { name = "0.75-0.85", value = table.Rows[0]["S_85"].ToString() });
                    ((PieLegend)option2.legend).data.Add("0.75-0.85");
                }
                if (int.Parse(table.Rows[0]["S_75"].ToString()) > 0)
                {
                    pieItem.data.Add(new PieDataItem() { name = "0.60-0.75", value = table.Rows[0]["S_75"].ToString() });
                    ((PieLegend)option2.legend).data.Add("0.60-0.75");
                }
                if (int.Parse(table.Rows[0]["S_60"].ToString()) > 0)
                {
                    pieItem.data.Add(new PieDataItem() { name = "0.45-0.65", value = table.Rows[0]["S_60"].ToString() });
                    ((PieLegend)option2.legend).data.Add("0.45-0.65");
                }
                if (int.Parse(table.Rows[0]["S_40"].ToString()) > 0)
                {
                    pieItem.data.Add(new PieDataItem() { name = "0-0.45", value = table.Rows[0]["S_40"].ToString() });
                    ((PieLegend)option2.legend).data.Add("0-0.45");
                }

                //历史曲线图
                //产生数据--年级
                if (scoreOption == 2) //ckyear.Checked
                {
                    sql = " SELECT a.AcademicYear, c.TypeName, a.testtype, a.testno,"
                      + " AVG({0}) AS AvgScore,d.testtime"
                      + " FROM dbo.s_tb_NormalScore a INNER JOIN"
                      + " tbStudentClass b ON a.SRID = b.SRID INNER JOIN"
                      + " s_tb_TestTypeInfo c ON a.testtype = c.TestType INNER JOIN"
                      + " s_tb_testlogin d ON a.AcademicYear = d.AcademicYear AND"
                      + " a.testno = d.TestNo"
                      + " WHERE (a.coursecode = @courseCode)"
                      + " AND (b.AcademicYear = @micYear)"
                      + " AND (a.testtype <> '0')"
                      + " and left(b.classcode,2)=@classNo"
                      + " and substring(a.srid,12,4)=@tempYear"
                      + " GROUP BY a.AcademicYear, a.semester, c.TypeName, a.testtype, a.testno,d.testtime"
                      + " ORDER BY a.AcademicYear , a.semester , a.testtype ,  cast(a.testno as int)";
                    sql = string.Format(sql, scoreType == 2 ? "a.NormalScore" : "a.NumScore");

                    var tempSql = "select top 1 GradeNo from tdGradeCode order by GradeNo";
                    DataTable tempTable = bll.FillDataTableByText(tempSql, null);
                    var minYear = int.Parse(tempTable.Rows[0][0].ToString());
                    var tempYear = micYear - int.Parse(gradeCode.GradeNo) + minYear;
                    table = bll.FillDataTableByText(sql, new { micYear = micYear, courseCode = gradeCourse.CourseCode, classNo = gradeClass.ClassNo, tempYear = tempYear });
                }
                else
                {
                    sql = " Select TestType,TypeName,TestNo,Avg({0}) as AvgScore,testtime "
                    + " from s_vw_ClassScoreNum "
                    + " where AcademicYear =@micYear"
                    + " and gradeno=@gradeNo"
                    + " and CourseCode =@courseCode"
                    + " group by TestNo,TestType,TypeName,testtime"
                    + " order by cast(testno as int)";
                    sql = string.Format(sql, scoreType == 2 ? "NormalScore" : "NumScore");
                    table = bll.FillDataTableByText(sql, new { micYear = micYear, courseCode = gradeCourse.CourseCode, gradeNo = gradeCode.GradeNo });
                }

                var length = table.Rows.Count;
                for (int i = 0; i < length; i++)
                {
                    var value = table.Rows[i]["AvgScore"].ToString();
                    value = string.IsNullOrEmpty(value) ? "0" : value;
                    var xTag = table.Rows[i]["TypeName"].ToString().Substring(0, 4);
                    xTag += table.Rows[i]["Testno"].ToString();
                    option3.xAxis.data.Add(xTag);
                    ((SeriesItem)option3.series[0]).data.Add(value);
                }

                //产生数据--班级
                if (scoreOption == 2) //ckyear.Checked
                {
                    sql = "SELECT a.AcademicYear, c.TypeName, a.testtype, a.testno,"
                       + " AVG(a.NormalScore) AS AvgScore"
                       + " FROM dbo.s_tb_NormalScore a INNER JOIN"
                       + " tbStudentClass b ON a.SRID = b.SRID INNER JOIN"
                       + " s_tb_TestTypeInfo c ON a.testtype = c.TestType"
                       + " WHERE (a.coursecode = @courseCode)"
                       + " AND (b.AcademicYear = @micYear)"
                       + " AND (a.testtype <> '0')"
                       + " AND b.ClassCode=@classNo"
                       + " and substring(a.srid,12,4)=@tempYear"
                       + " GROUP BY a.AcademicYear, a.semester, c.TypeName, a.testtype, a.testno"
                       + " ORDER BY a.AcademicYear , a.semester , a.testtype ,  cast(a.testno as int)";
                    sql = string.Format(sql, scoreType == 2 ? "a.NormalScore" : "a.NumScore");
                    var tempSql = "select top 1 GradeNo from tdGradeCode order by GradeNo";
                    DataTable tempTable = bll.FillDataTableByText(tempSql, null);
                    var minYear = int.Parse(tempTable.Rows[0][0].ToString());
                    var tempYear = micYear - int.Parse(gradeCode.GradeNo) + minYear;
                    table = bll.FillDataTableByText(sql, new { micYear = micYear, courseCode = gradeCourse.CourseCode, classNo = gradeClass.ClassNo, tempYear = tempYear });
                }
                else
                {
                    sql = "Select TestType,TypeName,TestNo,Avg({0}) as AvgScore,testtime"
                    + " from s_vw_ClassScoreNum"
                    + " where AcademicYear=@micYear"
                    + " and ClassCode=@classCode"
                    + " and CourseCode =@courseCode"
                    + " group by TestNo,TestType,TypeName,testtime"
                    + " order by cast(testno as int)";
                    sql = string.Format(sql, scoreType == 2 ? "NormalScore" : "NumScore");
                    table = bll.FillDataTableByText(sql, new { micYear = micYear, courseCode = gradeCourse.CourseCode, classCode = gradeClass.ClassNo });
                }

                length = table.Rows.Count;
                for (int i = 0; i < length; i++)
                {
                    var value = table.Rows[i]["AvgScore"].ToString();
                    value = string.IsNullOrEmpty(value) ? "0" : value;
                    var xTag = table.Rows[i]["TypeName"].ToString().Substring(0, 4);
                    xTag += table.Rows[i]["Testno"].ToString();
                    ((SeriesItem)option3.series[1]).data.Add(value);
                }
                return options;
            }
        }

        [WebMethod]
        public static string GetStat20GradeData1(int micYear, TestLogin testNo, GradeCode gradeCode, GradeCourse gradeCourse, GradeClass gradeClass)
        {
            using (AppBLL bll = new AppBLL())
            {
                var name = gradeCode.GradeBriefName + "年级";
                var sql = "SELECT Name, S_40, S_60, S_75, S_85, S_100, (S_40 + S_60 + S_75 + S_85 + S_100) S_Sum "
                 + " from (SELECT '" + name + "' as Name, a.S_5+a.S_10+a.S_15+a.S_20+a.S_25+a.S_30+a.S_35+a.S_40 S_40,"
                 + " a.S_45+a.S_50+a.S_55+a.S_60 S_60,a.S_65+a.S_70+a.S_75 S_75,a.S_80+a.S_85 S_85,"
                 + " a.S_90+a.S_95+a.S_100 S_100"
                 + " FROM  tdCourseCode b INNER JOIN"
                 + "  s_tb_Gradestat a ON b.CourseCode = a.CourseCode INNER JOIN"
                 + "  s_tb_TestTypeInfo c ON a.TestType = c.TestType"
                 + " Where a.Academicyear=@micYear"
                 + " and a.CourseCode=@courseCode"
                 + " and a.TestNo=@testNo"
                 + " and a.GradeNo=@gradeNo";
                name = string.Format("{0}({1})班", gradeCode.GradeBriefName, gradeClass.ClassNoPart);
                sql += " union all SELECT '" + name + "', a.S_5+a.S_10+a.S_15+a.S_20+a.S_25+a.S_30+a.S_35+a.S_40 S_40,"
                 + " a.S_45+a.S_50+a.S_55+a.S_60 S_60,a.S_65+a.S_70+a.S_75 s_75,a.S_80+a.S_85 s_85,"
                 + " a.S_90+a.S_95+a.S_100 s_100"
                 + " FROM  tdCourseCode b INNER JOIN"
                 + "  s_tb_ClassStat a ON b.CourseCode = a.CourseCode INNER JOIN"
                 + "  s_tb_TestTypeInfo c ON a.TestType = c.TestType"
                 + " Where a.Academicyear=@micYear"
                 + " and a.CourseCode=@courseCode"
                 + " and a.TestNo=@testNo"
                 + " and a.classno=@classNo) t";
                sql += " group by name,S_40, S_60, S_75, S_85, S_100";
                DataTable table = bll.FillDataTableByText(sql, new { micYear = micYear, courseCode = gradeCourse.CourseCode, gradeNo = gradeCode.GradeNo, testNo = testNo.TestLoginNo, classNo = gradeClass.ClassNo });

                return Newtonsoft.Json.JsonConvert.SerializeObject(table);
            }
        }


        [WebMethod]
        public static string GetStat20GradeData2(int micYear, TestLogin testNo, GradeCode gradeCode, GradeCourse gradeCourse, GradeClass gradeClass, int scoreType, int scoreOption)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "";
                DataTable table = new DataTable();
                //产生数据--年级
                if (scoreOption == 2) //ckyear.Checked
                {
                    sql = " SELECT a.AcademicYear, c.TypeName, a.testtype, a.testno,"
                      + " round(AVG({0}), 4) AS AvgScore,CONVERT(varchar(10), d.testtime, 25) testtime"
                      + " FROM dbo.s_tb_NormalScore a INNER JOIN"
                      + " tbStudentClass b ON a.SRID = b.SRID INNER JOIN"
                      + " s_tb_TestTypeInfo c ON a.testtype = c.TestType INNER JOIN"
                      + " s_tb_testlogin d ON a.AcademicYear = d.AcademicYear AND"
                      + " a.testno = d.TestNo"
                      + " WHERE (a.coursecode = @courseCode)"
                      + " AND (b.AcademicYear = @micYear)"
                      + " AND (a.testtype <> '0')"
                      + " and left(b.classcode,2)=@classNo"
                      + " and substring(a.srid,12,4)=@tempYear"
                      + " GROUP BY a.AcademicYear, a.semester, c.TypeName, a.testtype, a.testno,d.testtime"
                      + " ORDER BY a.AcademicYear , a.semester , a.testtype ,  cast(a.testno as int)";
                    sql = string.Format(sql, scoreType == 2 ? "a.NormalScore" : "a.NumScore");

                    var tempSql = "select top 1 GradeNo from tdGradeCode order by GradeNo";
                    DataTable tempTable = bll.FillDataTableByText(tempSql, null);
                    var minYear = int.Parse(tempTable.Rows[0][0].ToString());
                    var tempYear = micYear - int.Parse(gradeCode.GradeNo) + minYear;
                    table = bll.FillDataTableByText(sql, new { micYear = micYear, courseCode = gradeCourse.CourseCode, classNo = gradeClass.ClassNo, tempYear = tempYear });
                }
                else
                {
                    sql = " Select TestType,TypeName,TestNo,round(Avg({0}), 4) as AvgScore,CONVERT(varchar(10), testtime, 25) testtime "
                    + " from s_vw_ClassScoreNum "
                    + " where AcademicYear =@micYear"
                    + " and gradeno=@gradeNo"
                    + " and CourseCode =@courseCode"
                    + " group by TestNo,TestType,TypeName,testtime"
                    + " order by cast(testno as int)";
                    sql = string.Format(sql, scoreType == 2 ? "NormalScore" : "NumScore");
                    table = bll.FillDataTableByText(sql, new { micYear = micYear, courseCode = gradeCourse.CourseCode, gradeNo = gradeCode.GradeNo });
                }
                if (table.Rows.Count == 0) return "";
                var row1 = "";
                var row2 = "";
                var row4 = "";
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    row1 += string.Format("'{0}{1}' as S_{2},", table.Rows[i]["TypeName"].ToString().Substring(0, 2), table.Rows[i]["TestNo"].ToString(), i + 1);
                    row2 += string.Format("'{0}',", table.Rows[i]["testtime"].ToString());
                    row4 += string.Format("'{0}',", float.Parse(table.Rows[0]["AvgScore"].ToString()).ToString("f4"));
                }
                row1 = string.Format("SELECT '{0}' as S_0, {1}", "历次考试", row1.Substring(0, row1.Length - 1));
                row2 = string.Format(" union all SELECT '{0}' as S_0, {1}", " ", row2.Substring(0, row2.Length - 1));
                row4 = string.Format(" union all SELECT '{0}年级' as S_0, {1}", gradeCode.GradeBriefName, row4.Substring(0, row4.Length - 1));

                //产生数据--班级
                if (scoreOption == 2) //ckyear.Checked
                {
                    sql = "SELECT a.AcademicYear, c.TypeName, a.testtype, a.testno,"
                       + " round(AVG({0}), 4) AS AvgScore"
                       + " FROM dbo.s_tb_NormalScore a INNER JOIN"
                       + " tbStudentClass b ON a.SRID = b.SRID INNER JOIN"
                       + " s_tb_TestTypeInfo c ON a.testtype = c.TestType"
                       + " WHERE (a.coursecode = @courseCode)"
                       + " AND (b.AcademicYear = @micYear)"
                       + " AND (a.testtype <> '0')"
                       + " AND b.ClassCode=@classNo"
                       + " and substring(a.srid,12,4)=@tempYear"
                       + " GROUP BY a.AcademicYear, a.semester, c.TypeName, a.testtype, a.testno"
                       + " ORDER BY a.AcademicYear , a.semester , a.testtype ,  cast(a.testno as int)";
                    sql = string.Format(sql, scoreType == 2 ? "a.NormalScore" : "a.NumScore");
                    var tempSql = "select top 1 GradeNo from tdGradeCode order by GradeNo";
                    DataTable tempTable = bll.FillDataTableByText(tempSql, null);
                    var minYear = int.Parse(tempTable.Rows[0][0].ToString());
                    var tempYear = micYear - int.Parse(gradeCode.GradeNo) + minYear;
                    table = bll.FillDataTableByText(sql, new { micYear = micYear, courseCode = gradeCourse.CourseCode, classNo = gradeClass.ClassNo, tempYear = tempYear });
                }
                else
                {
                    sql = "Select TestType,TypeName,TestNo,round(Avg({0}), 4) as AvgScore,testtime"
                    + " from s_vw_ClassScoreNum"
                    + " where AcademicYear=@micYear"
                    + " and ClassCode=@classCode"
                    + " and CourseCode =@courseCode"
                    + " group by TestNo,TestType,TypeName,testtime"
                    + " order by cast(testno as int)";
                    sql = string.Format(sql, scoreType == 2 ? "NormalScore" : "NumScore");
                    table = bll.FillDataTableByText(sql, new { micYear = micYear, courseCode = gradeCourse.CourseCode, classCode = gradeClass.ClassNo });
                }

                var row3 = "";
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    row3 += string.Format("'{0}',", float.Parse(table.Rows[0]["AvgScore"].ToString()).ToString("f4"));
                }
                row3 = string.Format(" union all SELECT '{0}({1})班' as S_0, {2}", gradeCode.GradeBriefName, gradeClass.ClassNoPart, row3.Substring(0, row3.Length - 1));

                sql = row1 + row2 + row3 + row4;
                table = bll.FillDataTableByText(sql, null);
                return Newtonsoft.Json.JsonConvert.SerializeObject(table);
            }
        }
        #endregion
    }
}