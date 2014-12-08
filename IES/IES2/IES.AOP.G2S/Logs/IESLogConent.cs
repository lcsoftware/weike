using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.AOP.G2S
{
    /// <summary>
    /// IES日志内容类
    /// </summary>
    public class IESLogConent
    {
        public int UserID { set; get; }
        public int CourseID { set; get; }
        public int OCID { set; get; }
        public int ModuleID { set; get; }
        public int ActionID { set; get; }
        public string IP { set; get; }
        public DateTime Date { set; get; }
        public string Conten { set; get; }

        /// <summary>
        /// 实例化一个  IES日志内容 对象
        /// </summary>
        /// <param name="userID">userID</param>
        /// <param name="courseID">courseID</param>
        /// <param name="ocID">ocID</param>
        /// <param name="moduleID">moduleID</param>
        /// <param name="actionID">actionID</param>
        /// <param name="ip">ip</param>
        /// <param name="conten">conten</param>
        public IESLogConent(int userID, int courseID, int ocID, int moduleID, int actionID, string ip, string conten)
            : this(userID, courseID, ocID, moduleID, actionID, ip, DateTime.Now, conten)
        { }
        /// <summary>
        ///  实例化一个  IES日志内容 对象
        /// </summary>
        /// <param name="userID">userID</param>
        /// <param name="courseID">courseID</param>
        /// <param name="ocID">ocID</param>
        /// <param name="moduleID">moduleID</param>
        /// <param name="actionID">actionID</param>
        /// <param name="ip">ip</param>
        /// <param name="date">date</param>
        /// <param name="conten">conten</param>
        public IESLogConent(int userID, int courseID, int ocID, int moduleID, int actionID, string ip, DateTime date, string conten)
        {
            this.UserID = userID;
            this.CourseID = courseID;
            this.OCID = ocID;
            this.ModuleID = moduleID;
            this.ActionID = actionID;
            this.IP = ip;
            this.Date = date;
            this.Conten = conten;
        }
    }
}
