using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.SYS.Model
{
    [Serializable]
    public partial class Log
    {
        public Log()
        {}

        #region 补充信息
        /// <summary>
        /// 操作名
        /// </summary>
        private string _actname;

        public string Actname
        {
            get { return _actname; }
            set { _actname = value; }
        }
        /// <summary>
        /// 用户编号
        /// </summary>
        private string _userno;

        public string Userno
        {
            get { return _userno; }
            set { _userno = value; }
        }
        /// <summary>
        /// 功能模块名
        /// </summary>
        private string _modname;

        public string Modname
        {
            get { return _modname; }
            set { _modname = value; }
        }
        /// <summary>
        /// 用户身份
        /// </summary>
        private string _role;

        public string Role
        {
            get { return _role; }
            set { _role = value; }
        }
        /// <summary>
        /// 登录名
        /// </summary>
        private string _loginname;

        public string Loginname
        {
            get { return _loginname; }
            set { _loginname = value; }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        private DateTime _startdate;

        public DateTime Startdate
        {
            get { return _startdate; }
            set { _startdate = value; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        private DateTime _enddate;

        public DateTime Enddate
        {
            get { return _enddate; }
            set { _enddate = value; }
        }

        public int rowscount { get; set; }

        #endregion

        #region Model
        private int _Logid;
        private int _userid;
        private int _courseid;
        private int _ocid;
        private int _moduleid;
        private int _actionid;
        private string _ip;
        private DateTime _date;
        private string _conten;
        private string _loglevel;
        /// <summary>
        /// 日志ID
        /// </summary>
        public int LogID
        {
            get { return _Logid; }
            set { _Logid = value; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int Userid
        {
            get { return _userid; }
            set { _userid = value; }
        }
        /// <summary>
        /// 课程ID
        /// </summary>
        public int Courseid
        {
            get { return _courseid; }
            set { _courseid = value; }
        }
        /// <summary>
        /// 在线课程ID
        /// </summary>
        public int Ocid
        {
            get { return _ocid; }
            set { _ocid = value; }
        }
        /// <summary>
        /// 功能模块ID
        /// </summary>
        public int Moduleid
        {
            get { return _moduleid; }
            set { _moduleid = value; }
        }
        /// <summary>
        /// 操作ID
        /// </summary>
        public int Actionid
        {
            get { return _actionid; }
            set { _actionid = value; }
        }
        /// <summary>
        /// IP
        /// </summary>
        public string Ip
        {
            get { return _ip; }
            set { _ip = value; }
        }
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string Conten
        {
            get { return _conten; }
            set { _conten = value; }
        }
        /// <summary>
        /// 日志层次
        /// </summary>
        public string Loglevel
        {
            get { return _loglevel; }
            set { _loglevel = value; }
        }
        #endregion
    }
}
