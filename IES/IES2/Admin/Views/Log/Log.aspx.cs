using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IES.G2S.JW.IBLL;
using IES.JW.Model;
using IES.Service.Common;
namespace Admin.Views.Log
{
    public partial class Log : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {              
                AspNetPager1.PageSize = Browse.PageSize;
                DropDownList1.SelectedValue = Browse.PageSize.ToString();
                DataBinder(1);
            }
        }     
        //绑定数据 
        private void DataBinder(int pageindex)
        {
            if (Session["Conditions"] != null)
            { GetSession(); }
            DateTime beginTime = Convert.ToDateTime(this.BeginTime.Value);
            DateTime endTime = Convert.ToDateTime(this.EndTime.Value);
            string key = this.Key.Value;
            IES.JW.Model.Log _log = new IES.JW.Model.Log { Key=key,Role="1", StartTime = beginTime, EndTime = endTime };
            IES.G2S.JW.BLL.LogBLL logbll = new IES.G2S.JW.BLL.LogBLL();

            List<IES.JW.Model.Log> loglist = logbll.Log_List(_log, pageindex, AspNetPager1.PageSize);
            if (loglist.Count > 0)
            {
                AspNetPager1.RecordCount = loglist[0].rowscount;
                Repeater1.DataSource = loglist;
                Repeater1.DataBind();
                this.number.InnerText = "（共" + AspNetPager1.RecordCount + "条）";
            }
            else
            {
                AspNetPager1.RecordCount = 1;
                Repeater1.DataSource = loglist;
                Repeater1.DataBind();
                this.number.InnerText = "（共0条）";
            }   
            
        }
        #region 显示角色
        public static string GetUser(string val)
        {
            if (val == "1")
            {
                return "超级管理员";
            }
            if (val == "2")
            {
                return "子管理员";
            }
            else if (val == "4")
            {
                return "学生";
            }
            if (val == "6")
            {
                return "子管理员<br/>学生";
            }
            if (val == "8")
            {
                return "教师";
            }
            if (val == "10")
            {
                return "子管理员<br/>教师";
            }
            if (val == "15")
            {
                return "超级管理员<br/>子管理员<br/>学生<br/>教师";
            }
            if (val == "16")
            {
                return "系统外用户";
            }
            if (val == "31")
            {
                return "万能用户";
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            DataBinder(AspNetPager1.CurrentPageIndex);

        }
        public void GetSession()
        {
            IES.JW.Model.Log log = Session["Conditions"] as IES.JW.Model.Log;
            this.BeginTime.Value = log.StartTime.ToString("yyyy-MM-dd ");
            this.EndTime.Value = log.EndTime.ToString("yyyy-MM-dd ");
            this.Key.Value = log.Key;
        }
        //搜索
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            DateTime beginTime = Convert.ToDateTime(this.BeginTime.Value);
            DateTime endTime = Convert.ToDateTime(this.EndTime.Value);
            string key = this.Key.Value;
            IES.JW.Model.Log _log = new IES.JW.Model.Log { Key = key, Role = "1", StartTime = beginTime, EndTime = endTime };
            Session["Log"] = _log;
            DataBinder(1);
        }
        //切换PageSize
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            AspNetPager1.PageSize = Int32.Parse(DropDownList1.SelectedValue);
            Browse.SetPageSize(AspNetPager1.PageSize);
            DataBinder(1);
        }
    }
}