using App.Score.Data;
using App.Score.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App.Web.Score.DataProvider
{
    public partial class Query : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 获得课程
        /// </summary>
        /// <param name="micyear">学年</param>
        /// <param name="teacherid">教师ID</param>
        /// <returns></returns> 
        [WebMethod]
        public static IList<GradeCourse> GetGradeCourseByTeacherId(int micyear, string teacherid)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "select a.CourseCode,b.FullName from tbTeacherClass as A,tdCourseCode as B" +
                           " where A.CourseCode=B.CourseCode and teacherid=@teacherid" +
                           " and  a.Academicyear=@micyear" +
                           " group by A.coursecode,b.FullName";
                return bll.FillListByText<GradeCourse>(sql, new { micyear = micyear, teacherid = teacherid });
            }
        }
    }
}