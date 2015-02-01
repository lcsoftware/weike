using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.CC.Affairs.Model;

namespace IES.G2S.OC.IBLL
{
    public interface IAffairsBLL
    {

        #region  列表

        List<OCAffairs> Affairs_List(OCAffairs model, int PageIndex, int PageSize);

        #endregion


        #region 详细信息


        #endregion


        #region  新增

     


        #endregion


        #region 对象更新

        bool OCAffairs_Status_Upd(OCAffairs model);


        #endregion


        #region 单个批量更新


        #endregion


        #region 属性批量操作


        bool OCAffairs_Beach_Upd(OCAffairs model);


        #endregion

        #region 删除
        bool Affairs_Del(OCAffairs model);
        //bool Affairs_Batch_Del(Affairs model);

        #endregion

    }
}
