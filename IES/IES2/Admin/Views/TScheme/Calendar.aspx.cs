using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.TScheme
{
    public partial class Calendar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder(1);
            }
        }

        #region 绑定数据
        protected void rptypelist_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rep = e.Item.FindControl("rpquestionlist") as Repeater;//找到里层的repeater对象
                IES.JW.Model.Term rowv = e.Item.DataItem as IES.JW.Model.Term; ;  //找到分类Repeater关联的数据项 
                int termid = Convert.ToInt32(rowv.TermID); //获取填充子类的id 
                IES.JW.Model.Term _term = new IES.JW.Model.Term { TermID = termid };
                IES.G2S.JW.BLL.TermBLL termbll = new IES.G2S.JW.BLL.TermBLL();
                List<IES.JW.Model.Lesson> lessonlist = termbll.Lesson_List(_term);
                rep.DataSource = lessonlist;
                rep.DataBind();
            }
        }
        private void DataBinder(int pageindex)
        {
            if (Session["Term"] != null)
            { GetSession(); }
            string key = this.Key.Value;
            IES.JW.Model.Term _term = new IES.JW.Model.Term { Key = key};
            IES.G2S.JW.BLL.TermBLL termbll = new IES.G2S.JW.BLL.TermBLL();
            List<IES.JW.Model.Term> termlist = termbll.Term_List(_term);
            if (termlist != null)
                if (termlist.Count > 0)
                {
                    rptypelist.DataSource = termlist;
                    rptypelist.DataBind();
                    this.number.InnerText = "（共" + termlist.Count + "条）";
                }
                else
                {
                    rptypelist.DataSource = termlist;
                    rptypelist.DataBind();
                    this.number.InnerText = "（共0条）";
                }

        }
        public void GetSession()
        {
            IES.JW.Model.Term term = Session["Term"] as IES.JW.Model.Term;
            this.Key.Value = term.Key.ToString();
        }
        #endregion

        #region 操作
        //删除
        protected void btnInfo_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.hfID.Value);
            IES.JW.Model.Term _term = new IES.JW.Model.Term { TermID = id };
            IES.G2S.JW.BLL.TermBLL termbll = new IES.G2S.JW.BLL.TermBLL();
            bool result = termbll.Term_Del(_term);
            if (result == true)
            { DataBinder(1); }
        }
        //搜索
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            string key = this.Key.Value;
            IES.JW.Model.Term _term = new IES.JW.Model.Term { Key = key };
            Session["Term"] = _term;
            DataBinder(1);
        }
        #endregion

    }
}