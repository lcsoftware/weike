using IES.Cache;
using IES.G2S.JW.DAL;
using IES.JW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.JW.BLL
{
    public class AuBLL
    {
        #region 列表



        


        /// <summary>
        /// 获取系统的模块列表

        /// </summary>
        /// <returns></returns>
        public List<AuModule> AuModule_List()
        {
            ICache cache = CacheFactory.Create();
            if (!cache.Exists(string.Empty, "AuModule"))
            {
                List<AuModule> aumodulelist = AuDAL.AuModule_List();
                cache.Set(string.Empty, "AuModule", aumodulelist);
                return aumodulelist;
            }
            else
            {
                return cache.Get<List<AuModule>>(string.Empty, "AuModule");
            }
        }

        /// <summary>
        /// 获取模块的行为列表

        /// </summary>
        /// <returns></returns>
        public List<AuAction> AuAction_List()
        {
            ICache cache = CacheFactory.Create();
            if (!cache.Exists(string.Empty, "AuAction"))
            {
                List<AuAction> auactionlist = AuDAL.AuAction_List();
                cache.Set(string.Empty, "AuAction", auactionlist);
                return auactionlist;
            }
            else
            {
                return cache.Get<List<AuAction>>(string.Empty, "AuAction");
            }

        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public List<AuRole> AuRole_List()
        {
            ICache cache = CacheFactory.Create();
            if (!cache.Exists(string.Empty, "AuRole"))
            {
                List<AuRole> aurolelist = AuDAL.AuRole_List();
                cache.Set(string.Empty, "AuRole", aurolelist);
                return aurolelist;
            }
            else
            {
                return cache.Get<List<AuRole>>(string.Empty, "AuRole");
            }
        }

        /// <summary>
        /// 获取角色模块列表
        /// </summary>
        /// <returns></returns>
        public List<AuRoleModule> AuRoleModule_List()
        {
            ICache cache = CacheFactory.Create();
            if (!cache.Exists(string.Empty, "AuRoleModule"))
            {
                List<AuRoleModule> aurolemodulelist = AuDAL.AuRoleModule_List();
                cache.Set(string.Empty, "AuRoleModule", aurolemodulelist);
                return aurolemodulelist;
            }
            else
            {
                return cache.Get<List<AuRoleModule>>(string.Empty, "AuRoleModule");
            }

        }

        /// <summary>
        /// 获取模块角色列表
        /// </summary>
        /// <returns></returns>
        public List<AuUserRoleOrg> AuUserRoleOrg_List()
        {

            ICache cache = CacheFactory.Create();
            if (!cache.Exists(string.Empty, "AuUserRoleOrg"))
            {
                List<AuUserRoleOrg> auuserroleorglist = AuDAL.AuUserRoleOrg_List();
                cache.Set(string.Empty, "AuUserRoleOrg", auuserroleorglist);
                return auuserroleorglist;
            }
            else
            {
                return cache.Get<List<AuUserRoleOrg>>(string.Empty, "AuUserRoleOrg");
            }
        }


        /// <summary>
        /// 获取系统的菜单

        /// </summary>
        /// <returns></returns>
        public List<Menu> Menu_List()
        {
            ICache cache = CacheFactory.Create();
            if (!cache.Exists(string.Empty, "Menu"))
            {
                List<Menu> menulist = AuDAL.Menu_List();
                cache.Set(string.Empty, "Menu", menulist);
                return menulist;
            }
            else
            {
                return cache.Get<List<Menu>>(string.Empty, "Menu");
            }
        }

        #endregion
    }
}
