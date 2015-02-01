using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.Server
{
    public partial class DiskQuota : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataParm();
            if (!IsPostBack)
            {
                DataBinder(1);
                if (Session["Parms"] == null)
                    Session["Parms"] = "-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,";
            }
        }

        private void DataParm()
        {
            IES.G2S.JW.BLL.ParmInfoBLL parmbll = new IES.G2S.JW.BLL.ParmInfoBLL();
            IES.JW.Model.ParmInfo parmlist = parmbll.Parm_Info_List();

            TeacherSpace.DataSource = parmlist.cfglist;
            TeacherSpace.DataBind();

            StudentSpace.DataSource = parmlist.cfglist;
            StudentSpace.DataBind();
        }

        #region  页面加载
        private void DataBinder(int pageindex)
        {
            if (Session["User"] != null)
            { GetSession(); }
            string userName = this.txtKey.Value;
            bool DiskFreeze = false;
            int DiskSize = 0;
            IES.JW.Model.User user = new IES.JW.Model.User { UserName = userName, UserType = 8, DiskFreeze = DiskFreeze, DiskSize = DiskSize };
            IES.G2S.JW.BLL.UserBLL userbll = new IES.G2S.JW.BLL.UserBLL();
            List<IES.JW.Model.User> userlist = userbll.User_DiskSpace_List(user, pageindex, 10);
            if (userlist != null)
                if (userlist.Count > 0)
                {
                    Repeater1.DataSource = userlist;
                    Repeater1.DataBind();
                }
                else
                {
                    Repeater1.DataSource = userlist;
                    Repeater1.DataBind();
                }
            IES.JW.Model.User _user = new IES.JW.Model.User { UserName = userName, UserType = 4, DiskFreeze = DiskFreeze, DiskSize = DiskSize };
            IES.G2S.JW.BLL.UserBLL _userbll = new IES.G2S.JW.BLL.UserBLL();
            List<IES.JW.Model.User> _userlist = _userbll.User_DiskSpace_List(_user, pageindex, 10);
            if (_userlist != null)
                if (_userlist.Count > 0)
                {
                    Repeater2.DataSource = _userlist;
                    Repeater2.DataBind();
                }
                else
                {
                    Repeater2.DataSource = _userlist;
                    Repeater2.DataBind();
                }
            IES.JW.Model.User user_ = new IES.JW.Model.User { UserName = userName, UserType = 0, DiskFreeze = DiskFreeze, DiskSize = DiskSize };
            IES.G2S.JW.BLL.UserBLL userbll_ = new IES.G2S.JW.BLL.UserBLL();
            List<IES.JW.Model.User> userlist_ = userbll_.User_DiskSpace_List(user_, pageindex, 10);
            if (userlist_ != null)
                if (userlist_.Count > 0)
                {
                    Repeater3.DataSource = userlist_;
                    Repeater3.DataBind();
                }
                else
                {
                    Repeater3.DataSource = userlist_;
                    Repeater3.DataBind();
                }
        }
        #endregion


        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {

            DataBinder(AspNetPager1.CurrentPageIndex);
        }

        private void GetSession()
        {
            IES.JW.Model.User _user = Session["User"] as IES.JW.Model.User;
            this.txtKey.Value = _user.UserName;
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            string key = this.txtKey.Value;
            IES.JW.Model.User _user = new IES.JW.Model.User { Key = key };
            Session["User"] = _user;
            DataBinder(1);
        }

        protected void btnInfo_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.hfID.Value);
            IES.JW.Model.User user = new IES.JW.Model.User { UserID = id };
            IES.G2S.JW.BLL.UserBLL userbll = new IES.G2S.JW.BLL.UserBLL();
            bool result = userbll.User_Del(user);
            if (result == true)
            {
                DataBinder(1);
            }
        }

        //修改教师的存储空间
        protected void btnUpd_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.hfID.Value);
            string newname  = Request["txt" + id + ""].ToString();
            if (newname == "")
            { Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('请输入存储空间！');</script>"); }
            else
            {
                IES.JW.Model.CfgSchool _cfgschool = new IES.JW.Model.CfgSchool { TeacherSpace = Convert.ToInt32(newname),  UserType= 1 };
                IES.G2S.JW.BLL.CfgSchoolBLL cfgschoolbll = new IES.G2S.JW.BLL.CfgSchoolBLL();
                bool result = cfgschoolbll.CfgSchoolSpace_Upd(_cfgschool);
                if (result != false)
                {
                    DataParm();
                    DataBinder(1);
                }
            }
        }

        //修改学生的存储空间
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.hfID.Value);
            string newname = Request["txts" + id + ""].ToString();
            if (newname == "")
            { Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('请输入存储空间！');</script>"); }
            else
            {
                IES.JW.Model.CfgSchool _cfgschool = new IES.JW.Model.CfgSchool { TeacherSpace = Convert.ToInt32(newname), UserType = 2 };
                IES.G2S.JW.BLL.CfgSchoolBLL cfgschoolbll = new IES.G2S.JW.BLL.CfgSchoolBLL();
                bool result = cfgschoolbll.CfgSchoolSpace_Upd(_cfgschool);
                if (result != false)
                {
                    DataParm();
                    DataBinder(1);
                }
            }
        }

        public int Fun(int a, int b)
        {
            int c;
            c = a - b;          
            return c;
            
        } 
    }
}