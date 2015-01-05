using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.JW.Model;
using IES.SYS.Model;
using IES.CC.OC.Model;
using IES.G2S.SYS.DAL;
using IES.G2S.SYS.BLL ;
using IES.Cache;
using IES.Resource.Model;
using IES.G2S.Resource.DAL;
using IES.Security;

namespace IES.Service
{
    public class UserService
    {
        /// <summary>
        /// 获取用户的在线课程列表

        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static List<OC> User_OC_List(IES.SYS.Model.User model)
        {
            IES.G2S.SYS.BLL.UserBLL userbll = new IES.G2S.SYS.BLL.UserBLL();

            return userbll.User_OC_List(model);
        }

        /// <summary>
        /// 获取用户的授权模块列表，模块的行为列表

        /// </summary>
        /// <param name="userid">用户编号</param>
        /// <returns></returns>
        public static UserAuInfo UserAuInfo_Info_Get(int userid)
        {
            return new UserAuInfo();
        }


        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Login(User model)
        {
            try
            {
                IES.G2S.SYS.BLL.UserBLL userbll = new IES.G2S.SYS.BLL.UserBLL();
                model = userbll.User_Get(model);
                IESCookie.ADDCookie(model.UserID.ToString());
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        /// <summary>
        /// 获取用户的详细信息

        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static User User_Get(User model)
        {
            try
            {
                IES.G2S.SYS.BLL.UserBLL userbll = new IES.G2S.SYS.BLL.UserBLL();
                return userbll.User_Get(model);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取用户的图片

        /// </summary>
        /// <returns></returns>
        public static string User_IMG_Get(User user)
        {
            Attachment attachment = new Attachment { Source = "User" };
            return FileService.Attachment_List(attachment).First(x => x.SourceID.Equals(user.UserID)).DownURL;
            //return null;
        }

        /// <summary>
        /// 获取系统通知信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static List<Notice> User_Notice_List(User user)
        {
            NoticeBLL noticebll = new NoticeBLL();

            return noticebll.Notice_Receive_List(user);
        }



        /// <summary>
        /// 获取当前登录用户的信息

        /// </summary>
        public static IES.SYS.Model.User CurrentUser
        {
            get
            {
                string userid = "1";// IESCookie.GetCookieValue("ies");

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
