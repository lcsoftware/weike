using IES.G2S.CourseLive.DAL.Score;
using IES.G2S.CoursLive.IBLL.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.CourseLive.BLL.Score
{
    /// <summary>
    /// 成绩详细列表
    /// 徐卫
    /// 2014年12月31日18:41:48
    /// </summary>
    public class ScoreWithInfoBLL : IScoreWithInfoBLL
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
        public List<CC.Model.Score.ScoreWithInfo> ScoreWithInfo_List(CC.Model.Score.ScoreWithInfo swi, int PageIndex = 1, int PageSize = 20)
        {
            return ScoreWithInfoDAL.ScoreWithInfo_List(swi, PageIndex, PageSize);
        } 
        #endregion
    }
}
