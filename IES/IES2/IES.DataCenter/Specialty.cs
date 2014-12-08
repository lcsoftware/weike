/**  版本信息模板在安装目录下，可自行修改。
* Specialty.cs
*
* 功 能： N/A
* 类 名： Specialty
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/11/27 8:35:00   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace IES.DataCenter.Model
{
	/// <summary>
	/// 专业
	/// </summary>
	[Serializable]
	public partial class Specialty
	{
		public Specialty()
		{}
		#region Model
		private int _specialtyid;
		private string _specialtyno;
		private int? _parentid=0;
		private string _specialtyname;
		private string _specialtynameen;
		private int? _schoolinglength=4;
		private int _organizationid;
		private int? _specialtytypeid;
		private string _introduction;
		private string _introductionen;
		private bool _isshow= false;
		/// <summary>
		/// 
		/// </summary>
		public int SpecialtyID
		{
			set{ _specialtyid=value;}
			get{return _specialtyid;}
		}
		/// <summary>
		/// 专业代码
		/// </summary>
		public string SpecialtyNo
		{
			set{ _specialtyno=value;}
			get{return _specialtyno;}
		}
		/// <summary>
		/// 上级专业编号，用于专业方向中使用
		/// </summary>
		public int? ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 专业名称
		/// </summary>
		public string SpecialtyName
		{
			set{ _specialtyname=value;}
			get{return _specialtyname;}
		}
		/// <summary>
		/// 专业英文名称
		/// </summary>
		public string SpecialtyNameEn
		{
			set{ _specialtynameen=value;}
			get{return _specialtynameen;}
		}
		/// <summary>
		/// 学制
		/// </summary>
		public int? SchoolingLength
		{
			set{ _schoolinglength=value;}
			get{return _schoolinglength;}
		}
		/// <summary>
		/// 所属组织机构ID
		/// </summary>
		public int OrganizationID
		{
			set{ _organizationid=value;}
			get{return _organizationid;}
		}
		/// <summary>
		/// 所属专业类别ID（所属学科）
		/// </summary>
		public int? SpecialtyTypeID
		{
			set{ _specialtytypeid=value;}
			get{return _specialtytypeid;}
		}
		/// <summary>
		/// 介绍
		/// </summary>
		public string Introduction
		{
			set{ _introduction=value;}
			get{return _introduction;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IntroductionEn
		{
			set{ _introductionen=value;}
			get{return _introductionen;}
		}
		/// <summary>
		/// 是否在门户中显示
		/// </summary>
		public bool IsShow
		{
			set{ _isshow=value;}
			get{return _isshow;}
		}
		#endregion Model

	}
}

