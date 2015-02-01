using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.JW.Course
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

            DDLSemester.DataSource = parmlist.trlist;
            DDLSemester.DataTextField = "TermTypeName";
            DDLSemester.DataValueField = "TermTypeID";
            DDLSemester.DataBind();
            DDLSemester.Items.Insert(0, new ListItem("请选择", "0"));

            DDLMethods.DataSource = parmlist.crtchtylist;
            DDLMethods.DataTextField = "Name";
            DDLMethods.DataValueField = "TeachingTypeID";
            DDLMethods.DataBind();
            DDLMethods.Items.Insert(0, new ListItem("请选择", "0"));

            DDLDiscipline.DataSource = parmlist.sptylist;
            DDLDiscipline.DataTextField = "SpecialtyTypeName";
            DDLDiscipline.DataValueField = "SpecialtyTypeID";
            DDLDiscipline.DataBind();
            DDLDiscipline.Items.Insert(0, new ListItem("请选择", "0"));

            SpecialtyType.DataTextField = "请选择";
            SpecialtyType.DataValueField = "0";
            SpecialtyType.DataBind();
            SpecialtyType.Items.Insert(0, new ListItem("请选择", "0"));
            //SpecialtyType.Items.Clear();
            //SpecialtyType.Items.Insert(0, new ListItem("请选择", "0"));

            DDLOrganization.DataSource = parmlist.orglist;
            DDLOrganization.DataTextField = "OrganizationName";
            DDLOrganization.DataValueField = "OrganizationID";
            DDLOrganization.DataBind();
            DDLOrganization.Items.Insert(0, new ListItem("请选择", "0"));

        }

        public void CourseType(int ID)
        {
            IES.JW.Model.SpecialtyType specialty = new IES.JW.Model.SpecialtyType { ParentID = ID };
            IES.G2S.JW.BLL.ParmInfoBLL parmbll = new IES.G2S.JW.BLL.ParmInfoBLL();
            List<IES.JW.Model.SpecialtyType> parmlist = parmbll.SpecialtyTyep2_List(specialty);

            SpecialtyType.DataSource = parmlist;
            SpecialtyType.DataTextField = "SpecialtyTypeName";
            SpecialtyType.DataValueField = "SpecialtyTypeID";
            SpecialtyType.DataBind();
        }

        #endregion

        #region  数据获取
        private void DataBinder(int id)
        {
            IES.JW.Model.Course course = new IES.JW.Model.Course { CourseID = id };
            IES.G2S.JW.BLL.CourseBLL coursebll = new IES.G2S.JW.BLL.CourseBLL();
            IES.JW.Model.Course _course = coursebll.Course_Get(course.CourseID);

            this.CourseNo.Value = _course.CourseNo;
            this.Name.Value = _course.CourseName;
            this.EnglishName.Value = _course.CourseNameEn;
            this.DDLSemester.SelectedValue = _course.TermTypeID.ToString();
            this.DDLDiscipline.SelectedValue = _course.SubjectID1.ToString();
            if (_course.SubjectID1 == 0)
            {
                SpecialtyType.Items.Clear();
                SpecialtyType.Items.Insert(0, new ListItem("请选择", "0"));
            }
            else
            {
                CourseType(_course.SubjectID1);
                this.SpecialtyType.SelectedValue = _course.SubjectID2.ToString();
            }          
            this.DDLOrganization.SelectedValue = _course.OrganizationID.ToString();
            this.DDLMethods.SelectedValue = _course.TeachingTypeID.ToString();
            this.Credit.Value = _course.Credit.ToString();
            this.Hours.Value = _course.Hours.ToString();
            this.oEditor1.Value = _course.Introduction;
            this.oEditor2.Value = _course.OutLine;
            this.oEditor3.Value = _course.Team;
            this.oEditor4.Value = _course.Schedule;
            this.hfIDS.Value = _course.CourseTypeID.ToString();
        }
        #endregion

        #region 新增或修改
        protected void update_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request["id"]);
            string CourseNo = this.CourseNo.Value;
            string CourseName = this.Name.Value;
            string CourseNameEn = this.EnglishName.Value;
            int TermTypeID = Convert.ToInt32(this.DDLSemester.SelectedValue);
            int OrganizationId = Convert.ToInt32(this.DDLOrganization.SelectedValue);
            int SubjectID1 = Convert.ToInt32(this.DDLDiscipline.SelectedValue);
            int SubjectID2 = Convert.ToInt32(this.SpecialtyType.SelectedValue);
            int CourseTypeID = Convert.ToInt32(this.hfIDS.Value);
            decimal? Hours = Convert.ToDecimal(this.Hours.Value);
            decimal? Credit = Convert.ToDecimal(this.Credit.Value);
            int TeachingTypeID = Convert.ToInt32(this.DDLMethods.SelectedValue);
            string Introduction = this.oEditor1.Value;
            string OutLine = this.oEditor2.Value;
            string Team = this.oEditor3.Value;
            string Schedule = this.oEditor4.Value;

            IES.G2S.JW.BLL.CourseBLL coursebll = new IES.G2S.JW.BLL.CourseBLL();
            IES.JW.Model.Course _course = new IES.JW.Model.Course { CourseID = id, CourseNo = CourseNo, CourseName = CourseName, CourseNameEn = CourseNameEn, TermTypeID = TermTypeID, OrganizationID = OrganizationId, SubjectID1 = SubjectID1, SubjectID2 = SubjectID2, CourseTypeID = CourseTypeID, Hours = Hours, Credit = Credit, TeachingTypeID = TeachingTypeID, Introduction = Introduction, OutLine = OutLine, Team = Team, Schedule = Schedule };
            IES.JW.Model.Course result = coursebll.Course_Edit(_course);
            if (result != null)
            {
                if (result.output != null && result.output!="")
                {
                    Response.Write("<script>alert('" + result.output + "');</script>");
                }
                else if (result.op_CourseID != 0)
                {
                    if (id > 0)
                    { Response.Write("<script>alert('修改成功!');location.href='Course.aspx?PID=A113';</script>"); }
                    else if (id == 0)
                    { Response.Write("<script>alert('新增成功!');location.href='Course.aspx?PID=A113';</script>"); }
                }          
            }
            else
            {
                Response.Write("<script>alert('操作失败!');</script>");
            }

        }
        #endregion

        #region 学科
        protected void DDLDiscipline_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.DDLDiscipline.SelectedValue != "0")
            {
                int sptyid = Convert.ToInt32(this.DDLDiscipline.SelectedValue);
                CourseType(sptyid);
            }
            else if (this.DDLDiscipline.SelectedValue == "0")
            {
                SpecialtyType.Items.Clear();
                SpecialtyType.Items.Insert(0, new ListItem("请选择", "0"));
            }
        }
        #endregion
    }
}