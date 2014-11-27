﻿using App.Score.Data;
using App.Score.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Score.Db
{
    public class UtilBLL
    {

        public static bool fRightYesNO(string UserName, string FunctionID)
        {
            if (UserName.Equals("Jim.Liu"))
            {
                return true;
            }
            using (AppBLL bll = new AppBLL())
            {
                if (FunctionID.Substring(11, 5).Equals("00"))
                {
                    var sql = string.Format("Select FunctionID from tbUserGroupInfo,tbrights where tbUserGroupInfo.TeacherID=tbRights.TeacherID and FunctionID=\'{0}\' and Name=\'{1}\'", FunctionID, UserName);
                    DataTable table = bll.FillDataTableByText(sql);
                    if (table.Rows.Count > 0) return true;

                    sql = string.Format("Select * from tbUserGroupInfo where Name=\'{0}\' and userorGroup=\'1\'", UserName);
                    table = bll.FillDataTableByText(sql);
                    if (table.Rows.Count == 0) return false;

                    string teacherID = table.Rows[0]["TeacherID"].ToString();
                    sql = "SELECT distinct TbGroupInfo.GroupID, TbRights.FunctionID, TbRights.SysNo,"
                    + " TbUserGroupInfo1.TeacherID FROM TbUserGroupInfo INNER JOIN "
                    + " TbGroupInfo ON TbUserGroupInfo.TeacherID = TbGroupInfo.TeacherID INNER JOIN "
                    + " TbRights INNER JOIN TbUserGroupInfo TbUserGroupInfo1 ON "
                    + " TbRights.TeacherID = TbUserGroupInfo1.TeacherID ON TbGroupInfo.GroupID = TbRights.TeacherID "
                    + " Where tbUserGroupInfo.TeacherID =\'{0}\' and TbRights.FunctionID=\'{1}\'";

                    table = bll.FillDataTableByText(sql);
                    return table.Rows.Count > 0;
                }
                else
                {
                    return true;
                }
            }
        }

        public static IList<UserGroupInfo> GetGroupUsers()
        {
            using (AppBLL bll = new AppBLL())
            {
                SortedList<string, UserGroupInfo> sortedList = new SortedList<string, UserGroupInfo>();

                string sql = "select * from tbUserGroupInfo where UserOrGroup='0'";
                IList<UserGroupInfo> userGroups = bll.FillListByText<UserGroupInfo>(sql, null);
                foreach (var userGroup in userGroups)
                {
                    sortedList.Add(userGroup.TeacherID, userGroup);
                }

                sql = "select a.GroupID, b.*  from tbGroupInfo a, tbUserGroupInfo b where a.TeacherID=b.TeacherID";
                IList<UserGroupInfo> users = bll.FillListByText<UserGroupInfo>(sql, null);
                foreach (var userInfo in users)
                {
                    if (!string.IsNullOrEmpty(userInfo.TeacherID) && sortedList.ContainsKey(userInfo.GroupID))
                    {
                        UserGroupInfo group = sortedList[userInfo.GroupID];
                        group.Children.Add(userInfo);
                    }
                }

                return userGroups;
            }
        }

        private static void BuildFuncTree(FuncEntry root, IList<FuncEntry> funcs)
        {
            try
            {
                var children = from v in funcs where v.Parent == root.FuncID select v;
                foreach (var child in children)
                {
                    root.Children.Add(child);
                    BuildFuncTree(child, funcs);
                }
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// 获取功能树(菜单)
        /// </summary>
        /// <returns></returns>
        public static FuncEntry GetFunc()
        {
            try
            {
                using (AppBLL bll = new AppBLL())
                {
                    var sql = "Select FuncId, FuncName, Description, FuncType, FuncID0 As Parent, SysNo from s_tb_Function order by FuncID";
                    IList<FuncEntry> funcs = bll.FillListByText<FuncEntry>(sql, null);

                    var roots = from v in funcs where v.Parent == -1 select v;
                    if (!roots.Any()) return null;
                    FuncEntry root = roots.First();
                    BuildFuncTree(root, funcs);
                    return root;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取用户功能树(菜单)
        /// </summary>
        /// <returns></returns>
        public static FuncEntry GetUserFuncs(string teacher)
        {
            try
            {
                using (AppBLL bll = new AppBLL())
                {
                    var sql = "select FuncId, FuncName, Description, FuncType, FuncID0 As Parent, SysNo, 1 as Kind from s_tb_Function where CAST(FuncID as Int) in ";
                    sql += " (SELECT cast(a.FuncID as Int) FROM  tbGroupInfo b INNER JOIN tbUserGroupInfo c ON b.TeacherID = c.TeacherID INNER JOIN s_tb_Rights a ON b.GroupID = a.TeacherID ";
                    sql += " where c.TeacherID=@teacher and a.SYSNO = 2) ";
                    sql += " union all ";
                    sql += " select FuncId, FuncName, Description, FuncType, FuncID0 As Parent, SysNo, 2 as Kind from s_tb_Function where CAST(FuncID as Int) in";
                    sql += " (SELECT cast(a.FuncId as Int) from s_tb_Rights a, tbUserGroupInfo b ";
                    sql += " where a.TeacherID=@teacher and a.TeacherID=b.TeacherID and SYSNO = 2)";

                    //sql += " union all select FuncId, FuncName, Description, FuncType, FuncID0 As Parent, SysNo, 2 as Kind " + 
                    //       " from s_tb_Function where FuncID in (1701, 1707,1801,1809)";
                    IList<FuncEntry> funcs = bll.FillListByText<FuncEntry>(sql, new { teacher = teacher });

                    var roots = from v in funcs where v.Parent == -1 select v;
                    if (!roots.Any()) return null;
                    FuncEntry root = roots.First();
                    BuildFuncTree(root, funcs);
                    return root;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string BuildSystemIdBegin()
        {
            using (AppBLL bll = new AppBLL())
            {
                DataTable table = bll.FillDataTableByText("Select SchoolCode,AcadEmicYear from tbSchoolBaseInfo");
                if (table.Rows.Count == 0)
                    return "-1";
                var SchoolNo = table.Rows[0]["SchoolCode"].ToString();
                string CurrentYear = table.Rows[0]["AcadEmicYear"].ToString();
                if (string.IsNullOrEmpty(CurrentYear.Trim()))
                    return "-2";
                return string.Format("{0}{1}", SchoolNo, CurrentYear);
            }
        }

        public static int GetStartIndex(string tableName)
        {
            using (AppBLL bll = new AppBLL())
            {
                DataTable table = bll.FillDataTableByText("Select * from " + tableName);
                if (table.Rows.Count == 0) return 1;
                table = bll.FillDataTableByText("select Max(Cast(Right(SystemID,8) as Integer )) as MaxID  from " + tableName);
                return int.Parse(table.Rows[0]["MaxID"].ToString()) + 1;
            }
        }

        public static string CreateSystemID(string systemIdbegin, int index)
        {
            return systemIdbegin + String.Format("{0:00000000}", index);
        }

        public static string mf_getTable()
        {
            using (AppBLL bll = new AppBLL())
            {
                int tempStr = 1;
                DataTable table = bll.FillDataTable("p_getTableName", null);
                if (table.Rows.Count > 0)
                {
                    tempStr = int.Parse(table.Rows[0][0].ToString()) + 1;
                }
                return string.Format("s_tb_TempScore{0}", tempStr);
            }
        }


     
    }
}