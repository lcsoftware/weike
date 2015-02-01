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
        public static List<OCClass> ClassList(OCClass model, int PageIndex, int PageSize)
        {
            OCClassBLL oCClassBLL = new OCClassBLL();
            return oCClassBLL.OCClass_List(model, PageIndex, PageSize);
        }

        [WebMethod]
        public static bool OCClass_RegNum_Upd(OCClass model)
        {
            OCClassBLL oCClassBLL = new OCClassBLL();
            return oCClassBLL.OCClass_RegNum_Upd(model);
        }

        [WebMethod]
        public static bool OCClass_IsHistroy_Upd(OCClass model)
        {
            OCClassBLL oCClassBLL = new OCClassBLL();
            return oCClassBLL.OCClass_IsHistroy_Upd(model);
        }

        [WebMethod]
        public static bool OCClass_Del(OCClass model)
        {
            OCClassBLL oCClassBLL = new OCClassBLL();
            return oCClassBLL.OCClass_Del(model);
        }

        [WebMethod]
        public static bool OCClass_InputOutAll(OCClass model)
        {
            OCClassBLL oCClassBLL = new OCClassBLL();
            return true;// oCClassBLL.OCClass_IsHistroy_Upd(model);
        }

        /// <summary>
        /// 获取编辑教学班学生信息
        /// </summary>
        /// <param name="occlassid"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<OCClassStudent> OCClassStudent_List(OCClass occlass, int PageIndex, int PageSize)
        {
            OCClassBLL oCClassBLL = new OCClassBLL();
            return oCClassBLL.OCClassStudent_List(occlass, PageIndex, PageSize);
        }

        /// <summary>
        /// 教学班添加学生搜索
        /// </summary>
        /// <param name="occlassid"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<OCClassStudent> OCClass_Student_List(OCClassStudent occlassid, int PageIndex, int PageSize)
        {
            OCClassBLL oCClassBLL = new OCClassBLL();
            return oCClassBLL.OCClass_Student_List(occlassid, PageIndex, PageSize);
        }
        /// <summary>
        /// 获取编辑教学班
        /// </summary>
        /// <param name="occlass"></param>
        /// <returns></returns>
        [WebMethod]
        public static OCClass OCClass_Get(OCClass occlass)
        {
            OCClassBLL oCClassBLL = new OCClassBLL();

            return oCClassBLL.OCClass_Get(occlass);
        }
        /// <summary>
        /// 获取编辑教学班授课教师
        /// </summary>
        /// <param name="occlass"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<OCTeamDropdownList> OCTeam_Dropdown_List(OCTeamDropdownList occlass)
        {
            OCClassBLL oCClassBLL = new OCClassBLL();

            return oCClassBLL.OCTeam_Dropdown_List(occlass);
        }
        /// <summary>
        /// 行政班列表
        /// </summary>
        /// <param name="occlass"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<IES.JW.Model.Class> Class_List(IES.JW.Model.Class model, int PageIndex, int PageSize)
        {
            OCClassBLL oCClassBLL = new OCClassBLL();

            return oCClassBLL.Class_List(model, PageIndex, PageSize);
        }

        /// <summary>
        /// 通过行政班添加学生获取行政班列学生表
        /// </summary>
        /// <param name="occlass"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<OCClassStudent> ClassStudent_List(IES.JW.Model.Class occlass, List<OCClassStudent> occlassstudentlist, int PageIndex, int PageSize)
        {
            OCClassBLL oCClassBLL = new OCClassBLL();

            return oCClassBLL.ClassStudent_List(occlass, occlassstudentlist, PageIndex, PageSize);
        }

        /// <summary>
        /// 通过搜索添加学生
        /// </summary>
        /// <param name="occlass"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<OCClassStudent> SelectSelectedStudent_List(List<OCClassStudent> occlassstudentsearchlist, List<OCClassStudent> occlassstudentlist)
        {
            OCClassBLL oCClassBLL = new OCClassBLL();

            return oCClassBLL.SelectSelectedStudent_List(occlassstudentsearchlist, occlassstudentlist);
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="occlassstudentsearchlist"></param>
        /// <param name="occlassstudentlist"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<OCClassStudent> SelectAll_List(List<OCClassStudent> occlassstudentsearchlist, bool isselectall)
        {
            OCClassBLL oCClassBLL = new OCClassBLL();

            return oCClassBLL.SelectAll_List(occlassstudentsearchlist, isselectall);
        }

        /// <summary>
        /// 单页全选
        /// </summary>
        /// <param name="occlassstudentsearchlist"></param>
        /// <param name="occlassstudentlist"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<OCClassStudent> Select_Page_List(List<OCClassStudent> occlassstudentsearchlist, bool isselectall, int PageIndex,int PageSize)
        {
            OCClassBLL oCClassBLL = new OCClassBLL();

            return oCClassBLL.Select_Page_List(occlassstudentsearchlist, isselectall, PageIndex, PageSize);
        }


        [WebMethod]
        public static OCClass OCClass_Edit(OCClass occlass, List<OCTeamDropdownList> octeamdropdownlist, List<OCClassStudent> occlassstudent)
        {
            OCClassBLL oCClassBLL = new OCClassBLL();
            return oCClassBLL.OCClass_Edit(occlass, octeamdropdownlist, occlassstudent);
        }

        /// <summary>
        /// 删除学生
        /// </summary>
        /// <param name="occlass"></param>
        /// <param name="octeamdropdownlist"></param>
        /// <param name="occlassstudent"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<OCClassStudent> DeleteStudent(OCClassStudent occlassstudent, List<OCClassStudent> occlassstudentlist)
        {
            //return occlassstudentlist;
            OCClassBLL oCClassBLL = new OCClassBLL();
            return oCClassBLL.DeleteStudent(occlassstudent, occlassstudentlist);
        }

        [WebMethod]
        public static List<OCClassStudent> BatchDeleteStudent(List<OCClassStudent> occlassstudentlist)
        {
            OCClassBLL oCClassBLL = new OCClassBLL();
            return oCClassBLL.BatchDeleteStudent(occlassstudentlist);
        }

    }
}