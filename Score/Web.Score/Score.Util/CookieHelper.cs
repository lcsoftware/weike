﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace App.Score.Util
{
    /// <summary>
    /// 不支持 cookie values
    /// </summary>
    public class CookieHelper
    { 
        #region 获取Cookie
 
        /// <summary> 
        /// 获得Cookie的值 
        /// </summary> 
        /// <param name="cookieName"></param> 
        /// <returns></returns> 
        public static string GetCookieValue(string cookieName)
        {
            HttpRequest request = HttpContext.Current.Request;
            if (request != null)
                return GetCookieValue(request.Cookies[cookieName]);
            return "";
        }

        /// <summary> 
        /// 获得Cookie的值 
        /// </summary> 
        /// <param name="cookie"></param> 
        /// <returns></returns> 
        public static string GetCookieValue(HttpCookie cookie)
        {
            if (cookie != null)
            {
                return HttpUtility.UrlDecode(cookie.Value);
            }
            return "";
        }

        /// <summary> 
        /// 获得Cookie 
        /// </summary> 
        /// <param name="cookieName"></param> 
        /// <returns></returns> 
        public static HttpCookie GetCookie(string cookieName)
        {
            HttpRequest request = HttpContext.Current.Request;
            if (request != null)
                return request.Cookies[cookieName];
            return null;
        }

        #endregion

        #region 删除Cookie


        /// <summary> 
        /// 删除Cookie
        /// </summary> 
        /// <param name="cookieName"></param> 
        public static void RemoveCookie(string cookieName)
        {
            HttpResponse response = HttpContext.Current.Response;
            if (response != null)
            {
                HttpCookie cookie = response.Cookies[cookieName];
                if (cookie != null)
                {
                    response.Cookies.Remove(cookieName);
                }
            }
        }
        /// <summary> 
        /// 使Cookie无效
        /// </summary> 
        /// <param name="cookieName"></param> 
        public static void CookieExpired(string cookieName)
        {
            HttpResponse response = HttpContext.Current.Response;
            HttpCookie myCookie = new HttpCookie(cookieName);
            myCookie.Expires = DateTime.Now.AddDays(-1d);
            response.Cookies.Add(myCookie);
        }
        #endregion

        #region 设置/修改Cookie


        /// <summary> 
        /// 设置Cookie 
        /// </summary> 
        /// <param name="cookieName"></param> 
        /// <param name="key"></param> 
        /// <param name="value"></param> 
        /// <param name="expires"></param> 
        public static void SetCookie(string cookieName, string value, DateTime? expires)
        {
            HttpResponse response = HttpContext.Current.Response;
            if (response != null)
            {
                HttpCookie cookie = response.Cookies[cookieName];
                if (cookie != null)
                {
                    cookie.Value = HttpUtility.UrlEncode(value);
                    if (expires != null)
                        cookie.Expires = expires.Value;
                    response.SetCookie(cookie);
                }
            }

        }

        #endregion

        #region 添加Cookie



        /// <summary> 
        /// 添加为Cookie.Values集合 
        /// </summary> 
        /// <param name="cookieName"></param> 
        /// <param name="key"></param> 
        /// <param name="value"></param> 
        /// <param name="expires"></param> 
        public static void AddCookie(string cookieName, string value, DateTime expires)
        {
            //Guard.IsNotNullOrEmpty(cookieName, "cookieName");

            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Expires = expires;
            cookie.Value = value;
            AddCookie(cookie);
        }

        /// <summary> 
        /// 添加Cookie 
        /// </summary> 
        /// <param name="cookie"></param> 
        public static void AddCookie(HttpCookie cookie)
        {
            HttpResponse response = HttpContext.Current.Response;
            if (response != null)
            {
                //指定客户端脚本是否可以访问[默认为false] 
                cookie.HttpOnly = true;
                cookie.Path = "/";
                //设置跨域,这样在其它二级域名下就都可以访问到了 
                //cookie.Domain = "nas.com"; 
                response.AppendCookie(cookie);
            }
        }

        #endregion


    }
}
