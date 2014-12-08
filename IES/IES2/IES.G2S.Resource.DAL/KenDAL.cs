using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using IES.DataBase;
using Dapper;
using IES.Resource.Model;

namespace IES.G2S.Resource.DAL
{
    public class KenDAL
    {
        /// <summary>
        /// 知识点新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Ken Ken_ADD(Ken model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@KenID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@CreateUserID", model.CreateUserID);
                    p.Add("@OwnerUserID", model.OwnerUserID);
                    p.Add("@CourseID", model.CourseID);
                    p.Add("@OCID", model.OCID);
                    p.Add("@Name", model.Name);
                    p.Add("@Pingyin", model.Pingyin);
                    p.Add("@Requirement", model.Requirement);
                    conn.Execute("Ken_ADD", p, commandType: CommandType.StoredProcedure);
                    model.KenID = p.Get<int>("KenID");
                    return model;
                }
            }
            catch (Exception e ) 
            {
                return model;
            }

        }

        /// <summary>
        /// 知识点更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool  Ken_Upd(Ken model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@KenID", model.KenID);
                    p.Add("@UserID", model.CreateUserID);
                    p.Add("@Name", model.Name);
                    p.Add("@Pingyin", model.Pingyin);
                    p.Add("@Requirement", model.Requirement);
                    conn.Execute("Ken_Upd", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }

        }

        /// <summary>
        ///  知识点删除
        /// </summary>
        /// <param name="id"></param>
        public static bool Ken_Del(Ken model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@KenID", model.KenID);
                    conn.Execute("Ken_Del", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}
