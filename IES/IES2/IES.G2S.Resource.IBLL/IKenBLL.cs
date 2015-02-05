using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.Resource.Model;

namespace IES.G2S.Resource.IBLL
{
    public interface IKenBLL
    {

        bool Ken_Del( Ken model );

        Ken Ken_ADD(Ken model);

        bool Ken_Upd(Ken model);

        List<Ken> Ken_List(Ken model);

        List<Ken> ExerciseOrFile_Ken_List(string SearchKey, string Source, int UserID, int TopNum,int OCID);

        List<Ken> Ken_ExerciseCount_List(int OCID, int UserID, int ExerciseType, int Diffcult);

        IList<Ken> Ken_FileFilter_ChapterID_List(Chapter chapter);

        IList<Ken> Ken_ExerciseFilter_ChapterID_List(Chapter chapter);

        IList<Exercise> Exercise_KenID_ChapterID_List(Chapter chapter, Ken ken);

        List<File> File_KenID_ChapterID_List(Chapter model, Ken ken);

    }
}
