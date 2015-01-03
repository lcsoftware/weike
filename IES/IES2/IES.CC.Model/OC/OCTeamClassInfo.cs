using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.CC.OC.Model
{
    public class OCTeamClassInfo
    {
        public OCTeamClassInfo()
        { }
        #region Model
        private int _teachingclassid;
        private string _studentcount;
        private string _classname;
        /// <summary>
        /// 教学班编号
        /// </summary>
        public int TeachingClassID
        {
            get { return _teachingclassid; }
            set { _teachingclassid = value; }
        }
        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 教学班名称
        /// </summary>
        public string ClassName
        {
            set { _classname = value; }
            get { return _classname; }
        }
        /// <summary>
        /// 学生数量
        /// </summary>
        public string StudentCount
        {
            set { _studentcount = value; }
            get { return _studentcount; }
        }
        #endregion Model
    }
}
