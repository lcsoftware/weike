using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using IES.DataBase;
using Dapper;
using IES.JW.Model;

namespace IES.G2S.JW.DAL
{
    public class SpecialtyTypeDAL
    {

        #region 列表
        /// <summary>
        /// 学科列表
        /// </summary>
        public static List<SpecialtyType> SpecialtyType_Tree_List()
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    return conn.Query<SpecialtyType>("SpecialtyType_Tree", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 编辑学科
        /// </summary>
        public static SpecialtyType SpecialtyType_Edit(SpecialtyType model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@SpecialtyTypeID",model.SpecialtyTypeID);
                    p.Add("@SpecialtyTypeNo",model.SpecialtyTypeNo);
                    p.Add("@SpecialtyTypeName",model.SpecialtyTypeName);
                    p.Add("@ParentID",model.ParentID);
                    p.Add("@op_SpecialtyTypeID", "", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@output", "", dbType: DbType.String, direction: ParameterDirection.Output);
                    conn.Execute("SpecialtyType_Edit", p, commandType: CommandType.StoredProcedure);
                    model.output = p.Get<string>("@output");
                    return model;
                }
            }
            catch (Exception e)
            {
                return new SpecialtyType();
            }
        }
        #endregion

        #region 上级学科
        public static List<SpecialtyType> SpecialtyType_P_List()
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    return conn.Query<SpecialtyType>("SpecialtyType_P_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception)
            {
                return new List<SpecialtyType>();
            }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除学科
        /// </summary>
        public static bool SpecialtyType_Del(SpecialtyType model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@SpecialtyTypeID",model.SpecialtyTypeID);
                    conn.Execute("SpecialtyType_Del", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region 取消删除
        /// <summary>
        /// 删除学科
        /// </summary>
        public static bool SpecialtyType_CancelDel(SpecialtyType model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@SpecialtyTypeID", model.SpecialtyTypeID);
                    conn.Execute("SpecialtyType_CancelDel", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

    }
}
