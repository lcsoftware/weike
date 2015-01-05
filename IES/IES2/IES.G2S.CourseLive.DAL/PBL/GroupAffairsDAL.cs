using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Data;
using System.Data.SqlClient;
using IES.CC.Model.PBL;
using IES.DataBase;
using Dapper;

namespace IES.G2S.CourseLive.DAL.PBL
{
    /// <summary>
    /// 小组事务管理，如申请换组，审批换组信息
    /// </summary>
    public  class GroupAffairsDAL
    {
        #region  列表

        public static List<GroupAffairs> GroupAffairs_List( GroupAffairs model )
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ToGroupID", model.ToGroupID);
                    return conn.Query<GroupAffairs>("GroupAffairs_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }



        #endregion


        #region 详细信息

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


        #endregion

    }
}
