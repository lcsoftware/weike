using IES.CC.Forum.Model;
using IES.CC.OC.Model;
using IES.G2S.OC.DAL.Mooc;
using IES.G2S.OC.IBLL.OC;
using IES.Resource.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.OC.BLL.Mooc
{
    public class MOOCPreviewBLL : IMOOCPreviewBLL
    {    
        #region  列表
        /// <summary>
        /// 获取章节下关联的文件列表信息
        /// </summary>
        /// <param name="OCID"></param>
        /// <param name="MoocStatus"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<Chapter> ChapterStudy_List(int OCID, int UserID)
       {
           return MOOCPreviewDAL.ChapterStudy_List(OCID, UserID);
       }
        /// <summary>
        /// 获取MOCC章节下的文件列表,或者所有的文件列表 
        /// </summary>
        /// <param name="OCID"></param>
        /// <param name="UserID"></param>
        /// <param name="ChapterID"></param>
        /// <returns></returns>
        public List<OCMoocFile> OCMoocFileStudy_List(int OCID, int UserID, int ChapterID)
        {
            return MOOCPreviewDAL.OCMoocFileStudy_List(OCID, UserID, ChapterID);
        }
        /// <summary>
        /// 获取章节下的讨论列表
        /// </summary>
        /// <param name="ChapterID"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<ForumTopic> ForumTopic_ChapterID_List(int ChapterID, int PageIndex, int PageSize)
        {
            return MOOCPreviewDAL.ForumTopic_ChapterID_List(ChapterID, PageIndex, PageSize);
        }
        /// <summary>
        /// 学习资源时学习时长累计入库
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="ChapterID"></param>
        /// <param name="FileID"></param>
        /// <param name="Seconds"></param>
        /// <returns></returns>
        public bool OCMoocStuFile_Add(int UserID, int ChapterID, int FileID, int Seconds) {
            return MOOCPreviewDAL.OCMoocStuFile_Add(UserID, ChapterID, FileID, Seconds);
        }
        /// <summary>
        /// 学习资源时学习时长累计入库
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="ChapterID"></param>
        /// <param name="FileID"></param>
        /// <param name="Seconds"></param>
        /// <returns></returns>
        public bool OCMoocStuFile_StuVideoDesc_Add(int UserID, int ChapterID, int FileID, int Seconds)
        {
            return MOOCPreviewDAL.OCMoocStuFile_StuVideoDesc_Add(UserID, ChapterID, FileID, Seconds);
        }

        /// <summary>
        /// 获取某视频知识卡列表
        /// </summary>
        /// <param name="ChapterID"></param>
        /// <param name="FileID"></param>
        /// <returns></returns>
        public List<OCMoocVideoInsert> OCMoocVideoInsert_List(int ChapterID, int FileID) {
            return MOOCPreviewDAL.OCMoocVideoInsert_List(ChapterID,FileID);
        }
        /// <summary>
        /// 新增修改视频下的知识卡
        /// </summary>
        /// <param name="ocmoocvideo"></param>
        /// <returns></returns>
        public int OCMoocVideoInsert_Edit(OCMoocVideoInsert ocmoocvideo)
        {
            return MOOCPreviewDAL.OCMoocVideoInsert_Edit(ocmoocvideo);
        }
        /// <summary>
        /// 删除知识卡
        /// </summary>
        /// <param name="InsertID"></param>
        public void OCMoocVideoInsert_Del(int InsertID) {
            MOOCPreviewDAL.OCMoocVideoInsert_Del(InsertID);
        }
       #endregion
    }
}
