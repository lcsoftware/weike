/**  版本信息模板在安装目录下，可自行修改。
* AuModule.cs
*
* 功 能： N/A
* 类 名： AuModule
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
	/// IES系统模块
	/// </summary>
	[Serializable]
	public partial class AuModule
	{
		public AuModule()
		{}
		#region Model
		private int _moduleid;
		private int _sysid;
		private int _scope;
		private string _name;
		private int _parentid=0;
		private string _url;
		private bool _ismenu= false;
		private string _icon;
		private bool _isshow= true;
		private string _brief;
		private bool _isdeleted= false;
		private int _orde=1;
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
		public int SysID
		{
			set{ _sysid=value;}
			get{return _sysid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Scope
		{
			set{ _scope=value;}
			get{return _scope;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string URL
		{
			set{ _url=value;}
			get{return _url;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsMenu
		{
			set{ _ismenu=value;}
			get{return _ismenu;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Icon
		{
			set{ _icon=value;}
			get{return _icon;}
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
		public string Brief
		{
			set{ _brief=value;}
			get{return _brief;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsDeleted
		{
			set{ _isdeleted=value;}
			get{return _isdeleted;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Orde
		{
			set{ _orde=value;}
			get{return _orde;}
		}
		#endregion Model

	}
}

