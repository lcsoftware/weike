using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions; 


namespace Admin
{
    public partial class CAS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Token"] != null)
            {
                //分站凭证存在
                Response.Write("恭喜，分站凭证存在，您被授权访问该页面！");
            }
            else
            {
                //令牌验证结果
                if (Request.QueryString["Token"] != null)
                {
                    if (Request.QueryString["Token"] != "$Token$")
                    {
                        //持有令牌
                        string tokenValue = Request.QueryString["Token"];
                        //调用WebService获取主站凭证
                        TokenService.TokenServiceSoapClient tokenService = new TokenService.TokenServiceSoapClient();
                        TokenService.MySoapHeader header = new TokenService.MySoapHeader();
                        header.UserID = "able";
                        header.PassWord = "able@2003";
                        object o = tokenService.TokenGetCredence( header, tokenValue);
                        if (o != null)
                        {
                            //令牌正确
                            Session["Token"] = o;

                            //wshgkjqbwhfbxlfrh
                            string[] resultString = Regex.Split(o.ToString(), "wshgkjqbwhfbxlfrh", RegexOptions.IgnoreCase);

                            IES.JW.Model.User user = new IES.JW.Model.User { LoginName = resultString[0], Pwd = resultString[1] };

                            if (IES.Service.UserService.Login(user))
                            {
                                if (Request.QueryString["ReturnUrl"] != null)
                                {
                                    string ReturnUrl = Request.QueryString["ReturnUrl"];
                                    Response.Redirect(ReturnUrl);
                                }
                            }

                            //Response.Write("恭喜，令牌存在，您被授权访问该页面！");
                        }
                        else
                        {
                            //令牌错误
                            Response.Redirect(this.ReplaceToken());
                        }
                    }
                    else
                    {
                        //未持有令牌
                        Response.Redirect(this.ReplaceToken());
                    }
                }
                //未进行令牌验证，去主站验证
                else
                {
                    Response.Redirect(this.getTokenURL());
                }
            }
        }



        /// <summary>
        /// 获取带令牌请求的URL
        /// </summary>
        /// <returns></returns>
        private string getTokenURL()
        {
            string url = Request.Url.AbsoluteUri;
            Regex reg = new Regex(@"^.*\?.+=.+$");
            if (reg.IsMatch(url))
                url += "&Token=$Token$";
            else
                url += "?Token=$Token$";

            return "http://192.168.4.231:6667/portal/cas/gettoken.aspx?BackURL=" + Server.UrlEncode(url);
        }

        /// <summary>
        /// 去掉URL中的令牌
        /// </summary>
        /// <returns></returns>
        private string ReplaceToken()
        {
            string url = Request.Url.AbsoluteUri;
            url = Regex.Replace(url, @"(\?|&)Token=.*", "", RegexOptions.IgnoreCase);
            return "http://192.168.4.231:6667/portal/login.aspx?BackURL=" + Server.UrlEncode(url);
        }
    }
}