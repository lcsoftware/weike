using IES.Resource.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using IES.Common.Data;

namespace App.Resource.DataProvider.Resource
{
    public partial class ResourceProvider : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

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
    }
}