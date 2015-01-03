using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq.Expressions;

using IES.Resource.Model;
using IES.G2S.Resource.BLL;
using IES.SYS.Model;
using IES.Cache;
using System.Web.Security;
using IES.Security;

namespace Test
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string userid = IESCookie.GetCookieValue("ies"); 

            IES.SYS.Model.User user = new User { UserID = 1, UserName = TextBox1.Text  };
            ICache cache = CacheFactory.Create();
            cache.Set( userid , "wsh", user);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string userid = IESCookie.GetCookieValue("ies"); 
            ICache cache = CacheFactory.Create();
            IES.SYS.Model.User user = cache.Get<IES.SYS.Model.User>( userid , "wsh");
            Label1.Text = user.UserName;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            IES.G2S.SYS.BLL.AuBLL aubll = new IES.G2S.SYS.BLL.AuBLL();

            List<AuModule>  AuModulelist =    aubll.AuModule_List();

            List<IES.SYS.Model.Menu> Menulist = aubll.Menu_List();


        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            IES.G2S.SYS.BLL.AuBLL aubll = new IES.G2S.SYS.BLL.AuBLL();

            List<AuModule> AuModulelist = aubll.AuModule_List();

            List<IES.SYS.Model.Menu> Menulist = aubll.Menu_List();

            var query = from t1 in AuModulelist
                        join t2 in Menulist on t1.ModuleID equals t2.ModuleID
                        select new { Name = t2.Title, Title = t2.ParentID };
            int i = 0;

            foreach (var g in query)
            {
                Console.WriteLine(string.Format("{0} {1} 年龄:{1}", g.Title, g.Name));
                i++;
            }

            Label1.Text = i.ToString();


        }
    }
}