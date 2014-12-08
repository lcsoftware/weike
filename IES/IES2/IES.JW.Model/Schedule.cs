/**  版本信息模板在安装目录下，可自行修改。
* Schedule.cs
*
* 功 能： N/A
* 类 名： Schedule
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/1 13:35:34   N/A    初版
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
	/// 课表
	/// </summary>
	[Serializable]
	public partial class Schedule
	{
		public Schedule()
		{}
		#region Model
		private int _scheduleid;
		private int _termid;
		private string _termno;
		private string _weeknumbers;
		private int _weektype;
		private int _weeknum;
		private string _lessonnos;
		private int _classroomid;
		private int _teachingclassid;
		private int _schoolzoneid;
		/// <summary>
		/// 
		/// </summary>
		public int ScheduleID
		{
			set{ _scheduleid=value;}
			get{return _scheduleid;}
		}
		/// <summary>
		/// 学年
		/// </summary>
		public int TermID
		{
			set{ _termid=value;}
			get{return _termid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TermNo
		{
			set{ _termno=value;}
			get{return _termno;}
		}
		/// <summary>
		/// 上课周次集合（逗号分隔)
		/// </summary>
		public string WeekNumbers
		{
			set{ _weeknumbers=value;}
			get{return _weeknumbers;}
		}
		/// <summary>
		/// 单双周（0：不计，1：单周，2：双周）
		/// </summary>
		public int WeekType
		{
			set{ _weektype=value;}
			get{return _weektype;}
		}
		/// <summary>
		/// 周几
		/// </summary>
		public int WeekNum
		{
			set{ _weeknum=value;}
			get{return _weeknum;}
		}
		/// <summary>
		/// 课节编号集合（逗号分隔）
		/// </summary>
		public string LessonNos
		{
			set{ _lessonnos=value;}
			get{return _lessonnos;}
		}
		/// <summary>
		/// 教室ID
		/// </summary>
		public int ClassroomID
		{
			set{ _classroomid=value;}
			get{return _classroomid;}
		}
		/// <summary>
		/// 教学班ID
		/// </summary>
		public int TeachingClassID
		{
			set{ _teachingclassid=value;}
			get{return _teachingclassid;}
		}
		/// <summary>
		/// 校区ID
		/// </summary>
		public int SchoolZoneID
		{
			set{ _schoolzoneid=value;}
			get{return _schoolzoneid;}
		}
		#endregion Model

	}
}

