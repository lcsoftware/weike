using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IES.SYS.Model;

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
            IES.G2S.SYS.BLL.AuBLL aubll = new IES.G2S.SYS.BLL.AuBLL();
            return aubll.Menu_List();
        }

        /// <summary>
        /// 获取模块列表
        /// </summary>
        /// <returns></returns>
        public static List<AuModule> AuModule_List()
        {
            IES.G2S.SYS.BLL.AuBLL aubll = new IES.G2S.SYS.BLL.AuBLL();
            return aubll.AuModule_List();
        }

        /// <summary>
        /// 加载模块行为列表
        /// </summary>
        /// <returns></returns>
        public static List<AuAction> AuAction_List()
        {
            IES.G2S.SYS.BLL.AuBLL aubll = new IES.G2S.SYS.BLL.AuBLL();
            return aubll.AuAction_List();
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public static List<AuRole> AuRole_List()
        {
            IES.G2S.SYS.BLL.AuBLL aubll = new IES.G2S.SYS.BLL.AuBLL();
            return aubll.AuRole_List();
        }

        /// <summary>
        /// 获取角色模块列表
        /// </summary>
        /// <returns></returns>
        public static List<AuRoleModule> AuRoleModule_List()
        {
            IES.G2S.SYS.BLL.AuBLL aubll = new IES.G2S.SYS.BLL.AuBLL();
            return aubll.AuRoleModule_List();
        }

        /// <summary>
        /// 用户角色组织机构列表
        /// </summary>
        /// <returns></returns>
        public static List<AuUserRoleOrg> AuUserRoleOrg_List()
        {
            IES.G2S.SYS.BLL.AuBLL aubll = new IES.G2S.SYS.BLL.AuBLL();
            return aubll.AuUserRoleOrg_List();
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
            List<Menu> leftmenulist = Menu_List().Where( x => x.ParentID.Equals(ParentID) && x.Scope == scope).ToList<Menu>();
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

    }
}
