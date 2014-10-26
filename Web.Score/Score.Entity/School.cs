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
        public override bool Equals(object obj)
        {
            var other = (GradeClass)obj;
            return  this.GradeNo.Equals(other.GradeNo) &&
                    this.AcadEmicYear.Equals(other.AcadEmicYear) &&
                    this.ClassNo.Equals(other.ClassNo);
        }
    }


    public class GradCourse
    {
        public string SystemID;
        public string GradCourseCode;
        public string GradCourseName;
    }

}
