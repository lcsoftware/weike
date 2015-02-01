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
using IES.JW.Model;



namespace IES.G2S.OC.DAL.Team
{
    public class OCTeamDAL
    {

        #region  列表
        /// <summary>
        /// 获取在线课程教学团队列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        public static List<OCTeam> OCTeam_Cache_List()
        {
            using (var conn = DbHelper.CCService())
            {
                return conn.Query<IES.CC.OC.Model.OCTeam>("OCTeam_Cache_List", null, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        /// <summary>
        /// 获取在线课程教学团队教学班列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        public static List<OCTeamClass> OCTeamClass_Cache_List()
        {
            using (var conn = DbHelper.CCService())
            {
                return conn.Query<IES.CC.OC.Model.OCTeamClass>("OCTeamClass_Cache_List", null, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        /// <summary>
        /// 获取在线课程教学团队列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        public static List<OCTeam> OCTeam_List(int OCID)
        {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@OCID", OCID);
                return conn.Query<IES.CC.OC.Model.OCTeam>("OCTeam_List", p, commandType: CommandType.StoredProcedure).ToList();
            }
        }


        public static List<OCTeamClass> OCTeamClass_List(OCTeamClass model)
        {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@OCID", model.OCID);
                p.Add("@UserID", model.UserID);
                return conn.Query<IES.CC.OC.Model.OCTeamClass>("OCTeamClass_List", p, commandType: CommandType.StoredProcedure).ToList();
            }
        }


        /// <summary>
        /// 获取在线课程中用户所属用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<OCTeam> OCTeam_OCOwner_List(int userid )
        {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@UserID", userid );
                return conn.Query<IES.CC.OC.Model.OCTeam>("OCTeam_OCOwner_List", p, commandType: CommandType.StoredProcedure).ToList();
            }
        }





        #endregion

        #region  详细信息

        public static OCTeamInfo OCTeam_Get(OCTeam model)
        {
            using (var conn = DbHelper.CCService())
            {
                OCTeamInfo octeaminfo = new OCTeamInfo();
                var p = new DynamicParameters();
                p.Add("@OCID", model.OCID);
                p.Add("@UserID", model.UserID);
                var multi = conn.QueryMultiple("OCTeamInfo_Get", p, commandType: CommandType.StoredProcedure);

                var octeam = multi.Read<OCTeam>().ToList();

                var octeamclass = multi.Read<OCTeamClass>().ToList();
                if (octeam.Count > 0)
                {
                    octeaminfo.OcTeam = octeam[0];
                }
                else
                {
                    octeaminfo.OcTeam = null;
                }
                octeaminfo.OcTeamClass = octeamclass;
                return octeaminfo;
            }


        }
        public static OCTeamInfo TeacherInfo_Get(int UserID)
        {
            using (var conn = DbHelper.CCService())
            {
                OCTeamInfo octeaminfo = new OCTeamInfo();
                var p = new DynamicParameters();
                p.Add("@UserID", UserID);
                var multi = conn.QueryMultiple("TeacherInfo_Get", p, commandType: CommandType.StoredProcedure);

                var octeam = multi.Read<OCTeam>().First();

                var octeamclass = multi.Read<OCTeamClass>().ToList();
                octeaminfo.OcTeam = octeam;
                octeaminfo.OcTeamClass = octeamclass;
                return octeaminfo;
            }


        }
        public static OcTeamFunctionInfo OCTeam_Class_Function_Get(int OCID, int UserID)
        {
            using (var conn = DbHelper.CCService())
            {
                OcTeamFunctionInfo octeamfunctioninfo = new OcTeamFunctionInfo();
                var p = new DynamicParameters();
                p.Add("@OCID", OCID);
                p.Add("@UserID", UserID);
                var multi = conn.QueryMultiple("OCTeam_Class_Function_Get", p, commandType: CommandType.StoredProcedure);
                octeamfunctioninfo.OcTeamFunctionClass = multi.Read<OcTeamFunctionClass>().ToList();
                octeamfunctioninfo.OcTeamFunctionModule = multi.Read<OcTeamFunctionModule>().ToList();
                octeamfunctioninfo.OCTeam = multi.Read<OCTeam>().Single();
                return octeamfunctioninfo;
            }
        }


        #endregion

        #region  新增

        public static OCTeam OCTeam_Class_Function_Save(OCTeam model)
        {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@OCID", model.OCID);
                p.Add("@UserID", model.UserID);
                p.Add("@OCClassIDs", model.OCTeamClassIDs);
                p.Add("@ModuleIDs", model.OCTeamModuleIDs);
                conn.Execute("OCTeam_Class_Function_Save", p, commandType: CommandType.StoredProcedure);
                return model;
            }
        }
        public static OCTeam OCTeam_ADD(OCTeam model)
        {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@TeamID", model.TeamID, DbType.Int32, ParameterDirection.InputOutput);
                p.Add("@OCID", model.OCID);
                p.Add("@UserID", model.UserID);
                p.Add("@Role", model.Role);
                p.Add("@Brief", model.Brief);
                p.Add("@Status", model.Status);
                conn.Execute("OCTeam_ADD", p, commandType: CommandType.StoredProcedure);
                model.TeamID = p.Get<int>("TeamID");
                return model;
            }
        }

        public static OCTeamInfo OCTeam_ADD(OCTeamInfo model)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region  对象更新
        public static bool OCTeam_Role_Upd(OCTeam model)
        {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@TeamID", model.TeamID);
                p.Add("@Role", model.Role);
                return Convert.ToBoolean(conn.Execute("OCTeam_Role_Upd", p, commandType: CommandType.StoredProcedure));

            }
        }

        public static bool OCTeam_Brief_Upd(OCTeam model)
        {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@TeamID", model.TeamID);
                p.Add("@Brief", model.Brief);
                return Convert.ToBoolean(conn.Execute("OCTeam_Brief_Upd", p, commandType: CommandType.StoredProcedure));

            }
        }

        public static bool OCTeam_IsLocked_Upd(OCTeam model)
        {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@OCID", model.OCID);
                p.Add("@UserID", model.UserID);
                return Convert.ToBoolean(conn.Execute("OCTeam_IsLocked_Upd", p, commandType: CommandType.StoredProcedure));

            }
        }

        public static bool OCTeam_Status_Upd(int OCID, OCTeam model)
        {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@OCID", OCID);
                p.Add("@UserID", model.TeamID);
                p.Add("@Status", model.Status);
                return Convert.ToBoolean(conn.Execute("OCTeam_Status_Upd", p, commandType: CommandType.StoredProcedure));

            }
        }
        #endregion

        #region  单个批量更新
        #endregion

        #region  属性批量操作
        #endregion

        #region  删除

        public static bool OCTeam_Del(OCTeam model)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@OCID", model.OCID);
                    p.Add("@UserID", model.UserID);
                    conn.Execute("OCTeam_Del", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool OCTeam_Del(int OCID, int UserID)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@OCID", OCID);
                    p.Add("@UserID", UserID);
                    conn.Execute("OCTeam_Del", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion



       
    }
}
