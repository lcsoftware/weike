using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.JW.Class
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
        private void DataBinder(int id)
        {
            IES.JW.Model.Class _class = new IES.JW.Model.Class { ClassID = id };
            IES.G2S.JW.BLL.ClassBLL clasbll = new IES.G2S.JW.BLL.ClassBLL();
            IES.JW.Model.Class classlist = clasbll.Class_Get(_class.ClassID);

            this.ClassNo.Value = classlist.ClassNo;
            this.ClassName.Value = classlist.ClassName;
            this.Organization.SelectedValue = classlist.OrganizationID.ToString();
            this.Specialty.SelectedValue = classlist.SpecialtyID.ToString();
            this.CreateTime.Value = classlist.EntryDate.ToString("yyyy-MM-dd ");
            this.Teacher.Value = classlist.TeacherName;
            this.hfIDS.Value = classlist.TeacherID.ToString();

            IES.JW.Model.User _user = new IES.JW.Model.User { ClassID = id };
            IES.G2S.JW.BLL.ClassBLL classbll = new IES.G2S.JW.BLL.ClassBLL();
            List<IES.JW.Model.User> userlist = classbll.ClassStudent_List(_user.ClassID);
            if (userlist != null)
                if (userlist.Count > 0)
                {
                    Repeater1.DataSource = userlist;
                    Repeater1.DataBind();
                    string ids = "";
                    for (var i = 0; i < userlist.Count; i++)
                    {
                        ids += userlist[i].UserID + ",";
                    }
                    this.Students.Value = ids;
                }
        }
        //绑定条件
        public void DataParm()
        {
            IES.G2S.JW.BLL.ParmInfoBLL parmbll = new IES.G2S.JW.BLL.ParmInfoBLL();
            IES.JW.Model.ParmInfo parmlist = parmbll.Parm_Info_List();

            Specialty.DataSource = parmlist.splist;
            Specialty.DataTextField = "SpecialtyName";
            Specialty.DataValueField = "SpecialtyID";
            Specialty.DataBind();
            Specialty.Items.Insert(0, new ListItem("请选择", "0"));

            Organization.DataSource = parmlist.orglist;
            Organization.DataTextField = "OrganizationName";
            Organization.DataValueField = "OrganizationID";
            Organization.DataBind();
            Organization.Items.Insert(0, "请选择");

        }
        #endregion

        public void Sumbit()
        {
            int id = Convert.ToInt32(Request["id"]);
            string classno = this.ClassNo.Value;
            string classname = this.ClassName.Value;
            int orgid = Convert.ToInt32(this.Organization.SelectedValue);
            int sptyid = Convert.ToInt32(this.Specialty.SelectedValue);
            DateTime crtime = Convert.ToDateTime(this.CreateTime.Value);
            int tchname = Convert.ToInt32(this.hfIDS.Value);
            string students = this.Students.Value;

            IES.G2S.JW.BLL.ClassBLL spltybll = new IES.G2S.JW.BLL.ClassBLL();
            IES.JW.Model.Class _class = new IES.JW.Model.Class { ClassID = id, ClassNo = classno, ClassName = classname, OrganizationID = orgid, SpecialtyID = sptyid, EntryDate = crtime, TeacherID = tchname };
            IES.JW.Model.Class result = spltybll.Class_Edit(_class);
            if (result != null)
            {
                if (id> 0)
                {

                    IES.JW.Model.Class class_ = new IES.JW.Model.Class { ClassID = id, StudentIDs = students };
                    bool Results = spltybll.ClassStudent_Save(class_);
                    if (Results == true)
                    {
                        Response.Write("<script>alert('修改成功!');location.href='Class.aspx?PID=A116';</script>");
                    }

                    
                }
                else if (result.op_ClassID != 0)
                {
                    IES.JW.Model.Class class_ = new IES.JW.Model.Class { ClassID = result.op_ClassID, StudentIDs = students };
                    bool Results = spltybll.ClassStudent_Save(class_);
                    if (Results == true)
                    {
                        Response.Write("<script>alert('新增成功!');location.href='Class.aspx?PID=A116';</script>");
                    }
                }
                else{
                    Response.Write("<script>alert('" + result.output + "');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('操作失败');</script>");
            }
            
        }

        protected void update_Click(object sender, EventArgs e)
        {
            Sumbit();
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            Response.Write("<script>location.href='Class.aspx?PID=A116';</script>");
        }

        protected void btnInfo_Click(object sender, EventArgs e)
        {
            int TeachingClassID = -1;
            string SutdentIDs = this.Students.Value;
            int PageIndex = 1;
            int PageSize = 100;
            IES.G2S.JW.BLL.TeachingClassBLL teachbll = new IES.G2S.JW.BLL.TeachingClassBLL();
            List<IES.JW.Model.TeachingClassStudent> Teachingclassstudentlist = teachbll.TeachingClassStudent_List(TeachingClassID, SutdentIDs, PageIndex, PageSize);
            if (Teachingclassstudentlist != null)
            {
                if (Teachingclassstudentlist.Count > 0)
                {
                    Repeater1.DataSource = Teachingclassstudentlist;
                    Repeater1.DataBind();
                }
            }
        }
    }
}