/**  版本信息模板在安装目录下，可自行修改。
* AuRoleModule.cs
*
* 功 能： N/A
* 类 名： AuRoleModule
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
namespace IES.SYS.Model
{
	/// <summary>
	/// 角色模块授权
	/// </summary>
	[Serializable]
	public partial class AuRoleModule
	{
		public AuRoleModule()
		{}
		#region Model
		private int _rolemoduleauid;
		private int _roleid;
		private int _moduleid;
		private int _actionid;
		/// <summary>
		/// 
		/// </summary>
		public int RoleModuleAuID
		{
			set{ _rolemoduleauid=value;}
			get{return _rolemoduleauid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int RoleID
		{
			set{ _roleid=value;}
			get{return _roleid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ModuleID
		{
			set{ _moduleid=value;}
			get{return _moduleid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ActionID
		{
			set{ _actionid=value;}
			get{return _actionid;}
		}
		#endregion Model

	}
}

