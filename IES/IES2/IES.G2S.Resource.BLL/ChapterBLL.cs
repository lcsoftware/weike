﻿using System;
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
    public class ChapterBLL:IChapterBLL
    {

        #region  列表
        public List<Chapter> Chapter_List(int OCID)
        {
            return ChapterDAL.Chapter_List(new Chapter() { OCID = OCID });
        } 
        public List<Ken> Chapter_Ken_List(Chapter model)
        {
            return ChapterDAL.Chapter_Ken_List(model);
        }
        public IList<Chapter> Chapter_List(Chapter model)
        {
            return ChapterDAL.Chapter_List(model);
        }

        public List<IES.Resource.Model.File> File_ChapterID_KenID_List(Chapter chapter, Ken ken)
        {
            return ChapterDAL.File_ChapterID_KenID_List(chapter, ken);
        }

        public List<IES.Resource.Model.Exercise> Exercise_ChapterID_KenID_List(Chapter chapter, Ken ken)
        {
            return ChapterDAL.Exercise_ChapterID_KenID_List(chapter, ken);
        }

        public IList<Exercise> Chapter_Exercise_List(Chapter chapter, Ken ken)
        {
            return ChapterDAL.Chapter_Exercise_List(chapter, ken);
        }

        public List<Chapter> Chapter_ExerciseCount_List(int OCID, int UserID, int ExerciseType, int Diffcult)
        {
            return ChapterDAL.Chapter_ExerciseCount_List(OCID, UserID, ExerciseType, Diffcult);
        }
      
        #endregion

        #region  新增
        public Chapter Chapter_ADD(Chapter model)
        {
            return ChapterDAL.Chapter_ADD(model);
        }
        #endregion

        #region 移动

        public bool Chapter_Move(int chapterID, string direction)
        {
            return ChapterDAL.Chapter_Move(chapterID, direction);
        }

        #endregion

        #region 更新
        public bool Chapter_Upd(Chapter model)
        {
            return ChapterDAL.Chapter_Upd(model);
        }
        #endregion

        #region 删除
        public bool Chapter_Del(Chapter model)
        {
            return ChapterDAL.Chapter_Del(model);
        }

        #endregion

    }
}
