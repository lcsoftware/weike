using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.G2S.Resource.DAL;
using IES.Resource.Model;
using IES.AOP.G2S;
using IES.G2S.Resource.IBLL;


namespace IES.G2S.Resource.BLL
{
    /// <summary>
    /// 知识点
    /// </summary>
    public class KenBLL : IKenBLL
    {


        public bool Ken_Del( Ken model)
        {
            return KenDAL.Ken_Del(model);
        }

        public Ken Ken_ADD(Ken model)
        {
            return  KenDAL.Ken_ADD(model);
        }

        public bool Ken_Upd(Ken model)
        {
           return   KenDAL.Ken_Upd(model);
        }


        public List<Ken> Ken_List(Ken model)
        {
            return KenDAL.Ken_List(model);
        }


        /// <summary>
        /// 获取文件、习题相关有效知识点 
        /// </summary>
        /// <param name="SearchKey"></param>
        /// <param name="Source"></param>
        /// <param name="UserID"></param>
        /// <param name="TopNum"></param>
        /// <returns></returns>
        public List<Ken> ExerciseOrFile_Ken_List(string SearchKey, string Source, int UserID, int TopNum,int OCID)
        {
            return KenDAL.ExerciseOrFile_Ken_List(SearchKey, Source, UserID, TopNum,OCID);
        }


        public List<Ken> Ken_ExerciseCount_List(int OCID, int UserID, int ExerciseType, int Diffcult)
        {
            return KenDAL.Ken_ExerciseCount_List(OCID, UserID, ExerciseType, Diffcult);
        }

        public IList<Chapter> Chapter_KenID_List(int kenId, int ocid)
        {
            return KenDAL.Chapter_KenID_List(new Ken() { KenID = kenId, OCID = ocid }); 
        }

        public List<IES.Resource.Model.File> File_KenID_ChapterID_List(Chapter chapter, Ken ken)
        {
            return KenDAL.File_KenID_ChapterID_List(chapter, ken);
        }
        public IList<Exercise> Exercise_KenID_ChapterID_List(Chapter chapter, Ken ken)
        {
            return KenDAL.Exercise_KenID_ChapterID_List(chapter, ken);
        }

        public IList<Ken> Ken_FileFilter_ChapterID_List(Chapter chapter)
        {
            return KenDAL.Ken_FileFilter_ChapterID_List(chapter);
        }

        public IList<Ken> Ken_ExerciseFilter_ChapterID_List(Chapter chapter)
        {
            return KenDAL.Ken_ExerciseFilter_ChapterID_List(chapter);
        }
    }
}
