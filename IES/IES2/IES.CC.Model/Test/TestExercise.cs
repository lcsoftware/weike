/**  版本信息模板在安装目录下，可自行修改。
* TestExercise.cs
*
* 功 能： N/A
* 类 名： TestExercise
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 20:23:47   N/A    初版
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
	/// 流水批阅设置
	/// </summary>
	[Serializable]
	public partial class TestExercise
	{
		public TestExercise()
		{}
		#region Model
		private int _testexerciseid;
		private int _testid;
		private int _paperid;
		private int _exerciseid;
		private int? _orde;
		private decimal? _score;
		private string _objectivescore;
		private string _subjectivescore;
		/// <summary>
		/// 
		/// </summary>
		public int TestExerciseID
		{
			set{ _testexerciseid=value;}
			get{return _testexerciseid;}
		}
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
		public int PaperID
		{
			set{ _paperid=value;}
			get{return _paperid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ExerciseID
		{
			set{ _exerciseid=value;}
			get{return _exerciseid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Orde
		{
			set{ _orde=value;}
			get{return _orde;}
		}
		/// <summary>
		/// 分值
		/// </summary>
		public decimal? Score
		{
			set{ _score=value;}
			get{return _score;}
		}
		/// <summary>
		///  '客观题分值（答题卡）多个分值之间用逗号分隔
		/// </summary>
		public string ObjectiveScore
		{
			set{ _objectivescore=value;}
			get{return _objectivescore;}
		}
		/// <summary>
		/// 主观题分值（答题卡）多个分值之间用逗号分隔
		/// </summary>
		public string SubjectiveScore
		{
			set{ _subjectivescore=value;}
			get{return _subjectivescore;}
		}
		#endregion Model

	}
}

