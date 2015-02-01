using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.JW.Model;
using IES.CC.OC.Model;
using IES.G2S.JW.DAL;
using IES.G2S.JW.BLL;
using IES.Cache;
using IES.Resource.Model;
using IES.G2S.Resource.DAL;
using IES.Security;

namespace IES.Service.Common
{
    public class Browse
    {
        /// <summary>
        /// 获取分页大小
        /// </summary>
        public static int PageSize
        {
            get
            {
                ICache cache = CacheFactory.Create();
                if (cache.Exists(UserService.CurrentUser.UserID.ToString(), "PageSize"))
                    return cache.Get<int>(UserService.CurrentUser.UserID.ToString(), "PageSize");
                else
                    return 20;
            }
        }

        /// <summary>
        /// 设置分页大小
        /// </summary>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static int SetPageSize(int PageSize)
        {
            ICache cache = CacheFactory.Create();
            cache.Set<int>(UserService.CurrentUser.UserID.ToString(), "PageSize", PageSize);
            return PageSize;
        }

        /// <summary>
        /// 获取用户当前空间
        /// </summary>
        public static string UserSpace
        {
            get
            {
                ICache cache = CacheFactory.Create();
                if (cache.Exists(UserService.CurrentUser.UserID.ToString(), "UserSpace"))
                    return cache.Get<string>(UserService.CurrentUser.UserID.ToString(), "UserSpace");
                else
                    return "2";
            }
        }

        /// <summary>
        /// 设置用户的空间
        /// </summary>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static void SetUserSpace(string UserSpace)
        {
            ICache cache = CacheFactory.Create();
            cache.Set<string>(UserService.CurrentUser.UserID.ToString(), "UserSpace", UserSpace);
            cache.SetExpire(UserService.CurrentUser.UserID.ToString(), "TopMenu");
            cache.SetExpire(UserService.CurrentUser.UserID.ToString(), "LeftMenu");
        }


        /// <summary>
        /// 设置当天顶部菜单
        /// </summary>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static void SetTopMenu(string Menu)
        {
            ICache cache = CacheFactory.Create();
            cache.Set<string>(UserService.CurrentUser.UserID.ToString(), "TopMenu", Menu);
        }


        /// <summary>
        /// 获取当前顶部菜单
        /// </summary>
        public static string TopMenu
        {
            get
            {
                ICache cache = CacheFactory.Create();
                if (cache.Exists(UserService.CurrentUser.UserID.ToString(), "TopMenu"))
                    return cache.Get<string>(UserService.CurrentUser.UserID.ToString(), "TopMenu");
                else
                {
                    if ( UserSpace == "2")
                        return "B1";
                    else
                        return "C1";
                }           
            }
        }



        public static void SetLeftMenu(string Menu)
        {
            ICache cache = CacheFactory.Create();
            cache.Set<string>(UserService.CurrentUser.UserID.ToString(), "LeftMenu", Menu);
        }

        /// <summary>
        /// 获取当前顶部菜单
        /// </summary>
        public static string LeftMenu
        {
            get
            {
                ICache cache = CacheFactory.Create();
                if (cache.Exists(UserService.CurrentUser.UserID.ToString(), "LeftMenu"))
                    return cache.Get<string>(UserService.CurrentUser.UserID.ToString(), "LeftMenu");
                else
                {
                    if (UserSpace == "2")
                        return "B10";
                    else
                        return "C11";
                }
            }
        }


        public static void SetCurrentOC( string OCID )
        {
            ICache cache = CacheFactory.Create();
            cache.Set<string>(UserService.CurrentUser.UserID.ToString(), "CurrentOC", OCID );
        }

        /// <summary>
        /// 获取当前顶部菜单
        /// </summary>
        public static string CurrentOC
        {
            get
            {
                ICache cache = CacheFactory.Create();
                if (cache.Exists(UserService.CurrentUser.UserID.ToString(), "CurrentOC"))
                    return cache.Get<string>(UserService.CurrentUser.UserID.ToString(), "CurrentOC");
                else
                {
                    return  "0" ;
                }
            }
        }




    }
}
