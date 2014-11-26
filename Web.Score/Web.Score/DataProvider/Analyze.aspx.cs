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

namespace App.Web.Score.DataProvider
{
    public partial class Analyze : System.Web.UI.Page
    {
        [WebMethod]
        public static IList<GradeCourse> GetCourses(int micYear, GradeCode gradeCode, TestLogin testLogin)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "Select testno,coursecode from s_tb_testlogin" 
                       + " where academicyear=@micYear"
                       + " and testno=@testNo";
                DataTable table = bll.FillDataTableByText(sql, new { micYear = micYear, testNo = testLogin.TestLoginNo });
                if (string.IsNullOrEmpty(table.Rows[0]["coursecode"].ToString()) || table.Rows[0]["coursecode"].ToString() == "00000")
                {
                    sql = "select a.Academicyear, a.GradeNo, a.CourseCode, b.FullName from tbcourseuse a,tdcoursecode b"
                               + " where academicyear=@micYear"
                               + " and gradeno=@gradeNo"
                               + " and a.coursecode=b.coursecode";
                    return bll.FillListByText<GradeCourse>(sql, new { micYear = micYear, gradeNo = gradeCode.GradeNo });
                }
                else {
                    sql = "select * from tdcourseCode where coursecode=@courseCode";
                    return bll.FillListByText<GradeCourse>(sql, new { courseCode = table.Rows[0]["coursecode"].ToString() });
                }
            }
        }
    }
}