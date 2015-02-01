using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using IES.G2S.JW.BLL;
using IES.JW.Model;

namespace App.G2S.Views.Home
{
    /// <summary>
    /// Notice 的摘要说明
    /// </summary>
    public class Notice : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.AddHeader("Cache-Control", "no-cache,must-revalidate");
            string action = context.Request.Params["action"];

            if (!string.IsNullOrEmpty(action)) this.GetType().GetMethod(action).Invoke(this, new object[] { context });
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        //获取通知列表
        public void GetNoticeList(HttpContext context)
        {
            NoticeBLL noticeBLL = new NoticeBLL();
            IES.JW.Model.Notice notice = new IES.JW.Model.Notice();
            IES.JW.Model.User user = IES.Service.UserService.CurrentUser;
            notice.UserID = user.UserID;
            notice.SysID = 1;
            notice.ModuleID = Convert.ToInt32(context.Request.Params["ModuleID"]);
            int PageSize = Convert.ToInt32(context.Request.Params["PageSize"]);
            int PageIndex = Convert.ToInt32(context.Request.Params["PageIndex"]);
            //noticeBLL.Notice_List(notice, 1, 20);
            DataTable dt = IES.Common.ListToDateUtil.ListToDataTable<IES.JW.Model.Notice>(noticeBLL.Notice_List(notice, PageIndex, PageSize));
            if (dt != null && dt.Rows.Count > 0)
            {
                context.Response.Write(Tools.JsonConvert.GetJSON(dt));
            }
            else
            {
                context.Response.Write("empty");
            }
        }

        //获取通知回复列表
        public void GetCommentList(HttpContext context)
        {
            NoticeBLL noticeBLL = new NoticeBLL();
            IES.JW.Model.NoticeResponse notice = new IES.JW.Model.NoticeResponse();
            notice.NoticeID = Convert.ToInt32(context.Request.Params["NoticeID"]);
            int PageSize = Convert.ToInt32(context.Request.Params["PageSize"]);
            int PageIndex = Convert.ToInt32(context.Request.Params["PageIndex"]);
            //noticeBLL.Notice_List(notice, 1, 20);
            DataTable dt = IES.Common.ListToDateUtil.ListToDataTable<IES.JW.Model.NoticeResponse>(noticeBLL.NoticeResponse_List(notice, PageIndex, PageSize));
            if (dt != null && dt.Rows.Count > 0)
            {
                context.Response.Write(Tools.JsonConvert.GetJSON(dt));
            }
            else
            {
                context.Response.Write("empty");
            }
        }

        //获取通知回复列表
        public void NoticeResponse_ADD(HttpContext context)
        {
            IES.JW.Model.User user = IES.Service.UserService.CurrentUser;
            NoticeBLL noticeBLL = new NoticeBLL();
            IES.JW.Model.NoticeResponse notice = new IES.JW.Model.NoticeResponse();
            notice.ResponseID = -1;
            notice.NoticeID = Convert.ToInt32(context.Request.Params["NoticeID"]);
            notice.Conten = context.Request.Params["Conten"].ToString();
            notice.UserID = user.UserID;

            IES.JW.Model.NoticeResponse addnotice = noticeBLL.NoticeResponse_ADD(notice);
            if (addnotice.ResponseID != -1)
            {
                context.Response.Write("true");
            }
            else
            {
                context.Response.Write("empty");
            }

        }
        //获取要发送的教师机构信息
        public void GetTeacher(HttpContext context)
        {
            DataTable dt = new DataTable();
            OrganizationBLL organizationBll = new OrganizationBLL();
            ShortOranization shortoranization = new ShortOranization();
            shortoranization.OrganizationIDs = context.Request.Params["OrganizationIDs"].ToString();

            dt = IES.Common.ListToDateUtil.ListToDataTable<IES.JW.Model.ShortOranization>(organizationBll.Organization_S_List(shortoranization));

            if (dt != null && dt.Rows.Count > 0)
            {
                context.Response.Write(Tools.JsonConvert.GetJSON(dt));
            }
            else
            {
                context.Response.Write("empty");
            }
        }


        public void SessionStudents(HttpContext context)
        {
            string SpecialtyIDs = context.Request.Params["hdnSSpecialIDs"].ToString();
            string SYearIDs = context.Request.Params["hdnSYearIDs"].ToString();
            context.Session["GetStudentsUrl"] = SpecialtyIDs + "@" + SYearIDs;
            context.Response.Write("empty");
        }
        //获取要发送的学生的专业信息
        public void GetStudent(HttpContext context)
        {
            DataTable dt = new DataTable();
            SpecialtyBLL specialtyBll = new SpecialtyBLL();
            ShortSpecialty shortspecialty = new ShortSpecialty();
            shortspecialty.SpecialtyIDs = context.Request.Params["SpecialtyIDs"].ToString();

            dt = IES.Common.ListToDateUtil.ListToDataTable<IES.JW.Model.ShortSpecialty>(specialtyBll.Specialty_Short_List(shortspecialty));

            if (dt != null && dt.Rows.Count > 0)
            {
                context.Response.Write(Tools.JsonConvert.GetJSON(dt));
            }
            else
            {
                context.Response.Write("empty");
            }
        }

        //发送通知
        public void AddNotice(HttpContext context)
        {
            IES.JW.Model.User user = IES.Service.UserService.CurrentUser;

            NoticeBLL noticeBLL = new NoticeBLL();
            IES.JW.Model.Notice notice = new IES.JW.Model.Notice();
            notice.Title = context.Request.Params["Title"].ToString();
            notice.Conten = context.Request.Params["Conten"].ToString();
            notice.IsTop = Convert.ToBoolean(context.Request.Params["IsTop"]);
            notice.IsForMail = Convert.ToBoolean(context.Request.Params["IsEmail"]);
            notice.IsForSMS = Convert.ToBoolean(context.Request.Params["IsMSM"]);
            notice.SysID = Convert.ToInt32(context.Request.Params["SysID"].ToString());
            notice.ModuleID = Convert.ToInt32(context.Request.Params["ModuleID"].ToString());
            notice.UserID = user.UserID;
            notice.EndDate = DateTime.Now.AddDays(30);
            notice.Source2 = context.Request.Params["Source2"].ToString();
            notice.SourceIDs = context.Request.Params["SourceIDs"].ToString();
            notice.Source = context.Request.Params["Source"].ToString();
            notice.SourceIDs2 = context.Request.Params["SourceIDs2"].ToString();
            notice.EntryDates = context.Request.Params["EntryDates"].ToString();
            notice = noticeBLL.Notice_ADD(notice);

            if (notice.NoticeID > 0)
            {
                if (notice.IsCanSendMsg == 0)
                {
                    context.Response.Write("False");
                }
                else
                {
                    context.Response.Write("True");
                }
            }
            else
            {
                context.Response.Write("empty");
            }
        }


    }
}