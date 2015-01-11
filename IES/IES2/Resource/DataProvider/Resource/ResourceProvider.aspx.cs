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
        public static IList<File> File_Search(File file, int PageSize, int PageIndex)
        {
            return new FileBLL().File_Search(file, PageSize, PageIndex);
        }
        /// <summary>
        /// 删除文件功能
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool File_Del(IList<File> file)
        {
            try
            {
                for (int i = 0; i < file.Count; i++)
                {
                    new FileBLL().File_Del(file[i]);
                }
                return true;
            }
            catch
            {
                return false;
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
            return new FileBLL().Folder_List(folder);
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
        #endregion
    }
}