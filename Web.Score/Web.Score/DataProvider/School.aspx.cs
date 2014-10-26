/* **************************************************************
  * Copyright(c) 2014 Score.web, All Rights Reserved.   
  * File             : School.aspx.cs
  * Description      : 学校相关数据处理
  * Author           : shujianhua 
  * Created          : 2014-10-05  
  * Revision History : 
******************************************************************/ 
namespace App.Web.Score.DataProvider
{
    using App.Score.Data;
    using App.Score.Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Services;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    public partial class School : System.Web.UI.Page
    {
        [WebMethod]
        public static SchoolBaseInfo LoadSchool()
        {
            using (AppBLL bll = new AppBLL())
            {
                var sql = "select * FROM tbSchoolBaseInfo";
                var schools = bll.FillListByText<SchoolBaseInfo>(sql, null);
                return schools.Any() ? schools.First() : null;
            }
        }
    }
}