using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.JW.Model
{
    /// <summary>
    /// 查询条件集合
    /// </summary>
    public class ParmInfo
    {
        /// <summary>
        /// 课程分类
        /// </summary>
        public List<Coursetype> crtylist { get; set; }
        /// <summary>
        /// 所属机构
        /// </summary>
        public List<Organization> orglist { get; set; }
        /// <summary>
        /// 学期
        /// </summary>
        public List<TermType> trlist { get; set; }
        /// <summary>
        /// 学年
        /// </summary>
        public List<Term> tyerlist { get; set; }
        /// <summary>
        /// 授课方式
        /// </summary>
        public List<CourseTeachingType> crtchtylist { get; set; }
        /// <summary>
        /// 行政班
        /// </summary>
        public List<Class> clslist { get; set; }
        /// <summary>
        /// 入学年份
        /// </summary>
        public List<User> etylist { get; set; }
        /// <summary>
        /// 学制
        /// </summary>
        public List<Specialty> schlenlist { get; set; }
        /// <summary>
        /// 学科
        /// </summary>
        public List<SpecialtyType> sptylist { get; set; }
        /// <summary>
        /// 专业
        /// </summary>
        public List<Specialty> splist { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public List<AuRole> arolist { get; set; }
        /// <summary>
        /// 子系统
        /// </summary>
        public List<Sys> syslist { get; set; }
        /// <summary>
        /// 角色存储空间
        /// </summary>
        public List<CfgSchool> cfglist { get; set; }
    }
}
