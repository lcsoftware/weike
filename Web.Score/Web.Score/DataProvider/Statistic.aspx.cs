/* **************************************************************
  * Copyright(c) 2014 Score.web, All Rights Reserved.   
  * File             : Statistic.aspx.cs
  * Description      : 系统级别的服务器处理程序
  * Author           : zhaotianyu 
  * Created          : 2014-11-14  
  * Revision History : 
******************************************************************/
namespace App.Web.Score.DataProvider
{
    using App.Score.Data;
    using App.Score.Entity;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web;
    using System.Web.Services;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    public partial class Statistic : System.Web.UI.Page
    {
        [WebMethod]
        public static int Stat07()
        {
            return -1;
        }
    }
}