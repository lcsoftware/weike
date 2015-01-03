using IES.CC.OC.Model;
using IES.JW.Model;
using IES.SYS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.CC.OC.Model
{
    public class OCFCInfo
    {
        /// <summary>
        /// 翻转课堂学生总数
        /// </summary>
        public int fcStudentCount { get; set; }
        /// <summary>
        /// 翻转课堂小组总数
        /// </summary>
        public int fcLiveGroupCount { get; set; }
        /// <summary>
        /// 教师name
        /// </summary>
        public string fcteacherName { get; set; }
        /// <summary>
        /// 翻转课堂基本信息
        /// </summary>
        public OCFC ocfc { get; set; }

        /// <summary>
        /// 翻转课堂学生信息
        /// </summary>
        public List<User> fcUserList { get; set; }

        /// <summary>
        /// 翻转课堂小组信息
        /// </summary>
        public List<OCFCGroup> fcGroupList { get; set; }

        /// <summary>
        /// 翻转课堂教学班信息
        /// </summary>
        public List<TeachingClass> fcTeachingClass { get; set; }

        /// <summary>
        /// 翻转课堂教师信息
        /// </summary>
        public User fcTeacher { get; set; }

    }
}
