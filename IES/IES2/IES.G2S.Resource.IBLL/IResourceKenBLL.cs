using IES.Resource.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.Resource.IBLL
{
    public interface IResourceKenBLL
    {
        #region Add 
        ResourceKen ResourceKen_ADD(ResourceKen model); 
        
        #endregion

        #region Del
        bool ResourceKen_Del(ResourceKen model);

        #endregion

        #region List 
        IList<ResourceKen> ResourceKen_List_Source(int ocid, string source);

        IList<Ken> ResourceKen_List(int ocid, string searchKey, string source, int userId, int topNum);
        #endregion


    }
}
