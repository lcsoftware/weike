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
    public class OCDefaultColumnDAL
    {

        #region 列表
        /// <summary>
        /// 获取列表
        /// </summary>
        public static List<OCDefaultColumn> OCDefaultColumn_List()
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();                   
                    return conn.Query<OCDefaultColumn>("OCDefaultColumn_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<OCDefaultColumn>();
            }
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增一条数据
        /// </summary>
        public static OCDefaultColumn OCDefaultColumn_ADD(OCDefaultColumn model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@Name",model.Name);
                    conn.Execute("OCDefaultColumn_ADD", p, commandType: CommandType.StoredProcedure);
                    return model;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region 栏目上移下移
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool OCDefaultColumn_Edit(OCDefaultColumn model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ColumID",model.ColumID);
                    p.Add("@Orde",model.Orde);
                    p.Add("@topbm", model.topbm);
                    conn.Execute("OCDefaultColumn_Edit", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool OCDefaultColumn_Del(OCDefaultColumn model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ColumID",model.ColumID);
                    p.Add("@Orde", model.Orde);
                    conn.Execute("OCDefaultColumn_Del", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region 重命名
        /// <summary>
        /// 
        /// </summary>
        public static bool OCDefaultColumn_ReName(OCDefaultColumn model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ColumID", model.ColumID);
                    p.Add("@Name", model.Name);
                    conn.Execute("OCDefaultColumn_ReName", p, commandType: CommandType.StoredProcedure);
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
