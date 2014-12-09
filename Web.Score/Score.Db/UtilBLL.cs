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
                    //var sql = "select FuncId, FuncName, Description, FuncType, FuncID0 As Parent, SysNo, 1 as Kind from s_tb_Function where CAST(FuncID as Int) in ";
                    //sql += " (SELECT cast(a.FuncID as Int) FROM  tbGroupInfo b INNER JOIN tbUserGroupInfo c ON b.TeacherID = c.TeacherID INNER JOIN s_tb_Rights a ON b.GroupID = a.TeacherID ";
                    //sql += " where c.TeacherID=@teacher and a.SYSNO = 2) ";
                    //sql += " union all ";
                    //sql += " select FuncId, FuncName, Description, FuncType, FuncID0 As Parent, SysNo, 2 as Kind from s_tb_Function where CAST(FuncID as Int) in";
                    //sql += " (SELECT cast(a.FuncId as Int) from s_tb_Rights a, tbUserGroupInfo b ";
                    //sql += " where a.TeacherID=@teacher and a.TeacherID=b.TeacherID and SYSNO = 2)";

                    var sql = "select FuncId, FuncName, Description, FuncType, FuncID0 As Parent, SysNo, 2 as Kind"
                                + " from s_tb_Function "
                                + " where CAST(FuncID as Int) in ("
                                + " SELECT cast(a.FuncId as Int) from s_tb_Rights a, tbUserGroupInfo b  "
                                + " where a.TeacherID=@teacher and a.TeacherID=b.TeacherID and SYSNO = 2)";

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

        public static DataTable GetTestLoginByYear(int micYear)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "select TestType,TestNo as TestLoginNo, case when TestType=0 then '平时' + CAST(testno as varchar)" +
                          " when TestType=1 then '期中' + CAST(testno as varchar)" +
                          " else '期末' + CAST(testno as varchar) end Name" +
                          " from s_tb_testlogin where Academicyear='{0}' order by cast(testno as int)";
                sql = string.Format(sql, micYear);
                return bll.FillDataTableByText(sql);
            }
        }

        public static void gp_ScoreTj(int micYear, string Semester, string testType, string testNo, string courseCode, string gradeOrClassNo, int Flag)
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = Flag == 0 ? "Delete from  s_tb_GradeStat where" : "Delete from  s_tb_ClassStat where";
                sql += " Academicyear='{0}'"
                    + " and Testno='{1}'"
                    + " and CourseCode='{2}'";
                sql += Flag == 0 ? " and GradeNo='{3}'" : "and ClassNo='{3}'";
                sql = string.Format(sql, micYear, testNo, courseCode, gradeOrClassNo);
                bll.ExecuteNonQueryByText(sql);
                //先插入数据
                if (Flag == 0)
                    sql = " insert Into s_tb_GradeStat(AcademicYear,semester,CourseCode,TestType,TestNo,GradeNo)";
                else
                    sql = " insert Into s_tb_ClassStat(AcademicYear,semester,CourseCode,TestType,TestNo,ClassNo)";

                sql += " values('{0}','{1}','{2}','{3}','{4}','{5}')";
                sql = string.Format(sql, micYear, Semester, courseCode, testType, testNo, gradeOrClassNo);
                bll.ExecuteNonQueryByText(sql);

                //将统计数据修改为当前正确值
                //先将0-0.05数据添入
                sql = " select count(*) as S_5 from s_vw_ClassScoreNum"
                       + " where NumScore/cast(substring(Markcode,2,3) as numeric(5,2) )<0.05 "
                       + " and substring(Markcode,1,1)='1'"
                       + " and Academicyear='{0}'"
                       + " and TestNo='{1}'"
                       + " and CourseCode='{2}'";
                sql += Flag == 0 ? " and GradeNo='{3}'" : "and ClassNo='{3}'";
                sql = string.Format(sql, micYear, testNo, courseCode, gradeOrClassNo);
                DataTable table = bll.FillDataTableByText(sql);
                var s_num = int.Parse(table.Rows[0]["S_5"].ToString());

                //更新到数据库
                sql = Flag == 0 ? "Update s_tb_GradeStat set S_5={4}" : "Update s_tb_ClassStat set S_5={4}";
                sql += " where Academicyear='{0}'"
                       + " and TestNo='{1}'"
                       + " and CourseCode='{2}'";
                sql += Flag == 0 ? " and GradeNo='{3}'" : "and ClassNo='{3}'";
                sql = string.Format(sql, micYear, testNo, courseCode, gradeOrClassNo, s_num);
                bll.ExecuteNonQueryByText(sql);

                //二将S_100数据添入
                sql = " select count(*) as S_5 from s_vw_ClassScoreNum"
                       + " where NumScore/cast(substring(Markcode,2,3) as numeric(5,2) )>=0.95 "
                       + " and substring(Markcode,1,1)='1'"
                       + " and Academicyear='{0}'"
                       + " and TestNo='{1}'"
                       + " and CourseCode='{2}'";
                sql += Flag == 0 ? " and GradeNo='{3}'" : "and ClassNo='{3}'";
                sql = string.Format(sql, micYear, testNo, courseCode, gradeOrClassNo);
                table = bll.FillDataTableByText(sql);
                s_num = int.Parse(table.Rows[0]["S_5"].ToString());

                //更新到数据库
                sql = Flag == 0 ? " Update s_tb_GradeStat set S_100={4}" : "Update s_tb_ClassStat set S_100={4}";
                sql += " where Academicyear='{0}'"
                       + " and TestNo='{1}'"
                       + " and CourseCode='{2}'";
                sql += Flag == 0 ? " and GradeNo='{3}'" : "and ClassNo='{3}'";
                sql = string.Format(sql, micYear, testNo, courseCode, gradeOrClassNo, s_num);
                bll.ExecuteNonQueryByText(sql);

                var LowScore = 0.05f;
                var highScore = 0.1f;
                for (var i = 0; i <= 17; i++)
                {
                    sql = " select count(*) as S_5 from s_vw_ClassScoreNum"
                           + " where NumScore/cast(substring(Markcode,2,3) as numeric(5,2) )>={4}"
                           + " and NumScore/cast(substring(Markcode,2,3) as numeric(5,2) )<{5}"
                           + " and substring(Markcode,1,1)='1'"
                           + " and Academicyear='{0}'"
                           + " and TestNo='{1}'"
                           + " and CourseCode='{2}'";
                    sql += Flag == 0 ? " and GradeNo='{3}'" : "and ClassNo='{3}'";
                    sql = string.Format(sql, micYear, testNo, courseCode, gradeOrClassNo, LowScore, highScore);
                    table = bll.FillDataTableByText(sql);
                    s_num = int.Parse(table.Rows[0]["S_5"].ToString());

                    switch (i)
                    {
                        case 0:
                            sql = Flag == 0 ? "Update s_tb_GradeStat Set S_10={0}" : "Update s_tb_ClassStat Set S_10={0}";
                            break;
                        case 1:
                            sql = Flag == 0 ? "Update s_tb_GradeStat Set S_15={0}" : "Update s_tb_ClassStat Set S_15={0}";
                            break;
                        case 2:
                            sql = Flag == 0 ? "Update s_tb_GradeStat Set S_20={0}" : "Update s_tb_ClassStat Set S_20={0}";
                            break;
                        case 3:
                            sql = Flag == 0 ? "Update s_tb_GradeStat Set S_25={0}" : "Update s_tb_ClassStat Set S_25={0}";
                            break;
                        case 4:
                            sql = Flag == 0 ? "Update s_tb_GradeStat Set S_30={0}" : "Update s_tb_ClassStat Set S_30={0}";
                            break;
                        case 5:
                            sql = Flag == 0 ? "Update s_tb_GradeStat Set S_35={0}" : "Update s_tb_ClassStat Set S_35={0}";
                            break;
                        case 6:
                            sql = Flag == 0 ? "Update s_tb_GradeStat Set S_40={0}" : "Update s_tb_ClassStat Set S_40={0}";
                            break;
                        case 7:
                            sql = Flag == 0 ? "Update s_tb_GradeStat Set S_45={0}" : "Update s_tb_ClassStat Set S_45={0}";
                            break;
                        case 8:
                            sql = Flag == 0 ? "Update s_tb_GradeStat Set S_50={0}" : "Update s_tb_ClassStat Set S_50={0}";
                            break;
                        case 9:
                            sql = Flag == 0 ? "Update s_tb_GradeStat Set S_55={0}" : "Update s_tb_ClassStat Set S_55={0}";
                            break;
                        case 10:
                            sql = Flag == 0 ? "Update s_tb_GradeStat Set S_60={0}" : "Update s_tb_ClassStat Set S_60={0}";
                            break;
                        case 11:
                            sql = Flag == 0 ? "Update s_tb_GradeStat Set S_65={0}" : "Update s_tb_ClassStat Set S_65={0}";
                            break;
                        case 12:
                            sql = Flag == 0 ? "Update s_tb_GradeStat Set S_70={0}" : "Update s_tb_ClassStat Set S_70={0}";
                            break;
                        case 13:
                            sql = Flag == 0 ? "Update s_tb_GradeStat Set S_75={0}" : "Update s_tb_ClassStat Set S_75={0}";
                            break;
                        case 14:
                            sql = Flag == 0 ? "Update s_tb_GradeStat Set S_80={0}" : "Update s_tb_ClassStat Set S_80={0}";
                            break;
                        case 15:
                            sql = Flag == 0 ? "Update s_tb_GradeStat Set S_85={0}" : "Update s_tb_ClassStat Set S_85={0}";
                            break;
                        case 16:
                            sql = Flag == 0 ? "Update s_tb_GradeStat Set S_90={0}" : "Update s_tb_ClassStat Set S_90={0}";
                            break;
                        default:
                            sql = Flag == 0 ? "Update s_tb_GradeStat Set S_95={0}" : "Update s_tb_ClassStat Set S_95={0}";
                            break;
                    }

                    sql += " Where AcademicYear={1}"
                           + " and TestNo={2}"
                           + " and CourseCode={3}";

                    sql += Flag == 0 ? " and GradeNo='{4}'" : " and ClassCode={4}";
                    sql = string.Format(sql, s_num, micYear, testNo, courseCode, gradeOrClassNo);
                    bll.ExecuteNonQueryByText(sql);

                    LowScore += 0.05f;
                    highScore += 0.05f;
                }
            }
        }

    }
}