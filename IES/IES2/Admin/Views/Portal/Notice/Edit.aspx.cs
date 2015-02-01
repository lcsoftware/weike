using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.Portal.Notice
{
    public partial class Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["id"] != null)
                {
                    int id = Convert.ToInt32(Request["id"]);
                    DataBinder(id);
                }
            }
        }
        private void DataBinder(int id)
        {
            IES.JW.Model.Notice _notice = new IES.JW.Model.Notice { NoticeID = id };
            IES.G2S.JW.BLL.NoticeBLL noticebll = new IES.G2S.JW.BLL.NoticeBLL();
            IES.JW.Model.Notice noticelist = noticebll.Notice_Get(_notice);

            this.txtTitle.Value = noticelist.Title;
            this.IsTop.Checked = noticelist.IsTop;
            this.oEditor1.Value = noticelist.Conten;

        }
        public void Sumbit()
        {
            int id = Convert.ToInt32(Request["id"]);
            string txtTitle = this.txtTitle.Value;
            bool isTop = this.IsTop.Checked;
            string conten = this.oEditor1.Value;

            IES.G2S.JW.BLL.NoticeBLL noticebll = new IES.G2S.JW.BLL.NoticeBLL();

            if (id != 0)
            {
                IES.JW.Model.Notice _notice = new IES.JW.Model.Notice{ NoticeID = id, Title = txtTitle, IsTop = isTop,  Conten = conten };
                bool result = noticebll.Notice_Upd(_notice);
                if (result == true)
                { Response.Write("<script>alert('修改成功');location.href='Notice.aspx';</script>"); }
            }
            else
            {
                IES.JW.Model.Notice _notice = new IES.JW.Model.Notice { Title = txtTitle, IsTop = isTop, Conten = conten, UserID=1 };
                IES.JW.Model.Notice result = noticebll.Notice_ADD(_notice);
                Response.Write("<script>alert('新增成功');location.href='Notice.aspx';</script>");
            }
        }
        public void Cancel()
        {
            Response.Write("<script>location.href='Notice.aspx';</script>");
        }

        protected void update_Click(object sender, EventArgs e)
        {
            Sumbit();
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }
    }
}