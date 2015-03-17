using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IES.JW.Model;
using IES.CC.OC.Model;

namespace IES.Service
{

    /// <summary>
    /// 用户获取相关的权限

    /// </summary>
    public  class AuService
    {

        /// <summary>
        /// 系统用户授权信息加载
        /// </summary>
        /// <returns></returns>
        public static bool  AuLoad()
        {
            try
            {
                Menu_List();
                AuModule_List();
                AuAction_List();
                AuRole_List();
                AuRoleModule_List();
                AuUserRoleOrg_List();
                AuRoleModule_ByUserRole_List();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
            
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        public static List<Menu> Menu_List()
        {
            IES.G2S.JW.BLL.AuBLL aubll = new IES.G2S.JW.BLL.AuBLL();
            return aubll.Menu_List();
        }

        /// <summary>
        /// 获取模块列表
        /// </summary>
        /// <returns></returns>
        public static List<AuModule> AuModule_List()
        {
            IES.G2S.JW.BLL.AuBLL aubll = new IES.G2S.JW.BLL.AuBLL();
            return aubll.AuModule_List();
        }

        /// <summary>
        /// 加载模块行为列表
        /// </summary>
        /// <returns></returns>
        public static List<AuAction> AuAction_List()
        {
            IES.G2S.JW.BLL.AuBLL aubll = new IES.G2S.JW.BLL.AuBLL();
            return aubll.AuAction_List();
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public static List<AuRole> AuRole_List()
        {
            IES.G2S.JW.BLL.AuBLL aubll = new IES.G2S.JW.BLL.AuBLL();
            return aubll.AuRole_List();
        }

        /// <summary>
        /// 获取角色模块列表
        /// </summary>
        /// <returns></returns>
        public static List<AuRoleModule> AuRoleModule_List()
        {
            IES.G2S.JW.BLL.AuBLL aubll = new IES.G2S.JW.BLL.AuBLL();
            return aubll.AuRoleModule_List();
        }

        /// <summary>
        ///获取用户模块行为角色列表
        /// </summary>
        /// <returns></returns>
        public static List<AuRoleModule> AuRoleModule_ByUserRole_List()
        {
            IES.G2S.JW.BLL.AuBLL aubll = new IES.G2S.JW.BLL.AuBLL();
            return aubll.AuRoleModule_ByUserRole_List();
        }

        /// <summary>
        /// 用户角色组织机构列表
        /// </summary>
        /// <returns></returns>
        public static List<AuUserRoleOrg> AuUserRoleOrg_List()
        {
            IES.G2S.JW.BLL.AuBLL aubll = new IES.G2S.JW.BLL.AuBLL();
            return aubll.AuUserRoleOrg_List();
        }




        /// <summary>
        /// 获取所有用户的指定在线课程模块列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        public static List<AuUserModule> AuUserModule_List(string ocid)
        {
            IES.G2S.JW.BLL.AuBLL aubll = new IES.G2S.JW.BLL.AuBLL();
            return aubll.AuUserModule_List(ocid);
        }







        /// <summary>
        /// 获取顶部的导航菜单

        /// </summary>
        /// <param name="model"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        public static List<Menu> Menu_Top_List( int scope  )
        {
            User user = UserService.CurrentUser;
            List<Menu> topmenulist = Menu_List().Where(x => x.ParentID.Equals("0") && x.Scope == scope).ToList<Menu>();

            return topmenulist;
        }

        /// <summary>
        /// 获取左侧的导航菜单

        /// </summary>
        /// <param name="model"></param>
        /// <param name="ParentID"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        public static List<Menu> Menu_Left_List(string ParentID , int scope )
        {
            //if (ParentID == "B2")
            //{
            //    if (!UserService.IsTeacher)
            //    {
            //        ParentID = "C2";
            //        scope = 3;
            //    }
            //}


            List<Menu> leftmenulist  = new List<Menu>();
            if( ParentID != "D" )
                    leftmenulist = Menu_List().Where( x => x.ParentID.Equals(ParentID) && x.Scope == scope).ToList<Menu>();
            else
                    leftmenulist = Menu_List().Where( x => x.ParentID.Equals(ParentID) ).ToList<Menu>();


            return leftmenulist;
        }

        /// <summary>
        /// 获取用户的下拉菜单，该菜单为全局菜单
        /// </summary>
        /// <returns></returns>
        public static List<Menu> Menu_UserDropDown_List()
        {
            List<Menu> userdropdownmenulist = Menu_List().Where(x => x.ParentID.Equals("D") && x.Scope == 0 ).ToList<Menu>();
            return userdropdownmenulist ;
        }


        /// <summary>
        /// 获取上级菜单列表
        /// </summary>
        /// <param name="MenuID"></param>
        /// <returns></returns>
        public static Menu ParentMenu(string MenuID)
        {
            if (MenuID == null)
                return new Menu();
            Menu parentmenu = Menu_List().Where(x => x.MenuID == MenuID.Substring( 0,MenuID.Length -1 ) ).Single<Menu>();
            return parentmenu;
        }


        /// <summary>
        /// 当前菜单编号
        /// </summary>
        /// <param name="MenuID"></param>
        /// <returns></returns>
        public static Menu CurrentMenu( string MenuID )
        {
            if (MenuID == null)
                return new Menu();
            Menu menu = Menu_List().Where( x => x.MenuID == MenuID ).Single<Menu>();
            return menu;
        }


    }
}
