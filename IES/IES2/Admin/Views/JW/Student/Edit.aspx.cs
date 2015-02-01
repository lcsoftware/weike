using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace Admin.Views.JW.Student
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

        private void DataBinder(int id)
        {
            IES.JW.Model.User user = new IES.JW.Model.User { UserID = id };
            IES.G2S.JW.BLL.UserBLL userbll = new IES.G2S.JW.BLL.UserBLL();
            IES.JW.Model.User _user = userbll.UserTS_Get(user);
            //绑定个人信息数据
            this.UserNo.Value = _user.UserNo;
            this.UserName.Value = _user.UserName;
            this.UserNameEn.Value = _user.UserNameEn;
            this.Sex.SelectedValue = _user.Gender.ToString();
            this.Organization.SelectedValue = _user.OrganizationID.ToString();
            this.Specialty.SelectedValue = _user.SpecialtyID.ToString();
            this.Class.SelectedValue = _user.ClassID.ToString();
            this.EntryTime.SelectedValue = _user.EntryDate.ToString();
            this.Tel.Value=_user.Tel;
            this.Mobile.Value = _user.Mobile;
            this.Email.Value = _user.Email;
            this.oEditor1.Value=_user.Brief;
            this.imgyl.Src = _user.img;

        }
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

            Specialty.DataSource = parmlist.splist;
            Specialty.DataTextField = "SpecialtyName";
            Specialty.DataValueField = "SpecialtyID";
            Specialty.DataBind();
            Specialty.Items.Insert(0, "请选择");

            Class.DataSource = parmlist.clslist;
            Class.DataTextField = "ClassName";
            Class.DataValueField = "ClassID";
            Class.DataBind();
            Class.Items.Insert(0, "请选择");

            DateTime tnow = DateTime.Now;//现在时间
            ArrayList AlYear = new ArrayList();
            var n = int.Parse(tnow.Date.Year.ToString());
            int i;
            for (i = n - 8; i <= n; i++)
                AlYear.Add(i);
            EntryTime.DataSource = AlYear;
            EntryTime.DataBind();//绑定年
            EntryTime.Items.Insert(0, "请选择");

        }
        public void Sumbit()
        {
            int id = Convert.ToInt32(Request["id"]);
            //绑定个人信息数据
            string loginname = this.LoginName.Value;
            string pwd = this.Pwd.Value;
            string userno = this.UserNo.Value;
            string username = this.UserName.Value;
            string usernameen = this.UserNameEn.Value;
            int gender = Convert.ToInt32(this.Sex.SelectedValue);
            string email = this.Email.Value;
            string tel = this.Tel.Value;
            string mobile = this.Mobile.Value;
            int orgid = 0;
            if (this.Organization.SelectedValue != "请选择")
                orgid = Convert.ToInt32(this.Organization.SelectedValue);
            int spid = 0;
            if (this.Specialty.SelectedValue != "请选择")
                spid = Convert.ToInt32(this.Specialty.SelectedValue);
            int classid = 0;
            if (this.Class.SelectedValue != "请选择")
                classid = Convert.ToInt32(this.Class.SelectedValue);
            int entrydate = 0;
            if (this.EntryTime.SelectedValue != "请选择")
                entrydate = Convert.ToInt32(this.EntryTime.SelectedValue);
            string content = this.oEditor1.Value;
            int role = 4;

            IES.G2S.JW.BLL.UserBLL userbll = new IES.G2S.JW.BLL.UserBLL();
            if (id != 0)//修改
            {
                IES.JW.Model.User _user = new IES.JW.Model.User { UserID = id, UserNo = userno, UserName = username, UserNameEn = usernameen, Gender = gender, Email = email, Tel = tel, Mobile = mobile, OrganizationID = orgid, SpecialtyID = spid, ClassID = classid, EntryDate = entrydate, Brief = content, UserType = role };
                bool result = userbll.User_Upd(_user);
                if (result == true)
                {
                   shangchuan(id);
                   Response.Write("<script>alert('修改成功');location.href='Student.aspx?PID=A115';</script>");


                }
            }
            else//新增
            {
                IES.JW.Model.User _user = new IES.JW.Model.User { LoginName = loginname, Pwd = pwd, UserNo = userno, UserName = username, UserNameEn = usernameen, Gender = gender, Email = email, Tel = tel, Mobile = mobile, OrganizationID = orgid, SpecialtyID = spid, ClassID = classid, EntryDate = entrydate, Brief = content, UserType = role };
                IES.JW.Model.User result = userbll.User_ADD(_user);
                if (result.output == null)
                {
                    shangchuan(id);
                    Response.Write("<script>alert('新增成功');location.href='Student.aspx?PID=A115';</script>");
                }
                else
                {
                    Response.Write("<script>alert('" + result.output + "');</script>");
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
        protected void update_Click(object sender, EventArgs e)
        {
            Sumbit();
        }

    }
}