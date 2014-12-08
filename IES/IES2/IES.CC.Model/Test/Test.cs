/**  版本信息模板在安装目录下，可自行修改。
* Test.cs
*
* 功 能： N/A
* 类 名： Test
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 20:23:46   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace IES.CC.Test.Model
{
	/// <summary>
	/// 作业与考试
	/// </summary>
	[Serializable]
	public partial class Test
	{
		public Test()
		{}
		#region Model
		private int _testid;
		private int _userid;
		private int _ocid;
		private int _courseid;
		private string _name;
		private DateTime _startdate;
		private DateTime _enddate;
		private int _type;
		private int _buildmode;
		private int _lesstimes=0;
		private int _moretimes=0;
		private int _passscore=0;
		private int _scoremode=1;
		private int _scoresource=1;
		private bool _showresult= false;
		private bool _showexercise= true;
		private bool _issend= false;
		private int _exerciseshowmode=0;
		private int _checkmode;
		private DateTime _updatetime= DateTime.Now;
		private int? _timeout=0;
		private bool _isdeleted= false;
		/// <summary>
		/// 
		/// </summary>
		public int TestID
		{
			set{ _testid=value;}
			get{return _testid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
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
		public int CourseID
		{
			set{ _courseid=value;}
			get{return _courseid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime StartDate
		{
			set{ _startdate=value;}
			get{return _startdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime EndDate
		{
			set{ _enddate=value;}
			get{return _enddate;}
		}
		/// <summary>
		/// 1. 作业、  2考试 、3 达标训练   
		/// </summary>
		public int Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		///  1选择试卷   2 智能选题     3 答题卡    4 附件型作业  5第三方批阅
		/// </summary>
		public int BuildMode
		{
			set{ _buildmode=value;}
			get{return _buildmode;}
		}
		/// <summary>
		/// 最少参与次数；多份试卷模式下，最少做几份试卷
		/// </summary>
		public int LessTimes
		{
			set{ _lesstimes=value;}
			get{return _lesstimes;}
		}
		/// <summary>
		/// 最多参与次数多份试卷模式下，最多做几份试卷
		/// </summary>
		public int MoreTimes
		{
			set{ _moretimes=value;}
			get{return _moretimes;}
		}
		/// <summary>
		/// 通过分数
		/// </summary>
		public int PassScore
		{
			set{ _passscore=value;}
			get{return _passscore;}
		}
		/// <summary>
		///  1 最高分 ; 2 平均分
		/// </summary>
		public int ScoreMode
		{
			set{ _scoremode=value;}
			get{return _scoremode;}
		}
		/// <summary>
		/// 成绩评定：1 教师评分；   2学生评分 ； 4 学生自评
		/// </summary>
		public int ScoreSource
		{
			set{ _scoresource=value;}
			get{return _scoresource;}
		}
		/// <summary>
		/// 提交后是否立刻显示答案
		/// </summary>
		public bool ShowResult
		{
			set{ _showresult=value;}
			get{return _showresult;}
		}
		/// <summary>
		/// 成绩发放后，是否还可以显示试卷的详细内容
		/// </summary>
		public bool ShowExercise
		{
			set{ _showexercise=value;}
			get{return _showexercise;}
		}
		/// <summary>
		/// 成绩是否已经发放
		/// </summary>
		public bool IsSend
		{
			set{ _issend=value;}
			get{return _issend;}
		}
		/// <summary>
		/// 0 不随机；  1习题随机   ； 2选项随机 ； 3习题和选项都随机
		/// </summary>
		public int ExerciseShowMode
		{
			set{ _exerciseshowmode=value;}
			get{return _exerciseshowmode;}
		}
		/// <summary>
		/// 流水批阅模式0： 非流水批阅 ； 1按人流水批阅 ； 2按习题流水批阅
		/// </summary>
		public int CheckMode
		{
			set{ _checkmode=value;}
			get{return _checkmode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		/// <summary>
		/// 0 禁止迟交； 1申请补交 ； >1 迟交扣分 （扣分5,10,15,20,25,30,35,40）八个档次
		/// </summary>
		public int? TimeOut
		{
			set{ _timeout=value;}
			get{return _timeout;}
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

