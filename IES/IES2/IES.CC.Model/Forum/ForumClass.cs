/**  版本信息模板在安装目录下，可自行修改。
* ForumClass.cs
*
* 功 能： N/A
* 类 名： ForumClass
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 13:12:01   N/A    初版
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
	/// 班级论坛
	/// </summary>
	[Serializable]
	public partial class ForumClass
	{
		public ForumClass()
		{}
		#region Model
		private int _id;
		private int _forumtypeid;
		private int _teachingclassid;
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
		public int ForumTypeID
		{
			set{ _forumtypeid=value;}
			get{return _forumtypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int TeachingClassID
		{
			set{ _teachingclassid=value;}
			get{return _teachingclassid;}
		}
		#endregion Model

	}
}

