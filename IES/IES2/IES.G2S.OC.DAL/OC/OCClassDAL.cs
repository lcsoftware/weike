using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

using IES.CC.OC.Model;
using Dapper;
using IES.DataBase;

namespace IES.G2S.OC.DAL.OC
{
    public class OCClassDAL
    {
        #region  列表

        /// <summary>
        /// 教学班列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <param name="TeamID"></param>
        /// <param name="Searchkey"></param>
        /// <param name="IsHistroy"></param>
        /// <returns></returns>
        public static List<OCClass> OCClass_List(OCClass model, int PageIndex, int PageSize)
        {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@key", model.Key);
                p.Add("@OCID", model.OCID);
                p.Add("@IsHistroy", model.IsHistroy);
                p.Add("@PageIndex", PageIndex);
                p.Add("@PageSize", PageSize);
                var multi = conn.QueryMultiple("OCClass_List", p, commandType: CommandType.StoredProcedure);
                var occlassinfo = multi.Read<OCClass>().ToList();

                return occlassinfo;

            }
        }

        /// <summary>
        /// 获取编辑教学班学生信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<OCClassStudent> OCClassStudent_List(OCClass model, int PageIndex, int PageSize)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@OCClassID", model.OCClassID);
                    p.Add("@PageIndex", PageIndex);
                    p.Add("@PageSize", PageSize);
                    return conn.Query<OCClassStudent>("OCClassStudent_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<OCClassStudent>();
            }
        }


        /// <summary>
        /// 教学班添加学生搜索
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<OCClassStudent> OCClass_Student_List(OCClassStudent model, int PageIndex, int PageSize)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@key", model.Key);
                    p.Add("@OrganizationID", "-1");
                    p.Add("@SpecialtyID", "-1");
                    p.Add("@ClassID", "-1");
                    p.Add("@IsRegister", "-1");
                    p.Add("@StudentIDs", model.StudentIDs);
                    p.Add("@PageIndex", PageIndex);
                    p.Add("@PageSize", PageSize);
                    return conn.Query<OCClassStudent>("Student_Search", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<OCClassStudent>();
            }
        }

        /// <summary>
        /// 获取行政班列表
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="OrganizationID"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static List<IES.JW.Model.Class> Class_List(IES.JW.Model.Class model, int PageIndex, int PageSize)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@Key", model.Key);
                    p.Add("@OrganizationID", "-1");
                    p.Add("@StartTime", "1900-01-01");
                    p.Add("@EndTime", "9999-01-01");
                    p.Add("@PageSize", PageSize);
                    p.Add("@PageIndex", PageIndex);
                    return conn.Query<IES.JW.Model.Class>("Class_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<IES.JW.Model.Class>();
            }
        }

        /// <summary>
        ///通过行政班获取学生信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static List<OCClassStudent> ClassStudent_List(IES.JW.Model.Class model, int PageIndex, int PageSize)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ClassID", model.ClassID);
                    p.Add("@StudentIDs", "");
                    p.Add("@PageIndex", PageIndex);
                    p.Add("@PageSize", PageSize);
                    return conn.Query<OCClassStudent>("ClassStudent_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<OCClassStudent>();
            }
        }
       
        /// <summary>
        /// 获取授课教师列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<OCTeamDropdownList> OCTeam_Dropdown_List(OCTeamDropdownList model)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@OCID", model.OCID);
                    p.Add("@OCClassID", model.OCClassID);
                    p.Add("@Role", model.Role);
                    return conn.Query<OCTeamDropdownList>("OCTeam_Dropdown_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<OCTeamDropdownList>();
            }
        }


        /// <summary>
        /// 获取自己相关的教学班列表
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="OCID"></param>
        /// <returns></returns>
        public static List<IES.JW.Model.TeachingClass> TeachingClass_Owner_List(int UserID, int OCID)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@UserID", UserID);
                    p.Add("@OCID", OCID);
                    return conn.Query<IES.JW.Model.TeachingClass>("TeachingClass_Owner_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<IES.JW.Model.TeachingClass>();
            }
        }


        #endregion


        #region 详细信息
        /// <summary>
        /// 获取教学班基本信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static OCClass OCClass_Get(OCClass model)
        {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@OCClassID", model.OCClassID);
                return conn.Query<OCClass>("OCClass_Get", p, commandType: CommandType.StoredProcedure).First();

            }
        }

        #endregion


        #region  新增
        /// <summary>
        /// 添加教学班
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static OCClass OCClass_Edit(OCClass model)
        {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@OCClassID", model.OCClassID);
                //p.Add("@TeachingClassID", model.TeachingClassID, DbType.Int32, ParameterDirection.InputOutput);
                p.Add("@OCID", model.OCID);
                p.Add("@TeachingClassName", model.TeachingClassName);
                p.Add("@RegNum", model.RegNum);
                p.Add("@RegStatus", model.RegStatus);
                p.Add("@StartDate", model.StartDate);
                p.Add("@EndDate", model.EndDate);
                p.Add("@TeacherID", model.TeacherID);
                p.Add("@TeacherIDS", model.TeacherIDS);
                p.Add("@StudentIDS", model.StudentIDS);
                p.Add("@op_OCClassID", model.OCClassID, DbType.Int32, ParameterDirection.InputOutput);
                p.Add("@op_TeachingClassID", model.TeachingClassID, DbType.Int32, ParameterDirection.InputOutput);
                conn.Execute("OCClass_Edit", p, commandType: CommandType.StoredProcedure);
                model.OCClassID = p.Get<int>("op_OCClassID");
                model.TeachingClassID = p.Get<int>("op_TeachingClassID");
                return model;
            }
        }



        #endregion


        #region 对象更新
        /// <summary>
        /// 修改网络教学班结业状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool OCClass_IsHistroy_Upd(OCClass model)
        {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@OCClassID", model.OCClassID);
                return Convert.ToBoolean(conn.Execute("OCClass_IsHistroy_Upd", p, commandType: CommandType.StoredProcedure));
            }
        }
        /// <summary>
        /// 修改注册码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool OCClass_RegNum_Upd(OCClass model)
        {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@OCClassID", model.OCClassID);
                p.Add("@RegNum", model.RegNum);
                p.Add("@RegStatus", model.RegStatus);
                return Convert.ToBoolean(conn.Execute("OCClass_RegNum_Upd", p, commandType: CommandType.StoredProcedure));

            }
        }
        /// <summary>
        /// 设置网络教学班招生状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool OCClass_RecruitStatus_Upd(OCClass model)
        {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@OCClassID", model.OCClassID);
                return Convert.ToBoolean(conn.Execute("OCClass_RecruitStatus_Upd", p, commandType: CommandType.StoredProcedure));

            }
        }

        #endregion


        #region 单个批量更新

        #endregion

        #region 删除
        /// <summary>
        ///删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool OCClass_Del(OCClass model)
        {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@OCClassID", model.OCClassID);
                return Convert.ToBoolean(conn.Execute("OCClass_Del", p, commandType: CommandType.StoredProcedure));
            }
        }

        #endregion

    }
}
