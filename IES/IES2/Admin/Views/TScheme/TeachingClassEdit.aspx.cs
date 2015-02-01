using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.TScheme
{
    public partial class TeachingClassEdit : System.Web.UI.Page
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

        #region 数据加载
        private void DataBinder(int id)
        {
            IES.JW.Model.TeachingClassInfo _class = new IES.JW.Model.TeachingClassInfo { TeachingClassID = id };
            IES.G2S.JW.BLL.TeachingClassBLL teachingclassbll = new IES.G2S.JW.BLL.TeachingClassBLL();
            IES.JW.Model.TeachingClassInfo teachingclasslist = teachingclassbll.TeachingClassInfo_Get(_class);

            this.TeachingNo.Value = teachingclasslist.teachingclass.ClassNo;
            this.TeachingName.Value = teachingclasslist.teachingclass.ClassName;
            this.Organization.SelectedValue = teachingclasslist.teachingclass.OrganizationID.ToString();
            this.Teacher.Value = teachingclasslist.teachingclass.UserName;
            this.TeaCourse.Value = teachingclasslist.teachingclass.CourseName;
            this.CourIDs.Value = teachingclasslist.teachingclass.CourseID.ToString();
            this.BeginTime.Value = Convert.ToDateTime(teachingclasslist.teachingclass.StartDate).ToString("yyyy-MM-dd ");
            this.EndTime.Value = Convert.ToDateTime(teachingclasslist.teachingclass.EndDate).ToString("yyyy-MM-dd ");
            this.hfIDS.Value = teachingclasslist.teachingclassteacherlist[0].UserID.ToString();
            if (teachingclasslist != null)
            {
                if (teachingclasslist.teachingclassstudentlist.Count > 0)
                {
                    Repeater1.DataSource = teachingclasslist.teachingclassstudentlist;
                    Repeater1.DataBind();
                    string ids = "";
                    for (var i = 0; i < teachingclasslist.teachingclassstudentlist.Count; i++)
                    {
                        ids += teachingclasslist.teachingclassstudentlist[i].UserID + ",";
                    }
                    this.Students.Value = ids;
                }
            }
        }

        private void DataParm()
        {
            IES.G2S.JW.BLL.ParmInfoBLL parmbll = new IES.G2S.JW.BLL.ParmInfoBLL();
            IES.JW.Model.ParmInfo parmlist = parmbll.Parm_Info_List();

            Organization.DataSource = parmlist.orglist;
            Organization.DataTextField = "OrganizationName";
            Organization.DataValueField = "OrganizationID";
            Organization.DataBind();
            Organization.Items.Insert(0, new ListItem("请选择", "0"));
        }
        #endregion

        /// <summary>
        /// 修改或新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void update_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request["id"]);
            string TeachingNo = this.TeachingNo.Value;
            string TeachingName = this.TeachingName.Value;
            DateTime StartDate = Convert.ToDateTime(this.BeginTime.Value);
            DateTime EndDate = Convert.ToDateTime(this.EndTime.Value);
            int CourseID = Convert.ToInt32(this.CourIDs.Value);
            int TermID = Convert.ToInt32(this.CourIDs.Value);
            int OrganizationID = Convert.ToInt32(this.Organization.SelectedValue);
            int Source = 2;
            int MainUserID = Convert.ToInt32(this.hfIDS.Value);
            string OtherUserIDS = this.BuidlTeacher.Value;
            string studens = this.Students.Value;
            IES.G2S.JW.BLL.TeachingClassBLL teachingclassbll = new IES.G2S.JW.BLL.TeachingClassBLL();
            if (id > 0)
            {
                IES.JW.Model.TeachingClass _teachingclass = new IES.JW.Model.TeachingClass { TeachingClassID = id, ClassName = TeachingName, StartDate = StartDate, EndDate = EndDate, CourseID = CourseID, TermID = TermID, OrganizationID = OrganizationID, MainUserID = MainUserID, OtherUserIDS = OtherUserIDS };
                bool result = teachingclassbll.TeachingClass_Upd(_teachingclass);
                IES.JW.Model.TeachingClassStudent _teachclassstudent = new IES.JW.Model.TeachingClassStudent { UserIDS = studens, TeachingClassID = id };
                IES.JW.Model.TeachingClassStudent teachclassstudent = teachingclassbll.TeachingClassStudent_Edit(_teachclassstudent);
                if (result == true && teachclassstudent != null)
                {
                    Response.Write("<script>alert('修改成功！');location.href='TeachingClass.aspx';</script>");
                }
                else
                {
                    Response.Write("<script>alert('修改失败！');location.href='TeachingClass.aspx';</script>");
                }
            }
            else
            {
                IES.JW.Model.TeachingClass _teachingclass = new IES.JW.Model.TeachingClass { TeachingClassID = id, ClassNo = TeachingNo, ClassName = TeachingName, StartDate = StartDate, EndDate = EndDate, CourseID = CourseID, OrganizationID = OrganizationID, TermID = TermID, Source = Source, MainUserID = MainUserID, OtherUserIDS = OtherUserIDS };
                IES.JW.Model.TeachingClass teachingclas = teachingclassbll.TeachingClass_ADD(_teachingclass);
                int AddId = teachingclas.TeachingClassID;
                IES.JW.Model.TeachingClassStudent _teachclassstudent = new IES.JW.Model.TeachingClassStudent { UserIDS = studens, TeachingClassID = AddId };
                IES.JW.Model.TeachingClassStudent teachclassstudent = teachingclassbll.TeachingClassStudent_Edit(_teachclassstudent);
                if (teachingclas != null && teachclassstudent != null)
                {
                    Response.Write("<script>alert('新增成功！');location.href='TeachingClass.aspx';</script>");
                }
                else
                {
                    Response.Write("<script>alert('新增失败！');location.href='TeachingClass.aspx';</script>");
                }
            }
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