/**  版本信息模板在安装目录下，可自行修改。
* AuRole.cs
*
* 功 能： N/A
* 类 名： AuRole
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 9:12:44   N/A    初版
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
	/// 角色
	/// </summary>
	[Serializable]
	public partial class AuRole
	{
		public AuRole()
		{}
		#region Model
		private int _roleid;
		private string _title;
		private string _brief;
		private bool _isshow= true;
		private int _orde=1;
		private int? _sysid;
		private bool _isdeleted= false;
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
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Brief
		{
			set{ _brief=value;}
			get{return _brief;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsShow
		{
			set{ _isshow=value;}
			get{return _isshow;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Orde
		{
			set{ _orde=value;}
			get{return _orde;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SysID
		{
			set{ _sysid=value;}
			get{return _sysid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsDeleted
		{
			set{ _isdeleted=value;}
			get{return _isdeleted;}
		}
		#endregion Model

	}
}

