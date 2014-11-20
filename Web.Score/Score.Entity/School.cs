using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Score.Entity
{
    public class SchoolBaseInfo
    {
        public string SystemID;
        public string SchoolCode;
        public string SchoolName;
        public string BriefName;
        public string DistricNo;
        public string EduSysType;
        public string IsLodging;
        public string ImportantType;
        public string SchoolStyle;
        public string SchoolOrgan;
        public string AddressSchool;
        public string ZipSchool;
        public string PhoneSchool;
        public string FaxSchool;
        public string EmailSchool;
        public string WebSite;
        public string Bank;
        public string Account;
        public string TotalTeacher;
        public string TotalEmployee;
        public string AcademicYear;
        public string Semester;


    }
    public class Academicyear
    {
        public int MicYear;
    }
    public class GradeCode
    {
        public string SystemID;
        public string GradeName;
        public string GradeBriefName;
        public string GradeNo;
        public IList<GradeClass> GradeClasses = new List<GradeClass>();
        public override bool Equals(object obj)
        {
            return this.GradeNo.Equals(((GradeCode)obj).GradeNo);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class GradeClass
    {
        public string SystemID;
        public string GradeNo;
        public string GradeName;
        public string GradeBriefName;
        public string AcadEmicYear;
        public string ClassNo;
        public string ClassType;
        public string IsDelete;
        public string ClassNoPart
        {
            get
            {
                return this.ClassNo.Substring(2);
            }
        }
        public IList<Student> Students = new List<Student>();
        public override bool Equals(object obj)
        {
            var other = (GradeClass)obj;
            return this.GradeNo.Equals(other.GradeNo) &&
                    this.AcadEmicYear.Equals(other.AcadEmicYear) &&
                    this.ClassNo.Equals(other.ClassNo);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        } 

    }

    public class Student
    {
        public string StudentId;
        public string StdName;
        public int Sex;
        public string ClassCode;
        public string ClassSN;
        public bool Keep = false; //留级为true
    }

    public class StudentClass
    {
        public string SystemID;
        public string SRID;
        public string ClassSN;
        public string ClassCode;
        public int AcademicYear;
        public int IsDelete;
    }

    public class GradeAndClass
    {
        public string GradeSystemID;
        public string GradeName;
        public string GradeBriefName;
        public string GradeNo;

        public string ClassSystemID;
        public string AcadEmicYear;
        public string ClassNo;
        public string ClassType;
        public string IsDelete;

        public GradeCode Grade
        {
            get
            {
                return new GradeCode()
                {
                    GradeBriefName = this.GradeBriefName,
                    GradeName = this.GradeName,
                    GradeNo = this.GradeNo,
                    SystemID = this.GradeSystemID
                };
            }
        }

        public GradeClass GClass
        {
            get
            {
                return new GradeClass()
                {
                    AcadEmicYear = this.AcadEmicYear,
                    ClassNo = this.ClassNo,
                    IsDelete = this.IsDelete,
                    SystemID = this.ClassSystemID,
                    ClassType = this.ClassType,
                    GradeNo = this.GradeNo
                };
            }
        }
    }


    public class GradeCourse
    {
        public int Academicyear;
        public string GradeNo;
        public string CourseCode;
        public string FullName;
    }

    public class TestType
    {
        public int Code;
        public string Name;
    }


    public class TestLogin
    {
        public int TestLoginNo;
        public int AcademicYear;
        public int Semester;
        public int TestType;
        public int TestNo;
        public string GradeNo;
        public string Coursecode;
        public int MarkTypeCode;
    }

    public class StudentScore
    {
        public string SRID; 
        public string GradeName;
        public string ClassCode;
        public string ClassSN;
        public string StdName;
        public string Coursecode;
        public string CourseName; 
        public string NumScore; 
        public string StandardScore;
        public string MarkName;
    }

    public class SumDecEntry
    {
        public string Avg1;
        public string Avg2;
        public int Count1;
        public int Count2;
        public int Count3; 
        public int Count4;
        public int Count5; 
        public int Count6;
    }

    public class XjEntry
    {
        public int MicYear;
        public string SRID;
        public string CourseCode;
        public string CourseName;
        public string TeacherID;
        public string MarkCode;
        public float Score;
        public string Operator;
    }

    public class TeacherOption
    {
        public string SRID;
        public int AcademicYear;
        public string TeacherOP;
    }

    public class StudentImportEntry
    {
        public string MicYear;
        public string SchoolNo;
        public string Grade;
        public string GradeClass;
        public string ClassN;
        public string Name;
        public string Sex;
    }

    public class ParamStudentImport1
    {
        public string YEARCODE;
        public string ST_SRID;
    }

    /// <summary>
    /// 返回值说明
    /// </summary>
    public class ResultEntry
    {
        public int Code { get; set; }
        public object Message { get; set; }
    }
}
