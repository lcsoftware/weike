using IES.CC.Model.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.CoursLive.IBLL.Score
{
    /// <summary>
    /// 成绩加权接口
    /// 徐卫
    /// 2014年12月27日15:28:26
    /// </summary>
    public interface IScoreWeightBLL
    {
        #region 列表
        /// <summary>
        /// 成绩权重类别列表
        /// </summary>
        /// <param name="teachingClassID"></param>
        /// <returns></returns>
        List<ScoreWeight> ScoreWeight_List(int OCID, int? teachingClassID);
        #endregion

        #region 属性更新
        /// <summary>
        /// 更改成绩类别的权重系数
        /// </summary>
        /// <param name="weightID"></param>
        /// <returns></returns>
        bool ScoreWeight_Power_Upd(ScoreWeight sw);

        /// <summary>
        /// 更改成绩类别加权参与次数
        /// </summary>
        /// <param name="weightID"></param>
        /// <returns></returns>
        bool ScoreWeight_JoinNum_Upd(ScoreWeight sw);
        #endregion
    }
}
