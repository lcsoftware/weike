/* **************************************************************
* Copyright(c) 2014 IES, All Rights Reserved.   
* File             : ResourceProvider.aspx.cs
* Description      : 试卷数据访问
* Author           : shujianhua 
* Created          : 2014-12-29  
* Revision History : 
******************************************************************/
namespace App.Resource.DataProvider.Resource
{
    using IES.Common.Data;
    using IES.Resource.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Services;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using IES.G2S.Resource.BLL;
    using IES.Service;
    using IES.CC.OC.Model;

    public partial class ResourceProvider : System.Web.UI.Page
    {
        #region 文件列表
        /// <summary>
        /// 获取资源的文件类型
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static IList<ResourceDict> Resource_Dict_FileType_Get()
        {
            return ResourceCommonData.Resource_Dict_FileType_Get();
        }

        /// <summary>
        /// 获取上传时间的设置
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static IList<ResourceDict> Resource_Dict_TimePass_Get()
        {
            return ResourceCommonData.Resource_Dict_TimePass_Get();
        }

        /// <summary>
        /// 获取使用权限
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static IList<ResourceDict> Resource_Dict_ShareRange_Get()
        {
            return ResourceCommonData.Resource_Dict_ShareRange_Get();
        }

        /// <summary>
        /// 文件查询列表
        /// </summary>
        /// <param name="file"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        [WebMethod]
        public static IList<File> File_Search(File file)
        {
            return new FileBLL().File_Search(file);
        }
        /// <summary>
        /// 删除文件功能
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool File_Del(File file)
        {
            return new FileBLL().File_Del(file);
        }

        /// <summary>
        /// 文件新增
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [WebMethod]
        public static File File_ADD(File file)
        {
            return new FileBLL().File_ADD(file);
        }

        /// <summary>
        /// 文件重命名
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool File_FileTitle_Upd(File file)
        {
            return new FileBLL().File_FileTitle_Upd(file);
        }

        /// <summary>
        /// 文件移动
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool File_FolderID_Upd(File file)
        {
            return new FileBLL().File_FolderID_Upd(file);
        }

        /// <summary>
        /// 批量设置使用权限
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool File_Batch_ShareRange(string FileIDS, int ShareRange)
        {
            return new FileBLL().File_Batch_ShareRange(FileIDS, ShareRange);
        }

        [WebMethod]
        public static bool File_Chapter_Ken_Edit(File model, Chapter chapter, Ken ken)
        {
            return new FileBLL().File_Chapter_Ken_Edit(model, chapter, ken);
        }
        #endregion

        #region FolderRelation

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        [WebMethod]
        public static IList<FolderRelation> FolderRelation_List(Folder folder, File file)
        {
            //if (folder.ParentID == 0) folder.ParentID = -1;
            //IList<Folder> allFolders = new FileBLL().Folder_List(folder);
            IList<Folder> allFolders = Folder_List(folder);
            if(file != null)
            {
                file.FolderID = folder.ParentID;
                file.UploadTime = DateTime.Now.AddMonths(-1);
            }            
            IList<File> allFiles = new FileBLL().File_Search(file);

            IList<FolderRelation> allFolderRelations = new List<FolderRelation>();
            //TODO

            foreach (var item in allFolders)
            {
                FolderRelation fr = new FolderRelation();
                fr.Id = item.FolderID;
                fr.Name = item.FolderName;
                fr.OCID = item.OCID;
                fr.ParentID = item.ParentID;
                fr.RelationType = FileType.Folder;
                fr.CourseId = item.CourseID;
                fr.CreateTime = item.CreateTime;
                allFolderRelations.Add(fr);
            }
            foreach (var item in allFiles)
            {
                FolderRelation fr = new FolderRelation();
                fr.Id = item.FileID;
                fr.Name = item.FileTitle;
                fr.OCID = item.OCID;
                fr.ParentID = item.FolderID;
                fr.RelationType = FileType.File;
                fr.CourseId = item.CourseID;
                fr.FileSize = item.FileSize;
                fr.Ext = item.Ext;
                fr.CreateTime = item.UploadTime;
                allFolderRelations.Add(fr);
            }

            IList<FolderRelation> roots = new List<FolderRelation>();

            foreach (var root in allFolderRelations)
            {
                if (folder.ParentID == -1) folder.ParentID = 0;
                if (root.ParentID == folder.ParentID) roots.Add(root);
                //if (root.RelationType == FileType.Folder)
                //{
                //    if (root.ParentID == folder.ParentID) roots.Add(root);
                //}
                //else
                //{
                //    roots.Add(root);
                //}
            }
            foreach (var root in roots)
            {
                if (root.RelationType == FileType.Folder)
                {
                    BuildRelationFolder(allFolderRelations, root);
                }
            }            
             
            return roots;
        }

        private static void BuildRelationFolder(IList<FolderRelation> allFolders, FolderRelation root)
        {
            var children = from v in allFolders
                           where v.ParentID == root.Id
                           select v;
            foreach (var child in children)
            { 
                root.Children.Add(child);
                if (child.RelationType == FileType.Folder)
                {
                    BuildRelationFolder(allFolders, child);
                }
            }
        }
        #endregion

        #region 文件夹列表

        /// <summary>
        /// 文件夹查询列表
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        [WebMethod]
        public static IList<Folder> Folder_List(Folder folder)
        {
            if (folder.ParentID == 0)
            {
                folder.ParentID = -1;
            }

            IList<Folder> allFolders = new FileBLL().Folder_List(folder);            

            return allFolders;
        }

        /// <summary>
        /// 新建文件夹
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        [WebMethod]
        public static Folder Folder_ADD(Folder folder)
        {
            return new FileBLL().Folder_ADD(folder);
        }
        /// <summary>
        /// 修改文件夹名称
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool Folder_Name_Upd(Folder folder)
        {
            return new FileBLL().Folder_Name_Upd(folder);
        }
        /// <summary>
        /// 获得空对象
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static Folder Folder_Get()
        {
            return new FileBLL().Folder_Get();
        }
        /// <summary>
        /// 获得文件夹对象
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static Folder Folder_GetModel(Folder folder)
        {
            return new FileBLL().Folder_GetModel(folder);
        }

        /// <summary>
        /// 文件夹移动
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool Folder_ParentID_Upd(Folder folder)
        {
            return new FileBLL().Folder_ParentID_Upd(folder);
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool Folder_Del(Folder folder)
        {
            return new FileBLL().Folder_Del(folder);
        }
        #endregion
    }
}