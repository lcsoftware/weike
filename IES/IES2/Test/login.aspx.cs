using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using IES.Common;
using IES.Security;

namespace Test
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lb1.Text = StringHelp.GetLastString("aa.aa.doc", ".");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            IES.JW.Model.User user = new IES.JW.Model.User { LoginName = tbuser.Text, Pwd = tbpassword.Text };


            IES.Service.UserService.Login(user);


            string ReturnUrl = Request.QueryString["ReturnUrl"];
            Response.Redirect(ReturnUrl);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            List<IES.Resource.Model.Attachment>  list =  IES.Service.FileService.AttachmentUpload();


        }
    }
}