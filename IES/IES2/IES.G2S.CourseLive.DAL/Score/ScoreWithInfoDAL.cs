using Dapper;
using IES.CC.Model.Score;
using IES.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.CourseLive.DAL.Score
{
    /// <summary>
    /// 徐卫
    /// 2014年12月31日18:22:08
    /// 单个成绩详细信息
    /// </summary>
    public class ScoreWithInfoDAL
    {
        #region 成绩详细列表
        /// <summary>
        /// 徐卫
        /// 2014年12月31日18:23:38
        /// 学生成绩详细列表
        /// </summary>
        /// <param name="swi"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static List<ScoreWithInfo> ScoreWithInfo_List(ScoreWithInfo swi, int PageIndex = 1, int PageSize = 20)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@TestID", swi.TestID);
                    p.Add("@UserName", swi.UserName);
                    p.Add("@ClassID", swi.ClassID);
                    p.Add("@TeachingClassID", swi.TeachingClassID);
                    p.Add("@PageIndex", PageIndex);
                    p.Add("@PageSize", PageSize);
                    return conn.Query<ScoreWithInfo>("ScoreWithInfo_List", p, commandType: CommandType.StoredProcedure).ToList();
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
