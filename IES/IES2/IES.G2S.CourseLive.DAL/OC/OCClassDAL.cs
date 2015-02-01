using IES.CC.OC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.DataBase;
using Dapper;
using System.Data;

namespace IES.G2S.CourseLive.DAL.OC
{
    /// <summary>
    /// xuwei
    /// 2015年1月10日11:53:37
    /// 
    /// </summary>
    public class OCClassDAL
    {
        #region 列表
        /// <summary>
        /// 网络教学班下拉列表
        /// 2015年1月10日11:55:42
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        public static List<OCClass> OCClass_Dropdown_List(int OCID)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@OCID", OCID);
                    return conn.Query<OCClass>("OCClass_Dropdown_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception)
            {

                return null;
            }
        } 
        #endregion
    }
}
