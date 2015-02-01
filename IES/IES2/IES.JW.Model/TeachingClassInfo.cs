using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.JW.Model
{
    public class TeachingClassInfo
    {
        public int TeachingClassID { get; set; }

        public TeachingClass teachingclass { get; set; }

        public List<TeachingClassStudent> teachingclassstudentlist { get; set; }

        public List<TeachingClassTeacher> teachingclassteacherlist { get; set; }
    }
}
