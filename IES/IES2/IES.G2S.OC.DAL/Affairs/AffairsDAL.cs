using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using IES.CC.Model;
using IES.DataBase;
using IES.CC.OC.Model;
using IES.CC.Affairs.Model;
using IES.JW.Model;
namespace IES.G2S.OC.DAL
{
    public class AffairsDAL
    {


        #region  列表
        /// <summary>
        /// 获取申请审核列表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static List<OCAffairs> Affairs_List(OCAffairs model, int PageIndex, int PageSize)
        {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@OCID", model.OCID);
                p.Add("@DictID", model.DictID);
                p.Add("@PageIndex", PageIndex);
                p.Add("@PageSize", PageSize);
                return conn.Query<OCAffairs>("OCAffairs_List", p, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        /// <summary>
        /// 获取系统字典列表审核列表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static List<Dict> Dict_List(Dict model)
        {
            using (var conn = DbHelper.JWService())
            {
                var p = new DynamicParameters();
                p.Add("@Source", model.Source);
                return conn.Query<Dict>("Dict_List", p, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        #endregion


        #region 详细信息


        #endregion


        #region  新增




        #endregion


        #region 对象更新

        /// <summary>
        /// 审核授权操作 同意 拒绝
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool OCAffairs_Status_Upd(OCAffairs model)
        {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@AffairID", model.AffairID);
                p.Add("@AffairIDs", "");
                p.Add("@Status", model.Status);
                return Convert.ToBoolean(conn.Execute("OCAffairs_Status_Upd", p, commandType: CommandType.StoredProcedure));
            }
        }

        #endregion


        #region 单个批量更新

        #endregion


        #region 属性批量操作


        /// <summary>
        /// 审核授权操作 同意 拒绝
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool OCAffairs_Beach_Upd(OCAffairs model)
        {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@AffairID", "-1");
                p.Add("@AffairIDs", model.AffairIDs);
                p.Add("@Status", model.Status);
                return Convert.ToBoolean(conn.Execute("OCAffairs_Status_Upd", p, commandType: CommandType.StoredProcedure));
            }
        }


        #endregion

        #region 删除

        /// <summary>
        /// 删除申请授权
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Affairs_Del(OCAffairs model)
        {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@AffairsIDs", model.AffairIDs);
                return Convert.ToBoolean(conn.Execute("", p, commandType: CommandType.StoredProcedure));
            }
        }
        #endregion
    }
}
