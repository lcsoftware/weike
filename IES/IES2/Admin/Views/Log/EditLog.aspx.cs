using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IES.Service.Common;
namespace Admin.Views.Log
{
    public partial class EditLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                AspNetPager1.PageSize = Browse.PageSize;
                //DropDownList1.SelectedValue = Browse.PageSize.ToString();
                DataBinder(1);
            }
        }

        private void DataBinder(int pageindex)
        {
            DateTime beginTime = Convert.ToDateTime(this.BeginTime.Value);
            DateTime endTime = Convert.ToDateTime(this.EndTime.Value);
            string loginName = this.OperationNum.SelectedValue;
            string operType = this.OperationType.SelectedValue;
            string module = this.Module.Value;
            string operObj = this.OperationObj.Value;
            IES.JW.Model.Log _log = new IES.JW.Model.Log { LoginName = string.Empty, UserNo = string.Empty, Role = "1", StartTime = beginTime, EndTime = endTime };
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
        //删除
        protected void btnInfo_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.hfID.Value);
            IES.Portal.Model.News _news = new IES.Portal.Model.News { NewsID = id };
            IES.G2S.Portal.BLL.NewsBLL newsbll = new IES.G2S.Portal.BLL.NewsBLL();
            bool result = newsbll.News_Del(_news);
            if (result == true)
            {
                DataBinder(1);
            }
        }

        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {

            DataBinder(AspNetPager1.CurrentPageIndex);

        }
        //搜索
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            DataBinder(1);
        }
        //切换PageSize
        //protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    AspNetPager1.PageSize = Int32.Parse(DropDownList1.SelectedValue);
        //    Browse.SetPageSize(AspNetPager1.PageSize);
        //    DataBinder(1);
        //}
    }
}