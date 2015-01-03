/**  版本信息模板在安装目录下，可自行修改。
* TeachingClassStudent.cs
*
* 功 能： N/A
* 类 名： TeachingClassStudent
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/1 13:35:37   N/A    初版
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
	/// 教学班学生表，即选课表
	/// </summary>
	[Serializable]
	public partial class TeachingClassStudent
	{
		public TeachingClassStudent()
		{}
		#region Model
		private int _id;
		private int _teachingclassid;
		private int _userid;
		private int _source;
		private int _status=1;
		private DateTime? _regdate= DateTime.Now;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 教学班编号
		/// </summary>
		public int TeachingClassID
		{
			set{ _teachingclassid=value;}
			get{return _teachingclassid;}
		}
		/// <summary>
		/// 学生编号
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
        /// 添加人员编号， 1 教务系统数据联动 ； 2 为管理员；  3为教师本人添加的教学班学生；4表示学生自己注册的
		/// </summary>
		public int Source
		{
			set{ _source=value;}
			get{return _source;}
		}
		/// <summary>
        /// 审核状态 ：审核状态 ：0拒绝， 1待审核 ，2通过
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 注册日期
		/// </summary>
		public DateTime? RegDate
		{
			set{ _regdate=value;}
			get{return _regdate;}
		}


        /// <summary>
        /// 驳回理由
        /// </summary>
        public string Reason { get; set; }



		#endregion Model

	}
}

