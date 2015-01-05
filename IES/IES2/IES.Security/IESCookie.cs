using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Web;

namespace IES.Security
{
    public class IESCookie
    {
        public static  bool ADDCookie( string value )
        {
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                                            1, // version 
                                            "userid", // name 
                                            DateTime.Now, // creation 
                                            DateTime.Now.AddMinutes(120),// Expiration 
                                            false , // Persistent 
                                            value ); // User data 
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket); //加密
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            HttpContext.Current.Response.Cookies.Add(authCookie);
            return true;
        }

        public static string GetCookieValue(string key)
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[ key ];
            if (authCookie == null)
                return string.Empty;
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            return authTicket.UserData;
        }

        

    }
}
