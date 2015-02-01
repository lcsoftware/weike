using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.Config
{
    public partial class SMS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { DataBinder(); }
        }
        #region 绑定数据
        private void DataBinder()
        {
            string key = "";
            if (Session["SMS"] != null)
            {
                key = Session["SMS"].ToString();
            }
            Repeater1.DataSource = null;
            Repeater1.DataBind();
            this.znum.InnerText = "?"+"条";
            this.snum.InnerText = "?"+"条";
        }       

        #endregion

        #region 操作
        //删除
        protected void btnInfo_Click(object sender, EventArgs e)
        {
            //string zhtype = this.zhType.Value;
            DataBinder();

        }
        //搜索
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            string key = this.Key.Value;
            Session["SMS"] = key;
            DataBinder();
        }
        
        //锁定
        protected void btnLock_Click(object sender, EventArgs e)
        {
            int id = 1;
            DataBind();
        }
        #endregion
    }
}