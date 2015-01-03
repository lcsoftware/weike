using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.G2S.OC.IBLL.OC;
using IES.CC.OC.Model;
using IES.G2S.OC.DAL;
using IES.Security;
using IES.Cache;
using IES.G2S.OC.DAL.OC;

namespace IES.G2S.OC.BLL.OC
{
    public class OCClassBLL : IOCClassBLL
    {
        #region IOCClassBLL 成员
        
        #region  列表
        public List<OCClassRegStudent> OCClassRegStudent_List(int OCID)
        {
            return OCClassDAL.OCClassRegStudent_List(OCID);
        }
        public List<OCClassInfo> OCClassInfo_List(int OCID, int TeamID, string Searchkey, int IsHistroy)
        {
            return OCClassDAL.OCClassInfo_List(OCID, TeamID, Searchkey, IsHistroy);
        }

        public List<OCClassRegInfo> OCClassList(OCClass model, int PageIndex, int PageSize)
        {
            return OCClassDAL.OCClassList(model, PageIndex, PageSize);
        }
       
        
        #endregion
        #endregion
    }
}
