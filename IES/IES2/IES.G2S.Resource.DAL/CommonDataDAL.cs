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
    public class CommonDataDAL
    {
        #region 列表
        public static List<ResourceDict> ResourceDict_List()
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    return conn.Query<ResourceDict>("ResourceDict_List", null, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return  null ;
            }

        }

        #endregion 
    }
}
