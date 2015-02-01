using IES.CC.Test.Model;
using IES.G2S.CourseLive.DAL.Test;
using IES.G2S.CoursLive.IBLL.Test;
using IES.Resource.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.CourseLive.BLL.Test
{
    public class TestBLL : ITestBLL
    {
       /// <summary>
        /// 获取待批阅的作业列表（未评阅作业） 
       /// </summary>
       /// <param name="OCID"></param>
       /// <param name="UserID"></param>
       /// <returns></returns>
       public List<TestUser> TestUser_NotCheck_List(int OCID, int UserID) {
           return TestDAL.TestUser_NotCheck_List(OCID, UserID);
       }

        /// <summary>
        /// 作业列表
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="UserID"></param>
        /// <param name="OCID"></param>
        /// <param name="Type"></param>
        /// <param name="IsSend"></param>
        /// <param name="UpdateTime"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
       public TestInfo TestInfo_List(string Name, int UserID, int OCID, int Type, int IsSend, string UpdateTime, int PageSize, int PageIndex)
       {
           return TestDAL.TestInfo_List(Name,UserID, OCID, Type, IsSend, UpdateTime, PageSize, PageIndex);
       }



        /// <summary>
        /// 教学班学生列表
        /// </summary>
        /// <param name="TeachingClassID"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
       public List<IES.CC.OC.Model.OCClassStudent> TeachingClassStudent_List(int TeachingClassID, int PageIndex, int PageSize)
       {
           return TestDAL.TeachingClassStudent_List(TeachingClassID, PageIndex, PageSize);
       }

        /// <summary>
        /// 单个测试详细信息
        /// </summary>
        /// <param name="TestID"></param>
        /// <returns></returns>
       public CC.Test.Model.Test Test_Get(int TestID)
       {
           if (TestID == 0)
           {
               return new CC.Test.Model.Test();
           }
           else
           {
               return TestDAL.Test_Get(TestID);
           }
       }
        /// <summary>
        /// 添加或更新测试
        /// </summary>
        /// <param name="Test"></param>
        /// <returns></returns>
       public int Test_Add_Update(CC.Test.Model.Test Test)
       {
           if (Test.TestID ==0)
           { 
           return TestDAL.Test_Add(Test);
           }else
           {
               return TestDAL.Test_Update(Test);
           }
       }

        /// <summary>
        /// 添加测试对象
        /// </summary>
        /// <param name="TestID"></param>
        /// <param name="OCID"></param>
        /// <param name="IDS"></param>
       public void TestObject_Add(int TestID, int OCID, string IDS)
       {
           TestDAL.TestObject_Add(TestID, OCID, IDS);
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
       public int Paper_OfflineTest_Edit(int TestID, string PaperName, int OCID, int UserID, string content, string answer)
       {
           return TestDAL.Paper_OfflineTest_Edit(TestID, PaperName, OCID, UserID, content, answer);
       }
        /// <summary>
       ///  获取试卷的详细信息
        /// </summary>
        /// <param name="PaperID"></param>
        /// <returns></returns>
       public PaperInfo PaperInfo_Get(int PaperID) {
           return TestDAL.PaperInfo_Get(PaperID);
       }
       /// <summary>
       /// 获取作业的学生答案信息
       /// </summary>
       /// <param name="TestID"></param>
       /// <param name="UserID"></param>
       /// <param name="CheckUserID"></param>
       /// <returns></returns>
       public List<ExerciseAnswer> TestAnswer_Get(int TestID, int UserID, int CheckUserID) {
           return TestDAL.TestAnswer_Get(TestID, UserID, CheckUserID);
       }


       public void TestPaper_Edit(int TestID, int PaperID)
       {
           TestDAL.TestPaper_Edit(TestID, PaperID);
       }
    }
}
