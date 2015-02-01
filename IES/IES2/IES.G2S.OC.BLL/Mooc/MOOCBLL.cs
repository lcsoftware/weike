using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.CC.OC.Model;
using IES.G2S.OC.IBLL;
using IES.G2S.OC.DAL.Mooc;
using IES.Resource.Model;
using IES.G2S.Resource.BLL;
using IES.G2S.Resource.IBLL;
using IES.G2S.OC.IBLL.OC;

namespace IES.G2S.OC.BLL.Mooc
{
    public class MOOCBLL : IMOOCBLL
    {
        #region 新增
        /// <summary>
        /// 新增讨论主题
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OCMoocLive OCMoocLive_Add(OCMoocLive model)
        {
            return MOOCDAL.OCMoocLive_Add(model);
        } 
        #endregion

        #region 更新
        /// <summary>
        /// 编辑资料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void OCMoocFile_Edit(OCMoocFile model)
        {
            MOOCDAL.OCMoocFile_Edit(model);
        }
        #endregion

        #region 详细信息
        /// <summary>
        /// 获取MOOC基本信息
        /// </summary>
        /// <param name="OCID">在线课程ID</param>
        /// <returns></returns>
        public OCMooc OCMooc_Get(int OCID)
        {
            return MOOCDAL.OCMooc_Get(OCID);
        }


        /// <summary>
        /// 获取MOOC详细信息
        /// </summary>
        /// <param name="OCID">在线课程ID</param>
        /// <returns></returns>
        public OCMoocInfo OCMoocInfo_Get(int OCID)
        {
            OCMooc ocmooc = OCMooc_Get(OCID);
            IChapterBLL chapterbll = new ChapterBLL();
            List<Chapter> listchapter = chapterbll.Chapter_List(OCID);
            OCMoocInfo ocmoocinfo = new OCMoocInfo();
            ocmoocinfo.OcMooc = ocmooc;
            ocmoocinfo.ChapterList = listchapter;
            return ocmoocinfo;

        }

        #endregion

        #region 列表信息
        /// <summary>
        /// 获取见面课列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        public List<OCMoocOffline> OCMoocOffline_List(int OCID)
        {
            return MOOCDAL.OCMoocOffline_List(OCID);
        }

        /// <summary>
        /// 获取课程资料列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        public List<OCMoocFile> OCMoocFile_List(int OCID, int ChapterID)
        {
            return MOOCDAL.OCMoocFile_List(OCID, ChapterID);
        }

        /// <summary>
        /// 获取章节讨论列表
        /// </summary>
        /// <param name="ChapterID"></param>
        /// <returns></returns>
        public List<OCMoocLive> OCMoocLiveDiscuss_List(int ChapterID)
        {
            return MOOCDAL.OCMoocLiveDiscuss_List(ChapterID);
        }


        #endregion
    }
}
