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
    public class KeyDAL
    {
        #region  列表

        /// <summary>
        /// 获取标签列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<Key> Key_List(Key model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@OCID", model.OCID);

                    return conn.Query<Key>("Key_List", p, commandType: CommandType.StoredProcedure).ToList();
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
