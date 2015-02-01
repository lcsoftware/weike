using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IES.JW.Model;

namespace Admin.Views.JW.Specialty
{
    public partial class SpTeacherADD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AspNetPager1.PageSize = 20;
                DataBinder(1);
            }

        }
        //生成树
        public string TreeData()
        {
            var datas = "";
            List<IES.JW.Model.Organization> orglist = OrgList();
            for (var i = 0; i < orglist.Count; i++)
            {
                if (i == orglist.Count - 1)
                {
                    var menudata = " { 'ID': " + orglist[i].OrganizationID + ", 'Name': '" + orglist[i].OrganizationName + "', 'ParentID': " + orglist[i].ParentID + " }";
                    datas += menudata;
                }
                else
                {
                    var menudata = " { 'ID': " + orglist[i].OrganizationID + ", 'Name': '" + orglist[i].OrganizationName + "', 'ParentID': " + orglist[i].ParentID + " },";
                    datas += menudata;
                }
            }
            return datas;
        }
        private void DataBinder(int pageindex)
        {
            int orgid = -1;
            if(Session["SelOrg"]!=null)
            { 
                orgid = Convert.ToInt32(Session["SelOrg"].ToString());
            }            
            string key = this.Key.Value;
            int role = 8;
            int modevalue = 32;
            int specialtyid = -1;
            int classid = -1;
            int islocked = -1;
            int isregister = -1;
            int isassistant = -1;
            int isshow = -1;
            int isinschool = -1;
            int entrydate = -1;
            IES.JW.Model.User user = new IES.JW.Model.User { Key = key, Role = role, modevalue = modevalue, OrganizationID = orgid, SpecialtyID = specialtyid, ClassID = classid, IsLocked = islocked, IsRegister = isregister, IsAssistant = isassistant, IsShow = isshow, IsInSchool = isinschool, EntryDate = entrydate };
            IES.G2S.JW.BLL.UserBLL userbll = new IES.G2S.JW.BLL.UserBLL();
            List<IES.JW.Model.User> userlist = userbll.User_List(user, pageindex, AspNetPager1.PageSize);
            if (userlist != null)
                if (userlist.Count > 0)
                {
                    AspNetPager1.RecordCount = userlist[0].rowscount;
                    Repeater1.DataSource = userlist;
                    Repeater1.DataBind();
                }
                else
                {
                    AspNetPager1.RecordCount = 1;
                    Repeater1.DataSource = userlist;
                    Repeater1.DataBind();
                }
        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            DataBinder(AspNetPager1.CurrentPageIndex);
        }
        public List<IES.JW.Model.Organization> OrgList()
        {
            IES.G2S.JW.BLL.ParmInfoBLL parmbll = new IES.G2S.JW.BLL.ParmInfoBLL();
            IES.JW.Model.ParmInfo parmlist = parmbll.Parm_Info_List();
            return parmlist.orglist;
        }
        protected void OrgChange_Click(object sender, EventArgs e)
        {
            int orgid=Convert.ToInt32(this.hfID.Value);
            Session["SelOrg"] = orgid;
            DataBinder(1);
        }
    }
}