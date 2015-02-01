/**  版本信息模板在安装目录下，可自行修改。
* AuUserRoleOrg.cs
*
* 功 能： N/A
* 类 名： AuUserRoleOrg
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 9:12:45   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace IES.JW.Model
{
    /// <summary>
    /// 用户角色
    /// </summary>
    [Serializable]
    public partial class AuUserRoleOrg
    {
        public AuUserRoleOrg()
        { }
        #region Model
        private int _userroleid;
        private int _userid;
        private int _roleid;
        private int _organizationid = 0;
        /// <summary>
        /// 
        /// </summary>
        public int UserRoleID
        {
            set { _userroleid = value; }
            get { return _userroleid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int RoleID
        {
            set { _roleid = value; }
            get { return _roleid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int OrganizationID
        {
            set { _organizationid = value; }
            get { return _organizationid; }
        }
        #endregion Model

    }
}

