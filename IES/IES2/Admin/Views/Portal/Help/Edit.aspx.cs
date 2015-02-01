using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.Portal.Help
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
            IES.Portal.Model.Help _help = new IES.Portal.Model.Help { HelpID = id };
            IES.G2S.Portal.BLL.HelpBLL helpbll = new IES.G2S.Portal.BLL.HelpBLL();
            IES.Portal.Model.Help helplist = helpbll.Help_Get(_help);
            //绑定数据
            this.HelpTitle.Value = helplist.Title;
            this.oEditor1.Value = helplist.Content;

        }
        public void Sumbit()
        {
            int id = Convert.ToInt32(Request["id"]);
            string helpTitle = this.HelpTitle.Value;
            string content = this.oEditor1.Value;

            IES.G2S.Portal.BLL.HelpBLL helpbll = new IES.G2S.Portal.BLL.HelpBLL();
            //通过ID判断是新增还是修改
            if (id != 0)
            {
                IES.Portal.Model.Help _help = new IES.Portal.Model.Help { HelpID = id, Title = helpTitle, Content = content };
                bool result = helpbll.Help_Upd(_help);
                if (result == true)
                { Response.Write("<script>alert('修改成功');location.href='Help.aspx';</script>"); }
            }
            else
            {
                IES.Portal.Model.Help _help = new IES.Portal.Model.Help() { Title = helpTitle, Content = content };
                IES.Portal.Model.Help result = helpbll.Help_ADD(_help);
                Response.Write("<script>alert('新增成功');location.href='Help.aspx';</script>");
            }
        }
        public void Cancel()
        {
            Response.Write("<script>location.href='Help.aspx';</script>");
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