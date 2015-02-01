/**  版本信息模板在安装目录下，可自行修改。
* TestAnswer.cs
*
* 功 能： N/A
* 类 名： TestAnswer
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
	public partial class TestAnswer
	{
		public TestAnswer()
		{}
		#region Model
		private int _answerid;
		private int _testid;
		private int _userid;
		private int _exerciseid;
		private string _conten;
		private int? _fileid=0;
		private decimal _score;
		private string _comment;
		/// <summary>
		/// 
		/// </summary>
		public int AnswerID
		{
			set{ _answerid=value;}
			get{return _answerid;}
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
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}

        /// <summary>
        /// 本习题的批阅教师编号
        /// </summary>
        public int CheckUserID { get; set; }

		/// <summary>
		/// 标准的习题编号ExerciseID；或者答题卡习题编号CardExerciseID
		/// </summary>
		public int ExerciseID
		{
			set{ _exerciseid=value;}
			get{return _exerciseid;}
		}
		/// <summary>
		/// 学生答案内容
		/// </summary>
		public string Conten
		{
			set{ _conten=value;}
			get{return _conten;}
		}
		/// <summary>
		/// 学生上传的文件
		/// </summary>
		public int? FileID
		{
			set{ _fileid=value;}
			get{return _fileid;}
		}
		/// <summary>
		/// 得分
		/// </summary>
        public decimal Score
		{
			set{ _score=value;}
			get{return _score;}
		}

		/// <summary>
		/// 评语
		/// </summary>
		public string Comment
		{
			set{ _comment=value;}
			get{return _comment;}
		}
		#endregion Model

	}
}

