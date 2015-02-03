using IES.G2S.Resource.DAL;
using IES.G2S.Resource.IBLL;
using IES.Resource.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.Resource.BLL
{
    public class ResourceKenBLL: IResourceKenBLL
    {
        public ResourceKen ResourceKen_ADD(ResourceKen model)
        {
            return ResourceKenDAL.ResourceKen_ADD(model);
        }
        public bool ResourceKen_Del(ResourceKen model)
        {
            return ResourceKenDAL.ResourceKen_Del(model);
        }
        public IList<ResourceKen> ResourceKen_List(string searchKey, string source, int userId, int topNum)
        {
            return ResourceKenDAL.ResourceKen_List(searchKey, source, userId, topNum);
        }
        public IList<ResourceKen> ResourceKen_List_Source(int ocid, string source) 
        {
            return ResourceKenDAL.ResourceKen_List_Source(ocid, source); 
        }
    }
}
