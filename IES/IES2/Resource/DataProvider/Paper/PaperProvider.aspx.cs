/* **************************************************************
 * Copyright(c) 2014 Eastday, All Rights Reserved.   
 * File             : PaperProvider.aspx.cs
 * Description      : 试卷数据访问
 * Author           : fenglujian
 * Created          : 2014-12-29  
 * Revision History : 
******************************************************************/
namespace App.Resource.DataProvider.Paper
{
    using IES.Common.Data;
    using IES.Resource.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Services;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    public partial class PaperProvider : System.Web.UI.Page
    {
        [WebMethod]
        public static IList<ResourceDict> GetPaperTypes()
        {
            IList<ResourceDict> dicts = new ResourceCommonData().Resource_Dict_PaperType_Get();
            return dicts;
        }
    }
}