using IES.CC.Model.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.DataBase;
using Dapper;
using System.Data;

namespace IES.G2S.CourseLive.DAL.Score
{
    /// <summary>
    /// 成绩管理
    /// 徐卫
    /// 2014年12月31日11:12:57
    /// </summary>
    public class ScoreManageInfoDAL
    {
        #region 列表
        /// <summary>
        /// 成绩管理列表
        /// </summary>
        /// <param name="smi"></param>
        /// <returns></returns>
        public static List<ScoreManageInfo> ScoreManageInfo_List(ScoreManageInfo smi, int PageIndex = 1, int PageSize = 20)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@OCID", smi.OCID);
                    p.Add("@TestName", smi.Name);
                    p.Add("@ScoreType", smi.ScoreTypeID);
                    p.Add("@PageIndex", PageIndex);
                    p.Add("@PageSize", PageSize);
                    return conn.Query<ScoreManageInfo>("ScoreManageInfo_List", p, commandType: CommandType.StoredProcedure).ToList();
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
