using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.CC.Model.Score;
using IES.DataBase;
using Dapper;
using System.Data;

namespace IES.G2S.CourseLive.DAL.Score
{
    /// <summary>
    /// xuwei
    /// 2014年12月25日
    /// </summary>
    public class ScoreTypeDAL
    {
        #region  列表
        /// <summary>
        /// 成绩类别列表
        /// </summary>
        /// <param name="scoreType"></param>
        /// <returns></returns>
        public static List<ScoreType> ScoreType_List(int? OCID)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@OCID", OCID);
                    return conn.Query<ScoreType>("ScoreType_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region  新增
        /// <summary>
        /// 新增成绩类别
        /// </summary>
        /// <param name="scoreType"></param>
        /// <returns></returns>
        public static ScoreType ScoreType_Add(ScoreType scoreType)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ScoreTypeID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@OCID", scoreType.OCID);
                    p.Add("@Name", scoreType.Name);
                    p.Add("@ParentID", scoreType.ParentID);
                    conn.Execute("ScoreType_Add", p, commandType: CommandType.StoredProcedure);
                    scoreType.ScoreTypeID = p.Get<int>("ScoreTypeID");
                    return scoreType;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region 属性更新
        /// <summary>
        /// 更新成绩类别名称
        /// </summary>
        /// <param name="scoreType"></param>
        /// <returns></returns>
        public static bool ScoreType_Name_Upd(ScoreType scoreType)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ScoreTypeID", scoreType.ScoreTypeID);
                    p.Add("@Name", scoreType.Name);
                    conn.Execute("ScoreType_Name_Upd", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 更新成绩类别启用状态
        /// </summary>
        /// <param name="scoreType"></param>
        /// <returns></returns>
        public static bool ScoreType_Status_Upd(ScoreType scoreType)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ScoreTypeID", scoreType.ScoreTypeID);
                    p.Add("@Status", scoreType.Status);
                    conn.Execute("ScoreType_Status_Upd", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除成绩类别
        /// </summary>
        /// <param name="scoreType"></param>
        /// <returns></returns>
        public static bool ScoreType_Del(ScoreType scoreType)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ScoreTypeID", scoreType.ScoreTypeID);
                    conn.Execute("ScoreType_Del", p, commandType: CommandType.StoredProcedure);
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
