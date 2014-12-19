
namespace App.AngularMvc.DataProvider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Services;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Data;

    public partial class UserProvider : System.Web.UI.Page
    {
        [WebMethod]
        public static int Login(string userName, string password)
        {
            return userName.Equals("test") && password.Equals("123") ? 1 : 0; 
        }

        [WebMethod]
        public static int Register(string userName, string password)
        {
            return userName.Equals("test") && password.Equals("123") ? 1 : 0; 
        } 
       
    } 
}