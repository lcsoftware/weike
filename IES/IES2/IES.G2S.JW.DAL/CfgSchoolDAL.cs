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
    public  class CfgSchoolDAL
    {

        public static List<CfgSchool> CfgSchool_List()
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    return conn.Query<CfgSchool>("CfgSchool_List", null, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<CfgSchool>();
            }
        }


        #region 修改教师或学生的储存空间
        public static bool CfgSchoolSpace_Upd(CfgSchool model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@Space", model.TeacherSpace);
                    p.Add("@UserType", model.UserType);
                    conn.Execute("CfgSchool_Space_Edit", p, commandType: CommandType.StoredProcedure);
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
