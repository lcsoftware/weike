using IES.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.JW.Teacher
{
   
        public partial class Teacher : System.Web.UI.Page
        {
            public static string sorting = "";
            protected void Page_Load(object sender, EventArgs e)
            {
                DataParm();
                if (!IsPostBack)
                {
                    AspNetPager1.PageSize = Browse.PageSize;
                    DropDownList1.SelectedValue = Browse.PageSize.ToString();
                    DataBinder(1);
                    if (Session["Parms"] == null)
                        Session["Parms"] = "-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,";
                }
            }
            #region 绑定数据
            private void DataBinder(int pageindex)
            {
                if (Session["Teacher"] != null)
                { GetSession(); }
                string key = this.txtKey.Value;
                int role = 8;
                int modevalue = 32;
                int organizationid = -1;
                int specialtyid = -1;
                int classid = -1;
                int islocked = -1;
                int isregister = -1;
                int isassistant = -1;
                int isshow = -1;
                int isinschool = -1;
                int entrydate = -1;
                string parms = this.Parms.Value;
                if (parms != "")
                {
                    var ary = parms.Split(',');
                    organizationid = Convert.ToInt32(ary[9]);                    
                    entrydate = Convert.ToInt32(ary[4]);
                    classid = Convert.ToInt32(ary[3]);
                    isinschool = Convert.ToInt32(ary[2]);
                    isassistant = Convert.ToInt32(ary[1]);
                    isshow = Convert.ToInt32(ary[0]);
                    Session["Parms"] = parms;
                }  
                IES.JW.Model.User user = new IES.JW.Model.User { Key = key, Role = role, modevalue = modevalue, OrganizationID = organizationid, SpecialtyID = specialtyid, ClassID = classid, IsLocked = islocked, IsRegister = isregister, IsAssistant=isassistant, IsShow=isshow, IsInSchool=isinschool,EntryDate=entrydate };
                IES.G2S.JW.BLL.UserBLL userbll = new IES.G2S.JW.BLL.UserBLL();
                List<IES.JW.Model.User> userlist = userbll.User_List( user, pageindex, AspNetPager1.PageSize);
                if (userlist != null)
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
            //绑定条件
            public void DataParm()
            {
                IES.G2S.JW.BLL.ParmInfoBLL parmbll = new IES.G2S.JW.BLL.ParmInfoBLL();
                IES.JW.Model.ParmInfo parmlist = parmbll.Parm_Info_List();
                //绑定数据
                rptorg.DataSource = parmlist.orglist;
                rptorg.DataBind();
            }
            public void GetSession()
            {
                IES.JW.Model.User student = Session["Teacher"] as IES.JW.Model.User;
                this.txtKey.Value = student.Key.ToString();
            }
            public string Getclass()
            {
                string parms = Session["Parms"].ToString();
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "../../Portal/Edit.js", "<script>Saveclass('" + parms + "');</script>");
                return null;
            }
            #endregion

            #region 操作
            //删除
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
            //批量删除
            protected void BatchDel_Click(object sender, EventArgs e)
            {
                DelBatch();
            }
            public void DelBatch()
            {
                string IDS = this.hfIDS.Value;
                IES.G2S.JW.BLL.UserBLL userbll = new IES.G2S.JW.BLL.UserBLL();
                bool result = userbll.User_Batch_Del(IDS);
                if (result == true)
                {
                    DataBinder(1);
                }
            }
            //切换PageSize
            protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
            {
                AspNetPager1.PageSize = Int32.Parse(DropDownList1.SelectedValue);
                Browse.SetPageSize(AspNetPager1.PageSize);
                DataBinder(1);
            }
            //搜索
            protected void btnSelect_Click(object sender, EventArgs e)
            {
                string key = this.txtKey.Value;
                IES.JW.Model.User _user = new IES.JW.Model.User { Key = key };
                Session["Teacher"] = _user;
                DataBinder(1);
            }
            #endregion
        }
    
}