using IES.CC.Model.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IES.G2S.CourseLive.BLL.Score;
using System.Web.Services;

namespace App.G2S.DataProvider.CourseLive.Score
{
    public partial class ScoreProvider : System.Web.UI.Page
    {
        #region 成绩类别
        #region 列表
        /// <summary>
        /// 徐卫
        /// 成绩类别列表
        /// 2014年12月27日16:51:22
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<ScoreType> ScoreType_List(int? OCID)
        {
            return new ScoreTypeBLL().ScoreType_List(OCID);
        }
        #endregion

        #region 新增
        /// <summary>
        /// 徐卫
        /// 2014年12月27日16:50:57
        /// 添加成绩类别
        /// </summary>
        /// <param name="scoreType"></param>
        /// <returns></returns>
        [WebMethod]
        public static ScoreType ScoreType_Add(ScoreType scoreType)
        {
            return new ScoreTypeBLL().ScoreType_Add(scoreType);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 徐卫
        /// 2014年12月29日11:31:16
        /// 删除成绩类别
        /// </summary>
        /// <param name="scoreType"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool ScoreType_Del(ScoreType scoreType)
        {
            return new ScoreTypeBLL().ScoreType_Del(scoreType);
        }
        #endregion

        #region 属性更新
        /// <summary>
        /// 徐卫
        /// 2014年12月29日11:31:16
        /// 修改成绩类别名称
        /// </summary>
        /// <param name="scoreType"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool ScoreType_Name_Upd(ScoreType scoreType)
        {
            return new ScoreTypeBLL().ScoreType_Name_Upd(scoreType);
        }

        /// <summary>
        /// 徐卫
        /// 2014年12月29日11:31:16
        /// 修改成绩类别状态
        /// </summary>
        /// <param name="scoreType"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool ScoreType_Status_Upd(ScoreType scoreType)
        {
            return new ScoreTypeBLL().ScoreType_Status_Upd(scoreType);
        }
        #endregion
        #endregion

        #region 成绩类别加权
        #region 列表
        /// <summary>
        /// 成绩类别加权设置列表
        /// 徐卫
        /// 2014年12月29日11:36:58
        /// </summary>
        /// <param name="teachingClassID"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<ScoreWeight> ScoreWeight_List(int OCID, int? teachingClassID)
        {
            return new ScoreWeightBLL().ScoreWeight_List(OCID, teachingClassID);
        }
        #endregion

        #region 属性更新
        /// <summary>
        /// 修改成绩类别权重系数
        /// 徐卫
        /// 2014年12月29日11:38:25
        /// </summary>
        /// <param name="weightID">权重ID</param>
        /// <returns>bool</returns>
        [WebMethod]
        public static bool ScoreWeight_Power_Upd(ScoreWeight sw)
        {
            return new ScoreWeightBLL().ScoreWeight_Power_Upd(sw);
        }

        /// <summary>
        /// 修改成绩类别加权参与次数
        /// 徐卫
        /// 2014年12月29日11:38:25
        /// </summary>
        /// <param name="weightID">权重ID</param>
        /// <returns>bool</returns>
        [WebMethod]
        public static bool ScoreWeight_JoinNum_Upd(ScoreWeight sw)
        {
            return new ScoreWeightBLL().ScoreWeight_JoinNum_Upd(sw);
        }
        #endregion
        #endregion

        #region 成绩管理
        #region 列表
        /// <summary>
        /// 成绩管理列表
        /// 徐卫
        /// 2014年12月31日11:57:28
        /// </summary>
        /// <param name="smi"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<ScoreManageInfo> ScoreManageInfo_List(ScoreManageInfo smi, int PageIndex = 1, int PageSize = 20)
        {
            return new ScoreManageInfoBLL().ScoreManageInfo_List(smi, PageIndex: PageIndex, PageSize: PageSize);
        } 
        #endregion
        #endregion

        #region 成绩详细信息
        #region 列表
        /// <summary>
        /// 成绩详细信息列表
        /// 徐卫
        /// 2015年1月4日08:41:27
        /// </summary>
        /// <param name="swi"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<ScoreWithInfo> ScoreWithInfo_List(ScoreWithInfo swi, int PageIndex = 1, int PageSize = 20)
        {
            return new ScoreWithInfoBLL().ScoreWithInfo_List(swi, PageIndex, PageSize);
        } 
        #endregion
        #endregion

    }
}