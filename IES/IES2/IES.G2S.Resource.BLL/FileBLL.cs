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
    public class FileBLL:IFileBLL
    {

        #region 文件夹操作

        public Folder Folder_Get()
        {
            return FileDAL.Folder_Get();
        }
        public Folder Folder_GetModel(Folder folder)
        {
            return FileDAL.Folder_GetModel(folder);
        }

        public List<Folder> Folder_List(Folder folder)
        {
            return FileDAL.Folder_List(folder).ToList();
        }
        public List<Folder> Folder_Tree(Folder model)
        {
            return FileDAL.Folder_List(model).ToList();
        }
        
        [PermissionsCallHandler(Order = 2)]
        [ExceptionCallHandler(Order = 1)]
        public IES.Resource.Model.Folder Folder_ADD(Folder model)
        {
            return FileDAL.Folder_ADD(model);
        }

        public bool Folder_Name_Upd(Folder model)
        {
            return FileDAL.Folder_Name_Upd(model); ;
        }

        public bool Folder_ParentID_Upd(Folder model)
        {
            return FileDAL.Folder_ParentID_Upd(model);
        }

        public bool Folder_Upd(Folder model)
        {
            return true;
        }

        public bool Folder_Move(Folder source, Folder Target)
        {
            return true;
        }


        [PermissionsCallHandler(Order = 2)]
        [ExceptionCallHandler(Order = 1)]
        public bool Folder_Del(Folder  folder)
        {
           return  FileDAL.Folder_Del(folder);
        }

        public bool Folder_Batch_Del( List<Folder> folderlist)
        {
            return FileDAL.Folder_Batch_Del(folderlist);
        }

        #endregion 


        #region 文件操作

        /// <summary>
        ///  文件查询列表
        /// </summary>
        /// <param name="Searchkey">查询关键字</param>
        /// <param name="CourseID">课程编号 ，我的资料库courseid=0 </param>
        /// <param name="FolderID">文件夹编号， 0 表示ParentID=0 </param>
        /// <param name="FileType">文件类型，视频、word PPT。。。。 </param>
        /// <param name="UploadTime"> 上传日期 ，需要从业务层计算 >= @UploadTime </param>
        /// <param name="ShareRange">共享范围</param>
        /// <param name="UserID">当前用户编号</param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public List<File> File_Search( File file )
        {
            return FileDAL.File_Search( file ).ToList();
        }



        public File File_ADD(File model)
        {
            return FileDAL.File_ADD(model); 
        }

        public bool File_FileTitle_Upd(File model)
        {
            return FileDAL.File_FileTitle_Upd(model); 
        }

        public bool File_ShareRange(File model)
        {
            return true;
        }

        public bool File_Batch_ShareRange(List<File> filelist)
        {
            return true;
        }

        public bool File_Attribute(File model, List<Chapter> chapterlist, List<Ken> kenlist)
        {
            return true;
        }


        public bool File_Keys(File model, List<Key> keylist)
        {
            return true;
        }


        public bool File_Move(File source, Folder Target)
        {
            return true;
        }


        public bool File_Del(File model)
        {
            return FileDAL.File_Del(model);
        }


        #endregion 


    }
}
