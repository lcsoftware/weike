using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.CC.OC.Model;

namespace IES.G2S.OC.IBLL.OC
{
    public interface IOCClassBLL
    {

        #region  列表
        ///// <summary>
        ///// 新的学生申请列表
        ///// </summary>
        ///// <param name="OCID"></param>
        ///// <returns></returns>
        //List<OCClassRegStudent> OCClass_RegStudent_List(int OCID);
        /// <summary>
        /// 获取教学班列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<OCClass> OCClass_List(OCClass model, int PageIndex, int PageSize);

        /// <summary>
        /// 获取教学班学生列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<OCClassStudent> OCClassStudent_List(OCClass model, int PageIndex, int PageSize);

        /// <summary>
        /// 学生列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<OCClassStudent> OCClass_Student_List(OCClassStudent model, int PageIndex, int PageSize);
        ///// <summary>
        ///// 获取教学班在读学生列表
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //List<OCClassStudent> OCClassStudent_OnLine_List(OCClassStudent model, int PageIndex, int PageSize);

        #endregion


        #region 详细信息
        /// <summary>
        /// 获取教学班基本信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        OCClassInfo OCClassInfo_Get(OCClass model, int PageIndex, int PageSize);

        #endregion


        #region  新增
        /// <summary>
        /// 添加教学班
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        OCClass OCClass_Edit(OCClass model);


        #endregion


        #region 对象更新



        #endregion


        #region 单个批量更新




        #endregion


        #region 属性批量操作





        #endregion


        #region 删除
        bool OCClass_Del(OCClass model);
        bool OCClass_Batch_Del(string OCClassIDs);

        #endregion
    }
}
