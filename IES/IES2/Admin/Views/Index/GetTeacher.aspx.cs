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
    public partial class GetTeacher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["TOrgIDs"] != null)
                {
                    txtOrganizationID.Value = Request["TOrgIDs"].ToString();
                }
                Bind_Org();
            }
        }

        //绑定教学组织 
        public void Bind_Org()
        {
            DataTable dt = new DataTable();
            OrganizationBLL organizationBll = new OrganizationBLL();
            if (ViewState["Org"] == null)
            {
                ShortOranization shortoranization = new ShortOranization();
                shortoranization.OrganizationIDs = "";
                dt = IES.Common.ListToDateUtil.ListToDataTable<IES.JW.Model.ShortOranization>(organizationBll.Organization_S_List(shortoranization));
                ViewState["Org"] = dt;
            }
            else
            {
                dt = ViewState["Org"] as DataTable;
            }
            dt.Rows.Add(new string[] { "0", "全部", "-1", "0" });
            KnowledgeSpotTree1.IsShowCheckBox = true;
            KnowledgeSpotTree1.TreeDataSource = dt;
            KnowledgeSpotTree1.DisplayField = "OrganizationName";
            KnowledgeSpotTree1.palContainer.BorderWidth = 1;
            KnowledgeSpotTree1.Height =230;
            KnowledgeSpotTree1.Width =395;
            KnowledgeSpotTree1.ValueField = "OrganizationID";
            KnowledgeSpotTree1.ParentIDField = "ParentID";
            KnowledgeSpotTree1.PageType = "AddClass";

            KnowledgeSpotTree1.SelectedKnowlegeIDs = txtOrganizationID.Value;
            KnowledgeSpotTree1.TreeDataBind();

        }

        public string getXmlResourceName(string ResourceId)
        {
            return ResourceId;
        }
    }
}