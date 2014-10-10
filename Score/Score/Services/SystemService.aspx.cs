/* **************************************************************
  * Copyright(c) 2014 ScoreAnalyze, All Rights Reserved.   
  * File             : SystemService.aspx.cs
  * Description      : 系统级别的服务器处理程序
  * Author           : Fenglujian 
  * Created          : 2014-10-01  
  * Revision History : 
******************************************************************/
namespace App.ScoreAnalyze.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Services;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Nevupo.Data;
    using Score.Entry;

    /// <summary>
    /// 系统级别的服务器处理程序 
    /// </summary>
    public partial class SystemService : System.Web.UI.Page
    {
        /// <summary>
        /// 用户验证
        /// </summary>
        /// <param name="user">登录名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        [WebMethod]
        public static UserEntry Verify(string user, string pwd)
        {
            using (SystemBLL bll = new SystemBLL())
            {
                return bll.Verify(user, pwd);
            }
        }

        /// <summary>
        /// 用户权限 
        /// </summary>
        /// <param name="teacherID">用户编号</param>
        /// <returns></returns>
        [WebMethod]
        public static List<Score.Entry.FuncEntry> GetFuncs(string teacherID)
        {
            using (SystemBLL bll = new SystemBLL())
            {
                return bll.GetFuncs(teacherID);
            }
        }
    }
}