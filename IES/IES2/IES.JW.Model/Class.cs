/**  版本信息模板在安装目录下，可自行修改。
* Class.cs
*
* 功 能： N/A
* 类 名： Class
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/1 13:35:30   N/A    初版
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
	/// 行政班、自然班
	/// </summary>
	[Serializable]
	public partial class Class
	{
		public Class()
		{}
		#region Model
		private int _classid;
		private string _classno;
		private string _classname;
		private int _organizationid;
		private int _specialtyid;
		private int _teacherid;
		private DateTime? _entrydate;
		private bool _isdeleted= false;
		/// <summary>
		/// 
		/// </summary>
		public int ClassID
		{
			set{ _classid=value;}
			get{return _classid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ClassNo
		{
			set{ _classno=value;}
			get{return _classno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ClassName
		{
			set{ _classname=value;}
			get{return _classname;}
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
		/// 
		/// </summary>
		public int SpecialtyID
		{
			set{ _specialtyid=value;}
			get{return _specialtyid;}
		}
		/// <summary>
		/// 辅导员用户ID
		/// </summary>
		public int TeacherID
		{
			set{ _teacherid=value;}
			get{return _teacherid;}
		}
		/// <summary>
		/// 入学日期（年级）
		/// </summary>
		public DateTime? EntryDate
		{
			set{ _entrydate=value;}
			get{return _entrydate;}
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

