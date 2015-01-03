using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.JW.Model;


namespace IES.Common.Data
{
    /// <summary>
    /// 通过课程获取课程的相关数据
    /// </summary>
    public class CourseCommonData
    {
        /// <summary>
        /// 获取课程下教学班
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="courseid"></param>
        /// <returns></returns>
        public static List<TeachingClass> Course_TeachingClass_Get(string userid, string courseid)
        {
            return new List<TeachingClass>();
        }

    }
}
