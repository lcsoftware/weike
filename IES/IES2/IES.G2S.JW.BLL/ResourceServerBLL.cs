using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IES.G2S.JW.DAL;
using IES.JW.Model;
using IES.AOP.G2S;
using IES.G2S.JW.IBLL;


namespace IES.G2S.JW.BLL
{
    public class ResourceServerBLL : IResourceServerBLL
    {
        public List<ResourceServer> ResourceServer_List()
        {
            return ResourceServerDAL.ResourceServer_List();
        }
        public ResourceServer ResourceServer_Get(ResourceServer model)
        {
            return ResourceServerDAL.ResourceServer_Get(model);
        }
        public bool ResourceServer_Del(ResourceServer model)
        {
            return ResourceServerDAL.ResourceServer_Del(model);
        }

        public ResourceServer ResourceServer_ADD(ResourceServer model)
        {
            return ResourceServerDAL.ResourceServer_ADD(model);
        }
        //编辑存储服务器
        public ResourceServer ResourceServer_Edit(ResourceServer model)
        {
            return ResourceServerDAL.ResourceServer_Edit(model);
        }

    }
}
