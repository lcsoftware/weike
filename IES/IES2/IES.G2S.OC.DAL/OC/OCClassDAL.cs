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
        public static List<OCClassInfo> OCClassInfo_List(int OCID, int TeamID, string Searchkey, int IsHistroy)
        {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@OCID", OCID);
                p.Add("@TeamID", TeamID);
                p.Add("@Searchkey", Searchkey);
                p.Add("@IsHistroy", IsHistroy);
                var multi = conn.QueryMultiple("OCClassInfo_List", p, commandType: CommandType.StoredProcedure);
                var occlassinfo = multi.Read<OCClassInfo>().ToList();

                return occlassinfo;

            }
        }
        /// <summary>
        /// 学生申请列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        public static List<OCClassRegStudent> OCClassRegStudent_List(int OCID)
        {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@OCID", OCID);
                var multi = conn.QueryMultiple("OCClassRegStudent_List", p, commandType: CommandType.StoredProcedure);
                var occlassinfo = multi.Read<OCClassRegStudent>().ToList();

                return occlassinfo;

            }
        }
        /// <summary>
        /// 网络注册教学班列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        public static List<OCClassRegInfo> OCClassList(OCClass model, int PageIndex, int PageSize)
        {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@OCID",model.OCID);
                p.Add("@IsHistroy", model.IsHistroy);
                p.Add("@ClassType", model.ClassType);
                p.Add("@UserID", model.UserID);
                p.Add("@PageIndex", PageIndex);
                p.Add("@PageSize", PageSize);
                var multi = conn.QueryMultiple("OCClassList", p, commandType: CommandType.StoredProcedure);
                var occlassinfo = multi.Read<OCClassRegInfo>().ToList();
                return occlassinfo;

            }
        }

        #endregion


        #region 详细信息

        #endregion


        #region  新增




        #endregion


        #region 对象更新



        #endregion


        #region 单个批量更新
        #endregion

     
    }
}
