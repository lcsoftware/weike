using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.CC.OC.Model;

namespace IES.G2S.OC.IBLL.OC
{
    public interface IMOOCBLL
    {
        #region 详细信息
        /// <summary>
        /// 获取MOOC基本信息
        /// </summary>
        /// <param name="OCID">在线课程ID</param>
        /// <returns></returns>
        OCMooc OCMooc_Get(int OCID);


        /// <summary>
        /// 获取MOOC详细信息
        /// </summary>
        /// <param name="OCID">在线课程ID</param>
        /// <returns></returns>
        OCMoocInfo OCMoocInfo_Get(int OCID);


        #endregion

        #region 列表信息
        /// <summary>
        /// 获取见面课列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        List<OCMoocOffline> OCMoocOffline_List(int OCID);

        /// <summary>
        /// 获取课程资料列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        List<OCMoocFile> OCMoocFile_List(int OCID, int ChapterID);

        /// <summary>
        /// 获取章节讨论列表
        /// </summary>
        /// <param name="ChapterID"></param>
        /// <returns></returns>
        List<OCMoocLive> OCMoocLiveDiscuss_List(int ChapterID);

        #endregion

        #region 新增

        /// <summary>
        /// 新增讨论主题
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        OCMoocLive OCMoocLive_Add(OCMoocLive model);

        #endregion

        #region 更新

        /// <summary>
        /// 编辑资料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void OCMoocFile_Edit(OCMoocFile model);

        #endregion

    }
}
