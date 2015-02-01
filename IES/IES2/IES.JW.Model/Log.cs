using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.JW.Model
{
    [Serializable]
    public partial class Log
    {
        public Log()
        { }

        #region 补充信息
        /// <summary>
        /// 操作名称
        /// </summary>
        public string Actname { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserNo { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 功能模块名
        /// </summary>
        public string ModName{ get; set; }
        /// <summary>
        /// 用户身份
        /// </summary>
        public string Role{ get; set; }
        public int UserType { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime{ get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime{ get; set; }
        /// <summary>
        /// 列表总数
        /// </summary>
        public int rowscount { get; set; }

        #endregion

        #region Model
        private int _ID;
        private int _UserID;
        private int _CourseID;
        private int _OCID;
        private string _ModuleID;
        private int _ActionID;
        private string _IP;
        private DateTime _Date;
        private string _Conten;
        private string _LogLevel;

        /// <summary>
        /// 编号
        /// </summary>
        public int ID
        {
            set { _ID = value; }
            get { return _ID; }
        }

        /// <summary>
        /// 当前操作用户编号
        /// </summary>
        public int UserID
        {
            set { _UserID = value; }
            get { return _UserID; }
        }

        /// <summary>
        /// 课程编号
        /// </summary>
        public int CourseID
        {
            set { _CourseID = value; }
            get { return _CourseID; }
        }

        /// <summary>
        /// 在线网站编号
        /// </summary>
        public int OCID
        {
            set { _OCID = value; }
            get { return _OCID; }
        }

        /// <summary>
        /// 模块编号
        /// </summary>
        public string ModuleID
        {
            set { _ModuleID = value; }
            get { return _ModuleID; }
        }

        /// <summary>
        /// 模块行为编号
        /// </summary>
        public int ActionID
        {
            set { _ActionID = value; }
            get { return _ActionID; }
        }

        /// <summary>
        /// 来源IP
        /// </summary>
        public string IP
        {
            set { _IP = value; }
            get { return _IP; }
        }

        /// <summary>
        /// 生成时间
        /// </summary>
        public DateTime Date
        {
            set { _Date = value; }
            get { return _Date; }
        }

        /// <summary>
        /// 执行的内容描述
        /// </summary>
        public string Conten
        {
            set { _Conten = value; }
            get { return _Conten; }
        }

        /// <summary>
        /// 日志级别
        /// </summary>
        public string LogLevel
        {
            set { _LogLevel = value; }
            get { return _LogLevel; }
        }

        #endregion

    }
}
