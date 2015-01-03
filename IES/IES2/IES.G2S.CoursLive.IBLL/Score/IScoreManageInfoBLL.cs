using IES.CC.Model.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.CoursLive.IBLL.Score
{
    /// <summary>
    /// 成绩管理
    /// 徐卫
    /// 2014年12月31日11:38:03
    /// </summary>
    public interface IScoreManageInfoBLL
    {
        #region 列表
        /// <summary>
        /// 成绩管理列表
        /// 徐卫
        /// 2014年12月31日11:46:42
        /// </summary>
        /// <param name="smi"></param>
        /// <param name="PageIndex">页数</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        List<ScoreManageInfo> ScoreManageInfo_List(ScoreManageInfo smi, int PageIndex = 1); 
        #endregion
    }
}
