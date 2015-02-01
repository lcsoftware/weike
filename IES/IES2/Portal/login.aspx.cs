using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Auth.Class;

namespace Portal
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 产生绝对唯一字符串，用于令牌
        /// </summary>
        /// <returns></returns>
        public string GetGuidString()
        {
            return Guid.NewGuid().ToString();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            lberror.Visible = false ;
            IES.JW.Model.User user = new IES.JW.Model.User { LoginName = txtAccount.Value , Pwd = txtPassport.Value };

            if ( IES.Service.UserService.Login(user) )
            {
                //产生令牌
                string tokenValue = this.GetGuidString();
                HttpCookie tokenCookie = new HttpCookie("Token");
                tokenCookie.Values.Add("Value", tokenValue);
                //    tokenCookie.Domain = "auth.com";
                Response.AppendCookie(tokenCookie);

                //产生主站凭证
                object info = txtAccount.Value + "wshgkjqbwhfbxlfrh" + txtPassport.Value ;
                Auth.Class.TokenCache.TokenInsert(tokenValue, info, DateTime.Now.AddMinutes(30));

                //跳转回分站
                if (Request.QueryString["BackURL"] != null)
                {
                    Response.Redirect("/portal/cas/GetToken.aspx?BackURL=" + Request.QueryString["BackURL"]);
                }
                else
                {
                    string url = string.Empty;
                    switch (((Button)sender).CommandName)
                    {
                        case "admin":
                            url = "";
                            break;
                        case "student":
                            url = "";
                            break;
                        case "teacher":
                            url = "";
                            break;
                    }
                    Response.Redirect( url );
                }


            }
            else
            {
                //Response.Write("抱歉，帐号或密码有误！");
                lberror.Visible = true;
            }
        }
    }
}