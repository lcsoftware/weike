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
		private string  _moduleid;
		private int _sysid;
		private int _scope;
		private string _name;
        private string  _parentid ;


		private string _brief;
		private bool _isdeleted= false;
		private int _orde=1;
		/// <summary>
		/// 
		/// </summary>
		public string  ModuleID
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
		public string  ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}


        public bool IsClassify { get; set; }





        public bool IsThirdApp { get; set; }

        public int OpenLevel { get; set; }


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

