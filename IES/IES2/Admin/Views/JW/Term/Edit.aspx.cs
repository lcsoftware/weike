using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.JW.Term
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
                    DataBinder();
                }
            }
        }
        //绑定数据
        public void DataBinder()
        {
            int id = Convert.ToInt32(Request["id"]);
            IES.JW.Model.Term _term = new IES.JW.Model.Term { TermID = id };
            IES.G2S.JW.BLL.TermBLL termbll = new IES.G2S.JW.BLL.TermBLL();
            IES.JW.Model.TermInfo list = termbll.TermInfo_Get(_term);
            this.TermYear.Value = list.term.TermYear;
            this.Termn.SelectedValue = list.term.TermID.ToString();
            this.BeginTime.Value = list.term.StartDate.ToString("yyyy-MM-dd");
            this.EndTime.Value = list.term.EndDate.ToString("yyyy-MM-dd");
            Repeater1.DataSource = list.lesslist;
            Repeater1.DataBind();
        }
        //绑定条件
        public void DataParm()
        {
            IES.G2S.JW.BLL.ParmInfoBLL parmbll = new IES.G2S.JW.BLL.ParmInfoBLL();
            IES.JW.Model.ParmInfo parmlist = parmbll.Parm_Info_List();

            Termn.DataSource = parmlist.trlist;
            Termn.DataTextField = "TermTypeName";
            Termn.DataValueField = "TermTypeID";
            Termn.DataBind();
            Termn.Items.Insert(0, "请选择");

        }
        public int Sumbit()
        {
            int id = Convert.ToInt32(Request["id"]);
            DateTime start = Convert.ToDateTime(this.BeginTime.Value);
            DateTime end = Convert.ToDateTime(this.EndTime.Value);
            string termyear = this.TermYear.Value;
            int termnid = Convert.ToInt32(this.Termn.SelectedValue);

            IES.JW.Model.Term _term = new IES.JW.Model.Term { TermID = id, StartDate = start, EndDate = end, TermYear = termyear, TermTypeID = termnid };
            IES.G2S.JW.BLL.TermBLL termbll = new IES.G2S.JW.BLL.TermBLL();
            IES.JW.Model.Term result = termbll.Term_Edit(_term);
            if (result != null)
                return result.op_TermID;
            return 0;
        }
        //删除
        protected void btnInfo_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.hfID.Value);
            IES.JW.Model.Term _term = new IES.JW.Model.Term { TermID = id };
            IES.G2S.JW.BLL.TermBLL termbll = new IES.G2S.JW.BLL.TermBLL();
            bool result = termbll.Term_Del(_term);
            if (result == true)
            { DataBinder(); }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {

            string lname = this.termname.Value;
            TimeSpan stime = TimeSpan.Parse(this.stime.Value);
            TimeSpan etime = TimeSpan.Parse(this.etime.Value);
            TimeSpan dur = etime.Subtract(stime);
            int h = dur.Hours;
            int m = dur.Minutes;
            int duration = h * 60 + m;
            int id = Sumbit();
            if (id != 0)
            {
                IES.JW.Model.Lesson lesson = new IES.JW.Model.Lesson { TermID = id, LessonName = lname, StartTime = stime, EndTime = etime, Duration = duration };
                IES.G2S.JW.BLL.TermBLL lessbll = new IES.G2S.JW.BLL.TermBLL();
                IES.JW.Model.Lesson rs = lessbll.Lesson_Edit(lesson);
                if (rs.LessonID != 0)
                {
                    DataBinder();
                }
            }
        }
        protected void update_Click(object sender, EventArgs e)
        {
            int id = Sumbit();
            if (id != 0)
                Response.Write("<script>alert('操作成功');location.href='Term.aspx';</script>");
        }
        protected void cancel_Click(object sender, EventArgs e)
        {
            Response.Write("<script>location.href='Term.aspx';</script>");
        }
    }
}