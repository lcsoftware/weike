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
using IES.CC.Model.OC;

namespace IES.G2S.OC.DAL
{
   public  class OCTemplateDAL
    {
       /// <summary>
        /// 获取模板列表
       /// </summary>
       /// <returns></returns>
       public static List<OCTemplate> OCTemplate_List() {
           using (var conn = DbHelper.CCService())
           {
               var p = new DynamicParameters();
               return conn.Query<OCTemplate>("OCTemplate_List", p, commandType: CommandType.StoredProcedure).ToList();


           }
       }
    }
}
