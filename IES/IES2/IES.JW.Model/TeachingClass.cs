/**  版本信息模板在安装目录下，可自行修改。
* TeachingClass.cs
*
* 功 能： N/A
* 类 名： TeachingClass
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/1 13:35:36   N/A    初版
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
	/// 专业推荐教师
	/// </summary>
	[Serializable]
	public partial class TeachingClass
    {
        #region 补充信息
        public int StudentsNumber { get; set; }
        public int MainUserID { get; set; }
        public string OtherUserIDS { get; set; }
        public int rowscount { get; set; }
        public string CourseName { get; set; }
        public string UserName { get; set; }
        public string OrganizationName { get; set; }
        public string Key { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        #endregion
        public TeachingClass()
		{}
		#region Model
		private int _teachingclassid;
		private string _classno;
		private string _classname;
		private int? _termid=0;
		private int _courseid;
		private int? _startweek;
		private DateTime? _startdate;
		private int? _endweek;
		private DateTime? _enddate;
		private int _source=0;
		private bool _isdeleted= false;
		/// <summary>
		/// 
		/// </summary>
		public int TeachingClassID
		{
			set{ _teachingclassid=value;}
			get{return _teachingclassid;}
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
		/// 0表示 教师自己创建的教学班 或者MOOC对外教学班
		/// </summary>
		public int? TermID
		{
			set{ _termid=value;}
			get{return _termid;}
		}


        public int? OrganizationID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int CourseID
		{
			set{ _courseid=value;}
			get{return _courseid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? StartWeek
		{
			set{ _startweek=value;}
			get{return _startweek;}
		}
		/// <summary>
		/// 开始时间
		/// </summary>
		public DateTime? StartDate
		{
			set{ _startdate=value;}
			get{return _startdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? EndWeek
		{
			set{ _endweek=value;}
			get{return _endweek;}
		}
		/// <summary>
		/// 结束时间
		/// </summary>
		public DateTime? EndDate
		{
			set{ _enddate=value;}
			get{return _enddate;}
		}
		/// <summary>
        /// 添加来源，1  教务系统数据联动 ；2 管理员添加；  3：教师本人添加的教学班学生；
		/// </summary>
		public int Source
		{
			set{ _source=value;}
			get{return _source;}
		}


        /// <summary>
        /// 教学班学生总数
        /// </summary>
        public int StudentCount { get; set; }
        
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

