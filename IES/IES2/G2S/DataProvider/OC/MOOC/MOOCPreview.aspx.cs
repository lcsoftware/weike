using IES.CC.Forum.Model;
using IES.CC.OC.Model;
using IES.G2S.OC.BLL.Mooc;
using IES.G2S.OC.BLL.Site;
using IES.G2S.OC.IBLL.OC;
using IES.G2S.OC.IBLL.Site;
using IES.Resource.Model;
using IES.Security;
using IES.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App.AngularMvc.DataProvider.OC.MOOC
{
    public partial class MOOCPreview : System.Web.UI.Page
    { 
        /// <summary>
        /// 获取章节的列表信息
        /// </summary>
        /// <param name="OCID"></param>
        /// <param name="MoocStatus"></param>
        /// <returns></returns>
         [WebMethod]
        public static List<Chapter> ChapterStudy_List(int OCID)
        {
            IES.JW.Model.User user = IES.Service.UserService.CurrentUser;
            Chapter chapter = new Chapter();
            IMOOCPreviewBLL moocprbll = new MOOCPreviewBLL();
            List<Chapter> chapter_list = moocprbll.ChapterStudy_List(OCID, user.UserID);
            ForeachPropertyNode(chapter_list.OrderBy(i => i.Orde).ToList(), chapter, 0);
            return chapter.Children;
        }
        /// <summary>
         /// 获取MOCC章节下的文件列表,或者所有的文件列表    
        /// </summary>
        /// <param name="OCID"></param>
        /// <param name="ChapterID"></param>
        /// <returns></returns>
         [WebMethod]
         public static List<OCMoocFile> OCMoocFileStudy_List(int OCID, int ChapterID)
         {
             IES.JW.Model.User user = IES.Service.UserService.CurrentUser;
             IMOOCPreviewBLL moocprbll = new MOOCPreviewBLL();
             return moocprbll.OCMoocFileStudy_List(OCID, user.UserID, ChapterID);
         }

        /// <summary>
         /// 获取章节下的讨论列表
        /// </summary>
        /// <param name="ChapterID"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        [WebMethod]
         public static List<ForumTopic> ForumTopic_ChapterID_List(int ChapterID, int PageIndex, int PageSize)
         {
             IMOOCPreviewBLL moocprbll = new MOOCPreviewBLL();
             return moocprbll.ForumTopic_ChapterID_List(ChapterID, PageIndex, PageSize);
         }
        /// <summary>
        /// 学习资源时学习时长累计入库
        /// </summary>
        /// <param name="ChapterID"></param>
        /// <param name="FileID"></param>
        /// <param name="Seconds"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool OCMoocStuFile_Add(int ChapterID, int FileID, int Seconds) {
            IES.JW.Model.User user = IES.Service.UserService.CurrentUser;
            IMOOCPreviewBLL moocprbll = new MOOCPreviewBLL();
            return moocprbll.OCMoocStuFile_Add(user.UserID,ChapterID,FileID,Seconds);
        }

        /// <summary>
        /// 学习资源时视频点入库,且记录日志
        /// </summary>
        /// <param name="ChapterID"></param>
        /// <param name="FileID"></param>
        /// <param name="Seconds"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool OCMoocStuFile_StuVideoDesc_Add(int ChapterID, int FileID, int Seconds) {
            IES.JW.Model.User user = IES.Service.UserService.CurrentUser;
            IMOOCPreviewBLL moocprbll = new MOOCPreviewBLL();
            return moocprbll.OCMoocStuFile_StuVideoDesc_Add(user.UserID, ChapterID, FileID, Seconds);
        }

        /// <summary>
        /// 获取某视频知识卡列表
        /// </summary>
        /// <param name="ChapterID"></param>
        /// <param name="FileID"></param>
        /// <returns></returns>
       
        [WebMethod]
        public static List<OCMoocVideoInsert> OCMoocVideoInsert_List(int ChapterID, int FileID) {
            IMOOCPreviewBLL moocprbll = new MOOCPreviewBLL();
            return moocprbll.OCMoocVideoInsert_List(ChapterID, FileID);
        }
        /// <summary>
        /// 新增修改视频下的知识卡
        /// </summary>
        /// <param name="InsertID"></param>
        /// <param name="ChapterID"></param>
        /// <param name="FileID"></param>
        /// <param name="Second"></param>
        /// <param name="Conten"></param>
        /// <returns></returns>
        [WebMethod]
        public static int OCMoocVideoInsert_Edit(int InsertID, int ChapterID, int FileID, int Second, string Conten) {
            OCMoocVideoInsert ocmoocvideo = new OCMoocVideoInsert();
            ocmoocvideo.InsertID = InsertID;
            ocmoocvideo.ChapterID = ChapterID;
            ocmoocvideo.FileID = FileID;
            ocmoocvideo.Second = Second;
            ocmoocvideo.Conten = Conten;
            IMOOCPreviewBLL moocprbll = new MOOCPreviewBLL();
            return moocprbll.OCMoocVideoInsert_Edit(ocmoocvideo);
        }
        /// <summary>
        /// 删除知识卡
        /// </summary>
        /// <param name="InsertID"></param>
        [WebMethod]
        public static void OCMoocVideoInsert_Del(int InsertID) {
            IMOOCPreviewBLL moocprbll = new MOOCPreviewBLL();
            moocprbll.OCMoocVideoInsert_Del(InsertID);
        }


        /// <summary>
        /// 获取在线课程的基本信息
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<IES.CC.OC.Model.OC> OC_Get(int OCID)
        {
            ISiteBLL ocbll = new SiteBLL();
            return ocbll.OC_Get(OCID);
        }




         private static void ForeachPropertyNode(List<Chapter> ocsitecloumn, Chapter node, int pid)
         {
             List<Chapter> dvDict = ocsitecloumn.FindAll(delegate(Chapter item) { return item.ParentID == pid; });
             if (dvDict.Count > 0)
             {
                 foreach (Chapter view in dvDict)
                 {
                     Chapter childNodeItem = new Chapter()
                     {
                         ChapterID = view.ChapterID,
                         OCID = view.OCID,
                         Title=view.Title,
                         ParentID=view.ParentID,
                         Orde=view.Orde,
                         PlanDay=view.PlanDay,
                         MinHour=view.MinHour,
                         FileNum=view.FileNum,
                         IsFinish=view.IsFinish,
                         IsTest=view.IsTest,
                         IsAllowStudy=view.IsAllowStudy,
                         IsActive=false
                        
                     };

                     ForeachPropertyNode(ocsitecloumn, childNodeItem, childNodeItem.ChapterID);
                     node.Children.Add(childNodeItem);
                 }
             }
         }

      


         
    }
}