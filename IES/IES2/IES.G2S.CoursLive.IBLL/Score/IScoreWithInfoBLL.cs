using IES.CC.Model.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.CoursLive.IBLL.Score
{
    /// <summary>
    /// 徐卫
    /// 2014年12月31日18:37:16
    /// 成绩详细信息
    /// </summary>
    public interface IScoreWithInfoBLL
    {
        #region 列表
        /// <summary>
        /// 成绩详细信息列表
        /// 徐卫
        /// 2014年12月31日18:39:31
        /// </summary>
        /// <param name="swi"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        List<ScoreWithInfo> ScoreWithInfo_List(ScoreWithInfo swi, int PageIndex = 1, int PageSize = 20); 
        #endregion
    }
}
