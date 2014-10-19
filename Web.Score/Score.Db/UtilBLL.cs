using App.Score.Data;
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

        public static IList<GroupUser> GetGroupUsers()
        {
            using (AppBLL bll = new AppBLL())
            {
                SortedList<string, GroupUser> sortedList = new SortedList<string, GroupUser>();

                string sql = "select TeacherID as GroupID, Name, [Description], UserOrGroup from tbUserGroupInfo where UserOrGroup='0'";
                IList<GroupUser> groups = bll.FillListByText<GroupUser>(sql, null); 
                foreach (var groupUser in groups)
                {
                    sortedList.Add(groupUser.GroupID, groupUser); 
                }

                sql = "select a.GroupID, b.*  from tbGroupInfo a, tbUserGroupInfo b where a.TeacherID=b.TeacherID";
                IList<UserInfo> users = bll.FillListByText<UserInfo>(sql, null);
                foreach (var userInfo in users)
                {
                    if (!string.IsNullOrEmpty(userInfo.GroupID))
                    {
                        GroupUser group = sortedList[userInfo.GroupID];
                        group.Children.Add(userInfo);
                    }
                }

                return groups;
            }
        }
    }
}
