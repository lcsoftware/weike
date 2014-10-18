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
    using App.Score.Data;
    using App.Score.Entity;
    using App.Score.Util;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Services;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    public partial class Admin : System.Web.UI.Page
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
            CookieHelper.CookieExpired(COOKIE_NAME);
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
            using (AppBLL bll = AppBLL.Instance())
            {
                UserEntry userEntry = bll.GetDataItem<UserEntry>("USP_System_Verify", new { User = user, Pwd = pwd });
                if (userEntry != null)
                {
                    CookieHelper.SetCookie(COOKIE_NAME, Newtonsoft.Json.JsonConvert.SerializeObject(userEntry), DateTime.Now.AddDays(1));
                }
                return userEntry;
            }
        }

        [WebMethod]
        public static int ChangePwd(string teacherID, string oldPwd, string newPwd, int status)
        {
            using (AppBLL bll = AppBLL.Instance())
            {
                ChangePwdEntity entity = new ChangePwdEntity() { TeacherID = teacherID, OldPwd = oldPwd, NewPwd = newPwd, Status = status, Result = 0 };
                bll.ExecuteNonQuery("USP_System_ChangePwd", entity);
                return entity.Result;
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
            using (AppBLL bll = AppBLL.Instance())
            {
                return bll.FillList<FuncEntry>("s_p_getTeacherRight", new { TeacherID = teacherID });
            }
        }

        /// <summary>
        /// 读取菜单内容 
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string GetMenuFromFile()
        {
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets/menu.json");
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            try
            {
                StreamReader sr = new StreamReader(fs);
                sr.BaseStream.Seek(0, SeekOrigin.Begin);
                return sr.ReadToEnd();
            }
            catch
            {
                return "";
            }
            finally
            {
                fs.Close();
            }
        }

        [WebMethod]
        public static IList<UserGroupInfo> GetUserGroup()
        {
            using (AppBLL bll = AppBLL.Instance())
            {
                string sql = "Select * from tbUserGroupInfo Order by Userorgroup,substring(TeacherID,1,4)";
                return bll.FillListByText<UserGroupInfo>(sql, null);
            }
        }
        [WebMethod]
        public static IList<GroupUser> GetGroupUsers(string teacher)
        {
            using (AppBLL bll = AppBLL.Instance())
            {
                return bll.FillList<GroupUser>("p_GetGroupUser", new { TeacherID = teacher, Flag = 2 });
            }
        }

        [WebMethod]
        public static int AddUserGroup(UserGroupInfo userGroup)
        {
            using (AppBLL bll = AppBLL.Instance())
            {
                return bll.ExecuteNonQuery("USP_System_InsertUserGroup", userGroup);
            }
        }
    }
}