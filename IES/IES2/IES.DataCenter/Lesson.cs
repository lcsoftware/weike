/**  版本信息模板在安装目录下，可自行修改。
* Lesson.cs
*
* 功 能： N/A
* 类 名： Lesson
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/11/27 8:34:58   N/A    初版
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
	/// 课节
	/// </summary>
	[Serializable]
	public partial class Lesson
	{
		public Lesson()
		{}
		#region Model
		private int _lessonid;
		private int _termid=0;
		private string _lessonno;
		private string _lessonname;
		private DateTime _starttime;
		private DateTime _endtime;
		private int _duration=40;
		/// <summary>
		/// 
		/// </summary>
		public int LessonID
		{
			set{ _lessonid=value;}
			get{return _lessonid;}
		}
		/// <summary>
		/// 0 表示 默认课节
		/// </summary>
		public int TermID
		{
			set{ _termid=value;}
			get{return _termid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LessonNo
		{
			set{ _lessonno=value;}
			get{return _lessonno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LessonName
		{
			set{ _lessonname=value;}
			get{return _lessonname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime StartTime
		{
			set{ _starttime=value;}
			get{return _starttime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime EndTime
		{
			set{ _endtime=value;}
			get{return _endtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Duration
		{
			set{ _duration=value;}
			get{return _duration;}
		}
		#endregion Model

	}
}

