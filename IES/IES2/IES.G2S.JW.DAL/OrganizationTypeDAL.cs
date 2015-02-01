using IES.DataBase;
using IES.JW.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace IES.G2S.JW.DAL
{
    /// <summary>
    /// 组织机构类别
    /// </summary>
    public class OrganizationTypeDAL
    {

        /// <summary>
        /// 组织机构类别列表
        /// </summary>
        /// <returns></returns>
        public static List<OrganizationType> OrganizationType_List() {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    return conn.Query<OrganizationType>("OrganizationType_List", null, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception )
            {
                return null;
            }
        }
    }
}
