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
    public class ChapterBLL:IChapterBLL 
    {
        public IList<Chapter> Chapter_List(Chapter model)
        {
            return ChapterDAL.Chapter_List(model);
        }
        public List<IES.Resource.Model.File> Chapter_File_List(Chapter chapter, Ken ken)
        {
            return ChapterDAL.Chapter_File_List(chapter, ken);
        }

        public bool Chapter_Del(Chapter model)
        {
            return ChapterDAL.Chapter_Del(model);
        }

        public Chapter Chapter_ADD(Chapter model)
        {
            return ChapterDAL.Chapter_ADD(model);
        }

        public bool Chapter_Upd(Chapter model)
        {
            return ChapterDAL.Chapter_Upd(model);
        }

        public Chapter Chapter_Get()
        {
            return new Chapter();
        }

        public IList<Exercise> Chapter_Exercise_List(Chapter chapter, Ken ken)
        {
            return ChapterDAL.Chapter_Exercise_List(chapter, ken);
        }

    }
}
