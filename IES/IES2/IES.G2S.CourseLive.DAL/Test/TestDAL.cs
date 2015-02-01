using Dapper;
using IES.CC.OC.Model;
using IES.CC.Test.Model;
using IES.DataBase;
using IES.Resource.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.CourseLive.DAL.Test
{
   public class TestDAL
    {
        #region  列表
       /// <summary>
        /// 获取待批阅的作业列表（未评阅作业）
       /// </summary>
       /// <param name="OCID"></param>
       /// <param name="UserID"></param>
       /// <returns></returns>
       public static List<TestUser> TestUser_NotCheck_List(int OCID, int UserID)
       {
           using (var conn = DbHelper.CCService())
           {
               var p = new DynamicParameters();
               p.Add("@OCID", OCID);
               p.Add("@UserID", UserID);
               return conn.Query<TestUser>("TestUser_NotCheck_List", p, commandType: CommandType.StoredProcedure).ToList();
           } 
       }
       /// <summary>
       /// 获取作业列表
       /// </summary>
       /// <param name="UserID"></param>
       /// <param name="OCID"></param>
       /// <param name="Type"></param>
       /// <param name="IsSend"></param>
       /// <param name="UpdateTime"></param>
       /// <param name="PageSize"></param>
       /// <param name="PageIndex"></param>
       /// <returns></returns>
       public static IES.CC.Test.Model.TestInfo TestInfo_List(string Name, int UserID, int OCID, int Type, int IsSend, string UpdateTime, int PageSize, int PageIndex)
       {
           using (var conn = DbHelper.CCService())
           {  
               TestInfo ti = new TestInfo();
               var p = new DynamicParameters();
               p.Add("@Name", Name);
               p.Add("@UserID",UserID);
               p.Add("@OCID", OCID);
               p.Add("@Type", Type);
               p.Add("@IsSend", IsSend);
               p.Add("@UpdateTime", UpdateTime);
               p.Add("@PageSize", PageSize);
               p.Add("@OCID", PageIndex);
               //return conn.Query<IES.CC.Test.Model.TestInfo>("TestInfo_List", p, commandType: CommandType.StoredProcedure).ToList();
               var testinfo = conn.QueryMultiple("TestInfo_List", p, commandType: CommandType.StoredProcedure);
               ti.test = testinfo.Read<IES.CC.Test.Model.Test>().ToList();
               ti._teststatuslist = testinfo.Read<_TestStatus>().ToList();
               ti._testScoreStatisticslist = testinfo.Read<_TestScoreStatistics>().ToList();
               return ti;

           } 
       }

       /// <summary>
       /// 获取教学班学生
       /// </summary>
       /// <param name="TeachingClassID">教学班id</param>
       /// <param name="PageIndex"></param>
       /// <param name="PageSize"></param>
       /// <returns></returns>
       public static List<OCClassStudent> TeachingClassStudent_List(int TeachingClassID, int PageIndex, int PageSize)
       {
           try
           {
               using (var conn = DbHelper.CommonService())
               {
                   List<OCClassStudent> ul = new List<OCClassStudent>();
                   var p = new DynamicParameters();
                   p.Add("@TeachingClassID", TeachingClassID);
                   p.Add("@PageSize", PageSize);
                   p.Add("@PageIndex", PageIndex);
                   ul = conn.Query<OCClassStudent>("TeachingClassStudent_List", p, commandType: CommandType.StoredProcedure).ToList();
                   return ul;

               } 
           }
           catch 
           {
               return null;               
           }
       }

       /// <summary>
       /// 获取测试详细信息
       /// </summary>
       /// <param name="TestID"></param>
       /// <returns></returns>
       public static IES.CC.Test.Model.Test Test_Get(int TestID)
       {
           try
           {
               using (var conn = DbHelper.CCService())
               {
                   var p = new DynamicParameters();
                   p.Add("@TestID", TestID);
                   return conn.Query<IES.CC.Test.Model.Test>("Test_Get", p, commandType: CommandType.StoredProcedure).Single();
               } 
           }
           catch
           {
               return new CC.Test.Model.Test();
           }
       }


       //获取试卷的详细信息
       /// <summary>
       /// 获取试卷的详细信息
       /// </summary>
       /// <param name="PaperID"></param>
       /// <returns></returns>
       public static PaperInfo PaperInfo_Get(int PaperID)
       {
           using (var conn = DbHelper.ResourceService())
           {
               PaperInfo pi = new PaperInfo();
               var p = new DynamicParameters();
               p.Add("@PaperID", PaperID);
               var paperinfo = conn.QueryMultiple("PaperInfo_Get", p, commandType: CommandType.StoredProcedure);
               List<Paper> paper = paperinfo.Read<Paper>().ToList();
               if (paper != null && paper.Count > 0) {
                   pi.paper = paper[0];
               }
               pi.papergrouplist = paperinfo.Read<PaperGroup>().ToList();
               pi.attachmentlist = paperinfo.Read<Attachment>().ToList();
               pi.exerciselist = paperinfo.Read<PaperExercise>().ToList();
               pi.ExerciseChoices = paperinfo.Read<ExerciseChoice>().ToList();
               return pi;

           }                   
       }

       /// <summary>
       /// 获取作业的学生答案信息
       /// </summary>
       /// <param name="TestID"></param>
       /// <param name="UserID"></param>
       /// <param name="CheckUserID"></param>
       /// <returns></returns>
       public static List<ExerciseAnswer> TestAnswer_Get(int TestID, int UserID, int CheckUserID) {
           using (var conn = DbHelper.CCService())
           {
               var p = new DynamicParameters();
               p.Add("@TestID", TestID);
               p.Add("@UserID", UserID);
               p.Add("@CheckUserID", CheckUserID);
               return conn.Query<ExerciseAnswer>("TestAnswer_Get", p, commandType: CommandType.StoredProcedure).ToList();
           } 
       } 

      

        #endregion

        #region 新增
        public static int Test_Add(CC.Test.Model.Test Test)
       {
           try
           {
               using (var conn = DbHelper.CCService())
               {
                   var p = new DynamicParameters();
                   p.Add("@TestID", Test.TestID, DbType.Int32, ParameterDirection.InputOutput);
                   p.Add("@UserID", Test.UserID);
                   p.Add("@UserName",Test.UserName);
                   p.Add("@OCID",Test.OCID);
                   p.Add("@CourseID", Test.CourseID);
                   p.Add("@Name", Test.Name);
                   p.Add("@StartDate", Test.StartDate);
                   p.Add("@EndDate", Test.EndDate);
                   p.Add("@ChapterID", Test.ChapterID);
                   p.Add("@ChapterName", Test.ChapterName);
                   p.Add("@Type", Test.Type);
                   p.Add("@ScaleType", Test.ScaleType);
                   p.Add("@BuildMode", Test.BuildMode);
                   p.Add("@LessTimes", Test.LessTimes);
                   p.Add("@MoreTimes", Test.MoreTimes);
                   p.Add("@PassScore", Test.PassScore);
                   p.Add("@ScoreMode", Test.ScoreMode);
                   p.Add("@ScoreSource", Test.ScoreSource);
                   p.Add("@ShowResult", Test.ShowResult);
                   p.Add("@ShowExercise", Test.ShowExercise);
                   p.Add("@ExerciseShowMode", Test.ExerciseShowMode);
                   p.Add("@Delay", Test.Delay);
                   p.Add("@DelayScoreDiscount", Test.DelayScoreDiscount);
                   p.Add("@StudentCheckNum", Test.StudentCheckNum);
                   p.Add("@LostScoreDiscount", Test.LostScoreDiscount);
                   p.Add("@EndCheckTime", Test.EndCheckTime);

                   conn.Execute("Test_ADD", p, commandType: CommandType.StoredProcedure);
                   return p.Get<int>("TestID");
               } 
           }
           catch 
           {
               return 0;
           }
       }

       /// <summary>
       /// 测试对象新增
       /// </summary>
       /// <param name="TestID"></param>
       /// <param name="OCID"></param>
       /// <param name="IDS"></param>
        public static void TestObject_Add(int TestID,int OCID,string IDS)
        {
            try
            {
                using(var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@TestID", TestID);
                    p.Add("@OCID", OCID);
                    p.Add("@IDS", IDS);

                    conn.Execute("TestObject_Edit", p, commandType: CommandType.StoredProcedure);
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// 线下作业
        /// </summary>
        /// <param name="TestID">测试id</param>
        /// <param name="PaperName">试卷名称</param>
        /// <param name="OCID"></param>
        /// <param name="UserID">创建人id</param>
        /// <param name="content">内容</param>
        /// <param name="answer">答案</param>
        /// <returns></returns>
       public static int Paper_OfflineTest_Edit(int TestID,string PaperName, int OCID,int UserID,string content,string answer)
        {
            try
            {
                using (var conn = DbHelper.CommonService())
                {
                    var p = new DynamicParameters();
                    //p.Add("@PaperID");
                    p.Add("@PaperID", 0, DbType.Int32, ParameterDirection.InputOutput);
                    p.Add("@TestID", TestID);
                    p.Add("@Papername", PaperName);
                    p.Add("@OCID", OCID);
                    p.Add("@CreateUserID", UserID);
                    p.Add("@Conten", content);
                    p.Add("@Answer", answer);

                    conn.Execute("Paper_OfflineTest_Edit", p, commandType: CommandType.StoredProcedure);
                    return p.Get<int>("PaperID");
                }
            }
            catch
            {
                
                throw;
            }
        }

       /// <summary>
       /// 测试与试卷信息绑定
       /// </summary>
       /// <param name="Test"></param>
       /// <returns></returns>
       public static void TestPaper_Edit(int TestID,int PaperID)
       {
           try
           {
               using (var conn = DbHelper.CCService())
               {
                   var p = new DynamicParameters();
                   p.Add("@TestID", TestID);
                   p.Add("@PaperID", PaperID);

                   conn.Execute("TestPaper_Edit", p, commandType: CommandType.StoredProcedure);
               }
           }
           catch
           {

           }
       }
        #endregion

        #region 更新
        public static int Test_Update(CC.Test.Model.Test Test)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@TestID", Test.TestID, DbType.Int32, ParameterDirection.InputOutput);
                    p.Add("@UserID", Test.UserID);
                    p.Add("@UserName", Test.UserName);
                    p.Add("@OCID", Test.OCID);
                    p.Add("@CourseID", Test.CourseID);
                    p.Add("@Name", Test.Name);
                    p.Add("@StartDate", Test.StartDate);
                    p.Add("@EndDate", Test.EndDate);
                    p.Add("@ChapterID", Test.ChapterID);
                    p.Add("@ChapterName", Test.ChapterName);
                    p.Add("@Type", Test.Type);
                    p.Add("@ScaleType", Test.ScaleType);
                    p.Add("@BuildMode", Test.BuildMode);
                    p.Add("@LessTimes", Test.LessTimes);
                    p.Add("@MoreTimes", Test.MoreTimes);
                    p.Add("@PassScore", Test.PassScore);
                    p.Add("@ScoreMode", Test.ScoreMode);
                    p.Add("@ScoreSource", Test.ScoreSource);
                    p.Add("@ShowResult", Test.ShowResult);
                    p.Add("@ShowExercise", Test.ShowExercise);
                    p.Add("@ExerciseShowMode", Test.ExerciseShowMode);
                    p.Add("@Delay", Test.Delay);
                    p.Add("@DelayScoreDiscount", Test.DelayScoreDiscount);
                    p.Add("@StudentCheckNum", Test.StudentCheckNum);
                    p.Add("@LostScoreDiscount", Test.LostScoreDiscount);
                    p.Add("@EndCheckTime", Test.EndCheckTime);

                    conn.Execute("Test_Upd", p, commandType: CommandType.StoredProcedure);
                    return p.Get<int>("TestID");
                }
            }
            catch
            {
                return 0;
            }
        }
        #endregion
    }
}
