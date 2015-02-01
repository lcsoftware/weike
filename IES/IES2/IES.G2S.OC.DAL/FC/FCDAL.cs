using Dapper;
using IES.CC.OC.Model;
using IES.DataBase;
using IES.JW.Model;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.OC.DAL.FC
{
    public class FCDAL
    {
        /// <summary>
        /// 翻转课堂组合信息
        /// </summary>
        /// <param name="FCID"></param>
        /// <returns></returns>
        public static OCFCInfo OCFCInfo_Get(int FCID)
        {
            using (var conn = DbHelper.CCService())
            {
                try
                {
                    OCFCInfo ci = new OCFCInfo();
                    var p = new DynamicParameters();
                    p.Add("@FCID", FCID);
                    //return conn.Query<OCFCInfo>("OCFC_List", p, commandType: CommandType.StoredProcedure).ToList();
                    var multi = conn.QueryMultiple("OCFCInfo_Get", p, commandType: CommandType.StoredProcedure);
                    var ocfc = multi.Read<OCFC>().Single();
                    var grouplist = multi.Read<IES.CC.Model.PBL.Group>().ToList();
                    var livelist = multi.Read<OCFCLive>().ToList();
                    var fcOfflinelist = multi.Read<OCFCOffline>().ToList();

                    ci.FCLiveList = livelist;
                    ci.FCOfflineList = fcOfflinelist;
                    ci.ocfc = ocfc;
                    ci.GroupList = grouplist;
                    return ci;
                }
                catch
                {
                    return null;
                }
            }
        }



        /// <summary>
        /// 翻转课堂基础信息列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static List<OCFC> OCFC_List(int OCID, int UserID)
        {
            List<OCFC> ocfclist = new List<OCFC>();
            using (var conn = DbHelper.CCService())
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("@OCID", OCID);
                    p.Add("@UserID", UserID);
                    ocfclist = conn.Query<OCFC>("OCFC_List", p, commandType: CommandType.StoredProcedure).ToList();
                    return ocfclist;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 获取翻转课堂资料列表
        /// </summary>
        /// <param name="ocfc"></param>
        /// <returns></returns>
        public static List<OCFCFile> OCFCFile_List(OCFC ocfc)
        {
            List<OCFCFile> ocfcfilelist = new List<OCFCFile>();
            using (var conn = DbHelper.CCService())
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("@FCID", ocfc.FCID);
                    ocfcfilelist = conn.Query<OCFCFile>("OCFCFile_List", p, commandType: CommandType.StoredProcedure).ToList();
                    return ocfcfilelist;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 单个翻转课堂
        /// </summary>
        /// <param name="fc"></param>
        /// <returns></returns>
        public static OCFC OCFC_Get(OCFC fc)
        {
            OCFC ocfc = new OCFC();
            using (var conn = DbHelper.CCService())
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("@FCID", fc.FCID);
                    ocfc = conn.Query<OCFC>("OCFC_Get", p, commandType: CommandType.StoredProcedure).Single();
                    return ocfc;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 翻转课堂互动任务列表
        /// </summary>
        /// <param name="ocfc"></param>
        /// <returns></returns>
        public static List<OCFCLive> OCFCLive_List(OCFC ocfc)
        {
            using (var conn = DbHelper.CCService())
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("@FCID", ocfc.FCID);
                    return conn.Query<OCFCLive>("OCFC_Get", p, commandType: CommandType.StoredProcedure).ToList();
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 编辑翻转课堂基本信息，返回fcid
        /// </summary>
        /// <param name="ocfc"></param>
        /// <returns></returns>
        public static int OCFC_Edit(OCFC ocfc)
        {

            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@FCID", ocfc.FCID, DbType.Int32, ParameterDirection.InputOutput);
                p.Add("@OCID", ocfc.OCID);
                p.Add("@UserID", ocfc.UserID);
                p.Add("@StartDate", ocfc.StartDate);
                p.Add("@EndDate", ocfc.EndDate);
                p.Add("@UpdateTime", ocfc.UpdateTime);
                p.Add("@Brief", ocfc.Brief);
                p.Add("@GroupModeID", ocfc.GroupModeID);

                conn.Execute("OCFC_Edit", p, commandType: CommandType.StoredProcedure);

                return p.Get<int>("FCID");
            }
            
        }

        /// <summary>
        /// 新增翻转课堂基本信息，返回fcid
        /// </summary>
        /// <param name="ocfc"></param>
        /// <returns></returns>
        public static int OCFC_ADD(OCFC ocfc)
        {

            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@FCID", ocfc.FCID, DbType.Int32, ParameterDirection.InputOutput);
                p.Add("@OCID", ocfc.OCID);
                p.Add("@UserID", ocfc.UserID);
                p.Add("@StartDate", ocfc.StartDate);
                p.Add("@EndDate", ocfc.EndDate);
                p.Add("@UpdateTime", ocfc.UpdateTime);
                p.Add("@Brief", ocfc.Brief);
                p.Add("@GroupModeID", ocfc.GroupModeID);

                conn.Execute("OCFC_Add", p, commandType: CommandType.StoredProcedure);

                return p.Get<int>("FCID");
            }

        }

        /// <summary>
        /// 删除资料
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool OCFCFile_Del(OCFCFile file)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@FCFileID", file.FCFileID);
                    conn.Execute("OCFCFile_Del", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 删除互动
        /// </summary>
        /// <param name="live"></param>
        /// <returns></returns>
        public static bool OCFCLive_Del(OCFCLive live)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@FCLiveID", live.FCLiveID);
                    conn.Execute("OCFCLive_Del", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch 
            {
                return false;
            }
        }

        public static bool OCFCFile_Must(OCFCFile file)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@FCID", file.FCFileID);
                    conn.Execute("OCFCLive_Del", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch 
            {
                return false;
            }
        }

        
    }
}
