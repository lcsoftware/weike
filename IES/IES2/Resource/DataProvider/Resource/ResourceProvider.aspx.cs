using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
<<<<<<< HEAD
using IES.Common.Data;
using IES.G2S.Resource.BLL;
=======
>>>>>>> 401bc7c75e28e47471b651cc44c5b24708657140

namespace App.Resource.DataProvider.Resource
{
    public partial class ResourceProvider : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
<<<<<<< HEAD
        #region 文件列表
        /// <summary>
        /// 获取资源的文件类型
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<ResourceDict> Resource_Dict_FileType_Get()
        {
            try
            {
                ResourceCommonData rcd = new ResourceCommonData();
                List<ResourceDict> list = rcd.Resource_Dict_FileType_Get();
                return list.Count > 0 ? list : null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取上传时间的设置
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<ResourceDict> Resource_Dict_TimePass_Get()
        {
            try
            {
                ResourceCommonData rcd = new ResourceCommonData();
                List<ResourceDict> list = rcd.Resource_Dict_TimePass_Get();
                return list.Count > 0 ? list : null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取使用权限
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<ResourceDict> Resource_Dict_ShareRange_Get()
        {
            try
            {
                ResourceCommonData rcd = new ResourceCommonData();
                List<ResourceDict> list = rcd.Resource_Dict_ShareRange_Get();
                return list.Count > 0 ? list : null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 文件查询列表
        /// </summary>
        /// <param name="file"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<File> File_Search(File file, int PageSize, int PageIndex)
        {
            try
            {
                FileBLL bll = new FileBLL();
                List<File> list = bll.File_Search(file, PageSize, PageIndex);
                return list.Count > 0 ? list : null;
            }
            catch
            {
                return null;
            }
        }
        #endregion
=======
>>>>>>> 401bc7c75e28e47471b651cc44c5b24708657140
    }
}