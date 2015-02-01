using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.Portal.Link
{
    public partial class Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(Request["id"]!=null)
                {
                    int id=Convert.ToInt32(Request["id"]);
                    DataBinder(id);
                }
            }
        }
        private void DataBinder(int id)
        {
            IES.Portal.Model.Link _link = new IES.Portal.Model.Link { LinkID = id };
            IES.G2S.Portal.BLL.LinkBLL linkbll = new IES.G2S.Portal.BLL.LinkBLL();
            IES.Portal.Model.Link linklist = linkbll.Link_Get(_link);
            //绑定数据
            this.LinkTitle.Value = linklist.Title;
            this.LinkUrl.Value = linklist.URL;

        }
        public void Sumbit()
        {
            int id = Convert.ToInt32(Request["id"]);
            string linkTitle = this.LinkTitle.Value;
            string linkUrl = this.LinkUrl.Value;

            IES.G2S.Portal.BLL.LinkBLL linkbll = new IES.G2S.Portal.BLL.LinkBLL();
            //通过ID判断是新增还是修改
            if (id != 0)
            {
                IES.Portal.Model.Link _link = new IES.Portal.Model.Link{ LinkID = id, Title = linkTitle,URL=linkUrl};
                bool result = linkbll.Link_Upd(_link);
                if (result == true)
                { Response.Write("<script>alert('修改成功');location.href='Link.aspx';</script>"); }
            }
            else
            {
                IES.Portal.Model.Link _link = new IES.Portal.Model.Link { Title = linkTitle, URL = linkUrl };
                IES.Portal.Model.Link result = linkbll.Link_ADD(_link);
                Response.Write("<script>alert('新增成功');location.href='Link.aspx';</script>");
            }
        }
        public void Cancel()
        {
            Response.Write("<script>location.href='Link.aspx';</script>");
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