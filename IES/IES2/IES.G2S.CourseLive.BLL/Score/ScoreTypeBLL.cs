using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.G2S.CoursLive.IBLL.Score;
using IES.CC.Model.Score;
using IES.G2S.CourseLive.DAL.Score;


namespace IES.G2S.CourseLive.BLL.Score
{
    /// <summary>
    /// xuwei
    /// 2014年12月25日15:19:05
    /// </summary>
    public class ScoreTypeBLL:IScoreTypeBLL
    {
        #region  列表
        /// <summary>
        /// 成绩类别列表
        /// </summary>
        /// <param name="scoreType"></param>
        /// <returns></returns>
        public List<ScoreType> ScoreType_List(int? OCID)
        {
            return ScoreTypeDAL.ScoreType_List(OCID);
        }
        #endregion

        #region  新增
        /// <summary>
        /// 新增成绩类别
        /// </summary>
        /// <param name="scoreType"></param>
        /// <returns></returns>
        public ScoreType ScoreType_Add(ScoreType scoreType)
        {
            return ScoreTypeDAL.ScoreType_Add(scoreType);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除成绩类别
        /// </summary>
        /// <param name="scoreType"></param>
        /// <returns></returns>
        public bool ScoreType_Del(ScoreType scoreType)
        {
            return ScoreTypeDAL.ScoreType_Del(scoreType);
        }
        #endregion

        #region 属性更新
        /// <summary>
        /// 更新成绩类别名称
        /// </summary>
        /// <param name="scoreType"></param>
        /// <returns></returns>
        public bool ScoreType_Name_Upd(ScoreType scoreType)
        {
            return ScoreTypeDAL.ScoreType_Name_Upd(scoreType);
        }
        /// <summary>
        /// 更新成绩类别启用状态
        /// </summary>
        /// <param name="scoreType"></param>
        /// <returns></returns>
        public bool ScoreType_Status_Upd(ScoreType scoreType)
        {
            return ScoreTypeDAL.ScoreType_Status_Upd(scoreType);
        }
        #endregion

    }
}
