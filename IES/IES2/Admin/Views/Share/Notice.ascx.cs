using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using IES.SYS.Model;
using IES.Cache;
using IES.Security;
using IES.G2S.SYS.BLL;
using IES.Service;

namespace Admin.Views.Share
{
    public partial class Notice : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        public string NoticeList
        {
            get 
            {
                const string notice = " <li {0}> <i class='icon notice_icon'></i>{1}<p><a href='{2}'>[详细]</a> <span>{3}</span></p></li>";
                string userid = IESCookie.GetCookieValue("ies");
                IES.SYS.Model.User user = new IES.SYS.Model.User { UserID = Int32.Parse(userid) };
                user = UserService.User_Get(user);
                List<IES.SYS.Model.Notice> noticelist = UserService.User_Notice_List(user);
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < noticelist.Count; i++)
                {

                    if (i == 0)
                    {
                        sb.Append(string.Format(notice, "style='display:block;'", noticelist[i].Title, noticelist[i].NoticeID, noticelist[i].UpdateTime));
                    }
                    else
                    {
                        sb.Append(string.Format(notice, string.Empty, noticelist[i].Title, noticelist[i].NoticeID, noticelist[i].UpdateTime));
                    }
                }
                return sb.ToString();
                
            }
        }
    }
}