using Dapper;
using IES.DataBase;
using IES.Resource.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.Resource.DAL
{
    public class ResourceKenDAL
    {
        #region 新增

        /// <summary>
        /// 知识点关联新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ResourceKen ResourceKen_ADD(ResourceKen model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@KenID", model.KenID);
                    p.Add("@ResourceID", model.ResourceID);
                    p.Add("@Source", model.Source);
                    conn.Execute("ResourceKen_ADD", p, commandType: CommandType.StoredProcedure);
                    model.ID = p.Get<int>("ID");
                    return model;
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }

        /// <summary>
        /// 知识点关联删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool ResourceKen_Del(ResourceKen model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@KenID", model.KenID);
                    p.Add("@ResourceID", model.ResourceID);
                    p.Add("@Source", model.Source);
                    conn.Execute("ResourceKen_Del", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }

        }
       
        #endregion

        #region 列表

        /// <summary>
        /// 知识点章节
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IList<ResourceKen> ResourceKen_List_OCID(int ocid) 
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@OCID", ocid);
                    return conn.Query<ResourceKen>("ResourceKen_List_OCID", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }

        #endregion
    }
}
