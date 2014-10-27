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

        private static IList<GradeCode> BuildGrades(IList<GradeAndClass> ClassList)
        {
            IList<GradeCode> gradeList = new List<GradeCode>();
            foreach (var gradeAndClass in ClassList)
            {
                if (!gradeList.Contains(gradeAndClass.Grade))
                {
                    gradeList.Add(gradeAndClass.Grade);
                }
            }
            return gradeList;
        }

        private static void BuildGradeClass(GradeCode grade, IList<GradeAndClass> ClassList)
        {
            var gradeClasses = from v in ClassList where v.GradeNo == grade.GradeNo select v;
            foreach (var gradeClass in gradeClasses)
            {
                if (!grade.GradeClasses.Contains(gradeClass.GClass))
                {
                    grade.GradeClasses.Add(gradeClass.GClass);
                }
            }
        }

        [WebMethod]
        public static IList<GradeCode> LoadGradeClass(int academicYear, bool andStudent)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "select a.SystemID GradeSystemID, a.GradeNo,a.GradeName, a.GradeBriefName," +
                            " b.SystemID as ClassSystemID, b.ClassNo, b.AcadEmicYear, b.ClassType, b.IsDelete" +
                            " from tdGradeCode a left join tbGradeClass b on a.GradeNo=b.GradeNo and b.AcademicYear=@Year AND b.ClassType='0'" +
                            " ORDER BY a.GradeNo, b.ClassNo";
                IList<GradeAndClass> ClassList = bll.FillListByText<GradeAndClass>(sql, new { Year = academicYear });
                IList<GradeCode> gradeList = BuildGrades(ClassList);
                foreach (var grade in gradeList)
                {
                    BuildGradeClass(grade, ClassList);
                }
                if (andStudent)
                {
                    sql = "SELECT tbStudentClass.SRID StudentId," +
                        " tbStudentBaseInfo.StdName StdName," +
                        " tbStudentBaseInfo.Sex," +
                        " tbStudentClass.ClassCode ClassCode," +
                        " tbStudentClass.ClassSN ClassSN " +
                        " FROM tbStudentClass LEFT JOIN tbStudentBaseInfo ON " +
                        " tbStudentClass.SRID = tbStudentBaseInfo.SRID " +
                        " LEFT JOIN tbStudentStatus ON " +
                        " tbStudentClass.SRID = tbStudentStatus.SRID AND " +
                        " tbStudentClass.AcademicYear = tbStudentStatus.AcademicYear " +
                        " WHERE tbStudentClass.AcademicYear =@Year" +
                        " AND tbStudentStatus.Status IN ('01','02','03') " +
                        " AND tbStudentBaseInfo.IsDelete = '0' ";
                    IList<Student> allStudents = bll.FillListByText<Student>(sql, new { Year = academicYear });
                    BuildClassStudent(gradeList, allStudents);
                }
                return gradeList;
            }
        }

        public static void BuildClassStudent(IList<GradeCode> gradeList, IList<Student> allStudents)
        {
            foreach (var grade in gradeList)
            {
                foreach (var gradeClass in grade.GradeClasses)
                {
                    var classStudents = from v in allStudents where v.ClassCode.Equals(gradeClass.ClassNo) select v;
                    foreach (var student in classStudents)
                    {
                        gradeClass.Students.Add(student);
                    }
                }
            }
        }

        //SELECT GradeNo,GradeName FROM tdGradeCode ORDER BY GradeNo ASC 

        //SELECT tbStudentClass.SRID StudentId,
        //            tbStudentBaseInfo.StdName StdName,
        //            CASE tbStudentBaseInfo.Sex WHEN 1 THEN '男' WHEN 2 THEN '女' END Sex,
        //            tbStudentClass.ClassCode ClassCode,
        //            tbStudentClass.ClassSN ClassSN 
        //            FROM tbStudentClass LEFT JOIN tbStudentBaseInfo ON 
        //             tbStudentClass.SRID = tbStudentBaseInfo.SRID 
        //             LEFT JOIN tbStudentStatus ON 
        //             tbStudentClass.SRID = tbStudentStatus.SRID AND 
        //             tbStudentClass.AcademicYear = tbStudentStatus.AcademicYear 
        //            WHERE tbStudentClass.AcademicYear = '2013' 
        //            AND tbStudentClass.ClassCode =2301
        //            AND tbStudentStatus.Status IN ('01','02','03') 
        //            AND tbStudentBaseInfo.IsDelete = '0' 

    }
}