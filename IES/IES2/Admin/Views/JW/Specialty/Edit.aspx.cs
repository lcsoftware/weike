using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.JW.Specialty
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
            IES.JW.Model.SpecialtyInfo _specialty = new IES.JW.Model.SpecialtyInfo { SpecialtyID = id };
            IES.G2S.JW.BLL.SpecialtyBLL specialtybll = new IES.G2S.JW.BLL.SpecialtyBLL();
            IES.JW.Model.SpecialtyInfo splist = specialtybll.SpecialtyInfo_Get(_specialty);
            //绑定数据
            this.SpecialtyNo.Value = splist.specialty.SpecialtyNo;          
            int spty1 = Convert.ToInt32(splist.specialty.ParentID);
            int spty2 = Convert.ToInt32(splist.specialty.SpecialtyTypeID);          
            GetSpty2(spty1);
            this.SpecialtyType.SelectedValue = spty1.ToString();
            this.SpecialtyType2.SelectedValue = spty2.ToString();
            this.SpecialtyName.Value = splist.specialty.SpecialtyName;
            this.SpecialtyNameEn.Value = splist.specialty.SpecialtyNameEn;
            this.Organization.SelectedValue = splist.specialty.OrganizationID.ToString();
            this.SchoolingLength.SelectedValue = splist.specialty.SchoolingLength.ToString();
            this.oEditor1.Value = splist.specialty.Introduction;
            this.IsShow.Checked = splist.specialty.IsShow;

            Repeater1.DataSource = splist.specialtyteacherlist;
            Repeater1.DataBind();
        }
        //绑定条件
        public void DataParm()
        {
            IES.G2S.JW.BLL.ParmInfoBLL parmbll = new IES.G2S.JW.BLL.ParmInfoBLL();
            IES.JW.Model.ParmInfo parmlist = parmbll.Parm_Info_List();

            SpecialtyType.DataSource = parmlist.sptylist;
            SpecialtyType.DataTextField = "SpecialtyTypeName";
            SpecialtyType.DataValueField = "SpecialtyTypeID";
            SpecialtyType.DataBind();
            SpecialtyType.Items.Insert(0, "请选择");

            Organization.DataSource = parmlist.orglist;
            Organization.DataTextField = "OrganizationName";
            Organization.DataValueField = "OrganizationID";
            Organization.DataBind();
            Organization.Items.Insert(0, "请选择");

            SchoolingLength.DataSource = parmlist.schlenlist;
            SchoolingLength.DataTextField = "SchoolingLength";
            SchoolingLength.DataValueField = "SchoolingLength";
            SchoolingLength.DataBind();
            SchoolingLength.Items.Insert(0, "请选择");

        }
        public void Sumbit()
        {
            int id = Convert.ToInt32(Request["id"]);
            string spltyno = this.SpecialtyNo.Value;
            string spltyname = this.SpecialtyName.Value;
            string spltynameen = this.SpecialtyNameEn.Value;
            decimal schLength = Convert.ToDecimal(this.SchoolingLength.SelectedValue);
            int orgid = 0;
            if (this.Organization.SelectedValue != "请选择")
                orgid = Convert.ToInt32(this.Organization.SelectedValue);
            int sptyid = 0;
            if (this.SpecialtyType.SelectedValue != "请选择")
                sptyid = Convert.ToInt32(this.SpecialtyType2.SelectedValue);
            string itrd = this.oEditor1.Value;
            bool isshow = this.IsShow.Checked;

            IES.G2S.JW.BLL.SpecialtyBLL spltybll = new IES.G2S.JW.BLL.SpecialtyBLL();
            IES.JW.Model.Specialty _specialty = new IES.JW.Model.Specialty { SpecialtyID = id, SpecialtyNo = spltyno, SpecialtyName = spltyname, SpecialtyNameEn = spltynameen, SchoolingLength = schLength, OrganizationID = orgid, SpecialtyTypeID = sptyid, Introduction = itrd, IsShow = isshow };
            IES.JW.Model.Specialty result = spltybll.Specialty_Edit(_specialty);
            if (result != null)
            {
                
                if (result.Output != null)
                { Response.Write("<script>alert('" + result.Output + "');</script>"); }
                else if (result.op_SpecialtyID != 0)
                {
                    if (id == 0)
                    { Response.Write("<script>alert('新增成功!');location.href='Specialty.aspx?PID=A112';</script>"); }
                    else
                    { Response.Write("<script>alert('修改成功!');location.href='Specialty.aspx?PID=A112';</script>"); }
                }               
            }
            else
            { Response.Write("<script>alert('操作失败');</script>"); }
        }
        protected void update_Click(object sender, EventArgs e)
        {
            Sumbit();
        }
        protected void cancel_Click(object sender, EventArgs e)
        {
            Response.Write("<script>location.href='Specialty.aspx';</script>");
        }

        protected void SpecialtyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.SpecialtyType.SelectedValue!="请选择")
            {
                int sptyid = Int32.Parse(this.SpecialtyType.SelectedValue);
                GetSpty2(sptyid);
            }
            else
            {
                 SpecialtyType2.Items.Clear();
                 SpecialtyType2.Items.Insert(0, new ListItem("请选择", "0"));
            }
        }
        public void GetSpty2(int pid)
        {
            IES.JW.Model.SpecialtyType spty = new IES.JW.Model.SpecialtyType { ParentID = pid };
            IES.G2S.JW.BLL.ParmInfoBLL parmbll = new IES.G2S.JW.BLL.ParmInfoBLL();
            List<IES.JW.Model.SpecialtyType> parmlist = parmbll.SpecialtyTyep2_List(spty);
            if(pid==0)
            {
                SpecialtyType.DataSource = parmlist;
                SpecialtyType.DataTextField = "SpecialtyTypeName";
                SpecialtyType.DataValueField = "SpecialtyTypeID";
                SpecialtyType.DataBind();
            }
            else
            {
                SpecialtyType2.DataSource = parmlist;
                SpecialtyType2.DataTextField = "SpecialtyTypeName";
                SpecialtyType2.DataValueField = "SpecialtyTypeID";
                SpecialtyType2.DataBind();
            }
        }
    }
}