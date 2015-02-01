using IES.JW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.Config
{
    public partial class Site : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { DataBinder(); }
        }
        #region 绑定数据
        private void DataBinder()
        {
            IES.G2S.JW.BLL.OCDefaultColumnBLL columnbll = new IES.G2S.JW.BLL.OCDefaultColumnBLL();
            List<OCDefaultColumn> columnlist = columnbll.OCDefaultColumn_List();
            Repeater1.DataSource = columnlist;
            Repeater1.DataBind();
        }
        #endregion

        #region 操作
        public void Sumbit(int i)
        {
            string parms = this.hfID.Value;
            var ary = parms.Split(',');
            int id = Convert.ToInt32(ary[0]);
            int orde = Convert.ToInt32(ary[1]);
            if (i == 1 && orde == 1) { }
            else
            {
                IES.JW.Model.OCDefaultColumn _column = new IES.JW.Model.OCDefaultColumn { ColumID = id, Orde = orde, topbm = i };
                IES.G2S.JW.BLL.OCDefaultColumnBLL columnbll = new IES.G2S.JW.BLL.OCDefaultColumnBLL();
                bool result = columnbll.OCDefaultColumn_Edit(_column);
            }
        }
        //上移
        protected void btnTp_Click(object sender, EventArgs e)
        {
            Sumbit(1);
            DataBinder();
        }
        //下移
        protected void btnBm_Click(object sender, EventArgs e)
        {
            Sumbit(2);
            DataBinder();
        }
        //重命名
        protected void btnReName_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.hfID.Value);
            string name = Request["Title" + id].ToString();
            IES.JW.Model.OCDefaultColumn _column = new IES.JW.Model.OCDefaultColumn { ColumID = id, Name = name };
            IES.G2S.JW.BLL.OCDefaultColumnBLL columnbll = new IES.G2S.JW.BLL.OCDefaultColumnBLL();
            bool result = columnbll.OCDefaultColumn_ReName(_column);
            DataBinder();
        }
        //新增
        protected void btnADD_Click(object sender, EventArgs e)
        {
            string name = pinpai1.Value;
            IES.JW.Model.OCDefaultColumn _column = new IES.JW.Model.OCDefaultColumn { Name = name };
            IES.G2S.JW.BLL.OCDefaultColumnBLL columnbll = new IES.G2S.JW.BLL.OCDefaultColumnBLL();
            IES.JW.Model.OCDefaultColumn result = columnbll.OCDefaultColumn_ADD(_column);
            pinpai1.Value = "";
            DataBinder();
        }
        //删除
        protected void btnInfo_Click(object sender, EventArgs e)
        {
            string parms = this.hfID.Value;
            var ary = parms.Split(',');
            int id = Convert.ToInt32(ary[0]);
            int orde = Convert.ToInt32(ary[1]);
            IES.JW.Model.OCDefaultColumn _column = new IES.JW.Model.OCDefaultColumn { ColumID = id, Orde = orde };
            IES.G2S.JW.BLL.OCDefaultColumnBLL columnbll = new IES.G2S.JW.BLL.OCDefaultColumnBLL();
            bool result = columnbll.OCDefaultColumn_Del(_column);
            DataBinder();
        }
        #endregion
    }
}