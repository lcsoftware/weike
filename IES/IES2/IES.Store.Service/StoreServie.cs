using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.G2S.SYS.BLL;
using IES.SYS.Model;


namespace IES.Store.Service
{
    /// <summary>
    /// 存储服务
    /// </summary>
    public class StoreServie
    {

        /// <summary>
        /// 获取所有的存储服务器列表
        /// </summary>
        /// <returns></returns>
        public List<ResourceServer> ResourceServer_List()
        {
            ResourceServerBLL resourceserverbll = new ResourceServerBLL();
            return resourceserverbll.ResourceServer_List();
        }



    }
}
