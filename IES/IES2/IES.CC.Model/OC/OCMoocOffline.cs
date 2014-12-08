/**  版本信息模板在安装目录下，可自行修改。
* OCMoocOffline.cs
*
* 功 能： N/A
* 类 名： OCMoocOffline
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 20:19:27   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace IES.CC.OC.Model
{
	/// <summary>
	/// Mooc见面课
	/// </summary>
	[Serializable]
	public partial class OCMoocOffline
	{
		public OCMoocOffline()
		{}
		#region Model
		private int _moocofflineid;
		private int _ocid;
		private string _title;
		private string _tasktype;
		private decimal _hours;
		private string _username;
		private int? _chapterid;
		private string _purpose;
		private string _points;
		private string _grouping;
		private string _score;
		private string _resource;
		private string _assess;
		/// <summary>
		/// 
		/// </summary>
		public int MoocOfflineID
		{
			set{ _moocofflineid=value;}
			get{return _moocofflineid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int OCID
		{
			set{ _ocid=value;}
			get{return _ocid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 网络面授；实践实验；小组讨论
		/// </summary>
		public string TaskType
		{
			set{ _tasktype=value;}
			get{return _tasktype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Hours
		{
			set{ _hours=value;}
			get{return _hours;}
		}
		/// <summary>
		/// 授课教师
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 建议授课章节
		/// </summary>
		public int? ChapterID
		{
			set{ _chapterid=value;}
			get{return _chapterid;}
		}
		/// <summary>
		/// 本次见面课目的
		/// </summary>
		public string Purpose
		{
			set{ _purpose=value;}
			get{return _purpose;}
		}
		/// <summary>
		/// 教师要点说明
		/// </summary>
		public string Points
		{
			set{ _points=value;}
			get{return _points;}
		}
		/// <summary>
		/// 学生组织说明
		/// </summary>
		public string Grouping
		{
			set{ _grouping=value;}
			get{return _grouping;}
		}
		/// <summary>
		/// 评价与成绩说明
		/// </summary>
		public string Score
		{
			set{ _score=value;}
			get{return _score;}
		}
		/// <summary>
		/// 资源配套说明
		/// </summary>
		public string Resource
		{
			set{ _resource=value;}
			get{return _resource;}
		}
		/// <summary>
		/// 考核方式
		/// </summary>
		public string Assess
		{
			set{ _assess=value;}
			get{return _assess;}
		}
		#endregion Model

	}
}

