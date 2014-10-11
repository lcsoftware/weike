/* **************************************************************
  * Copyright(c) 2014 Score.web, All Rights Reserved.   
  * File             : DataProvider.aspx.cs
  * Description      : 系统级别的服务器处理程序
  * Author           : zhaotianyu 
  * Created          : 2014-10-02  
  * Revision History : 
******************************************************************/ 
namespace App.Score.Web.DataProvider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Services;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Score.Entity;
    using App.Score.Business;

    /// <summary>
    /// 数据提供器 
    /// </summary>
    public partial class DataProvider : System.Web.UI.Page
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
            using (AdminBLL bll = new AdminBLL())
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
        public static List<Score.Entity.FuncEntry> GetFuncs(string teacherID)
        {
            using (AdminBLL bll = new AdminBLL())
            {
                return bll.GetFuncs(teacherID);
            }
        }
    }
}