using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.CC.OC.Model;

namespace IES.G2S.OC.IBLL.OC
{
    public interface IOCClassBLL
    {

        #region  列表

        List<OCClassInfo> OCClassInfo_List(int OCID, int TeamID, string Searchkey,int IsHistroy );

        List<OCClassRegStudent> OCClassRegStudent_List(int OCID);

        List<OCClassRegInfo> OCClassList(OCClass model, int PageIndex, int PageSize);

        #endregion


        #region 详细信息

        #endregion


        #region  新增

      


        #endregion


        #region 对象更新
       


        #endregion


        #region 单个批量更新




        #endregion


        #region 属性批量操作





        #endregion


        #region 删除


        #endregion
    }
}
