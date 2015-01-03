/**  版本信息模板在安装目录下，可自行修改。
* Organization.cs
*
* 功 能： N/A
* 类 名： Organization
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/1 13:35:33   N/A    初版
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
	/// 组织机构表
	/// </summary>
	[Serializable]
	public partial class Organization
    {
        #region  补充信息
        public string OrganizationTypeName { get; set; }

        public int ChildNoeds { get; set; }

        #endregion 

        public Organization()
		{}
		#region Model
		private int _organizationid;
		private string _organizationno;
		private string _organizationname;
		private string _organizationnameen;
		private int _parentid=0;
		private int? _organizationtypeid;
		private string _introduction;
		private string _introductionen;
		private bool _isshow= true;
		private string _link;
		private bool _isteaching= true;
		private bool _linkstatus= false;
		private bool _isdeleted= false;
		/// <summary>
		/// 
		/// </summary>
		public int OrganizationID
		{
			set{ _organizationid=value;}
			get{return _organizationid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OrganizationNo
		{
			set{ _organizationno=value;}
			get{return _organizationno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OrganizationName
		{
			set{ _organizationname=value;}
			get{return _organizationname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OrganizationNameEn
		{
			set{ _organizationnameen=value;}
			get{return _organizationnameen;}
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
		public int? OrganizationTypeID
		{
			set{ _organizationtypeid=value;}
			get{return _organizationtypeid;}
		}
		/// <summary>
		/// 
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
		/// 门户是否显示
		/// </summary>
		public bool IsShow
		{
			set{ _isshow=value;}
			get{return _isshow;}
		}
		/// <summary>
		/// 外部链接地址
		/// </summary>
		public string Link
		{
			set{ _link=value;}
			get{return _link;}
		}
		/// <summary>
		/// 是否开课机构
		/// </summary>
		public bool IsTeaching
		{
			set{ _isteaching=value;}
			get{return _isteaching;}
		}
		/// <summary>
		/// 是否启用外部链接
		/// </summary>
		public bool LinkStatus
		{
			set{ _linkstatus=value;}
			get{return _linkstatus;}
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

