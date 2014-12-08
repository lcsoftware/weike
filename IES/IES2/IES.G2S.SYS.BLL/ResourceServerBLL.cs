using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IES.G2S.SYS.DAL;
using IES.SYS.Model;
using IES.AOP.G2S;


namespace IES.G2S.SYS.BLL
{
    public class ResourceServerBLL
    {
        public List<ResourceServer> ResourceServer_List()
        {
            return ResourceServerDAL.ResourceServer_List();
        }

        public bool ResourceServer_Del(ResourceServer model)
        {
            return ResourceServerDAL.ResourceServer_Del(model);
        }

        public ResourceServer ResourceServer_ADD(ResourceServer model)
        {
            return ResourceServerDAL.ResourceServer_ADD(model);
        }

    }
}
