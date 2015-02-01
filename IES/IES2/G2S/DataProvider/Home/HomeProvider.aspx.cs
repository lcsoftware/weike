using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using IES.CC.OC.Model;
using IES.G2S.Resource.BLL;
using IES.G2S.Resource.IBLL;
using IES.Resource.Model;

namespace App.G2S.DataProvider
{
    public partial class HomeProvider : System.Web.UI.Page
    {
        #region  列表

        /// <summary>
        /// 获取教学团队信息列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<IES.CC.OC.Model.OC> OC_List()
        {
            IES.G2S.OC.BLL.OC.OCBLL ocbll = new IES.G2S.OC.BLL.OC.OCBLL();
            IES.JW.Model.User user = IES.Service.UserService.CurrentUser;
            return ocbll.OC_List(user.UserID, user.Role);
        }


        /// <summary>
        /// 获取资料列表
        /// </summary>
        /// <param name="file"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<File> File_Search(File file, int PageSize, int PageIndex)
        {
            IFileBLL filebll = new FileBLL();
            IES.JW.Model.User user = IES.Service.UserService.CurrentUser;
            file.CreateUserID = user.UserID;
            List<File> File_List = filebll.File_Search(file, PageSize, PageIndex);
            return File_List;
        }

        /// <summary>
        /// 删除资源
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool File_Del(File file)
        {
            IFileBLL filebll = new FileBLL();
            return filebll.File_Del(file);
        }
        
        #endregion
    }
}