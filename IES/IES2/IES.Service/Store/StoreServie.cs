using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.IO;
using IES.G2S.JW.BLL;
using IES.JW.Model;
using IES.Cache;
using IES.Resource.Model;
using IES.G2S.Resource.BLL;
using IES.Common;


namespace IES.Service
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
        public static List<ResourceServer> ResourceServer_List()
        {
            ResourceServerBLL resourceserverbll = new ResourceServerBLL();
            return resourceserverbll.ResourceServer_List();
        }

        /// <summary>
        /// 根据存储编号获取分布存储服务器信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<ResourceServer> ResourceServer_List(int ServerID)
        {
            List<int> IDList = IES.Common.Delivery.DeliveryList(ServerID, 4096);
            ResourceServerBLL resourceserverbll = new ResourceServerBLL();

            List<ResourceServer> DistrServerList = new List<ResourceServer>();

            foreach (ResourceServer o in resourceserverbll.ResourceServer_List() )
            {
                if (IDList.Contains(o.ServerID))
                    DistrServerList.Add(o);
            }
            return DistrServerList;
        }


        public static ResourceServer ResourceServer_Get(int ServerID)
        {
            List<ResourceServer> serverlist = ResourceServer_List().Where(x => x.ServerID == ServerID).ToList<ResourceServer>();
            if (serverlist.Count > 0)
                return serverlist[0];
            else
                return null;

        }

        /// <summary>
        /// 获取最快的一台存储服务器
        /// </summary>
        /// <returns></returns>
        public static ResourceServer ResourceServer_Fast_Get()
        {
            List<ResourceServer> serverlist = ResourceServer_List();
            return serverlist[0]; 
        }

    }
}
