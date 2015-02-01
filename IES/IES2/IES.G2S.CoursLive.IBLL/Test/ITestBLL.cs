using IES.CC.Test.Model;
using IES.Resource.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.CoursLive.IBLL.Test
{
   public interface ITestBLL
    {
       System.Collections.Generic.List<TestUser> TestUser_NotCheck_List(int OCID, int UserID);

       TestInfo TestInfo_List(string Name,int UserID, int OCID, int Type, int IsSend, string UpdateTime, int PageSize, int PageIndex);

       List<IES.CC.OC.Model.OCClassStudent> TeachingClassStudent_List(int TeachingClassID, int PageIndex, int PageSize);

       CC.Test.Model.Test Test_Get(int TestID);

       int Test_Add_Update(CC.Test.Model.Test Test);

       void TestObject_Add(int TestID, int OCID, string IDS);

       int Paper_OfflineTest_Edit(int TestID, string PaperName, int OCID, int UserID, string content, string answer);

       PaperInfo PaperInfo_Get(int PaperID);

       List<ExerciseAnswer> TestAnswer_Get(int TestID, int UserID, int CheckUserID);

       void TestPaper_Edit(int TestID, int PaperID);
    }
}
