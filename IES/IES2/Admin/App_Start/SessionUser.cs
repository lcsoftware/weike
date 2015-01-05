using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using IES.SYS.Model;
using IES.Cache;
using IES.Security;
using IES.G2S.SYS.BLL;
using IES.Service;

namespace Admin
{
    public class SessionUser
    {
        /// <summary>
        /// 获取当前登录用户的信息
        /// </summary>
        public static IES.SYS.Model.User CurrentUser
        {
            get
            {
                string userid = IESCookie.GetCookieValue("ies");
                IES.SYS.Model.User user = new IES.SYS.Model.User { UserID = Int32.Parse(userid) };
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