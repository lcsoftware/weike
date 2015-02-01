using IES.G2S.JW.DAL;
using IES.JW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.JW.BLL
{
    public class AuRoleBLL
    {
        #region 新增角色
        public AuRole AuRole_ADD(AuRole model)
        {
            return AuRoleDAL.AuRole_ADD(model);
        }
        #endregion

        #region 删除角色
        public bool AuRole_Del(AuRole model)
        {
            return AuRoleDAL.AuRole_Del(model);
        }
        #endregion

        #region 角色重命名
        public bool AuRole_Upd(AuRole model)
        {
            return AuRoleDAL.AuRole_Upd(model);
        }
        #endregion

        #region 角色下用户列表
        public List<User> AuUserRoleOrg_List(User model, int PageIndex, int PageSize)
        {
            return AuRoleDAL.AuUserRoleOrg_List(model, PageIndex, PageSize);
        }
        #endregion

        #region 角色下平台列表
        public List<Sys> AuRoleSys_List(AuRole model)
        {
            return AuRoleDAL.AuRoleSys_List(model);
        }
        #endregion

        #region 模块下权限列表
        public List<AuModule> AuModelAction_Tree(AuRole model)
        {
            return AuRoleDAL.AuModelAction_Tree(model);
        }
        #endregion

        #region 子系统下角色拥有权限列表
        public List<AuRoleModule> AuRoleModule_List(AuRole model)
        {
            return AuRoleDAL.AuRoleModule_List(model);
        }      
        #endregion

        #region 新增角色下用户
        public bool AuRoleUser_ADD(User model)
        {
            return AuRoleDAL.AuRoleUser_ADD(model);
        }
        #endregion

        #region 删除角色下用户
        public bool AuRoleUser_Del(User model)
        {
            return AuRoleDAL.AuRoleUser_Del(model);
        }
        #endregion

        #region 保存角色在子系统中权限
        public bool AuRoleModule_Edit(AuRole model)
        {
            return AuRoleDAL.AuRoleModule_Edit(model);
        }
        #endregion

        #region 模块操作列表
        public List<AuAction> AuAction_List()
        {
            return AuRoleDAL.AuAction_List();
        }
        #endregion
    }
}
