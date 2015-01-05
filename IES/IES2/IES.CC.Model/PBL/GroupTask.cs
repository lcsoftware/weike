/**  版本信息模板在安装目录下，可自行修改。
* GroupTask.cs
*
* 功 能： N/A
* 类 名： GroupTask
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/14 17:44:58   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace IES.CC.Model.PBL
{
	/// <summary>
	/// 小组任务表
	/// </summary>
	[Serializable]
	public partial class GroupTask
	{
		public GroupTask()
		{}

        #region 补充信息

        /// <summary>
        /// 参与任务的小组数量
        /// </summary>
        public int groupcount { get; set; }

        /// <summary>
        /// 当前任务的提交信息
        /// </summary>
        public int submitinfo { get; set; }

        #endregion 


        #region Model
        private int _taskid;
		private int _ocid;
		private int? _courseid;
		private string _taskname;
		private int _resultrequire=2;
		private int? _termsid=0;
		private DateTime? _startdate;
		private DateTime? _submittime;
		private DateTime? _enddate;
		private string _introduction;
		private int _userid;
		private DateTime _createtime= DateTime.Now;
		private int? _isallowseeothergroup=1;
		private bool _isoctask= false;
		private int? _isdeleted=0;
		/// <summary>
		/// 教学任务编号
		/// </summary>
		public int TaskID
		{
			set{ _taskid=value;}
			get{return _taskid;}
		}
		/// <summary>
		/// 在线课程编号
		/// </summary>
		public int OCID
		{
			set{ _ocid=value;}
			get{return _ocid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CourseID
		{
			set{ _courseid=value;}
			get{return _courseid;}
		}
		/// <summary>
		/// 教学任务名称
		/// </summary>
		public string TaskName
		{
			set{ _taskname=value;}
			get{return _taskname;}
		}
		/// <summary>
		/// 成果要求： 1 每组提交一份，2 每人提交一份
		/// </summary>
		public int ResultRequire
		{
			set{ _resultrequire=value;}
			get{return _resultrequire;}
		}
		/// <summary>
		/// 学期编号 对应表tTerms中的fTermsID
		/// </summary>
		public int? TermsID
		{
			set{ _termsid=value;}
			get{return _termsid;}
		}
		/// <summary>
		/// 开始日期
		/// </summary>
		public DateTime? StartDate
		{
			set{ _startdate=value;}
			get{return _startdate;}
		}
		/// <summary>
		/// 成果最后提交时间，必须大于=结束时间
		/// </summary>
		public DateTime? SubmitTime
		{
			set{ _submittime=value;}
			get{return _submittime;}
		}
		/// <summary>
		/// 结束日期
		/// </summary>
		public DateTime? EndDate
		{
			set{ _enddate=value;}
			get{return _enddate;}
		}
		/// <summary>
		/// 任务介绍
		/// </summary>
		public string Introduction
		{
			set{ _introduction=value;}
			get{return _introduction;}
		}
		/// <summary>
		/// 任务发起 人
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}

        public int GroupModeID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 结束日期前，是否允许小组之间互相查看讨论与评论（1是，0否）
		/// </summary>
		public int? IsAllowSeeOtherGroup
		{
			set{ _isallowseeothergroup=value;}
			get{return _isallowseeothergroup;}
		}
		/// <summary>
		/// 0:PBL任务 1:在线课程的其他插入小组任务
		/// </summary>
		public bool IsOCTask
		{
			set{ _isoctask=value;}
			get{return _isoctask;}
		}
		/// <summary>
		/// 是否删除 1是 0否
		/// </summary>
		public int? IsDeleted
		{
			set{ _isdeleted=value;}
			get{return _isdeleted;}
		}







		#endregion Model

	}
}

