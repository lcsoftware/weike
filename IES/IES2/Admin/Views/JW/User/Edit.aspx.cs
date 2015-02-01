using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.JW.User
{
    public partial class Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Binding();
                if (Request["id"] != null)
                {
                    int id = Convert.ToInt32(Request["id"]);
                    DataBinder(id);
                }
            }
        }

        public void Binding()
        {
            ObtainOrganization();
        }

        #region  绑定数据

        /// <summary>
        /// 所属机构
        /// </summary>
        public void ObtainOrganization()
        {
            List<IES.JW.Model.Organization> organlist = new IES.G2S.JW.BLL.OrganizationBLL().NewsOrganization_List();
            Organization.DataSource = organlist;
            Organization.DataTextField = "OrganizationName";
            Organization.DataValueField = "OrganizationID";
            Organization.DataBind();
        }
        #endregion

        private void DataBinder(int id)
        {
            //IES.JW.Model.User user = new IES.JW.Model.User { UserID = id };
            //IES.G2S.JW.BLL.UserBLL userbll = new IES.G2S.JW.BLL.UserBLL();
            //IES.JW.Model.User _user = userbll.User_Get(user);

            //this.LoginName.Value = _user.LoginName;
            //this.UserNo.Value = _user.UserNo;
            //this.UserName.Value = _user.UserName;
            //this.EnglishName.Value = _user.UserNameEn;
            //this.OrganName.Value = _user.OrganizationName;
            //this.Email.Value = _user.Email;
            //this.iPhone.Value = _user.Mobile;
            //this.telephone.Value = _user.Tel;
            //this.Content.Value = _user.Brief;
        }

        protected void update_Click(object sender, EventArgs e)
        {
            //int id = Convert.ToInt32(Request["id"]);
            //string pwd = this.Pwd.Value;
            //string UserNo = this.UserNo.Value;
            //string UserName = this.UserName.Value;
            //string UserNameEn = this.EnglishName.Value;
            ////int OrganizationID = this.OrganName.Value;
            //string Email = this.Email.Value;
            //string Mobile = this.iPhone.Value;
            //string tel = this.telephone.Value;
            //string brief = this.Content.Value;

            //IES.JW.Model.User user = new IES.JW.Model.User { UserID = id };
            //IES.G2S.JW.BLL.UserBLL userbll=new IES.G2S.JW.BLL.UserBLL();
            //IES.JW.Model.User _user = userbll.User_Get(user);
            //string Ranks = _user.Ranks;
            //int EntryDate = Convert.ToInt32(_user.EntryDate);
            //int SpecialtyID = _user.SpecialtyID;
            //int ClassID = _user.ClassID;
            //if (id > 0)
            //{
            //    IES.JW.Model.User _class = new IES.JW.Model.User { UserID = id,UserNo=UserNo, UserName = UserName, UserNameEn = UserNameEn,Email=Email,Tel=tel,Mobile=Mobile,Ranks=Ranks,EntryDate=EntryDate,SpecialtyID=SpecialtyID,ClassID=ClassID };
            //    bool result = userbll.User_Upd(_class);
            //    if (result == true)
            //    {
            //        Response.Write("<script>alert('修改成功');location.href='News.aspx';</script>");
            //    }
            //}

        }

        protected void cancel_Click(object sender, EventArgs e)
        {

        }
    }
}