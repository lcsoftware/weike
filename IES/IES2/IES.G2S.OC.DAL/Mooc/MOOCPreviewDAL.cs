using Dapper;
using IES.CC.Forum.Model;
using IES.CC.OC.Model;
using IES.DataBase;
using IES.Resource.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.OC.DAL.Mooc
{
   public class MOOCPreviewDAL
   {
       #region  列表
       /// <summary>
       /// 获取章节的列表信息
       /// </summary>
       /// <param name="OCID"></param>
       /// <param name="MoocStatus"></param>
       /// <returns></returns>
       public static List<Chapter> ChapterStudy_List(int OCID, int UserID)
       {
           using (var conn = DbHelper.CCService())
           {
               ChapterInfo chapterinfo = new ChapterInfo();
               var p = new DynamicParameters();
               p.Add("@OCID", OCID);
               p.Add("@UserID", UserID);
               //var ci= conn.QueryMultiple("ChapterStudy_List", p, commandType: CommandType.StoredProcedure);
               //chapterinfo.Chapters = ci.Read<Chapter>().ToList();
               //chapterinfo.ChapterTests = ci.Read<ChapterTest>().ToList();
               //return chapterinfo;
               return conn.Query<Chapter>("ChapterStudy_List", p, commandType: CommandType.StoredProcedure).ToList();


            

           }
       }
       /// <summary>
       /// 获取MOCC章节下的文件列表,或者所有的文件列表
       /// </summary>
       /// <param name="OCID"></param>
       /// <param name="UserID"></param>
       /// <param name="ChapterID"></param>
       /// <returns></returns>
       public static List<OCMoocFile> OCMoocFileStudy_List(int OCID, int UserID, int ChapterID)
       {
           using (var conn = DbHelper.CCService())
           {
               var p = new DynamicParameters();
               p.Add("@OCID", OCID);
               p.Add("@UserID", UserID);
               p.Add("@ChapterID", ChapterID);
               return conn.Query<OCMoocFile>("OCMoocFileStudy_List", p, commandType: CommandType.StoredProcedure).ToList();
           }  
       }
       /// <summary>
       /// 获取章节下的讨论列表
       /// </summary>
       /// <param name="ChapterID"></param>
       /// <param name="PageIndex"></param>
       /// <param name="PageSize"></param>
       /// <returns></returns>
       public static List<ForumTopic> ForumTopic_ChapterID_List(int ChapterID, int PageIndex, int PageSize) {
           using (var conn = DbHelper.CCService())
           {
               var p = new DynamicParameters();
               p.Add("@ChapterID", ChapterID);
               p.Add("@PageIndex", PageIndex);
               p.Add("@PageSize", PageSize);
               return conn.Query<ForumTopic>("ForumTopic_ChapterID_List", p, commandType: CommandType.StoredProcedure).ToList();
           }  
       }

       /// <summary>
       /// 学习资源时学习时长累计入库
       /// </summary>
       /// <param name="UserID"></param>
       /// <param name="ChapterID"></param>
       /// <param name="FileID"></param>
       /// <param name="Seconds"></param>
       /// <returns></returns>
       public static bool OCMoocStuFile_Add(int UserID, int ChapterID, int FileID, int Seconds)
       {
           using (var conn = DbHelper.CCService())
           {
               var p = new DynamicParameters();
               p.Add("@UserID", UserID);
               p.Add("@ChapterID", ChapterID);
               p.Add("@FileID", FileID);
               p.Add("@TimeCount", Seconds);
               return Convert.ToBoolean(conn.Execute("OCMoocStuFile_Add", p, commandType: CommandType.StoredProcedure));
           }  
       }
       /// <summary>
       /// 学习资源时视频点入库,且记录日志
       /// </summary>
       /// <param name="UserID"></param>
       /// <param name="ChapterID"></param>
       /// <param name="FileID"></param>
       /// <param name="Seconds"></param>
       /// <returns></returns>
       public static bool OCMoocStuFile_StuVideoDesc_Add(int UserID, int ChapterID, int FileID, int Seconds) {
           using (var conn = DbHelper.CCService())
           {
               var p = new DynamicParameters();
               p.Add("@UserID", UserID);
               p.Add("@ChapterID", ChapterID);
               p.Add("@FileID", FileID);
               p.Add("@Seconds", Seconds);
               return Convert.ToBoolean(conn.Execute("OCMoocStuFile_StuVideoDesc_Add", p, commandType: CommandType.StoredProcedure));
           }  
       }

       /// <summary>
       /// 获取某视频知识卡列表
       /// </summary>
       /// <param name="ChapterID"></param>
       /// <param name="FileID"></param>
       /// <returns></returns>
       public static List<OCMoocVideoInsert> OCMoocVideoInsert_List(int ChapterID, int FileID) {
           using (var conn = DbHelper.CCService())
           {
               var p = new DynamicParameters();
               p.Add("@ChapterID", ChapterID);
               p.Add("@FileID", FileID);
               return conn.Query<OCMoocVideoInsert>("OCMoocVideoInsert_List", p, commandType: CommandType.StoredProcedure).ToList(); 
           }
       }

       /// <summary>
       /// 新增修改视频下的知识卡
       /// </summary>
       /// <param name="ocmoocvideo"></param>
       /// <returns></returns>
       public static int OCMoocVideoInsert_Edit(OCMoocVideoInsert ocmoocvideo) {
           using (var conn = DbHelper.CCService())
           {
               var p = new DynamicParameters();
               p.Add("@InsertID", ocmoocvideo.InsertID);
               p.Add("@ChapterID", ocmoocvideo.ChapterID);
               p.Add("@FileID", ocmoocvideo.FileID);
               p.Add("@Second", ocmoocvideo.Second);
               p.Add("@Conten", ocmoocvideo.Conten);
               p.Add("@op_InsertID", ocmoocvideo.InsertID, DbType.Int32, ParameterDirection.InputOutput);
               conn.Execute("OCMoocVideoInsert_Edit", p, commandType: CommandType.StoredProcedure);
               return p.Get<int>("op_InsertID");
           } 
       }
       /// <summary>
       /// 删除知识卡
       /// </summary>
       /// <param name="InsertID"></param>
       public static void OCMoocVideoInsert_Del(int InsertID) {
           using (var conn = DbHelper.CCService())
           {
               var p = new DynamicParameters();

               p.Add("@InsertID", InsertID);
               conn.Execute("OCMoocVideoInsert_Del", p, commandType: CommandType.StoredProcedure);
              
           }  
       }

     
       #endregion
   }
}
