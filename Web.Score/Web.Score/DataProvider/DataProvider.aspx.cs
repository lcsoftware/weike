/* **************************************************************
  * Copyright(c) 2014 Score.web, All Rights Reserved.   
  * File             : DataProvider.aspx.cs
  * Description      : 系统级别的服务器处理程序
  * Author           : zhaotianyu 
  * Created          : 2014-10-02  
  * Revision History : 
******************************************************************/ 
namespace App.Web.Score.DataProvider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Services;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using App.Score.Entity;
    using App.Score.Business;
    using App.Score.Util;

    /// <summary>
    /// 数据提供者
    /// </summary>
    public partial class DataProvider : System.Web.UI.Page
    {
        private const string COOKIE_NAME = "ScoreUser";

        /// <summary>
        /// 读取cookie
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string GetCookieInfo()
        {
            return CookieHelper.GetCookieValue(COOKIE_NAME);
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        [WebMethod]
        public static void Logout()
        {
            CookieHelper.RemoveCookie(COOKIE_NAME);
        }
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
                UserEntry userEntry = bll.Verify(user, pwd);
                if (userEntry != null)
                {
                    CookieHelper.SetCookie(COOKIE_NAME, userEntry.ToString(), DateTime.Now.AddDays(1));
                }
                return userEntry;
            }
        }

        /// <summary>
        /// 获取用户功能 
        /// </summary>
        /// <param name="teacherID">用户编号</param>
        /// <returns></returns>
        [WebMethod]
        public static List<App.Score.Entity.FuncEntry> GetFuncs(string teacherID)
        {
            using (AdminBLL bll = new AdminBLL())
            {
                return bll.GetFuncs(teacherID);
            }
        }
    }
}