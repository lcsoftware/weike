/**  版本信息模板在安装目录下，可自行修改。
* SpecialtyType.cs
*
* 功 能： N/A
* 类 名： SpecialtyType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/1 13:35:36   N/A    初版
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
	/// 专业推荐教师
	/// </summary>
	[Serializable]
	public partial class SpecialtyType
	{
		public SpecialtyType()
		{}
		#region Model
		private int _specialtytypeid;
		private string _specialtytypeno;
		private string _specialtytypename;
		private int _parentid;
		private bool _isdeleted= false;
		/// <summary>
		/// 
		/// </summary>
		public int SpecialtyTypeID
		{
			set{ _specialtytypeid=value;}
			get{return _specialtytypeid;}
		}
		/// <summary>
		/// 专业代码
		/// </summary>
		public string SpecialtyTypeNo
		{
			set{ _specialtytypeno=value;}
			get{return _specialtytypeno;}
		}
		/// <summary>
		/// 专业名称
		/// </summary>
		public string SpecialtyTypeName
		{
			set{ _specialtytypename=value;}
			get{return _specialtytypename;}
		}
		/// <summary>
		/// 上级编号
		/// </summary>
		public int ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
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

