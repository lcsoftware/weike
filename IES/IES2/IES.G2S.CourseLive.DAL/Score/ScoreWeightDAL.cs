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
    /// 徐卫
    /// 成绩类别加权
    /// </summary>
    public class ScoreWeightDAL
    {
        #region 列表
        /// <summary>
        /// 成绩权重类别列表
        /// </summary>
        /// <param name="teachingClassID"></param>
        /// <returns></returns>
        public static List<ScoreWeight> ScoreWeight_List(int OCID, int? teachingClassID)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@OCID", OCID);
                    p.Add("@TeachingClassID", teachingClassID);
                    return conn.Query<ScoreWeight>("ScoreWeight_List", p, commandType: CommandType.StoredProcedure).ToList(); ;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region 属性更新
        /// <summary>
        /// 更改成绩类别的权重系数
        /// </summary>
        /// <param name="weightID"></param>
        /// <returns></returns>
        public static bool ScoreWeight_Power_Upd(ScoreWeight sw)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@WeightID", sw.WeightID);
                    p.Add("@Power", sw.Power);
                    conn.Execute("ScoreWeight_Power_Upd", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 更改成绩类别加权参与次数
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static bool ScoreWeight_JoinNum_Upd(ScoreWeight sw)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@WeightID", sw.WeightID);
                    p.Add("@JoinNum", sw.JoinNum);
                    conn.Execute("ScoreWeight_JoinNum_Upd", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

    }
}
