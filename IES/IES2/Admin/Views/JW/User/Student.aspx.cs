using IES.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.JW.User
{
    public partial class Student : System.Web.UI.Page
    {
        public static string sorting = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AspNetPager1.PageSize = Browse.PageSize;
                DropDownList1.SelectedValue = Browse.PageSize.ToString();
                DataBinder(1);
            }
        }

        #region 绑定数据
        private void DataBinder(int pageindex)
        {
            string key = "";
            int role = 4;
            int orgid=-1;
            int spid=-1;
            int classid=-1;
            int islocked = -1;
            int isregister = -1;


            IES.JW.Model.User _user = new IES.JW.Model.User { Key = key, Role = role, modevalue = 32, OrganizationID = orgid, SpecialtyID = spid, ClassID = classid, IsLocked = islocked, IsRegister = isregister };
            Session["Student"] = _user;
            IES.G2S.JW.BLL.UserBLL userbll = new IES.G2S.JW.BLL.UserBLL();
            List<IES.JW.Model.User> userlist = userbll.User_List(_user, pageindex, AspNetPager1.PageSize);
            if (userlist.Count > 0)
            {
                AspNetPager1.RecordCount = userlist[0].rowscount;
                Repeater1.DataSource = userlist;
                Repeater1.DataBind();
                this.number.InnerText = "（共" + AspNetPager1.RecordCount + "条）";
            }
            else
            {
                AspNetPager1.RecordCount = 1;
                Repeater1.DataSource = userlist;
                Repeater1.DataBind();
                this.number.InnerText = "（共0条）";
            }

        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            DataBinder(AspNetPager1.CurrentPageIndex);
        }
        public void GetSession()
        {
            //IES.Portal.Model.News news = Session["News"] as IES.Portal.Model.News;
            //this.BeginTime.Value = news.StartTime.ToString();
            //this.EndTime.Value = news.EndTime.ToString();
            //this.NewsKey.Value = news.Key;
            //this.Section.SelectedValue = news.SectionID.ToString();
        }
        public void DataSection()
        {
            //List<IES.Portal.Model.NewsSection> sectionlist = new IES.G2S.Portal.BLL.NewsBLL().NewsSection_List();
            //Section.DataSource = sectionlist;
            //Section.DataTextField = "SectionName";
            //Section.DataValueField = "SectionID";
            //Section.DataBind();
            //Section.Items.Insert(0, new ListItem("全部", "-1"));
        }
        #endregion

        #region 操作
        //删除
        protected void btnInfo_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.hfID.Value);
            IES.Portal.Model.News _news = new IES.Portal.Model.News { NewsID = id };
            IES.G2S.Portal.BLL.NewsBLL newsbll = new IES.G2S.Portal.BLL.NewsBLL();
            bool result = newsbll.News_Del(_news);
            if (result == true)
            { DataBinder(1); }
        }
        //批量删除
        protected void BatchDel_Click(object sender, EventArgs e)
        {
            DelBatch();
        }
        public void DelBatch()
        {
            string IDS = this.hfIDS.Value;
            IES.G2S.Portal.BLL.NewsBLL newsbll = new IES.G2S.Portal.BLL.NewsBLL();
            bool result = newsbll.News_Batch_Del(IDS);
            if (result == true)
            { DataBinder(1); }
        }
        //搜索
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            DataBinder(1);
        }
        //切换PageSize
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            AspNetPager1.PageSize = Int32.Parse(DropDownList1.SelectedValue);
            Browse.SetPageSize(AspNetPager1.PageSize);
            DataBinder(1);
        }
        //排序
        protected void Order_Click(object sender, EventArgs e)
        {
            if (sorting != "order")
            { sorting = "order"; }
            else
            { sorting = "desc"; }
            DataBinder(1);
        }
        #endregion
    }        
}