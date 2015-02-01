using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using IES.CC.OC.Model;
using IES.G2S.CourseLive.IBLL.Forum;
using IES.G2S.CourseRun.BLL;
using IES.G2S.OC.BLL.Mooc;
using IES.G2S.OC.IBLL.OC;
using IES.G2S.Resource.BLL;
using IES.G2S.Resource.IBLL;
using IES.Resource.Model;

namespace App.G2S.DataProvider.OC.MOOC
{
    public partial class MOOCProvider : System.Web.UI.Page
    {

        #region 新增

        /// <summary>
        /// 新增讨论主题
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [WebMethod]
        public static Chapter Chapter_Add(Chapter model)
        {
            IChapterBLL chapterbll = new ChapterBLL();
            return chapterbll.Chapter_ADD(model);
        }
        #endregion

        #region 详细信息
        /// <summary>
        /// 获取MOOC基本信息
        /// </summary>
        /// <param name="OCID">课程编号</param>
        /// <returns></returns>
        [WebMethod]
        public static OCMoocInfo OCMoocInfo_Get(int OCID)
        {
            IMOOCBLL moocbll = new MOOCBLL();
            OCMoocInfo ocMoocInfo = moocbll.OCMoocInfo_Get(OCID);
            return ocMoocInfo;
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新章节信息
        /// </summary>
        /// <param name="model"></param>
        [WebMethod]
        public static void Chapter_Upd(Chapter model)
        {
            IChapterBLL chapterbll = new ChapterBLL();
            chapterbll.Chapter_Upd(model);
        }

        /// <summary>
        /// 更新章节
        /// </summary>
        /// <param name="model"></param>
        [WebMethod]
        public static OCMoocLive OCMoocLiveDiscuss_Edit(OCMoocLive model)
        {
            if (model.MoocLiveID ==-1)   //新增讨论主题
            {
                IForumTopicBLL forumtopicbll = new ForumTopicBLL();
                model = forumtopicbll.ForumTopic_Add(model);
                model.SourceID = model.TopicID;
                model.Source = "ForumTopic";
                model.IsDiscuss = true;
                model.IsMust = true;
                IMOOCBLL moocbll = new MOOCBLL();
                model=moocbll.OCMoocLive_Add(model);
            }
            else if(model.MoocLiveID>0)                  //编辑主题,主需要编辑论坛表
            {
                IForumTopicBLL forumtopicbll = new ForumTopicBLL();
                forumtopicbll.ForumTopicTitle_Upd(model);
            }
            return model;
        }

        /// <summary>
        /// 更新资料信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [WebMethod]
        public static void OCMoocFile_Edit(OCMoocFile model)
        {
            IMOOCBLL moocbll = new MOOCBLL();
            moocbll.OCMoocFile_Edit(model);
        }


        #endregion

        #region 列表信息
        /// <summary>
        /// 获取见面课列表
        /// </summary>
        /// <param name="OCID">课程ID</param>
        [WebMethod]
        public static List<OCMoocOffline> OCMoocOffline_List(int OCID)
        {
            IMOOCBLL moocbll = new MOOCBLL();
            List<OCMoocOffline> ocMoocOfflineList = moocbll.OCMoocOffline_List(OCID);
            return ocMoocOfflineList;
        }

        /// <summary>
        /// 获取课程资料列表
        /// </summary>
        ///  <param name="OCID">课程ID</param>
        [WebMethod]
        public static List<OCMoocFile> OCMoocFile_List(int OCID, int ChapterID)
        {
            IMOOCBLL moocbll = new MOOCBLL();
            List<OCMoocFile> ocMoocFileList = moocbll.OCMoocFile_List(OCID, ChapterID);
            return ocMoocFileList;
        }

        /// <summary>
        /// 获取资料列表
        /// </summary>
        /// <param name="file"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<File> File_Search(File file, int PageSize, int PageIndex)
        {
            IFileBLL filebll = new FileBLL();
            List<File> File_List = filebll.File_Search(file, PageSize, PageIndex);
            return File_List;
        }
        


        /// <summary>
        /// 获取章节讨论列表
        /// </summary>
        /// <param name="ChapterID"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<OCMoocLive> OCMoocLiveDiscuss_List(int ChapterID)
        {
            IMOOCBLL moocbll = new MOOCBLL();
            List<OCMoocLive> OCMoocLiveDiscussList = moocbll.OCMoocLiveDiscuss_List(ChapterID);
            return OCMoocLiveDiscussList;
        }


        #endregion

        #region 删除
        /// <summary>
        /// 删除章节
        /// </summary>
        /// <param name="model"></param>
        [WebMethod]
        public static void Chapter_Del(Chapter model)
        {
            IChapterBLL chapterbll = new ChapterBLL();
            chapterbll.Chapter_Del(model);
        }
        #endregion
    }
}