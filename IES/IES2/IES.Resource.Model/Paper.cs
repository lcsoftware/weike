/**  版本信息模板在安装目录下，可自行修改。
* Paper.cs
*
* 功 能： N/A
* 类 名： Paper
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/4 17:26:50   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace IES.Resource.Model
{
	/// <summary>
	/// 试卷库（主表）
	/// </summary>
	[Serializable]
    public partial class Paper : IPaper 
	{
		public Paper()
		{}
		#region Model
		private int _paperid;
		private int _courseid;
		private int? _ocid;
		private int? _owneruserid=0;
		private int _createuserid;
		private string _papername;
		private int _type=1;
		private int _scope=1;
		private int _sharescope=0;
		private int _timelimit=0;
		private string _brief;
		private DateTime _updatetime;
		private int _num=0;
		private decimal _score;
		private string _conten;
		private string _answer;
		private bool _isdeleted= false;
		/// <summary>
		/// 主键
		/// </summary>
		public int PaperID
		{
			set{ _paperid=value;}
			get{return _paperid;}
		}
		/// <summary>
		/// 课程编号
		/// </summary>
		public int CourseID
		{
			set{ _courseid=value;}
			get{return _courseid;}
		}
		/// <summary>
		/// 在线课程编号
		/// </summary>
		public int? OCID
		{
			set{ _ocid=value;}
			get{return _ocid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? OwnerUserID
		{
			set{ _owneruserid=value;}
			get{return _owneruserid;}
		}
		/// <summary>
		/// 试卷创建人编号
		/// </summary>
		public int CreateUserID
		{
			set{ _createuserid=value;}
			get{return _createuserid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Papername
		{
			set{ _papername=value;}
			get{return _papername;}
		}
		/// <summary>
		/// 试卷类型：1.智能型试卷  ； 2  自测型试卷 ； 3  答题卡试卷
		/// </summary>
		public int Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 1  作业与网络考试与随机训练 ；  2 学生自测  ；4  正式考试专用
		/// </summary>
		public int Scope
		{
			set{ _scope=value;}
			get{return _scope;}
		}
		/// <summary>
		/// 0 不公开、 1仅教学班学生、2同课程教学团队师生、 4同课程师生 
		/// </summary>
		public int ShareScope
		{
			set{ _sharescope=value;}
			get{return _sharescope;}
		}
		/// <summary>
		/// 试卷时长 分钟
		/// </summary>
		public int TimeLimit
		{
			set{ _timelimit=value;}
			get{return _timelimit;}
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
		/// 最后更新时间
		/// </summary>
		public DateTime UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		/// <summary>
		/// 试卷总题数
		/// </summary>
		public int Num
		{
			set{ _num=value;}
			get{return _num;}
		}
		/// <summary>
		/// 试卷总分
		/// </summary>
		public decimal Score
		{
			set{ _score=value;}
			get{return _score;}
		}
		/// <summary>
		/// 自测型试卷的试卷内容
		/// </summary>
		public string Conten
		{
			set{ _conten=value;}
			get{return _conten;}
		}
		/// <summary>
		/// 自测型试卷的习题答案
		/// </summary>
		public string Answer
		{
			set{ _answer=value;}
			get{return _answer;}
		}
		/// <summary>
		/// 0：未删除 ； 1删除（测试中复制的试卷默认值为1，不在前端显示）
		/// </summary>
		public bool IsDeleted
		{
			set{ _isdeleted=value;}
			get{return _isdeleted;}
		}
		#endregion Model

	}
}

