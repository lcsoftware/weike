using IES.G2S.CourseLive.BLL.Test;
using IES.G2S.CoursLive.IBLL.Test;
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
    public partial class MarkingProvider : System.Web.UI.Page
    {
        /// <summary>
        /// 获取试卷的详细信息
        /// </summary>
        /// <param name="PaperID"></param>
        /// <returns></returns>
        [WebMethod]
        public static PaperInfo PaperInfo_Get(int PaperID) {
            ITestBLL itestbll = new TestBLL();
            PaperInfo paperinfo=  itestbll.PaperInfo_Get(PaperID);
            return paperinfo;
        }

         /// <summary>
        /// 获取测试详细信息
         /// </summary>
         /// <param name="TestID"></param>
         /// <returns></returns>
        [WebMethod]
        public static IES.CC.Test.Model.Test Test_Get(int TestID) {
            ITestBLL itestbll = new TestBLL();
             return itestbll.Test_Get(TestID);
        }

        /// <summary>
        /// 学生答案
        /// </summary>
        /// <param name="TestID"></param>
        /// <param name="CheckUserID"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<ExerciseAnswer> TestAnswer_Get(int TestID, int CheckUserID) {
            string userid = IESCookie.GetCookieValue("ies");
            IES.JW.Model.User user = new IES.JW.Model.User { UserID = Int32.Parse(userid) };
            user = UserService.User_Get(user);
            ITestBLL itestbll = new TestBLL();
            return itestbll.TestAnswer_Get(TestID, user.UserID, CheckUserID);
        }

        //[WebMethod]
        //public static 


      


         

    }
}