/* **************************************************************
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
                var sql = "SELECT b.* FROM dbo.s_vw_CourseUse a,tdGradeCode b"+
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
                var sql = "SELECT a.TeacherID, b.Name FROM  tbTeacherClass a INNER JOIN "+
                          "tbUserGroupInfo b ON a.TeacherID = b.TeacherID "+
                          " where substring(a.ClassID,1,2)=@gradeNo and a.Academicyear=@micyear" +
                          " and a.coursecode= @courseCode group by a.TeacherID, b.Name";
                return bll.FillListByText<UserGroupInfo>(sql,
                    new { 
                        micyear = micyear.MicYear ,
                        gradeNo = gradeNo.GradeNo,
                        courseCode = courseCode.CourseCode
                    });
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

                legend = string.Format("{0}({1})班与年级平均{2}成绩历史曲线图", gradeCode.GradeBriefName, gradeClass.ClassNo.Substring(2), gradeCourse.FullName);
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
                    //if (table.Rows.Count == 0) return null; //无合适的数据！
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
                    //if (table.Rows.Count == 0) return null; //无合适的数据！
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
                    if (table.Rows.Count == 0) return null; //无合适的数据！
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
                    if (table.Rows.Count == 0) return null; //无合适的数据！
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
        #endregion
    }
}