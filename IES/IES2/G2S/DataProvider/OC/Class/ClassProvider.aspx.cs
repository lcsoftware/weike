using IES.CC.OC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using IES.G2S.OC.BLL.OC;
namespace App.G2S.DataProvider
{
    public partial class ClassProvider : System.Web.UI.Page
    {
        [WebMethod]
        public static List<OCClassInfo> ClassList(int OCID, int TeamID, string Searchkey, int IsHistroy)
        {
            OCClassBLL oCClassBLL = new OCClassBLL();
            return oCClassBLL.OCClassInfo_List(OCID, TeamID, Searchkey, IsHistroy);
        }

        //[WebMethod]
        //public static List<OCClassRegStudent> OnLienClassList(int OCID)
        //{
        //    OCClassBLL oCClassBLL = new OCClassBLL();
        //    return oCClassBLL.OCClassRegStudent_List(OCID);
        //}
        [WebMethod]
        public static List<OCClassRegStudent> RegStudentList(int OCID)
        {
            OCClassBLL oCClassBLL = new OCClassBLL();
            return oCClassBLL.OCClassRegStudent_List(OCID);
        }

        [WebMethod]
        public static List<OCClassRegInfo> OCClassList(int OCID, string TeachingClassName, bool IsHistroy, int ClassType, int UserID, int PageIndex, int PageSize)
        {
            OCClassBLL oCClassBLL = new OCClassBLL();
            OCClass model = new OCClass();
            model.OCID = OCID;
            model.TeachingClassName = TeachingClassName;
            model.IsHistroy = IsHistroy;
            model.ClassType = ClassType;
            model.UserID = UserID;
            return oCClassBLL.OCClassList(model, PageIndex, PageSize);
        }
    }
}