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
                    option1.series[0].data.Add(tempTable.Rows.Count == 0 ? "0" : tempTable.Rows[0]["classorder"].ToString());
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
                        option1.series[1].data.Add("0");
                    }
                    else
                    {
                        option1.series[1].data.Add(tempTable.Rows[0]["ClassOrder"].ToString());
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
                    option2.series[0].data.Add(tempTable.Rows.Count == 0 || string.IsNullOrEmpty(tempTable.Rows[0]["score"].ToString()) ? "0" : tempTable.Rows[0]["score"].ToString());

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
                    option2.series[2].data.Add(tempTable.Rows.Count == 0 || string.IsNullOrEmpty(tempTable.Rows[0]["score"].ToString()) ? "0" : tempTable.Rows[0]["score"].ToString());
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
                    option2.series[1].data.Add(tempTable.Rows.Count == 0 || string.IsNullOrEmpty(tempTable.Rows[0]["score"].ToString()) ? "0" : tempTable.Rows[0]["score"].ToString());

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
                    option3.series[0].data.Add(tempTable.Rows.Count == 0 || string.IsNullOrEmpty(tempTable.Rows[0]["score"].ToString()) ? "0" : tempTable.Rows[0]["score"].ToString());

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
                    option3.series[1].data.Add(tempTable.Rows.Count == 0 || string.IsNullOrEmpty(tempTable.Rows[0]["score"].ToString()) ? "0" : tempTable.Rows[0]["score"].ToString());

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
                    option3.series[2].data.Add(tempTable.Rows.Count == 0 || string.IsNullOrEmpty(tempTable.Rows[0]["score"].ToString()) ? "0" : tempTable.Rows[0]["score"].ToString());
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

<<<<<<< HEAD
        #region 教师教课情况报表（不分班）
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

        //获取教师情况统计表
        [WebMethod]
        public static string GetTeacherAnalysis(int micyear, GradeCode gradeNo, GradeCourse courseCode, int testNo, int? ck)
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
                dt = bll.FillDataTableByText(sql, new{micyear = micyear,testNo = testNo});
                var strTestTime = dt.Rows[0]["strTestTime"];//考试时间

                //老师，学生数，平均分
                sql = "SELECT ltrim(rtrim(d.Name)) TeacherName,count(b.ClassCode) ClassCode,(SUM(a.numscore)/COUNT(*)) Score" +
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
                            " GROUP BY d.Name"+
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
                        " SELECT ltrim(rtrim(d.Name)) TeacherName,b.ClassCode"+
                        " FROM s_tb_NormalScore a LEFT JOIN tbStudentClass b ON "+
                        " a.AcademicYear=b.AcademicYear "+
                        " AND a.SRID=b.SRID "+
                        " LEFT JOIN tbTeacherClass c ON "+
                        " a.AcademicYear=c.AcademicYear AND "+
                        " a.CourseCode=c.CourseCode  AND"+
                        " b.ClassCode=c.classID  "+
                        " LEFT JOIN tbUserGroupInfo d ON "+
                        " c.TeacherID=d.TeacherID "+
                        " WHERE a.AcademicYear=@micyear" +
                        " AND SUBSTRING(b.ClassCode,1,2)=@gradeNo" +
                        " AND a.CourseCode=@courseCode" +
                        " AND a.testno=@testNo" +
                        " GROUP BY d.Name,b.ClassCode  ) t"+
                        " group by TeacherName";
                DataTable dtGradeNum = bll.FillDataTableByText(sql,
                    new
                    {
                        micyear = micyear,
                        gradeNo = gradeNo.GradeNo,
                        courseCode = courseCode.CourseCode,
                        testNo = testNo
                    });
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
                            dr["GradeNum"] = dtGradeNum.Rows[n]["GradeNum"];
                        }
                    }
                    dr["devScore"] = Math.Round(((Convert.ToDouble(dr["Score"]) - Convert.ToDouble(strGradeAverageScore)) / Convert.ToDouble(strStdDev)),2);
                    dr["strCourseName"] = strCourseName;
                    dr["strGradeName"] = strGradeName;
                    dr["strTestNo"] = strTestNo;
                    dr["strGradeAverageScore"] = strGradeAverageScore;
                    dr["strStdDev"] = strStdDev;
                    dr["strGradeNumCount"] = strGradeNumCount;
                    dr["strTestTime"] = strTestTime;
                    dr["Ranking"] = i + 1;
                    dr["datetime"] = DateTime.Now.ToString("yyyy-MM-dd");   
                }
                return null;
            }
        }

=======
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
        public static string GetStat19Base(int micYear, TestLogin testNo, GradeCourse gradeCourse, GradeClass gradeClass, int scoreOption)
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
                          + " and ClassCode=@classNo";
                table = bll.FillDataTableByText(sql, new { micYear = micYear, courseCode = gradeCourse.CourseCode, classCode = gradeClass.ClassNo, testNo = testNo.TestLoginNo });
                string ARenShu = table.Rows[0]["ARenShu"].ToString();
                string BRenShu = table.Rows[0]["BRenShu"].ToString();
                string CRenShu = table.Rows[0]["CRenShu"].ToString();
                string DRenShu = table.Rows[0]["DRenShu"].ToString();
                string ERenShu = table.Rows[0]["ERenShu"].ToString();
                tempSql += string.Format("Select {0} as ARenShu, {1} as BRenShu, {2} as CRenShu, {3} as DRenShu, {4} as ERenShu",
                                            ARenShu, BRenShu, CRenShu, DRenShu, ERenShu);
                table = bll.FillDataTableByText(tempSql, null);
                return Newtonsoft.Json.JsonConvert.SerializeObject(table);
            }
        }

        //[WebMethod]
        //public static IList<ChartOption> GetStat19Charts(int micYear, TestLogin testNo, GradeCourse gradeCourse, GradeClass gradeClass, int scoreType, int scoreOption)
        //{
        //    using (AppBLL bll = new AppBLL())
        //    {
        //        IList<ChartOption> options = new List<ChartOption>();
        //        ChartOption option1 = new ChartOption() { legend = new Legend(), xAxis = new XAxis() };
        //        ChartOption option2 = new ChartOption() { legend = new Legend(), xAxis = new XAxis() };
        //        ChartOption option3 = new ChartOption() { legend = new Legend(), xAxis = new XAxis() };
        //        options.Add(option1);
        //        options.Add(option2);
        //        options.Add(option3);

        //        option1.legend.data.Add(student.StdName);
        //        option1.legend.data.Add("班级平均分");

        //        option1.series.Add(new SeriesItem() { type = "line", name = student.StdName });
        //        option1.series.Add(new SeriesItem() { type = "line", name = "班级平均分" });

        //        option2.legend.data.Add(student.StdName);
        //        option2.legend.data.Add("年级平均分");
        //        option2.legend.data.Add("班级平均分");

        //        option2.series.Add(new SeriesItem() { type = "line", name = student.StdName });
        //        option2.series.Add(new SeriesItem() { type = "line", name = "年级平均分" });
        //        option2.series.Add(new SeriesItem() { type = "line", name = "班级平均分" });

        //        option3.legend.data.Add(student.StdName);
        //        option3.legend.data.Add("年级总分平均");
        //        option3.legend.data.Add("班级总分平均");

        //        option3.series.Add(new SeriesItem() { type = "line", name = student.StdName });
        //        option3.series.Add(new SeriesItem() { type = "line", name = "年级总分平均" });
        //        option3.series.Add(new SeriesItem() { type = "line", name = "班级总分平均" });

        //        var sql = "";
        //        DataTable table = new DataTable();

        //        sql = "Select Academicyear,TypeName,TestNo,ClassCode from s_vw_ClassScoreNum"
        //         + " Where CourseCode=@courseCode"
        //         + " and SRID =@SRID"
        //         + " group by Academicyear,TypeName,TestNo,ClassCode"
        //         + " Order by Academicyear,Cast(TestNo as Int)";
        //        table = bll.FillDataTableByText(sql, new { micYear = micYear, testNo = testNo.TestLoginNo, courseCode = gradeCourse.CourseCode, SRID = student.StudentId });
        //        int length = table.Rows.Count;
        //        for (int i = 0; i < length; i++)
        //        {
        //            var tempType = table.Rows[i]["TypeName"].ToString().Substring(0, 2);
        //            var temptestno = table.Rows[i]["TestNo"].ToString();
        //            var tempyear = table.Rows[i]["Academicyear"].ToString();
        //            var tempClass = table.Rows[i]["ClassCode"].ToString();

        //            option1.xAxis.data.Add(string.Format("{0}{1}({2})", tempType.Trim(), temptestno.Trim(), tempyear));
        //            option2.xAxis.data.Add(string.Format("{0}{1}({2})", tempType.Trim(), temptestno.Trim(), tempyear));
        //            option3.xAxis.data.Add(string.Format("{0}{1}({2})", tempType.Trim(), temptestno.Trim(), tempyear));

        //            option1.yAxis.name = "名次";
        //            option2.yAxis.name = "学科成绩";
        //            option3.yAxis.name = "总分成绩";

        //            //加入学生的名次
        //            sql = "Select classorder from s_tb_normalscore "
        //                   + " where AcademicYear=@micYear"
        //                   + " and TestNo=@testNo"
        //                   + " and SRID=@SRID"
        //                   + " and CourseCode=@courseCode";
        //            DataTable tempTable = bll.FillDataTableByText(sql, new { micYear = tempyear, testNo = temptestno, courseCode = gradeCourse.CourseCode, SRID = student.StudentId });
        //            option1.series[0].data.Add(tempTable.Rows.Count == 0 ? "0" : tempTable.Rows[0]["classorder"].ToString());
                 
        //            //加入班级平均成绩的排名
        //            sql = " Select Avg(NumScore) as AvgScore from s_vw_ClassScoreNum"
        //                    + " Where CourseCode=@courseCode"
        //                    + " and ClassCode=@tempClass"
        //                    + " and Academicyear =@tempYear"
        //                    + " and TestNo=@tempTestNo";
        //            tempTable = bll.FillDataTableByText(sql, new
        //            {
        //                courseCode = gradeCourse.CourseCode,
        //                tempClass = tempClass,
        //                tempYear = tempyear,
        //                tempTestNo = temptestno
        //            });
        //            var tempClassAvg = Math.Round(float.Parse(tempTable.Rows[0]["AvgScore"].ToString()));

        //            sql = " Select * from s_vw_ClassScoreNum "
        //             + " where Numscore>=@NumScore"
        //             + " and ClassCode=@tempClass"
        //             + " and Academicyear=@tempYear"
        //             + " and TestNo=@temptestno"
        //             + " and CourseCode=@courseCode"
        //             + " order by Numscore";
        //            tempTable = bll.FillDataTableByText(sql, new
        //            {
        //                NumScore = tempClassAvg,
        //                courseCode = gradeCourse.CourseCode,
        //                tempClass = tempClass,
        //                tempYear = tempyear,
        //                temptestno = temptestno
        //            });
        //            if (tempTable.Rows.Count == 0)
        //            {
        //                option1.series[1].data.Add("0");
        //            }
        //            else
        //            {
        //                option1.series[1].data.Add(tempTable.Rows[0]["ClassOrder"].ToString());
        //            }
        //            //加学生
        //            sql = "Select typename,{0} as score from s_vw_ClassScoreNum"
        //                   + " where CourseCode=@courseCode"
        //                   + " and SRID=@SRID"
        //                   + " and Academicyear=@tempYear"
        //                   + " and TestNo=@temptestno";
        //            sql = string.Format(sql, scoreType == 1 ? "NumScore" : "NormalScore");

        //            tempTable = bll.FillDataTableByText(sql, new
        //            {
        //                courseCode = gradeCourse.CourseCode,
        //                SRID = student.StudentId,
        //                tempYear = tempyear,
        //                temptestno = temptestno
        //            });
        //            option2.series[0].data.Add(tempTable.Rows.Count == 0 || string.IsNullOrEmpty(tempTable.Rows[0]["score"].ToString()) ? "0" : tempTable.Rows[0]["score"].ToString());

        //            //根据条件查班级
        //            sql = "Select avg({0}) as score from s_vw_ClassScoreNum"
        //                  + " where CourseCode=@courseCode"
        //                  + " and ClassCode=@classCode"
        //                  + " and Academicyear=@tempYear"
        //                  + " and TestNo=@temptestno";
        //            sql = string.Format(sql, scoreType == 1 ? "NumScore" : "NormalScore");
        //            tempTable = bll.FillDataTableByText(sql, new
        //            {
        //                courseCode = gradeCourse.CourseCode,
        //                classCode = gradeClass.ClassNo,
        //                tempYear = tempyear,
        //                temptestno = temptestno
        //            });
        //            option2.series[2].data.Add(tempTable.Rows.Count == 0 || string.IsNullOrEmpty(tempTable.Rows[0]["score"].ToString()) ? "0" : tempTable.Rows[0]["score"].ToString());
        //            //年级
        //            sql = " Select Avg({0}) as score from s_vw_ClassScoreNum"
        //               + " where CourseCode=@courseCode"
        //               + " and GradeNo=@gradeNo"
        //               + " and Academicyear=@tempyear"
        //               + " and TestNo=@temptestno";
        //            sql = string.Format(sql, scoreType == 1 ? "NumScore" : "NormalScore");
        //            tempTable = bll.FillDataTableByText(sql, new
        //            {
        //                courseCode = gradeCourse.CourseCode,
        //                gradeNo = gradeClass.GradeNo,
        //                tempYear = tempyear,
        //                temptestno = temptestno
        //            });
        //            option2.series[1].data.Add(tempTable.Rows.Count == 0 || string.IsNullOrEmpty(tempTable.Rows[0]["score"].ToString()) ? "0" : tempTable.Rows[0]["score"].ToString());

        //            //加入总分图
        //            //先年级
        //            sql = " Select Avg({0}) as score from s_vw_SumScore"
        //            + " where GradeNo=@gradeNo"
        //            + " and Academicyear=@tempYear"
        //            + " and TestNo=@temptestno";

        //            sql = string.Format(sql, scoreType == 1 ? "sumScore" : "SumBZ");
        //            tempTable = bll.FillDataTableByText(sql, new
        //            {
        //                gradeNo = gradeClass.GradeNo,
        //                tempYear = tempyear,
        //                temptestno = temptestno
        //            });
        //            option3.series[0].data.Add(tempTable.Rows.Count == 0 || string.IsNullOrEmpty(tempTable.Rows[0]["score"].ToString()) ? "0" : tempTable.Rows[0]["score"].ToString());

        //            //根据条件查班级
        //            sql = " Select Avg({0}) as score from s_vw_SumScore"
        //                   + " where ClassCode=@classCode"
        //                   + " and Academicyear=@tempyear"
        //                   + " and TestNo=@temptestno";
        //            sql = string.Format(sql, scoreType == 1 ? "sumScore" : "SumBZ");
        //            tempTable = bll.FillDataTableByText(sql, new
        //            {
        //                classCode = gradeClass.ClassNo,
        //                tempYear = tempyear,
        //                temptestno = temptestno
        //            });
        //            option3.series[1].data.Add(tempTable.Rows.Count == 0 || string.IsNullOrEmpty(tempTable.Rows[0]["score"].ToString()) ? "0" : tempTable.Rows[0]["score"].ToString());

        //            //加学生
        //            sql = " Select {0} as score from s_vw_SumScore"
        //                   + " where SRID=@SRID"
        //                   + " and Academicyear=@tempYear"
        //                   + " and TestNo=@temptestno";
        //            sql = string.Format(sql, scoreType == 1 ? "sumScore" : "SumBZ");
        //            tempTable = bll.FillDataTableByText(sql, new
        //            {
        //                SRID = student.StudentId,
        //                tempYear = tempyear,
        //                temptestno = temptestno
        //            });
        //            option3.series[2].data.Add(tempTable.Rows.Count == 0 || string.IsNullOrEmpty(tempTable.Rows[0]["score"].ToString()) ? "0" : tempTable.Rows[0]["score"].ToString());
        //        }
        //        return options;
        //    }
        //}

        [WebMethod]
        public static string GetStat19Data(int micYear, TestLogin testNo, GradeCourse gradeCourse, GradeClass gradeClass, int scoreOption)
        {
            return "";
        }
>>>>>>> a2442250586f15f7fda1a9d0e699361bec6384ca
        #endregion
    }
}