/**  版本信息模板在安装目录下，可自行修改。
* TermType.cs
*
* 功 能： N/A
* 类 名： TermType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/1 13:35:39   N/A    初版
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
	/// 学期类型
	/// </summary>
	[Serializable]
	public partial class TermType
	{
		public TermType()
		{}
		#region Model
		private int _termtypeid;
		private string _termtypeno;
		private string _termtypename;
		private int _orde;
		/// <summary>
		/// 
		/// </summary>
		public int TermTypeID
		{
			set{ _termtypeid=value;}
			get{return _termtypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TermTypeNo
		{
			set{ _termtypeno=value;}
			get{return _termtypeno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TermTypeName
		{
			set{ _termtypename=value;}
			get{return _termtypename;}
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

