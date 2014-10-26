/* **************************************************************
  * Copyright(c) 2014 Score.web, All Rights Reserved.   
  * File             : School.aspx.cs
  * Description      : 学校相关数据处理
  * Author           : shujianhua 
  * Created          : 2014-10-05  
  * Revision History : 
******************************************************************/ 
namespace App.Web.Score.DataProvider
{
    using App.Score.Data;
    using App.Score.Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Services;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    public partial class School : System.Web.UI.Page
    {
        [WebMethod]
        public static SchoolBaseInfo LoadSchool()
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "select * FROM tbSchoolBaseInfo";
                var schools = bll.FillListByText<SchoolBaseInfo>(sql, null);
                return schools.Any() ? schools.First() : null;
            }
        }

        [WebMethod]
        public static GradeCode LoadGradeClass(int academicYear)
        {
            GradeCode grade = new GradeCode();
            using (AppBLL bll = new AppBLL()) { 
                var sql = "select a.GradeNo,a.GradeName, b.ClassNo,a.GradeBriefName ShortName" +
                          " from tdGradeCode a left join tbGradeClass b on a.GradeNo=b.GradeNo" +
                          " and b.AcademicYear=@academicYear AND b.ClassType='0'" +
                          " ORDER BY a.GradeNo, b.ClassNo";
                bll.FillDataTable(sql, new { academicYear = academicYear });
            }
            return grade;
        }
        SELECT GradeNo,GradeName FROM tdGradeCode ORDER BY GradeNo ASC


select a.SystemID GradeSystem, a.GradeNo,a.GradeName, a.GradeBriefName ShortName,
b.SystemID as ClassSystem, b.ClassNo, b.AcadEmicYear,b.ClassNo,
from tdGradeCode a left join tbGradeClass b on a.GradeNo=b.GradeNo and b.AcademicYear='2013' AND b.ClassType='0' 
ORDER BY a.GradeNo, b.ClassNo


SELECT tbStudentClass.SRID StudentId,
            tbStudentBaseInfo.StdName StdName,
            CASE tbStudentBaseInfo.Sex WHEN 1 THEN '男' WHEN 2 THEN '女' END Sex,
            tbStudentClass.ClassCode ClassCode,
            tbStudentClass.ClassSN ClassSN 
	        FROM tbStudentClass LEFT JOIN tbStudentBaseInfo ON 
             tbStudentClass.SRID = tbStudentBaseInfo.SRID 
             LEFT JOIN tbStudentStatus ON 
             tbStudentClass.SRID = tbStudentStatus.SRID AND 
             tbStudentClass.AcademicYear = tbStudentStatus.AcademicYear 
	        WHERE tbStudentClass.AcademicYear = '2013' 
      		AND tbStudentClass.ClassCode =2301
      		AND tbStudentStatus.Status IN ('01','02','03') 
      		AND tbStudentBaseInfo.IsDelete = '0' 

    }
}