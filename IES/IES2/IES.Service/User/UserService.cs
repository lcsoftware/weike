using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.JW.Model;
using IES.CC.OC.Model;
using IES.G2S.JW.DAL;
using IES.G2S.JW.BLL ;
using IES.Cache;
using IES.Resource.Model;
using IES.G2S.Resource.DAL;
using IES.Security;
using IES.G2S.OC.BLL.Team;

namespace IES.Service
{
    public class UserService
    {

        /// <summary>
        /// 获取用户的在线课程列表

        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static List<OC> User_OC_List(IES.JW.Model.User model)
        {
            IES.G2S.JW.BLL.UserBLL userbll = new IES.G2S.JW.BLL.UserBLL();

            return userbll.User_OC_List(model);
        }

        public static List<OC> OC_Simple_List(IES.JW.Model.User model)
        {
            IES.G2S.OC.BLL.OC.OCBLL bll = new G2S.OC.BLL.OC.OCBLL();

            return bll.OC_Simple_List( model.UserID , Int32.Parse(model.CurrentUserSpace )  );
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
                IES.G2S.JW.BLL.UserBLL userbll = new IES.G2S.JW.BLL.UserBLL();
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
                IES.G2S.JW.BLL.UserBLL userbll = new IES.G2S.JW.BLL.UserBLL();
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
            try
            { 
                return FileService.Attachment_List(attachment).First(x => x.SourceID.Equals(user.UserID)).DownURL;
            }
            catch
            {
                return "/Images/default/User_M.jpg";
            }
          
        
            //return null;
        }

        /// <summary>
        /// 获取用户的所属在线课程用户编号
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<OCTeam> OCTeam_OCOwner_List
        {
            get
            {
                //TODO:
                string  userid =  IESCookie.GetCookieValue("ies");
                IES.G2S.OC.BLL.Team.OCTeamBLL bll = new G2S.OC.BLL.Team.OCTeamBLL();
                return bll.OCTeam_OCOwner_List(Int32.Parse(userid));   
            }
        }

        /// <summary>
        /// 获取在线课程的资源所属人
        /// </summary>
        /// <param name="ocid">在线课程编号</param>
        /// <returns></returns>
        public static int  User_OCOwner_Get(int ocid   )
        {
            List<OCTeam> octeamlist = OCTeam_OCOwner_List.Where(o => o.OCID == ocid && o.Role == 3).ToList<OCTeam>();
            if (octeamlist.Count > 0)
                return octeamlist[0].OwnerUserID;
            else
            {
                //TODO:
                string userid = IESCookie.GetCookieValue("ies");
                return Int32.Parse(userid);
            }          
        }


        /// <summary>
        /// 获取用户相关的教学班列表
        /// </summary>
        /// <param name="ocid"></param>
        /// <returns></returns>
        public static List<TeachingClass> TeachingClass_Owner_List(int ocid)
        {
            string userid = IESCookie.GetCookieValue("ies");
            IES.G2S.OC.BLL.OC.OCClassBLL bll = new G2S.OC.BLL.OC.OCClassBLL();
            return bll.TeachingClass_Owner_List( Int32.Parse(userid) , ocid );   
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


        #region 用户权限与角色

        /// <summary>
        /// 当前登录用户是否是管理员
        /// </summary>
        public static bool IsAdmin
        {
            get
            {
                return IES.Common.Delivery.IsInDeliveryList(2, CurrentUser.UserType, 128);
            }
        }

        /// <summary>
        /// 当前登录用户是否是教师
        /// </summary>
        public static bool IsTeacher
        {
            get
            {
                return IES.Common.Delivery.IsInDeliveryList(8, CurrentUser.UserType, 128);
            }
        }


        /// <summary>
        /// 当前登录用户是否是超级管理员
        /// </summary>
        public static bool IsSuperAdmin
        {
            get
            {
                return IES.Common.Delivery.IsInDeliveryList(1, CurrentUser.UserType, 128);
            }
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
        /// 当前登录用户的模块、行为列表信息
        /// </summary>
        public static List<AuRoleModule> CurrentUser_AuRoleModule_List
        {
            get
            {
                return AuService.AuRoleModule_ByUserRole_List().Where(x => x.UserID.Equals( CurrentUser.UserID )).ToList<AuRoleModule>();
            }
        }

        /// <summary>
        /// 获取当前登录用户的指定在线课程模块列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        public static List<AuUserModule> AuUserModule_UserID_List( string ocid  )
        {
            IES.G2S.JW.BLL.AuBLL aubll = new IES.G2S.JW.BLL.AuBLL();
            return aubll.AuUserModule_List(ocid).Where(x => x.UserID.Equals(CurrentUser.UserID)).ToList<AuUserModule>();
        }

        /// <summary>
        /// 获取当前登录用户所有在线课程角色列表
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static List<OCTeam> OCTeam_UserID_List()
        {
            IES.G2S.OC.BLL.Team.OCTeamBLL octeambll = new G2S.OC.BLL.Team.OCTeamBLL();
            return octeambll.OCTeam_UserID_List(CurrentUser.UserID);
        }


        /// <summary>
        /// 获取用户的在线课程角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static OCTeamRole OCTeam_Role_Get(OCTeamRole model)
        {
            return new OCTeamRoleBLL().OCTeam_Role_Get(model);
        }
        /// <summary>
        /// 获取用户所有在线课程角色
        /// </summary>
        /// <returns></returns>
        public static List<OCTeam> OC_ALLRole_Get()
        {
            return new OCTeamRoleBLL().OC_ALLRole_Get(CurrentUser.UserID);
        }

        #endregion
    }
}
