using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.Resource.Model;

namespace IES.G2S.Resource.IBLL
{

    public interface  IFileBLL
    {

        #region 文件夹操作

        /// <summary>
        /// 文件夹列表
        /// </summary>
        /// <param name="model">文件夹对象</param>
        /// <returns></returns>
        List<Folder> Folder_List(Folder model);

        /// <summary>
        /// 文件夹树列表
        /// </summary>
        /// <param name="model">文件夹对象</param>
        /// <returns></returns>
        List<Folder> Folder_Tree(Folder model);

        Folder Folder_ADD(Folder model);

        bool Folder_Upd(Folder model);

        bool Folder_Move(Folder source, Folder Target);



        bool Folder_Del(Folder model);

        /// <summary>
        /// 文件夹批量删除
        /// </summary>
        /// <param name="folderlist"></param>
        /// <returns></returns>
        bool Folder_Batch_Del(List<Folder> folderlist);


        #endregion 


        #region 文件操作
        /// <summary>
        /// 文件查询
        /// </summary>
        /// <param name="model">文件对象</param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        List<File> File_Search(File model, int PageSize, int PageIndex);

        File File_ADD(File model);

        bool File_ShareRange(File model);

        bool File_Batch_ShareRange( List<File> model );


        bool File_Attribute(File model, List<Chapter> chapterlist, List<Ken> kenlist);

        bool File_Keys(File model, List<Key> keylist );
             
        bool File_Move(File source, Folder Target);

        bool File_Del(File model);






        #endregion 









    }
}
