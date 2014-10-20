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
    using App.Score.Db;
    using App.Score.Entity;
    using App.Score.Util;
    using System;
    using System.Collections.Generic;
    using System.Data;
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
        public static UserInfo Verify(string user, string pwd)
        {
            using (AppBLL bll = new AppBLL())
            {
                UserInfo userInfo = bll.GetDataItem<UserInfo>("USP_System_Verify", new { User = user, Pwd = pwd });
                if (userInfo != null)
                {
                    CookieHelper.SetCookie(COOKIE_NAME, Newtonsoft.Json.JsonConvert.SerializeObject(userInfo), DateTime.Now.AddDays(1));
                }
                return userInfo;
            }
        }

        [WebMethod]
        public static int ChangePwd(string teacherID, string oldPwd, string newPwd, int status)
        {
            using (AppBLL bll = new AppBLL())
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
            using (AppBLL bll = new AppBLL())
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
        public static IList<UserInfo> GetUsers()
        {
            using (AppBLL bll = new AppBLL())
            {
                string sql = "Select * from tbUserGroupInfo where UserOrGroup='1' Order by Userorgroup,substring(TeacherID,1,4)";
                return bll.FillListByText<UserInfo>(sql, null);
            }
        }
        [WebMethod]
        public static IList<GroupUser> GetGroupAndUsers()
        {
            return UtilBLL.GetGroupUsers();
        }

        /// <summary>
        /// 帮前端生成实体对象
        /// </summary>
        /// <param name="category">类别</param>
        /// <returns></returns>
        [WebMethod]
        public static UserInfo AddUserGroup(string category)
        {
            return new UserInfo() { UserOrGroup = category };
        }

        [WebMethod]
        public static int SaveUserGroup(UserInfo userGroup)
        {

            var sql = "Select * from tbUserGroupInfo Where Name=:UserName";
            using (AppBLL bll = new AppBLL())
            {
                DataTable table = bll.FillDataTableByText(sql, new { UserName = userGroup.Name });
                if (table.Rows.Count > 0) return -1; //数据库中已经存在名称为{0}的用户或组

                return bll.ExecuteNonQuery("USP_System_InsertUserGroup", userGroup); 
            }
        }
    }
}