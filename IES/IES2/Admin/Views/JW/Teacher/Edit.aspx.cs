using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.JW.Teacher
{
    public partial class Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataParm();
                if (Request["id"] != null)
                {
                    int id = Convert.ToInt32(Request["id"]);
                    DataBinder(id);
                }
            }
        }

        #region  绑定数据

        //绑定条件
        public void DataParm()
        {
            IES.G2S.JW.BLL.ParmInfoBLL parmbll = new IES.G2S.JW.BLL.ParmInfoBLL();
            IES.JW.Model.ParmInfo parmlist = parmbll.Parm_Info_List();

            Organization.DataSource = parmlist.orglist;
            Organization.DataTextField = "OrganizationName";
            Organization.DataValueField = "OrganizationID";
            Organization.DataBind();
            Organization.Items.Insert(0, "请选择");
        }
        #endregion

        private void DataBinder(int id)
        {
            IES.JW.Model.User user = new IES.JW.Model.User { UserID = id };
            IES.G2S.JW.BLL.UserBLL userbll = new IES.G2S.JW.BLL.UserBLL();
            IES.JW.Model.User _user = userbll.UserTS_Get(user);

            //this.LoginName.Value = _user.LoginName;
            this.UserNo.Value = _user.UserNo;
            this.Name.Value = _user.UserName;
            this.EnglishName.Value = _user.UserNameEn;
            if (_user.Gender == 1)
            {
                this.Sex.Items[0].Selected = true;
            }
            else
            {
                this.Sex.Items[1].Selected = true;
            }
            this.Email.Value = _user.Email;
            this.Tel.Value = _user.Tel;
            this.Organization.SelectedValue = _user.OrganizationID.ToString();
            //this.Specialty.SelectedValue = _user.SpecialtyID.ToString();
            this.Identity.SelectedValue = _user.IsInSchool.ToString();
            //this.Appointment.SelectedValue = _user.EntryDate.ToString();
            this.Gateway.SelectedValue = _user.IsShow.ToString();
            this.Ranks.Value = _user.Ranks;
            this.oEditor1.Value = _user.Brief;

            this.imgyl.Src = _user.img;
        }

        protected void update_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request["id"]);
            string UserNo = this.UserNo.Value;
            string UserName = this.Name.Value;
            string NameEn = this.EnglishName.Value;
            string LoginName = this.LoginName.Value;
            string Pwd = this.Pwd.Value;
            int Gender;
            if (this.Sex.Items[0].Selected == true)
            {
                Gender = 1;
            }
            else
            {
                Gender = 0;
            }
            string Email = this.Email.Value;
            string Tel = this.Tel.Value;
            int organizationID = Convert.ToInt32(this.Organization.SelectedValue);
            string ranks = this.Ranks.Value;
            //int EntryDate= Convert.ToInt32(this.Appointment.SelectedValue);
            //int specialtyID = Convert.ToInt32(this.Specialty.SelectedValue);
            int classID = 0;
            int UserType = 8;
            int isregister = 0;
            string brief = this.oEditor1.Value;
            int IsinSchool = Convert.ToInt32(this.Identity.SelectedValue);
            int Isshow = Convert.ToInt32(this.Gateway.SelectedValue);
            IES.G2S.JW.BLL.UserBLL userbll = new IES.G2S.JW.BLL.UserBLL();
            if (id > 0)
            {
                IES.JW.Model.User _class = new IES.JW.Model.User { UserID = id, UserNo = UserNo, UserName = UserName, UserNameEn = NameEn, Gender = Gender, Email = Email, Tel = Tel, OrganizationID = organizationID, Ranks = ranks, ClassID = classID, IsRegister = isregister, Brief = brief, IsInSchool = IsinSchool, IsShow = Isshow };
                bool result = userbll.User_Upd(_class);
                if (result == true)
                {
                    shangchuan(id);
                    Response.Write("<script>alert('修改成功');location.href='Teacher.aspx?PID=A115';</script>");
                }
            }
            else
            {
                IES.JW.Model.User _class = new IES.JW.Model.User { UserID = id, UserNo = UserNo, UserName = UserName, UserNameEn = NameEn, LoginName = LoginName, Pwd = Pwd, Gender = Gender, Email = Email, Tel = Tel, Mobile = "", OrganizationID = organizationID, Ranks = ranks, ClassID = classID, UserType = UserType, IsRegister = isregister, Brief = brief, IsInSchool = IsinSchool, IsShow = Isshow };
                IES.JW.Model.User clas = userbll.User_ADD(_class);
                if (clas.output == null)
                {
                    shangchuan(id);
                    Response.Write("<script>alert('新增成功');location.href='Teacher.aspx?PID=A115';</script>");
                }
                else
                {
                    Response.Write("<script>alert('" + clas.output + "');</script>");
                }
            }

        }
        //上传图片
        public void shangchuan(int id)
        {
            bool rs = false;
            List<IES.Resource.Model.Attachment> list = IES.Service.FileService.AttachmentUpload();
            if (list.Count == 1)
            {
                string guid = list[0].Guid;
                int sourceid = id;
                string source = "User";
                IES.Resource.Model.Attachment atmt = new IES.Resource.Model.Attachment { Guid = guid, Source = source, SourceID = sourceid };
                rs = IES.Service.FileService.AttachmentRelation(atmt);
            }
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            Response.Write("<script>location.href='Teacher.aspx';</script>");
        }
    }
}