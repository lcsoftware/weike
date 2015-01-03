using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Admin.Demo
{
    public partial class ItemEvent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rptInfo.DataSource = dtDemo(1);
                rptInfo.DataBind();

                Repeater1.DataSource = dtDemo(1);
                Repeater1.DataBind();
            }
        }

        private DataTable dtDemo(int Index)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ID"));
            dt.Columns.Add(new DataColumn("FirstName"));
            dt.Columns.Add(new DataColumn("LastName"));
            dt.Columns.Add(new DataColumn("Age"));
            dt.Columns.Add(new DataColumn("Sex"));
            dt.Columns.Add(new DataColumn("Progress"));
            DataRow dr;
            for (int i = Index; i < Index + 10; i++)
            {
                dr = dt.NewRow();
                dr["ID"] = i;
                dr["FirstName"] = "FirstName" + i.ToString();
                dr["LastName"] = "LastName" + i.ToString();
                dr["Age"] = i;
                dr["Sex"] = i % 2;
                dr["Progress"] = "30%";
                dt.Rows.Add(dr);
            }
            return dt;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}