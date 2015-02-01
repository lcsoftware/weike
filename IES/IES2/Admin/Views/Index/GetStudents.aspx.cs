using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IES.G2S.JW.BLL;
using IES.JW.Model;

namespace Admin.Views.Index
{
    public partial class GetStudents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["GetStudentsUrl"] != null)
                {
                    String StudentsUrl = Session["GetStudentsUrl"].ToString();
                    string[] urlList = StudentsUrl.Split('@');
                    txtSpecialID.Value = urlList[0].ToString();
                    txtDateID.Value = urlList[1].ToString();
                }
                Bind_Special();
                Bind_Date();
            }

        }
        //绑定教学专业
        public void Bind_Special()
        {
            DataTable dt = new DataTable();
            SpecialtyBLL specialtyBLL = new SpecialtyBLL();
            if (ViewState["Specialty"] == null)
            {
                ShortSpecialty shortspecialty = new ShortSpecialty();
                shortspecialty.SpecialtyIDs = "";
                dt = IES.Common.ListToDateUtil.ListToDataTable<IES.JW.Model.ShortSpecialty>(specialtyBLL.Specialty_Short_List(shortspecialty));
                ViewState["Specialty"] = dt;
            }
            else
            {
                dt = ViewState["Specialty"] as DataTable;
            }
            dt.Rows.Add(new string[] { "0", "全部", "-1" });
            KnowledgeSpotTree1.IsShowCheckBox = true;
            KnowledgeSpotTree1.TreeDataSource = dt;
            KnowledgeSpotTree1.DisplayField = "SpecialtyName";
            KnowledgeSpotTree1.palContainer.BorderWidth = 1;
            KnowledgeSpotTree1.Height = 250;
            KnowledgeSpotTree1.Width = 298;
            KnowledgeSpotTree1.ValueField = "SpecialtyID";
            KnowledgeSpotTree1.ParentIDField = "ParentID";
            KnowledgeSpotTree1.PageType = "AddClass";
            KnowledgeSpotTree1.fOrder = "SpecialtyID";
            KnowledgeSpotTree1.SelectedKnowlegeIDs = txtSpecialID.Value;
            KnowledgeSpotTree1.TreeDataBind();

        }



        //绑定教学年级
        public void Bind_Date()
        {
            DataTable dt = new DataTable();

            if (ViewState["ClassYear"] == null)
            {
                dt.Columns.Add(new DataColumn("OrganizationID", typeof(System.Int32)));
                dt.Columns.Add(new DataColumn("OrganizationName", typeof(System.String)));
                dt.Columns.Add(new DataColumn("ParentID", typeof(System.Int32)));
                for (int i = DateTime.Now.Year; i > DateTime.Now.Year - 6; i--)
                {
                    dt.Rows.Add(i, i.ToString(), 0);
                }
                dt.Rows.Add(0, "全部", -1);
            }
            else
            {
                dt = ViewState["ClassYear"] as DataTable;
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                KnowledgeSpotTree3.IsShowCheckBox = true;
                KnowledgeSpotTree3.TreeDataSource = dt; //ds.Tables[0];
                KnowledgeSpotTree3.DisplayField = "OrganizationName";
                KnowledgeSpotTree3.palContainer.BorderWidth = 1;
                KnowledgeSpotTree3.Height = 250;
                KnowledgeSpotTree3.Width = 198;
                KnowledgeSpotTree3.ValueField = "OrganizationID";
                KnowledgeSpotTree3.ParentIDField = "ParentID";
                KnowledgeSpotTree3.fOrder = "OrganizationID";
                KnowledgeSpotTree3.PageType = "AddClass";
                KnowledgeSpotTree3.SelectedKnowlegeIDs = txtDateID.Value;
                KnowledgeSpotTree3.TreeDataBind();
            }
            else
            {
            }
        }
    }
}