using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using IES.JW.Model;
using Dapper;
using IES.DataBase;

namespace IES.G2S.JW.DAL
{
    /// <summary>
    /// TeachingClass ,TeachingClassStudent , TeachingClassTeacher
    /// </summary>
    public  class TeachingClassDAL
    {

        #region  列表


        #endregion

        #region 详细信息


        public static TeachingClassInfo TeachingClassInfo_Get(TeachingClass model)
        {

            try
            {
                using (IDbConnection conn = DbHelper.ResourceService())
                {
                    TeachingClassInfo tc = new TeachingClassInfo();
                    var p = new DynamicParameters();
                    p.Add("@TeachingClassID", model.TeachingClassID);

                    var multi = conn.QueryMultiple("TeachingClassInfo_Get", p, commandType: CommandType.StoredProcedure);

                    var teachingclass = multi.Read<TeachingClass>().Single();
                    var teachingclassstudentlist = multi.Read<TeachingClassStudent>().ToList();
                    var teachingclassteacherlist = multi.Read<TeachingClassTeacher>().ToList();

                    tc.teachingclass = teachingclass ;
                    tc.teachingclassstudentlist = teachingclassstudentlist ;
                    tc.teachingclassteacherlist = teachingclassteacherlist;
  
                    return tc ;
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }


        #endregion

        #region  新增




        #endregion

        #region 对象更新



        #endregion

        #region 单个批量更新





        #endregion

        #region 属性批量操作





        #endregion

        #region 删除

        public static bool TeachingClass_Del(TeachingClass model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@TeachingClassID", model.TeachingClassID);
                    conn.Execute("TeachingClass_Del", p, commandType: CommandType.StoredProcedure);
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
