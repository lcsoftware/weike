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



namespace IES.G2S.OC.DAL.Team
{
    public class OCTeamDAL
    {

        #region  列表

        public static List<OCTeamInfo> OCTeam_Get(int OCID)
        {
            using (var conn = DbHelper.CCService())
            {
                List<OCTeamInfo> octeaminfo = new List<OCTeamInfo>();


                var p = new DynamicParameters();
                p.Add("@OCID", OCID);
                var multi = conn.QueryMultiple("OCTeamInfo_List", p, commandType: CommandType.StoredProcedure);
                var octeamall = multi.Read<OCTeam>().ToList();
                var octeamclassall = multi.Read<OCTeamClass>().ToList();
                //var octeamclassinfoall = multi.Read<OCTeamClassInfo>().ToList();
                foreach (var team in octeamall)
                {
                    //var 
                    var octeamclasstemp = octeamclassall.Where(ins => team.UserID.Equals(ins.UserID));
                    OCTeamInfo octf = new OCTeamInfo();
                    octf.OcTeam = team;
                    octf.OcTeamClass = octeamclasstemp.ToList();
                    //octf.octeamclassinfo = octeamclassinfoall;
                    octeaminfo.Add(octf);
                }
                return octeaminfo;

            }
        }
        #endregion
        #region  详细信息

        public static OCTeamInfo OCTeam_Get(int OCID, int UserID)
        {
            using (var conn = DbHelper.CCService())
            {
                OCTeamInfo octeaminfo = new OCTeamInfo();
                var p = new DynamicParameters();
                p.Add("@OCID", OCID);
                p.Add("@UserID", UserID);
                var multi = conn.QueryMultiple("OCTeamInfo_Get", p, commandType: CommandType.StoredProcedure);
                var octeam = multi.Read<OCTeam>().Single();
                var octeamclass = multi.Read<OCTeamClass>().ToList();
               // var octeamclassinf = multi.Read<OCTeamClassInfo>().ToList();
                octeaminfo.OcTeam = octeam;
                octeaminfo.OcTeamClass = octeamclass;
                //octeaminfo.octeamclassinfo = octeamclassinf;
                //return conn.Query<OCTeamInfo>("OCTeamInfo_Get", p, commandType: CommandType.StoredProcedure).ToList()[0];
                return octeaminfo;
            }
        }
        #endregion
        #region  新增
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
                p.Add("@ApplyDate", model.ApplyDate);
                p.Add("@IsLocked", model.IsLocked);

                conn.Execute("OCTeam_ADD", p, commandType: CommandType.StoredProcedure);
                model.TeamID=p.Get<int>("TeamID");
                return model;
            }
        }

        public static OCTeamInfo OCTeam_ADD(OCTeamInfo model)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region  对象更新
        public static bool OCTeam_Role_Upd(int TeamID, int Role)
        {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@TeamID", TeamID);
                p.Add("@Role", Role);
                return Convert.ToBoolean(conn.Execute("OCTeam_Role_Upd", p));

            }
        }

        public static bool OCTeam_Brief_Upd(int TeamID, int Brief)
        {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@TeamID", TeamID);
                p.Add("@Brief", Brief);
                return Convert.ToBoolean(conn.Execute("OCTeam_Brief_Upd", p));

            }
        }
        public static bool OCTeam_Status_Upd(int TeamID, int Status)
        {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@TeamID", TeamID);
                p.Add("@Status", Status);
                return Convert.ToBoolean(conn.Execute("OCTeam_Status_Upd", p));

            }
        }
        public static bool OCTeam_Upd(OCTeamInfo model)
        {
            throw new NotImplementedException();
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
                    p.Add("@TeamID", model.TeamID);
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
