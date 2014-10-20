namespace App.Web.Score.DataProvider
{
    using App.Score.Data;
    using App.Score.Db;
    using App.Score.Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Services;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class Util : System.Web.UI.Page
    {
        [WebMethod]
        public static IList<TdNation> GetNations()
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "Select * from tdNation ";
                return bll.FillListByText<TdNation>(sql, null);
            }
        }
        [WebMethod]
        public static IList<TdPolitic> GetPolitics()
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "Select * from tdPolitics ";
                return bll.FillListByText<TdPolitic>(sql, null);
            }
        }
        [WebMethod]
        public static IList<TdResidenceType> GetResidenceType()
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "Select * from tdResidenceType ";
                return bll.FillListByText<TdResidenceType>(sql, null);
            }
        }
    }
}