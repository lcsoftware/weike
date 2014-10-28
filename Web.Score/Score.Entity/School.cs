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
    }

    public class GradeClass
    {
        public string SystemID;
        public string GradeNo;
        public string AcadEmicYear;
        public string ClassNo;
        public string ClassType;
        public string IsDelete;
        public IList<Student> Students = new List<Student>();
        public override bool Equals(object obj)
        {
            var other = (GradeClass)obj;
            return this.GradeNo.Equals(other.GradeNo) &&
                    this.AcadEmicYear.Equals(other.AcadEmicYear) &&
                    this.ClassNo.Equals(other.ClassNo);
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


    public class GradCourse
    {
        public string SystemID;
        public string GradCourseCode;
        public string GradCourseName;
    }

}
