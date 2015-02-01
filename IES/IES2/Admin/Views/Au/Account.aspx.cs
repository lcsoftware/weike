using IES.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.Au
{
    public partial class Account : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataParm();
            if (!IsPostBack)
            {
                DataBinder(1);
                if (Session["UserType"] == null)
                    Session["UserType"] = "-1";
            }
        }

        public void DataParm()
        {
            IES.G2S.JW.BLL.ParmInfoBLL parmbll = new IES.G2S.JW.BLL.ParmInfoBLL();
            IES.JW.Model.ParmInfo parmlist = parmbll.Parm_Info_List();

            Repeater1.DataSource = parmlist.orglist;
            Repeater1.DataBind();
        }
        //得到Session添加样式
        public string Getclass()
        {
            string parms = Session["UserType"].ToString();
            ClientScript.RegisterStartupScript(ClientScript.GetType(), "../Portal/Edit.js", "<script>Userclass('" + parms + "');</script>");
            return null;
        }
        #region 列表显示
        private void DataBinder(int pageindex)
        {
            if (Session["Teacher"] != null)
            { GetSession(); }
            int organizationID;
            if (Session["UserType"] != null)
            {
                organizationID = Convert.ToInt32(this.hfID.Value);
                Session["UserType"] = organizationID;
            }
            else
            {
                organizationID = -1;
            }
            
            string key = this.txtKey.Value;
            int role = 31;
            int modevalue = 32;
            int specialtyid = -1;
            int classid = -1;
            int islocked = -1;
            int isregister = -1;
            int isassistant = -1;
            int isshow = -1;
            int isinschool = -1;
            int entrydate = -1;
            IES.JW.Model.User user = new IES.JW.Model.User { Key = key, Role = role, modevalue = modevalue, OrganizationID = organizationID, SpecialtyID = specialtyid, ClassID = classid, IsLocked = islocked, IsRegister = isregister, IsAssistant = isassistant, IsShow = isshow, IsInSchool = isinschool, EntryDate = entrydate };
            IES.G2S.JW.BLL.UserBLL userbll = new IES.G2S.JW.BLL.UserBLL();
            List<IES.JW.Model.User> userlist = userbll.User_List(user, pageindex, 20);
            if (userlist != null)
                if (userlist.Count > 0)
                {
                    Repeater2.DataSource = userlist;
                    Repeater2.DataBind();
                    this.number.InnerText = userlist.Count.ToString();
                }
                else
                {
                    Repeater2.DataSource = userlist;
                    Repeater2.DataBind();
                    this.number.InnerText = "0";
                }
        }
        #endregion

        public void GetSession()
        {
            IES.JW.Model.User student = Session["Teacher"] as IES.JW.Model.User;
            this.txtKey.Value = student.Key.ToString();
        }

        #region 显示角色
        public static string GetUser(string val)
        {
            if (val == "1")
            {
                return "超级管理员";
            }
            if (val == "2")
            {
                return "子管理员";
            }
            else if (val == "4")
            {
                return "学生";
            }
            if (val == "6")
            {
                return "子管理员<br/>学生";
            }
            if (val == "8")
            {
                return "教师";
            }
            if (val == "10")
            {
                return "子管理员<br/>教师";
            }
            if (val == "15")
            {
                return "超级管理员<br/>子管理员<br/>学生<br/>教师";
            }
            if (val == "16")
            {
                return "系统外用户";
            }
            if (val == "31")
            {
                return "万能用户";
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion

        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {

            DataBinder(AspNetPager1.CurrentPageIndex);
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            string key = this.txtKey.Value;
            IES.JW.Model.User _user = new IES.JW.Model.User { Key = key };
            Session["Teacher"] = _user;
            DataBinder(1);
        }

        protected void RoleChange_Click(object sender, EventArgs e)
        {
            int organizationID = Convert.ToInt32(this.hfID.Value);
            Session["UserType"] = organizationID;
            DataBinder(1);
        }
    }
}