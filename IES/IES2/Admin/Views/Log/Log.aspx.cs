using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IES.G2S.SYS.IBLL;
using IES.SYS.Model;

namespace Admin.Views.Log
{
    public partial class Log : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                AspNetPager1.PageSize = 20;
                DataBinder(1);

            }
        }

        private void DataBinder(int pageindex)
        {
            string beginTime = this.BeginTime.Value;
            string endTime = this.EndTime.Value;
            IES.SYS.Model.Log _log = new IES.SYS.Model.Log { Loginname = string.Empty, Userno = string.Empty,Role="1", Startdate = Convert.ToDateTime(beginTime), Enddate = Convert.ToDateTime(endTime) };
            IES.G2S.SYS.BLL.LogBLL logbll = new IES.G2S.SYS.BLL.LogBLL();

            List<IES.SYS.Model.Log> loglist = logbll.Log_List(_log, pageindex, AspNetPager1.PageSize);
            if (loglist.Count > 0)
            {
                AspNetPager1.RecordCount = loglist[0].rowscount;
                Repeater1.DataSource = loglist;
                Repeater1.DataBind();
            }
        }
        //搜索
        public string DataSelect()
        {
            DataBinder(1);
            return null;
        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {

            DataBinder(AspNetPager1.CurrentPageIndex);

        }

    }
}