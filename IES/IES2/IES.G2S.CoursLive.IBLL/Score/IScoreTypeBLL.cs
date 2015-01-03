using IES.CC.Model.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.CoursLive.IBLL.Score
{
    /// <summary>
    /// xuwei 
    /// 2014年12月25日
    /// </summary>
    public interface IScoreTypeBLL
    {
        #region  列表
        /// <summary>
        /// 成绩类别列表
        /// </summary>
        /// <param name="scoreType"></param>
        /// <returns></returns>
        List<ScoreType> ScoreType_List(int? OCID);
        #endregion

        #region  新增
        /// <summary>
        /// 新增成绩类别
        /// </summary>
        /// <param name="scoreType"></param>
        /// <returns></returns>
        ScoreType ScoreType_Add(ScoreType scoreType);
        #endregion

        #region 属性更新
        /// <summary>
        /// 更新成绩类别名称
        /// </summary>
        /// <param name="scoreType"></param>
        /// <returns></returns>
        bool ScoreType_Name_Upd(ScoreType scoreType);

        /// <summary>
        /// 更新成绩类别启用状态
        /// </summary>
        /// <param name="scoreType"></param>
        /// <returns></returns>
        bool ScoreType_Status_Upd(ScoreType scoreType);
        #endregion

        #region 删除
        /// <summary>
        /// 删除成绩类别
        /// </summary>
        /// <param name="scoreType"></param>
        /// <returns></returns>
        bool ScoreType_Del(ScoreType scoreType);
        #endregion
    }
}
