using IES.CC.Model.Test;
using IES.CC.OC.Model;
using IES.CC.Test.Model;
using IES.G2S.CourseLive.BLL.Test;
using IES.G2S.CoursLive.IBLL.Test;
using IES.G2S.OC.BLL.OC;
using IES.G2S.OC.IBLL.OC;
using IES.G2S.Resource.BLL;
using IES.G2S.Resource.IBLL;
using IES.JW.Model;
using IES.Resource.Model;
using IES.Security;
using IES.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App.AngularMvc.DataProvider.CourseLive.Test
{
    public partial class TestProvider : System.Web.UI.Page
    {
        /// <summary>
        /// 获取待批阅的作业列表（未评阅作业） 
        /// </summary>
        /// <param name="OCID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [WebMethod]
        public static  List<TestUser> TestUser_NotCheck_List(int OCID)
        {
            string userid = IESCookie.GetCookieValue("ies");
            IES.JW.Model.User user = new IES.JW.Model.User { UserID = Int32.Parse(userid) };
            user = UserService.User_Get(user);
            ITestBLL testbll = new TestBLL();
            return testbll.TestUser_NotCheck_List(OCID, user.UserID);
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
        [WebMethod]
        public static List<TestList_Test> TestInfo_List(string Name, int OCID, int Type, int IsSend, string UpdateTime, int PageSize, int PageIndex)
        {
        
            string userid = IESCookie.GetCookieValue("ies");
            IES.JW.Model.User user = new IES.JW.Model.User { UserID = Int32.Parse(userid) };
            user = UserService.User_Get(user);
            ITestBLL testbll = new TestBLL();
            TestInfo testinfo = testbll.TestInfo_List(Name,user.UserID, OCID, Type, IsSend, UpdateTime, PageSize, PageIndex);
            List<TestList_Test> tl = new List<TestList_Test>();
            for (int i = 0; i < testinfo.test.Count; i++)
            {
                TestList_Test testlist = new TestList_Test();
                testlist.test=testinfo.test[i];
                testlist.MarkingCount = GetStatusByCount(testinfo._teststatuslist, testinfo.test[i].TestID,23,0);  //已批阅数量
                testlist.submittedCount = GetStatusByCount(testinfo._teststatuslist, testinfo.test[i].TestID, 20, 0);//已提交数量
                testlist.MarkingPercent = (int)(Convert.ToDouble(Convert.ToDouble(testlist.MarkingCount) / Convert.ToDouble(testlist.submittedCount))*1000)/10.0;
                testlist.UncommittedCount = GetStatusByCount(testinfo._teststatuslist, testinfo.test[i].TestID, 1, 0) + GetStatusByCount(testinfo._teststatuslist, testinfo.test[i].TestID, 10, 0);//未提交+暂存
                testlist.DelayCount = GetStatusByCount(testinfo._teststatuslist, testinfo.test[i].TestID, 0, 1); //迟交
                testlist.DSocreSection = ListToDictionary(testinfo._testScoreStatisticslist, testinfo.test[i].TestID);
                
                tl.Add(testlist);
            }
            return tl;

             //TestList_Test  新增model 用户列表

        }
        /// <summary>
        /// 获取提交状态的数量
        /// </summary>
        /// <param name="teststatus"></param>
        /// <param name="TestID"></param>
        /// <param name="Status"></param>
        /// <param name="IsDelay"></param>
        /// <returns></returns>
        public static int GetStatusByCount(List<_TestStatus> teststatus, int TestID, int Status, int IsDelay)
        {
            int flag = 0;
            for (int i = 0; i < teststatus.Count; i++)
            {
                if (teststatus[i].TestID == TestID) {
                    if (teststatus[i].Status == Status) {
                        return flag = teststatus[i].UserNum;
                    }
                    else if (teststatus[i].IsDelay == 1 && IsDelay==1)
                    {
                        return flag = teststatus[i].UserNum;
                    }      
                }
            }
            return flag; 
        }
        /// <summary>
        /// 把数据转换为Dictionary<key,value>
        /// </summary>
        /// <param name="testscore"></param>
        /// <param name="TestID"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ListToDictionary(List<_TestScoreStatistics> testscore, int TestID)
        {
            Dictionary<string, string> dSocreSection = new Dictionary<string, string>();
            for (int i = 0; i < testscore.Count; i++)
            {
                if (testscore[i].TestID == TestID)
                {
                    dSocreSection.Add(testscore[i].SocreSection, testscore[i].UserNum);
                } 
            }
            return dSocreSection;
        }

        /// <summary>
        /// 教学班列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<TeachingClass> TeachingClass_Owner_List(int OCID)
        {
            List<TeachingClass> teachingClassList = new List<TeachingClass>();
            teachingClassList = IES.Service.UserService.TeachingClass_Owner_List(OCID);
            return teachingClassList;
        }

        /// <summary>
        /// 教学班学生信息
        /// </summary>
        /// <param name="OCClass"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<OCClassStudent> Student_List(string TeachingClassIDs)
        {
            IES.CC.OC.Model.OCClass model = new IES.CC.OC.Model.OCClass();
            List<OCClassStudent> userList = new List<OCClassStudent>();
            string[] TeachingClassIDList = TeachingClassIDs.Split(',');
            ITestBLL bll = new TestBLL();
            foreach (string item in TeachingClassIDList)
            {
                if (item != "" && item != null)
                {
                    userList.AddRange(bll.TeachingClassStudent_List(Convert.ToInt32(item), 1, 100000));
                }
            }
            return userList;
        }

        [WebMethod]
        public static IES.CC.Test.Model.Test Test_Get(int TestID)
        {
            ITestBLL bll = new TestBLL();
            return bll.Test_Get(TestID);
        }

        /// <summary>
        /// 成绩类型
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<Dict> TestScaleType_Get()
        {
            List<Dict> DictList = IES.Service.CommonData.CCCommonData.Dict_TestScaleType_Get().Where(x => x.ParentID==0).ToList<Dict>();
            return DictList;
        }

        /// <summary>
        /// 添加测试
        /// </summary>
        /// <param name="Test"></param>
        /// <param name="OCID"></param>
        /// <param name="IDS"></param>
        /// <returns></returns>
        [WebMethod]
        public static int Test_Add_Update(IES.CC.Test.Model.Test Test, PaperDefineInfo PaperDefineInfo, int OCID, int CourseID, string IDS, string content, string answer)
        {
            
            IES.JW.Model.User user = IES.Service.UserService.CurrentUser;
            Test.UserID = user.UserID;
            Test.UserName = user.UserName;
            Test.ChapterID = 0;  //章节id  
            Test.Type = 1;  //测试类型  1. 作业、  2考试 、3 达标训练   、4录入成绩（仅在成绩管理模块中添加录入成绩用）  
            Test.LessTimes = 1;  //最少参与次数  给默认值
            Test.MoreTimes = 1; //最多参与次数   给默认值
            Test.PassScore = 60; //通过分数   给默认值
            Test.ScoreMode = 1; //1 最高分 ; 2 平均分   给默认值
            Test.ShowResult = false; //是否立即显示答案 给默认值
            Test.ShowExercise = false; //成绩发放后是否显示试卷详细信息  给默认值
            Test.ExerciseShowMode = 0; //0 不随机；  1习题随机   ； 2选项随机 ； 3习题和选项都随机    给默认值


            ITestBLL bll = new TestBLL();
            int TestID = bll.Test_Add_Update(Test);
            //添加测试对象
            TestObject_Add(TestID, OCID, IDS);
            if (Test.BuildMode == 4)
            {
                string PaperName = "";
                //线下作业内容
                Paper_OfflineTest_Edit(TestID, PaperName, OCID, user.UserID, content, answer);
            }
            if (Test.BuildMode == 2)
            {
                //题库选题 选中内容添加
                int PaperID = Paper_ADD(OCID, CourseID, Test.Name, 0);
                TestPaper_Edit(TestID, PaperID);
                foreach (PaperGroup item in PaperDefineInfo.papergrouplist)
                {
                    if (item.GroupID < 1)
                    {
                        int groupid = PaperGroup_ADD(PaperID, item.GroupName, item.Orde, item.Timelimit);
                        foreach (PaperExercise item1 in PaperDefineInfo.exerciselist)
                        {
                            if (item1.PaperGroupID == item.GroupID)
                            {
                                PaperExercise_ADD(PaperID, groupid, item1.ExerciseID, 0, 0);
                            }
                        }

                        foreach (PaperTactic item2 in PaperDefineInfo.papertacticlist)
                        {
                            if (item2.GroupID == item.GroupID)
                            {
                                if (item2.PaperTacticID > 0 || Convert.ToInt32(item2.Num) > 0)
                                {
                                    PaperTactic pt = item2;
                                    pt.GroupID = groupid;
                                    pt.PaperID = PaperID;
                                    PaperTactic_Edit(pt);
                                }
                            }
                        }
                    }
                    else
                    {
                        //更新
                    }
                }

                //int GroupID = PaperGroup_ADD(PaperID,"",)
            }
            return TestID;
        }

        /// <summary>
        /// 添加测试对象
        /// </summary>
        /// <param name="TestID"></param>
        /// <param name="OCID"></param>
        /// <param name="IDS"></param>
        [WebMethod]
        public static void TestObject_Add(int TestID,int OCID,string IDS)
        {
            ITestBLL bll = new TestBLL();
            bll.TestObject_Add(TestID, OCID, IDS);
        }

        /// <summary>
        /// 线下作业内容
        /// </summary>
        /// <param name="TestID"></param>
        /// <param name="PaperName"></param>
        /// <param name="OCID"></param>
        /// <param name="UserID"></param>
        /// <param name="content"></param>
        /// <param name="answer"></param>
        [WebMethod]
        public static void Paper_OfflineTest_Edit(int TestID, string PaperName, int OCID, int UserID, string content, string answer)
        {
            ITestBLL bll = new TestBLL();
            bll.Paper_OfflineTest_Edit(TestID, PaperName, OCID, UserID, content, answer);
        }


        /// <summary>
        /// 题型
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<ResourceDict> Resource_Dict_ExerciseType_List()
        {
            List<ResourceDict> list = IES.Common.Data.ResourceCommonData.Resource_Dict_ExerciseType_Get();
            return list;
        }

        /// <summary>
        ///难度系数
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<ResourceDict> Resource_Dict_Diffcult_List()
        {
            List<ResourceDict> list = IES.Common.Data.ResourceCommonData.Resource_Dict_Diffcult_Get();
            return list;
        }

        /// <summary>
        /// 试卷类型列表
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<ResourceDict> Resource_Dict_PaperType_List()
        {
            List<ResourceDict> list = IES.Common.Data.ResourceCommonData.Resource_Dict_PaperType_Get();
            return list;
        }

        /// <summary>
        /// 习题列表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="key"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<Exercise> Exercise_Search(int PageSize, int PageIndex, string Searchkey, int OCID, int CourseID, int EXType, int Diffcult,int keyID)
        {
            IES.JW.Model.User user = IES.Service.UserService.CurrentUser;
            Exercise model = new Exercise();
            Key key = new Key();
            model.Conten = Searchkey;
            model.OCID = OCID;
            model.CourseID = CourseID;
            model.CreateUserID = user.UserID;
            model.ExerciseType = EXType;
            model.Diffcult = Diffcult;
            model.Scope = 1;
            model.ShareRange = 0;
            key.KeyID = keyID;
            IExerciseBLL bll = new ExerciseBLL();
            List<Exercise> list = bll.Exercise_Search(model,key,"", "",PageSize,PageIndex);
            return list;
        }
        
        /// <summary>
        /// 试卷列表
        /// </summary>
        /// <param name="Searchkey"></param>
        /// <param name="OCID"></param>
        /// <param name="CourseID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<Paper> Paper_Search(string Searchkey,int OCID,int CourseID,int Type)
        {
            IES.JW.Model.User user = IES.Service.UserService.CurrentUser;
            IPaperBLL bll = new PaperBLL();
            List<Paper> list = bll.Paper_Search(Searchkey, OCID, Type, -1, new DateTime(2010,1,1), -1,user.UserID, 100000, 1);
            return list;
        }

        /// <summary>
        /// 知识点列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<Ken> Ken_List(int OCID)
        {
            IES.JW.Model.User user = IES.Service.UserService.CurrentUser;
            IKenBLL bll = new KenBLL();
            List<Ken> list = bll.ExerciseOrFile_Ken_List("", "Exercise", user.UserID, 10000, OCID);

            return list.Distinct(new ken_collection()).ToList(); 
        }

        /// <summary>
        /// 标签列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<Key> Key_List(int OCID)
        {
            IES.JW.Model.User user = IES.Service.UserService.CurrentUser;
            IKeyBLL bll = new KeyBLL();
            List<Key> list = bll.ExerciseOrFile_Key_List("", "Exercise", user.UserID, 10000,OCID);

            return list.Distinct(new key_collection()).ToList();
        }
        

        /// <summary>
        /// 试卷的详细结构信息
        /// </summary>
        /// <param name="PaperID"></param>
        /// <returns></returns>
        [WebMethod]
        public static PaperDefineInfo PaperDefineInfo_Get(int PaperID)
        {
            PaperDefineInfo PaperDefineInfo = new PaperDefineInfo();
            //新增时返回一个空对象
            if(PaperID == 0)
            {
                List<PaperGroup> list = new List<PaperGroup>();
                PaperGroup paperGroup = new PaperGroup();
                paperGroup.GroupID = 0;
                paperGroup.GroupName = "第一部分";
                paperGroup.Orde = 1;
                list.Add(paperGroup);

                

                PaperDefineInfo.papergrouplist = list;
                return PaperDefineInfo;
            }else
            {
                return PaperDefineInfo;//后面写更新的
            }
        }

        /// <summary>
        /// 试卷新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Paper_ADD(int OCID,int CourseID, string PaperName,int Timelimit)
        {
            IES.JW.Model.User user = IES.Service.UserService.CurrentUser;
            Paper paper = new Paper();
            
            paper.PaperID = 0;
            paper.OCID = OCID;
            paper.OwnerUserID = 0;
            paper.CourseID = CourseID;
            paper.CreateUserID = user.UserID;
            paper.Papername = PaperName;
            paper.Type = 1;
            paper.Scope = 1;
            paper.ShareScope = 0;
            paper.TimeLimit = Timelimit;
            paper.Brief = "";
            paper.Conten = "";
            paper.Answer = "";
            IPaperBLL bll = new PaperBLL();
            int id = bll.Paper_ADD(paper);
            return id;
        }

        /// <summary>
        /// 新增试卷分组
        /// </summary>
        /// <param name="PaperID"></param>
        /// <param name="GroupName"></param>
        /// <param name="Order"></param>
        /// <param name="Timelimit"></param>
        /// <returns></returns>
        public static int PaperGroup_ADD(int PaperID,string GroupName,int Order,int Timelimit)
        {
            PaperGroup pg = new PaperGroup();
            pg.GroupID = 0;
            pg.PaperID = PaperID;
            pg.GroupName = GroupName;
            pg.Orde = Order;
            pg.Brief = "";
            pg.Timelimit = Timelimit;
            IPaperBLL bll = new PaperBLL();
            int id = bll.PaperGroup_ADD(pg);
            return id;
        }

        /// <summary>
        /// 知识点列题
        /// </summary>
        /// <param name="OCID"></param>
        /// <param name="ExerciseType"></param>
        /// <param name="Diffcult"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<Ken> Ken_ExerciseCount_List(int OCID, int ExerciseType, int Diffcult)
        {
            IES.JW.Model.User user = IES.Service.UserService.CurrentUser;
            List<Ken> list = new List<Ken>();
            IKenBLL bll = new KenBLL();
            list = bll.Ken_ExerciseCount_List(OCID, user.UserID, ExerciseType, Diffcult);
            return list;
        }

        /// <summary>
        /// 章节列题
        /// </summary>
        /// <param name="OCID"></param>
        /// <param name="ExerciseType"></param>
        /// <param name="Diffcult"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<Chapter> Chapter_ExerciseCount_List(int OCID, int ExerciseType, int Diffcult)
        {
            IES.JW.Model.User user = IES.Service.UserService.CurrentUser;
            List<Chapter> list = new List<Chapter>();
            IChapterBLL bll = new ChapterBLL();
            list = bll.Chapter_ExerciseCount_List(OCID, user.UserID, ExerciseType, Diffcult);
            return list;
        }

        /// <summary>
        /// 试卷试题添加
        /// </summary>
        /// <param name="PaperID"></param>
        /// <param name="PaperGroupID"></param>
        /// <param name="ExerciseID"></param>
        /// <param name="Score"></param>
        /// <param name="Order"></param>
        /// <returns></returns>
        public static bool PaperExercise_ADD(int PaperID,int PaperGroupID,int ExerciseID,int Score,int Order)
        {
            IPaperBLL bll = new PaperBLL();
            bool bl = bll.PaperExercise_ADD(PaperID, PaperGroupID, ExerciseID, Score, Order);
            return bl;
        }

        /// <summary>
        /// 测试添加到试卷
        /// </summary>
        /// <param name="TestID"></param>
        /// <param name="PaperID"></param>
        public static void TestPaper_Edit(int TestID,int PaperID)
        {
            ITestBLL testBll = new TestBLL();
            testBll.TestPaper_Edit(TestID, PaperID);
        }

        public static bool PaperTactic_Edit(PaperTactic model)
        {
            IPaperBLL bll = new PaperBLL();
            return bll.PaperTactic_Edit(model);
        }

    }



    /// <summary>
    /// 知识点list去重复
    /// </summary>
    class ken_collection : IEqualityComparer<Ken>
    {

        public bool Equals(Ken x, Ken y)
        {
            if (x.KenID == y.KenID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode(Ken obj)
        {
            return 0;
        }
    }
    /// <summary>
    /// 标签list去重复
    /// </summary>
    class key_collection : IEqualityComparer<Key>
    {

        public bool Equals(Key x, Key y)
        {
            if (x.KeyID == y.KeyID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode(Key obj)
        {
            return 0;
        }
    }
}