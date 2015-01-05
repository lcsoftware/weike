/**  版本信息模板在安装目录下，可自行修改。
* AuAction.cs
*
* 功 能： N/A
* 类 名： AuAction
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 9:12:43   N/A    初版
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
	/// 模块的操作类型（新增、删除、编辑、导出、导入、评价、批阅。。。。。）；
	/// </summary>
	[Serializable]
	public partial class AuAction
	{
		public AuAction()
		{}
		#region Model
		private int _actionid;
		private string   _moduleid;
		private string _name;
		private int _orde;
		private string _url;
		/// <summary>
		/// 
		/// </summary>
		public int ActionID
		{
			set{ _actionid=value;}
			get{return _actionid;}
		}
		/// <summary>
		/// 
		/// </summary>
        public string ModuleID
		{
			set{ _moduleid=value;}
			get{return _moduleid;}
		}
		/// <summary>
		/// 操作值 如新增编辑  ；  删除  ；导出；导入批阅
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
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
		public string URL
		{
			set{ _url=value;}
			get{return _url;}
		}
		#endregion Model

	}
}

