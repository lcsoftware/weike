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
        public static UserGroupInfo Verify(string user, string pwd)
        {
            using (AppBLL bll = new AppBLL())
            {
                UserGroupInfo userInfo = bll.GetDataItem<UserGroupInfo>("USP_System_Verify", new { User = user, Pwd = pwd });
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
        /// 获取用户功能 用于系统初始化菜单
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
        /// 获得用户功能 用于权限编辑
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        [WebMethod]
        public static IList<UserGroupFunc> GetUserFuncs(UserGroupInfo teacher)
        {
            try
            {
                using (AppBLL bll = new AppBLL())
                {
                    //用户从组所获得的功能(权限)
                    var sql = "SELECT c.TeacherID, cast(b.GroupID as varchar) as GroupID, a.FuncID FROM  tbGroupInfo b INNER JOIN" +
                                " tbUserGroupInfo c ON b.TeacherID = c.TeacherID INNER JOIN " +
                                " s_tb_Rights a ON b.GroupID = a.TeacherID " +
                                " where c.TeacherID=@teacher and a.SYSNO = 2 " +
                                " union all" +
                                " SELECT TeacherID, '-1' As GroupID, FuncId from s_tb_Rights where TeacherID=@teacher and SYSNO = 2";
                    return bll.FillListByText<UserGroupFunc>(sql, new { teacher = teacher.TeacherID });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 读取菜单内容 
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static FuncEntry GetMenus()
        {
            return UtilBLL.GetFunc();
        }

        [WebMethod]
        public static IList<UserGroupInfo> GetUserGroups(string userOrGroup)
        {
            using (AppBLL bll = new AppBLL())
            {
                string sql = "Select * from tbUserGroupInfo where UserOrGroup=@userOrGroup Order by Userorgroup,substring(TeacherID,1,4)";
                return bll.FillListByText<UserGroupInfo>(sql, new { userOrGroup = userOrGroup });
            }
        }
        [WebMethod]
        public static IList<UserGroupInfo> GetGroupAndUsers()
        {
            return UtilBLL.GetGroupUsers();
        }

        [WebMethod]
        public static IList<UserGroupInfo> GetUsersOfGroup(string groupID)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "select a.GroupID, b.*  from tbGroupInfo a, tbUserGroupInfo b where a.TeacherID=b.TeacherID and a.GroupID=@p";
                return bll.FillListByText<UserGroupInfo>(sql, new { p = groupID });
            }
        }

        [WebMethod]
        public static IList<UserGroupInfo> GetAllUsers()
        {
            IList<UserGroupInfo> allUserGroups = GetAllUserGroups();
            var allUsers = from v in allUserGroups where v.UserOrGroup.Equals("1") select v;
            return allUsers.ToList<UserGroupInfo>();
        }

        [WebMethod]
        public static IList<UserGroupInfo> GetAllUserGroups()
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "select *  from tbUserGroupInfo Order by Userorgroup,substring(TeacherID,1,4)";
                return bll.FillListByText<UserGroupInfo>(sql, null);
            }
        }

        [WebMethod]
        public static IList<UserAuth> GetUserAuths(string teacher)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "SELECT a.FuncId, b.FuncName, b.Description, b.FuncType" +
                          " FROM  s_tb_Rights a INNER JOIN   s_tb_Function b ON a.FuncId = b.FuncID" +
                          " where a.TeacherID in (select @teacherID" +
                          " union Select GroupID from tbGroupInfo where teacherID=@teacherID)";
                return bll.FillListByText<UserAuth>(sql, new { teacherID = teacher });
            }
        }

        /// <summary>
        /// 帮前端生成实体对象
        /// </summary>
        /// <param name="category">类别</param>
        /// <returns></returns>
        [WebMethod]
        public static UserGroupInfo AddUserGroup(string category)
        {
            return new UserGroupInfo() { UserOrGroup = category, TeacherID = "-1" };
        }

        [WebMethod]
        public static int LeaveGroup(string teacher, string groupID)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "delete from tbGroupInfo Where teacherID=@teacherID and Groupid=@GroupID";
                return bll.ExecuteNonQueryByText(sql, new { teacherID = teacher, GroupID = groupID });
            }
        }

        [WebMethod]
        public static int JoinGroup(string teacher, string groupID)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "insert into tbGroupInfo(teacherID, groupID) Values(@teacherID, @GroupID)";
                return bll.ExecuteNonQueryByText(sql, new { teacherID = teacher, GroupID = groupID });
            }
        }

        [WebMethod]
        public static int SaveUserGroup(UserGroupInfo userGroup)
        {
            using (AppBLL bll = new AppBLL())
            {
                if (userGroup.TeacherID.Trim().Equals("-1"))
                {
                    //新增查重
                    var sql = "Select * from tbUserGroupInfo Where Name=@UserName";
                    DataTable table = bll.FillDataTableByText(sql, new { UserName = userGroup.Name });
                    if (table.Rows.Count > 0) return -1; //数据库中已经存在名称为{0}的用户或组
                }
                return bll.ExecuteNonQuery("USP_System_InsertUserGroup", userGroup);
            }
        }

        [WebMethod]
        public static ResultEntity RemoveUserGroup(UserGroupInfo userGroup)
        {
            try
            {
                using (AppBLL bll = new AppBLL())
                {
                    var sql = "";
                    DataTable table;
                    sql = "Select Count(*) as iCount from tbLog where TeacherID=@p";
                    table = bll.FillDataTableByText(sql, new { p = userGroup.TeacherID });
                    if (int.Parse(table.Rows[0][0].ToString()) > 0)
                        return new ResultEntity() { State = -1, Context = "发现此用户有日志信息！" };

                    sql = "Select * from tbRights where TeacherID=@p";
                    table = bll.FillDataTableByText(sql, new { p = userGroup.TeacherID });
                    if (table.Rows.Count > 0)
                        return new ResultEntity() { State = -2, Context = "请先去除用户(组)权限！" };

                    if (userGroup.UserOrGroup.Equals("1"))
                    {
                        //用户
                        sql = "Select * from tbTeacherClass where TeacherID=@p and AcademicYear=(Select top 1 AcadEmicYear from tbSchoolBaseInfo)";
                        table = bll.FillDataTableByText(sql, new { p = userGroup.TeacherID });
                        if (table.Rows.Count > 0)
                            return new ResultEntity() { State = -3, Context = "老师本学年有课！" };

                        sql = "Select * from tbGroupInfo where TeacherID=@p";
                        table = bll.FillDataTableByText(sql, new { p = userGroup.TeacherID });
                        if (table.Rows.Count > 0)
                            return new ResultEntity() { State = -4, Context = string.Format("把({0})从组去除！", userGroup.Name) };
                    }
                    else
                    {
                        sql = "Select * from tbGroupInfo where GroupID=@p";
                        table = bll.FillDataTableByText(sql, new { p = userGroup.TeacherID });
                        if (table.Rows.Count > 0)
                            return new ResultEntity() { State = -5, Context = string.Format("请把组({0})的所有用户去除！", userGroup.Name) };
                    }
                    var strTemp = userGroup.TeacherID.Substring(10, 4);
                    if (strTemp.Equals("0001") || strTemp.Equals("0002") || strTemp.Equals("0003")
                        || strTemp.Equals("1001") || strTemp.Equals("0888"))
                    {
                        return new ResultEntity() { State = -6, Context = "系统保留用户(组)！" };
                    }
                    sql = "Select TeacherID,Name from tbUserGroupInfo Where TeacherID=@p";
                    table = bll.FillDataTableByText(sql, new { p = userGroup.TeacherID });
                    if (table.Rows.Count == 0)
                        return new ResultEntity() { State = -7, Context = "数据库错误！" };

                    sql = "Delete  from tbUserGroupinfo Where TeacherID=@p";
                    bll.ExecuteNonQueryByText(sql, new { p = userGroup.TeacherID });
                }
                return new ResultEntity() { State = 1, Context = string.Format("用户(组)<{0}>已经删除！", userGroup.Name) };
            }
            catch (Exception ex)
            {
                return new ResultEntity() { State = -8, Context = string.Format("用户(组)删除失败！", userGroup.Name) };
            }
        }

        private static void BuildChild(FuncEntry func, IList<FuncEntry> funcs)
        {
            var functions = from v in funcs where v.Parent == func.FuncID select v;
            foreach (var entry in functions)
            {
                func.Children.Add(entry);
                BuildChild(entry, funcs);
            }
        }

        /// <summary>
        /// 获得功能树 权限编辑
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static FuncEntry GetFuncTree()
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "Select FuncId, FuncName, Description, FuncType, FuncID0 As Parent, SysNo from s_tb_Function Where FuncName!='-' order by FuncID";
                IList<FuncEntry> funcs = bll.FillListByText<FuncEntry>(sql, null);
                var functions = from v in funcs where v.Parent == null select v;
                if (!functions.Any()) return null;
                var rootFunc = functions.First();
                BuildChild(rootFunc, funcs);
                return rootFunc;
            }
        }

        /// <summary>
        /// 赋予权限
        /// </summary>
        /// <param name="teacher"></param>
        /// <param name="funcID"></param>
        /// <param name="rtype"></param>
        /// <param name="sysNo"></param>
        /// <returns></returns>
        [WebMethod]
        public static int Grant(string teacher, string funcID, int rtype, int sysNo)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "Insert Into s_tb_rights(teacherID,FuncID,Rtype,SYSNO) Values(@UserID,@FuncID,@rtype,@sysNo)";
                return bll.ExecuteNonQueryByText(sql, new { UserID = teacher, FuncID = funcID, rtype = rtype, sysNo = sysNo });
            }
        }

        public static void Revoke(AppBLL bll, string teacher, FuncEntry funcEntry)
        {
            if (funcEntry.Kind > 0)
            {
                var sql = "Delete from s_tb_rights Where TeacherID=:UserID and FuncID=:FuncID and SYSNO=2";
                bll.ExecuteNonQueryByText(sql, new { UserID = teacher, FuncID = funcEntry.FuncID });
            }
            if (funcEntry.Children.Any())
            {
                foreach (var func in funcEntry.Children)
                {
                    Revoke(bll, teacher, func); 
                }
            }
        }
        /// <summary>
        /// 权限回收
        /// </summary>
        /// <param name="teacher"></param>
        /// <param name="funcEntry"></param>
        /// <returns></returns>
        [WebMethod]
        public static int Revoke(string teacher, FuncEntry funcEntry)
        {
            using (AppBLL bll = new AppBLL())
            {
                Revoke(bll, teacher, funcEntry);
            }
            return 1;
        }
    }
}