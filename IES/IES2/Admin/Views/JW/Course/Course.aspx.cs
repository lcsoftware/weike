using IES.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.JW.Course
{
    public partial class Course : System.Web.UI.Page
    {
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
            if (Session["Course"] != null)
            { GetSession(); }
            string key = this.txtKey.Value;
            decimal beginfen = Convert.ToDecimal(this.BeginFen.Value == "" ? "0" : this.BeginFen.Value);
            decimal endfen = Convert.ToDecimal(this.EndFen.Value == "" ? "0" : this.EndFen.Value);
            string parms = this.Parms.Value;
            int termtypeid = -1;
            int orgid = -1;
            int cstyid = -1;
            int tchtyid = -1;
            int sbjid1 = -1;
            int sbjid2 = -1;
            if (parms != "")
            {
                var ary = parms.Split(',');
                orgid = Convert.ToInt32(ary[9]);
                tchtyid = Convert.ToInt32(ary[6]);
                cstyid = Convert.ToInt32(ary[7]);
                Session["Parms"] = parms;
            }

            IES.JW.Model.Course course = new IES.JW.Model.Course { Key = key,TermTypeID=termtypeid, OrganizationID = orgid, CourseTypeID = cstyid, TeachingTypeID = tchtyid, SubjectID1 = sbjid1, SubjectID2 = sbjid2, BeginFen = beginfen, EndFen = endfen };
            IES.G2S.JW.BLL.CourseBLL coursebll = new IES.G2S.JW.BLL.CourseBLL();
            List<IES.JW.Model.Course> courselist = coursebll.Course_List(course, pageindex, AspNetPager1.PageSize);
            if (courselist != null)
            if (courselist.Count > 0)
            {
                AspNetPager1.RecordCount = courselist[0].rowscount;
                Repeater1.DataSource = courselist;
                Repeater1.DataBind();
                this.number.InnerText = "（共" + AspNetPager1.RecordCount + "条）";
            }
            else
            {
                AspNetPager1.RecordCount = 1;
                Repeater1.DataSource = courselist;
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

            rptorg.DataSource = parmlist.orglist;
            rptorg.DataBind();
            //rpttr.DataSource = parmlist.trlist;
            //rpttr.DataBind();
            rptcrty.DataSource = parmlist.crtylist;
            rptcrty.DataBind();
            rptcrtchty.DataSource = parmlist.crtchtylist;
            rptcrtchty.DataBind();
        }
        private void GetSession()
        {
            IES.JW.Model.Course course = Session["Course"] as IES.JW.Model.Course;
            this.txtKey.Value = course.Key.ToString();
            this.BeginFen.Value = course.BeginFen.ToString();
            this.EndFen.Value = course.EndFen.ToString();
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
            IES.JW.Model.Course _course = new IES.JW.Model.Course { CourseID = id };
            IES.G2S.JW.BLL.CourseBLL coursebll = new IES.G2S.JW.BLL.CourseBLL();
            bool result = coursebll.Course_Del(_course);
            if (result == true)
            {
                DataBinder(1);
            }
        }      
        //搜索
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            string key = this.txtKey.Value;
            decimal beginfen = Convert.ToDecimal(this.BeginFen.Value == "" ? "0" : this.BeginFen.Value);
            decimal endfen = Convert.ToDecimal(this.EndFen.Value == "" ? "0" : this.EndFen.Value);
            IES.JW.Model.Course course = new IES.JW.Model.Course { Key = key,BeginFen=beginfen,EndFen=endfen};
            Session["Course"] = course;
            DataBinder(1);
        }
        //批量删除
        protected void BatchDel_Click(object sender, EventArgs e)
        {
            DelBatch();
        }
        public void DelBatch()
        {
            string IDS = this.hfIDS.Value;
            IES.G2S.JW.BLL.CourseBLL newsbll = new IES.G2S.JW.BLL.CourseBLL();
            bool result = newsbll.Course_Batch_Del(IDS);
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
        #endregion
    }
}