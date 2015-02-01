using IES.CC.Forum.Model;
using IES.CC.OC.Model;
using IES.Resource.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.OC.IBLL.OC
{
   public interface IMOOCPreviewBLL
    {
       /// <summary>
        /// 获取章节下关联的文件列表信息
       /// </summary>
       /// <param name="OCID"></param>
       /// <param name="MoocStatus"></param>
       /// <param name="UserID"></param>
       /// <returns></returns>
       List<Chapter> ChapterStudy_List(int OCID, int UserID);
       /// <summary>
       /// 获取MOCC章节下的文件列表,或者所有的文件列表
       /// </summary>
       /// <param name="OCID"></param>
       /// <param name="UserID"></param>
       /// <param name="ChapterID"></param>
       /// <returns></returns>
       System.Collections.Generic.List<OCMoocFile> OCMoocFileStudy_List(int OCID, int UserID, int ChapterID);
       /// <summary>
       /// 获取章节下的讨论列表
       /// </summary>
       /// <param name="ChapterID"></param>
       /// <param name="PageIndex"></param>
       /// <param name="PageSize"></param>
       /// <returns></returns>
       System.Collections.Generic.List<ForumTopic> ForumTopic_ChapterID_List(int ChapterID, int PageIndex, int PageSize);
       /// <summary>
       /// 学习资源时学习时长累计入库
       /// </summary>
       /// <param name="UserID"></param>
       /// <param name="ChapterID"></param>
       /// <param name="FileID"></param>
       /// <param name="Seconds"></param>
       /// <returns></returns>
       bool OCMoocStuFile_Add(int UserID, int ChapterID, int FileID, int Seconds);
       /// <summary>
       /// 学习资源时视频点入库,且记录日志
       /// </summary>
       /// <param name="UserID"></param>
       /// <param name="ChapterID"></param>
       /// <param name="FileID"></param>
       /// <param name="Seconds"></param>
       /// <returns></returns>
       bool OCMoocStuFile_StuVideoDesc_Add(int UserID, int ChapterID, int FileID, int Seconds);
       /// <summary>
       /// 获取某视频知识卡列表
       /// </summary>
       /// <param name="ChapterID"></param>
       /// <param name="FileID"></param>
       /// <returns></returns>
       List<OCMoocVideoInsert> OCMoocVideoInsert_List(int ChapterID, int FileID);
       /// <summary>
       /// 新增修改视频下的知识卡
       /// </summary>
       /// <param name="ocmoocvideo"></param>
       /// <returns></returns>
       int OCMoocVideoInsert_Edit(OCMoocVideoInsert ocmoocvideo);
       /// <summary>
       /// 删除知识卡
       /// </summary>
       /// <param name="InsertID"></param>
       void OCMoocVideoInsert_Del(int InsertID);
    }
}
