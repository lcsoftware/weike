/**  版本信息模板在安装目录下，可自行修改。
* ForumType.cs
*
* 功 能： N/A
* 类 名： ForumType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 13:12:03   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace IES.CC.Forum.Model
{
	/// <summary>
	/// 论坛版块
	/// </summary>
	[Serializable]
	public partial class ForumType
	{
		public ForumType()
		{}
		#region Model
		private int _forumtypeid;
		private int _courseid;
		private string _title;
		private int _orde;
		private bool _istop= false;
		private string _brief;
		private bool _ispublic= true;
		private int? _userid;
		private bool _isdeleted= false;
		/// <summary>
		/// 
		/// </summary>
		public int ForumTypeID
		{
			set{ _forumtypeid=value;}
			get{return _forumtypeid;}
		}
		/// <summary>
		/// 是否设为精选版块
		/// </summary>
		public int CourseID
		{
			set{ _courseid=value;}
			get{return _courseid;}
		}
		/// <summary>
		/// 版块名称
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
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
		/// 是否设为精选版块
		/// </summary>
		public bool IsTop
		{
			set{ _istop=value;}
			get{return _istop;}
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
		/// 是否课程公共版块
		/// </summary>
		public bool IsPublic
		{
			set{ _ispublic=value;}
			get{return _ispublic;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? UserID
		{
			set{ _userid=value;}
			get{return _userid;}
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

