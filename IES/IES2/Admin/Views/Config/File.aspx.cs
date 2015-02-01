using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.Config
{
    public partial class File : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            { DataBinder(); }
            
        }
        #region 绑定数据
        private void DataBinder()
        {
            this.Bjq.Value = "";
            this.Zlk.Value = "";
        }
        #endregion

        #region 操作
        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //string bjq = this.Bjq.Value;
            //string zlk = this.Zlk.Value;
            DataBinder();

        }
        #endregion
    }
}