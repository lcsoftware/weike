/**  版本信息模板在安装目录下，可自行修改。
* SpecialtyTeacher.cs
*
* 功 能： N/A
* 类 名： SpecialtyTeacher
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/11/27 8:35:02   N/A    初版
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
	/// 专业推荐教师
	/// </summary>
	[Serializable]
	public partial class SpecialtyTeacher
	{
		public SpecialtyTeacher()
		{}
		#region Model
		private int _id;
		private int _specialtyid;
		private string _userno;
		private string _username;
		private string _brief;
		private int? _attachmentid=0;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
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
		/// 
		/// </summary>
		public string UserNo
		{
			set{ _userno=value;}
			get{return _userno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
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
		public int? AttachmentID
		{
			set{ _attachmentid=value;}
			get{return _attachmentid;}
		}
		#endregion Model

	}
}

