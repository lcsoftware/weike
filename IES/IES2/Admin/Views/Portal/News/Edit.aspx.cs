using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.Portal.News
{
    public partial class Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSection();
                if (Request["id"] != null)
                {
                    int id = Convert.ToInt32(Request["id"]);
                    DataBinder(id);
                }
            }
        }
        private void DataBinder(int id)
        {
            IES.Portal.Model.News _news = new IES.Portal.Model.News { NewsID = id };
            IES.G2S.Portal.BLL.NewsBLL newsbll = new IES.G2S.Portal.BLL.NewsBLL();
            IES.Portal.Model.News newslist = newsbll.News_Get(_news);
            //绑定数据
            this.txtTitle.Value = newslist.Title;
            this.IsImp.Checked = newslist.IsImportant;
            this.IsTop.Checked = newslist.IsTop;
            this.BeginTime.Value = newslist.CreateDate.ToString();
            this.EndTime.Value = newslist.EndDate.ToString();
            this.oEditor1.Value = newslist.Content;
            this.Section.SelectedValue = newslist.SectionID.ToString();
            this.Sys.SelectedValue = newslist.SysID.ToString();

        }
        //得到新闻公告所属模块
        public void DataSection()
        {
            List<IES.Portal.Model.NewsSection> sectionlist = new IES.G2S.Portal.BLL.NewsBLL().NewsSection_List();
            Section.DataSource = sectionlist;
            Section.DataTextField = "SectionName";
            Section.DataValueField = "SectionID";
            Section.DataBind();
            Section.Items.Insert(0, "请选择");
        }
        public void Sumbit()
        {
            int id = Convert.ToInt32(Request["id"]);
            string txtTitle = this.txtTitle.Value;
            DateTime beginTime = Convert.ToDateTime(this.BeginTime.Value);
            DateTime endTime = Convert.ToDateTime(this.EndTime.Value);
            bool IsImp = this.IsImp.Checked;
            bool IsTop = this.IsTop.Checked;
            string content = this.oEditor1.Value;
            int sectionid = Convert.ToInt32(this.Section.SelectedValue);
            int sysid = Convert.ToInt32(this.Sys.SelectedValue);

            IES.G2S.Portal.BLL.NewsBLL newsbll = new IES.G2S.Portal.BLL.NewsBLL();
            //通过ID判断新增还是修改
            if (id != 0)
            {
                IES.Portal.Model.News _news = new IES.Portal.Model.News { NewsID = id, Title = txtTitle, IsImportant = IsImp, IsTop = IsTop, CreateDate = beginTime, EndDate = endTime, Content = content, SectionID = sectionid, SysID = sysid };
                bool result = newsbll.News_Upd(_news);
                if (result == true)
                { Response.Write("<script>alert('修改成功');location.href='News.aspx';</script>"); }
            }
            else
            {
                IES.Portal.Model.News _news = new IES.Portal.Model.News { Title = txtTitle, IsImportant = IsImp, IsTop = IsTop, CreateDate = beginTime, EndDate = endTime, Content = content };
                IES.Portal.Model.News result = newsbll.News_ADD(_news);
                Response.Write("<script>alert('新增成功');location.href='News.aspx';</script>");
            }
        }
        public void Cancel()
        {
            Response.Write("<script>location.href='News.aspx';</script>");
        }
        protected void update_Click(object sender, EventArgs e)
        {
            if (this.txtTitle.Value == "" || this.oEditor1.Value=="")
            { Response.Write("<script>alert('新闻标题或新闻内容不能为空！')</script>"); }
            else
            { Sumbit(); }
        }
        protected void cancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }
    }
}