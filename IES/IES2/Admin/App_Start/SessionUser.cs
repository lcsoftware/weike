using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using IES.JW.Model;
using IES.Cache;
using IES.Security;
using IES.G2S.JW.BLL;
using IES.Service;

namespace Admin
{
    public class SessionUser
    {
        /// <summary>
        /// 获取当前登录用户的信息
        /// </summary>
        public static IES.JW.Model.User CurrentUser
        {
            get
            {
                string userid = IESCookie.GetCookieValue("ies");
                IES.JW.Model.User user = new IES.JW.Model.User { UserID = Int32.Parse(userid) };
                user = UserService.User_Get(user);
                return user;
            }
        }

        /// <summary>
        /// 获取当前登录用户的图片信息
        /// </summary>
        public static string CurrentUserIMG
        {
            get
            {
                return UserService.User_IMG_Get(CurrentUser);
            }
        }

       

    }
}