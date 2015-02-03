/* **************************************************************
 * Copyright(c) 2015 IES, All Rights Reserved.   
 * File             : KnowProvider.aspx.cs
 * Description      : 知识点数据访问
 * Author           : zhaotianyu
 * Created          : 2015-01-17  
 * Revision History : 
******************************************************************/
namespace App.Resource.DataProvider.Knowledge
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Services;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using IES.Resource.Model;
    using IES.G2S.Resource.BLL;
    public partial class KnowProvider : System.Web.UI.Page
    {

        [WebMethod]
        public static IList<Ken> Ken_List(Ken model)
        {
            return new KenBLL().Ken_List(model);
        }

        [WebMethod]
        public static Ken Ken_ADD(Ken model)
        {
            return new KenBLL().Ken_ADD(model);
        }

        [WebMethod]
        public static bool Ken_Upd(Ken model)
        {
            return new KenBLL().Ken_Upd(model);
        }

        [WebMethod]
        public static bool Ken_Del(Ken model)
        {
            return new KenBLL().Ken_Del(model);
        }

        [WebMethod]
        public static IList<Chapter> Ken_Chapter_List(Ken model)
        {
            return new KenBLL().Ken_Chapter_List(model.KenID);
        } 
    }
}