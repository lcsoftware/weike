using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.Resource.Model;

namespace IES.G2S.Resource.IBLL
{
    public interface IChapterBLL
    {

        #region  列表
        List<Chapter> Chapter_List(int OCID);
        List<Chapter> Chapter_ExerciseCount_List(int OCID, int UserID, int ExerciseType, int Diffcult);
        List<Ken> Chapter_Ken_List(Chapter model);

        List<IES.Resource.Model.File> File_ChapterID_KenID_List(Chapter chapter, Ken ken);
        List<IES.Resource.Model.Exercise> Exercise_ChapterID_KenID_List(Chapter chapter, Ken ken);

        #endregion

        #region 详细信息

        #endregion

        #region  新增


        Chapter Chapter_ADD(Chapter model);


        #endregion

        #region 对象更新

        bool Chapter_Upd(Chapter model);

        #endregion


        #region 移动

        bool Chapter_Move(int chapterID, string direction);

        #endregion

        #region 单个批量更新





        #endregion

        #region 属性批量操作




        #endregion

        #region 删除

        bool Chapter_Del(Chapter model);

        #endregion

    }
}
