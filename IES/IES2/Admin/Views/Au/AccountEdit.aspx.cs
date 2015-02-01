using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.Au
{
    public partial class AccountEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["id"] != null)
                {
                    this.Oname.Style.Add("display", "none");
                    this.Tname.Style.Add("display", "block");
                    this.Opwd.Style.Add("display", "none");
                    this.Tpwd.Style.Add("display", "none");
                    this.Upwd.Style.Add("display", "block");
                    int id = Convert.ToInt32(Request["id"]);
                    DataBinder(id);
                }                    
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        public void DataBinder(int id)
        {
            IES.JW.Model.User user = new IES.JW.Model.User { UserID = id };
            IES.G2S.JW.BLL.UserBLL userbll = new IES.G2S.JW.BLL.UserBLL();
            IES.JW.Model.User _user = userbll.User_Get(user);

            this.UserID.Value = _user.UserID.ToString();
            this.Tname.InnerText = _user.UserNo;
            this.LoginName.Value = _user.LoginName;
            this.Pwd.Value = _user.Pwd;
            this.UserName.Value = _user.UserName;
            if (_user.Gender == 1)
            {
                this.RadioButton1.Checked = true;
            }
            else
            {
                this.RadioButton2.Checked = true;
            }
            this.UserNo.Value = _user.UserNo;
            this.Tel.Value = _user.Tel;
            this.Email.Value = _user.Email;
        }

        /// <summary>
        /// 新增或修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Update_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request["id"]);
            string LoginName = this.LoginName.Value;
            string Pwd = this.Pwd.Value;
            string UserNo = this.UserNo.Value;
            string UserName = this.UserName.Value;
            int Gender = 1;
            if (this.RadioButton1.Checked == true)
            {
                Gender = 1;
            }
            else if (this.RadioButton2.Checked == true)
            {
                Gender = 0;
            }
            string Tel = this.Tel.Value;
            string Email = this.Email.Value;
            int Entrydate = 0;
            IES.G2S.JW.BLL.UserBLL userbll = new IES.G2S.JW.BLL.UserBLL();
            if (id > 0)
            {
                IES.JW.Model.User _user = new IES.JW.Model.User { UserID = id, UserNo = UserNo, UserName = UserName, Gender = Gender, Email = Email, Tel = Tel };
                bool result = userbll.User_Upd(_user);
                if (result == true)
                {
                    Response.Write("<script>alert('修改成功');location.href='Account.aspx';</script>");
                }
            }
            else
            {
                IES.JW.Model.User _user = new IES.JW.Model.User { UserID = id, UserNo = UserNo, UserName = UserName, LoginName = LoginName, Pwd = Pwd, Gender = Gender, Email = Email, Tel = Tel, EntryDate=Entrydate};
                IES.JW.Model.User user = userbll.User_ADD(_user);
                if (user.output == null)
                {
                    Response.Write("<script>alert('新增成功');location.href='Account.aspx';</script>");
                }
                else
                {
                    Response.Write("<script>alert('" + user.output + "');</script>");
                }
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Write("<script>location.href='Account.aspx';</script>");
        }
    }
}