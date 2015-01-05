/**  版本信息模板在安装目录下，可自行修改。
* Course.cs
*
* 功 能： N/A
* 类 名： Course
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/1 13:35:31   N/A    初版
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
	/// 课程表
	/// </summary>
	[Serializable]
	public partial class Course
    {
        #region 补充信息

        public int rowscount { get; set; }
        public string OrganizationName { get; set; }
        public string CourseTypeName { get; set; }
        public string SubjectName1 { get; set; }
        public string SubjectName2 { get; set; }
        public string TeachingTypeName { get; set; }

        #endregion 

        public Course()
		{}
		#region Model
		private int _courseid;
		private string _courseno;
		private string _coursename;
		private string _coursenameen;
		private string _termno;
		private int _organizationid;
		private int _subjectid1=0;
		private int _subjectid2=0;
		private int _coursetypeID;
		private decimal? _hours;
		private decimal? _credit;
		private int _teachingtypeid=0;
		private string _introduction;
		private string _introductionen;
		private bool _isdeleted= false;
		private string _outline;
		private string _outlineen;
		private string _team;
		private string _teamen;
		private string _schedule;
		private string _scheduleen;
		/// <summary>
		/// 
		/// </summary>
		public int CourseID
		{
			set{ _courseid=value;}
			get{return _courseid;}
		}
		/// <summary>
		/// 课程编号
		/// </summary>
		public string CourseNo
		{
			set{ _courseno=value;}
			get{return _courseno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CourseName
		{
			set{ _coursename=value;}
			get{return _coursename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CourseNameEn
		{
			set{ _coursenameen=value;}
			get{return _coursenameen;}
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
		/// 开课机构
		/// </summary>
		public int OrganizationID
		{
			set{ _organizationid=value;}
			get{return _organizationid;}
		}
		/// <summary>
		/// 学科属性1
		/// </summary>
		public int SubjectID1
		{
			set{ _subjectid1=value;}
			get{return _subjectid1;}
		}
		/// <summary>
		/// 学科属性2
		/// </summary>
		public int SubjectID2
		{
			set{ _subjectid2=value;}
			get{return _subjectid2;}
		}
		/// <summary>
		/// 课程类别，示例：必修，选修，通选
		/// </summary>
		public int CourseTypeID
		{
			set{ _coursetypeID=value;}
			get{return _coursetypeID;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Hours
		{
			set{ _hours=value;}
			get{return _hours;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Credit
		{
			set{ _credit=value;}
			get{return _credit;}
		}
		/// <summary>
		/// 授课方式
		/// </summary>
		public int TeachingTypeID
		{
			set{ _teachingtypeid=value;}
			get{return _teachingtypeid;}
		}
		/// <summary>
		/// 课程简介
		/// </summary>
		public string Introduction
		{
			set{ _introduction=value;}
			get{return _introduction;}
		}
		/// <summary>
		/// 课程简介英文
		/// </summary>
		public string IntroductionEn
		{
			set{ _introductionen=value;}
			get{return _introductionen;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsDeleted
		{
			set{ _isdeleted=value;}
			get{return _isdeleted;}
		}
		/// <summary>
		/// 教学大纲中文
		/// </summary>
		public string OutLine
		{
			set{ _outline=value;}
			get{return _outline;}
		}
		/// <summary>
		/// 教学大纲英文
		/// </summary>
		public string OutLineEn
		{
			set{ _outlineen=value;}
			get{return _outlineen;}
		}
		/// <summary>
		/// 教学团队中文
		/// </summary>
		public string Team
		{
			set{ _team=value;}
			get{return _team;}
		}
		/// <summary>
		/// 教学团队英文
		/// </summary>
		public string TeamEn
		{
			set{ _teamen=value;}
			get{return _teamen;}
		}
		/// <summary>
		/// 教学进度表
		/// </summary>
		public string Schedule
		{
			set{ _schedule=value;}
			get{return _schedule;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ScheduleEn
		{
			set{ _scheduleen=value;}
			get{return _scheduleen;}
		}
		#endregion Model

	}
}

