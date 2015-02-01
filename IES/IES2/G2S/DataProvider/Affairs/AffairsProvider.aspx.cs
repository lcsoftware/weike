using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using IES.G2S.OC.BLL;
using IES.CC.Affairs.Model;
using IES.JW.Model;
namespace App.G2S.DataProvider
{
    public partial class AffairsProvider : System.Web.UI.Page
    {
        #region  列表
        [WebMethod]
        public static List<Dict> Dict_List(Dict model)
        {
            AffairsBLL affairsBLL = new AffairsBLL();
            return affairsBLL.Dict_List(model);
        }
        [WebMethod]
        public static List<OCAffairs> Affairs_List(OCAffairs model, int PageIndex, int PageSize)
        {
            AffairsBLL affairsBLL = new AffairsBLL();
            return affairsBLL.Affairs_List(model, PageIndex, PageSize);
        }
        #endregion


        #region 详细信息


        #endregion


        #region  新增




        #endregion


        #region 对象更新

        [WebMethod]
        public static bool OCAffairs_Status_Upd(OCAffairs model)
        {
            AffairsBLL affairsBLL = new AffairsBLL();
            return affairsBLL.OCAffairs_Status_Upd(model);
        }

        #endregion


        #region 单个批量更新
   
        #endregion


        #region 属性批量操作


        [WebMethod]
        public static bool OCAffairs_Beach_Upd(OCAffairs model)
        {
            AffairsBLL affairsBLL = new AffairsBLL();
            return affairsBLL.OCAffairs_Beach_Upd(model);
        }
        #endregion




        #region 删除
        [WebMethod]
        public static bool Affairs_Del(OCAffairs model)
        {
            AffairsBLL affairsBLL = new AffairsBLL();
            return Affairs_Del(model);
        }
        #endregion
    }
}